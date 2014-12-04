Public Class CustomerCreateBankAccount
    Inherits System.Web.UI.Page

    Dim DB As New ClassDBAccounts
    Dim Valid As New ClassValidate
    Dim DBCustomer As New ClassDBCustomer
    Dim DBDate As New ClassDBDate
    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccounts As New ClassDBAccounts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtAccountName.Visible = True
        txtAccountNumber.Visible = True
        txtInitialDeposit.Visible = True
        btnApply.Visible = True
        Label1.Visible = True
        Label2.Visible = True
        Label7.Visible = True
    End Sub

    Protected Sub ddlBankAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankAccounts.SelectedIndexChanged
        'reset error label
        lblError.Text = ""

        'find the maximum existing account number in the database, and add one to create the new account's number
        DB.GetMaxAccountNumber()
        If DB.AccountsDataset.Tables("tblAccounts").Rows(0).Item("MaxAccountNumber") Is DBNull.Value Then
            Session("AccountNumber") = 1000000000
        Else
            Session("AccountNumber") = DB.AccountsDataset.Tables("tblAccounts").Rows(0).Item("MaxAccountNumber") + 1
        End If

        txtAccountNumber.Text = Session("AccountNumber").ToString

        'set the name of the bank account; use defaults for checking and savings; user may change if desired
        'set account type

        If ddlBankAccounts.SelectedIndex = 1 Then
            txtAccountName.Text = "Longhorn Checking"
            Session("AccountType") = "Checking"
        End If

        If ddlBankAccounts.SelectedIndex = 2 Then
            txtAccountName.Text = "Longhorn Savings"
            Session("AccountType") = "Savings"
        End If

        DBCustomer.GetDOBByCustmomerNumber(Session("CustomerNumber"))
        DBDate.GetYear()
        If ddlBankAccounts.SelectedIndex = 3 Then
            DB.GetByCustomerNumberIRA(Session("CustomerNumber"))
            If DB.AccountsDataset3.Tables("tblAccounts").Rows.Count = 0 Then
                If CInt(DBDate.DateDataset2.Tables("tblSystemDate").Rows(0).Item("Date")) - CInt(DBCustomer.CustDataset2.Tables("tblCustomers").Rows(0).Item("DOB")) < 70 Then
                    txtAccountName.Text = ""
                    Session("AccountType") = "IRA"
                Else
                    lblError.Text = "You can only open an IRA if you are younger than 70"
                    txtAccountName.Visible = False
                    txtAccountNumber.Visible = False
                    txtInitialDeposit.Visible = False
                    btnApply.Visible = False
                    Label1.Visible = False
                    Label2.Visible = False
                    Label7.Visible = False
                    Exit Sub
                End If
            Else
                lblError.Text = "You cannot have more than one IRA. Please select another account type"
                txtAccountName.Visible = False
                txtAccountNumber.Visible = False
                txtInitialDeposit.Visible = False
                btnApply.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label7.Visible = False
            End If
        End If


        If ddlBankAccounts.SelectedIndex = 4 Then
            DB.GetByCustomerNumberStock(Session("CustomerNumber"))
            If DB.AccountsDataset3.Tables("tblAccounts").Rows.Count = 0 Then
                txtAccountName.Text = ""
                Session("AccountType") = "Stock"
            Else
                lblError.Text = "You cannot have more than one Stock Account. Please select another account type"
                txtAccountName.Visible = False
                txtAccountNumber.Visible = False
                txtInitialDeposit.Visible = False
                btnApply.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label7.Visible = False
            End If
        End If
    End Sub

    Public Sub GetTransactionNumber()
        DBTransactions.GetMaxTransactionNumber()
        If DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber") Is DBNull.Value Then
            Session("TransactionNumber") = 1
        Else
            Session("TransactionNumber") = CInt(DBTransactions.TransactionsDataset.Tables("tblTransactions").Rows(0).Item("MaxTransactionNumber")) + 1
        End If
    End Sub

    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        If Not IsValid Then
            Exit Sub
        End If

        'make sure deposit is numeric
        If Valid.CheckDigits(txtInitialDeposit.Text) = False Then
            lblError.Text = "Please enter a valid initial deposit"
            Exit Sub
        End If

        'make sure deposit is not negative
        If CInt(txtInitialDeposit.Text) <= 0 Then
            lblError.Text = "Please enter an initial deposit of more than $0"
            Exit Sub
        End If

        'if the account is an IRA, they cannot deposit more than 5k
        'but they are automatically stopped by the initial deposit validation below
        'but we need to make sure the manager cannot approve them -- they cannot enter that much regardless, so perhaps its better to stop them now
        If Session("AccountType").ToString = "IRA" Then
            If CInt(txtInitialDeposit.Text) > 5000 Then
                lblError.Text = "You cannot enter more than $5000 into an IRA account per year. Please select a lower initial deposit"
                Exit Sub
            End If
        End If

        ''right now this is here to trigger a label for the manager that the deposit needs to be verified -- once manager approves, 
        'set session to true, go into database and change account to active and approved deposit to true
        Session("Active") = "True"
        Session("ManagerApprovedDeposit") = "True"
        If CInt(txtInitialDeposit.Text) >= 5000 Then
            Session("Active") = "False"
            Session("ManagerApprovedDeposit") = "False"
        End If

        'if the account is a stock account, they automatically need manager approval
        If Session("AccountType").ToString = "Stock" Then
            Session("Active") = "False"
            Session("ManagerApprovedStock") = "False"
        End If

        'to add the account, we need: 
        'CustomerID ?!?! --> session variable from log on?
        'account number, account name, account type, active, manager approved deposit, initial, balance
        'automatically active or should we wait for the manager approved deposit? 
        'is the manager approved deposit false until the manager approves it? if the deposit is under 5k, should it automatically be true?
        If Session("AccountType") = "Checking" Or Session("AccountType") = "Savings" Then
            DB.AddAccountChecking(CInt(Session("CustomerNumber")), CInt(txtAccountNumber.Text), txtAccountName.Text, Session("AccountType").ToString, Session("Active").ToString, Session("ManagerApprovedDeposit").ToString, CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text))
        End If

        If Session("AccountType") = "IRA" Then
            DB.AddAccountIRA(CInt(Session("CustomerNumber")), CInt(txtAccountNumber.Text), txtAccountName.Text, Session("AccountType").ToString, Session("Active").ToString, Session("ManagerApprovedDeposit").ToString, CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text))
        End If

        If Session("AccountType") = "Stock" Then
            DB.AddAccountStock(CInt(Session("CustomerNumber")), CInt(txtAccountNumber.Text), txtAccountName.Text, Session("AccountType").ToString, Session("Active").ToString, Session("ManagerApprovedDeposit").ToString, CInt(txtInitialDeposit.Text), CInt(txtInitialDeposit.Text), Session("ManagerApprovedStock").ToString, CInt(txtInitialDeposit.Text))
        End If
        DBDate.GetDate()
        Dim strDate As String = DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date").ToString
        Dim strDescription As String = "Deposited " & txtInitialDeposit.Text & " to account " & txtAccountNumber.Text & " on " & strDate & " while opening the account"

        If Session("AccountType") = "IRA" Then
            DBTransactions.AddTransaction(Session("TransactionNumber"), CInt(txtAccountNumber.Text), "Deposit", strDate, CInt(txtInitialDeposit.Text), strDescription, CInt(txtInitialDeposit.Text), Nothing, "True", CInt(txtInitialDeposit.Text))
            DBAccounts.UpdateIRATotalDeposit(CInt(txtAccountNumber.Text), CDec(txtInitialDeposit.Text))
        Else
            DBTransactions.AddTransaction(Session("TransactionNumber"), CInt(txtAccountNumber.Text), "Deposit", strDate, CInt(txtInitialDeposit.Text), strDescription, CInt(txtInitialDeposit.Text), Nothing, "False", CInt(txtInitialDeposit.Text))
        End If
        
        'clear form once application is submitted and show message to customer. or redirect after lag????
        lblError.Text = "Application Submitted"
        Response.AddHeader("Refresh", "2; URL= CustomerHome.aspx")
    End Sub

    Protected Sub btnCancelProfile_Click(sender As Object, e As EventArgs) Handles btnCancelProfile.Click
        Response.Redirect("CustomerHome.aspx")
    End Sub
End Class