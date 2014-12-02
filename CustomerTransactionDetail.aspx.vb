Option Strict On

Public Class CustomerTransactionDetail
    Inherits System.Web.UI.Page

    Dim DBTransactions As New ClassDBTransactions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")

            'lblTransactionID=Session("

            'DBTransactions.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

            ''get the record id from the select
            'mCustomerID = CInt(Session("CustomerNumber"))

            If IsPostBack = False Then
                'ReloadDatasetAndDDL()
                FillTextboxes()
            End If

        End If
    End Sub

    Private Sub FillTextboxes()

        'declare variables
        'Dim strPhone As String

        'populate variables
        'strPhone = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString


        'fill textboxes
        'txtDescription.Text = DBTransactions.CustDataset.Tables("tblCustomers").Rows(0).Item("lastname").ToString
        'txtTransactionType.Text = DBTransactions.CustDataset.Tables("tblCustomers").Rows(0).Item("firstname").ToString
        'txtAmount.Text = DBTransactions.CustDataset.Tables("tblCustomers").Rows(0).Item("MI").ToString
        'txtTransactionNumber.Text = DBTransactions.CustDataset.Tables("tblCustomers").Rows(0).Item("password").ToString
        'txtAddress.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("address").ToString
        'txtCity.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("city").ToString
        'txtState.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("state").ToString
        'txtZip.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("zipcode").ToString
        'txtEmail.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("emailaddr").ToString
        'txtPhone.Text = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("phone").ToString

    End Sub


End Class