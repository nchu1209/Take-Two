<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="ManagerDisputeResolution.aspx.vb" Inherits="KProject.DisputeResolution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id ="title">
         Dispute Resolution
        <br />
        <br />
    </div>

    <div id="center">

      <div id="employeeModifyAddress" class="text-justify">

            <div class="text-left">

            <br />
            <br />
                <div id ="label">
                     <asp:Label ID="Label3" runat="server" Text="Dispute Number:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label4" runat="server" Text="Customer's Comments:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label5" runat="server" Text="Actual Amount:" Height="26px"></asp:Label>
                     <br />
                    <asp:Label ID="Label7" runat="server" Text="Customer Claim Amount:" Height="26px"></asp:Label>
                    </div>

                <div id ="textbox">
                <asp:TextBox ID="txtDisputeNumber" runat="server" Width="168px" Height="26px" ReadOnly="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="You must enter an address.">*</asp:RequiredFieldValidator>
                <br />
               <asp:TextBox ID="txtComments" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtActual" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtClaim" runat="server" Width="89px" Height="26px" ReadOnly="True"></asp:TextBox>
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
