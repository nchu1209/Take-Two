Public Class CustomerPayBills
    Inherits System.Web.UI.Page

    Dim dbpay As New ClassDBPayee
    Dim valid As New ClassValidate
    Dim dbact As New ClassDBAccounts
    Dim dbdate As New ClassDBDate
    Dim dbtrans As New ClassDBTransactions
    Dim dbpending As New ClassDBPending
    Dim dbbill As New ClassDBBill

    Dim mdecTotalToday As Decimal
    Dim mdecTotalPending As Decimal
    Dim mdecTotalWithdrawal As Decimal
    Dim mdecBalance As Decimal
    Dim mdecAvailableBalance As Decimal

    Const OVERDRAFT_MAXIMUM As Decimal = 50D
    Const OVERDRAFT_FEE As Decimal = 30D
    Const LATE_FEE As Decimal = 5D

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        If IsPostBack = False Then
            dbpay.GetCustomerPayees(Session("CustomerNumber"))

            dbact.GetCheckingandSavingsByCustomerNumber(Session("CustomerNumber"))
            ddlAccount.DataSource = dbact.AccountsDataset.Tables("tblAccounts")
            ddlAccount.DataTextField = "Details"
            ddlAccount.DataValueField = "AccountNumber"
            ddlAccount.DataBind()

            btnConfirm.Visible = False
            btnAbort.Visible = False

            dbdate.GetDate()
            Dim datToday As Date
            datToday = dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date")
            dbbill.GetCustomerBills(Session("CustomerNumber"), datToday)
            If dbbill.BillDataset.Tables("tblBill").Rows.Count <> 0 Then
                For i = 0 To gvMyPayees.Rows.Count - 1
                    For k = 0 To dbbill.BillDataset.Tables("tblBill").Rows.Count - 1
                        Dim x As Label = DirectCast(gvMyPayees.Rows(i).Cells(1).FindControl("lblPayeeID"), Label)
                        If CInt(x.Text) = dbbill.BillDataset.Tables("tblBill").Rows(k).Item("PayeeID") Then
                            Dim b As ImageButton = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("btnBill"), ImageButton)
                            Dim a As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblBillAmount"), Label)
                            Dim d As Label = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("lblDueDate"), Label)
                            Dim f As Label = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("lblOutstandingBalance"), Label)
                            'set notification
                            b.ImageUrl = "~/eBill.jpg"
                            b.Enabled = True
                            b.CommandName = "GoToBill"
                            'highlight payee
                            gvMyPayees.Rows(i).BackColor = Drawing.Color.LightGray
                            'populate bill due date, amount, and outstanding balance
                            Dim decBill As Decimal
                            decBill = CDec(dbbill.BillDataset.Tables("tblBill").Rows(k).Item("BillAmount"))
                            a.Text = decBill.ToString("c2")
                            Dim datBill As Date
                            datBill = CDate(dbbill.BillDataset.Tables("tblBill").Rows(k).Item("DueDate")).Date
                            d.Text = datBill.ToString
                            Dim decOutstandingBalance As Decimal
                            decOutstandingBalance = CDec(dbbill.BillDataset.Tables("tblBill").Rows(k).Item("AmountRemaining"))
                            f.Text = decOutstandingBalance.ToString("c2")
                        End If
                    Next
                Next
            End If

            mdecTotalToday = 0
            mdecTotalPending = 0

            dbbill.GetMinimumPayment(Session("CustomerNumber"))
            If dbbill.BillDataset3.Tables("tblBill").Rows.Count <> 0 Then
                pnlSetup.Visible = False
                pnlViewPayment.Visible = True
                Dim decMinimumAmount As Decimal = dbbill.BillDataset3.Tables("tblBill").Rows(0).Item("MinimumAmount")
                lblMinimumPayment.Text = decMinimumAmount.ToString("c2")
            Else
                pnlSetup.Visible = True
                pnlViewPayment.Visible = False
            End If

        End If

        lblMessageTotal.Text = ""
        lblMessageFee.Text = ""
        lblMessageSuccess.Text = ""
        lblmessagefee2.text = ""

    End Sub

    Protected Sub txtPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click

        'validate selected account balance >= 0
        dbact.GetBalanceByAccountNumber(ddlAccount.SelectedValue.ToString)
        mdecBalance = CDec(dbact.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        mdecAvailableBalance = CDec(dbact.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        If mdecAvailableBalance < 0 Then
            lblMessageTotal.Text = "Please select a checking or savings account with a positive available balance."
            Exit Sub
        End If

        'validate textbox fields
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            If t.Text <> "" Then
                If valid.CheckDecimal(t.Text) = -1 Then
                    lblMessageTotal.Text = "Please enter valid payment amounts."
                    Exit Sub
                End If

                Dim decPayment As Decimal = CDec(t.Text)
                Dim r As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblOutstandingBalance"), Label)
                Dim decAmountRemaining As Decimal
                If r.Text <> "" Then
                    decAmountRemaining = CDec(r.Text)
                End If

                If gvMyPayees.Rows(i).Cells(2).Text = "Utilities" Or gvMyPayees.Rows(i).Cells(2).Text = "Other" Then
                    If decPayment > decAmountRemaining Then
                        lblMessageTotal.Text = "For Utilities and Other eBills, payment amount may not exceed the outstanding balance."
                        Exit Sub
                    End If
                End If
            End If

        Next

        'validate date fields
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            Dim c As Calendar = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("calDate"), Calendar)
            If t.Text <> "" Then
                If c.SelectedDate = Nothing Then
                    lblMessageTotal.Text = "Please enter a date for each payment."
                    Exit Sub
                End If
                If dbdate.CheckSelectedDate(c.SelectedDate) = -1 Then
                    lblMessageTotal.Text = "Please do not enter a date prior to today's date."
                    Exit Sub
                End If
            End If
        Next

        'find the total withdrawal amounts wooooo
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            Dim c As Calendar = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("calDate"), Calendar)
            'total today
            If dbdate.CheckSelectedDate(c.SelectedDate) = 0 And t.Text <> "" Then
                mdecTotalToday += CDec(t.Text)
            End If
            'total pending
            If dbdate.CheckSelectedDate(c.SelectedDate) = 1 And t.Text <> "" Then
                mdecTotalPending += CDec(t.Text)
            End If
        Next

        mdecTotalWithdrawal = mdecTotalToday + mdecTotalPending

        'validate that total amount TODAY is less than balance (overdraft stuff)
        If mdecBalance + OVERDRAFT_MAXIMUM < mdecTotalToday Then
            lblMessageTotal.Text = "Your payment total for today (excluding scheduled future payments) is " & mdecTotalToday.ToString("c2") & ", which exceeds your account balance by more than the maximum overdraft amount (" & OVERDRAFT_MAXIMUM.ToString("c2") & ")."
            Exit Sub
        End If

        'whee confirmation stuff
        lblMessageTotal.Text = "By clicking 'Confirm' below, you agree to send/schedule payment(s) totaling " & mdecTotalWithdrawal.ToString("c2") & " to the specified payee(s)."
        btnConfirm.Visible = True
        btnAbort.Visible = True

        'overdraft fee notice
        If mdecBalance < mdecTotalToday Then
            lblMessageFee.Text = "Note: Your payment total for today (excluding scheduled future payments) is " & mdecTotalToday.ToString("c2") & ", which exceeds your selected account's balance. You will be charged an overdraft fee of $30.00 in addition to your specified payment(s)."
        End If

        'late fee notice
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            Dim c As Calendar = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("calDate"), Calendar)
            Dim d As Label = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("lblDueDate"), Label)
            Dim r As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblOutstandingBalance"), Label)
            Dim k As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblBillAmount"), Label)
            If t.Text <> "" And d.Text <> "" Then
                If c.SelectedDate > CDate(d.Text) And r.Text = k.Text Then
                    lblMessageFee2.Text = "Note: You will be charged a late fee of $5.00 for every bill paid after its due date."
                    Exit Sub
                End If
            End If
        Next

    End Sub

    Private Sub GetTransactionNumber()
        dbtrans.GetMaxTransactionNumber()
        If dbtrans.TransactionsDataset.Tables("tblTransactions").Rows.Count = 0 Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(dbtrans.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        dbact.GetBalanceByAccountNumber(ddlAccount.SelectedValue.ToString)
        mdecBalance = CDec(dbact.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))
        mdecAvailableBalance = CDec(dbact.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance"))

        'pay bills - now and pending
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            Dim c As Calendar = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("calDate"), Calendar)
            Dim n As HyperLink = DirectCast(gvMyPayees.Rows(i).Cells(2).FindControl("lnkName"), HyperLink)
            Dim k As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblBillAmount"), Label)
            Dim j As Label = DirectCast(gvMyPayees.Rows(i).Cells(1).FindControl("lblPayeeID"), Label)
            Dim d As Label = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("lblDueDate"), Label)

            'PAYMENTS TODAY
            If dbdate.CheckSelectedDate(c.SelectedDate) = 0 And t.Text <> "" Then
                'non eBill
                If k.Text = "" Then
                    mdecBalance = mdecBalance - CDec(t.Text)
                    dbact.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
                    mdecAvailableBalance = mdecAvailableBalance - CDec(t.Text)
                    dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

                    Dim strPaymentMessage As String
                    strPaymentMessage = "Sent payment of " & t.Text & " to " & n.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & c.SelectedDate.ToString
                    GetTransactionNumber()
                    'update the transactions table
                    dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Payment", c.SelectedDate, CDec(t.Text), strPaymentMessage, mdecBalance, "NULL", "False", mdecAvailableBalance)
                End If

                'eBill Payments
                If k.Text <> "" Then
                    mdecBalance = mdecBalance - CDec(t.Text)
                    dbact.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
                    mdecAvailableBalance = mdecAvailableBalance - CDec(t.Text)
                    dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

                    Dim datToday As Date
                    dbdate.GetDate()
                    datToday = CDate(dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))

                    dbbill.GetBillByPayeeID(Session("CustomerNumber"), datToday, j.Text)
                    Dim intBillID As Integer
                    intBillID = CInt(dbbill.BillDataset2.Tables("tblBill").Rows(0).Item("BillID"))

                    Dim strPaymentMessage As String
                    strPaymentMessage = "Sent eBill payment of " & t.Text & " to " & n.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & c.SelectedDate.ToString
                    GetTransactionNumber()
                    'update the transactions table
                    dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "eBill Payment", c.SelectedDate, CDec(t.Text), strPaymentMessage, mdecBalance, intBillID.ToString, "False", mdecAvailableBalance)

                    'update bills table
                    
                    Dim decAmountPaid As Decimal
                    decAmountPaid = CDec(dbbill.BillDataset2.Tables("tblBill").Rows(0).Item("AmountPaid")) + CDec(t.Text)
                    Dim datBillDate As Date
                    datBillDate = CDate(dbbill.BillDataset2.Tables("tblBill").Rows(0).Item("BillDate"))

                    Dim decAmountRemaining As Decimal
                    If decAmountPaid > CDec(k.Text) Then
                        decAmountRemaining = 0
                    Else
                        decAmountRemaining = CDec(k.Text) - decAmountPaid
                    End If

                    Dim strStatus As String
                    If decAmountRemaining = 0 Then
                        strStatus = "Paid"
                    Else
                        strStatus = "Partially Paid"
                    End If
                    dbbill.ModifyBill(CDec(k.Text).ToString("n2"), datBillDate.ToString, d.Text, decAmountPaid.ToString, decAmountRemaining.ToString, strStatus, "TRUE", intBillID.ToString)
                End If

            End If

            'SCHEDULE FUTURE PAYMENTS
            If dbdate.CheckSelectedDate(c.SelectedDate) = 1 And t.Text <> "" Then
                'non eBill
                If k.Text = "" Then
                    mdecAvailableBalance = mdecAvailableBalance - CDec(t.Text)
                    dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

                    Dim strPendingMessage As String
                    strPendingMessage = "Send payment of " & t.Text & " to " & n.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & c.SelectedDate.ToString
                    GetTransactionNumber()
                    'update pending transactions table
                    dbpending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Payment", c.SelectedDate, CDec(t.Text), strPendingMessage, "NULL", "False", mdecAvailableBalance)
                End If

                'eBills
                If k.Text <> "" Then

                    mdecAvailableBalance = mdecAvailableBalance - CDec(t.Text)
                    dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

                    Dim datToday As Date
                    dbdate.GetDate()
                    datToday = CDate(dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))

                    dbbill.GetBillByPayeeID(Session("CustomerNumber"), datToday, j.Text)
                    Dim intBillID As Integer
                    intBillID = CInt(dbbill.BillDataset2.Tables("tblBill").Rows(0).Item("BillID"))

                    GetTransactionNumber()
                    Dim strPendingMessage = "Send payment of " & t.Text & " to " & n.Text & " from account " & ddlAccount.SelectedValue.ToString & " on " & c.SelectedDate.ToString
                    'update pending transactions table
                    dbpending.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "eBill Payment", c.SelectedDate, CDec(t.Text), strPendingMessage, intBillID.ToString, "False", mdecAvailableBalance)
                End If

            End If
        Next

        'overdraft fee if necessary
        If mdecBalance < 0 Then
            dbdate.GetDate()
            Dim datDate As Date
            datDate = dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString()

            mdecBalance = mdecBalance - OVERDRAFT_FEE
            dbact.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
            mdecAvailableBalance = mdecAvailableBalance - OVERDRAFT_FEE
            dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

            Dim strFeeMessage As String
            strFeeMessage = "Overdraft fee of " & OVERDRAFT_FEE.ToString & " charged to account " & ddlAccount.SelectedValue.ToString & " on " & datDate.ToString
            GetTransactionNumber()
            'update the transactions table
            dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Fee", datDate, OVERDRAFT_FEE, strFeeMessage, mdecBalance, "NULL", "False", mdecAvailableBalance)
        End If

        'late fee if necessary
        For i = 0 To gvMyPayees.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
            Dim c As Calendar = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("calDate"), Calendar)
            Dim d As Label = DirectCast(gvMyPayees.Rows(i).Cells(5).FindControl("lblDueDate"), Label)
            Dim r As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblOutstandingBalance"), Label)
            Dim k As Label = DirectCast(gvMyPayees.Rows(i).Cells(3).FindControl("lblBillAmount"), Label)
            If t.Text <> "" And d.Text <> "" And dbdate.CheckSelectedDate(c.SelectedDate) = 0 Then
                If c.SelectedDate > CDate(d.Text) And CDec(r.Text) = CDec(k.Text) Then
                    mdecBalance = mdecBalance - LATE_FEE
                    dbact.UpdateBalance(CInt(ddlAccount.SelectedValue), mdecBalance)
                    mdecAvailableBalance = mdecAvailableBalance - LATE_FEE
                    dbact.UpdateAvailableBalance(CInt(ddlAccount.SelectedValue), mdecAvailableBalance)

                    Dim strFeeMessage As String = "Late fee of " & LATE_FEE.ToString & " charged to account " & ddlAccount.SelectedValue.ToString & " on " & c.SelectedDate.ToString
                    GetTransactionNumber()
                    'update the transactions table
                    dbtrans.AddTransaction(CInt(Session("TransactionNumber")), CInt(ddlAccount.SelectedValue), "Fee", c.SelectedDate.ToString, LATE_FEE, strFeeMessage, mdecBalance, "NULL", "False", mdecAvailableBalance)
                End If
            End If
        Next

        lblMessageSuccess.Text = "Payments successfully sent and/or scheduled."
        btnConfirm.Visible = False
        btnAbort.Visible = False

        Response.AddHeader("Refresh", "2; URL= CustomerPayBills.aspx")

    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        btnConfirm.Visible = False
        btnAbort.Visible = False
    End Sub

    Protected Sub gvMyPayees_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        If (e.CommandName = "GoToBill") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim intRow As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = gvMyPayees.Rows(intRow)

            '' Add code here to add the item to the shopping cart.
            dbdate.GetDate()
            Dim datToday As Date
            datToday = dbdate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date")
            dbbill.GetCustomerBills(Session("CustomerNumber"), datToday)

            Response.Redirect("CustomerBillDetail.aspx?ID=" & dbbill.BillDataset.Tables("tblBill").Rows(intRow).Item("BillID"))

        End If
    End Sub

    Protected Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim intTest As Integer
        intTest = DateDiff(DateInterval.Month, #12/1/2014#, #3/29/2015#)

        txtCustomAmount.Text = intTest.ToString

        'plan: figure out # months between selected date and current date + 1, if < 1 don't do anything
        'take total months * minimum payment, then run the loop to distribute payment among bills
    End Sub

    Protected Sub btnSetup_Click(sender As Object, e As EventArgs) Handles btnSetup.Click
        'validations
        If radAmount.SelectedIndex = Nothing Then
            lblMinimumMessage.Text = "Please select a preset amount or 'Custom Amount.'"
            Exit Sub
        End If

        If radAmount.SelectedValue = "Custom" And txtCustomAmount.Text = "" Then
            lblMinimumMessage.Text = "Please enter a custom amount in the textbox provided."
            Exit Sub
        End If

        If txtCustomAmount.Text <> "" Then
            If valid.CheckDecimal(txtCustomAmount.Text) = -1 Or valid.CheckDecimal(txtCustomAmount.Text) = 0 Then
                lblMinimumMessage.Text = "Please enter a valid custom amount."
                Exit Sub
            End If
        End If

        If calMinimum.SelectedDate = Nothing Then
            lblMinimumMessage.Text = "Please select a date to begin your monthly payments."
            Exit Sub
        End If

        If dbdate.CheckSelectedDate(calMinimum.SelectedDate) = -1 Then
            lblMinimumMessage.Text = "Please do not select a date prior to today."
            Exit Sub
        End If

        'add to table
        Dim decAmount As Decimal
        If radAmount.SelectedValue <> "Custom" Then
            decAmount = CDec(radAmount.SelectedValue)
        Else
            decAmount = CDec(txtCustomAmount.Text)
        End If
        dbbill.SetUpMinimumPayment(Session("CustomerNumber"), decAmount, calMinimum.SelectedDate)

        lblMinimumMessage.Text = "You have successfully set up minimum payments."
    End Sub
End Class