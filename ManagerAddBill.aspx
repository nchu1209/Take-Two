<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerAddBill.aspx.vb" Inherits="KProject.ManagerAddBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Add a New eBill<br />
        <br />
    </div>
    <div id ="label2">
        <asp:Label ID="Label3" runat="server" Text="Select Payee:"></asp:Label>
        <br />
        <asp:Label ID="lblCustomer" runat="server" Text="Select Customer:"></asp:Label>
        <br />
        <br />
        <br />
&nbsp;</div>
    <div id ="textbox">
        <asp:DropDownList ID="ddlPayee" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="ddlCustomer" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm Selection" />
        <br />
        <br />
        <br />
        <br />
    </div>


</asp:Content>
