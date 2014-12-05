Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBCustomer

    'Declare module-level variables
    Dim mDatasetCustomer As New DataSet
    Dim mDatasetCustomer2 As New DataSet
    Dim mDatasetCustomer3 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mMyView3 As New DataView

    'Define a public read-only property so "outsiders" can access the dataset filled by this class
    Public ReadOnly Property CustDataset() As DataSet
        Get
            'Return dataset to user
            Return mDatasetCustomer
        End Get
    End Property
    Public ReadOnly Property MyView() As DataView
        Get
            Return mMyView
        End Get
    End Property


    Public ReadOnly Property CustDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetCustomer2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

    Public ReadOnly Property CustDataset3() As DataSet
        Get
            'Return dataset to user
            Return mDatasetCustomer3
        End Get
    End Property
    Public ReadOnly Property MyView3() As DataView
        Get
            Return mMyView3
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
            mDatasetCustomer.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")
            'copy dataset to dataview
            mMyView.Table = mDatasetCustomer.Tables("tblCustomers")
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
            mDatasetCustomer2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetCustomer2, "tblCustomers")
            'copy dataset to dataview
            mMyView2.Table = mDatasetCustomer2.Tables("tblCustomers")
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
            mDatasetCustomer.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")
            'copy dataset to dataview
            mMyView.Table = mDatasetCustomer.Tables("tblCustomers")
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
            mDatasetCustomer2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetCustomer2, "tblCustomers")
            'copy dataset to dataview
            mMyView2.Table = mDatasetCustomer2.Tables("tblCustomers")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub LinkZip(ByVal strCustomerID As String)
        RunProcedureOneParameter("usp_innerjoin_customer_city_by_zip", "@CustomerID", strCustomerID)
    End Sub

    Public Sub CustomerNumber()
        RunProcedureCustomerNumber("usp_customer_number")
    End Sub

    Public Sub GetByCustomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter("usp_customers_get_by_customer_number", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetDOBByCustmomerNumber(strCustomerNumber As String)
        RunProcedureOneParameter2("usp_customers_get_DOB_by_customer_number", "@CustomerNumber", strCustomerNumber)
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
            mDatasetCustomer.Clear()

            'fill the dataset
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")

            'close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)
        End Try

    End Sub

    Public Function CheckEmail(ByVal strEmail As String) As Boolean
        'Purpose: checks if username is valid
        'Arguments: 1 string
        'Returns: True - if username is valid; False - if username is not valid
        'Author: Nicole Chu (nc7997)
        'Date: 9/18/2014

        'Use SelectQuery sub using strUsername
        mstrQuery = "SELECT * FROM tblCustomers WHERE EmailAddr='" & strEmail & "'"
        SelectQuery(mstrQuery)

        'Check to see how many records are in CustDataset
        If mDatasetCustomer.Tables("tblCustomers").Rows.Count = 1 Then
            'username is valid, so return true
            Return True
        Else : Return False 'because username is invalid
        End If

    End Function


    Public Function CheckEmailSP(strEmail As String) As Boolean
        RunProcedureOneParameter("usp_customers_get_email", "@Email", strEmail)

        If mDatasetCustomer.Tables("tblCustomers").Rows.Count <> 0 Then
            Return True
        End If

        Return False
    End Function

    Public Function CheckPassword(ByVal strUsername As String, strPassword As String) As Boolean
        'Purpose: checks if password is valid for given username
        'Arguments: 1 string
        'Returns: True - if password matches; False - if password doesn't match
        'Author: Nicole Chu (nc7997)
        'Date: 9/18/2014

        'get DB password that corresponds to username
        Dim strDBPassword As String
        strDBPassword = mDatasetCustomer.Tables("tblCustomers").Rows(0).Item("password").ToString

        'check if input password matches DB password and return T/F
        If strPassword = strDBPassword Then
            'password is valid, so return true
            Return True
        Else : Return False 'because password is not valid
        End If

    End Function

    Public Sub GetAllCustomers()
        'Purpose: get all customers
        'Arguments: none
        'Returns: none
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        'Use SelectQuery sub to get all customers
        mstrQuery = "SELECT * FROM tblCustomers ORDER BY LastName, FirstName"
        SelectQuery(mstrQuery)

    End Sub

    Public Function CheckUsername(strUsername As String) As Boolean
        'Purpose: To check if the entered username is valid
        'Arguments: username, as string
        'Returns: Boolean value. True if username is valid, false otherwise
        'Author: Amy Enrione (ae7882)

        mstrQuery = "select * from tblCustomers where EmailAddr ='" & strUsername & "'"
        SelectQuery(mstrQuery)

        'check to see how many records were in dataset. 
        'if one, return true
        'if 0 return false
        If mDatasetCustomer.Tables("tblCustomers").Rows.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    Public Sub GetNumberFNameLNameAllCustomers()
        RunProcedureNoParam("usp_customers_get_number_fname_lname_for_all_customers")
    End Sub

    Public Sub GetByRecordID(ByVal strRecordID As String)
        'Purpose: get customer by recordID
        'Arguments: 1 string
        'Returns: none
        'Author: Nicole Chu (nc7997)
        'Date: 10/14/14

        'Use SelectQuery sub to get customer by recordID
        mstrQuery = "SELECT * FROM tblCustomers WHERE recordID=" & strRecordID
        SelectQuery(mstrQuery)

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

    Public Sub AddCustomer(intCustomerNumber As Integer, ByVal strLastName As String, ByVal strFirstName As String, ByVal strInitial As String, strPassword As String, ByVal strAddress As String, ByVal strZip As String, ByVal strEmail As String, ByVal strPhone As String, strDOB As String)
        'Purpose: adds a customer to database
        'Arguments: 11 strings
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 9/24/2014

        mstrQuery = "INSERT INTO tblCustomers (CustomerNumber, EmailAddr, Password, LastName, FirstName, MI, Address, ZipCode, Phone, DOB) VALUES (" & _
            "'" & intCustomerNumber & "', " & _
            "'" & strEmail & "', " & _
            "'" & strPassword & "', " & _
            "'" & strLastName & "', " & _
            "'" & strFirstName & "', " & _
            "'" & strInitial & "', " & _
            "'" & strAddress & "', " & _
            "'" & strZip & "', " & _
            "'" & strPhone & "', " & _
            "'" & strDOB & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    'need to change this to SP later

    Public Sub ModifyCustomer(strEmail As String, strLast As String, strFirst As String, strMiddle As String, strAddress As String, strZip As String, strPhone As String, ByVal intCustomerNumber As Integer)



        mstrQuery = "UPDATE tblCustomers SET " & _
            "EmailAddr = '" & strEmail & "', " & _
            "LastName = '" & strLast & "', " & _
            "FirstName = '" & strFirst & "', " & _
            "MI = '" & strMiddle & "', " & _
            "Address = '" & strAddress & "', " & _
            "ZipCode = '" & strZip & "', " & _
            "Phone = '" & strPhone & "' " & _
            "WHERE CustomerNumber = " & intCustomerNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub ModifyPassword(strPassword As String, ByVal intCustomerNumber As Integer)



        mstrQuery = "UPDATE tblCustomers SET " & _
            "Password = '" & strPassword & "' " & _
            "WHERE CustomerNumber = " & intCustomerNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub DeleteCustomer(ByVal intRecordID As Integer)
        'Purpose: deletes a customer from database
        'Arguments: 1 integer (recordID)
        'Returns: nothing
        'Author: Nicole Chu (nc7997)
        'Date: 10/7/14

        mstrQuery = "DELETE FROM tblCustomers WHERE RecordID =" & intRecordID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub





    'Purpose:to search by the typed empid
    'Arguments: the strIN
    'Returns: True or false
    'Author: Leah Carroll
    'Date: 10-20-2014
    Public Function SearchByCustomerNumber(strIN As String) As Boolean

        ' to Get Customer use Select
        mstrQuery = "select * from tblCustomers where CustomerNumber='" & strIN & "'"
        ' run the query
        SelectQuery(mstrQuery)

        If mDatasetCustomer.Tables("tblCustomers").Rows.Count = 0 Then
            'means there is none in there
            Return False
        Else
            'means nothing is one there
            Return True
        End If
    End Function

    Public Sub ModifyCustomer2(strEmail As String, strPassword As String, strLast As String, strFirst As String, strMiddle As String, strAddress As String, strZip As String, strPhone As String, ByVal intCustomerNumber As Integer)



        mstrQuery = "UPDATE tblCustomers SET " & _
            "EmailAddr = '" & strEmail & "', " & _
            "Password = '" & strPassword & "', " & _
            "LastName = '" & strLast & "', " & _
            "FirstName = '" & strFirst & "', " & _
            "MI = '" & strMiddle & "', " & _
            "Address = '" & strAddress & "', " & _
            "ZipCode = '" & strZip & "', " & _
            "Phone = '" & strPhone & "' " & _
            "WHERE CustomerNumber = " & intCustomerNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub


    'Leah
    'used in employee manage customers and manager manage customers
    Public Sub ModifyStatus(intNotAccountStatus As Integer, ByVal intCustomerNumber As Integer)

        mstrQuery = "UPDATE tblCustomers SET " & _
            "Active = " & intNotAccountStatus & " " & _
            "WHERE CustomerNumber = " & intCustomerNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

End Class
