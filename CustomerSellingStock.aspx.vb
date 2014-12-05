Public Class CustomerSellingStock
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts
    Dim DBStocks As New ClassDBStocks
    Dim valid As New ClassValidate
    Dim DBDate As New ClassDBDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CustomerFirstName") Is Nothing Then
            Response.Redirect("CustomerLogin.aspx")
        End If

        DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

        If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
            Response.Redirect("CustomerCreateBankAccount.aspx")
        End If

        lblMessage.Text = ""

    End Sub

    Protected Sub gvStocks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStocks.SelectedIndexChanged
        Dim index As Integer = gvStocks.SelectedIndex

        Session("Ticker") = gvStocks.Rows(index).Cells(2).Text
        Session("PurchaseDate") = CDate(gvStocks.Rows(index).Cells(3).Text)

        Response.Redirect("CustomerStockAdditionalDetails.aspx")
    End Sub

    Protected Sub btnSell_Click(sender As Object, e As EventArgs) Handles btnSell.Click
        'count all textboxes <> "" -- if <> 1, error: you can only sell from 1 at a time!
        Dim intCountFilled As Integer
        Dim intSelectedIndex As Integer

        For i = 0 To gvStocks.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvStocks.Rows(i).Cells(8).FindControl("txtAmount"), TextBox)
            If t.Text <> "" Then
                intCountFilled += 1
            End If

            intSelectedIndex = i
        Next

        If intCountFilled <> 1 Then
            lblMessage.Text = "You may only sell shares from one stock set at a time. Please ensure that only one 'Sell Shares' textbox is filled."
            Exit Sub
        End If

        Dim txtAmount As TextBox = DirectCast(gvStocks.Rows(intSelectedIndex).Cells(8).FindControl("txtAmount"), TextBox)

        'integer
        If valid.CheckInteger(txtAmount.Text) <= 0 Then
            lblMessage.Text = "Please enter a valid amount of shares to sell."
            Exit Sub
        End If

        Dim intAmount As Integer = CInt(txtAmount.Text)

        '<= SharesHeld
        Dim intSharesHeld As Integer = CInt(gvStocks.Rows(intSelectedIndex).Cells(5).Text)
        If intAmount > intSharesHeld Then
            lblMessage.Text = "You cannot sell more shares than you currently hold."
            Exit Sub
        End If

        'check sell date selected
        Dim calSaleDate As Calendar = DirectCast(gvStocks.Rows(intSelectedIndex).Cells(9).FindControl("calSaleDate"), Calendar)
        If calSaleDate.SelectedDate = Nothing Then
            lblMessage.Text = "Please select a sale date."
            Exit Sub
        End If

        Dim datSaleDate As Date = CDate(calSaleDate.SelectedDate)
        Dim datPurchaseDate As Date = CDate(gvStocks.Rows(intSelectedIndex).Cells(3).Text)

        'sale date >= currentdate
        If DBDate.CheckSelectedDate(datSaleDate) = -1 Then
            lblMessage.Text = "Error: You have selected a sale date prior to today."
            Exit Sub
        End If

        'sale date >= purchasedate
        If datSaleDate < datPurchaseDate Then
            lblMessage.Text = "Error: You have selected a sale date prior to the purchase date."
            Exit Sub
        End If

        'pass whatever stuff I need to pass
        'name, number of shares, number of shares remaining, fees, NET profit
        Session("Ticker") = gvStocks.Rows(intSelectedIndex).Cells(2).Text
        Session("intAmount") = intAmount
        Session("sharesRemaining") = intSharesHeld - intAmount
        Session("CurrentFees") = gvStocks.Rows(intSelectedIndex).Cells(7).Text
        Session("CurrentPrice") = CDec(gvStocks.Rows(intSelectedIndex).Cells(6).Text)
        Session("PurchasePrice") = CDec(gvStocks.Rows(intSelectedIndex).Cells(4).Text)

        'send to confirmation page
        Response.Redirect("CustomerSellingStockConfirm.aspx")
    End Sub
End Class