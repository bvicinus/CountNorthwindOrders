//created by: brandon vicinus (3/21/16)
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    List<Customer> customer_list = new List<Customer>();
    List<OrderDetail> orderDetail_list = new List<OrderDetail>();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblOutput.Text = "";
        tbCompName.Focus();


    }//end Page_Load

    //tb is autopostback and autofocused 
    protected void tbCompName_TextChanged(object sender, EventArgs e)
    {
        //clear the ddl everytime the text is changed 
        ddlCompanies.Items.Clear();

        if (tbCompName.Text == "") //if the text box is empty, restore default page 
        {
            lblOutput.Text = "";
            ddlCompanies.Enabled = false;
            return; 
        }
        string companyName = tbCompName.Text;
        
        string errMsg = "Failed Company Lookup";
        customer_list = Query.Get_Customer(companyName, out errMsg);

        if (customer_list.Count == 0)  //no matching results for company name entered 
        {
            lblOutput.Text = "There are no customers matching " + tbCompName.Text;
            ddlCompanies.Enabled = false;
            return;
        }
        else if (customer_list.Count == 1) //if only 1 match is found, display the message 
        {
            string errMsg1 = "Failed Customer Order Lookup";
            string errMsg2 = "Failed Order Detail Lookup";

            Order order1 = Query.Get_Order(customer_list[0].Customerid, out errMsg1); //query the order
           
            orderDetail_list = Query.get_OrderDetail(order1.Orderid, out errMsg2);
            
            lblOutput.Text = customer_list[0].Companyname + " has orders totaling " 
                + Net_Cost_Helper(orderDetail_list[0].Uniteprice, orderDetail_list[0].Quantity, orderDetail_list[0].Discount);

            return; 
        }

        //add dummy entry to ddl
        ListItem li = new ListItem("Select a company name", "");
        ddlCompanies.Items.Add(li);

        //enable the ddl 
        ddlCompanies.Enabled = true;

        foreach (Customer tempCustomer in customer_list)
        {
            ListItem li1 = new ListItem(tempCustomer.Companyname, tempCustomer.Customerid);
            ddlCompanies.Items.Add(li1); //populate the dropdown list 
        }

    } //end function tbCompName_TextChanged

    //ddl is autopostback 
    protected void ddlCompanies_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCompanies.SelectedIndex == 0) { return; }  //error, invalid selection 

        string errMsg = "Failed Customer Order Lookup";
        string errMsg2 = "Failed Order Detail Lookup";

        Order order1 = Query.Get_Order(ddlCompanies.SelectedValue, out errMsg); //query the order

        orderDetail_list = Query.get_OrderDetail(order1.Orderid, out errMsg2);  //query the orderDetail list 
        if(orderDetail_list.Capacity == 0)
        {
            lblOutput.Text = "Invalid OrderDetails Lookup (line 87)";
            return;
        } 

        Decimal net_cost = 0;

        foreach (OrderDetail tempOrderDetail in orderDetail_list) //calculate the net cost 
        {
            net_cost += Net_Cost_Helper(tempOrderDetail.Uniteprice, tempOrderDetail.Quantity, tempOrderDetail.Discount);
        }

        //display output message 
        lblOutput.Text = ddlCompanies.SelectedItem.ToString() + " has orders totaling " + net_cost.ToString();

    } //end function ddlCompanies_SelectedIndexChanged

    //helper function to calculate net cost 
    public static Decimal Net_Cost_Helper(Decimal unit_price, int quantity, Decimal discount)
    {
        return ((Decimal.Round(unit_price * quantity, 2)) - (Decimal.Round((Decimal.Round(unit_price * quantity, 2)) * (Decimal)discount, 2)));
    }

} //end class