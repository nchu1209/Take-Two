<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerSellingStock.aspx.vb" Inherits="KProject.CustomerSellingStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

-	Customers can sell all or part of a certain set of shares. 
    <br />
    -	Shares are sold at the current market price
    <br />
    -	Customers choose which of their stocks to sell from a gridview, which displays the name of the stock, purchase price, current price, transaction fee, and number of shares held.
    <br />
    -	Each of the customers stocks will have an option to show more details. This link can either take them to a new page or display the information on the same page<br />
&nbsp;o	Show More Details will show a gridview with ever date that the prices over the period that the customer has held the stock, the price of the stock on that date, and the amount the stock value increased or decreased (include the + or – symbols)<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
o	You are encouraged to include other graphical enhancements such as color coding the increased/decreased amounts or including a graphical representation of the stock’s value over time
    <br />
    -	Customers cannot enter a sale date that is prior to the date of purchase.
<br />
    -	A customer must select which stock and the number of shares to sell, and then they should be presented with a summary screen that allows them to confirm or cancel the transaction. 
    <br />
    o	This page should show the name of the stock being sold, the number of shares being sold, the number of shares remaining after the sale, any fees relating to the sale, and the net profit that will be gained from the sale.<br />
&nbsp;o	Confirming the transaction will update the portfolio with the new stock amounts, and the customer should be redirected to the portfolio detail page, which should reflect the new portfolio price. A confirmation message should be displayed as well. 

</asp:Content>
