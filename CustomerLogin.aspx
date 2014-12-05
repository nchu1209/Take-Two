<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerLogin.aspx.vb" Inherits="KProject.CustomerLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Longhorn Bank</title>
    <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerLogin.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 233px;
            height: 83px;
        }
        .auto-style2 {
            width: 128px;
            height: 128px;
        }
        .auto-style3 {
            width: 168px;
            height: 130px;
        }
        .auto-style4 {
            width: 149px;
            height: 127px;
        }
        .auto-style5 {
            width: 196px;
            height: 127px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlLogin" runat="server">
        <div id="lefttopcorner">
            <img class="auto-style1" src="Petunia2.jpg" /><br />
            <br />
            <br />
            <br />
            Email
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <br />
            Password
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="Password is Required">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" PostBackUrl="~/CustomerCreateAccount.aspx">Apply for an Account!</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkForgotPassword" runat="server" CausesValidation="False" PostBackUrl="~/CustomerForgotPassword.aspx">Forgot Password? </asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkEmployeeHome" runat="server" CausesValidation="False" PostBackUrl="~/EmployeeLogin.aspx">Employee? Login Here!</asp:LinkButton>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <br />
        </div>
       
        <div id="rightcornertitle">
            <div id="title">
                <br />
                Welcome to Longhorn Bank&nbsp;
                <br />
            </div>
            <div id="title100">
                <br />
                Choose the Account that works best for you!
            <br />
            </div>
        </div>



        <div id="rightcornercontent">
             <div id="a">
                 <br />
                 <br />
                 <b>Checking Accounts</b><br />
                 <br />
                 <img alt="" class="auto-style3" src="checkingaccounts.png" /><br />
                 <br />
                 <br />
                 A single, simple checking account<br />
                 <br />
                 Transfer money between accounts for free<br />
                 <br />
                 Free access to thousands of Longhorn Bank ATMs<br />
                 <br />
            <br />
        </div>

         <div id="b">
             <br />
             <br />
             <b>Savings Accounts</b><br />
            <br />
             <img alt="" class="auto-style4" src="Piggy%20bank.png" /><br />
             <br />
             <br />
             Savings accounts to fit your individual and family goals<br />
             <br />
             Whether you&#39;re just starting out or saving for a child&#39;s college, feel confident in Longhorn Bank savings
             <br />
             <br />
             <br />
            <br />
        </div>
             <div id="c">
                 <br />
                 <br />
                 <b>IRAs</b><br />
                 <br />
                 <img alt="" class="auto-style5" src="ira.png" /><br />
                 <br />
                 <br />
                 IRAs give you tax advantages for long-term individual retirement savings<br />
                 <br />
                 Deposit up to $5,000 a year until age 70 for maximum savings<br />
                 <br />
                 <br />
                 <br />
                 <br />
        </div>
             <div id="d">
                 <br />
                 <br />
                 <b>Stock Portfolios</b><br />
                 <br />
                 <img alt="" class="auto-style2" src="app-stock-icon.png" /><br />
                 <br />
                 <br />
                 Buy and sell stocks offered through Longhorn Bank<br />
                 <br />
                 Now including: Ordinary Stocks, Index Funds, Mutual Funds, ETFs, and Futures Shares<br />
                 <br />
                 <br />
                 <br />
                 <br />
        </div>


        </div>
    </asp:Panel>
        
        
        <br />
        <br />
        <asp:Panel ID="pnlNoLongerCustomer" runat="server">
            <br />
            <img alt="" class="auto-style1" src="Petunia2.jpg" />
            <br/>
            <br/>
            <div id="contentBIG" class="auto-style2">
                   Hello
            <asp:Label ID="lblDisabled" runat="server" Text=""></asp:Label>
                </div>

            
        </asp:Panel>
         <div id="footer">
        <br />
        <br />
        Website Created by One or None, Ltd.
        <br />
        Group 3: Leah Carroll, Nicole Chu, Amy Enrione, Catherine King
             </div>
    

    </form>
  

 
  

    </body>
</html>
