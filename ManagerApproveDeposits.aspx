<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerApproveDeposits.aspx.vb" Inherits="KProject.ManagerApproveDeposits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id ="title">
        Approve Deposits<br />
        <br />
    </div>
    <div id ="footer">
        <div id="subtitle">Transactions Awaiting Manager Approval</div>
            <asp:GridView ID="gvTransactionsAwaitingManagerApproval" runat="server">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Approve" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
