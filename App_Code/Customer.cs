//created by: brandon vicinus (3/21/16)
using System.Data.SqlClient;

public class Customer
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private string customerid;
    private string companyname;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    
        //encapsulated fields (edit->refractor->encapsulate field)
        //dont want to be able to 'set'either the customer id or the company name

    public string Customerid
    {
        get
        {
            return customerid;
        }
    }

    public string Companyname
    {
        get
        {
            return companyname;
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //constructor
    public Customer(SqlDataReader rdr)
    {
        //assign private variables to the data in the sql table
        customerid = (string)rdr["CustomerID"];
        companyname = (string)rdr["CompanyName"];

    }
}