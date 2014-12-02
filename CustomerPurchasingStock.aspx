<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPurchasingStock.aspx.vb" Inherits="KProject.CustomerPurchasingStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Purchase Stocks
    </div>

    <asp:Panel ID="pnlPurchaseStocks" runat="server">

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
    <br />

    <div id="content">


         <%-- Make sure to not have the 2 accounts the same --%><%-- because the money comes from an outside source, any amount is allowed --%>
         <asp:Panel ID="TransferPanel" runat="server" Visible =" false">
         <div id ="label">
             <asp:Label ID="Label1" runat="server" Text="Transfer Amount: "></asp:Label>
             <br />

             <asp:Label ID="Label2" runat="server" Text="Transfer From: "></asp:Label>
             <br />

             <asp:Label ID="Label7" runat="server" Text="Transfer To: "></asp:Label>
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
                 <asp:TextBox ID="txtAmountTransfer" runat="server"></asp:TextBox>
             <br />
                 <asp:DropDownList ID="ddlAccounts" runat="server">
             </asp:DropDownList>
              
                 <br />
                 <asp:TextBox ID="txtTransferDate" runat="server"></asp:TextBox>
              
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ClientIDMode="Static" ControlToValidate="txtTransferDate" ErrorMessage="ERROR: Transfer Date Required">*</asp:RequiredFieldValidator>
              
                 <br />
                 <br />
             
                 <asp:Calendar ID="TransferCalendar" runat="server"></asp:Calendar>
             
                 <br />
                 <asp:Button ID="btnTransfer" runat="server" Text="Confirm Transfer" />
                 <br />
                 <asp:Label ID="lblErrorTransfer" runat="server"></asp:Label>
                 <br />
                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" />
                 <br />
                 
             </div>
             
             

        
             

        
        </asp:Panel>
        <br />
        <br />
        <asp:Button ID="btnPurchaseStocks" runat="server" Text="Submit Purchase Order" />

        <br />

    </div>

        </asp:Panel>



    <asp:Panel ID="pnlNotApproved" runat="server">
        &nbsp;<br />
        <div id="contentBIG">
            <br />
            <br />
            <strong>Message from Longhorn Bank:</strong> You do not have approval to access purchase stocks at this time.
        </div>

    </asp:Panel>




    -	They will also see a listing of their other accounts (checking, savings, and the cash-value of their portfolio) and their current balances. Customers must select which of their other accounts the funds will come from.
    (DDL (like on customer perform transaction))<br />
    &nbsp;&nbsp;&nbsp;
    -	As with all other banking transactions, the customer must enter a date for the stock purchase.
    (again customer perform transaction page)<br />
    &nbsp;&nbsp;&nbsp;
    -	After making these selections, customers will submit their stock purchase order. 
    <br />
    o	If they do not have enough money to make the purchase, the transaction is not processed and they should see a descriptive error message.
    <br />
    o	If the transaction is successful, the money should be withdrawn from the selected account, and the transaction should be listed as Type: “Withdrawal” and Description: “Stock Purchase – Account [Stock Portfolio Account Number]”
    <br />
    o	A successful transaction should also result in a descriptive confirmation message.
    <br />
    o	Once a transaction is processed, the customer should receive a completion message and have the option to return to their portfolio detail page, where they should see all stocks in the portfolio, including the one they just purchased.  
    (redirect)  
</asp:Content>