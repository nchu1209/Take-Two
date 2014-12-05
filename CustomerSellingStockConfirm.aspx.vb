Public Class CustomerSellingStockConfirm
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DBTransaction As New ClassDBTransactions
    Dim DBStocks As New ClassDBStocks
    Dim DBDate As New ClassDBDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        If Session("Ticker") = Nothing Then
            Response.Redirect("CustomerSellingStock.aspx")
        End If

        lblTicker.Text = Session("Ticker").ToString
        lblSharesSold.Text = Session("intAmount").ToString
        lblRemainingShares.Text = Session("sharesRemaining").ToString
        lblFees.Text = Session("CurrentFees").ToString
        lblProfit.Text = (CDec(Session("CurrentPrice")) - CDec(Session("PurchasePrice"))) * CInt(Session("intAmount"))
    End Sub


    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        'updates!
        'update portfolio with new stock amounts
        Dim intSharesHeld As Integer = CInt(lblSharesSold.Text) + CInt(lblRemainingShares.Text)
        DBStocks.UpdateStockTransactionsShares(CInt(lblRemainingShares.Text), CInt(Session("SetNumber")), lblTicker.Text)

        'update new portfolio price: now in cash
        DBAccounts.GetStockAccountByCustomerNumber2(Session("CustomerNumber").ToString)
        Dim intStockAccountNumber As Integer = CInt(DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows(0).Item("AccountNumber"))
        Dim decTransactionAmount As Decimal = CDec(Session("CurrentPrice")) * CInt(lblSharesSold.Text)

        Dim decBalance As Decimal = CDec(DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows(0).Item("Balance")) + decTransactionAmount
        Dim decAvailableBalance As Decimal = CDec(DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + decTransactionAmount
        DBAccounts.UpdateBalance(intStockAccountNumber, decBalance)
        DBAccounts.UpdateAvailableBalance(intStockAccountNumber, decAvailableBalance)

        'update transaction : type, deposit, amt: need to pass current price as well, amount is current price * number sold
        ' cont^, description includes ticker, shares sold , Session("PurchasePrice"), Session("CurrentPrice"), total gains/losses
        DBStocks.GetByStockTicker(lblTicker.Text)
        GetTransactionNumber()
        DBDate.GetDate()
        Dim datToday = CDate(DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))

        Dim strDescription As String = "Sold " & lblSharesSold.Text & " shares of " & lblTicker.Text & ", bought at " & Session("PurchasePrice").ToString & "/share and sold at " & Session("CurrentPrice").ToString & " for net profit of " & lblProfit.Text

        DBTransaction.AddTransaction(CInt(Session("TransactionNumber")), intStockAccountNumber, "Deposit", datToday.ToString, decTransactionAmount, strDescription, decBalance, Nothing, "", decAvailableBalance, "Deposit")

        'add dtransaction fee transactions: deducted from the cash value portion of the portfolio, descr: "fee for sale of [ticker]
        GetTransactionNumber()
        decBalance = decBalance - CDec(lblFees.Text)
        decAvailableBalance = decAvailableBalance - CDec(lblFees.Text)
        DBAccounts.UpdateBalance(intStockAccountNumber, decBalance)
        DBAccounts.UpdateAvailableBalance(intStockAccountNumber, decAvailableBalance)
        Dim strFeeDescription As String = "Fee for sale of " & lblTicker.Text
        DBTransaction.AddTransaction(CInt(Session("TransactionNumber")), intStockAccountNumber, "Fee", datToday, CDec(lblFees.Text), strFeeDescription, decBalance, Nothing, "", decAvailableBalance, "Fee")

        lblError.Text = "Sale confirmed"
        Response.AddHeader("Refresh", "2; URL= CustomerPortfolioDetail.aspx")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("CustomerSellingStock.aspx")
    End Sub

    Private Sub GetTransactionNumber()
        DBTransaction.GetMaxTransactionNumber()
        If DBTransaction.TransactionsDataset.Tables("tblTransactions").Rows.Count = 0 Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(DBTransaction.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub
End Class