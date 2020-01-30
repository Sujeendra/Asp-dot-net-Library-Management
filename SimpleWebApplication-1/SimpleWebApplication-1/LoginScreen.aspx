<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginScreen.aspx.cs" Inherits="SimpleWebApplication_1.LoginScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="LoginScreen.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
        <div id="div1">
             <h1>Library Management System</h1>
        <asp:Login runat="server" OnAuthenticate="Login1_Authenticate" ID="Login1" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" Height="231px" style="margin-left: 0px; margin-top: 0px" Width="430px">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            
        </asp:Login>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Register</asp:LinkButton>
            </div>
    </form>
</body>
</html>
