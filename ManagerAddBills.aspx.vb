Option Strict On

Public Class ManagerAddBills
    Inherits System.Web.UI.Page

    Dim dbpayee As New ClassDBPayee
    Dim dbcustomer As New ClassDBCustomer
    Dim valid As New ClassValidate
    Dim dbdate As New ClassDBDate
    Dim dbbill As New ClassDBBill

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeFirstName") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        If IsPostBack = False Then
            dbpayee.GetAllPayees()

            ddlPayee.DataSource = dbpayee.PayeeDataset.Tables("tblPayees")
            ddlPayee.DataTextField = "PayeeName"
            ddlPayee.DataValueField = "PayeeID"
            ddlPayee.DataBind()

            dbpayee.GetPayeeCustomers(ddlPayee.SelectedValue.ToString)

            pnlAddBill.Visible = False
        End If

        FillCustomers()
        lblMessage1.Text = ""

    End Sub

    Private Sub FillCustomers()
        ddlCustomer.DataSource = dbpayee.PayeeDataset2.Tables("tblPayees")
        ddlCustomer.DataTextField = "FullName"
        ddlCustomer.DataValueField = "CustomerNumber"
        ddlCustomer.DataBind()

    End Sub

    Protected Sub ddlPayee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPayee.SelectedIndexChanged
        dbpayee.GetPayeeCustomers(ddlPayee.SelectedValue.ToString)
        FillCustomers()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If ddlCustomer.SelectedValue = "" Then
            lblMessage1.Text = "Please select a valid payee/customer combination."
            Exit Sub
        End If

        'CHECK IF CUSTOMER ALREADY HAS A BILL FOR THIS PAYEE!!

        pnlAddBill.Visible = True
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'validations
        If Page.IsValid = False Then
            Exit Sub
        End If

        If valid.CheckDecimal(txtAmount.Text) = -1 Or valid.CheckDecimal(txtAmount.Text) = 0 Then
            lblMessage2.Text = "Please enter a valid bill amount."
            Exit Sub
        End If

        If dbdate.CheckSelectedDate(calCreated.SelectedDate) = -1 Then
            lblMessage2.Text = "Please enter a valid Date Created."
            Exit Sub
        End If

        If dbdate.CheckSelectedDate(calDue.SelectedDate) = -1 Or calDue.SelectedDate < calCreated.SelectedDate Then
            lblMessage2.Text = "Please enter a valid Date Due."
            Exit Sub
        End If

        'add bill
        GetBillID()
        dbbill.AddBill(CInt(Session("BillID")), ddlCustomer.SelectedValue, CDec(txtAmount.Text), calCreated.SelectedDate, calDue.SelectedDate, ddlPayee.SelectedValue.ToString)

        lblMessage2.Text = "eBill successfully added."

        Response.AddHeader("Refresh", "2; URL=ManagerAddBill.aspx")
    End Sub

    Private Sub GetBillID()
        dbbill.GetMaxBillID()
        If IsDBNull(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("MaxBillID")) = True Then
            Session("BillID") = 1
        Else
            Session("BillID") = CInt(dbbill.BillDataset.Tables("tblBill").Rows(0).Item("MaxBillID")) + 1
        End If
    End Sub
End Class