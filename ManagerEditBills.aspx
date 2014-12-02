<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerEditBills.aspx.vb" Inherits="KProject.ManagerEditBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Edit eBill Details<br />
        <br />
    </div>
    <div id ="subtitle">
        <asp:Label ID="lblNotification" runat="server" Text="[]" ForeColor="#CC0000"></asp:Label>
        <br />
    </div>
    <div id ="center">
        <div id ="label2">
           <asp:Label ID="lblCustomerID" runat="server" Text="Customer Name:"></asp:Label>
           <br />
            <asp:Label ID="Label6" runat="server" Text="Payee Name:"></asp:Label>
           <br />
           <asp:Label ID="Label1" runat="server" Text="Bill Amount:"></asp:Label>
           <br />
           <asp:Label ID="Label3" runat="server" Text="Bill Date:"></asp:Label>
           <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
           <asp:Label ID="Label4" runat="server" Text="Due Date:"></asp:Label>
           <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
           <asp:Label ID="Label15" runat="server" Text="Payment Status:"></asp:Label>
           <br />
            <asp:Label ID="Label5" runat="server" Text="Active:"></asp:Label>
           <br />
        </div>
        <div id ="textbox">
            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPayee" runat="server"></asp:TextBox>
            <br />
             <asp:TextBox ID="txtBillAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtBillAmount" ErrorMessage="Please enter a bill amount.">*</asp:RequiredFieldValidator>
            <br />
             <asp:TextBox ID="txtBillDate" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Calendar ID="calBillDate" runat="server"></asp:Calendar>
            <br />
             <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Calendar ID="calDueDate" runat="server"></asp:Calendar>
            <br />
            <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:DropDownList ID="ddlActive" runat="server">
                <asp:ListItem>TRUE</asp:ListItem>
                <asp:ListItem>FALSE</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" />
&nbsp;&nbsp;
            <asp:Button ID="btnAbort" runat="server" CausesValidation="False" Text="Abort" />
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
