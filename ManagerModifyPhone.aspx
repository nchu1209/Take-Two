<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerModifyPhone.aspx.vb" Inherits="KProject.ManagerModifyPhone" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Modify Manager Phone
        <br />
        <br />
    </div>



      <div id="employeeModify" style="text-align: center">

            <br />Phone Number:<asp:TextBox ID="txtPhone" runat="server" Width="122px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Need New Phone Number">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="btnModifyPhone" runat="server" Text="Modify Phone Number" />
            <br />
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>

            <br />
            <br />
            <br />
            <br />
            <br />

        </div>


</asp:Content>

