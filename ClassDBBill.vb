Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBBill
    Dim mDatasetBill As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView

    Public ReadOnly Property BillDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetBill
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
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
            mDatasetBill.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetBill, "tblBill")
            'copy dataset to dataview
            mMyView.Table = mDatasetBill.Tables("tblBill")
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
            mDatasetBill.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetBill, "tblBill")
            'copy dataset to dataview
            mMyView.Table = mDatasetBill.Tables("tblBill")
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
            mDatasetBill.Clear()

            'fill the dataset
            mdbDataAdapter.Fill(mDatasetBill, "tblBill")

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub AddBill(intBillID As Integer, strCustomerID As String, decBillAmount As Decimal, datBillDate As Date, datDueDate As Date, strPayeeID As String)

        mstrQuery = "INSERT INTO tblBill (BillID, CustomerID, BillAmount, BillDate, DueDate, PayeeID, AmountPaid, AmountRemaining, Status, Active) VALUES (" & _
            "'" & intBillID & "', " & _
            "'" & strCustomerID & "', " & _
            "'" & decBillAmount & "', " & _
            "'" & datBillDate & "', " & _
            "'" & datDueDate & "', " & _
            "'" & strPayeeID & "'," & _
            "0, " & decBillAmount & ", 'Unpaid', 'TRUE')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetMaxBillID()
        RunProcedureNoParam("usp_bill_get_max_billID")
    End Sub

    Public Sub GetAllBills()
        RunProcedureNoParam("usp_bill_get_all")
    End Sub

    Public Sub GetCustomerBills(strCustomerNumber As String)
        RunProcedureOneParameter("usp_bill_get_by_customernumber", "@customernumber", strCustomerNumber)
    End Sub

    Public Sub GetBillDetails(strBillID As String)
        RunProcedureOneParameter("usp_bill_get_by_billID", "@billID", strBillID)
    End Sub

    Public Sub ModifyBill(strBillAmount As String, strBillDate As String, strDueDate As String, strAmountPaid As String, strAmountRemaining As String, strStatus As String, strActive As String, ByVal strBillID As String)

        mstrQuery = "UPDATE tblBill SET " & _
            "BillAmount = '" & strBillAmount & "', " & _
            "BillDate = '" & strBillDate & "', " & _
            "DueDate = '" & strDueDate & "', " & _
            "AmountPaid = '" & strAmountPaid & "', " & _
            "AmountRemaining = '" & strAmountRemaining & "', " & _
            "Status = '" & strStatus & "', " & _
            "Active = '" & strActive & "' " & _
            "WHERE BillID = " & strBillID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyBillActive(strActive As String, ByVal strBillID As String)

        mstrQuery = "UPDATE tblBill SET " & _
            "Active = '" & strActive & "' " & _
            "WHERE BillID = " & strBillID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

End Class
