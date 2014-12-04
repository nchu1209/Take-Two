 <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerAccountDetail.aspx.vb" Inherits="KProject.CustomerAccountDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerAccountDetail.aspx" />
        <div id="title">
            Account Detail<br />
            <asp:Label ID="lblAccountName" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <div id="subtitle">
            <asp:Button ID="btnTransactionSearch" runat="server" Text="Transaction Search/Details" />
            <br />
            </div>
            <br />
        </div>
    <div id ="footer">
        <div id="subtitle">Transactions Awaiting Manager Approval</div>
            <asp:GridView ID="gvTransactionsAwaitingManagerApproval" runat="server">
            </asp:GridView>
        </div>
        <div id ="footer">
            <div id="subtitle">View All Transactions for this Account</div>
            <asp:GridView ID="gvViewAllTransactions" runat="server">
            </asp:GridView>
        </div>


    </asp:Content>