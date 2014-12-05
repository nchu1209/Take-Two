Public Class CustomerSellingStock
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts
    Dim DBStocks As New ClassDBStocks

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If
    End Sub

    Protected Sub gvStocks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStocks.SelectedIndexChanged
        Response.Redirect("CustomerStockAdditionalDetails.aspx")
    End Sub
End Class