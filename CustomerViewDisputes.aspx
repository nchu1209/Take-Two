<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerViewDisputes.aspx.vb" Inherits="KProject.CustomerViewDisputes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        View All Disputes<br />
        <br />
    </div>
    <div id ="subtitle">

    </div>
    <div id ="footer">
        <asp:GridView ID="gvDisputes" runat="server"></asp:GridView>
    </div>



</asp:Content>