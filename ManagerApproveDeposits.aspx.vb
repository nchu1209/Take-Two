Public Class ManagerApproveDeposits
    Inherits System.Web.UI.Page

    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccount As New ClassDBAccounts
    Dim DBDate As New ClassDBDate

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
        Dim datDate As Date = CDate(gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(6).Text)
        DBTransactions.ModifyTransactionManagerApproved("Approved", strTransactionNumber)
        'always change to approved
        'if date is today, do the stuff already built below
        'if date is not today, pass into manager set date, build functionality there wiht the stuff below for transactions if ManagerApproved = "Approved"
        DBDate.GetDate()
        If DBDate.CheckSelectedDate(datDate) = 0 Or DBDate.CheckSelectedDate(datDate) = -1 Then
            DBAccount.GetBalanceByAccountNumber(strAccountNumber)
            Dim decBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")
            Dim decAvailableBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance")

            decBalance += decDepositAmt
            decAvailableBalance += decDepositAmt

            DBAccount.UpdateBalance(CInt(strAccountNumber), decBalance)
            DBAccount.UpdateAvailableBalance(CInt(strAccountNumber), decAvailableBalance)

            Response.Redirect("ManagerApproveDeposits.aspx")
        End If
    End Sub
End Class