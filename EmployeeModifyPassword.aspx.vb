Public Class EmployeeModifyPassword
    Inherits System.Web.UI.Page

    Dim DB As New ClassDBEmployee
    Dim Format As New ClassEmployeeFormat
    Dim Valid As ClassEmployeeValidation
    Dim mEmployeeID As Integer
    Dim mstrNewPassword As String
    Dim mstrOldPassword As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check to see if session emptype exists page 60
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        Dim strEmpID As String
        strEmpID = Session("EmpID")
        'get that custoemrs record
        DB.GetByEmpID(strEmpID)
        'End If

    End Sub






    Protected Sub btnConfirmPassword_Click(sender As Object, e As EventArgs) Handles btnConfirmPassword.Click
        'get that custoemrs record
        DB.GetByEmpID(Session("EmpID"))
 
        lblErrorPassword.Text = ""
        Session("OldPassword") = DB.EmpDataset.Tables("tblEmployees").Rows(0).Item("Password").ToString


        If txtOld.Text <> Session("OldPassword").ToString Then
            lblErrorPassword.Text = "Incorrect Password."
            Exit Sub
        End If

        Dim strNew As String
        strNew = txtNew.Text
        DB.ModifyPassword(strNew, CInt(Session("EmpID")))


        lblErrorPassword.Text = "Password Updated."

        'call next page(EmployeeHome)
        Response.AddHeader("Refresh", "2; URL=EmployeeHome.aspx")

    End Sub




End Class