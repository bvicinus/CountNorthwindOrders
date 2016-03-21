//created by: brandon vicinus (3/21/16)
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

//static class, no constructor
public static class Query
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public static List<Customer> Get_Customer(string company_name, out string error_msg)
    {
        error_msg = "";
        SqlDataReader rdr_Customer = null;
        SqlConnection cn_Customer = null;
        Customer customer1 = null;
        List<Customer> list1 = new List<Customer>();

        try
        {
            //setup connections 
            cn_Customer = Setup_Connection_Customers();

            //setup readers , find customer given company name
            rdr_Customer = Get_Reader_Customer(company_name, cn_Customer);
            while(rdr_Customer.Read())
            {
                customer1 = new Customer(rdr_Customer);
                if(customer1 != null)
                {
                    list1.Add(customer1);
                }
                else
                {
                    error_msg = "Customer Lookup Failed (line 37)";
                }
            }//end while
            
        }
        catch (Exception ex)
        {
            error_msg = "ERROR: " + ex.Message + " (line 44)";
        }
        finally //close all connections, if they arent already null
        {
            if (rdr_Customer != null) { rdr_Customer.Close(); }
            if (cn_Customer != null) { cn_Customer.Close(); }

        }//end try, catch, finally 

        return list1;

    }//end get_customer

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public static Order Get_Order(string customerID, out string error_msg)
    {
        error_msg = "";
        SqlDataReader rdr_Order = null;
        SqlConnection cn_Order = null;
        Order order1 = null;

        try
        {
            //setup connection

            cn_Order = Setup_Connection_Order();

            //setup reader
            //find order given customerID
            rdr_Order = Get_Reader_Order(customerID, cn_Order);
            if (rdr_Order.Read()) //only 1 order to read, only need an if statement 
            {
                order1 = new Order(rdr_Order);
            }
            else
            {
                error_msg = "Customer Order Lookup Failed ";
            }

        }
        catch (Exception ex)
        {
            error_msg = "ERROR: " + ex.Message;
        }
        finally //close all connections, if they arent already null
        {
            if (rdr_Order != null) { rdr_Order.Close(); }
            if (cn_Order != null) { cn_Order.Close(); }
        }//end try, catch, finally 

        return order1;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


    public static List<OrderDetail> get_OrderDetail(int orderID, out string error_msg)
    {
        error_msg = "";
        SqlDataReader rdr_OrderDetail = null;
        SqlConnection cn_OrderDetail = null;
        OrderDetail orderDetail1 = null;
        List<OrderDetail> list2 = new List<OrderDetail>(); 

        try
        {
            //setup connection
            cn_OrderDetail = Setup_Connection_OrderDetail();

            //lookup unitprice, quantity, discount given orderID
            rdr_OrderDetail = Get_Reader_OrderDetail(orderID, cn_OrderDetail);
            while(rdr_OrderDetail.Read())
            {
                orderDetail1 = new OrderDetail(rdr_OrderDetail);
                if(orderDetail1 != null)
                {
                    list2.Add(orderDetail1);
                }
                else
                {
                    error_msg = "OrderID Lookup Failed (line 125)";
                }
            }//end while loop 
        }
        catch (Exception ex)
        {
            error_msg = "ERROR: " + ex.Message;
        }
        finally //close all connections, if they arent already null
        {
            if (rdr_OrderDetail != null) { rdr_OrderDetail.Close(); }
            if (cn_OrderDetail != null) { cn_OrderDetail.Close(); }

        }//end try, catch, finally 

        return list2;
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public static SqlConnection Setup_Connection_Customers()
    {
        string connection_string = WebConfigurationManager.ConnectionStrings["CustomerTable"].ConnectionString;
        SqlConnection cn = new SqlConnection(connection_string);
        cn.Open();
        return cn;

    }

    public static SqlConnection Setup_Connection_Order()
    {
        string connection_string = WebConfigurationManager.ConnectionStrings["OrderTable"].ConnectionString;
        SqlConnection cn = new SqlConnection(connection_string);
        cn.Open();
        return cn;
    }

    public static SqlConnection Setup_Connection_OrderDetail()
    {
        string connection_string = WebConfigurationManager.ConnectionStrings["OrderDetailTable"].ConnectionString;
        SqlConnection cn = new SqlConnection(connection_string);
        cn.Open();
        return cn;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        
    public static SqlDataReader Get_Reader_Customer(string companyName, SqlConnection cn)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT customerid, companyname FROM [Customers] WHERE CompanyName LIKE @comp_name";
        cmd.Parameters.AddWithValue("@comp_name", companyName + "%"); //the % accounts for partial strings 
        cmd.Connection = cn; 
        return cmd.ExecuteReader();
    }


    
    public static SqlDataReader Get_Reader_Order(string customerID, SqlConnection cn)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT orderid, customerid FROM [Orders] WHERE CustomerID=@cust_id";
        cmd.Parameters.AddWithValue("@cust_id", customerID);
        cmd.Connection = cn; 
        return cmd.ExecuteReader();
    }
    

    
    public static SqlDataReader Get_Reader_OrderDetail(int orderID, SqlConnection cn)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT orderid, unitprice, quantity, discount FROM [Order Details] WHERE OrderID=@order_id";
        cmd.Parameters.AddWithValue("@order_id", orderID);
        cmd.Connection = cn; 
        return cmd.ExecuteReader();
    }


}