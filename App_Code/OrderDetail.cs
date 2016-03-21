//created by: brandon vicinus (3/21/16)
using System;
using System.Data.SqlClient;

public class OrderDetail
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private int orderid;
    private Decimal uniteprice;
    private int quantity;
    private Decimal discount;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //all variables only have 'get' access
        //doesnt make logical sense to be allowed to change any 
        //data in a lookup system app 

    public int Orderid
    {
        get
        {
            return orderid;
        }
    }

    public Decimal Uniteprice
    {
        get
        {
            return uniteprice;
        }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }
    }

    public Decimal Discount
    {
        get
        {
            return discount;
        }
    }

    public OrderDetail(SqlDataReader rdr)
    {
        orderid = (int)rdr["OrderID"];
        uniteprice = (Decimal)rdr["UnitPrice"];
        quantity = (int)(Int16)rdr["Quantity"];
        discount = System.Convert.ToDecimal(rdr["Discount"]);
    }
}