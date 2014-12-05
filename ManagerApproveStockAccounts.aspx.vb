Public Class ManageApproveStockAccounts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("empID") = Nothing Then
            Response.Redirect("ManageApproveStockAccounts.aspx")
        End If
    End Sub

End Class