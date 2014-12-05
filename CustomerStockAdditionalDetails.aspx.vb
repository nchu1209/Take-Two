Public Class CustomerStockAdditionalDetails
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        'for loop that calculates values for lblChange
        For i = 1 To gvAdditionalDetails.Rows.Count - 1
            'change is this row minus previous row
            Dim decChange As Decimal = CDec(gvAdditionalDetails.Rows(i).Cells(2).Text) - CDec(gvAdditionalDetails.Rows(i - 1).Cells(2).Text)
            gvAdditionalDetails.Rows(i).Cells(3).Text = decChange.ToString

            If decChange > 5 Then
                gvAdditionalDetails.Rows(i).BackColor = Drawing.Color.Green
            ElseIf decChange > 0 Then
                gvAdditionalDetails.Rows(i).BackColor = Drawing.Color.LightGreen
            ElseIf decChange < -5 Then
                gvAdditionalDetails.Rows(i).BackColor = Drawing.Color.DarkRed
            Else
                gvAdditionalDetails.Rows(i).BackColor = Drawing.Color.Red
            End If
        Next
    End Sub

    Protected Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Response.Redirect("CustomerSellingStock.aspx")
    End Sub
End Class