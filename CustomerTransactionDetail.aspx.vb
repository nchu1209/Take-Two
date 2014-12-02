﻿Option Strict On

Public Class CustomerTransactionDetail
    Inherits System.Web.UI.Page
    Dim DBDispute As New ClassDBDispute
    Dim DBTransactions As New ClassDBTransactions
    Dim Val As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerNumber") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If
        If IsPostBack = False Then
            DBTransactions.GetDetails(Session("TransactionID").ToString)
            lblDescription.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Description").ToString
            lblTransactionType.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Transaction Type").ToString
            lblAmount.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Amount").ToString
            lblTransactionDate.Text = DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("Date").ToString
            lblTransactionNumber.Text = Session("TransactionID").ToString

            'bind the gridview to the dataview
            DBTransactions.TransactionsByAccount(Session("AccountNumber").ToString)
            DBTransactions.GetFiveSimilar(lblTransactionType.Text)
            gvSimilar.DataSource = DBTransactions.MyView2
            gvSimilar.DataBind()

            Panel1.Visible = False
        End If
    End Sub
    Public Sub GetDisputeNumber()
        DBDispute.GetMaxDisputeNumber()
        If DBDispute.DisputeDataset.Tables("tblDispute").Rows(0).Item("MaxDisputeNumber") Is DBNull.Value Then
            Session("DisputeNumber") = 1
        Else
            Session("DisputeNumber") = CInt(DBDispute.DisputeDataset.Tables("tblDispute").Rows(0).Item("MaxDisputeNumber")) + 1
        End If
    End Sub
    Protected Sub btnCreateDispute_Click(sender As Object, e As EventArgs) Handles btnCreateDispute.Click
        Panel1.Visible = True
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        GetDisputeNumber()
        lblError.Text = ""
        'validate correct amount
        If Val.CheckDecimal(txtDisputeAmount.Text) = -1 Then
            lblError.Text = "Please enter a positive, numeric correct amount"
            Exit Sub
        End If
        'validate delete or not
        Dim strDelete As String
        If cblDeleteTransaction.Items(0).Selected = True Then
            strDelete = "Yes"
        ElseIf cblDeleteTransaction.Items(1).Selected = True Then
            strDelete = "No"
        Else
            lblError.Text = "Please select either yes or no on whether or not you would like to apply to delete this transaction"
            Exit Sub
        End If

        DBDispute.AddDispute(CInt(Session("DisputeNumber")), CInt(Session("CustomerNumber")), txtDisputeComments.Text, CDec(txtDisputeAmount.Text), strDelete, "Submitted", 0, "NA")
        lblError.Text = "Dispute Submitted"
        Response.AddHeader("Refresh", "2; URL= CustomerTransactionDetail.aspx")
    End Sub
End Class