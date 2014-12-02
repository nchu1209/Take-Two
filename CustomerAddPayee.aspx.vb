Public Class CustomerAddPayee
    Inherits System.Web.UI.Page

    Dim DB As New ClassDBPayee
    Dim valid As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DB.GetAllPayees()
        If IsPostBack = False Then
            LoadDDL()
        End If

        lblMessage.Text = ""
        lblMessage2.Text = ""
    End Sub

    Private Sub LoadDDL()
        ddlPayee.DataSource = DB.PayeeDataset.Tables("tblPayees")
        ddlPayee.DataTextField = "PayeeName"
        ddlPayee.DataValueField = "PayeeID"
        ddlPayee.DataBind()
    End Sub

    Protected Sub btnAddPayee_Click(sender As Object, e As EventArgs) Handles btnAddPayee.Click
        'check if combination already exists, if so, exit sub
        If DB.CheckDuplicate(Session("CustomerNumber").ToString, ddlPayee.SelectedValue.ToString) = True Then
            lblMessage.Text = "You have already added this payee."
            Exit Sub
        End If

        DB.AddExistingPayee(Session("CustomerNumber").ToString, ddlPayee.SelectedValue.ToString)
        lblMessage.Text = "Payee successfully added."
    End Sub

    Protected Sub btnCreatePayee_Click(sender As Object, e As EventArgs) Handles btnCreatePayee.Click
        'validations
        If Page.IsValid = False Then
            Exit Sub
        End If

        If valid.CheckPhone(txtPhone.Text) = False Then
            lblMessage2.Text = "ERROR: Invalid phone number."
            Exit Sub
        End If

        If valid.CheckZip(txtZip.Text) = False Then
            lblMessage2.Text = "ERROR: Invalid zip code."
            Exit Sub
        End If

        'add to Payee table
        DB.GetMaxPayeeID()
        Dim intPayeeID As Integer
        intPayeeID = CInt(DB.PayeeDataset2.Tables("tblPayees").Rows(0).Item("PayeeIDMax")) + 1

        DB.CreatePayee(intPayeeID.ToString, txtPayeeName.Text, ddlType.SelectedValue.ToString, txtAddress.Text, txtZip.Text, txtPhone.Text)

        'add to customer's payees
        DB.AddExistingPayee(Session("CustomerNumber").ToString, intPayeeID.ToString)

        'show message
        lblMessage2.Text = "Payee successfully added."

        'reload DDL
        DB.GetAllPayees()
        LoadDDL()

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtPayeeName.Text = ""
        txtAddress.Text = ""
        txtZip.Text = ""
        txtPhone.Text = ""
    End Sub
End Class