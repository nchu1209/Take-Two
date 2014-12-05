Public Class CustomerUserManual
    Inherits System.Web.UI.Page


    Dim dbact As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        dbact.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If dbact.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        DisputesPanel.Visible = False
        CreatingAccountPanel.Visible = False
        ModifyingYourAccountPanel.Visible = False
        StocksPanel.Visible = False

    End Sub

    Protected Sub ddlHelp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHelp.SelectedIndexChanged
        If ddlHelp.SelectedIndex = 1 Then
            DisputesPanel.Visible = True
        End If
        If ddlHelp.SelectedIndex = 2 Then
            CreatingAccountPanel.Visible = True

        End If
        If ddlHelp.SelectedIndex = 3 Then
            ModifyingYourAccountPanel.Visible = True

        End If
        If ddlHelp.SelectedIndex = 4 Then
            StocksPanel.Visible = True
        End If
    End Sub
End Class