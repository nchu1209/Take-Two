<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerAddBills.aspx.vb" Inherits="KProject.ManagerAddBills" %>
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
    </div>
    <div id ="textbox">
        <asp:DropDownList ID="ddlPayee" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="ddlCustomer" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm Selection" CausesValidation="False" />
        <br />
        <br />
        <asp:Label ID="lblMessage1" runat="server" Text="[]"></asp:Label>
        <br />
    </div>
    <asp:Panel ID="pnlAddBill" runat="server">
    <div id ="subtitle">eBill Details</div>
        <br />
    <div id ="center">
        <div id ="label2">
            <asp:Label ID="Label4" runat="server" Text="Amount"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Date Created:"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Date Due:"></asp:Label>
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
        <div id ="textbox">
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtAmount" ErrorMessage="Error: Please enter a bill amount.">*</asp:RequiredFieldValidator>
            <br />
            <asp:Calendar ID="calCreated" runat="server"></asp:Calendar>
            <asp:Calendar ID="calDue" runat="server"></asp:Calendar>
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Bill" />
            <br />
            <br />
            <asp:Label ID="lblMessage2" runat="server" Text="[]"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <br />
        </div>
        <br />
    </div>
    </asp:Panel>
</asp:Content>
