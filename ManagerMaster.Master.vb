Public Class ManagerMaster
    Inherits System.Web.UI.MasterPage

    Dim db As New ClassDBDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblName.Text = Session("EmployeeFirstName").ToString
        lblName2.Text = Session("EmployeeFirstName").ToString

        db.GetDate()

        Dim strDate As String
        Dim strNewDate As String

        strDate = db.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString
        strNewDate = strDate.Substring(0, strDate.Length - 11)
        lblDate.Text = strNewDate

    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click
        Session("EmployeeID") = Nothing
        Session("EmployeeFirstName") = Nothing
        Response.Redirect("EmployeeLogin.aspx")
    End Sub


End Class