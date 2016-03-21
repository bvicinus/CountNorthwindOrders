//created by: brandon vicinus (3/21/16)
using System.Data.SqlClient;

public class Order
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private int orderid;
    private string customerid;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //only 'get' access available 
    public int Orderid
    {
        get
        {
            return orderid;
        }
    }

    public string Customerid
    {
        get
        {
            return customerid;
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public Order(SqlDataReader rdr)
    {
        orderid = (int)rdr["OrderID"];
        customerid = (string)rdr["CustomerID"];
    }
}