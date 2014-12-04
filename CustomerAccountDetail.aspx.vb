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

            If Session("CustomerNumber").ToString = "" Then
                Response.Redirect("CustomerCreateBankAccount.aspx")
            End If
            'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
            'If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            '    Response.Redirect("CustomerCreateBankAccount.aspx")
            'End If

            If Request.QueryString("ID") Is Nothing Then
                DBTransactions.GetAllTransactions(Session("AccountNumber").ToString)
                DBAccount.GetAccountNameByAccountNumber(Session("AccountNumber").ToString)
                lblAccountName.Text = DBAccount.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountName").ToString
                Search()
            Else
                Dim strAccountNumber As String
                strAccountNumber = "100000" & Request.QueryString("ID").Substring(6, 4)
                Session("AccountNumber") = strAccountNumber

                ''''
                '1) MYVIEW is just the ones waiting approval from the manager
                'for this we will need to write a query to test if the transaction is awaiting approval
                'do we have something to see if an individual transaction is awaiting an approval
                'CUSTOMER NUMBER IS NOT LINKED TO ACCOUNT NUMBER in tbltransactions: need all accounts
                'need to join table accounts and tbltransactions on account number = account number; customernumber is second parameter for search

                '2) MYVIEW2 is for viewing all transactions, INCLUDING PENDING
                'for this, we will need a stored procedure to take a union of the two tables
                DBTransactions.GetAllTransactions(Session("AccountNumber").ToString)
                DBAccount.GetAccountNameByAccountNumber(Session("AccountNumber").ToString)
                lblAccountName.Text = DBAccount.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountName").ToString
                Search()


            End If
        End If
        DBTransactions.GetAllTransactions(Session("AccountNumber").ToString)
    End Sub

    Public Sub Search()
        'bind the gridview to the dataview
        gvTransactionsAwaitingManagerApproval.DataSource = DBTransactions.MyView
        gvTransactionsAwaitingManagerApproval.DataBind()

        gvViewAllTransactions.DataSource = DBTransactions.MyView2
        gvViewAllTransactions.DataBind()
    End Sub

    Protected Sub btnTransactionSearch_Click(sender As Object, e As EventArgs) Handles btnTransactionSearch.Click
        Response.Redirect("CustomerTransactionSearch.aspx")
    End Sub
End Class