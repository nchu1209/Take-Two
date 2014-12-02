Option Strict On

Public Class ManagerManageCustomer
    Inherits System.Web.UI.Page

    Dim EmpDB As New ClassDBEmployee
    Dim CustomerDB As New ClassDBCustomer
    Dim Format As New ClassFormat
    Dim DBAccounts As New ClassDBAccounts
    Dim Valid As New ClassValidate
    Dim mEmployeeID As Integer
    Dim mCustomerID As Integer
    Dim mstrNewPassword As String
    Dim mstrOldPassword As String
    Dim mintCustomerNumber As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check to see if session emptype exists page 60
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        Dim strEmpID As String
        strEmpID = Convert.ToString(Session("EmpID"))
        'get that custoemrs record
        'DB.GetByEmpID(strEmpID)
        'End If

        CustomerDB.GetAllCustomers()


        If IsPostBack = False Then
            ModifyProfile.Visible = False
            AccountNames.Visible = False
        End If





    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        CustomerDB.GetAllCustomers()
        Session("CustomerNumberForSearch") = txtCustomerNumber.Text

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumberForSearch").ToString)

        Dim intCurrentCustomerNumber As Integer
        intCurrentCustomerNumber = CInt(Session("CustomerNumberForSearch"))



        If txtCustomerNumber.Text = "" Then
            lblCustomerNumberError.Text = "ERROR: You did not enter a Customer Number."
            ModifyProfile.Visible = False
            AccountNames.Visible = False
            Exit Sub
        End If

        If txtCustomerNumber.Text <> "" Then
            If Valid.CheckDigits(txtCustomerNumber.Text) = False Then
                lblCustomerNumberError.Text = "ERROR: You did not enter a proper Customer Number."
                ModifyProfile.Visible = False
                AccountNames.Visible = False
                Exit Sub
            End If
        End If


        If CustomerDB.SearchByCustomerNumber(CStr(intCurrentCustomerNumber)) = False Then
            lblCustomerNumberError.Text = "ERROR: Invalid Customer Number."
            ModifyProfile.Visible = False
            AccountNames.Visible = False
            Exit Sub
        End If

        'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumberForSearch").ToString)
        ModifyProfile.Visible = True
        AccountNames.Visible = True
        ReloadDatasetAndDDL()
        FillTextboxes()
        lblMessage.Text = ""


        lblError.Text = ""
        lblCustomerNumberError.Text = ""




    End Sub


    Public Sub ReloadDatasetAndDDL()
        'Purpose: reloads the dataset and resets the ddl
        'Arguments: n/a
        'Returns: n/a
        'Author: Catherine King
        'Date: 10/4/2014

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumberForSearch").ToString)
        ddlAccounts.DataSource = DBAccounts.AccountsDataset4
        ddlAccounts.DataTextField = "AccountName"
        ddlAccounts.DataValueField = "AccountNumber"
        ddlAccounts.DataBind()

        If ddlAccounts.Items.Count = 0 Then
            AccountNames.Visible = False
            lblMessage.Text = "count"
        End If

    End Sub




    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        'Do not change anything.
        lblError.Text = ""
        FillTextboxes()
        lblMessage.Text = ""
        txtChangeName.Text = ""

    End Sub

    Protected Sub btnSaveAccountName_Click(sender As Object, e As EventArgs) Handles btnSaveAccountName.Click
        DBAccounts.ModifyAccountName(txtChangeName.Text, CInt(ddlAccounts.SelectedValue))
        ReloadDatasetAndDDL()
        lblMessage.Text = "Account Name was successfully changed."
        lblError.Text = ""
        txtChangeName.Text = ""
    End Sub

    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        mstrNewPassword = txtPassword.Text
        Session("NewPassword") = txtPassword.Text
        lblError.Text = ""
        CustomerDB.GetByCustomerNumber(Session("CustomerNumberForSearch").ToString)

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


        If txtEmail.Text <> CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("EmailAddr").ToString Then
            If CustomerDB.CheckEmailSP(txtEmail.Text) = True Then
                lblError.Text = "Email already used"
                Exit Sub
            End If
        End If

        If Not IsValid Then
            Exit Sub
        End If

        CustomerDB.ModifyCustomer2(txtEmail.Text, txtPassword.Text, txtLastName.Text, txtFirstName.Text, txtInitial.Text, txtAddress.Text, txtZip.Text, txtPhone.Text, CInt(Session("CustomerNumberForSearch")))
        CustomerDB.LinkZip(mCustomerID.ToString)
        FillTextboxes()
        lblMessage.Text = ""
        lblError.Text = "Profile successfully modified."
        txtChangeName.Text = ""
        'mstrOldPassword = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString
        'Session("OldPassword") = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString

    End Sub



    Protected Sub btnCancelAccountName_Click(sender As Object, e As EventArgs) Handles btnCancelAccountName.Click
        txtChangeName.Text = ""
        ReloadDatasetAndDDL()
        lblMessage.Text = ""
        txtChangeName.Text = ""
        lblError.Text = ""
    End Sub



    Private Sub FillTextboxes()

        'declare variables
        'Dim strPhone As String

        'populate variables
        'strPhone = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString

        mCustomerID = CInt(Session("CustomerNumberForSearch"))
        CustomerDB.LinkZip(mCustomerID.ToString)

        'fill textboxes
        txtLastName.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("lastname").ToString
        txtFirstName.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("firstname").ToString
        txtInitial.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("MI").ToString
        txtPassword.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("password").ToString
        txtAddress.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("address").ToString
        txtCity.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("city").ToString
        txtState.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("state").ToString
        txtZip.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("zipcode").ToString
        txtEmail.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("emailaddr").ToString
        txtPhone.Text = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString

        Dim strAccountStatus As String
        Dim strNotAccountStatus As String
        strAccountStatus = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("active").ToString

        If CBool(strAccountStatus) = True Then
            strAccountStatus = "active"
            strNotAccountStatus = "inactive"
        Else
            strAccountStatus = "inactive"
            strNotAccountStatus = "active"
        End If
        lblAccountStatus.Text = "This customer is currently " + strAccountStatus + ".  Would you like to make them " + strNotAccountStatus + "?"
    End Sub


    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        CustomerDB.GetByCustomerNumber(Session("CustomerNumberForSearch").ToString)
        'Dim intAccountStatus As Integer
        Dim intNotAccountStatus As Integer
        Dim strAccountStatus As String
        strAccountStatus = CustomerDB.CustDataset.Tables("tblCustomers").Rows(0).Item("active").ToString


        If CBool(strAccountStatus) = True Then
            intNotAccountStatus = 0
        Else
            intNotAccountStatus = 1
        End If

        CustomerDB.ModifyStatus(intNotAccountStatus, CInt(Session("CustomerNumberForSearch")))


        FillTextboxes()
        lblMessage.Text = ""
        lblError.Text = ""
        txtChangeName.Text = ""
    End Sub



End Class