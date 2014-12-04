<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="ManagerDisputeResolution.aspx.vb" Inherits="KProject.ManagerDisputeResolution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id ="title">
         Dispute Resolution
        <br />
        <br />
    </div>

    <div id="center">
        <asp:Label ID="lblNoDispute" runat="server"></asp:Label>
        <asp:Panel ID="Panel1" runat="server">
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
                                         <br />
                    <asp:Label ID="Label1" runat="server" Text="Adjusted Amount:" Height="26px"></asp:Label>
                                                             <br />
                    <asp:Label ID="Label2" runat="server" Text="Comment:" Height="26px"></asp:Label>

                    </div>

                <div id ="textbox">
                <asp:TextBox ID="txtDisputeNumber" runat="server" Width="168px" Height="26px" ReadOnly="True"></asp:TextBox>
                <br />
               <asp:TextBox ID="txtComments" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtActual" runat="server" ReadOnly="True" Height="26px"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtClaim" runat="server" Width="89px" Height="26px" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtAdjusted" runat="server" Width="89px" Height="26px" ReadOnly="True"></asp:TextBox>
                <br />

                <asp:TextBox ID="txtComment" runat="server" Width="89px" Height="26px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="True">
                        <asp:ListItem>Select Action</asp:ListItem>
                        <asp:ListItem>Accept</asp:ListItem>
                        <asp:ListItem>Reject</asp:ListItem>
                        <asp:ListItem>Adjust</asp:ListItem>
                    </asp:DropDownList>
                    <br />

                <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                  &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" PostBackUrl="~/ManagerResolveDisputes.aspx" Text="Back to Disputes" />
                  <br />
                 <br />

                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </div>
                  
            <br />
            <br />
            
            </div>
         
            <br />
          
            <br />
        </div>
            </asp:Panel>
</div>

</asp:Content>
