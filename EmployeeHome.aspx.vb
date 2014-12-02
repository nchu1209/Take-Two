Public Class EmployeeHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists page 60
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If
    End Sub

End Class