<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerAddStocks.aspx.vb" Inherits="KProject.ManagerAddStocks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title">
        Add Stocks
    </div>

    <br/>

    <asp:Panel ID="AddStockMaterial" runat="server">
    <div id="label2">

        <asp:Label ID="Label3" runat="server" Text="Stock Type:"></asp:Label>
        <br/>
        <asp:Label ID="Label1" runat="server" Text="Ticker Symbol:"></asp:Label>
        <br/>
        <asp:Label ID="Label4" runat="server" Text="Name:"></asp:Label>
        <br/>
        <asp:Label ID="Label5" runat="server" Text="Share Price:"></asp:Label>
        <br/>
        <asp:Label ID="Label6" runat="server" Text="Transaction Fee:"></asp:Label>
    </div>


    <div id="textbox">

        <asp:DropDownList ID="ddlStockType" runat="server" AutoPostBack="True" Height="25px">
            <asp:ListItem>Ordinary Stocks</asp:ListItem>
            <asp:ListItem>Index Funds</asp:ListItem>
            <asp:ListItem>Exchange-Traded Funds</asp:ListItem>
            <asp:ListItem>Mutual Funds</asp:ListItem>
            <asp:ListItem>Future Shares</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Stock Type Required." ControlToValidate="ddlStockType">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtTicker" runat="server" MaxLength="5"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Ticker Symbol Required." ControlToValidate="txtTicker">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtStockName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Name of Stock Required." ControlToValidate="txtStockName">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtSharePrice" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ErrorMessage="ERROR: Share Price Required." ControlToValidate="txtSharePrice">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtTransactionFee" runat="server"></asp:TextBox>

        <br />

        <br />

     </div>

        <div id="content">
    <div id="content">
        <asp:Button ID="btnAddStock" runat="server" Text="Add Stock" />
        <br />
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </div>
            </div>
</asp:Panel>



    <br/>
    <asp:Panel ID="StockSuccessfullyAdded" runat="server">

        <div id="subtitle">
            Stock successfully added.
        </div>

    </asp:Panel>
</asp:Content>
