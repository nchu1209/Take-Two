Option Strict On
Public Class CustomerAccountDetail

    Inherits System.Web.UI.Page
    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccount As New ClassDBAccounts
    Dim DBDate As New ClassDBDate
    Dim Val As New ClassValidate
    Dim DBAccounts As ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ''redirect to create account if no accounts

            If Session("CustomerFirstName") Is Nothing Then
                Response.Redirect("CustomerLogin.aspx")
            End If

            If Session("CustomerNumber").ToString = "" Then
                Response.Redirect("CustomerCreateBankAccount.aspx")
            End If
            'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
            'If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            '    Response.Redirect("CustomerCreateBankAccount.aspx")
            'End If

            If Request.QueryString("ID") Is Nothing Then
                DBTransactions.GetAllTransactions(Session("AccountNumber").ToString)
            Else
                Dim strAccountNumber As String
                strAccountNumber = "100000" & Request.QueryString("ID").Substring(6, 4)
                Session("AccountNumber") = strAccountNumber
            End If

            DBAccount.GetAccountNameByAccountNumber(Session("AccountNumber").ToString)
            lblAccountName.Text = DBAccount.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountName").ToString

            DBTransactions.GetAllTransactionsandPending(Session("AccountNumber").ToString)
            DBTransactions.GetAllTransactionsAwaitingManagerApproval(Session("AccountNumber").ToString, "Needed")

            Search()
        End If
        DBTransactions.GetAllTransactionsandPending(Session("AccountNumber").ToString)
        DBTransactions.GetAllTransactionsAwaitingManagerApproval(Session("AccountNumber").ToString, "Needed")
    End Sub

    Public Sub Search()
        'bind the gridview to the dataview
        gvTransactionsAwaitingManagerApproval.DataSource = DBTransactions.MyView2
        gvTransactionsAwaitingManagerApproval.DataBind()

        gvViewAllTransactions.DataSource = DBTransactions.MyView
        gvViewAllTransactions.DataBind()
    End Sub

    Protected Sub btnTransactionSearch_Click(sender As Object, e As EventArgs) Handles btnTransactionSearch.Click
        Response.Redirect("CustomerTransactionSearch.aspx")
    End Sub
End Class