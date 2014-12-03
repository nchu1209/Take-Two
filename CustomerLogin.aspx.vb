Public Class CustomerLogin
    Inherits System.Web.UI.Page

    'declare instance of Cust DB
    Dim DB As New ClassDBCustomer
    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            pnlNoLongerCustomer.Visible = False
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'check if empID exists in DB; if bad, add 1 to session count
        If DB.CheckEmail(txtEmail.Text) = False Then
            'empID is invalid, return error
            lblError.Text = "ERROR: Email address does not exist in database."
            Exit Sub
        End If

        'check password
        If DB.CheckPassword(txtEmail.Text, txtPassword.Text) = False Then
            'invalid password, return error
            lblError.Text = "ERROR: Invalid password."
            Exit Sub
        End If

        Dim bolActive As Boolean
        bolActive = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("active").ToString
        If bolActive = False Then
            pnlNoLongerCustomer.Visible = True
            pnlLogin.Visible = False

            lblDisabled.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("FirstName") & ", your account is currently disabled."
            Exit Sub
        End If




        lblError.Text = "YA"

        Session("CustomerFirstName") = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("FirstName")
        Session("CustomerNumber") = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("CustomerNumber")

        'DBAccounts.GetApprovedStatus(Session("CustomerNumber"))
        'Session("CustomerManagerApprovedStockAccount") = DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("CustomerID")
        Response.Redirect("CustomerHome.aspx")

    End Sub
End Class