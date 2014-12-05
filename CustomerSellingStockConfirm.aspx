<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="CustomerSellingStockConfirm.aspx.vb" Inherits="KProject.CustomerSellingStockConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="title">
        Confirm Stock Sale
    </div>

    <div id="label">
        Ticker <br />
        Number of Shares Sold <br />
        Remaining Shares <br />
        Fees <br />
        Profit from Sale
    </div>
    <div id="label">
        <asp:Label ID="lblTicker" runat="server" Text=""></asp:Label>  <br />
        <asp:Label ID="lblSharesSold" runat="server" Text=""></asp:Label> <br />
        <asp:Label ID="lblRemainingShares" runat="server" Text=""></asp:Label> <br />
        <asp:Label ID="lblFees" runat="server" Text=""></asp:Label> <br />
        <asp:Label ID="lblProfit" runat="server" Text=""></asp:Label> <br />
    </div>
    <div id="center">
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
</asp:Content>
