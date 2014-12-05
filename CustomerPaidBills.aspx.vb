Option Strict On
Public Class CustomerPaidBills
    Inherits System.Web.UI.Page
    Dim dbact As New ClassDBAccounts


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            If Session("CustomerFirstName") Is Nothing Then
                Response.Redirect("CustomerLogin.aspx")
            End If

            dbact.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

            If dbact.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
                Response.Redirect("CustomerCreateBankAccount.aspx")
            End If
        End If

    End Sub

End Class