Option Strict On

Public Class ManagerAddStocks
    Inherits System.Web.UI.Page


    Dim DBStocks As New ClassDBStocks
    Dim Valid As New ClassStockValidation


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check to see if session emptype exists
        If Session("EmpID") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If
        lblError.Text = ""
        DBStocks.GetAllStocks()

        If IsPostBack = False Then
            StockSuccessfullyAdded.Visible = False
            AddStockMaterial.Visible = True
        End If



    End Sub

    Protected Sub btnAddStock_Click(sender As Object, e As EventArgs) Handles btnAddStock.Click


        'DO VALIDATIONS
        'SalesPrice	A positive number with no more than two decimal places
        ' make sure if transaction fee isnt blank that it is a postive number with no more than two decimals that will be then changed into dollar format
        'LINE 415



        ' TickerSymbol	No more than 5 letters only
        If txtTicker.Text <> "" Then
            If Valid.CheckTickerSymbol(txtTicker.Text) = False Then
                lblError.Text = "Please input a valid Ticker value."
                Exit Sub
            End If
        End If

        'makes sure there are no other ticker that are the same
        If txtTicker.Text <> "" Then
            If DBStocks.CheckTickerSP(txtTicker.Text) = True Then
                lblError.Text = "Ticker already in use."
                Exit Sub
            End If
        End If

        'has to be positive and can only have two decimals
        If txtSharePrice.Text <> "" Then
            If Valid.CheckPrice(txtSharePrice.Text) = False Then
                lblError.Text = "You did not input a valid amount for the Share Price."
                Exit Sub
            End If
        End If

        'made positive number
        If txtTransactionFee.Text <> "" Then
            If Valid.CheckDecimal(txtTransactionFee.Text) = -1 Then
                lblError.Text = "A Transaction Fee must be a positive number."
                Exit Sub
            End If
        End If

        If Not IsValid Then
            Exit Sub
        End If


        Dim strStockType As String
        strStockType = ddlStockType.SelectedItem.ToString()
        DBStocks.AddStock(strStockType, txtTicker.Text.ToUpper, txtStockName.Text, txtSharePrice.Text, txtTransactionFee.Text)
        lblError.Text = ""
        StockSuccessfullyAdded.Visible = True
        AddStockMaterial.Visible = False
        'call next page(managerHome)
        Response.AddHeader("Refresh", "2; URL=ManagerHome.aspx")


    End Sub
End Class