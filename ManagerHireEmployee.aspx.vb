Public Class ManagerHireEmployee
    Inherits System.Web.UI.Page

    Dim EmpDB As New ClassDBEmployee
    Dim EmpDB2 As New ClassDBEmployee
    Dim Format As New ClassFormat
    Dim DBAccounts As New ClassDBAccounts
    Dim Valid As New ClassValidate
    Dim valid2 As New ClassEmployeeValidation
    Dim mEmployeeID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'check to see if session emptype exists page 60
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        Dim strEmpID As String
        strEmpID = Convert.ToString(Session("EmpID"))



        'EmpDB.GetAllEmployeesUsingSP()

        If IsPostBack = False Then
            pnlHired.Visible = False
        End If

    End Sub


    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        lblError.Text = ""


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

        If txtSSN.Text <> "" Then
            If valid2.CheckSSN(txtSSN.Text) = False Then
                lblError.Text = "Please enter a valid 9 digit Social Security number."
                Exit Sub
            End If
        End If


        If Not IsValid Then
            Exit Sub
        End If

        Dim strInputSSN As String
        strInputSSN = txtSSN.Text.ToString
        'find if ssn is already in use
        'If EmpDB.GetBySSN(strInputSSN) = False Then
        If EmpDB.CheckSSNSP(CStr(txtSSN.Text)) = False Then
            Dim strEmpID As String
            strEmpID = EmpDB.EmpDataset.Tables("tblEmployees").Rows(0).Item("EmpID").ToString
            lblError.Text = "SSN#" & strInputSSN & " is already in use at this company. Please go to the Modify Employee Account tab and enter their empID:" & strEmpID & " to edit their employee info or try again with a different SSN."
            Exit Sub
        End If
        'End If

        Dim intMaxEmpID As Integer
        EmpDB.GetMaxEmpIDUsingSP()
        intMaxEmpID = CInt(EmpDB.EmpDataset2.Tables("tblEmployees").Rows(0).Item("EmpID2")) + 1


        EmpDB.AddEmployee(txtEmpType.Text, txtPassword.Text, txtLastName.Text, txtFirstName.Text, txtInitial.Text, txtSSN.Text, txtAddress.Text, txtZip.Text, txtPhone.Text, intMaxEmpID)

        EmpDB.LinkZip(intMaxEmpID)

        lblMessage.Text = "Employee profile successfully added."
        pnlHired.Visible = True
        ModifyProfile.Visible = False


    End Sub







    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        'Do not change anything.
        lblError.Text = ""
        ClearTextboxes()

    End Sub



    Private Sub ClearTextboxes()
        txtAddress.Text = ""
        txtEmpType.Text = ""
        txtFirstName.Text = ""
        txtInitial.Text = ""
        txtLastName.Text = ""
        txtPassword.Text = ""
        txtPhone.Text = ""
        txtSSN.Text = ""
        txtZip.Text = ""
    End Sub
End Class