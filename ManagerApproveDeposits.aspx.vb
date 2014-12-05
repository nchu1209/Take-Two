
Imports System.Net.Mail

Public Class ManagerApproveDeposits
    Inherits System.Web.UI.Page

    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccount As New ClassDBAccounts
    Dim DBDate As New ClassDBDate
    Dim DBCustomer As New ClassDBCustomer
    Dim mstrEmail As String
    Dim mstrFirst As String
    Dim mstrLast As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            If Session("EmployeeFirstName") Is Nothing Then
                Response.Redirect("EmployeeLogin.aspx")
            End If

            DBTransactions.GetDetailsByManagerApprovalNeeded("Needed")

            gvTransactionsAwaitingManagerApproval.DataSource = DBTransactions.MyView
            gvTransactionsAwaitingManagerApproval.DataBind()
        End If
    End Sub

    Protected Sub gvTransactionsAwaitingManagerApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTransactionsAwaitingManagerApproval.SelectedIndexChanged
        Dim strAccountNumber As String = gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(1).Text
        Dim strTransactionNumber As String = gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(2).Text
        Dim decDepositAmt As Decimal = CDec(gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(5).Text)
        Dim datDate As Date = CDate(gvTransactionsAwaitingManagerApproval.SelectedRow.Cells(6).Text)
        DBTransactions.ModifyTransactionManagerApproved("Approved", strTransactionNumber)
        'always change to approved
        'if date is today, do the stuff already built below
        'if date is not today, pass into manager set date, build functionality there wiht the stuff below for transactions if ManagerApproved = "Approved"
        DBDate.GetDate()
        If DBDate.CheckSelectedDate(datDate) = 0 Or DBDate.CheckSelectedDate(datDate) = -1 Then
            DBAccount.GetBalanceByAccountNumber(strAccountNumber)
            Dim decBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance")
            Dim decAvailableBalance As Decimal = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("AvailableBalance")

            decBalance += decDepositAmt
            decAvailableBalance += decDepositAmt

            DBAccount.UpdateBalance(CInt(strAccountNumber), decBalance)
            DBAccount.UpdateAvailableBalance(CInt(strAccountNumber), decAvailableBalance)


            ''''''''''''''''''''''''''EMAIL'''''''''''''''''''''''''''''''''

            Dim strCustomerID As String = DBAccount.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("CustomerID")
            DBCustomer.GetByCustomerNumber(strCustomerID)
            'DBCustomer.GetByCustomerNumber(Session("CustomerIDForDispute").ToString)
            mstrEmail = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("EmailAddr").ToString
            mstrFirst = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("FirstName").ToString
            mstrLast = DBCustomer.CustDataset.Tables("tblCustomers").Rows(0).Item("LastName").ToString
            Dim strName As String
            strName = mstrFirst + " " + mstrLast
            'strName = mstrFirst + mstrLast
            Dim Msg As MailMessage = New MailMessage()
            Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
            Msg.From = New MailAddress("longhornbankingteam3@gmail.com", "Team 3")
            Msg.To.Add(New MailAddress(mstrEmail, strName))
            'Msg.To.Add(New MailAddress("leah.carroll@live.com", strName))
            Msg.IsBodyHtml = CBool("False")
            Msg.Body = "Hello" + strName + "!" & vbCrLf & "Your deposit has been approved by the manager." & vbCrLf & "Best regards," & vbCrLf & "Longhorn Bank Team 3"
            Msg.Subject = "Team 3:  Deposit Approved"
            MailObj.Send(Msg)
            Msg.To.Clear()

            '''''''''''''''''''''''''''EMAIL''''''''''''''''''''''''''''''''''''


            Response.Redirect("ManagerApproveDeposits.aspx")
        End If
    End Sub
End Class