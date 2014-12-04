﻿Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBDisputeManager
    'Declare module-level variables
    Dim mDatasetDispute As New DataSet
    Dim mDatasetDispute2 As New DataSet
    Dim mDatasetDispute3 As New DataSet
    Dim mDatasetDispute4 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mMyView3 As New DataView
    Dim mMyView4 As New DataView

    'Define a public read-only property so "outsiders" can access the dataset filled by this class
    Public ReadOnly Property DisputeDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDispute
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property DisputeDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDispute2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

    Public ReadOnly Property DisputeDataset3() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDispute3
        End Get
    End Property
    Public ReadOnly Property MyView3() As DataView
        Get
            Return mMyView3
        End Get
    End Property
    Public ReadOnly Property DisputeDataset4() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDispute4
        End Get
    End Property
    Public ReadOnly Property MyView4() As DataView
        Get
            Return mMyView4
        End Get
    End Property

    Public Sub RunProcedureNoParam(ByVal strProcedureName As String)
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetDispute.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDispute, "tblDispute")
            'copy dataset to dataview
            mMyView.Table = mDatasetDispute.Tables("tblDispute")
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
            mDatasetDispute.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDispute, "tblDispute")
            'copy dataset to dataview
            mMyView.Table = mDatasetDispute.Tables("tblDispute")
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
            mDatasetDispute3.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDispute3, "tblAccounts")
            'copy dataset to dataview
            mMyView3.Table = mDatasetDispute3.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameter3(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
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
            mDatasetDispute4.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDispute4, "tblAccounts")
            'copy dataset to dataview
            mMyView4.Table = mDatasetDispute4.Tables("tblAccounts")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

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
    Public Sub AddDispute(intDisputeID As Integer, intCustomerNumber As Integer, strCustomerComment As String, decCorrectAmount As Decimal, strDelete As String, strStatus As String, intEmpID As Integer, strManagerComment As String, intTransactionNumber As Integer)
        mstrQuery = "INSERT INTO tblDispute (DisputeID, CustomerID, CustomerComment, CorrectAmount, DeleteTransaction, Status, EmployeeID, ManagerComment, TransactionNumber) VALUES (" & _
            "'" & intDisputeID & "', " & _
            "'" & intCustomerNumber & "', " & _
            "'" & strCustomerComment & "', " & _
            "'" & decCorrectAmount & "', " & _
            "'" & strDelete & "', " & _
            "'" & strStatus & "', " & _
            "'" & intEmpID & "', " & _
            "'" & strManagerComment & "', " & _
            "'" & intTransactionNumber & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)
    End Sub

    Public Sub ModifyDisputeAmount(strUpdatedAmount As String, strTransactionID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblTransactions SET TransactionAmount= '" & strUpdatedAmount & _
            "'where TransactionNumber = " & strTransactionID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyStatusResolved(strStatus As String, strDisputeID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblDispute SET Status= '" & strStatus & _
            "' where DisputeID = " & strDisputeID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub
    Public Sub ModifyManagerComment(strComment As String, strDisputeID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblDispute SET ManagerComment= '" & strComment & _
            "' where DisputeID = " & strDisputeID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyManagerID(strEmployeeID As String, strDisputeID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblDispute SET EmployeeID= '" & strEmployeeID & _
            "' where DisputeID = " & strDisputeID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyTransactionDescription(strDescription As String, strTransactionID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblTransactions SET Description= '" & strDescription & _
            "' where TransactionNumber = " & strTransactionID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyAccountBalances(strNewAccountBalance As String, strNewAvailableBalance As String, strAccountNumber As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblAccounts SET Balance= '" & strNewAccountBalance & "', AvailableBalance= '" & strNewAvailableBalance & _
            "' where AccountNumber = " & strAccountNumber

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetAllUnresolvedDispute()
        RunProcedureNoParam("usp_dispute_get_unresolved")
    End Sub

    Public Sub GetAllDispute()
        RunProcedureNoParam("usp_dispute_get_all")
    End Sub

    Public Sub GetAllResolvedDispute()
        RunProcedureNoParam("usp_dispute_get_resolved")
    End Sub

    Public Sub GetByDisputeID(strDisputeID As String)
        RunProcedureOneParameter("usp_dispute_get_by_disputeID", "@disputeID", strDisputeID)
    End Sub

    Public Sub GetAccountByAccountNumber(strAccountNumber As String)
        RunProcedureOneParameter2("usp_accounts_get_by_account_Number", "@accountNumber", strAccountNumber)
    End Sub

    Public Sub GetAccountByAccountNumber2(strAccountNumber As String)
        RunProcedureOneParameter3("usp_accounts_get_by_account_Number", "@accountNumber", strAccountNumber)
    End Sub


End Class

