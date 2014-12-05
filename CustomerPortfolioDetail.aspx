<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPortfolioDetail.aspx.vb" Inherits="KProject.CustomerPortfolioDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/EmployeeManageCustomers.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    
       

    
    <div id ="title">
        Portfolio Detail
        <br />
        <div id="content">
            <asp:Label ID="lblPortfolioStatus" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblBalanced" runat="server" Text=""></asp:Label>
        </div>
        <span style="font-weight: normal; font-family: Arial; font-size: medium">Total Value of Your Portfolio=
        <asp:Label ID="lblTotalValue" runat="server"></asp:Label>
        <br />
        <br />
        sum of these two portions constitutes the total value of a portfolio.</span><br />
              <span style="font-weight: normal; font-family: Arial; font-size: medium">there rshould be a select or ddl on top for the custoemrs to select which stock portfolio they want to look at</span><br />
         <span style="font-family: Arial; font-size: medium; font-weight: normal">Customers can create a new stock portfolio through an option on their home page (after they login).
        <br />
    </span>

        
    </div>


    <div id="lefthalf">
        <div id ="subtitle">
            Cash-Value Portion
            <br />
            <div id="content">

                FIND ALEXS POST ON FB
                <br />
                <asp:Label ID="lblCashValue" runat="server" Text=""></asp:Label>
                <br />

                <div id ="gridviewright">


                    <br />
                    <br />
                    <br />

                </div>
                <br />
                <div id="content">

                    <asp:Panel ID="PanelCash" runat="server">
              <%--  <asp:GridView ID="gvCash" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Balance" HeaderText="Balance" SortExpression="Balance" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString2 %>" SelectCommand="usp_accounts_get_stock_balance" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="customerNumber" SessionField="CustomerNumber" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                        </asp:Panel>
                   
            </div>
                 <asp:Panel ID="PanelNoCash" runat="server" visible ="false">
                        You do not have any cash in your portfolio.
                    </asp:Panel>
        </div>
         </div>
    </div>


    <div id="righthalf">
        <div id ="subtitle">
            Stock Portion
            <br />
            <div id="content">

                <br />
                <br />
                <asp:Panel ID="PanelStockPortion" runat="server">
                <asp:GridView ID="gvStockPortion" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Ticker" HeaderText="Ticker" SortExpression="Ticker" />
                        <asp:BoundField DataField="TotalSharesHeld" HeaderText="TotalSharesHeld" ReadOnly="True" SortExpression="TotalSharesHeld" />
                        <asp:BoundField DataField="PurchasePrice" HeaderText="PurchasePrice" SortExpression="PurchasePrice" />
                        <asp:BoundField DataField="StockType" HeaderText="StockType" SortExpression="StockType" />
                        <asp:BoundField DataField="StockValue" HeaderText="StockValue" ReadOnly="True" SortExpression="StockValue" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="usp_stocks_get_balanced_portfolio" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="CustomerNumber" SessionField="CustomerNumber" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </asp:Panel>

                <asp:Panel ID="PanelNoStocks" runat="server" visible="false">
                    You do not own any stocks at this time
                </asp:Panel>
                <br />

            </div>
             </div>
        </div>




</asp:Content>
