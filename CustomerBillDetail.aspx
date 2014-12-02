<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerBillDetail.aspx.vb" Inherits="KProject.CustomerBillDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Details for eBill ID #<asp:Label ID="lblBillID" runat="server" Text="[]"></asp:Label>
        <br />
        <br />
    </div>
    <div id ="subtitle">
        <asp:LinkButton ID="lnkBack" runat="server" PostBackUrl="~/CustomerPayBills.aspx">Click to Return to Pay Bills</asp:LinkButton>
        <br />
    </div>
    <div id ="lefthalf">
       <div id ="subtitle">eBill Details</div>
       <div id ="label2">
           <asp:Label ID="lblCustomerID" runat="server" Text="Customer Name:"></asp:Label>
           <br />
           <asp:Label ID="Label1" runat="server" Text="Bill Amount:"></asp:Label>
           <br />
           <asp:Label ID="Label10" runat="server" Text="Bill Amount Paid to Date:"></asp:Label>
           <br />
           <asp:Label ID="Label11" runat="server" Text="Bill Outstanding Balance:"></asp:Label>
           <br />
           <asp:Label ID="Label3" runat="server" Text="Bill Date:"></asp:Label>
           <br />
           <asp:Label ID="Label4" runat="server" Text="Due Date:"></asp:Label>
           <br />
           <asp:Label ID="Label15" runat="server" Text="Payment Status:"></asp:Label>
           <br />
        </div>
        <div id ="textbox">
            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtBillAmount" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtAmountPaid" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtAmountRemaining" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtBillDate" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <br />
            <br />
        </div>
        <div id ="subtitle">Payee Details</div>
        <div id ="label2">
           <asp:Label ID="Label5" runat="server" Text="Payee Name:"></asp:Label>
           <br />
           <asp:Label ID="Label6" runat="server" Text="Payee Type:"></asp:Label>
           <br />
           <asp:Label ID="Label7" runat="server" Text="Payee Address:"></asp:Label>
           <br />
           <asp:Label ID="Label8" runat="server" Text="Payee Zip Code:"></asp:Label>
           <br />
           <asp:Label ID="Label9" runat="server" Text="Payee Phone Number:"></asp:Label>
           <br />
       </div>
       <div id ="textbox">
           <asp:TextBox ID="txtPayeeName" runat="server" ReadOnly="true"></asp:TextBox>
       <br />
           <asp:TextBox ID="txtType" runat="server" ReadOnly="true"></asp:TextBox>
       <br />
           <asp:TextBox ID="txtPayeeAddress" runat="server" ReadOnly="true"></asp:TextBox>
       <br />
           <asp:TextBox ID="txtZip" runat="server" ReadOnly="true"></asp:TextBox>
       <br />
           <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true"></asp:TextBox>
           <br />
       <br />
       <br />
       </div>
    </div>
    <div id ="righthalf">
        <div id ="subtitle">Make a Payment</div>
        <div id ="label2">
            <asp:Label ID="Label12" runat="server" Text="Select Account:"></asp:Label>
            <br />
            <asp:Label ID="Label13" runat="server" Text="Pay Amount:"></asp:Label>
            <br />
            <asp:Label ID="Label14" runat="server" Text="Date:"></asp:Label>
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
        <div id ="textbox">
            <asp:DropDownList ID="ddlAccount" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtAmount" ErrorMessage="Please enter a payment amount.">*</asp:RequiredFieldValidator>
            <br />
            <asp:Calendar ID="calDate" runat="server"></asp:Calendar>
            <br />
            <asp:Button ID="btnPay" runat="server" Text="Pay Bill" />
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
            <br />
            <asp:Label ID="lblMessageFee" runat="server" Text="[]"></asp:Label>
            <br />
            <asp:Label ID="lblMessageFee2" runat="server" Text="[]"></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
&nbsp;&nbsp;
            <asp:Button ID="btnAbort" runat="server" Text="Abort" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
