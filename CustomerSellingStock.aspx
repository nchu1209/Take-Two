<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerSellingStock.aspx.vb" Inherits="KProject.CustomerSellingStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Sell Stocks
    </div>

    <div id="content">

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Ticker" HeaderText="Ticker" SortExpression="Ticker" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="PurchasePrice" SortExpression="PurchasePrice" />
                <asp:BoundField DataField="NumberOfSharesHeld" HeaderText="NumberOfSharesHeld" SortExpression="NumberOfSharesHeld" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="SELECT [Ticker], [PurchasePrice], [NumberOfSharesHeld] FROM [tblStockPortfolio]"></asp:SqlDataSource>
        Need to join the table<br/>
        <br />


    </div>



</asp:Content>
