Public Class CustomerPurchasingStock
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim mCustomerID As Integer
    Dim Valid As New ClassStockValidation

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists page 60
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If


        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
        ''get the record id from the select
        mCustomerID = CInt(Session("CustomerNumber"))



        'NEED TO MAKE MANAGER APPROVED VARIBALE WORK BY DOING SELECCTION IN DB ACCOUNTS and also set session for customer login
        'this will go in a asub in DB Accounts. CAll it in the customer login
        'Select ManagerApprovedStockAccount from tblAccounts where CustomerID= 10001 and AccountType= 'Stock'

        'Dim strApprovedStockAccount As String
        'strApprovedStockAccount = Session("CustomerManagerApprovedStockAccount")
        'If strApprovedStockAccount <> "" Then
        '    If CBool(strApprovedStockAccount) <> True Then
        '        pnlNotApproved.Visible = True
        '        pnlPurchaseStocks.Visible = False
        '        Exit Sub

        '    Else
        '        pnlNotApproved.Visible = False
        '        pnlPurchaseStocks.Visible = True
        '    End If
        'Else
        '    pnlNotApproved.Visible = True
        '    pnlPurchaseStocks.Visible = False

        'End If

        pnlPurchaseStocks.Visible = True
        pnlNotApproved.Visible = False


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
        strDescriptiveMessage = ""

        'Start of loop to validate and add to db
        For i = 0 To gvPurchaseStocks.Rows.Count - 1

            'find the quantity
            Dim t As TextBox = DirectCast(gvPurchaseStocks.Rows(i).Cells(5).FindControl("txtQuantity"), TextBox)

            'Making sure the number is valid
            If t.Text <> "" Then
                If Not (Valid.CheckDigits(t.Text)) Then
                    lblErrorTransfer.Text = "ERROR: You did not enter a whole interger value when trying to purchase one or more stocks."
                    Exit Sub

                Else
                   
                    'ADD TO THE DB
                    If t.Text >= 1 Then

                        strDescriptiveMessage = strDescriptiveMessage.ToString + t.Text & "<br/>"
                        lblErrorTransfer.Text = strDescriptiveMessage
                    End If
                End If


            End If


        Next




    End Sub
End Class