<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerUserManual.aspx.vb" Inherits="KProject.CustomerUserManual" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title100">
        <br />
        View questions about: <asp:DropDownList ID="ddlHelp" runat="server" AutoPostBack="True">
            <asp:ListItem>Select...</asp:ListItem>
            <asp:ListItem>Dispute a Transaction</asp:ListItem>
            <asp:ListItem>Creating Bank Accounts</asp:ListItem>
            <asp:ListItem>Modifying Your Account</asp:ListItem>
            <asp:ListItem>Stocks</asp:ListItem>
        </asp:DropDownList>
        <br />
    </div>
     <br />
     <br />
     <br />
     <asp:Panel ID="DisputesPanel" runat="server">
         <br />
         <br />
        <div id="content">
            Transactions are created with every withdrawal, deposit, transfer, fee, etc.  In order to view your <br />
            transactions for an account, navigate to 'My Accounts' then click on 'View My Accounts'. Select the account you would<br />
            like to view by clicking on the 'Account Name' in the table.  Then, hit the 'Transaction Search/ Details' button on this page.<br />
            Next, click on the 'Select' cell of the table under View Details. This brings you to the transaction details page. <br />
            If you would like to dispute the transaction, please click on the 'Create Dispute' button.<br /><br />
            Note: if a dispute is submitted or resolved for a transaction, you may not submit another.
        </div>

    </asp:Panel>
    
    <asp:Panel ID="CreatingAccountPanel" runat="server">
        <br />

        <div id="content">
            In order to create bank accounts click on 'My Accounts' and choose the 'Apply for Account' button. <br />
            Enter the information.  Please note that you can create only one IRA account and one stock account per customer.
        </div>

    </asp:Panel>

     <asp:Panel ID="ModifyingYourAccountPanel" runat="server">
         <br />
        <div id="content">
            In order to modify your account,  in the case of an address, name, or contact information change, or  <br />
            to update the name of your accounts, navigate to the 'My Accounts' tab in the navigation bar and click on 'Manage Profile'.  <br />
            Click 'Save' after you have made your changes.
        </div>

    </asp:Panel>

    
    <asp:Panel ID="StocksPanel" runat="server">
        <br />
        <div id="content">
            In order to purchase stocks, click on the 'Stocks' tab in the navigation bar.  Choose 'Buy Stocks'. <br />
            Enter in the quantity of stocks you prefer.  Be sure to select the date you would like the purchase to go through.<br />
            Note that there is a fee for each type of stock bought, but not for quantity.<br />
            You can view your transaction purchases on the day you chose for the stock purchase to go through. 
        </div>
    </asp:Panel>

</asp:Content>
