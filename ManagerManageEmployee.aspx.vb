Option Strict On

Public Class ManagerManageEmployee
    Inherits System.Web.UI.Page


    Dim EmpDB As New ClassDBEmployee
    Dim EmpDB2 As New ClassDBEmployee
    Dim Format As New ClassFormat
    Dim DBAccounts As New ClassDBAccounts
    Dim Valid As New ClassValidate
    Dim valid2 As New ClassEmployeeValidation
    Dim mEmployeeID As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check to see if session emptype exists page 60
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        Dim strEmpID As String
        strEmpID = Convert.ToString(Session("EmpID"))



        EmpDB.GetAllEmployeesUsingSP()
        'get that custoemrs record
        'EmpDB.GetByEmpID(strEmpID)
        'End If





        If IsPostBack = False Then
            ModifyProfile.Visible = False
            ChangeStatus.Visible = False
        End If

        'FillTextboxes()



    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        EmpDB.GetAllEmployeesUsingSP()
        'get that custoemrs record

        If txtEmployeeID.Text = "" Then
            lblEmpIDError.Text = "ERROR: You did not enter an EmployeeID."
            ModifyProfile.Visible = False
            ChangeStatus.Visible = False
            Exit Sub
        End If

        If txtEmployeeID.Text <> "" Then
            If Valid.CheckDigits(txtEmployeeID.Text) = False Then
                lblEmpIDError.Text = "ERROR: You did not enter a proper EmployeeID."
                ModifyProfile.Visible = False
                ChangeStatus.Visible = False
                Exit Sub
            End If
        End If


        Session("EmployeeIDForSearch") = txtEmployeeID.Text

        'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumberForSearch").ToString)

        Dim intCurrentEmployeeID As Integer
        intCurrentEmployeeID = CInt(Session("EmployeeIDForSearch"))
        Dim intManagerAccount As Integer
        intManagerAccount = CInt(Session("EmpType"))

        If EmpDB.SearchByEmpID(CStr(intCurrentEmployeeID)) = False Then
            lblEmpIDError.Text = "ERROR: Invalid EmployeeID."
            ModifyProfile.Visible = False
            ChangeStatus.Visible = False
            Exit Sub
        End If

        'get that custoemrs record
        EmpDB2.GetByEmpID(CStr(intCurrentEmployeeID))
        Dim strNewEmpType As String
        strNewEmpType = EmpDB2.EmpDataset.Tables("tblEmployees").Rows(0).Item("empType").ToString

        If strNewEmpType = "102" Then
            If intCurrentEmployeeID = intManagerAccount Then
                lblEmpIDError.Text = "Please go to the Modify Manager Account tab to manage your own profile."
            Else
                lblEmpIDError.Text = "You do not have access to a manager's account."
            End If
            ModifyProfile.Visible = False
            ChangeStatus.Visible = False
            Exit Sub
        End If



        'DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumberForSearch").ToString)
        ModifyProfile.Visible = True
        ChangeStatus.Visible = True
        FillTextboxes()


        lblError.Text = ""
        lblEmpIDError.Text = ""




    End Sub




    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        'Do not change anything.
        lblError.Text = ""
        FillTextboxes()

    End Sub


    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        lblError.Text = ""
        EmpDB.GetByEmpID(Session("EmployeeIDForSearch").ToString)

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

        'EmpID must be 101 or 102
        If txtEmpType.Text <> "" Then
            If valid2.CheckEmpType(txtEmpType.Text) = False Then
                lblError.Text = "Please enter 101(Employee) or 102(Manager) for EmpType."
                Exit Sub
            End If
        End If


        If Not IsValid Then
            Exit Sub
        End If

        EmpDB.ModifyEmployee(txtEmpType.Text, txtPassword.Text, txtLastName.Text, txtFirstName.Text, txtInitial.Text, txtAddress.Text, txtZip.Text, txtPhone.Text, CInt(Session("EmployeeIDForSearch")))
        EmpDB.LinkZip(Session("EmployeeIDForSearch").ToString)
        FillTextboxes()
        lblError.Text = "Profile successfully modified."

    End Sub

    Private Sub FillTextboxes()

        'declare variables
        'Dim strPhone As String

        'populate variables
        'strPhone = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString
        EmpDB.GetByEmpID(Session("EmployeeIDForSearch").ToString)
        mEmployeeID = CInt(Session("EmployeeIDForSearch"))
        EmpDB.LinkZip(mEmployeeID.ToString)

        'fill textboxes
        txtLastName.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("lastname").ToString
        txtFirstName.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("firstname").ToString
        txtInitial.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("MI").ToString
        txtPassword.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("password").ToString
        txtAddress.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("address").ToString
        txtCity.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("city").ToString
        txtState.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("state").ToString
        txtZip.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("zipcode").ToString
        txtEmpType.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("emptype").ToString
        txtPhone.Text = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("phone").ToString

        Dim strAccountStatus As String
        Dim strNotAccountStatus As String
        strAccountStatus = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("active").ToString

        If strAccountStatus = "T" Then
            strAccountStatus = "currently working here"
            strNotAccountStatus = "fire him/her"
        Else
            strAccountStatus = "no longer working here"
            strNotAccountStatus = "rehire him/her"
        End If
        lblAccountStatus.Text = "This employee is " + strAccountStatus + ".  If you would you like to " + strNotAccountStatus + ", please click Confirm below."
    End Sub


    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        EmpDB.GetByEmpID(Session("EmployeeIDForSearch").ToString)
        'Dim intAccountStatus As Integer
        Dim strAccountStatus As String
        Dim strNotAccountStatus As String
        strAccountStatus = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("active").ToString


        If strAccountStatus = "T" Then
            strNotAccountStatus = "F"
        Else
            strNotAccountStatus = "T"
        End If

        EmpDB.ModifyStatus(strNotAccountStatus, CInt(Session("EmployeeIDForSearch")))


        FillTextboxes()
        lblError.Text = ""

    End Sub



End Class