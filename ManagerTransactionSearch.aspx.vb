Public Class ManagerTransactionSearch
    Inherits System.Web.UI.Page

    Dim DBCustomer As New ClassDBCustomer
    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeFirstName") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        DBCustomer.GetNumberFNameLNameAllCustomers()
        gvCustomers.DataSource = DBCustomer.MyView
        gvCustomers.DataBind()
        PanelAccounts.Visible = False
        If IsPostBack = False Then
            CustomerSelect.Visible = True
        End If
    End Sub

    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        PanelAccounts.Visible = True
        CustomerSelect.Visible = False
        'get the selected index
        Session("CustomerNumberTransactions") = gvCustomers.SelectedRow.Cells(1).Text
        'bind
        DBAccounts.GetAccountByCustomerNumberForEmployeeTransactionSearch(Session("CustomerNumberTransactions"))
        gvAccounts.DataSource = DBAccounts.MyView
        gvAccounts.DataBind()
    End Sub

    Protected Sub gvAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAccounts.SelectedIndexChanged
        Session("AccountNumberTransactions") = gvAccounts.SelectedRow.Cells(1).Text
        Response.Redirect("EmployeeTransactionSearch2.aspx")
    End Sub

    Protected Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        PanelAccounts.Visible = False
        CustomerSelect.Visible = True
        DBCustomer.GetNumberFNameLNameAllCustomers()
        gvCustomers.DataSource = DBCustomer.MyView
        gvCustomers.DataBind()
    End Sub
End Class