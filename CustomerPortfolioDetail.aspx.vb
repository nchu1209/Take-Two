Public Class CustomerPortfolioDetail
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts
    Dim DBStocks As New ClassDBStocks

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        DBStocks.GetMaxTransactionNumberByCustomerID(Session("CustomerNumber").ToString)

    End Sub

End Class