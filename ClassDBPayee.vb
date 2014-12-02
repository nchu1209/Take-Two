Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBPayee

    'Declare module-level variables
    Dim mDatasetPayee As New DataSet
    Dim mDatasetPayee2 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView

    'Define a public read-only property so "outsiders" can access the dataset filled by this class
    Public ReadOnly Property PayeeDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetPayee
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property


    Public ReadOnly Property PayeeDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetPayee2
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
            mDatasetPayee.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPayee, "tblPayees")
            'copy dataset to dataview
            mMyView.Table = mDatasetPayee.Tables("tblPayees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureTwoParameter(ByVal strProcedureName As String, ByVal strParameterName1 As String, ByVal strParameterValue1 As String, ByVal strParameterName2 As String, ByVal strParameterValue2 As String)
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName1, strParameterValue1))
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName2, strParameterValue2))
            'clear dataset
            mDatasetPayee.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPayee, "tblCustomersPayees")
            'copy dataset to dataview
            mMyView.Table = mDatasetPayee.Tables("tblCustomersPayees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName1.ToString & strParameterValue1.ToString & strParameterName2.ToString & strParameterValue2.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedurePayeeID(ByVal strProcedureName As String)
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            mDatasetPayee2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPayee2, "tblPayees")
            'copy dataset to dataview
            mMyView2.Table = mDatasetPayee2.Tables("tblPayees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureOneParameter(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)

        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetPayee.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPayee, "tblPayees")
            'copy dataset to dataview
            mMyView.Table = mDatasetPayee.Tables("tblPayees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedurePayeeCustomers(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)

        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetPayee2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPayee2, "tblPayees")
            'copy dataset to dataview
            mMyView2.Table = mDatasetPayee2.Tables("tblPayees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub UpdateDB(ByVal mstrQuery As String)
       
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

    Public Sub LinkZip(ByVal strPayeeID As String)
        RunProcedureOneParameter("usp_innerjoin_payee_city_by_zip", "@PayeeID", strPayeeID)
    End Sub

    Public Sub GetAllPayees()
        RunProcedureNoParam("usp_payees_get_all")
    End Sub

    Public Sub AddExistingPayee(ByVal strCustomerNumber As String, ByVal strPayeeID As String)
        RunProcedureTwoParameter("usp_customerspayees_add_payee", "@customernumber", strCustomerNumber, "@PayeeID", strPayeeID)
    End Sub

    Public Function CheckDuplicate(ByVal strCustomerNumber As String, ByVal strPayeeID As String) As Boolean
        RunProcedureTwoParameter("usp_customerspayees_check_duplicates", "@CustomerNumber", strCustomerNumber, "@payeeID", strPayeeID)

        If mDatasetPayee.Tables("tblCustomersPayees").Rows.Count <> 0 Then
            Return True
        End If

        Return False
    End Function

    Public Sub CreatePayee(strPayeeID As String, strPayeeName As String, strPayeeType As String, strAddress As String, strZip As String, strPhone As String)
        mstrQuery = "INSERT INTO tblPayees (PayeeID, PayeeName, PayeeType, Address, ZipCode, Phone) VALUES (" & _
            "'" & strPayeeID & "', " & _
            "'" & strPayeeName & "', " & _
            "'" & strPayeeType & "', " & _
            "'" & strAddress & "', " & _
            "'" & strZip & "', " & _
            "'" & strPhone & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)
    End Sub

    Public Sub UpdatePayee(strPayeeID As String, strPayeeName As String, strPayeeType As String, strAddress As String, strZip As String, strPhone As String)
        mstrQuery = "UPDATE tblPayees SET " & _
       "PayeeName = '" & strPayeeName & "', " & _
       "PayeeType = '" & strPayeeType & "', " & _
       "Address = '" & strAddress & "', " & _
       "ZipCode = '" & strZip & "', " & _
       "Phone = '" & strPhone & "' " & _
       "WHERE PayeeID = " & strPayeeID

        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetMaxPayeeID()
        RunProcedurePayeeID("usp_payees_max_PayeeID")
    End Sub

    Public Sub GetCustomerPayees(ByVal strCustomerNumber As String)
        RunProcedureOneParameter("usp_innerjoin_customerspayees_payees_by_customernumber", "@customernumber", strCustomerNumber)
    End Sub

    Public Sub GetPayeeByID(ByVal strPayeeID As String)
        RunProcedureOneParameter("usp_payees_get_by_payeeID", "@payeeID", strPayeeID)
    End Sub

    Public Sub GetPayeeCustomers(ByVal strPayeeID As String)
        RunProcedurePayeeCustomers("usp_innerjoin_customerspayees_customers_by_payeeID", "@PayeeID", strPayeeID)
    End Sub

End Class
