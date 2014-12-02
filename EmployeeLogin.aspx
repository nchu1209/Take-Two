<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeLogin.aspx.vb" Inherits="KProject.EmployeeLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Longhorn Bank</title>
    <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/EmployeeLogin.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .auto-style1 {
            width: 233px;
            height: 83px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:Panel ID="Login" runat="server">
    
        <div id="lefttopcorner">
            <img class="auto-style1" src="Petunia2.jpg" /><br />
            <br />
            <br />
        </div>
       
  
        <div id="employeeloginright">
             <div id ="title">
        Welcome Employees!
    </div>

            <br />
            <br />
            Employee ID
            <br />
            <asp:TextBox ID="txtEmployeeID" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmployeeID" ErrorMessage="Need Employee ID">*</asp:RequiredFieldValidator>
            <br />
            Password
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtPassword" ErrorMessage="Password is Required">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" />
            <br />
            <br />
            <asp:Label ID="lbltesting" runat="server"></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>

        </div>
        </asp:Panel>

<asp:Panel ID="Fired" runat="server">
            <br />
            <img alt="" class="auto-style1" src="Petunia2.jpg" />
            <br />
            <br />
            <br />
            <br />
            <div id="contentBIG">
                We are sorry--you no longer work for Longhorn Bank. Your access has been denied.
            </div>
 
    </asp:Panel>


        <div id="footer">
        <br />
            <br />
            <br />
            <br />
             <br />
        Website Created by One or None, Ltd.
        <br />
        Group 3: Leah Carroll, Nicole Chu, Amy Enrione, Catherine King
             </div>



    </form>

</body>
</html>
