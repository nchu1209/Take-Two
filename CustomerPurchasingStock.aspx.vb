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

    '    need to bring in the date and if there is enough to make the transaction
    '   There must be a validation for dates
    '    'need to connect price to the db
    'withdrawl ffromand also the transaction history
    'ETC


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists page 60
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If


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
            ddlAccounts.DataValueField = "AvailableBalance"
            'Session("AccountNumber") = DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountNumber")
            ddlAccounts.DataBind()
        End If



    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''
    '       START OF PURCHASE BUTTON                '
    '                                               '
    '''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub btnPurchaseStocks_Click(sender As Object, e As EventArgs) Handles btnPurchaseStocks.Click
        'Dim decTotalPurchasePrice As Decimal
        'decTotalPurchasePrice= 

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

            'get the specific price and fee for that stock
            strPrice = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString
            strFee = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Fee").ToString

            'Making sure the number is valid
            If t.Text <> "" Then
                If Not (Valid.CheckDigits(t.Text)) Then
                    lblErrorTransfer.Text = "ERROR: You did not enter a whole interger value when trying to purchase one or more stocks."
                    Exit Sub

                    'make sure selected date is greater or equal to system date
                    If DBDate.CheckSelectedDate(PurchaseCalendar.SelectedDate) = -1 Then
                        lblErrorTransfer.Text = "Please select a trans date that has not already passed"
                        Exit Sub
                    End If



                Else
                    'decTotal = CDec(strPrice) * CDec(t.Text) + (CDec(strFee))
                    
                    
                End If
            End If
                    Next










        '       START OF DESCRIPTIVE MESSAGE IF PURCHASE GETS APPROVED  
        Dim strDescriptiveMessage As String
        strDescriptiveMessage = "<strong>Purchased: </strong> </br>"

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
            If t.Text = "" Then
                t.Text = 0
            End If
            decTotal = CDec(strPrice) * CDec(t.Text) + (CDec(strFee))
            mdecSumTotal += decTotal

            If mdecSumTotal > ddlAccounts.SelectedValue.ToString Then
                lblErrorTransfer.Text = "We're sorry, insufficient funds."
                Exit Sub
            End If




            'DBAccounts.GetAccountByCustomerNumberTransferPurchaseStocks(Session("CustomerNumber").ToString)
            'Session("AccountNumber") = DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountNumber")


            'DBDisputeManager.GetAccountByAccountNumber(Session("AccountNumber"))

            'mdecAvailableBalance = DBDisputeManager.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")






            If t.Text = 1 Then

                strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " share of " + strTick + " for the price of :$" + CStr(decTotal) + "<br/>"


            ElseIf t.Text > 1 Then
                strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " shares of " + strTick + " for the price of :$" + CStr(decTotal) + "<br/>"

            End If

        Next

        strDescriptiveMessage = strDescriptiveMessage.ToString + "        = <strong>" + CStr(mdecSumTotal.ToString("C2")) + "</strong>"
        lblErrorTransfer.Text = strDescriptiveMessage


    End Sub

   
    Protected Sub TransferCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles PurchaseCalendar.SelectionChanged
        txtDate.Text = PurchaseCalendar.SelectedDate
    End Sub
End Class