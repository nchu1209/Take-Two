Option Strict On

Public Class ManagerResolveDisputes

    Inherits System.Web.UI.Page

    Dim DBDispute As New ClassDBDisputeManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session("Status") = "Null"
        End If
    End Sub

    Protected Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        Session("Status") = "Null"
    End Sub

    Protected Sub btnUnresolved_Click(sender As Object, e As EventArgs) Handles btnUnresolved.Click
        Session("Status") = "Resolved"
    End Sub

    Protected Sub btnResolved_Click(sender As Object, e As EventArgs) Handles btnResolved.Click
        Session("Status") = "Submitted"
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Session("DisputeID") = GridView1.SelectedRow.Cells(1).Text
        Response.Redirect("ManagerDisputeResolution.aspx")
    End Sub
End Class