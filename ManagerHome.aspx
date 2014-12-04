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
        ^no coding behind yet (line 408)</div>
    
    
    
    <br />
    <br />
    <asp:Panel ID="ProcessingConfirmation" runat="server">
        <div id="content">
        <asp:Label ID="lblConfirmation" runat="server" Text="The processing of the stock portfolios was successful."></asp:Label>
        </div>
    </asp:Panel>
    ^Panel to become visible when successful.<br />
    <br />
    Still need to make sure all of this is coded.<br />
    Tasks also include approving large deposits and disputes that need to be resolved. 

    <br />
    <br />
     <div id ="Div1">
        View eBills<br />
        <br />
    </div>
    <div id="content">
    <b><asp:Label ID="Label1" runat="server" Text="Tasks"></asp:Label></b>

    </div>
    <div id ="center">
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFilter" runat="server" Text="Resolve Disputes" PostBackUrl="~/ManagerResolveDisputes.aspx"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnActive" runat="server" Text="Approve Deposits" PostBackUrl="~/ManagerApproveDeposits.aspx" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
        <br />
</asp:Content>
