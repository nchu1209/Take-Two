<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerTransactionDetail.aspx.vb" Inherits="KProject.ManagerTransactionDetail" %>
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
           Employee Comments:<br />
           Employee ID: <br />
           
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
            <asp:Label ID="lblEmpID" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnReturn" runat="server" Text="Return to Transaction Search" />
            <br />
            <br />
        </div>
      </div>
    <div id="Div1">
        <div id="subtitle">
            <br />
            Similar Transactions</div>
        <asp:GridView ID="gvSimilar" runat="server"></asp:GridView>
    </div>
</asp:Content>