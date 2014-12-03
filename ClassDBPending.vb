Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBPending
    Dim mDatasetPending As New DataSet
    Dim mDatasetPending2 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView

    Public ReadOnly Property PendingDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetPending
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property PendingDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetPending2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

    Public Sub UpdateDB(ByVal mstrQuery As String)
        'Purpose: run given query to update database
        
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

        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetPending.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPending, "tblPending")
            'copy dataset to dataview
            mMyView.Table = mDatasetPending.Tables("tblPending")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureGetAll(ByVal strProcedureName As String)
        'Purpose: run any stored procedure with no parameters and fill dataset

        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetPending2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPending2, "tblPending")
            'copy dataset to dataview
            mMyView2.Table = mDatasetPending2.Tables("tblPending")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameter(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
        'Purpose: run any stored procedure with one parameter and fill dataset
       
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
            mDatasetPending.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPending, "tblPending")
            'copy dataset to dataview
            mMyView.Table = mDatasetPending.Tables("tblPending")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
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
            mDatasetPending.Clear()

            'fill the dataset
            mdbDataAdapter.Fill(mDatasetPending, "tblPending")

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub GetMaxTransactionNumber()
        RunProcedureNoParam("usp_transactions_get_max_transaction_number")
    End Sub

    Public Sub AddTransaction(intTransactionNumber As Integer, ByVal intAccountNumber As Integer, strTransactionType As String, strDate As String, decTransactionAmount As Decimal, strDescription As String, strBillID As String, strIRA As String, decAvailableBalance As Decimal)

        mstrQuery = "INSERT INTO tblPendingTransactions (TransactionNumber, AccountNumber, TransactionType, Date, TransactionAmount, Description, BillID, IRA, AvailableBalance) VALUES (" & _
            "'" & intTransactionNumber & "', " & _
            "'" & intAccountNumber & "', " & _
            "'" & strTransactionType & "', " & _
            "'" & strDate & "', " & _
            "'" & decTransactionAmount & "', " & _
            "'" & strDescription & "', " & _
            "'" & strBillID & "', " & _
            "'" & strIRA & "', " & _
            "'" & decAvailableBalance & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)
    End Sub

    Public Sub GetAllPendingTransactions()
        RunProcedureGetAll("usp_pending_get_all")
    End Sub

    Public Sub DeleteTransaction(intTransactionNumber As Integer)
        mstrQuery = "DELETE FROM tblPendingTransactions WHERE TransactionNumber = " & intTransactionNumber

        UpdateDB(mstrQuery)
    End Sub

End Class
