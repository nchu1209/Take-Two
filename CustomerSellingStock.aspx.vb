Public Class CustomerSellingStock
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

    End Sub

End Class