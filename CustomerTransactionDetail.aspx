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
           Employee Comments<br />
           
        </div>
        <div id ="textbox">
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
            <br />
             <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
            <br />
             <asp:Label ID="lblAmount" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblTransactionNumber" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblEmployeeComments" runat="server"></asp:Label>
            <br />
        </div>
      </div>
    <div id="center">
        <div id="subtitle">Similar Transactions</div>
        <asp:GridView ID="gvSimilar" runat="server"></asp:GridView>
    </div>
     <div id ="center">
       <div id ="subtitle">Create Dispute</div>
       <div id ="label2">
           <asp:Label ID="Label3" runat="server" Text="Description:"></asp:Label>
           <br />
           <asp:Label ID="Label4" runat="server" Text="Transaction Type:"></asp:Label>
           <br />
           <asp:Label ID="Label5" runat="server" Text="Amount:"></asp:Label>
           <br />
           <asp:Label ID="Label6" runat="server" Text="Transaction Number:"></asp:Label>
           <br />
           
        </div>
        <div id ="textbox">
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox4" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
      </div>
</asp:Content>
