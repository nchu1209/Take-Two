<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPurchasingStock.aspx.vb" Inherits="KProject.CustomerPurchasingStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Purchase Stocks
    </div>






    <asp:Panel ID="pnlPurchaseStocks" runat="server">

        <div id="content">


         <div id="accountsubtitleleft">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

    </div>

    <br />
    <asp:GridView ID="gvPurchaseStocks" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="TickerSymbol" HeaderText="Ticker Symbol" SortExpression="TickerSymbol" />
            <asp:BoundField DataField="StockType" HeaderText="Stock Type" SortExpression="StockType" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="SalesPrice" HeaderText="Sales Price" SortExpression="SalesPrice" DataFormatString="{0:$##,###.00}" >
            <HeaderStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="Fee" HeaderText="Fee" SortExpression="Fee" DataFormatString="{0:$##,###.00}" >
            <HeaderStyle Width="90px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString %>" SelectCommand="SELECT * FROM [tblStocks]"></asp:SqlDataSource>
    <br />

    <div id="content">


         <%-- Make sure to not have the 2 accounts the same --%><%-- because the money comes from an outside source, any amount is allowed --%>
        
             <div id="content">
             
              <div id ="label">
             <br />

             <asp:Label ID="Label2" runat="server" Text="Transfer From: "></asp:Label>
             <br />

             <%-- Default to Current Date --%>
             <asp:Label ID="Label4" runat="server" Text="Date:"></asp:Label>
             <br />
             <br />
             
             <br />
             <br />
             <br />
          </div>
        
             <div id="textbox">
             <br />
                 <asp:DropDownList ID="ddlAccounts" runat="server">
             </asp:DropDownList>
              
                 <br />
                 <br />
             
                 <asp:Calendar ID="TransferCalendar" runat="server"></asp:Calendar>
             
                 <br />
                 <asp:Button ID="btnPurchaseStocks" runat="server" Text="Submit Purchase Order" />
                 <br />
                 <br />
                 <asp:Label ID="lblErrorTransfer" runat="server"></asp:Label>
                 <br />
                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" />
                 <br />
                 
             </div>
             
             

            </div>
             
             </div>
        

    </asp:Panel>

    <asp:Panel ID="pnlNotApproved" runat="server">
                <br />

        <div id="contentBIG">
            <br />
            <br />
            <strong>Message from Longhorn Bank:</strong> You do not have approval to access purchase stocks at this time.
            <br />
            <br />
        </div>

    </asp:Panel>




    </asp:Content>