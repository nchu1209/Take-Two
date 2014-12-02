Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBDate

    'Declare module-level variables
    Dim mDatasetDate As New DataSet
    Dim mDatasetDate2 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView

    'Define a public read-only property so "outsiders" can access the dataset filled by this class
    Public ReadOnly Property DateDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDate
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property DateDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetDate2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

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
            mDatasetDate.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDate, "tblSystemDate")
            'copy dataset to dataview
            mMyView.Table = mDatasetDate.Tables("tblSystemDate")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureNoParam2(ByVal strProcedureName As String)
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
            mDatasetDate2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetDate2, "tblSystemDate")
            'copy dataset to dataview
            mMyView2.Table = mDatasetDate2.Tables("tblSystemDate")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
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

    Public Sub SetDate(ByVal strDate As String)

        mstrQuery = "UPDATE tblSystemDate SET " & _
            "Date = '" & strDate & "';"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetDate()

        RunProcedureNoParam("usp_systemdate_getdate")

    End Sub

    Public Sub GetYear()
        RunProcedureNoParam2("usp_systemdate_get_year")
    End Sub

    Public Function CheckSelectedDate(ByVal datSelected As Date) As Integer
        '-1 if past, 0 if today, 1 if future
        GetDate()

        Dim datSystemDate As Date
        datSystemDate = CDate(DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))

        If datSelected < datSystemDate Then
            Return -1
        ElseIf datSelected = datSystemDate Then
            Return 0
        Else : Return 1

        End If
    End Function

End Class

