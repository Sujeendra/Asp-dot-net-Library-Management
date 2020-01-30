<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Librarian.aspx.cs" Inherits="SimpleWebApplication_1.Librarian" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="bootstrap.min.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <br />
        <br />
        &nbsp;Librarian</div>
        <asp:GridView ID="GridView1" runat="server" Width="1284px" OnSelectedIndexChanged="DeleteEvent" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:CommandField SelectText="Delete" ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
        Enter Name of the Book :&nbsp;
        <asp:TextBox ID="TextBox3" runat="server" height="27px" width="297px"></asp:TextBox>
        <p>
            Enter Author of the Book:
            <asp:TextBox ID="TextBox1" runat="server" Width="297px"></asp:TextBox>
        </p>
        <p>
            Enter the Book Count&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : <asp:TextBox ID="TextBox4" runat="server" height="27px" width="30px"></asp:TextBox>
        </p>
        
            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
        
        <br />
        
    </form>
</body>
</html>
