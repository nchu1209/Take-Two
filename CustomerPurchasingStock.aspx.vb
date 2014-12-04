Public Class CustomerPurchasingStock
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim DBStocks As New ClassDBStocks
    Dim mCustomerID As Integer
    Dim Valid As New ClassStockValidation
    Dim DBDate As New ClassDBDate
    Dim mdecSumTotal As Decimal
    Dim DBDisputeManager As New ClassDBDisputeManager
    Dim mdecAvailableBalance As Decimal
    Dim mdecOriginalAvailableBalance As Decimal
    Dim mdecBalance As Decimal
    Dim mdatSystemDate As Date
    Dim mdecNewBalance As Decimal
    Dim mdecNewAvailableBalance As Decimal
    Dim DBTransactions As New ClassDBTransactions
    Dim DBPending As New ClassDBPending
    Dim mdecFeeTotal As Decimal
    Dim mdecidk As Decimal
    Dim mdecIStillDK As Decimal

    '    need to bring in the date and if there is enough to make the transaction
    '   There must be a validation for dates
    '    'need to connect price to the db
    'withdrawl ffromand also the transaction history
    'ETC


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        ''check to see if session emptype exists page 60
        'If Session("CustomerNumber") Is Nothing Then
        '    Response.Redirect("CustomerLogin.aspx")
        'End If


        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
        ''get the record id from the select
        mCustomerID = CInt(Session("CustomerNumber"))



        'Makes sure that the manager has approved that they can view this
        Dim strApprovedStockAccount As String
        strApprovedStockAccount = Session("CustomerManagerApprovedStockAccount")
        If strApprovedStockAccount <> "" Then
            If strApprovedStockAccount <> "True" Then
                pnlNotApproved.Visible = True
                pnlPurchaseStocks.Visible = False
                Exit Sub

            Else
                pnlNotApproved.Visible = False
                pnlPurchaseStocks.Visible = True
            End If
        Else
            pnlNotApproved.Visible = True
            pnlPurchaseStocks.Visible = False

        End If


        'filling the ddl
        If IsPostBack = False Then
            DBAccounts.GetAccountByCustomerNumberTransferPurchaseStocks(Session("CustomerNumber").ToString)
            ddlAccounts.DataSource = DBAccounts.AccountsDataset5
            ddlAccounts.DataTextField = "Details"
            ddlAccounts.DataValueField = "AccountNumber"
            'Session("AccountNumber") = DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountNumber")
            ddlAccounts.DataBind()
        End If

        lblErrorTransfer.Text = ""

    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''
    '       START OF PURCHASE BUTTON                '
    '                                               '
    '''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub btnPurchaseStocks_Click(sender As Object, e As EventArgs) Handles btnPurchaseStocks.Click
        'Dim decTotalPurchasePrice As Decimal
        'decTotalPurchasePrice= 

        Dim strDescription As String

        'START OF VALIDATIONS AND ADDING TO DB
        For i = 0 To gvPurchaseStocks.Rows.Count - 1

            'find the quantity
            Dim t As TextBox = DirectCast(gvPurchaseStocks.Rows(i).Cells(5).FindControl("txtQuantity"), TextBox)
            Dim strTick As String = gvPurchaseStocks.Rows(i).Cells(0).Text
            Dim strPrice As String
            Dim strFee As String
            'Dim decTotal As Decimal

            'get all the stocks
            DBStocks.GetAllStocks()

            'get the stock information according to its unique ticker
            DBStocks.GetByTickerSymbol(strTick)

            ''get the specific price and fee for that stock
            'strPrice = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString
            'strFee = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Fee").ToString

            'Making sure the number is valid
            If t.Text <> "" Or t.Text <> "0" Then
                If Not (Valid.CheckDigits(t.Text)) Then
                    lblErrorTransfer.Text = "ERROR: You did not enter a whole interger value when trying to purchase one or more stocks."
                    Exit Sub

                Else
                    'decTotal = CDec(strPrice) * CDec(t.Text) + (CDec(strFee))

                End If
            End If
        Next

        'make sure selected date is greater or equal to system date
        If DBDate.CheckSelectedDate(PurchaseCalendar.SelectedDate) = -1 Then
            lblErrorTransfer.Text = "Please select a date that has not already passed"
            Exit Sub
        End If

        '       START OF DESCRIPTIVE MESSAGE IF PURCHASE GETS APPROVED  
        Dim strDescriptiveMessage As String
        strDescriptiveMessage = "<strong>Purchased: </strong> </br>"

        DBDisputeManager.GetAccountByAccountNumber(ddlAccounts.SelectedValue.ToString)

        mdecOriginalAvailableBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")
        mdecAvailableBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")
        mdecidk = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")
        mdecBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")
        mdecIStillDK = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")

        'DESCRIPTIVE MESSAGE AND ADD TO THE DB
        For i = 0 To gvPurchaseStocks.Rows.Count - 1

            'find the quantity
            Dim t As TextBox = DirectCast(gvPurchaseStocks.Rows(i).Cells(5).FindControl("txtQuantity"), TextBox)
            Dim strTick As String = gvPurchaseStocks.Rows(i).Cells(0).Text
            Dim strPrice As String
            Dim strFee As String
            Dim decTotal As Decimal

            'get all the stocks
            DBStocks.GetAllStocks()

            'get the stock information according to its unique ticker
            DBStocks.GetByTickerSymbol(strTick)

            'get the specific price and fee for that stock
            strPrice = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString
            strFee = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Fee").ToString

            'Making sure the number is valid
            If t.Text = "" Or t.Text = "0" Then
                t.Text = 0
                decTotal = 0
                strFee = "0"
                mdecSumTotal += decTotal
                mdecFeeTotal += CDec(strFee)
            Else
                decTotal = CDec(strPrice) * CDec(t.Text) + (CDec(strFee))
                mdecSumTotal += decTotal
                mdecFeeTotal += CDec(strFee)
            End If


            If mdecSumTotal > mdecOriginalAvailableBalance Then
                lblErrorTransfer.Text = "We're sorry, insufficient funds."
                Exit Sub
            End If


            DBDate.GetDate()
            mdatSystemDate = CDate(DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))
            DBTransactions.GetMaxTransactionNumber()
            Dim intMaxTransaction As Integer
            intMaxTransaction = CInt(DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber").ToString) + 1

            If mdatSystemDate = PurchaseCalendar.SelectedDate And t.Text >= 1 Then
                'if the transaction occurs today, this is going to update the account information
                strDescription = "Fee for purchase of " & DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Name").ToString
                mdecIStillDK = CDec(mdecIStillDK - CDec(strFee))
                mdecidk = CDec(mdecidk - CDec(strFee))
                DBTransactions.AddTransaction(intMaxTransaction, CInt(ddlAccounts.SelectedValue.ToString), "Fee", PurchaseCalendar.SelectedDate.ToString, CDec(strFee), strDescription, mdecIStillDK, Nothing, "", mdecidk, "Fee")


                'DBStocks.AddStockPortfolio((ddlAccounts.SelectedValue.ToString, strTick, NumberOfSharesHeld, StockAccountNumber, CInt(t.Text), StockType, PurchasePrice)
            ElseIf t.Text >= 1 Then
                ' if in the future
                'if the transaction occurs in the future, this is going to updat the account information

                strDescription = "Fee for purchase of " & DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Name").ToString


                mdecidk = CDec(mdecidk - CDec(strFee))
                DBPending.AddTransactionPending(intMaxTransaction, CInt(ddlAccounts.SelectedValue.ToString), "Fee", PurchaseCalendar.SelectedDate.ToString, CDec(strFee), CDec(mdecBalance), strDescription, mdecidk)
            End If

            If t.Text = 1 Then
                strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " share of " + strTick + " for the price of: $" + CStr(decTotal) + "<br/>"

            ElseIf t.Text > 1 Then
                strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " shares of " + strTick + " for the price of: $" + CStr(decTotal) + "<br/>"
            End If

        Next


        'future dont change the balance, change the available balance
        'if today change balance and available balance

        'get date
        'check date
        ' if =1 or =0 (?) then its today

        ' Session("AccountNumber") = DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountNumber")


        'get date

        'decBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")
        'decAvailableBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")
        mdecNewBalance = mdecBalance - mdecSumTotal
        mdecNewAvailableBalance = mdecAvailableBalance - mdecSumTotal


        DBDate.GetDate()
        DBTransactions.GetMaxTransactionNumber()
        Dim intMaxTransaction2 As Integer
        intMaxTransaction2 = CInt(DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber").ToString) + 1
        mdatSystemDate = CDate(DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))
        If mdatSystemDate = PurchaseCalendar.SelectedDate Then
            'if the transaction occurs today, this is going to update the account information

            strDescription = "Stock Purchase - Account " & ddlAccounts.SelectedValue.ToString

            DBStocks.ModifyAccountBalances(mdecNewBalance.ToString, mdecNewAvailableBalance.ToString, ddlAccounts.SelectedValue.ToString)

            DBTransactions.AddTransaction(intMaxTransaction2, CInt(ddlAccounts.SelectedValue.ToString), "Withdrawal", PurchaseCalendar.SelectedDate.ToString, mdecSumTotal - mdecFeeTotal, strDescription, mdecBalance - mdecSumTotal, Nothing, "", mdecAvailableBalance - mdecSumTotal, "Withdrawal")


        Else ' if in the future
            'if the transaction occurs in the future, this is going to updat the account information
            DBStocks.ModifyAccountBalance1(mdecNewAvailableBalance.ToString, ddlAccounts.SelectedValue.ToString)

            '**** may want to add the XXXXXXXXX0004 to this

            strDescription = "Stock Purchase - Account " & ddlAccounts.SelectedValue.ToString
            'Dim decAvailableBalance As Decimal
            'decAvailableBalance = CDec(mdecAvailableBalance - CDec(strFee))
            DBPending.AddTransactionPending(intMaxTransaction2, CInt(ddlAccounts.SelectedValue.ToString), "Withdrawal", PurchaseCalendar.SelectedDate.ToString, mdecSumTotal - mdecFeeTotal, mdecBalance, strDescription, mdecAvailableBalance - mdecSumTotal)

        End If



        'DBAccounts.GetAccountByCustomerNumberTransferPurchaseStocks(Session("CustomerNumber").ToString)


        'mdecAvailableBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")




        strDescriptiveMessage = strDescriptiveMessage.ToString + "        = <strong>" + CStr(mdecSumTotal.ToString("C2")) + "</strong>"
        lblErrorTransfer.Text = strDescriptiveMessage

        'lblErrorTransfer.Text = mdecNewBalance.ToString
        DBAccounts.GetAccountByCustomerNumberTransferPurchaseStocks(Session("CustomerNumber").ToString)
        ddlAccounts.DataSource = DBAccounts.AccountsDataset5
        ddlAccounts.DataTextField = "Details"
        ddlAccounts.DataValueField = "AccountNumber"
        'Session("AccountNumber") = DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountNumber")
        ddlAccounts.DataBind()
    End Sub

   
    Protected Sub TransferCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles PurchaseCalendar.SelectionChanged
        txtDate.Text = PurchaseCalendar.SelectedDate
    End Sub


End Class