Option Strict On

Public Class CustomerManageAccount
    Inherits System.Web.UI.Page


    Dim DBAccounts As New ClassDBAccounts
    Dim DB As New ClassDBCustomer
    Dim Format As New ClassFormat
    Dim mCustomerID As Integer
    Dim Valid As New ClassValidate
    Dim mstrNewPassword As String
    Dim mstrOldPassword As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If



        ''get the record id from the select
        mCustomerID = CInt(Session("CustomerNumber"))

        If IsPostBack = False Then
            ReloadDatasetAndDDL()
            FillTextboxes()
        End If
        'mCustomerID = 10046
    End Sub

    Public Sub ReloadDatasetAndDDL()
        'Purpose: reloads the dataset and resets the ddl
        'Arguments: n/a
        'Returns: n/a
        'Author: Catherine King
        'Date: 10/4/2014

        'Purpose: reloads the dataset and resets the ddl
        'Arguments: n/a
        'Returns: n/a
        'Author: Leah Carroll

        DBAccounts.GetAccountWithAccountNumberByCustomerNumber(Session("CustomerNumber").ToString)
        ddlAccounts.DataSource = DBAccounts.AccountsDataset4
        ddlAccounts.DataTextField = "Details"
        ddlAccounts.DataValueField = "AccountNumber"
        ddlAccounts.DataBind()

    End Sub

    Private Sub FillTextboxes()

        'declare variables
        'Dim strPhone As String

        'populate variables
        'strPhone = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString
        DB.LinkZip(mCustomerID.ToString)

        'fill textboxes
        txtLastName.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("lastname").ToString
        txtFirstName.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("firstname").ToString
        txtInitial.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("MI").ToString
        txtPassword.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("password").ToString
        txtAddress.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("address").ToString
        txtCity.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("city").ToString
        txtState.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("state").ToString
        txtZip.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("zipcode").ToString
        txtEmail.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("emailaddr").ToString
        txtPhone.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString

    End Sub

    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        'Do not change anything.
        lblError.Text = ""
        FillTextboxes()
    End Sub

    Protected Sub btnSaveAccountName_Click(sender As Object, e As EventArgs) Handles btnSaveAccountName.Click
        DBAccounts.ModifyAccountName(txtChangeName.Text, CInt(ddlAccounts.SelectedValue))
        Response.AddHeader("Refresh", "0; URL=CustomerManageAccount.aspx")
    End Sub

    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        mstrNewPassword = txtPassword.Text
        Session("NewPassword") = txtPassword.Text
        lblError.Text = ""
        DB.GetByCustomerNumber(Session("CustomerNumber").ToString)

        'validations
        If txtPhone.Text <> "" Then
            If Valid.CheckPhone(txtPhone.Text) = False Then
                lblError.Text = "Please put in a valid 10-digit phone number."
                Exit Sub
            End If
        End If

        'MI must be 1 letter
        If txtInitial.Text <> "" Then
            If Valid.CheckInitial(txtInitial.Text) = False Then
                lblError.Text = "Please put in a single letter for middle initial."
                Exit Sub
            End If
        End If

        'zip code must be numeric and 5 digits
        If txtZip.Text <> "" Then
            If Valid.CheckZip(txtZip.Text) = False Then
                lblError.Text = "Please enter a five digit zip code"
                Exit Sub
            End If
        End If


        If txtEmail.Text <> DB.CustDataset.Tables("tblCustomers").Rows(0).Item("EmailAddr").ToString Then
            If DB.CheckEmailSP(txtEmail.Text) = True Then
                lblError.Text = "Email already used"
                Exit Sub
            End If
        End If


        If Not IsValid Then
            Exit Sub
        End If

        DB.ModifyCustomer(txtEmail.Text, txtLastName.Text, txtFirstName.Text, txtInitial.Text, txtAddress.Text, txtZip.Text, txtPhone.Text, CInt(Session("CustomerNumber")))
        DB.LinkZip(mCustomerID.ToString)
        FillTextboxes()

        mstrOldPassword = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString
        Session("OldPassword") = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString

        'lblError.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString()
        If mstrNewPassword <> mstrOldPassword Then
            Password.Visible = True
            ModifyProfile.Visible = False
            AccountNames.Visible = False
        End If

    End Sub

    Protected Sub btnCancelPassword_Click(sender As Object, e As EventArgs) Handles btnCancelPassword.Click
        Password.Visible = False
        ModifyProfile.Visible = True
        AccountNames.Visible = True

        FillTextboxes()

        lblError.Text = "Password not updated."

    End Sub

    Protected Sub btnConfirmPassword_Click(sender As Object, e As EventArgs) Handles btnConfirmPassword.Click

        If txtOld.Text <> Session("OldPassword").ToString Or txtNew.Text <> Session("NewPassword").ToString Then
            lblErrorPassword.Text = "Incorrect Password(s)."
            Exit Sub
        End If


        DB.ModifyPassword(Session("NewPassword").ToString, CInt(Session("CustomerNumber")))
        DB.LinkZip(mCustomerID.ToString)
        FillTextboxes()

        Password.Visible = False
        ModifyProfile.Visible = True
        AccountNames.Visible = True

    End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    'lblErrorPassword.Text = mstrOldPassword.ToString

    '    lblErrorPassword.Text = Session("OldPassword").ToString


    'End Sub

    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    lblErrorPassword.Text = Session("NewPassword").ToString


    'End Sub

    Protected Sub btnCancelAccountName_Click(sender As Object, e As EventArgs) Handles btnCancelAccountName.Click
        txtChangeName.Text = ""
    End Sub

    Protected Sub ddlAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccounts.SelectedIndexChanged

    End Sub
End Class