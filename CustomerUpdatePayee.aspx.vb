Option Strict On

Public Class CustomerUpdatePayee
    Inherits System.Web.UI.Page

    Dim db As New ClassDBPayee
    Dim valid As New ClassValidate
    Dim DBAccounts As New ClassDBAccounts


    Dim mintPayeeID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        If Request.QueryString("ID") Is Nothing Then
            Response.Redirect("CustomerPayBills.aspx")
        End If

        lblMessage.Text = ""

        mintPayeeID = CInt(Request.QueryString("ID"))
        If IsPostBack = False Then
            db.GetPayeeByID(mintPayeeID.ToString)
            FillTextboxes()
        End If

    End Sub

    Private Sub FillTextboxes()
        txtPayeeName.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("PayeeName").ToString
        txtAddress.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("Address").ToString
        txtCity.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("City").ToString
        txtState.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("State").ToString
        txtZip.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("ZipCode").ToString
        txtPhone.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("Phone").ToString
        txtType.Text = db.PayeeDataset.Tables("tblPayees").Rows(0).Item("PayeeType").ToString
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'validations
        If Page.IsValid = False Then
            Exit Sub
        End If

        If valid.CheckZip(txtZip.Text) = False Then
            lblMessage.Text = "Please enter a valid zip code."
            Exit Sub
        End If

        If valid.CheckPhone(txtPhone.Text) = False Then
            lblMessage.Text = "Please enter a valid phone number."
            Exit Sub
        End If

        'update payee
        db.UpdatePayee(mintPayeeID.ToString, txtPayeeName.Text, txtType.Text, txtAddress.Text, txtZip.Text, txtPhone.Text)

        'show messages and updated info woo
        lblMessage.Text = "Payee information successfully updated."
        db.GetPayeeByID(mintPayeeID.ToString)
        FillTextboxes()
    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        FillTextboxes()
    End Sub
End Class