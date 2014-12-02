<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerEditStocks.aspx.vb" Inherits="KProject.ManagerEditStocks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="title">
        Edit Stocks
    </div>
    <br/>

<asp:Panel ID="pnlEdit" runat="server">
    <div id="content">



        <asp:GridView ID="gvStocks" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="TickerSymbol" HeaderText="TickerSymbol" SortExpression="TickerSymbol" />
                <asp:BoundField DataField="StockType" HeaderText="StockType" SortExpression="StockType" >
                <HeaderStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
                <HeaderStyle Width="300px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="SalesPrice" SortExpression="SalesPrice">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SalesPrice") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("SalesPrice") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="Fee" HeaderText="Fee" SortExpression="Fee" DataFormatString="{0:$###,###.00}" >
                <HeaderStyle Width="100px" />
                </asp:BoundField>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="SELECT * FROM [tblStocks]" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

        
        <br />
        <asp:Button ID="btnChangePrice" runat="server" Text="Submit Change" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <br />



        </div>

    </asp:Panel>
        <asp:Panel ID="pnlDescriptiveMessage" runat="server">

            <br/>
            <br/>
            <div id="content">
    <asp:Label ID="lblDescriptiveMessage" runat="server" Text=""></asp:Label>
                </div>
        </asp:Panel>

</asp:Content>
