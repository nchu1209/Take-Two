Public Class ManagerApproveDeposits
    Inherits System.Web.UI.Page

    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccount As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            DBTransactions.GetDetailsByManagerApprovalNeeded("Needed")

            gvTransactionsAwaitingManagerApproval.DataSource = DBTransactions.MyView
            gvTransactionsAwaitingManagerApproval.DataBind()
        End If
    End Sub

    Protected Sub gvTransactionsAwaitingManagerApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTransactionsAwaitingManagerApproval.SelectedIndexChanged
        Dim strAccountNumber As String = gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(1).Text
        Dim strTransactionNumber As String = gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(2).Text
        Dim decDepositAmt As Decimal = CDec(gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(5).Text)
        DBTransactions.ModifyTransactionManagerApproved("Approved", strTransactionNumber)

        DBAccount.GetBalanceByAccountNumber(strAccountNumber)
        Dim decBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")
        Dim decAvailableBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance")

        decBalance += decDepositAmt
        decAvailableBalance += decDepositAmt

        DBAccount.UpdateBalance(CInt(strAccountNumber), decBalance)
        DBAccount.UpdateAvailableBalance(CInt(strAccountNumber), decAvailableBalance)

        Response.Redirect("ManagerApproveDeposits.aspx")
    End Sub
End Class