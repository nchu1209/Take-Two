<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="EmployeeModifyPassword.aspx.vb" Inherits="KProject.EmployeeModifyPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="employeeModifyPassword" class="text-center"> 
    <div id ="label2">
         <asp:Label ID="Label3" runat="server" Text="Old Password:"></asp:Label>
          <br />
          <asp:Label ID="Label4" runat="server" Text="New Password:"></asp:Label>
           <br />
    </div>


    <div id ="textbox" >
        <asp:TextBox ID="txtOld" runat="server" Width="135px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOld" ErrorMessage="You must enter your old password to update.">*</asp:RequiredFieldValidator>
        <br />
               
      <asp:TextBox ID="txtNew" runat="server" Width="135px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNew" ErrorMessage="To update, you must enter a new password.">*</asp:RequiredFieldValidator>
        
        <br />
        <br />
        <asp:Button ID="btnConfirmPassword" runat="server" Text="Update Password" />
         </div>

         <br />

    <br />
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
         <div class="text-center">
         <br />
          <asp:Label ID="lblErrorPassword" runat="server"></asp:Label>

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

         </div>
</asp:Content>
