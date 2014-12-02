<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerUpdatePayee.aspx.vb" Inherits="KProject.CustomerUpdatePayee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        View/Update Payee Information<br />
        <br />
    </div>
    <div id ="center">
    <div id ="label2">
        
        <asp:Label ID="Label1" runat="server" Text="Payee Name:"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Address:"></asp:Label>
            <br />
         <asp:Label ID="Label3" runat="server" Text="City:"></asp:Label>
            <br />    
         <asp:Label ID="Label6" runat="server" Text="State:"></asp:Label>
            <br />
        <asp:Label ID="Label5" runat="server" Text="Zip Code:"></asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Phone Number:"></asp:Label>
            <br />
            <asp:Label ID="Label8" runat="server" Text="Payee Type:"></asp:Label>
        <br />
    </div>
    <div id ="textbox">
        <asp:TextBox ID="txtPayeeName" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtPayeeName" ErrorMessage="ERROR: Payee Name is required.">*</asp:RequiredFieldValidator>

            <br />
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Address is required." ControlToValidate="txtAddress">*</asp:RequiredFieldValidator>
            <br />
        <asp:TextBox ID="txtCity" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtState" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
            <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Zip code is required." ControlToValidate="txtZip">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Phone number is required." ControlToValidate="txtPhone">*</asp:RequiredFieldValidator>
            <br />
        <asp:TextBox ID="txtType" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save Changes" />
        <br />
        <asp:Button ID="btnAbort" runat="server" Text="Abort Changes" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <br />
    </div>
    </div>
</asp:Content>
