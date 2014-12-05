<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerHome.aspx.vb" Inherits="KProject.ManagerHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div id="title">
        Manager Home
    </div>
    <br />
    
    <div id="content">
        <asp:Button ID="btnProcessPortfolios" runat="server" Text="Process Stock Portfolios" />

        <br />
        </div>
    
    
    
    <br />
    <br />
    <asp:Panel ID="ProcessingConfirmation" runat="server">
        <div id="content">
        <asp:Label ID="lblConfirmation" runat="server" Text="The processing of the stock portfolios was successful."></asp:Label>
        </div>
    </asp:Panel>
    <br />
    <div id ="Div1">
        <br />
        <br />
    </div>
    <div id="subtitle">
    <b><asp:Label ID="Label1" runat="server" Text="Manager Tasks"></asp:Label></b>

    </div>
    <div id ="center">
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFilter" runat="server" Text="Resolve Disputes" PostBackUrl="~/ManagerResolveDisputes.aspx"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnActive" runat="server" Text="Approve Deposits" PostBackUrl="~/ManagerApproveDeposits.aspx" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" Text="Approve Stock Accounts" PostBackUrl ="~/ManagerApproveStockAccounts.aspx" />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
</asp:Content>
