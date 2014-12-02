Option Strict On

Public Class ManagerResolveDisputes

    Inherits System.Web.UI.Page

    Dim DBDispute As New ClassDBDispute

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            DBDispute.GetAllUnresolvedDisputes()
        End If
    End Sub

    Protected Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        DBDispute.GetAllDisputes()
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        DBDispute.GetAllUnresolvedDisputes()
    End Sub

    Protected Sub btnActive_Click(sender As Object, e As EventArgs) Handles btnActive.Click

    End Sub
End Class