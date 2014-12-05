Option Strict On

Public Class CustomerCreateAccount

    Inherits System.Web.UI.Page

    Dim DB As New ClassDBCustomer
    Dim Valid As New ClassValidate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'add comment
    End Sub

    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        Response.Redirect("CustomerLogin.aspx")
    End Sub

    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        Dim intCustomerNumber As Integer

        DB.CustomerNumber()

        intCustomerNumber = CInt(DB.CustDataset2.Tables("tblCustomers").Rows(0).Item("CustomerNumber2")) + 1

        If Valid.CheckPhone(txtPhone.Text) = False Then
            lblError.Text = "ERROR: 10 digit phone number required"
            Exit Sub
        End If


        If Valid.CheckDOB(txtDOB.Text) = False Then
            lblError.Text = "ERROR: Enter 4 digit year of birth."
            Exit Sub
        End If

        'check to make sure email is not already in the system
        If DB.CheckUsername(txtEmail.Text) Then
            lblError.Text = "You have already created an account with this email address. Either log on with this account or use another email address"
            Exit Sub
        End If


        If Not IsValid Then
            Exit Sub
        End If

        DB.AddCustomer(intCustomerNumber, txtLastName.Text, txtFirstName.Text, txtInitial.Text, txtPassword.Text, txtAddress.Text, txtZip.Text, txtEmail.Text, txtPhone.Text, txtDOB.Text)

    End Sub

  
End Class