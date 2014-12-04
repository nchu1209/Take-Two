<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="ManagerResolveDisputes.aspx.vb" Inherits="KProject.ManagerResolveDisputes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id ="title">
        Customer Disputes<br />
        <br />
    </div>

    <div id="content">
      
        <asp:Button ID="btnViewAll" runat="server" Text="View All Disputes" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUnresolved" runat="server" Text="View Unresolved Disputes" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnResolved" runat="server" Text="View Resolved Disputes" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:CommandField HeaderText="Resolve Dispute" ShowSelectButton="True" />
                <asp:BoundField DataField="DisputeID" HeaderText="DisputeID" SortExpression="DisputeID" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" >
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" >
                </asp:BoundField>
                <asp:BoundField DataField="CustomerNumber" HeaderText="CustomerNumber" SortExpression="CustomerNumber" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="TransactionAmount" HeaderText="TransactionAmount" SortExpression="TransactionAmount" />
                <asp:BoundField DataField="CorrectAmount" HeaderText="CorrectAmount" SortExpression="CorrectAmount" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString2 %>" SelectCommand="usp_dispute_get_all" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="Submitted" Name="filter" SessionField="Status" Type="String" />
                <asp:SessionParameter DefaultValue="Accepted" Name="filter2" SessionField="Status2" Type="String" />
                <asp:SessionParameter DefaultValue="Adjusted" Name="filter3" SessionField="Status3" Type="String" />

            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

    </div>
</asp:Content>
