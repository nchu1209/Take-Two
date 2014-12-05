<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerApproveStockAccounts.aspx.vb" Inherits="KProject.ManageApproveStockAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="ContentStyle.css" rel="stylesheet" type="text/css" />
     <div id ="title">
        Approve Stock Accounts
         <br />
         <br />
    </div>
    <div id="center">

        <asp:Button ID="btnApprove" runat="server" Text="Approve Selected Accounts" />

        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>

        &nbsp;

        <br />
        <br />
        <asp:GridView ID="gvStockAccounts" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" ReadOnly="True" SortExpression="Customer Name" />
                <asp:BoundField DataField="AccountNumber" HeaderText="AccountNumber" SortExpression="AccountNumber" />
                <asp:BoundField DataField="AccountName" HeaderText="AccountName" SortExpression="AccountName" />
                <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" />
                <asp:TemplateField HeaderText="Approved" SortExpression="Approved">
                    <EditItemTemplate>
                        <asp:dropdownlist ID="ddlApproved" runat="server" Text='<%# Bind("Approved") %>' AutoPostBack="true">
                            <asp:ListItem>True</asp:ListItem>
                            <asp:ListItem>False</asp:ListItem>
                        </asp:dropdownlist>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:dropdownlist ID="ddlApproved" runat="server" Text='<%# Bind("Approved") %>' AutoPostBack="true">
                            <asp:ListItem>True</asp:ListItem>
                            <asp:ListItem>False</asp:ListItem>
                        </asp:dropdownlist>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="usp_accounts_stock_account_approval" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <br />

    </div>

</asp:Content>
