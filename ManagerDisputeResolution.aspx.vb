Option Strict On

Public Class ManagerDisputeResolution
    Inherits System.Web.UI.Page
    Dim DBDisputes As New ClassDBDisputeManager
    Dim mstrDisputeID As String
    Dim Valid As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mstrDisputeID = Session("DisputeID").ToString

        If IsPostBack = False Then
            FillTextboxes()



        End If

        lblError.Text = ""
    End Sub

    Public Sub FillTextboxes()
        'put info from selected customer into text boxes on form

        DBDisputes.GetByDisputeID(mstrDisputeID)

        txtDisputeNumber.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("DisputeID").ToString
        txtComments.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CustomerComment").ToString
        txtActual.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionAmount").ToString
        txtClaim.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CorrectAmount").ToString

    End Sub

    Protected Sub ddlAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAction.SelectedIndexChanged
        If ddlAction.SelectedIndex = 0 Then
            txtAdjusted.Text = ""
            txtAdjusted.ReadOnly = True
        End If

        If ddlAction.SelectedIndex = 1 Then
            txtAdjusted.Text = ""
            txtAdjusted.ReadOnly = True
        End If

        If ddlAction.SelectedIndex = 2 Then
            txtAdjusted.Text = ""
            txtAdjusted.ReadOnly = True
        End If

        If ddlAction.SelectedIndex = 3 Then
            txtAdjusted.ReadOnly = False
        End If
    End Sub


    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        DBDisputes.GetByDisputeID(mstrDisputeID)

        If ddlAction.SelectedIndex = 0 Then
            lblError.Text = "Please pick an action."
            Exit Sub
        End If

        If ddlAction.SelectedIndex = 1 Then
            DBDisputes.ModifyDisputeAmount(txtClaim.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
            DBDisputes.ModifyStatusResolved(txtDisputeNumber.Text)
            lblError.Text = "The transaction amount has been updated."
            Response.AddHeader("Refresh", "2; URL=ManagerReviewDispute.aspx")

            Exit Sub

        End If

        If ddlAction.SelectedIndex = 2 Then
            DBDisputes.ModifyStatusResolved(txtDisputeNumber.Text)
            lblError.Text = "The request has been rejected."
            Response.AddHeader("Refresh", "2; URL=ManagerReviewDispute.aspx")

            Exit Sub
        End If

        If ddlAction.SelectedIndex = 3 Then
            If Valid.CheckDecimal(txtAdjusted.Text) = -1 Then
                lblError.Text = "Please enter a valid adjusted amount."
                Exit Sub
            End If
            DBDisputes.ModifyDisputeAmount(txtAdjusted.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
            DBDisputes.ModifyStatusResolved(txtDisputeNumber.Text)
            lblError.Text = "The transaction amount has been updated."
            Response.AddHeader("Refresh", "2; URL=ManagerReviewDispute.aspx")

            Exit Sub
        End If
    End Sub
End Class