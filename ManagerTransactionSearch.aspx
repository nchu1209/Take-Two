<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerTransactionSearch.aspx.vb" Inherits="KProject.ManagerTransactionSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/EmployeeManageCustomers.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    
       

    
    <div id ="title">
        View Transactions
        <br />
    </div>

    <asp:Panel ID="CustomerSelect" runat="server" Visible ="true">
    <div id="subtitle">
        Select a customer to view transactions for
    </div>
    <div id="footer">
        <asp:GridView ID="gvCustomers" runat="server">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    </asp:Panel>
    
    <asp:Panel ID="PanelAccounts" runat="server" Visible ="true">
    <div id="subtitle">
        Select an account for this customer to view transactions for
    </div>
    <div id="footer">
        <asp:GridView ID="gvAccounts" runat="server">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:Button ID=btnSwitch runat="server" Text="Choose a different customer" />
    </div>
    </asp:Panel>
</asp:content>
