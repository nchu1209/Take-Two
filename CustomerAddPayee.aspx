<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerAddPayee.aspx.vb" Inherits="KProject.CustomerAddPayee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Add Payee
        <br />
        <br />
    </div>
    <div id ="lefthalf">
        <div id ="subtitle">Add Existing Payee</div>
        <br />
        <div id ="label">
            <asp:Label ID="Label1" runat="server" Text="Choose Payee:"></asp:Label>
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
            <asp:DropDownList ID="ddlPayee" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnAddPayee" runat="server" Text="Add Payee" CausesValidation="False" />
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
    <div id ="righthalf">
        <div id ="subtitle">Create New Payee</div>
        <br />
        <div id ="label">

            <asp:Label ID="Label2" runat="server" Text="Payee Name:"></asp:Label>

            <br />
            <asp:Label ID="Label3" runat="server" Text="Address:"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Zip Code:"></asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Phone Number:"></asp:Label>
            <br />
            <asp:Label ID="Label8" runat="server" Text="Payee Type:"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
        <div id ="textbox">

            <asp:TextBox ID="txtPayeeName" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtPayeeName" ErrorMessage="ERROR: Payee Name is required.">*</asp:RequiredFieldValidator>

            <br />
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Address is required." ControlToValidate="txtAddress">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Zip code is required." ControlToValidate="txtZip">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Phone number is required." ControlToValidate="txtPhone">*</asp:RequiredFieldValidator>
            <br />
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem>Credit Card</asp:ListItem>
                <asp:ListItem>Utilities</asp:ListItem>
                <asp:ListItem>Rent</asp:ListItem>
                <asp:ListItem>Mortgage</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnCreatePayee" runat="server" Text="Create New Payee" />
            <br />
            <asp:Button ID="btnClear" runat="server" CausesValidation="False" Text="Clear" />
            <br />
            <asp:Label ID="lblMessage2" runat="server" Text="[]"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <br />

        </div>
        <br />
    </div>
</asp:Content>
