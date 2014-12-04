Public Class ManagerTransactionDetail
    Inherits System.Web.UI.Page

    Dim DBDispute As New ClassDBDispute
    Dim DBTransactions As New ClassDBTransactions
    Dim Val As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            DBTransactions.GetDetails(Session("TransactionID").ToString)
            lblDescription.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Description").ToString
            lblTransactionType.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Transaction Type").ToString
            lblAmount.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Amount").ToString
            lblTransactionDate.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Date").ToString
            lblTransactionNumber.Text = Session("TransactionID").ToString

            DBDispute.GetDisputeByTransactionNumber(Session("TransactionID").ToString)
            If DBDispute.DisputeDataset.Tables("tblDispute").Rows.Count = 0 Then
                lblEmployeeComments.Text = "N/A"
                lblDisputeStatus.Text = "A dispute has not yet been submitted for this transaction"
                lblEmpID.Text = "N/A"
            Else
                lblEmployeeComments.Text = DBDispute.DisputeDataset.Tables("tblDispute").Rows(0).Item("ManagerComment").ToString
                lblDisputeStatus.Text = DBDispute.DisputeDataset.Tables("tblDispute").Rows(0).Item("Status").ToString
                lblEmpID.Text = DBDispute.DisputeDataset.Tables("tblDispute").Rows(0).Item("EmployeeID").ToString
            End If

            'bind the gridview to the dataview
            DBTransactions.GetFiveSimilarByAccount(Session("AccountNumberTransactions").ToString, lblTransactionType.Text)
            gvSimilar.DataSource = DBTransactions.MyView2
            gvSimilar.DataBind()
        End If
    End Sub

    Protected Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Response.Redirect("EmployeeTransactionSearch2.aspx")
    End Sub
End Class