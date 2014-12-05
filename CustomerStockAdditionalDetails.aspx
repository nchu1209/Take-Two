<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerStockAdditionalDetails.aspx.vb" Inherits="KProject.CustomerStockAdditionalDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Additional Details
    </div>

    <div id="content">
        <asp:Button ID="btnReturn" runat="server" Text="Return to Sell Stock Page" />
        <br />
        <asp:GridView ID="gvAdditionalDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Ticker" HeaderText="Ticker" SortExpression="Ticker" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:TemplateField HeaderText="Gains/Loss">
                    <EditItemTemplate>
                        <asp:Label ID="lblChange" runat ="server"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblChange" runat ="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="usp_stocks_get_times_and_prices_by_ticker" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="ticker" SessionField="Ticker" Type="String" />
                <asp:SessionParameter DbType="Date" Name="originalDate" SessionField="PurchaseDate" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
    </div>



</asp:Content>
