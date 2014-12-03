Public Class CustomerBillDetail
    Inherits System.Web.UI.Page

    Dim dbbill As New ClassDBBill
    Dim dbaccount As New ClassDBAccounts
    Dim dbDate As New ClassDBDate
    Dim dbtrans As New ClassDBTransactions
    Dim dbpending As New ClassDBPending
    Dim format As New ClassFormat
    Dim valid As New ClassValidate

    Dim mdecBalance As Decimal
    Dim mdecBillAmount As Decimal
    Dim mdecPayment As Decimal
    Dim mdecAmountRemaining As Decimal
    Dim mdecAvailableBalance As Decimal

    Const OVERDRAFT_MAXIMUM As Decimal = 50D
    Const OVERDRAFT_FEE As Decimal = 30D
    Const LATE_FEE As Decimal = 5D

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        If Request.QueryString("ID") Is Nothing Then
            Response.Redirect("CustomerPayBills.aspx")
        End If

        lblBillID.Text = Request.QueryString("ID")
        dbbill.GetBillDetails(Request.QueryString("ID"))

        If IsPostBack = False Then
            FillTextboxes()

            dbaccount.GetCheckingandSavingsByCustomerNumber(Session("CustomerNumber"))
            ddlAccount.DataSource = dbaccount.AccountsDataset.Tables("tblAccounts")
            ddlAccount.DataTextField = "Details"
            ddlAccount.DataValueField = "AccountNumber"
            ddlAccount.DataBind()

        End If
        
        btnConfirm.Visible = False
        btnAbort.Visible = False
        lblMessage.Text = ""
        lblMessageFee.Text = ""
        lblMessageFee2.Text = ""
    End Sub

    Private Sub FillTextboxes()
        Dim strPhone As String
        Dim decAmountPaid As Decimal
        Dim decAmountRemaining As Decimal
        txtCustomerName.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("CustomerName").ToString
        mdecBillAmount = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("BillAmount"))
        txtBillAmount.Text = mdecBillAmount.ToString("c2")
        decAmountPaid = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("AmountPaid"))
        txtAmountPaid.Text = decAmountPaid.ToString("c2")
        decAmountRemaining = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("AmountRemaining"))
        txtAmountRemaining.Text = decAmountRemaining.ToString("c2")
        txtBillDate.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("BillDate").ToString
        txtDueDate.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("DueDate").ToString
        txtStatus.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Status").ToString
        txtPayeeName.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("PayeeName").ToString
        txtType.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("PayeeType").ToString
        txtPayeeAddress.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Address").ToString
        txtZip.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("ZipCode").ToString

        strPhone = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Phone").ToString
        txtPhone.Text = format.FormatPhone(strPhone)
    End Sub

    Protected Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        'validations
        'validate selected account balance >= 0
        dbaccount.GetBalanceByAccountNumber(ddlAccount.SelectedValue.ToString)
        mdecBalance = CDec(dbaccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        mdecAvailableBalance = CDec(dbaccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        If mdecAvailableBalance < 0 Then
            lblMessage.Text = "Please select a checking or savings account with a positive available balance."
            Exit Sub
        End If

        'validate textbox
        If Page.IsValid = False Then
            Exit Sub
        End If

        If valid.CheckDecimal(txtAmount.Text) = -1 Or CDec(txtAmount.Text) = 0 Then
            lblMessage.Text = "Please enter a valid payment amount."
            Exit Sub
        End If

        mdecPayment = CDec(txtAmount.Text)
        mdecAmountRemaining = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("AmountRemaining"))

        If txtType.Text = "Utilities" Or txtType.Text = "Other" Then
            If mdecPayment > mdecAmountRemaining Then
                lblMessage.Text = "The payment amount you have entered exceeds the bill's outstanding balance."
                Exit Sub
            End If
        End If

        'validate date field
        If calDate.SelectedDate = Nothing Then
            lblMessage.Text = "Please enter a date for your payment."
            Exit Sub
        End If
        If dbDate.CheckSelectedDate(calDate.SelectedDate) = -1 Then
            lblMessage.Text = "Please do not enter a date prior to today's date."
            Exit Sub
        End If

        'validate that total amount is less than max withdrawal (overdraft stuff)
        If mdecBalance + OVERDRAFT_MAXIMUM < mdecPayment Then
            lblMessage.Text = "Your payment total is " & mdecPayment.ToString("c2") & ", which exceeds your account balance by more than the maximum overdraft amount (" & OVERDRAFT_MAXIMUM.ToString("c2") & ")."
            Exit Sub
        End If

        'whee confirmation stuff
        lblMessage.Text = "By clicking 'Confirm' below, you agree to send a total of " & mdecPayment.ToString("c2") & " to " & dbbill.BillDataset.Tables("tblBill").Rows(0).Item("PayeeName").ToString & "."
        btnConfirm.Visible = True
        btnAbort.Visible = True

        'overdraft fee notice
        If mdecBalance < mdecPayment And dbDate.CheckSelectedDate(calDate.SelectedDate) = 0 Then
            lblMessageFee.Text = "Note: The payment amount entered exceeds your selected account's balance. You will be charged an overdraft fee of $30.00 in addition to your specified payment."
        End If

        'late fee notice
        dbDate.GetDate()
        If CDate(txtDueDate.Text) < calDate.SelectedDate.Date And txtStatus.Text = "Unpaid" Then
            lblMessageFee2.Text = "Note: You have selected a date past this bill's due date. You will be charged a late fee of $5.00 in addition to your specified payment."
        End If

    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        txtAmount.Text = ""
        calDate.SelectedDate = Nothing
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        dbaccount.GetBalanceByAccountNumber(ddlAccount.SelectedValue.ToString)
        mdecBalance = CDec(dbaccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        mdecAvailableBalance = CDec(dbaccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        'pay bill or schedule payment
        If dbDate.CheckSelectedDate(calDate.SelectedDate) = 0 Then
            mdecBalance = mdecBalance - CDec(txtAmount.Text)
            mdecAvailableBalance = mdecAvailableBalance - CDec(txtAmount.Text)
            dbaccount.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
            dbaccount.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

            Dim strPaymentMessage As String
            strPaymentMessage = "Sent eBill payment of " & txtAmount.Text & " to " & txtPayeeName.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & calDate.SelectedDate.ToString
            GetTransactionNumber()
            'update the transactions table
            dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "eBill Payment", calDate.SelectedDate, CDec(txtAmount.Text), strPaymentMessage, mdecBalance, lblBillID.Text, "False", mdecAvailableBalance)
            'update the bills table
            Dim decAmountPaid
            decAmountPaid = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("AmountPaid")) + CDec(txtAmount.Text)

            Dim decAmountRemaining As Decimal
            If decAmountPaid > CDec(txtBillAmount.Text) Then
                decAmountRemaining = 0
            Else
                decAmountRemaining = CDec(txtBillAmount.Text) - decAmountPaid
            End If

            Dim strStatus As String
            If decAmountRemaining = 0 Then
                strStatus = "Paid"
            Else
                strStatus = "Partially Paid"
            End If
            dbbill.ModifyBill(CDec(txtBillAmount.Text).ToString("n2"), txtBillDate.Text, txtDueDate.Text, decAmountPaid.ToString, decAmountRemaining.ToString, strStatus, "TRUE", lblBillID.Text)
        End If

        If dbDate.CheckSelectedDate(calDate.SelectedDate) = 1 Then
            'schedule payments
            mdecAvailableBalance = mdecAvailableBalance - CDec(txtAmount.Text)
            dbaccount.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)
            Dim strPendingMessage As String
            strPendingMessage = "Sent eBill payment of " & txtAmount.Text & " to " & txtPayeeName.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & calDate.SelectedDate.ToString
            GetTransactionNumber()
            'update pending transactions table
            dbpending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "eBill Payment", calDate.SelectedDate, CDec(txtAmount.Text), strPendingMessage, lblBillID.Text, "False", mdecAvailableBalance)
        End If

        'overdraft fee if necessary
        If mdecBalance < 0 Then
            'only happens if the payment went through today
            dbDate.GetDate()
            Dim datDate As Date
            datDate = dbDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString()

            mdecBalance = mdecBalance - OVERDRAFT_FEE
            mdecAvailableBalance = mdecAvailableBalance - OVERDRAFT_FEE
            dbaccount.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
            dbaccount.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

            Dim strFeeMessage As String
            strFeeMessage = "Overdraft fee of " & OVERDRAFT_FEE.ToString & " charged to account " & ddlAccount.SelectedValue.ToString & " on " & datDate.ToString
            GetTransactionNumber()

            'update the transactions table
            dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Fee", datDate, OVERDRAFT_FEE, strFeeMessage, mdecBalance, "NULL", "False", mdecAvailableBalance)
        End If

        'late fee if necessary
        dbDate.GetDate()
        If CDate(txtDueDate.Text) < calDate.SelectedDate.Date And dbDate.CheckSelectedDate(calDate.SelectedDate) = 0 And txtStatus.Text = "Unpaid" Then
            Dim datDate As Date
            datDate = dbDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString()

            mdecBalance = mdecBalance - LATE_FEE
            mdecAvailableBalance = mdecAvailableBalance - LATE_FEE
            dbaccount.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
            dbaccount.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

            Dim strFeeMessage As String
            strFeeMessage = "Late fee of " & LATE_FEE.ToString & " charged to account " & ddlAccount.SelectedValue.ToString & " on " & datDate.ToString
            GetTransactionNumber()

            'update the transactions table
            dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Fee", datDate, LATE_FEE, strFeeMessage, mdecBalance, "NULL", "False", mdecAvailableBalance)
        End If

        lblMessage.Text = "Payments successfully sent and/or scheduled."
        btnConfirm.Visible = False
        btnAbort.Visible = False

        dbbill.GetBillDetails(Request.QueryString("ID"))
        FillTextboxes()
        txtAmount.Text = ""
        calDate.SelectedDate = Nothing

    End Sub

    Private Sub GetTransactionNumber()
        dbtrans.GetMaxTransactionNumber()
        If dbtrans.TransactionsDataset.Tables("tblTransactions").Rows.Count = 0 Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(dbtrans.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub

End Class