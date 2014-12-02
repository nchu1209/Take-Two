<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerCreateBankAccount.aspx.vb" Inherits="KProject.CustomerCreateBankAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerCreateBankAccount.aspx" />
     <div id ="title">
        Create a Bank Account
         <br />
    
    </div>

  
    
        <br />
       
        <%-- Make sure to not have the 2 accounts the same --%>
        <%-- because the money comes from an outside source, any amount is allowed --%>
        <div id ="createbankaccountfiller">

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
           

        </div>
         <div id ="label">
             <asp:Label ID="Label3" runat="server" Text="Account type: "></asp:Label>
             <br />
              <asp:Label ID="Label1" runat="server" Text="Account Name: "></asp:Label>
             <br />

             <asp:Label ID="Label2" runat="server" Text="Account Number: "></asp:Label>
             <br />

             <asp:Label ID="Label7" runat="server" Text="Initial Deposit:"></asp:Label>
             <br />
             <br />
             <%-- Default to Current Date --%>
             <br />
          </div>
        
             <div id="textbox">
                  <asp:DropDownList ID="ddlBankAccounts" runat="server" AutoPostBack="True">
            <asp:ListItem>Pick an account type</asp:ListItem>
            <asp:ListItem>Checking Account</asp:ListItem>
            <asp:ListItem>Savings Account</asp:ListItem>
            <asp:ListItem>Individual Retirement Account</asp:ListItem>
            <asp:ListItem>Stock Portfolio</asp:ListItem>
        </asp:DropDownList>
                <br />
                 <asp:TextBox ID="txtAccountName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtAccountName" ErrorMessage="ERROR: Account Name Required">*</asp:RequiredFieldValidator>
                   <br />
                 <asp:TextBox ID="txtAccountNumber" runat="server" ReadOnly="True"></asp:TextBox>
              
                 <br />
                 <asp:TextBox ID="txtInitialDeposit" runat="server"></asp:TextBox>
              
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ControlToValidate="txtInitialDeposit" ErrorMessage="ERROR: Initial Deposit Required">*</asp:RequiredFieldValidator>
              
                 <br />
                 <br />
             
                  <br />
                  <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Button ID="btnApply" runat="server" Text="Apply" />
            <asp:Button ID="btnCancelProfile" runat="server" Text="Cancel" CausesValidation="False" />
                  <br />
                  <br />
                  <asp:Label ID="lblError" runat="server"></asp:Label>
             
                 <br />
                  <br />
                 <br />
                  <br />
        
             </div>

        <br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />

        <br />



     <br />
     <br />

     <br />
     <br />
     <br />

</asp:Content>
