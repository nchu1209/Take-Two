Option Strict On
Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBPendingStocks

    'Declare module-level variables
    Dim mDatasetPendingStocks As New DataSet
    Dim mDatasetPendingStocks2 As New DataSet
    Dim mDatasetPendingStocks3 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbck614;user id=msbck614;password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mMyView3 As New DataView
    Private _session As String

    Public ReadOnly Property PendingStocksDataset() As DataSet
        Get
            ' return dataset to user
            Return mDatasetPendingStocks
        End Get
    End Property

    Public ReadOnly Property MyView() As DataView
        Get
            Return Me.mMyView
        End Get
    End Property

    Public ReadOnly Property PendingStocksDataset2() As DataSet
        Get
            ' return dataset to user
            Return mDatasetPendingStocks2
        End Get
    End Property

    Public ReadOnly Property MyView2() As DataView
        Get
            Return Me.mMyView2
        End Get
    End Property


    Public ReadOnly Property PendingStocksDataset3() As DataSet
        Get
            ' return dataset to user
            Return mDatasetPendingStocks2
        End Get
    End Property

    Public ReadOnly Property MyView3() As DataView
        Get
            Return Me.mMyView2
        End Get
    End Property


    Public Sub SelectQuery(ByVal strQuery As String)

        Try
            ' define data connection and data adapter
            mdbConn = New SqlConnection(mstrConnection)
            mdbDataAdapter = New SqlDataAdapter(strQuery, mdbConn)

            ' open the connection and dataset 
            mdbConn.Open()

            ' clear the dataset before filling
            mDatasetPendingStocks.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetPendingStocks, "tblStocks")

            ' close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & "error is " & ex.Message)
        End Try

    End Sub



    Public Sub RunProcedureNoParam(strName As String)
        'CREATES INSTANCES OF THE CONNECTION AND COMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim objCommand As New SqlDataAdapter(strName, objConnection)
        Try
            'SETS THE COMMAND TYPE TO "STORED PROCEDURE
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mDatasetPendingStocks.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetPendingStocks, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetPendingStocks.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try

    End Sub

    Public Sub RunProcedureGetMax(strName As String)
        'CREATES INSTANCES OF THE CONNECTION AND COMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim objCommand As New SqlDataAdapter(strName, objConnection)
        Try
            'SETS THE COMMAND TYPE TO "STORED PROCEDURE
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mDatasetPendingStocks.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetPendingStocks2, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetPendingStocks2.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try

    End Sub

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
            mDatasetPendingStocks.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPendingStocks, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetPendingStocks.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub



    Public Sub RunProcedureOneParameter2(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)

        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetPendingStocks2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPendingStocks2, "tblStocks")
            'copy dataset to dataview
            mMyView2.Table = mDatasetPendingStocks2.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub


    Public Sub RunProcedureOneParameter3(ByVal strProcedureName As String, ByVal strParameterName As String, ByVal strParameterValue As String)

        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strProcedureName, objConnection)
        Try
            'sets command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'add parameter to SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParameterName, strParameterValue))
            'clear dataset
            mDatasetPendingStocks.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetPendingStocks3, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetPendingStocks3.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    'for the future
    Public Sub ModifyAccountBalance1(strNewAvailableBalance As String, strAccountNumber As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblAccounts SET AvailableBalance= '" & strNewAvailableBalance & _
            "' where AccountNumber = " & strAccountNumber

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    Public Sub AddStockPortfolio(strStockPortfolioID As String, strTicker As String, intNumberOfSharesHeld As Integer, strStockAccountNumber As String, intNumberOfSharesInTransaction As Integer, strStockType As String, decPurchasePrice As Decimal, intTransactionNumber As Integer)

        mstrQuery = "INSERT INTO tblPendingStockPortfolio (StockPortfolioID, Ticker, NumberOfSharesHeld, StockAccountNumber,NumberOfSharesInTransaction, StockType, TransactionNumber, PurchasePrice) VALUES (" & _
            "'" & strStockPortfolioID & "', " & _
            "'" & strTicker & "', " & _
            "'" & intNumberOfSharesHeld & "', " & _
            "'" & strStockAccountNumber & "', " & _
             "'" & intNumberOfSharesInTransaction & "', " & _
            "'" & strStockType & "', " & _
            "'" & intTransactionNumber & "', " & _
            "'" & decPurchasePrice & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetMaxTransactionNumber()
        RunProcedureGetMax("usp_stocks_get_max_transaction_number")
    End Sub

    Public Sub GetStockPortfolioSummary(intTransactionNumber As Integer)
        RunProcedureOneParameter("usp_stockportfolio_get_by_transactionnumber", "@transactionnumber", intTransactionNumber.ToString)
    End Sub

    Public Sub GetMaxTransactionNumberByCustomerID(intCustomerID As Integer)
        RunProcedureOneParameter3("usp_stockportfolio_get_max_transactionnumber_by_customerID", "@customerID", intCustomerID.ToString)
    End Sub

    Public Sub ApproveStockAccount(strApproval As String, ByVal strAccountNumber As String)

        mstrQuery = "UPDATE tblAccounts SET " & _
            "ManagerApprovedStockAccount = '" & strApproval & "' " & _
            "WHERE AccountNumber = " & strAccountNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

End Class
