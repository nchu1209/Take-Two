Option Strict On

Public Class CustomerHome

    Inherits System.Web.UI.Page

    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        ''Checking*
        'DBAccounts.GetCheckingAccountByCustomerNumber(Session("CustomerNumber").ToString)

        'If DBAccounts.AccountsDataset.Tables("tblAccounts").Rows.Count <> 0 Then
        '    lblCheckingAccount.Text = DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("AccountNumber").ToString
        'Else
        '    Checking.Visible = False
        'End If


        ''Savings*
        'DBAccounts.GetSavingsAccountByCustomerNumber(Session("CustomerNumber").ToString)

        'If DBAccounts.AccountsDataset.Tables("tblAccounts").Rows.Count <> 0 Then
        '    lblSavingsAccount.Text = DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("AccountNumber").ToString
        'Else
        '    Savings.Visible = False
        'End If


        ''IRA
        'DBAccounts.GetIRAAccountByCustomerNumber(Session("CustomerNumber").ToString)

        'If DBAccounts.AccountsDataset.Tables("tblAccounts").Rows.Count <> 0 Then
        '    lblIRAAccount.Text = DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("AccountNumber").ToString
        'Else
        '    Stock.Visible = False
        'End If


        ''Stocks
        'DBAccounts.GetStockAccountByCustomerNumber(Session("CustomerNumber").ToString)

        'If DBAccounts.AccountsDataset.Tables("tblAccounts").Rows.Count <> 0 Then
        '    lblStockAccount.Text = DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("AccountNumber").ToString
        'Else
        '    Stock.Visible = False
        'End If


        ''get all the customers from DB
        'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        ''display all the customers on the page
        'gvAccounts.DataSource = DBAccounts.AccountsDataset4
        'gvAccounts.DataBind()



    End Sub


    Protected Sub gvAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAccounts.SelectedIndexChanged
        lblError.Text = gvAccounts.SelectedRow.Cells(0).Text
        Exit Sub
        Session("AccountNumber") = "100000" & gvAccounts.SelectedRow.Cells(0).Text.Substring(6, 4)
        Response.Redirect("CustomerTransactionSearch.aspx")
    End Sub
End Class