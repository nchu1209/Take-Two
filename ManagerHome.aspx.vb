Option Strict On
Public Class ManagerHome
    Inherits System.Web.UI.Page

    Dim dbaccounts As New ClassDBAccounts
    Dim dbtransactions As New ClassDBTransactions
    Dim dbdate As New ClassDBDate

    Const BONUS_PERCENT As Decimal = 0.1D

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("empID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        lblConfirmation.Text = ""
        lblMessage.Text = ""

    End Sub

    Protected Sub btnProcessPortfolios_Click(sender As Object, e As EventArgs) Handles btnProcessPortfolios.Click
        dbaccounts.GetBalancedPortfolios()
        dbdate.GetDate()
        Dim datToday As Date = CDate(dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))
        For i = 0 To dbaccounts.AccountsDataset.Tables("tblAccounts").Rows.Count - 1
            Dim decTotalStockValue As Decimal = CDec(dbaccounts.AccountsDataset.Tables("tblAccounts").Rows(i).Item("StockValue"))
            Dim decBonus As Decimal = BONUS_PERCENT * decTotalStockValue

            GetTransactionNumber()
            Dim intAccountNumber As Integer = CInt(dbaccounts.AccountsDataset.Tables("tblAccounts").Rows(i).Item("AccountNumber"))
            Dim decBalance As Decimal = CDec(dbaccounts.AccountsDataset.Tables("tblAccounts").Rows(i).Item("Balance")) + decBonus
            Dim decAvailableBalance As Decimal = CDec(dbaccounts.AccountsDataset.Tables("tblAccounts").Rows(i).Item("AvailableBalance")) + decBonus
            dbaccounts.UpdateBalance(intAccountNumber, decBalance)
            dbaccounts.UpdateAvailableBalance(intAccountNumber, decAvailableBalance)

            dbtransactions.AddTransaction(CInt(Session("TransactionNumber")), intAccountNumber, "Bonus", datToday.ToString, decBonus, "Balanced Portfolio Bonus", decBalance, Nothing, "", decAvailableBalance, "Bonus")

        Next

        lblConfirmation.Text = "You have successfully processed stock portfolios."

        Response.AddHeader("Refresh", "2; URL= ManagerHome.aspx")
    End Sub

    Private Sub GetTransactionNumber()
        dbtransactions.GetMaxTransactionNumber()
        If dbtransactions.TransactionsDataset.Tables("tblTransactions").Rows.Count = 0 Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(dbtransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub
End Class