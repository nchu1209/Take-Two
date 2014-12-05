<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerForgotPassword.aspx.vb" Inherits="KProject.CustomerForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Longhorn Bank</title>
    <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerForgotPassword.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="title">
            Forgot Password?
        </div>

        <div id ="content">

            <div id="content">
            <div id="label">

                <asp:Label ID="Label1" runat="server" Text="Email:"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Birth Year:"></asp:Label>

                </div>
            <div id="textbox">

                <asp:TextBox ID="txtEmail" runat="server" Height="25px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="ERROR: You must enter your email.">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtBirthYear" runat="server" Height="25px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirthYear" ErrorMessage="ERROR: You must enter a birth year.">*</asp:RequiredFieldValidator>
                <br />

            </div>

                </div>
            <div id="content">

                <asp:Button ID="btnRequest" runat="server" Text="Submit Request for Password" />
                <br />

                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <br />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

                </div>

            
    </div>
    </form>
</body>
</html>
