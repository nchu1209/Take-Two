Public Class CustomerPerformTransaction
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim DBTransactions As New ClassDBTransactions
    Dim DBDate As New ClassDBDate
    Dim DBPending As New ClassDBPending
    Dim Format As New ClassFormat
    Dim mCustomerID As Integer
    Dim Valid As New ClassValidate

    Const OVERDRAFT_MAXIMUM As Decimal = 50D
    Const OVERDRAFT_FEE As Decimal = 30D

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If


        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If


        If Session("CustomerNumber").ToString = "" Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)
        ''get the record id from the select
        mCustomerID = CInt(Session("CustomerNumber"))

        'clear labels
        lblErrorDeposit.Text = ""
        lblErrorWithdrawal.Text = ""
        lblErrorTransfer.Text = ""

        If IsPostBack = False Then
            SetFormNormal()
        End If
        txtDepositDate.ReadOnly = True
        txtTransferDate.ReadOnly = True
        txtWithdrawalDate.ReadOnly = True
    End Sub

    Private Sub SetFormNormal()
        WithdrawalPanel.Visible = False
        DepositPanel.Visible = False
        TransferPanel.Visible = False
        IRAFeeChoicePanel.Visible = False
    End Sub

    Public Function ValidateAmount(strIn As String)
        If IsNumeric(strIn) = False Then
            Return False
        ElseIf CDec(strIn) <= 0 Then
            Return False
        End If
        Return True
    End Function

    Public Sub GetTransactionNumber()
        DBTransactions.GetMaxTransactionNumber()
        If DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber") Is DBNull.Value Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub

    Protected Sub ddlTransactions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransactions.SelectedIndexChanged
        SetFormNormal()
        If ddlTransactions.SelectedValue = "Withdrawal" Then
            WithdrawalPanel.Visible = True
            DBAccounts.GetAccountByCustomerNumberTransfer(Session("CustomerNumber").ToString)
            ddlWithdrawal.DataSource = DBAccounts.AccountsDataset5
            ddlWithdrawal.DataTextField = "Details"
            ddlWithdrawal.DataValueField = "AccountNumber"
            ddlWithdrawal.DataBind()
        End If

        If ddlTransactions.SelectedValue = "Deposit" Then
            DepositPanel.Visible = True
            'fill the ddl
            'did not do in separate function since we have 4 different ddls on this page (gah pannels)
            DBAccounts.GetAccountByCustomerNumberTransfer(Session("CustomerNumber").ToString)
            ddlDeposit.DataSource = DBAccounts.AccountsDataset5
            ddlDeposit.DataTextField = "Details"
            ddlDeposit.DataValueField = "AccountNumber"
            ddlDeposit.DataBind()
        End If

        If ddlTransactions.SelectedValue = "Transfer" Then
            TransferPanel.Visible = True
            DBAccounts.GetAccountByCustomerNumberTransfer(Session("CustomerNumber").ToString)
            ddlFromAccount.DataSource = DBAccounts.AccountsDataset5
            ddlFromAccount.DataTextField = "Details"
            ddlFromAccount.DataValueField = "AccountNumber"
            ddlFromAccount.DataBind()

            DBAccounts.GetAccountByCustomerNumberTransfer2(Session("CustomerNumber").ToString)
            ddlTransferTo.DataSource = DBAccounts.AccountsDataset7
            ddlTransferTo.DataTextField = "Details"
            ddlTransferTo.DataValueField = "AccountNumber"
            ddlTransferTo.DataBind()
        End If
    End Sub

    Protected Sub WithdrawalCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles WithdrawalCalendar.SelectionChanged
        txtWithdrawalDate.Text = WithdrawalCalendar.SelectedDate
    End Sub

    Protected Sub DepositCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles DepositCalendar.SelectionChanged
        txtDepositDate.Text = DepositCalendar.SelectedDate
    End Sub

    Protected Sub TransferCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles TransferCalendar.SelectionChanged
        txtTransferDate.Text = TransferCalendar.SelectedDate
    End Sub

    Protected Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        If ValidateAmount(txtDepositAmount.Text) = False Then
            lblErrorDeposit.Text = "Please enter a positive, numeric amount"
            Exit Sub
        End If

        'make sure selected date is greater or equal to system date
        If DBDate.CheckSelectedDate(DepositCalendar.SelectedDate) = -1 Then
            lblErrorDeposit.Text = "Please select a deposit date that has not already passed"
            Exit Sub
        End If

        Dim strManagerApprovalNeeded As String = ""
        If CDec(txtDepositAmount.Text) > 5000 Then
            strManagerApprovalNeeded = "Needed"
        End If

        'if selected account is an IRA, make sure they haven't reached their maximum deposit for the year
        'if they have, suggest the maximum contribution
        'update IRATotalDeposit
        Dim decBalance As Decimal
        Dim decAvailableBalance As Decimal
        DBAccounts.GetBalanceByAccountNumber(ddlDeposit.SelectedValue.ToString)
        decBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        decAvailableBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        DBAccounts.GetAccountTypeByAccountNumber(ddlDeposit.SelectedValue.ToString)
        Dim strIRA As String
        If DBAccounts.AccountsDataset9.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            strIRA = "True"
        Else
            strIRA = "False"
        End If

        If DBAccounts.AccountsDataset9.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            Dim decIRATotal As Decimal
            DBAccounts.GetIRATotalDepositByAccountNumber(ddlDeposit.SelectedValue.ToString)
            decIRATotal = CDec(DBAccounts.AccountsDataset8.Tables("tblAccounts").Rows(0).Item("IRATotalDeposit"))
            Dim decMaxIRADeposit As Decimal
            decMaxIRADeposit = 5000 - decIRATotal
            If CDec(txtDepositAmount.Text) > decMaxIRADeposit Then
                lblErrorDeposit.Text = "You cannot contribute more than $5000 per year to your IRA. Would you like to contribute the maximum deposit, " + decMaxIRADeposit.ToString + " instead?"
                txtDepositAmount.Text = decMaxIRADeposit.ToString
                Exit Sub
            End If
            decIRATotal += CDec(txtDepositAmount.Text)
            DBAccounts.UpdateIRATotalDeposit(CInt(ddlDeposit.SelectedValue), decIRATotal)
        End If


        decBalance += CDec(txtDepositAmount.Text)
        Dim strDepositMessage As String
        strDepositMessage = "Deposited " & txtDepositAmount.Text & " to account " & ddlDeposit.SelectedValue.ToString & " on " & txtDepositDate.Text
        GetTransactionNumber()

        If strManagerApprovalNeeded = "" Then
            If DBDate.CheckSelectedDate(DepositCalendar.SelectedDate) = 0 Then
                'update the balance
                DBAccounts.UpdateBalance(CInt(ddlDeposit.SelectedValue), decBalance)
                'update the available balance
                decAvailableBalance += CDec(txtDepositAmount.Text)
                DBAccounts.UpdateAvailableBalance(CInt(ddlDeposit.SelectedValue), decAvailableBalance)
                'update the transactions table
                DBTransactions.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlDeposit.SelectedValue), "Deposit", txtDepositDate.Text, CDec(txtDepositAmount.Text), strDepositMessage, decBalance, Nothing, strIRA, decAvailableBalance, "Deposit")
            Else
                DBPending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlDeposit.SelectedValue), "Deposit", txtDepositDate.Text, CDec(txtDepositAmount.Text), strDepositMessage, Nothing, strIRA)
            End If
        Else
            If DBDate.CheckSelectedDate(DepositCalendar.SelectedDate) = 0 Then
                DBTransactions.AddTransactionNeedsApproval(CInt(Session("TransactionNumber")), CInt(ddlDeposit.SelectedValue), "Deposit", txtDepositDate.Text, CDec(txtDepositAmount.Text), strDepositMessage, decBalance, Nothing, strIRA, decAvailableBalance, "Deposit", strManagerApprovalNeeded)
            Else
                DBPending.AddTransactionNeedsApproval(CInt(Session("TransactionNumber")), CInt(ddlDeposit.SelectedValue), "Deposit", txtDepositDate.Text, CDec(txtDepositAmount.Text), strDepositMessage, Nothing, strIRA, strManagerApprovalNeeded)
            End If
        End If
        

        lblErrorDeposit.Text = "Deposit Confirmed"
        Response.AddHeader("Refresh", "2; URL= CustomerPerformTransaction.aspx")
    End Sub

    Protected Sub btnWithdrawal_Click(sender As Object, e As EventArgs) Handles btnWithdrawal.Click
        If ValidateAmount(txtWithdrawalAmount.Text) = False Then
            lblErrorWithdrawal.Text = "Please enter a positive, numeric amount"
            Exit Sub
        End If

        'make sure selected date is greater or equal to system date
        If DBDate.CheckSelectedDate(WithdrawalCalendar.SelectedDate) = -1 Then
            lblErrorWithdrawal.Text = "Please select a withdrawal date that has not already passed"
            Exit Sub
        End If

        'update the balance
        Dim decBalance As Decimal
        Dim decAvailableBalance As Decimal
        DBAccounts.GetBalanceByAccountNumber(ddlWithdrawal.SelectedValue.ToString)
        decBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        decAvailableBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        'make sure that you are not withdrawing more than you have in the current account
        '****check to make sure you can't overdraw with overdraft fees
        If decAvailableBalance < CInt(txtWithdrawalAmount.Text) Then
            lblErrorWithdrawal.Text = "Please enter an amount to withdraw less than or equal to the maximum overdraft ($50) for this account."
            Exit Sub
        End If

        decBalance = decBalance - CDec(txtWithdrawalAmount.Text)
        decAvailableBalance = decAvailableBalance - CDec(txtWithdrawalAmount.Text)
        Dim strWithdrawalMessage As String
        strWithdrawalMessage = "Withdrew " & txtWithdrawalAmount.Text & " from account " & ddlWithdrawal.SelectedValue.ToString & " on " & txtWithdrawalDate.Text
        GetTransactionNumber()
        If DBDate.CheckSelectedDate(WithdrawalCalendar.SelectedDate) = 0 Then
            DBAccounts.UpdateBalance(CInt(ddlWithdrawal.SelectedValue), decBalance)
            DBAccounts.UpdateAvailableBalance(CInt(ddlWithdrawal.SelectedValue), decAvailableBalance)
            'update the transactions table
            DBTransactions.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlWithdrawal.SelectedValue), "Withdrawal", txtWithdrawalDate.Text, CDec(txtWithdrawalAmount.Text), strWithdrawalMessage, decBalance, Nothing, "", decAvailableBalance, "Withdrawal")
        Else
            DBPending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlWithdrawal.SelectedValue), "Withdrawal", txtWithdrawalDate.Text, CDec(txtWithdrawalAmount.Text), strWithdrawalMessage, Nothing, "")
            DBAccounts.UpdateAvailableBalance(CInt(ddlWithdrawal.SelectedValue), decAvailableBalance)
        End If

        lblErrorWithdrawal.Text = "Withdrawal Confirmed"
        Response.AddHeader("Refresh", "2; URL= CustomerPerformTransaction.aspx")


    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        If ValidateAmount(txtAmountTransfer.Text) = False Then
            lblErrorTransfer.Text = "Please enter a positive, numeric amount"
            Exit Sub
        End If

        'make sure selected date is greater or equal to system date
        If DBDate.CheckSelectedDate(TransferCalendar.SelectedDate) = -1 Then
            lblErrorTransfer.Text = "Please select a transfer date that has not already passed"
            Exit Sub
        End If

        'ensure the two accounts selected are not the same
        If ddlFromAccount.SelectedValue = ddlTransferTo.SelectedValue Then
            lblErrorTransfer.Text = "Please select two different accounts to transfer money between"
            Exit Sub
        End If

        'TRANSFER TO
        Dim decTransferToBalance As Decimal
        Dim decTransferToAvailableBalance As Decimal
        DBAccounts.GetBalanceByAccountNumber(ddlTransferTo.SelectedValue.ToString)
        decTransferToBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        decTransferToAvailableBalance = CDec(DBAccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        DBAccounts.GetAccountTypeByAccountNumber(ddlTransferTo.SelectedValue.ToString)
        Dim strIRATo As String
        If DBAccounts.AccountsDataset9.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            strIRATo = "True"
        Else
            strIRATo = "False"
        End If

        If DBAccounts.AccountsDataset9.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            Dim decIRATotal As Decimal
            DBAccounts.GetIRATotalDepositByAccountNumber(ddlTransferTo.SelectedValue.ToString)
            decIRATotal = CDec(DBAccounts.AccountsDataset8.Tables("tblAccounts").Rows(0).Item("IRATotalDeposit"))
            Dim decMaxIRADeposit As Decimal
            decMaxIRADeposit = 5000 - decIRATotal
            If CDec(txtAmountTransfer.Text) > decMaxIRADeposit Then
                lblErrorTransfer.Text = "You cannot contribute more than $5000 per year to your IRA. Would you like to transfer in the maximum amount, " + decMaxIRADeposit.ToString + " instead?"
                txtAmountTransfer.Text = decMaxIRADeposit.ToString
                Exit Sub
            End If
            decIRATotal += CDec(txtAmountTransfer.Text)
            DBAccounts.UpdateIRATotalDeposit(CInt(ddlTransferTo.SelectedValue), decIRATotal)
        End If


        'TRANSFER FROM

        Dim decTransferFromBalance As Decimal
        Dim decTransferFromAvailableBalance As Decimal
        DBAccounts.GetBalanceByAccountNumber2(ddlFromAccount.SelectedValue.ToString)
        decTransferFromBalance = CDec(DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("Balance"))
        decTransferFromAvailableBalance = CDec(DBAccounts.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        'make sure that you are not withdrawing more than you have in the current account ***
        '****check to make sure you can't overdraw with overdraft fees
        If (CInt(txtAmountTransfer.Text) - decTransferFromAvailableBalance) >= OVERDRAFT_MAXIMUM Then
            lblErrorTransfer.Text = "Please enter an amount to transfer less than or equal to the available amount of money in the account you are transferring money from."
            Exit Sub
        End If

        If (CInt(txtAmountTransfer.Text) - decTransferFromAvailableBalance) > 0 Then
            DBDate.GetDate()
            Dim datDate As Date
            datDate = DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString()

            decTransferFromBalance = decTransferFromBalance - OVERDRAFT_FEE
            DBAccounts.UpdateBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromBalance)
            decTransferFromAvailableBalance = decTransferFromAvailableBalance - OVERDRAFT_FEE
            DBAccounts.UpdateAvailableBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromAvailableBalance)

            Dim strFeeMessage As String
            strFeeMessage = "Overdraft fee of " & OVERDRAFT_FEE.ToString & " charged to account " & ddlFromAccount.SelectedValue.ToString & " on " & datDate.ToString
            GetTransactionNumber()
            'update the transactions table
            DBTransactions.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlFromAccount.SelectedValue), "Fee", datDate, OVERDRAFT_FEE, strFeeMessage, decTransferFromBalance, Nothing, "", decTransferFromAvailableBalance, "Fee")
        End If

        If decTransferFromAvailableBalance < 0 Then
            lblErrorTransfer.Text = "You cannot complete transfers from an account with a negative balance, even if it is within the $50 overdraft limit"
            Exit Sub
        End If

        DBAccounts.GetAccountTypeByAccountNumber2(ddlFromAccount.SelectedValue.ToString)
        Dim strIRAFrom As String
        If DBAccounts.AccountsDataset3.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            strIRAFrom = "True"
        Else
            strIRAFrom = "False"
        End If

        DB.GetDOBByCustmomerNumber(Session("CustomerNumber"))
        DBDate.GetYear()
        Dim intMaxIRAWithdrawal As Integer
        intMaxIRAWithdrawal = 3000
        Dim intServiceFee As Integer
        intServiceFee = 30
        Dim decWithdrawalAmount As Decimal
        decWithdrawalAmount = CDec(txtAmountTransfer.Text)

        DBAccounts.GetAccountTypeByAccountNumber2(ddlFromAccount.SelectedValue.ToString)
        If DBAccounts.AccountsDataset3.Tables("tblAccounts").Rows(0).Item("AccountType") = "IRA" Then
            If CInt(DBDate.DateDataset2.Tables("tblSystemDate").Rows(0).Item("Date")) - CInt(DB.CustDataset2.Tables("tblCustomers").Rows(0).Item("DOB")) < 65 Then
                If CDec(txtAmountTransfer.Text) > intMaxIRAWithdrawal Then
                    lblErrorTransfer.Text = "You cannot withdraw more than $3000 from your IRA if you are younger than 65. Would you like to transfer in the maximum amount, $3000 instead?"
                    txtAmountTransfer.Text = intMaxIRAWithdrawal.ToString
                    Exit Sub
                End If
                'you will have to pay a service fee if you are making an unqualified distribution
                'this service fee can either be included in the withdrawal or in addition to the withdrawal
                'make sure that this service fee, if in addition, does not go over balance
                'record the transaction for the withdrawal, include the transaction for the fee
                Session("IRATransactionType") = "Transfer"
                If Session("UnqualifiedDistributionFee") Is Nothing Then
                    SetFormNormal()
                    IRAFeeChoicePanel.Visible = True
                    Exit Sub
                End If
                If Session("UnqualifiedDistributionFee") = "Include Fee" Then
                    If CDec(txtAmountTransfer.Text) <= 30 Then
                        lblErrorTransfer.Text = "Please enter an amount greater than $30 to transfer out of your account, since the $30 service charge will be deducted directly from this amount"
                        Exit Sub
                    End If
                    decWithdrawalAmount = decWithdrawalAmount - intServiceFee
                End If
            End If
        End If


        'EXECUTE: UPDATE ALL BALANCES
        decTransferFromBalance = decTransferFromBalance - decWithdrawalAmount
        decTransferFromAvailableBalance = decTransferFromAvailableBalance - decWithdrawalAmount
        Dim decIRAFeeBalance As Decimal
        decIRAFeeBalance = decTransferFromBalance - 30
        decTransferToBalance += CDec(txtAmountTransfer.Text)
        decTransferToAvailableBalance += CDec(txtAmountTransfer.Text)
        Dim strTransferMessage As String
        strTransferMessage = "Transfer from account " & ddlFromAccount.SelectedValue.ToString & " to account " & ddlTransferTo.SelectedValue.ToString
        GetTransactionNumber()

        If DBDate.CheckSelectedDate(TransferCalendar.SelectedDate) = 0 Then
            DBAccounts.UpdateBalance(CInt(ddlTransferTo.SelectedValue), decTransferToBalance)
            DBAccounts.UpdateBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromBalance)
            DBAccounts.UpdateAvailableBalance(CInt(ddlTransferTo.SelectedValue), decTransferToAvailableBalance)
            DBAccounts.UpdateAvailableBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromAvailableBalance)

            'update the transactions table
            DBTransactions.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlFromAccount.SelectedValue), "Transfer", txtTransferDate.Text, decWithdrawalAmount, strTransferMessage, decTransferFromBalance, Nothing, strIRAFrom, decTransferFromAvailableBalance, "Transfer From")
            'update the transactions table
            DBTransactions.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlTransferTo.SelectedValue), "Transfer", txtTransferDate.Text, decWithdrawalAmount, strTransferMessage, decTransferToBalance, Nothing, strIRATo, decTransferToAvailableBalance, "Transfer To")
            'update the transactions table with fees if making an unqualified distribution from an IRA account


            If Session("UnqualifiedDistributionFee") = "Add Fee" Or Session("UnqualifiedDistributionFee") = "Include Fee" Then
                DBTransactions.AddTransaction(CInt(Session("TransactionNumber")) + 1, CInt(ddlFromAccount.SelectedValue), "Fee", txtTransferDate.Text, 30, "$30 service fee for an unqualified distribution from an IRA account", decIRAFeeBalance, Nothing, "", Nothing, "Fee")
                DBAccounts.UpdateBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromBalance - 30)
                DBAccounts.UpdateAvailableBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromAvailableBalance - 30)
                Session("UnqualifiedDistributionFee") = Nothing
            End If
        Else
            DBPending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlFromAccount.SelectedValue), "Transfer From", txtTransferDate.Text, decWithdrawalAmount, strTransferMessage, Nothing, strIRAFrom)
            DBPending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlTransferTo.SelectedValue), "Transfer To", txtTransferDate.Text, decWithdrawalAmount, strTransferMessage, Nothing, strIRATo)
            DBAccounts.UpdateAvailableBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromAvailableBalance)
            If Session("UnqualifiedDistributionFee") = "Add Fee" Or Session("UnqualifiedDistributionFee") = "Include Fee" Then
                DBPending.AddTransaction(CInt(Session("TransactionNumber")) + 1, CInt(ddlFromAccount.SelectedValue), "Fee", txtTransferDate.Text, 30, "$30 service fee for an unqualified distribution from an IRA account", Nothing, "")
                DBAccounts.UpdateAvailableBalance(CInt(ddlFromAccount.SelectedValue), decTransferFromAvailableBalance - 30)
                Session("UnqualifiedDistributionFee") = Nothing
            End If
        End If

        lblErrorTransfer.Text = "Transfer Confirmed"
        Response.AddHeader("Refresh", "2; URL= CustomerPerformTransaction.aspx")
    End Sub

    Protected Sub btnAddFee_Click(sender As Object, e As EventArgs) Handles btnAddFee.Click
        Session("UnqualifiedDistributionFee") = "Add Fee"
        SetFormNormal()
        If Session("IRATransactionType") = "Transfer" Then
            lblErrorTransfer.Text = "Your preferences for this transfer have been noted. Please confirm your transaction to execute the transaction"
            TransferPanel.Visible = True
        ElseIf Session("IRATransactionType") = "Withdrawal" Then
            WithdrawalPanel.Visible = True
            lblErrorWithdrawal.Text = "Your preferences for this withdrawal have been noted. Please confirm your transaction to execute the transaction"
        End If
    End Sub

    Protected Sub btnIncludeFee_Click(sender As Object, e As EventArgs) Handles btnIncludeFee.Click
        Session("UnqualifiedDistributionFee") = "Include Fee"
        SetFormNormal()
        If Session("IRATransactionType") = "Transfer" Then
            lblErrorTransfer.Text = "Your preferences for this transfer have been noted. Please confirm your transaction to execute the transaction"
            TransferPanel.Visible = True
        ElseIf Session("IRATransactionType") = "Withdrawal" Then
            WithdrawalPanel.Visible = True
            lblErrorWithdrawal.Text = "Your preferences for this withdrawal have been noted. Please confirm your transaction to execute the transaction"
        End If
    End Sub
End Class

