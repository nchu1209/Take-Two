Public Class CustomerPurchasingStock
    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim mCustomerID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists page 60
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If


        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
        ''get the record id from the select
        mCustomerID = CInt(Session("CustomerNumber"))

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

        pnlPurchaseStocks.Visible = False
        pnlNotApproved.Visible = True


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






       
    End Sub
End Class