Public Class ManagerSetDate
    Inherits System.Web.UI.Page

    Dim db As New ClassDBDate
    Dim dbpending As New ClassDBPending
    Dim dbtransaction As New ClassDBTransactions
    Dim dbaccounts As New ClassDBAccounts
    Dim dbbill As New ClassDBBill
    Dim mstrDate As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub calSetDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSetDate.SelectionChanged
        
        mstrDate = calSetDate.SelectedDate.Year & "-" & calSetDate.SelectedDate.Month & "-" & calSetDate.SelectedDate.Day

        txtDate.Text = mstrDate
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        db.SetDate(txtDate.Text)
        db.GetDate()

        Dim intTransactionNumber As Integer
        Dim intAccountNumber As Integer
        Dim strTransactionType As String
        Dim strDate As String
        Dim decTransactionAmount As Decimal
        Dim strDescription As String
        Dim decAccountBalance As Decimal
        Dim strIRA As String
        Dim decAvailableBalance As Decimal

        'PENDING TRANSACTIONS
        dbpending.GetAllPendingTransactions()
        For i = 0 To dbpending.PendingDataset2.Tables("tblPending").Rows.Count - 1
            If dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("Date") <= db.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date") Then
                'send information to transaction table and update balance
                intTransactionNumber = CInt(dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("TransactionNumber"))
                intAccountNumber = CInt(dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("AccountNumber"))
                strTransactionType = dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("TransactionType").ToString
                strDate = dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("Date").ToString
                decTransactionAmount = CDec(dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("TransactionAmount"))
                strDescription = dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("Description").ToString
                strIRA = dbpending.PendingDataset2.Tables("tblPending").Rows(i).Item("IRA").ToString

                dbaccounts.GetBalanceByAccountNumber(intAccountNumber.ToString)

                If strTransactionType = "Deposit" Then
                    decAccountBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")) + decTransactionAmount
                    decAvailableBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + decTransactionAmount
                End If

                If strTransactionType = "Withdrawal" Or strTransactionType = "Payment" Then
                    decAccountBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")) - decTransactionAmount
                    decAvailableBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))
                End If

                If strTransactionType = "Transfer To" Then
                    decAccountBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")) + decTransactionAmount
                    decAvailableBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + decTransactionAmount
                End If

                If strTransactionType = "Transfer From" Then
                    decAccountBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")) - decTransactionAmount
                    decAvailableBalance = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))
                End If

                If strIRA = "True" And (strTransactionType = "Deposit" Or strTransactionType = "Transfer To") Then
                    dbaccounts.GetIRATotalDepositByAccountNumber(intAccountNumber)
                    Dim decIRATotal As Decimal
                    decIRATotal = CDec(dbaccounts.AccountsDataset8.Tables("tblAccounts").Rows(0).Item("IRATotalDeposit"))
                    dbaccounts.UpdateIRATotalDeposit(intAccountNumber, decIRATotal + decTransactionAmount)
                End If

                If strTransactionType = "Transfer To" Then
                    strTransactionType = "Transfer"
                End If

                If strTransactionType = "Transfer From" Then
                    strTransactionType = "Transfer"
                End If

                'add transaction
                'update balance and available balance
                dbtransaction.AddTransaction(intTransactionNumber, intAccountNumber, strTransactionType, strDate, decTransactionAmount, strDescription, decAccountBalance, Nothing, strIRA, decAvailableBalance)
                dbaccounts.UpdateBalance(intAccountNumber, decAccountBalance)
                dbaccounts.UpdateAvailableBalance(intAccountNumber, decAvailableBalance)

                'delete row from tblPending
                dbpending.DeleteTransaction(intTransactionNumber)
            End If

        Next

        'MINIMUM PAYMENTS
        dbbill.GetAllMinimumPayments()
        'For each person who signed up for minimum payments
        For i = 0 To dbbill.BillDataset.Tables("tblBill").Rows.Count - 1
            db.GetDate()
            Dim datToday As Date = CDate(db.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))

            Dim intCustomerNumber As Integer = CInt(dbbill.BillDataset.Tables("tblBill").Rows(i).Item("CustomerNumber"))
            Dim decMonthlyPayment As Decimal = CDec(dbbill.BillDataset.Tables("tblBill").Rows(i).Item("MinimumAmount"))
            Dim datSignUpDate As Date = CDate(dbbill.BillDataset.Tables("tblBill").Rows(i).Item("SignUpDate"))
            Dim datLastPaymentDate As Date = CDate(dbbill.BillDataset.Tables("tblBill").Rows(i).Item("LastPaymentDate"))
            Dim intAccountNumberMin As Integer = CInt(dbbill.BillDataset.Tables("tblBill").Rows(i).Item("AccountNumber"))

            Dim intMonths As Integer

            If datToday >= datLastPaymentDate And datSignUpDate = datLastPaymentDate Then
                intMonths = DateDiff(DateInterval.Month, datLastPaymentDate, datToday) + 1
            ElseIf datToday >= datLastPaymentDate And datSignUpDate <> datLastPaymentDate And datToday.Month <> datLastPaymentDate.Month Then
                intMonths = DateDiff(DateInterval.Month, datLastPaymentDate, datToday) + 1
            Else
                intMonths = 0
            End If

            Dim decTotalPayment As Decimal = intMonths * decMonthlyPayment

            dbaccounts.GetBalanceByAccountNumber(intAccountNumberMin.ToString)
            Dim decBalanceMin As Decimal = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
            Dim decAvailableBalanceMin As Decimal = CDec(dbaccounts.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

            'get customer's sorted bills
            dbbill.SortBills(intCustomerNumber.ToString)
            For j = 0 To dbbill.BillDataset2.Tables("tblBill").Rows.Count - 1
                Dim decBillAmount As Decimal = CDec(dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("BillAmount"))
                Dim decAmountRemaining As Decimal = CDec(dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("AmountRemaining"))
                Dim strPayeeNameMin As String = dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("PayeeName").ToString
                Dim intBillID As Integer = CInt(dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("BillID"))
                Dim datBillDate As Date = CDate(dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("BillDate"))
                Dim datDueDate As Date = CDate(dbbill.BillDataset2.Tables("tblBill").Rows(j).Item("DueDate"))

                If decAmountRemaining <= decTotalPayment Then 'pay bill in full
                    'add transaction
                    decBalanceMin = decBalanceMin - decAmountRemaining
                    decAvailableBalanceMin = decAvailableBalanceMin - decAmountRemaining
                    dbaccounts.UpdateBalance(intAccountNumberMin, decBalanceMin)
                    dbaccounts.UpdateAvailableBalance(intAccountNumberMin, decAvailableBalanceMin)
                    GetTransactionNumber()
                    Dim strDescriptionMin As String = "Sent eBill payment of " & decAmountRemaining.ToString & " to " & strPayeeNameMin & " from account " & intAccountNumberMin.ToString & " on " & datToday.ToString
                    dbtransaction.AddTransaction(Session("TransactionNumber"), intAccountNumberMin, "eBill Payment", datToday.ToString, decAmountRemaining, strDescriptionMin, decBalanceMin, intBillID, "", decAvailableBalanceMin)
                    'update bills table
                    dbbill.ModifyBill(decBillAmount.ToString, datBillDate.ToString, datDueDate.ToString, decAmountRemaining, "0", "Paid", "TRUE", intBillID.ToString)
                    'decrease total payment amount
                    decTotalPayment = decTotalPayment - decAmountRemaining
                End If

                If decAmountRemaining > decTotalPayment And decTotalPayment <> 0 Then 'pay rest of decTotalPayment
                    'add transaction
                    decBalanceMin = decBalanceMin - decTotalPayment
                    decAvailableBalanceMin = decAvailableBalanceMin - decTotalPayment
                    dbaccounts.UpdateBalance(intAccountNumberMin, decBalanceMin)
                    dbaccounts.UpdateAvailableBalance(intAccountNumberMin, decAvailableBalanceMin)
                    GetTransactionNumber()
                    Dim strDescriptionMin As String = "Sent eBill payment of " & decTotalPayment.ToString & " to " & strPayeeNameMin & " from account " & intAccountNumberMin.ToString & " on " & datToday.ToString
                    dbtransaction.AddTransaction(Session("TransactionNumber"), intAccountNumberMin, "eBill Payment", datToday.ToString, decTotalPayment, strDescriptionMin, decBalanceMin, intBillID, "", decAvailableBalanceMin)
                    'update bills table
                    decAmountRemaining = decAmountRemaining - decTotalPayment
                    dbbill.ModifyBill(decBillAmount.ToString, datBillDate.ToString, datDueDate.ToString, decTotalPayment.ToString, decAmountRemaining, "Partially Paid", "TRUE", intBillID.ToString)
                    'end loop
                    Exit For
                End If
            Next

            dbbill.UpdateMinimumPaymentDate(datToday, intCustomerNumber.ToString)

        Next


        'BUY/SELL STOCKS

        'lblError.Text = "Date successfully changed"
        Response.AddHeader("Refresh", "2; URL= ManagerHome.aspx")
    End Sub

    Public Sub GetTransactionNumber()
        dbtransaction.GetMaxTransactionNumber()
        If dbtransaction.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber") Is DBNull.Value Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(dbtransaction.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub
End Class