<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerUserManual.aspx.vb" Inherits="KProject.CustomerUserManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="title100">
        <br />
        View questions about: <asp:DropDownList ID="ddlHelp" runat="server">
            <asp:ListItem>Creating Bank Accounts</asp:ListItem>
            <asp:ListItem>Modifying Your Account</asp:ListItem>
            <asp:ListItem>IRAs</asp:ListItem>
            <asp:ListItem>Stocks</asp:ListItem>
        </asp:DropDownList>
        <br />
    </div>
     <br />
     <br />
     <br />
    
    <asp:Panel ID="CreatingAccount" runat="server">
        <div id="content">
            In order to create bank accounts click on 'My Accounts' and choose the '
        </div>

    </asp:Panel>

     <asp:Panel ID="ModifyingYourAccount" runat="server">
        <div id="content">
            In order to modify your account, click on 
        </div>

    </asp:Panel>

     <asp:Panel ID="IRA" runat="server">
        <div id="content">
            To access your IRA...
        </div>

    </asp:Panel>


</asp:Content>
