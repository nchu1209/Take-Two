Public Class CustomerMaster
    Inherits System.Web.UI.MasterPage

    Dim db As New ClassDBDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblName.Text = Session("CustomerFirstName").ToString
        lblName2.Text = Session("CustomerFirstName").ToString

        db.GetDate()

        Dim strDate As String
        Dim strNewDate As String

        strDate = db.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString
        strNewDate = strDate.Substring(0, strDate.Length - 11)
        lblDate.Text = strNewDate

    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click
        Session("CustomerNumber") = Nothing
        Session("CustomerFirstName") = Nothing
        Session("UnqualifiedDistributionFee") = Nothing
        Session("CustomerManagerApprovedStockAccount") = Nothing
        'do we need to reset all the session variables?
        Response.Redirect("CustomerLogin.aspx")
    End Sub
End Class