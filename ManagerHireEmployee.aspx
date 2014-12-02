<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerHireEmployee.aspx.vb" Inherits="KProject.ManagerHireEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="/favicon-4.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="http://localhost:51539/ManagerHireEmployee.aspx" rel="shortcut icon" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id="title">
        <br />
        Hire an Employee
        <br />
    </div>

    <asp:Panel ID="ModifyProfile" runat="server" Visible="true">
        <div id="content">
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
                <asp:Label ID="Label17" runat="server" Text="Social Security Number:"></asp:Label>
                <br />
                <asp:Label ID="Label8" runat="server" Text="Address:"></asp:Label>
                <br />
                <asp:Label ID="Label11" runat="server" Text="Zip Code:"></asp:Label>
                <br />
                <asp:Label ID="Label13" runat="server" Text="Phone Number:"></asp:Label>
                <br />
            </div>
            <div id="textbox">
                <asp:TextBox ID="txtLastName" runat="server" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtLastName" ErrorMessage="ERROR: Last Name Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtFirstName" runat="server" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtFirstName" ErrorMessage="ERROR: First Name Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtInitial" runat="server" MaxLength="1" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="10" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ControlToValidate="txtPassword" ErrorMessage="ERROR: Password Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtEmpType" runat="server" Height="26px" MaxLength="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmpType" ErrorMessage="ERROR: An employee must have an Employee Type.">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtSSN" runat="server" Height="26px" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSSN" ErrorMessage="ERROR: You must enter a SSN to hire this person.">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtAddress" runat="server" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ClientIDMode="Static" ControlToValidate="txtAddress" ErrorMessage="ERROR: Address Required">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtZip" runat="server" MaxLength="5" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="ERROR: Incorrect Zip Code Format" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ClientIDMode="Static" ControlToValidate="txtPhone" ErrorMessage="ERROR: Phone Number">*</asp:RequiredFieldValidator>
                <br />
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
              </div>
    </asp:Panel>
    <asp:Panel ID="pnlHired" runat="server">
                <div id="content2">
                    <br />
                    <br />
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
            </asp:Panel>
</asp:Content>
