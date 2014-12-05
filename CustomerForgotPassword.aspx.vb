Option Strict On

Imports System.Net.Mail

Public Class CustomerForgotPassword
    Inherits System.Web.UI.Page


    Dim DB As New ClassDBCustomer
    Dim mstrFirst As String
    Dim mstrLast As String
    Dim mstrEmail As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("ForgotPassword") Is Nothing Then
        '    Response.Redirect("CustomerLogin.aspx")
        'End If
    End Sub



    Protected Sub btnRequest_Click(sender As Object, e As EventArgs) Handles btnRequest.Click
        If DB.CheckEmail(txtEmail.Text) = False Then
            'empID is invalid, return error
            lblMessage.Text = "ERROR: Email address does not exist in database."
            Exit Sub
        End If


        'check password
        If DB.CheckBirthYear(txtEmail.Text, txtBirthYear.Text) = False Then
            'invalid password, return error
            lblMessage.Text = "ERROR: Invalid password."
            Exit Sub
        End If

        lblMessage.Text = "An email has been sent to your account with your password."

        Dim strPassword As String
        strPassword = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("Password").ToString




        ''''''''''''''''''''''''''EMAIL'''''''''''''''''''''''''''''''''

        mstrEmail = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("EmailAddr").ToString
        mstrFirst = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("FirstName").ToString
        mstrLast = DB.CustDataset.Tables("tblCustomers").Rows(0).Item("LastName").ToString
        Dim strName As String
        strName = mstrFirst + " " + mstrLast
        Dim Msg As MailMessage = New MailMessage()
        Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
        Msg.From = New MailAddress("longhornbankingteam3@gmail.com", "Team 3")
        Msg.To.Add(New MailAddress(mstrEmail, strName))
        'Msg.To.Add(New MailAddress("leah.carroll@live.com", strName))
        Msg.IsBodyHtml = CBool("False")
        Msg.Body = "Hello " + strName + "!" & vbCrLf & "Your password is: " + strPassword + " . " & vbCrLf & "Best regards," & vbCrLf & "Longhorn Bank Team 3"
        Msg.Subject = "Team 3: Password"
        MailObj.Send(Msg)
        Msg.To.Clear()

        '''''''''''''''''''''''''''EMAIL''''''''''''''''''''''''''''''''''''


        Response.AddHeader("Refresh", "2; URL= CustomerLogin.aspx")

    End Sub



End Class