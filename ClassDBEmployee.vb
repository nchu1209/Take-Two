Option Strict On
Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBEmployee

    'Declare module-level variables
    Dim mDatasetEmployee As New DataSet
    Dim mDatasetEmployee2 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbck614;user id=msbck614;password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Private _session As String


    'Purpose:to set the property of the dataset
    'Arguments: none
    'Returns: mDatasetEmployee
    'Author: Leah Carroll
    'Date: 10-19-2014
    ' define a public read only property for the outside world to access the dataset filled by this class
    Public ReadOnly Property EmpDataset() As DataSet
        Get
            ' return dataset to user
            Return mDatasetEmployee
        End Get
    End Property

    'Purpose:to set the propert of myview
    'Arguments: none
    'Returns: Me.mMyView
    'Author: Leah Carroll
    'Date: 10-19-2014
    Public ReadOnly Property MyView() As DataView
        Get
            Return Me.mMyView
        End Get
    End Property



    Public ReadOnly Property EmpDataset2() As DataSet
        Get
            ' return dataset to user
            Return mDatasetEmployee2
        End Get
    End Property

    'Purpose:to set the propert of myview
    'Arguments: none
    'Returns: Me.mMyView
    'Author: Leah Carroll
    'Date: 10-19-2014
    Public ReadOnly Property MyView2() As DataView
        Get
            Return Me.mMyView2
        End Get
    End Property




    'Purpose: run any select query and fill dataset
    'Arguments: strQuery
    'returns: the dataset
    ' Author: Leah Carroll
    'Date: 9-18-2014
    Public Sub SelectQuery(ByVal strQuery As String)

        Try
            ' define data connection and data adapter
            mdbConn = New SqlConnection(mstrConnection)
            mdbDataAdapter = New SqlDataAdapter(strQuery, mdbConn)

            ' open the connection and dataset 
            mdbConn.Open()

            ' clear the dataset before filling
            mDatasetemployee.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetEmployee, "tblemployees")

            ' close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & "error is " & ex.Message)
        End Try

    End Sub



    'Purpose:to read the inputed password and to return if it is valid or not
    'Arguments:password 
    'Returns: if the password is valid or not (True or False)
    'Author: Leah Carroll
    'Date: 9-18-2014
    Public Function CheckPassword(strPassword As String) As Boolean

        If strPassword = MyView(0).Item("password").ToString Then
            Return True
        Else
            Return False
        End If

    End Function


    'Purpose:to run a prcedure in the db that has no parameters
    'Arguments: the strName
    'Returns: none, just updates the view
    'Author: Leah Carroll
    'Date: 10-19-2014
    Public Sub RunProcedure(strName As String)
        'CREATES INSTANCES OF THE CONNECTION AND COMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim objCommand As New SqlDataAdapter(strName, objConnection)
        Try
            'SETS THE COMMAND TYPE TO "STORED PROCEDURE
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mDatasetemployee.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetEmployee, "tblEmployees")
            'copy dataset to dataview
            mMyView.Table = mDatasetEmployee.Tables("tblEmployees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try

    End Sub



    'Purpose:to run a prcedure in the db that has no parameters
    'Arguments: the strName
    'Returns: none, just updates the view
    'Author: Leah Carroll
    'Date: 10-19-2014
    Public Sub RunProcedureGetMax(strName As String)
        'CREATES INSTANCES OF THE CONNECTION AND COMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim objCommand As New SqlDataAdapter(strName, objConnection)
        Try
            'SETS THE COMMAND TYPE TO "STORED PROCEDURE
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mDatasetEmployee2.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetEmployee2, "tblEmployees")
            'copy dataset to dataview
            mMyView2.Table = mDatasetEmployee2.Tables("tblEmployees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try

    End Sub



    'Purpose:to get all employees through a stored procedure
    'Arguments: none
    'Returns: none directly, but returns the employee information
    'Author: Leah Carroll
    'Date: 10-20-2014
    Public Sub GetAllEmployeesUsingSP()
        RunProcedure("usp_employees_get_all")
    End Sub


    'Purpose:to search by the typed empid
    'Arguments: the strIN
    'Returns: True or false
    'Author: Leah Carroll
    'Date: 10-20-2014
    Public Function SearchByEmpID(strIN As String) As Boolean

        MyView.RowFilter = "empID='" & strIN & "'"

        If mMyView.Count = 0 Then
            Return False
        End If

        Return True
    End Function


    'Purpose:to get the record according to the ID
    'Arguments: the username input by the user
    'Returns: if the username is valid or not
    'Author: Leah Carroll
    'Date: 9-18-2014
    Public Sub GetByEmpID(strEmpID As String)
        mstrQuery = "Select * from tblEmployees where empID = '" & strEmpID & "'"
        SelectQuery(mstrQuery)
    End Sub


    'Purpose:to modify the customer in the db
    'Arguments: all of the fields that are inputted when modifying customer such as the username, password, first name, last name, recordID
    'Returns: none
    'Author: Leah Carroll
    'Date: 10-5-2014
    Public Sub ModifyEmployeeAddress(strAddress As String, strZipcode As String, strEmpID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblEmployees SET Address= '" & strAddress & _
            "', Zipcode = '" & strZipcode & _
            "'where EmpID = " & strEmpID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    'Purpose:to modify the customer in the db
    'Arguments: all of the fields that are inputted when modifying customer such as the username, password, first name, last name, recordID
    'Returns: none
    'Author: Leah Carroll
    'Date: 10-5-2014
    Public Sub ModifyEmployeePhone(strPhone As String, strEmpID As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblEmployees SET Phone= '" & strPhone & _
            "'where EmpID = " & strEmpID

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    'Purpose: run given query to update db
    'Argument: one string (any SQL statement)
    'returns: nothing
    'Author: Leah Carroll
    'Date: 9-23-2014
    Public Sub UpdateDB(strQuery As String)

        Try
            'make connection using the connection string above
            mdbConn = New SqlConnection(mstrConnection)
            Dim dbCommand As New SqlCommand(strQuery, mdbConn)

            'open the connection
            mdbConn.Open()

            'run the query
            dbCommand.ExecuteNonQuery()

            'close the connection
            mdbConn.Close()

        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)

        End Try
    End Sub



    Public Sub ModifyPassword(strPassword As String, ByVal intEmployeeID As Integer)

        mstrQuery = "UPDATE tblEmployees SET " & _
            "Password = '" & strPassword & "' " & _
            "WHERE EmpID = " & intEmployeeID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

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
            mDatasetEmployee.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetEmployee, "tblEmployees")
            'copy dataset to dataview
            mMyView.Table = mDatasetEmployee.Tables("tblEmployees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub






    Public Sub LinkZip(ByVal strEmployeeID As String)
        RunProcedureOneParameter("usp_innerjoin_employee_city_by_zip", "@EmpID", strEmployeeID)
    End Sub



    Public Sub ModifyEmployee(strEmpType As String, strPassword As String, strLast As String, strFirst As String, strMiddle As String, strAddress As String, strZip As String, strPhone As String, ByVal intEmpID As Integer)



        mstrQuery = "UPDATE tblEmployees SET " & _
            "EmpType = '" & strEmpType & "', " & _
            "Password = '" & strPassword & "', " & _
            "LastName = '" & strLast & "', " & _
            "FirstName = '" & strFirst & "', " & _
            "MI = '" & strMiddle & "', " & _
            "Address = '" & strAddress & "', " & _
            "ZipCode = '" & strZip & "', " & _
            "Phone = '" & strPhone & "' " & _
            "WHERE EmpID = " & intEmpID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub


    'Leah
    'used in manager manage employees
    Public Sub ModifyStatus(strNotAccountStatus As String, ByVal intEmpID As Integer)

        mstrQuery = "UPDATE tblEmployees SET " & _
            "Active = '" & strNotAccountStatus & "' " & _
            "WHERE EmpID = " & intEmpID

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub




    Public Function CheckSSNSP(strSSN As String) As Boolean
        RunProcedureOneParameter("usp_employee_get_ssn", "@ssn", strSSN)

        If mDatasetEmployee.Tables("tblEmployees").Rows.Count <> 0 Then
            Return False
        End If

        Return True
    End Function

    'use on manager hire employee
    Public Function GetBySSN(strSSN As String) As Boolean
        Dim intSSN As Integer
        intSSN = CInt(strSSN)
        mstrQuery = "Select * from tblEmployees where SSN = " & intSSN
        SelectQuery(mstrQuery)

        If mDatasetEmployee.Tables("tblEmployees").Rows.Count <> 1 Then
            Return True
        End If
        Return False
    End Function


    Public Sub GetMaxEmpIDUsingSP()
        RunProcedureGetMax("usp_employees_get_max_empID")
    End Sub

    Public Sub GetMaxEmpID()
        mstrQuery = "Select max(empID) from tblEmployees"
        SelectQuery(mstrQuery)
    End Sub



    Public Sub AddEmployee(strEmpType As String, strPassword As String, strLastName As String, strFirstName As String, strInitial As String, strSSN As String, strAddress As String, strZip As String, strPhone As String, strEmpID As String)
        Dim strActive As String
        strActive = "T"
        mstrQuery = "INSERT INTO tblEmployees (EmpType, Password, LastName, FirstName, MI, SSN, Address, ZipCode, Phone, Active, EmpID) VALUES (" & _
            "" & strEmpType & ", " & _
            "'" & strPassword & "', " & _
            "'" & strLastName & "', " & _
            "'" & strFirstName & "', " & _
            "'" & strInitial & "', " & _
            "" & strSSN & ", " & _
            "'" & strAddress & "', " & _
            "" & strZip & ", " & _
            "" & strPhone & ", " & _
             "'" & strActive & "', " & _
            "" & strEmpID & ")"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

End Class
