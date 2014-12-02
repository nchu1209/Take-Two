<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="ManagerResolveDisputes.aspx.vb" Inherits="KProject.ManagerResolveDisputes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id ="title">
        Resolve Disputes<br />
        <br />
    </div>

    <div id="content">
      
        <asp:Button ID="btnViewAll" runat="server" Text="View All Disputes" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFilter" runat="server" Text="View Unresolved Disputes" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnActive" runat="server" Text="View Resolved Disputes" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="DisputeID" HeaderText="DisputeID" SortExpression="DisputeID" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" >
                <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" >
                <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CustomerNumber" HeaderText="Customer Number" SortExpression="CustomerNumber" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="TransactionAmount" HeaderText="Transaction Amount" SortExpression="TransactionAmount" DataFormatString="{0:$###,###.00}" />
                <asp:BoundField DataField="CorrectAmount" HeaderText="Correct Amount" SortExpression="CorrectAmount" DataFormatString="{0:$###,###.00}" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString2 %>" SelectCommand="usp_dispute_get_all_plus_customer_info" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

    </div>
</asp:Content>
