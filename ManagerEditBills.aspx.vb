Option Strict On

Public Class ManagerEditBills
    Inherits System.Web.UI.Page

    Dim dbbill As New ClassDBBill
    Dim dbdate As New ClassDBDate
    Dim valid As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeFirstName") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        If Session("BillID") Is Nothing Then
            Response.Redirect("ManagerViewBills.aspx")
        End If

        If IsPostBack = False Then
            dbbill.GetBillDetails(Session("BillID").ToString)

            If dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Status").ToString = "Paid" Then
                lblNotification.Text = "This eBill has been paid in full and cannot be modified."
                Response.AddHeader("Refresh", "2; url= ManagerViewBills.aspx")
            End If

            If dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Active").ToString = "FALSE" Then
                lblNotification.Text = "This eBill has been deactivated and cannot be modified. Please reactivate the bill on the View Bills page to edit."
                Response.AddHeader("Refresh", "2; url= ManagerViewBills.aspx")
            End If

            FillTextboxes()
        End If

        lblMessage.Text = ""

    End Sub

    Private Sub FillTextboxes()
        txtCustomerName.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("CustomerName").ToString
        Dim decBillAmount = CDec(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("BillAmount"))
        txtBillAmount.Text = decBillAmount.ToString("n2")
        txtBillDate.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("BillDate").ToString
        txtDueDate.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("DueDate").ToString
        txtPayee.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("PayeeName").ToString
        txtStatus.Text = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Status").ToString
        calBillDate.SelectedDate = CDate(txtBillDate.Text)
        calDueDate.SelectedDate = CDate(txtDueDate.Text)
        ddlActive.SelectedValue = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("Active").ToString()
    End Sub

    Protected Sub calBillDate_SelectionChanged(sender As Object, e As EventArgs) Handles calBillDate.SelectionChanged
        txtBillDate.Text = calBillDate.SelectedDate.ToString
    End Sub


    Protected Sub calDueDate_SelectionChanged(sender As Object, e As EventArgs) Handles calDueDate.SelectionChanged
        txtDueDate.Text = calDueDate.SelectedDate.ToString
    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        dbbill.GetBillDetails(Session("BillID").ToString)
        FillTextboxes()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'validate bill amount
        If Page.IsValid = False Then
            Exit Sub
        End If

        If valid.CheckDecimal(txtBillAmount.Text) = -1 Or valid.CheckDecimal(txtBillAmount.Text) = 0 Then
            lblMessage.Text = "Please enter a valid bill amount."
            Exit Sub
        End If

        'validate dates
        If dbdate.CheckSelectedDate(calDueDate.SelectedDate) = -1 Then
            lblMessage.Text = "Please enter a valid due date. Dates prior to today are not allowed."
            Exit Sub
        End If

        If calBillDate.SelectedDate > calDueDate.SelectedDate Then
            lblMessage.Text = "Due date must be later than bill date."
            Exit Sub
        End If

        'update bill table
        dbbill.GetBillDetails(Session("BillID").ToString)
        Dim strAmountPaid As String = dbbill.BillDataset.Tables("tblBill").Rows(0).Item("AmountPaid").ToString
        Dim decAmountRemaining As Decimal = CDec(txtBillAmount.Text) - CDec(strAmountPaid)
        dbbill.ModifyBill(txtBillAmount.Text, calBillDate.SelectedDate.ToString, calDueDate.SelectedDate.ToString, strAmountPaid, decAmountRemaining.ToString, txtStatus.Text, ddlActive.SelectedValue, Session("BillID").ToString)

        lblMessage.Text = "eBill successfully modified."
        dbbill.GetBillDetails(Session("BillID").ToString)
        FillTextboxes()

    End Sub
End Class