Option Strict On

Public Class ManagerAddBill
    Inherits System.Web.UI.Page

    Dim dbpayee As New ClassDBPayee
    Dim dbcustomer As New ClassDBCustomer

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

            btnConfirm.Visible = False

            dbpayee.GetPayeeCustomers(ddlPayee.SelectedValue.ToString)

            
        End If

        FillCustomers()

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
End Class