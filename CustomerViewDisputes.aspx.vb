Public Class CustomerViewDisputes
    Inherits System.Web.UI.Page
    Dim DBDispute As New ClassDBDispute
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DBDispute.GetDisputeByAccountNumber(Session("CustomerNumber"))
        gvDisputes.DataSource = DBDispute.MyView
        gvDisputes.DataBind()
    End Sub
End Class