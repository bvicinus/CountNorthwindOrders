<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Count NW Orders</title>
    <link rel="stylesheet" href="StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <h1>  
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" 
                Font-Names="Calibri" Font-Size="XX-Large" ForeColor="Blue" 
                Text="Northwind Traders Orders"></asp:Label>
        </h1>

        <h2>

            <center>

            <asp:Label ID="lbl1" runat="server" Text="Company Name" Font-Names="Calibri"></asp:Label>
            <br />
            <asp:Label ID="lbl2" runat="server" Text="One or more initial letters" Font-Names="Calibri"></asp:Label>

            </center>
        </h2>
        
        <p>

            <center>

            <asp:TextBox ID="tbCompName" runat="server" AutoPostBack="True"
                 style="margin-left: 0px" Width="150px" OnTextChanged="tbCompName_TextChanged"></asp:TextBox>

            &nbsp;&nbsp;&nbsp;

            <asp:DropDownList ID="ddlCompanies" runat="server" AutoPostBack="True" 
                Enabled="False" Height="16px" Width="147px" OnSelectedIndexChanged="ddlCompanies_SelectedIndexChanged" TabIndex="1">
            </asp:DropDownList>

            <br />
            <br />
            <br />



            <asp:Label ID="lblOutput" runat="server" Font-Names="Calibri" Font-Bold="True"></asp:Label>

            </center>

        </p>

       

        
    <div>
    
    </div>
    </form>
</body>
</html>
