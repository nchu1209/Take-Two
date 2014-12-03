<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerTransactionDetail.aspx.vb" Inherits="KProject.CustomerTransactionDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Transaction Details<br />
        <br />
    </div>
    <div id ="center">
       <div id ="label2">
           <asp:Label ID="lblCustomerID" runat="server" Text="Description:"></asp:Label>
           <br />
           <asp:Label ID="Label1" runat="server" Text="Transaction Type:"></asp:Label>
           <br />
           <asp:Label ID="Label10" runat="server" Text="Amount:"></asp:Label>
           <br />
           <asp:Label ID="Label11" runat="server" Text="Transaction Number:"></asp:Label>
           <br />
           Date:
           <br />
           Dispute Status:
           <br />
           Employee Comments: <br />
           
        </div>
        <div id ="label3">
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
            <br />
             <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
            <br />
             <asp:Label ID="lblAmount" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblTransactionNumber" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblTransactionDate" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblDisputeStatus" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblEmployeeComments" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnReturn" runat="server" Text="Return to Transaction Search" />
            <br />
            <br />
        </div>
      </div>
    <div id="center">
        <div id="subtitle">
            <br />
            Similar Transactions</div>
        <asp:GridView ID="gvSimilar" runat="server"></asp:GridView>
    </div>
     <div id ="center">
       <div id ="subtitle">
           <br />
           Create Dispute</div>
       <div id ="footer">
           Would you like to dispute the selected transaction? 
           <asp:Button ID="btnCreateDispute" runat="server" Text="Create Dispute" CausesValidation="False" />
           <br />
           <asp:Label ID="lblAlreadySubmitted" runat="server"></asp:Label>
       </div>
       <asp:Panel ID="Panel1" runat="server">
       <div id ="label2">
           Comments:
           <br />
           Correct Transaction Amount:
           <br />
           Apply to Delete Transaction?
           <br />
           
        </div>
        <div id ="textbox">
            <asp:TextBox ID="txtDisputeComments" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ClientIDMode="Static" ControlToValidate="txtDisputeComments" ErrorMessage="ERROR: Comments required">*</asp:RequiredFieldValidator>
            <br />
             <asp:TextBox ID="txtDisputeAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ClientIDMode="Static" ControlToValidate="txtDisputeAmount" ErrorMessage="ERROR: Corrected Amount required">*</asp:RequiredFieldValidator>
            <br />
             <asp:CheckBoxList ID="cblDeleteTransaction" runat="server">
                 <asp:ListItem>Yes</asp:ListItem>
                 <asp:ListItem>No</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit Dispute" />
            <br />
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
        </asp:Panel>
      </div>
</asp:Content>
