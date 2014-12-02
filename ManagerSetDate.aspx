<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerSetDate.aspx.vb" Inherits="KProject.ManagerSetDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
     <div id ="title">
        Save the Date
         <br />
         <br />
    </div>
    <div id="center">
         <asp:Calendar ID="calSetDate" runat="server">
         </asp:Calendar>
         <br />
         <br />
         Change today&#39;s date to
         <asp:TextBox ID="txtDate" runat="server" ReadOnly="True"></asp:TextBox>
         <br />
         <br />
&nbsp;<asp:Button ID="btnConfirm" runat="server" Text="Confirm Date Change" />
        <br />
        <br />
    </div>
</asp:Content>
