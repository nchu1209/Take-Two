<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPaidBills.aspx.vb" Inherits="KProject.CustomerPaidBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id ="title">
        Bills You Have Paid
         <br />
         <br />
    </div>
    <div id="gridviewright">

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

    </div>
    <br />

    <div id ="gridviewleft">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="BillID" DataSourceID="SqlDataSource1">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="BillID" HeaderText="BillID" ReadOnly="True" SortExpression="BillID" />
                <asp:BoundField DataField="BillAmount" HeaderText="BillAmount" SortExpression="BillAmount" />
                <asp:BoundField DataField="BillDate" HeaderText="BillDate" SortExpression="BillDate" />
                <asp:BoundField DataField="DueDate" HeaderText="DueDate" SortExpression="DueDate" />
                <asp:BoundField DataField="PayeeID" HeaderText="PayeeID" SortExpression="PayeeID" />
                <asp:BoundField DataField="AmountPaid" HeaderText="AmountPaid" SortExpression="AmountPaid" />
                <asp:BoundField DataField="AmountRemaining" HeaderText="AmountRemaining" SortExpression="AmountRemaining" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="usp_bill_get_paid" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="customerNumber" SessionField="CustomerNumber" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
