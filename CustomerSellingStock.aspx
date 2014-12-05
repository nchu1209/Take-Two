<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerSellingStock.aspx.vb" Inherits="KProject.CustomerSellingStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Sell Stocks
    </div>

    <div id="center">


        <asp:Button ID="btnSell" runat="server" Text="Sell Shares" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="[]"></asp:Label>
        <br />
        <br />
        
        
        <asp:GridView ID="gvStocks" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="SetNumber" HeaderText="SetNumber" SortExpression="SetNumber" />
                <asp:BoundField DataField="CustomerNumber" HeaderText="CustomerNumber" SortExpression="CustomerNumber" />
                <asp:BoundField DataField="Ticker" HeaderText="Ticker" SortExpression="Ticker" />
                <asp:BoundField DataField="PurchaseDate" HeaderText="PurchaseDate" SortExpression="PurchaseDate" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="PurchasePrice" SortExpression="PurchasePrice" />
                <asp:BoundField DataField="SharesHeld" HeaderText="SharesHeld" SortExpression="SharesHeld" />
                <asp:BoundField DataField="CurrentPrice" HeaderText="CurrentPrice" SortExpression="CurrentPrice" />
                <asp:BoundField DataField="Fee" HeaderText="Fee" SortExpression="Fee" />
                <asp:TemplateField HeaderText="Sell Shares">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sale Date">
                    <EditItemTemplate>
                        <asp:Calendar ID="calSaleDate" runat="server"></asp:Calendar>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Calendar ID="calSaleDate" runat="server"></asp:Calendar>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="Show More Details" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="usp_innerjoin_stock_stocktransactions" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="customernumber" SessionField="CustomerNumber" Type="Double" />
            </SelectParameters>
        </asp:SqlDataSource>

        
    </div>





</asp:Content>
