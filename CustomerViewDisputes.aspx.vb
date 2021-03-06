﻿Public Class CustomerViewDisputes
    Inherits System.Web.UI.Page
    Dim DBDispute As New ClassDBDispute
    Dim dbact As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        dbact.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If dbact.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        DBDispute.GetDisputeByAccountNumber(Session("CustomerNumber"))
        gvDisputes.DataSource = DBDispute.MyView
        gvDisputes.DataBind()
    End Sub
End Class