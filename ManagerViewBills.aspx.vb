Option Strict On

Public Class ManagerViewBills
    Inherits System.Web.UI.Page

    Dim dbbill As New ClassDBBill

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeFirstName") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If

        lblMessage.Text = ""
    End Sub

    Protected Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        Session("Status") = "Null"
        Session("Active") = "Null"
    End Sub

    Protected Sub gvBills_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvBills.SelectedIndexChanged
        Dim index As Integer
        index = gvBills.SelectedIndex

        Session("BillID") = gvBills.Rows(index).Cells(1).Text
        Response.Redirect("ManagerEditBills.aspx")

    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        Session("Status") = "Paid"
        Session("Active") = "False"
    End Sub

    Protected Sub btnActive_Click(sender As Object, e As EventArgs) Handles btnActive.Click
        For i = 0 To gvBills.Rows.Count - 1
            Dim d As DropDownList = DirectCast(gvBills.Rows(i).Cells(9).FindControl("ddlActive"), DropDownList)
            Dim strBillID As String = gvBills.Rows(i).Cells(1).Text
            dbbill.ModifyBillActive(d.SelectedValue, strBillID)
        Next

        lblMessage.Text = "Active/Inactive status successfully updated for all bills."
    End Sub
End Class