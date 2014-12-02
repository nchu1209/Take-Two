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

</asp:Content>
