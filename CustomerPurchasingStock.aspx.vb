Public Class CustomerPurchasingStock
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim DBStocks As New ClassDBStocks
    Dim mCustomerID As Integer
    Dim Valid As New ClassStockValidation

    '    need to bring in the date and if there is enough to make the transaction
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


        'this is basically same as in the perform transactions but it takes only checking savings and stock
        If IsPostBack = False Then
            DBAccounts.GetAccountByCustomerNumberTransferPurchaseStocks(Session("CustomerNumber").ToString)
            ddlAccounts.DataSource = DBAccounts.AccountsDataset5
            ddlAccounts.DataTextField = "Details"
            ddlAccounts.DataValueField = "AccountNumber"
            ddlAccounts.DataBind()
        End If



    End Sub



    Protected Sub btnPurchaseStocks_Click(sender As Object, e As EventArgs) Handles btnPurchaseStocks.Click
        Dim strDescriptiveMessage As String
        strDescriptiveMessage = "<strong>Purchased: </strong> </br>"

        'Start of loop to validate and add to db
        For i = 0 To gvPurchaseStocks.Rows.Count - 1

            'find the quantity
            Dim t As TextBox = DirectCast(gvPurchaseStocks.Rows(i).Cells(5).FindControl("txtQuantity"), TextBox)
            Dim strTick As String = gvPurchaseStocks.Rows(i).Cells(0).Text
            Dim strPrice As String
            Dim strFee As String
            Dim decTotal As Decimal
            DBStocks.GetAllStocks()
            DBStocks.GetByTickerSymbol(strTick)


            strPrice = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString
            strFee = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("Fee").ToString

            'Making sure the number is valid
            If t.Text <> "" Then
                If Not (Valid.CheckDigits(t.Text)) Then
                    lblErrorTransfer.Text = "ERROR: You did not enter a whole interger value when trying to purchase one or more stocks."
                    Exit Sub

                Else
                    decTotal = CDec(strPrice) * CDec(t.Text) + (CDec(strFee))
                    'ADD TO THE DB
                    If t.Text = 1 Then

                        strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " share of " + strTick + " for the price of :$" + CStr(decTotal) + "<br/>"
                        lblErrorTransfer.Text = strDescriptiveMessage

                    ElseIf t.Text > 1 Then
                        strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text + " shares of " + strTick + " for the price of :$" + CStr(decTotal) + "<br/>"
                        lblErrorTransfer.Text = strDescriptiveMessage

                    End If
                End If


            End If


        Next




    End Sub
End Class