Public Class EmployeeMaster
    Inherits System.Web.UI.MasterPage

    Dim db As New ClassDBDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        db.GetDate()

        Dim strDate As String

        strDate = db.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString
        lblDate.Text = strDate

    End Sub

End Class