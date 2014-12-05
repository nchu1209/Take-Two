Option Strict On

Public Class ManageApproveStockAccounts
    Inherits System.Web.UI.Page

    Dim dbstocks As New ClassDBStocks

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("empID") Is Nothing Then
            Response.Redirect("ManageApproveStockAccounts.aspx")
        End If

        lblMessage.Text = ""
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        For i = 0 To gvStockAccounts.Rows.Count - 1
            Dim d As DropDownList = DirectCast(gvStockAccounts.Rows(i).Cells(4).FindControl("ddlApproved"), DropDownList)
            Dim strAccountNumber As String = gvStockAccounts.Rows(i).Cells(2).Text

            dbstocks.ApproveStockAccount(d.SelectedValue, strAccountNumber)

        Next

        lblMessage.Text = "Selected stocks successfully approved."
    End Sub

End Class