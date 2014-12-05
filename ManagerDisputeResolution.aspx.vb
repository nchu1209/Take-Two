﻿Option Strict On

Imports System.Net.Mail


Public Class ManagerDisputeResolution
    Inherits System.Web.UI.Page
    Dim DBDisputes As New ClassDBDisputeManager
    Dim DBTransactions As New ClassDBTransactions
    Dim mstrDisputeID As String
    Dim mstrDescription As String
    Dim mstrUpdatedDescription As String
    Dim Valid As New ClassValidate
    Dim mdecDifference As Decimal
    Dim mstrAccountNumber As String
    Dim mstrAccountNumber1 As String
    Dim mstrAccountNumber2 As String
    Dim DBCustomer As New ClassDBCustomer
    Dim mstrEmail As String
    Dim mstrFirst As String
    Dim mstrLast As String
    Dim mManagerMessageforDispute As String
    Dim mCustomerIDforDispute As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists page 60
        If Session("DisputeID") Is Nothing Then
            Response.Redirect("ManagerHome.aspx")
        End If


        If IsPostBack = False Then
            FillTextboxes()
            If Session("UpdatedStatus").ToString <> "Submitted" Then
                Panel1.Visible = False
                lblNoDispute.Text = "You have already resolved this dispute."
                Response.AddHeader("Refresh", "3; URL=ManagerResolveDisputes.aspx")
                Exit Sub
            End If


        End If

        mstrDisputeID = Session("DisputeID").ToString


        lblError.Text = ""
    End Sub

    Public Sub FillTextboxes()
        'put info from selected customer into text boxes on form

        DBDisputes.GetByDisputeID(Session("DisputeID").ToString)


        txtDisputeNumber.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("DisputeID").ToString
        txtComments.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CustomerComment").ToString
        txtActual.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionAmount").ToString
        txtClaim.Text = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CorrectAmount").ToString
        mCustomerIDforDispute = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CustomerID").ToString
        Session("CustomerIDforDispute") = mCustomerIDforDispute
        mManagerMessageforDispute = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("ManagerComment").ToString
        lblError.Text = mManagerMessageforDispute.ToString
        Session("ManagerMessageforDispute") = mManagerMessageforDispute
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

        mdecDifference = CDec(DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionAmount")) - CDec(DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("CorrectAmount"))
        Dim strTransactionType As String
        strTransactionType = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionType").ToString

        Dim strTransactionTypeSecret As String
        strTransactionTypeSecret = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionTypeSecretHiddenColumn").ToString

        'HERE IS WHERE WE HAVE OUR TRANSFER CODE
        If strTransactionType = "Transfer" Then
            DBTransactions.GetTransactionsByTransactionNumber(DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)

            mstrAccountNumber1 = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString
            DBDisputes.GetAccountByAccountNumber(mstrAccountNumber1)

            mstrAccountNumber2 = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(1).Item("AccountNumber").ToString
            DBDisputes.GetAccountByAccountNumber2(mstrAccountNumber2)

            Dim strTransactionTypeSecret1 As String
            strTransactionTypeSecret1 = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionTypeSecretHiddenColumn").ToString

            Dim strTransactionTypeSecret2 As String
            strTransactionTypeSecret2 = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(1).Item("TransactionTypeSecretHiddenColumn").ToString

            'for the first transaction
            If ddlAction.SelectedIndex = 0 Then
                lblError.Text = "Please pick an action."
                Exit Sub
            End If

            If ddlAction.SelectedIndex = 1 Then
                DBDisputes.ModifyDisputeAmount(txtClaim.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Accepted", txtDisputeNumber.Text)

                If strTransactionTypeSecret1 = "Deposit" Or strTransactionTypeSecret1 = "Transfer To" Then
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")) - mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) - mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString())
                Else
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")) + mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString())
                End If
            End If

            If ddlAction.SelectedIndex = 2 Then
                DBDisputes.ModifyStatusResolved("Rejected", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Rejected"
                lblError.Text = "The request has been rejected."
                UpdateEmpID()
            End If

            If ddlAction.SelectedIndex = 3 Then
                If Valid.CheckDecimal(txtAdjusted.Text) = -1 Then
                    lblError.Text = "Please enter a valid adjusted amount."
                    Exit Sub
                End If
                DBDisputes.ModifyDisputeAmount(txtAdjusted.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Adjusted", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Adjusted"
                UpdateEmpID()
                lblError.Text = "The transaction amount has been updated."
            End If

            If txtComment.Text <> "" Then
                DBDisputes.ModifyManagerComment(txtComment.Text, txtDisputeNumber.Text)
            End If

            'for the second transaction
            If ddlAction.SelectedIndex = 0 Then
                lblError.Text = "Please pick an action."
                Exit Sub
            End If

            If ddlAction.SelectedIndex = 1 Then
                DBDisputes.ModifyDisputeAmount(txtClaim.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Accepted", txtDisputeNumber.Text)

                If strTransactionTypeSecret2 = "Deposit" Or strTransactionTypeSecret2 = "Transfer To" Then
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset4.Tables("tblAccounts").Rows(0).Item("Balance")) - mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset4.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) - mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(1).Item("AccountNumber").ToString())
                Else
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset4.Tables("tblAccounts").Rows(0).Item("Balance")) + mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset4.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(1).Item("AccountNumber").ToString())
                End If

                Session("UpdatedStatus") = "Accepted"
                UpdateEmpID()
                lblError.Text = "The transaction amount has been updated."
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If ddlAction.SelectedIndex = 2 Then
                DBDisputes.ModifyStatusResolved("Rejected", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Rejected"
                lblError.Text = "The request has been rejected."
                UpdateEmpID()
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If ddlAction.SelectedIndex = 3 Then
                If Valid.CheckDecimal(txtAdjusted.Text) = -1 Then
                    lblError.Text = "Please enter a valid adjusted amount."
                    Exit Sub
                End If
                DBDisputes.ModifyDisputeAmount(txtAdjusted.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Adjusted", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Adjusted"
                UpdateEmpID()
                lblError.Text = "The transaction amount has been updated."
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If txtComment.Text <> "" Then
                DBDisputes.ModifyManagerComment(txtComment.Text, txtDisputeNumber.Text)
            End If
        Else
            mstrAccountNumber = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString
            DBDisputes.GetAccountByAccountNumber(mstrAccountNumber)

            If ddlAction.SelectedIndex = 0 Then
                lblError.Text = "Please pick an action."
                Exit Sub
            End If

            If ddlAction.SelectedIndex = 1 Then
                DBDisputes.ModifyDisputeAmount(txtClaim.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Accepted", txtDisputeNumber.Text)

                If strTransactionType = "Deposit" Or strTransactionType = "Transfer To" Then
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")) - mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) - mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString())
                Else
                    DBDisputes.ModifyAccountBalances((CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("Balance")) + mdecDifference).ToString, (CInt(DBDisputes.DisputeDataset3.Tables("tblAccounts").Rows(0).Item("AvailableBalance")) + mdecDifference).ToString, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("AccountNumber").ToString())
                End If

                Session("UpdatedStatus") = "Accepted"
                UpdateEmpID()
                lblError.Text = "The transaction amount has been updated."
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If ddlAction.SelectedIndex = 2 Then
                DBDisputes.ModifyStatusResolved("Rejected", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Rejected"
                lblError.Text = "The request has been rejected."

                DBCustomer.GetAllCustomers()
                UpdateEmpID()
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If ddlAction.SelectedIndex = 3 Then
                If Valid.CheckDecimal(txtAdjusted.Text) = -1 Then
                    lblError.Text = "Please enter a valid adjusted amount."
                    Exit Sub
                End If
                DBDisputes.ModifyDisputeAmount(txtAdjusted.Text, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString)
                DBDisputes.ModifyStatusResolved("Adjusted", txtDisputeNumber.Text)
                Session("UpdatedStatus") = "Adjusted"
                UpdateEmpID()
                lblError.Text = "The transaction amount has been updated."
                Response.AddHeader("Refresh", "2; URL=ManagerResolveDisputes.aspx")
            End If

            If txtComment.Text <> "" Then
                DBDisputes.ModifyManagerComment(txtComment.Text, txtDisputeNumber.Text)
            End If
        End If
    End Sub

    Protected Sub UpdateEmpID()
        DBDisputes.ModifyManagerID(Session("EmpID").ToString, txtDisputeNumber.Text)
        DBDisputes.GetByDisputeID(mstrDisputeID)
        mstrDescription = DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("Description").ToString()
        mstrUpdatedDescription = "Dispute " & Session("UpdatedStatus").ToString & ": " + mstrDescription
        DBDisputes.ModifyTransactionDescription(mstrUpdatedDescription, DBDisputes.DisputeDataset.Tables("tblDispute").Rows(0).Item("TransactionNumber").ToString())


        ''''''''''''''''''''''''''EMAIL'''''''''''''''''''''''''''''''''


        DBCustomer.GetByCustomerNumber(Session("CustomerIDForDispute").ToString)
        mstrEmail = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("EmailAddr").ToString
        mstrFirst = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("FirstName").ToString
        mstrLast = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("LastName").ToString
        Dim strName As String
        strName = mstrFirst + " " + mstrLast
        Dim Msg As MailMessage = New MailMessage()
        Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
        Msg.From = New MailAddress("longhornbankingteam3@gmail.com", "Team 3")
        'Msg.To.Add(New MailAddress(mstrEmail, strName))
        Msg.To.Add(New MailAddress("leah.carroll@live.com", strName))
        Msg.IsBodyHtml = CBool("False")
        If mManagerMessageforDispute = "" Or mManagerMessageforDispute = " " Then
            Msg.Body = "Hello " + strName + "!" & vbCrLf & " Your dispute has been " + Session("UpdatedStatus").ToString + ".  The manager that handled this had no comment to make about the dispute. " & vbCrLf & "Best regards," & vbCrLf & "Longhorn Bank Team 3"
        Else
            Msg.Body = "Hello " + strName + "!" & vbCrLf & " Your dispute has been " + Session("UpdatedStatus").ToString + ".  The manager that handled your account said: " & Session("ManagerMessageforDispute").ToString & ". " & vbCrLf & " Best regards," & vbCrLf & " Longhorn Bank Team 3"
        End If
        Msg.Subject = "Team 3:  Transaction Dispute Resolution"
        MailObj.Send(Msg)
        Msg.To.Clear()

        '''''''''''''''''''''''''''EMAIL''''''''''''''''''''''''''''''''''''



    End Sub
End Class