Public Class CustomerSellingStockConfirm
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DBTransaction As New ClassDBTransactions
    Dim DBStocks As New ClassDBStocks

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

        'update transaction : type, deposit, amt: need to pass current price as well, amount is current price * number sold
        ' cont^, description includes ticker, shares sold , Session("PurchasePrice"), Session("CurrentPrice"), total gains/losses
        DBStocks.GetByStockTicker(lblTicker.Text)
        'DBStocks.StocksDataset2

        'add dtransaction fee transactions: deducted from the cash value portion of the portfolio, descr: "fee for sale of [ticker]

        'if session("sharesRemaining") = 0 then remove?!?! from stock portion of portfolio page

        'update balanced and unbalanced portfolio

        '''''check for balanced and unbalanced portfolios

        lblError.Text = "Sale confirmed"
        Response.AddHeader("Refresh", "2; URL= CustomerPortfolioDetail.aspx")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("CustomerSellingStock.aspx")
    End Sub
End Class