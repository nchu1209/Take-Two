<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="EmployeeModifyAddress.aspx.vb" Inherits="KProject.EmployeeModifyAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Modify Employee Address
        <br />
        <br />
    </div>

    <div id="center">

      <div id="employeeModifyAddress" class="text-justify">

            <div class="text-left">

            <br />
            <br />
                <div id ="label">
                     <asp:Label ID="Label3" runat="server" Text="Address:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label4" runat="server" Text="City:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label5" runat="server" Text="State:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label7" runat="server" Text="Zip Code:" Height="26px"></asp:Label>
                    </div>

                <div id ="textbox">
                <asp:TextBox ID="txtAddress" runat="server" Width="168px" Height="26px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="You must enter an address.">*</asp:RequiredFieldValidator>
                <br />
               <asp:TextBox ID="txtCity" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtState" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtZipcode" runat="server" Width="89px" Height="26px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtZipcode" Enabled="False" ErrorMessage="You must have a zip code.">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="btnModifyAddress" runat="server" Text="Modify Address" />
                  <br />
                 <br />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </div>
                  
            <br />
            <br />
            
            </div>
         
            <br />
          
            <br />
        </div>

</div>
</asp:Content>
