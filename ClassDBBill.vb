Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBBill
    Dim mDatasetBill As New DataSet
    Dim mDatasetBill2 As New DataSet
    Dim mDatasetBill3 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False; initial catalog=mis333k_msbck614; user id=msbck614; password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mmyview3 As New DataView

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

    Public ReadOnly Property BillDataset2() As DataSet
        Get
            'Return dataset to user
            Return mDatasetBill2
        End Get
    End Property
    Public ReadOnly Property MyView2() As DataView
        Get
            Return mMyView2
        End Get
    End Property

    Public ReadOnly Property BillDataset3() As DataSet
        Get
            'Return dataset to user
            Return mDatasetBill3
        End Get
    End Property
    Public ReadOnly Property MyView3() As DataView
        Get
            Return mMyView3
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

    Public Sub RunProcedureSort(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)
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
            mDatasetBill2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetBill2, "tblBill")
            'copy dataset to dataview
            mMyView.Table = mDatasetBill2.Tables("tblBill")
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

    Public Sub SortBills(strCustomerNumber As String)
        RunProcedureSort("usp_bill_sort_minimum_payment", "@CustomerNumber", strCustomerNumber)
    End Sub

    Public Sub GetCustomerBills(strCustomerNumber As String, datSystemDate As Date)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList
        aryNames.Add("@customernumber")
        aryNames.Add("@systemdate")
        aryValues.Add(strCustomerNumber)
        aryValues.Add(datSystemDate)
        RunProcedureAnyParam("usp_bill_get_by_customernumber", BillDataset, mMyView, "tblBill", aryNames, aryValues)
    End Sub

    Public Sub GetBillByPayeeID(strCustomerNumber As String, datSystemDate As Date, strPayeeID As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList
        aryNames.Add("@customernumber")
        aryNames.Add("@systemdate")
        aryNames.Add("@payeeID")
        aryValues.Add(strCustomerNumber)
        aryValues.Add(datSystemDate)
        aryValues.Add(strPayeeID)
        RunProcedureAnyParam("usp_bill_get_billID_by_payeeID", BillDataset2, mMyView2, "tblBill", aryNames, aryValues)
    End Sub

    Public Sub SetUpMinimumPayment(strCustomerNumber As String, decMinimumAmount As Decimal, datSignUpDate As Date)

        mstrQuery = "INSERT INTO tblMinimumPayments (CustomerNumber, MinimumAmount, SignUpDate) VALUES (" & _
            "'" & strCustomerNumber & "', " & _
            "'" & decMinimumAmount & "', " & _
            "'" & datSignUpDate & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetMinimumPayment(strCustomerNumber As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList
        aryNames.Add("@customernumber")
        aryValues.Add(strCustomerNumber)
        RunProcedureAnyParam("usp_bill_get_minimumpayment_by_customernumber", BillDataset3, mmyview3, "tblBill", aryNames, aryValues)
    End Sub

    Public Sub GetAllMinimumPayments()
        RunProcedureNoParam("usp_minimumpayments_get_all")
    End Sub

End Class
