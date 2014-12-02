Public Class ManagerModifyAddress
    Inherits System.Web.UI.Page

    'declare classes
    Dim valid As New ClassEmployeeValidation
    Dim DB As New ClassDBEmployee
    Dim mEmployeeID As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")

        End If


        mEmployeeID = CInt(Session("EmpID"))

        If IsPostBack = False Then

            FillTextboxes()

        End If




    End Sub


    Protected Sub btnModifyAddress_Click(sender As Object, e As EventArgs) Handles btnModifyAddress.Click
        'get that custoemrs reord
        DB.GetByEmpID(mEmployeeID)

        'reset the label
        lblError.Text = ""



        'check zip code
        If txtZipcode.Text <> "" Then
            If valid.CheckZip(txtZipcode.Text) = False Then
                'err msg
                lblError.Text = "Zip code is invalid."
                'exit sub
                Exit Sub
            End If
        End If


        'the record validator check
        If Not IsValid Then
            Exit Sub
        End If

        'output that the record was added
        lblError.Text = "Record Modified."

        'QUERY STUFF

        ' runupdate query to modify record
        Dim strRow As String
        strRow = Session("EmpID").ToString

        ''get that custoemrs reord
        'DB.GetByEmpID(strRow)

        'modify the DB
        DB.ModifyEmployeeAddress(txtAddress.Text, txtZipcode.Text, strRow)
        DB.LinkZip(Session("EmpID").ToString)
        FillTextboxes()

        'call next page(EmployeeHome)
        Response.AddHeader("Refresh", "2; URL=EmployeeHome.aspx")


    End Sub


    'Purpose: filling the textboxes
    'Arguments: none
    'returns: the values to the textboxes when called
    ' Author: Leah Carroll
    'Date: 10-6- 2014
    Public Sub FillTextboxes()
        'put info from selected customer into text boxes on form

        DB.LinkZip(mEmployeeID.ToString)

        txtZipcode.Text = DB.EmpDataset.Tables("tblEmployees").Rows(0).Item("Zipcode").ToString
        txtAddress.Text = DB.EmpDataset.Tables("tblEmployees").Rows(0).Item("Address").ToString
        txtCity.Text = DB.EmpDataset.Tables("tblEmployees").Rows(0).Item("city").ToString
        txtState.Text = DB.EmpDataset.Tables("tblEmployees").Rows(0).Item("state").ToString

    End Sub
End Class