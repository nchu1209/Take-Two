<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerManageEmployee.aspx.vb" Inherits="KProject.ManagerManageEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/favicon-4.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="http://localhost:51539/ManagerManageEmployee.aspx" rel="shortcut icon" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id="title">
        Manage Employee&#39;s Account
        <br />
        <br />
    </div>
    <asp:Panel ID="SearchForCustomers" runat="server" Visible="true">
        <div id="SearchForCustomer">
            Search for Employee(Employee ID):<asp:TextBox ID="txtEmployeeID" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnFind" runat="server" Text="Find Customer Information" />
            <br />
            <asp:Label ID="lblEmpIDError" runat="server"></asp:Label>
            <br />
        </div>
    </asp:Panel>
    <asp:Panel ID="ModifyProfile" runat="server" Visible="true">
        <div id="lefthalf">
            <div id="subtitle">
                Modify Profile
                <br />
                <br />
            </div>
            <div id="label2">
                <asp:Label ID="Label3" runat="server" Text="Last Name:"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Text="First Name:"></asp:Label>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Middle Initial:"></asp:Label>
                <br />
                <asp:Label ID="Label7" runat="server" Text="Password:"></asp:Label>
                <br />
                <asp:Label ID="Label16" runat="server" Text="Employee Type:"></asp:Label>
                <br />
                <asp:Label ID="Label8" runat="server" Text="Address:"></asp:Label>
                <br />
                <asp:Label ID="Label9" runat="server" Text="City:"></asp:Label>
                <br />
                <asp:Label ID="Label10" runat="server" Text="State:"></asp:Label>
                <br />
                <asp:Label ID="Label11" runat="server" Text="Zip Code:"></asp:Label>
                <br />
                <asp:Label ID="Label13" runat="server" Text="Phone Number:"></asp:Label>
                <br />
            </div>
            <div id="textbox">
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtLastName" ErrorMessage="ERROR: Last Name Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtFirstName" ErrorMessage="ERROR: First Name Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtInitial" runat="server" MaxLength="1"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ControlToValidate="txtPassword" ErrorMessage="ERROR: Password Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtEmpType" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmpType" ErrorMessage="ERROR: An employee must have an Employee Type.">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ClientIDMode="Static" ControlToValidate="txtAddress" ErrorMessage="ERROR: Address Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtCity" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtState" runat="server" MaxLength="2" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtZip" runat="server" MaxLength="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="ERROR: Incorrect Zip Code Format" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ClientIDMode="Static" ControlToValidate="txtPhone" ErrorMessage="ERROR: Phone Number">*</asp:RequiredFieldValidator>
                <br />
                <br />
            </div>
            <div id="content">
                <br />
                <asp:Button ID="btnSaveProfile" runat="server" Text="Save" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelProfile" runat="server" CausesValidation="False" Text="Cancel" />
                <br />
                <br />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:Label ID="lblError" runat="server"></asp:Label>
                <br />
            </div>
            <br/>
            <%--<div id="content3">
                <div id="subtitle0">
                    Customer Status
                </div>
                <div id="content4">
                    <asp:Label ID="lblAccountStatus" runat="server"></asp:Label>
                    <br />
                </div>--%>
                <%-- <div id="content">
                <div id="textbox">
                    <br />
                    <br />
                    <br />
                    <br/>
                    </div>
                <div id="label2">
            <asp:RadioButtonList ID="radEnableDisable" runat="server" AutoPostBack="True" Width="67px">
                <asp:ListItem>Enable</asp:ListItem>
                <asp:ListItem>Disable</asp:ListItem>
            </asp:RadioButtonList>
                    </div>
        
                </div>--%>
                <%--<div id="content5">
                    <asp:Button ID="btnChangeStatus" runat="server" Text="Change Status" />
                </div>--%>
         </div>
    </asp:Panel>
    <asp:Panel ID="ChangeStatus" runat="server" Visible="true">
        <div id="righthalf">
           <div id="content3">
                <div id="subtitle">
                    Customer Status
                    <br />
                </div>
                <div id="content">
                    <asp:Label ID="lblAccountStatus" runat="server" style="font-size: medium; font-family: Arial"></asp:Label>
                    <br />
                    <br />
                </div>

                </div>
            <div id="content">
                    <asp:Button ID="btnChangeStatus" runat="server" Text="Confirm" />
                </div>
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
                <br />
                <br />
                <br />
            </div>
            <br />
            <br />
            <br />
            <br />
        </div>
    </asp:Panel>

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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
