Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBAccounts
    'Declare module-level variables
    Dim mDatasetAccounts As New DataSet
    Dim mDatasetAccounts2 As New DataSet
    Dim mDatasetAccounts3 As New DataSet
    Dim mDatasetAccounts4 As New DataSet
    Dim mDatasetAccounts5 As New DataSet
    Dim mDatasetAccounts6 As New DataSet
    Dim mDatasetAccounts7 As New DataSet
    Dim mDatasetAccounts8 As New DataSet
    Dim mDatasetAccounts9 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mMyView3 As New DataView
    Dim mMyView4 As New DataView
    Dim mMyView5 As New DataView
    Dim mMyView6 As New DataView
    Dim mMyView7 As New DataView
    Dim mMyView8 As New DataView
    Dim mMyView9 As New DataView

    Public ReadOnly Property AccountsDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property


    Public ReadOnly Property AccountsDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

    Public ReadOnly Property AccountsDataset3() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts3
        End Get
    End Property
    Public ReadOnly Property MyView3() As DataView
        Get
            Return mMyView3
        End Get
    End Property

    Public ReadOnly Property AccountsDataset4() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts4
        End Get
    End Property
    Public ReadOnly Property MyView4() As DataView
        Get
            Return mMyView4
        End Get
    End Property

    Public ReadOnly Property AccountsDataset5() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts5
        End Get
    End Property
    Public ReadOnly Property MyView5() As DataView
        Get
            Return mMyView5
        End Get
    End Property
    Public ReadOnly Property AccountsDataset6() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts6
        End Get
    End Property
    Public ReadOnly Property MyView6() As DataView
        Get
            Return mMyView6
        End Get
    End Property
    Public ReadOnly Property AccountsDataset7() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts7
        End Get
    End Property
    Public ReadOnly Property MyView7() As DataView
        Get
            Return mMyView7
        End Get
    End Property
    Public ReadOnly Property AccountsDataset8() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts8
        End Get
    End Property
    Public ReadOnly Property MyView8() As DataView
        Get
            Return mMyView8
        End Get
    End Property
    Public ReadOnly Property AccountsDataset9() As DataSet
        Get
            'Return dataset to user
            Return mDatasetAccounts9
        End Get
    End Property
    Public ReadOnly Property MyView9() As DataView
        Get
            Return mMyView9
        End Get
    End Property

    Public Sub UpdateDB(ByVal mstrQuery As String)
        'Purpose: run given query to update database
        'Arguments: 1 string (any SQL statement)
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        Try
            'make connection using the connection string
            mdbConn = New SqlConnection(mstrConnection)
            Dim dbCommand As New SqlCommand(mstrQuery, mdbConn)

            'open the connection
            mdbConn.Open()

            'run query
            dbCommand.ExecuteNonQuery()

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & mstrQuery.ToString & " error is " & ex.Message)
        End Try

    End Sub

    Public Sub RunProcedureNoParam(ByVal strProcedureName As String)
        'Purpose: run any stored procedure with no parameters and fill dataset
        'Arguments: 1 string that contains procedure name
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetAccounts.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts, "tblAccounts")
            'copy dataset to dataview
            mMyView.Table = mDatasetAccounts.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub


    Public Sub RunProcedureCustomerNumber(ByVal strProcedureName As String)
        'Purpose: run any stored procedure with no parameters and fill dataset
        'Arguments: 1 string that contains procedure name
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetAccounts2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts2, "tblAccounts")
            'copy dataset to dataview
            mMyView2.Table = mDatasetAccounts2.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub



    Public Sub RunProcedureOneParameter(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts, "tblAccounts")
            'copy dataset to dataview
            mMyView.Table = mDatasetAccounts.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameterIRA(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts3.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts3, "tblAccounts")
            'copy dataset to dataview
            mMyView3.Table = mDatasetAccounts3.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameterDDL(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts4.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts4, "tblAccounts")
            'copy dataset to dataview
            mMyView4.Table = mDatasetAccounts4.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameterTransfer(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts5.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts5, "tblAccounts")
            'copy dataset to dataview
            mMyView5.Table = mDatasetAccounts5.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameterTransfer2(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts6.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts6, "tblAccounts")
            'copy dataset to dataview
            mMyView6.Table = mDatasetAccounts6.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub
    Public Sub RunProcedureOneParameterTransfer3(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts7.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts7, "tblAccounts")
            'copy dataset to dataview
            mMyView7.Table = mDatasetAccounts7.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameterBeatMorgan(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts8.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts8, "tblAccounts")
            'copy dataset to dataview
            mMyView8.Table = mDatasetAccounts8.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub
    Public Sub RunProcedureOneParameter9(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
        'Arguments: 3 strings
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 10/21/14
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetAccounts9.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetAccounts9, "tblAccounts")
            'copy dataset to dataview
            mMyView9.Table = mDatasetAccounts9.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub LinkZip(ByVal strCustomerID As String)
        RunProcedureOneParameter("usp_innerjoin_customer_city_by_zip", "@CustomerID", strCustomerID)
    End Sub

    Public Sub GetApprovedStatus(ByVal strCustomerID As String)
        RunProcedureOneParameter("usp_account_get_by_customer_id_and_stock", "@CustomerID", strCustomerID)
    End Sub

    Public Sub CustomerNumber()
        RunProcedureCustomerNumber("usp_customer_number")
    End Sub

    Public Sub GetByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_customers_get_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetByCustomerNumberIRA(strCustomerNumber As String)
        RunProcedureOneParameterIRA("usp_accounts_get_by_customernumber_IRA", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetByCustomerNumberStock(strCustomerNumber As String)
        RunProcedureOneParameterIRA("usp_accounts_get_by_customer_number_stock", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetAllAccounts()
        RunProcedureNoParam("usp_accounts_get_all")
    End Sub

    Public Sub GetMaxAccountNumber()
        RunProcedureNoParam("usp_accounts_get_max_account_number")
    End Sub

    Public Sub GetAccountByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameterDDL("usp_accounts_get_account_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetAccountByCustomerNumberTransfer(strCustomerNumber As String)
        RunProcedureOneParameterTransfer("usp_accounts_get_account_by_customer_number_for_ddl", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetAccountByCustomerNumberTransfer2(strCustomerNumber As String)
        RunProcedureOneParameterTransfer3("usp_accounts_get_account_by_customer_number_for_ddl", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetCheckingAccountByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_checking_account_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetSavingsAccountByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_savings_account_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetIRAAccountByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_IRA_account_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetStockAccountByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_stock_account_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetBalanceByAccountNumber(strAccountNumber As String)
        RunProcedureOneParameterTransfer2("usp_accounts_get_balance_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetBalanceByAccountNumber2(strAccountNumber As String)
        RunProcedureOneParameterTransfer("usp_accounts_get_balance_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetAccountNameByAccountNumber(strAccountNumber As String)
        RunProcedureOneParameterTransfer("usp_accounts_get_account_name_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetCheckingandSavingsByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_checking_and_savings_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetAccountTypeByAccountNumber(strAccountNumber As String)
        RunProcedureOneParameter9("usp_accounts_get_account_type_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetAccountTypeByAccountNumber2(strAccountNumber As String)
        RunProcedureOneParameterIRA("usp_accounts_get_account_type_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetIRATotalDepositByAccountNumber(strAccountNumber As String)
        RunProcedureOneParameterBeatMorgan("usp_accounts_get_IRA_totaldeposit_by_account_number", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetAccountByCustomerNumberForEmployeeTransactionSearch(strCustomerNumber As String)
        RunProcedureOneParameter("usp_accounts_get_account_by_customer_number_for_employee_transaction_search", "@CustomerNumber", strCustomerNumber)
    End Sub


    Public Sub GetAccountByCustomerNumberTransferPurchaseStocks(strCustomerNumber As String)
        RunProcedureOneParameterTransfer("usp_accounts_get_account_by_customer_number_for_ddl_purchase", "@CustomerNumber", strCustomerNumber)
    End Sub




    Public Sub AddAccountChecking(intCustomerID As Integer, ByVal intAccountNumber As Integer, ByVal strAccountName As String, ByVal strAccountType As String, ByVal strActive As String, ByVal strManagerApprovedDeposit As String, ByVal intInitial As Integer, ByVal intBalance As Integer)
        'Purpose: adds a customer to database
        'Arguments: 11 strings
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        mstrQuery = "INSERT INTO tblAccounts (CustomerID, AccountNumber, AccountName, AccountType, Active, ManagerApprovedDeposit, Initial, Balance) VALUES (" & _
            "'" & intCustomerID & "', " & _
            "'" & intAccountNumber & "', " & _
            "'" & strAccountName & "', " & _
            "'" & strAccountType & "', " & _
            "'" & strActive & "', " & _
            "'" & strManagerApprovedDeposit & "', " & _
            "'" & intInitial & "', " & _
            "'" & intBalance & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub AddAccountIRA(intCustomerID As Integer, ByVal intAccountNumber As Integer, ByVal strAccountName As String, ByVal strAccountType As String, ByVal strActive As String, ByVal strManagerApprovedDeposit As String, ByVal intInitial As Integer, ByVal intBalance As Integer, ByVal intIRATotal As Integer)
        'Purpose: adds a customer to database
        'Arguments: 11 strings
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        mstrQuery = "INSERT INTO tblAccounts (CustomerID, AccountNumber, AccountName, AccountType, Active, ManagerApprovedDeposit, Initial, Balance, IRATotalDeposit) VALUES (" & _
            "'" & intCustomerID & "', " & _
            "'" & intAccountNumber & "', " & _
            "'" & strAccountName & "', " & _
            "'" & strAccountType & "', " & _
            "'" & strActive & "', " & _
            "'" & strManagerApprovedDeposit & "', " & _
            "'" & intInitial & "', " & _
            "'" & intBalance & "', " & _
            "'" & intIRATotal & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub AddAccountStock(intCustomerID As Integer, ByVal intAccountNumber As Integer, ByVal strAccountName As String, ByVal strAccountType As String, ByVal strActive As String, ByVal strManagerApprovedDeposit As String, ByVal intInitial As Integer, ByVal intBalance As Integer, ByVal strManagerApprovedStockAccount As String)
        'Purpose: adds a customer to database
        'Arguments: 11 strings
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        mstrQuery = "INSERT INTO tblAccounts (CustomerID, AccountNumber, AccountName, AccountType, Active, ManagerApprovedDeposit, Initial, Balance, ManagerApprovedStockAccount) VALUES (" & _
            "'" & intCustomerID & "', " & _
            "'" & intAccountNumber & "', " & _
            "'" & strAccountName & "', " & _
            "'" & strAccountType & "', " & _
            "'" & strActive & "', " & _
            "'" & strManagerApprovedDeposit & "', " & _
            "'" & intInitial & "', " & _
            "'" & intBalance & "', " & _
            "'" & strManagerApprovedStockAccount & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub UpdateBalance(intAccountNumber As Integer, ByVal decBalance As Decimal)
        mstrQuery = "UPDATE tblAccounts SET Balance='" & decBalance & "' WHERE AccountNumber='" & intAccountNumber & "'"
        UpdateDB(mstrQuery)
    End Sub

    Public Sub UpdateIRATotalDeposit(intAccountNumber As Integer, ByVal decIRATotalDeposit As Decimal)
        mstrQuery = "UPDATE tblAccounts SET IRATotalDeposit='" & decIRATotalDeposit & "' WHERE AccountNumber='" & intAccountNumber & "'"
        UpdateDB(mstrQuery)
    End Sub

    Public Sub SelectQuery(ByVal strQuery As String)
        'Purpose: run any select query and fill dataset
        'Arguments: 1 string that contains query
        'Returns: none (query results via property)
        'Author: Nicole Chu (nc7997)
        'Date: 9/18/2014

        Try
            'define data connection and data adapter
            mdbConn = New SqlConnection(mstrConnection)
            mdbDataAdapter = New SqlDataAdapter(strQuery, mdbConn)

            'open the connection and dataset
            mdbConn.Open()

            'clear the dataset before filling
            mDatasetAccounts.Clear()

            'fill the dataset
            mdbDataAdapter.Fill(mDatasetAccounts, "tblAccounts")

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)
        End Try

    End Sub

    Public Sub ModifyAccountName(strChangeName As String, intAccountNumber As Integer)
        mstrQuery = "UPDATE tblAccounts SET " &
            "AccountName = '" & strChangeName & "' " & _
            "WHERE AccountNumber = " & intAccountNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)
    End Sub



End Class
