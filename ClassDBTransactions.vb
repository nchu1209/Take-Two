Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBTransactions
    Dim mDatasetTransactions As New DataSet
    Dim mDatasetTransactions2 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView

    Public ReadOnly Property TransactionsDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetTransactions
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property
    Public ReadOnly Property TransactionsDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetTransactions2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
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
            mDatasetTransactions.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetTransactions, "tblTransactions")
            'copy dataset to dataview
            mMyView.Table = mDatasetTransactions.Tables("tblTransactions")
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
            mDatasetTransactions.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetTransactions, "tblTransactions")
            'copy dataset to dataview
            mMyView.Table = mDatasetTransactions.Tables("tblTransactions")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub
    Public Sub RunProcedureOneParameter2(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
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
            mDatasetTransactions2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetTransactions2, "tblTransactions")
            'copy dataset to dataview
            mMyView2.Table = mDatasetTransactions2.Tables("tblTransactions")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureAnyParam(ByVal strUSPName As String, ByVal strDatasetName As DataSet, ByVal strViewName As DataView, ByVal strTableName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        Dim objConnection As New SqlConnection(mstrConnection)
        Dim mdbDataAdapter As New SqlDataAdapter(strUSPName, objConnection)
        Try
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(CStr(aryParamNames(index)), CStr(aryParamValues(index))))
                index = index + 1
            Next
            strDatasetName.Clear()
            mdbDataAdapter.Fill(strDatasetName, strTableName)
            strViewName.Table = strDatasetName.Tables(strTableName)
        Catch ex As Exception
            Dim strError As String = ""
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                strError = strError & "ParamName: " & CStr(aryParamNames(index))
                strError = strError & "ParamValue: " & CStr(aryParamValues(index))
                index += 1
            Next
            Throw New Exception(strError & " error message is " & ex.Message)
        End Try
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
            mDatasetTransactions.Clear()

            'fill the dataset
            mdbDataAdapter.Fill(mDatasetTransactions, "tblTransactions")

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub GetMaxTransactionNumber()
        RunProcedureNoParam("usp_transactions_get_max_transaction_number")
    End Sub

    Public Sub AddTransaction(intTransactionNumber As Integer, ByVal intAccountNumber As Integer, strTransactionType As String, strDate As String, decTransactionAmount As Decimal, strDescription As String, decAccountBalance As Decimal, strBillID As String, strIRA As String, decAvailableBalance As Decimal)

        mstrQuery = "INSERT INTO tblTransactions (TransactionNumber, AccountNumber, TransactionType, Date, TransactionAmount, Description, AccountBalance, BillID, IRA, AvailableBalance) VALUES (" & _
            "'" & intTransactionNumber & "', " & _
            "'" & intAccountNumber & "', " & _
            "'" & strTransactionType & "', " & _
            "'" & strDate & "', " & _
            "'" & decTransactionAmount & "', " & _
            "'" & strDescription & "', " & _
            "'" & decAccountBalance & "', " & _
            "'" & strBillID & "', " & _
            "'" & strIRA & "', " & _
            "'" & decAvailableBalance & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)
    End Sub
    Public Sub DoSort()
        MyView.Sort = "[Transaction Number], [Transaction Type], Description, Amount, Date"
    End Sub

    Public Sub DoDateSort()
        MyView.Sort = "Date, [Transaction Number]"
    End Sub

    Public Sub GetAllTransactions(strAccountNumber As String)
        RunProcedureOneParameter("usp_transactions_get_all", "@AccountNumber", strAccountNumber)
    End Sub

    Public Sub GetDetails(strTransactionNumber As String)
        RunProcedureOneParameter("usp_transactions_get_details_by_transaction_number", "@transactionnumber", strTransactionNumber)
    End Sub

    Public Sub TransactionsByAccount(strAccountNumber As String)
        RunProcedureOneParameter2("usp_transactions_get_by_account_number", "@accountnumber", strAccountNumber)
    End Sub
    'right now this is pulling from tbltransactions as a whole. want to limit the data set to just this account.
    Public Sub GetFiveSimilar(strTransactionType As String)
        RunProcedureOneParameter2("usp_transactions_get_five_similar", "@transactiontype", strTransactionType)
    End Sub

    Public Sub Go(strIn1 As String, strIn2 As String, strIn3 As String, strIn4 As String, strIn5 As String)
        MyView.RowFilter = strIn1 & strIn2 & strIn3 & strIn4 & strIn5
    End Sub

    Public Sub GetFiveSimilarByAccount(strAccountNumber As String, strTransactionType As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList
        aryNames.Add("@AccountNumber")
        aryNames.Add("@TransactionType")
        aryValues.Add(strAccountNumber)
        aryValues.Add(strTransactionType)
        RunProcedureAnyParam("usp_transactions_get_five_similar_by_account", TransactionsDataset2, mMyView2, "tblTransactions", aryNames, aryValues)
    End Sub
End Class
