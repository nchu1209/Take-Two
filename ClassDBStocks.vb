Option Strict On
Imports System.Data
Imports System.Data.SqlClient

Public Class ClassDBStocks


    'Declare module-level variables
    Dim mDatasetStocks As New DataSet
    Dim mDatasetStocks2 As New DataSet
    Dim mDatasetStocks3 As New DataSet
    Dim mDatasetStocks4 As New DataSet
    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size =4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbck614;user id=msbck614;password=AmyEnrione1"
    Dim mMyView As New DataView
    Dim mMyView2 As New DataView
    Dim mMyView3 As New DataView
    Dim mMyView4 As New DataView
    Private _session As String

    Public ReadOnly Property StocksDataset() As DataSet
        Get
            ' return dataset to user
            Return mDatasetStocks
        End Get
    End Property

    Public ReadOnly Property MyView() As DataView
        Get
            Return Me.mMyView
        End Get
    End Property

    Public ReadOnly Property StocksDataset2() As DataSet
        Get
            ' return dataset to user
            Return mDatasetStocks2
        End Get
    End Property

    Public ReadOnly Property MyView2() As DataView
        Get
            Return Me.mMyView2
        End Get
    End Property


    Public ReadOnly Property StocksDataset3() As DataSet
        Get
            ' return dataset to user
            Return mDatasetStocks2
        End Get
    End Property

    Public ReadOnly Property MyView3() As DataView
        Get
            Return Me.mMyView2
        End Get
    End Property

    Public ReadOnly Property StocksDataset4() As DataSet
        Get
            ' return dataset to user
            Return mDatasetStocks4
        End Get
    End Property

    Public ReadOnly Property MyView4() As DataView
        Get
            Return Me.mMyView4
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
            mDatasetStocks.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetStocks, "tblStocks")

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
            Me.mDatasetStocks.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetStocks, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetStocks.Tables("tblStocks")
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
            Me.mDatasetStocks.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetStocks2, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetStocks2.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try

    End Sub

    Public Sub RunProcedureNoParam4(strName As String)
        'CREATES INSTANCES OF THE CONNECTION AND COMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure you will be executing
        Dim objCommand As New SqlDataAdapter(strName, objConnection)
        Try
            'SETS THE COMMAND TYPE TO "STORED PROCEDURE
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mDatasetStocks4.Clear()
            'OPEN CONNECTION AND FILL DATASET
            objCommand.Fill(mDatasetStocks4, "tblStocks")
            'copy dataset to dataview
            mMyView4.Table = mDatasetStocks4.Tables("tblStocks")
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
            mDatasetStocks.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetStocks, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetStocks.Tables("tblStocks")
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
            mDatasetStocks2.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetStocks2, "tblStocks")
            'copy dataset to dataview
            mMyView2.Table = mDatasetStocks2.Tables("tblStocks")
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
            mDatasetStocks.Clear()
            'open connection and fill dataset
            mdbDataAdapter.Fill(mDatasetStocks3, "tblStocks")
            'copy dataset to dataview
            mMyView.Table = mDatasetStocks3.Tables("tblStocks")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strProcedureName.ToString & "parameters are " & strParameterName.ToString & strParameterValue.ToString & " error is " & ex.Message)
        End Try
    End Sub


    Public Sub GetAllStocks()
        RunProcedureNoParam("usp_stocks_get_all")
    End Sub


    Public Function CheckTickerSP(strTicker As String) As Boolean
        RunProcedureOneParameter("usp_stocks_get_ticker", "@TickerSymbol", strTicker)

        If mDatasetStocks.Tables("tblStocks").Rows.Count <> 0 Then
            Return True
        End If

        Return False
    End Function

    Public Sub GetByStockTicker(strTicker As String)
        RunProcedureOneParameter2("usp_stocks_get_by_ticker", "@Ticker", strTicker)
    End Sub

    Public Sub AddStock(strStockType As String, strTicker As String, strStockName As String, strSharePrice As String, strTransactionFee As String)
        'Purpose: adds a stock to database
        'Arguments: strings
        'Returns: nothing
        'Author: Leah Carroll
        'Date: 11/25/2014

        mstrQuery = "INSERT INTO tblStocks (StockType, TickerSymbol, Name, SalesPrice, Fee) VALUES (" & _
            "'" & strStockType & "', " & _
            "'" & strTicker & "', " & _
            "'" & strStockName & "', " & _
            "'" & strSharePrice & "', " & _
            "'" & strTransactionFee & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub GetByTickerSymbol(strTicker As String)
        mstrQuery = "Select * from tblStocks where TickerSymbol= '" & strTicker & "'"
        SelectQuery(mstrQuery)
    End Sub




    Public Sub ModifyStockPrice(strPrice As String, strTicker As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblStocks SET SalesPrice= " & strPrice & _
            " where TickerSymbol = '" & strTicker & "'"

        'updates the db
        UpdateDB(mstrQuery)

    End Sub

    'if the buy date is today
    Public Sub ModifyAccountBalances(strNewAccountBalance As String, strNewAvailableBalance As String, strAccountNumber As String)
        'the strquery that will be modified
        mstrQuery = "UPDATE tblAccounts SET Balance= '" & strNewAccountBalance & "', AvailableBalance= '" & strNewAvailableBalance & _
            "' where AccountNumber = " & strAccountNumber

        'updates the db
        UpdateDB(mstrQuery)

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
        'Purpose: adds a stock to database
        'Arguments: strings
        'Returns: nothing
        'Author: Leah Carroll
        'Date: 11/25/2014

        mstrQuery = "INSERT INTO tblStockPortfolio (StockPortfolioID, Ticker, NumberOfSharesHeld, StockAccountNumber,NumberOfSharesInTransaction, StockType, TransactionNumber, PurchasePrice) VALUES (" & _
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
        RunProcedureGetMax("usp_transactions_get_max_transaction_number")
    End Sub

    Public Sub GetStockPortfolioSummary(intTransactionNumber As Integer)
        RunProcedureOneParameter("usp_stockportfolio_get_by_transactionnumber", "@transactionnumber", intTransactionNumber.ToString)
    End Sub

    Public Sub GetMaxTransactionNumberByCustomerID(intCustomerID As Integer)
        RunProcedureOneParameter3("usp_stockportfolio_get_max_transactionnumber_by_customerID", "@customerID", intCustomerID.ToString)
    End Sub

    Public Sub GetMaxSetNumber()
        RunProcedureNoParam4("usp_stocktransactions_get_max_setnumber")
    End Sub

    Public Sub ApproveStockAccount(strApproval As String, ByVal strAccountNumber As String)

        mstrQuery = "UPDATE tblAccounts SET " & _
            "ManagerApprovedStockAccount = '" & strApproval & "' " & _
            "WHERE AccountNumber = " & strAccountNumber

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub

    Public Sub AddNewPriceField(strTicker As String, strDate As String, decPrice As Decimal)
        'Purpose: adds a new price field for the tblTickerDatePrice
        'Arguments: strings
        'Returns: nothing
        'Author: Leah Carroll
        'Date: 11/25/2014

        mstrQuery = "INSERT INTO tblTickerDatePrice (Ticker, Date, Price) VALUES (" & _
            "'" & strTicker & "', " & _
            "'" & strDate & "', " & _
            "'" & decPrice & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub


    Public Sub AddStockTransaction(decSetNumber As Decimal, decCustomerNumber As Decimal, strTicker As String, decPurchasePrice As Decimal, intSharesHeld As Integer, strDate As String)
        'Arguments: strings
        'Returns: nothing
        'Author: Leah Carroll
        'Date: 11/25/2014

        mstrQuery = "INSERT INTO tblTickerDatePrice (SetNumber, CustomerNumber, Ticker, PurchasePrice, SharesHeld, Date) VALUES (" & _
            "'" & decSetNumber & "', " & _
            "'" & decCustomerNumber & "', " & _
             "'" & strTicker & "', " & _
            "'" & decPurchasePrice & "', " & _
             "'" & intSharesHeld & "', " & _
            "'" & strDate & "')"

        'use UpdateDB sub to update database
        UpdateDB(mstrQuery)

    End Sub


End Class
