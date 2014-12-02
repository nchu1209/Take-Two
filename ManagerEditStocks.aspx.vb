Public Class ManagerEditStocks
    Inherits System.Web.UI.Page

    Dim DBStocks As New ClassDBStocks
    Dim valid As New ClassStockValidation
    'have a gridview with textboxes for the prices.
    'for the descriptive message we should do loops to see what was changed and store that in a string to be output on a panel that will become visible after a succesful transaction

    '=========== HELLO FROM NICOLE ====================

    'Hey Leah! Okay, so I set up the gridview. It's all on the aspx page -- essentially you turn the column into a TemplateField, 
    'then turn the "template" to anything you want--textbox, calendar, imagebutton, etc.
    'to reference the cell, you have to do something a bit weird. You can see CustomerPayBills for working code, but essentially you have to take
    'whatever's in the cell and create a virtual textbox, then call that:

    'Dim t As TextBox = DirectCast(gvMyPayees.Rows(i).Cells(4).FindControl("txtAmount"), TextBox)
    'and then you use t.Text to pull the values.
    'Kind of convoluted, but it works. 

    'Let me know if you have any questions! Hope this helps :)





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeFirstName") Is Nothing Then
            Response.Redirect("EmployeeLogin.aspx")
        End If
    End Sub


    Protected Sub btnChangePrice_Click(sender As Object, e As EventArgs) Handles btnChangePrice.Click
        'DBStocks.GetByStockTicker(gvStocks.Rows(0).Cells(0))
        'mdecBalance = CDec(dbact.AccountsDataset6.Tables("tblAccounts").Rows(0).Item("Balance"))


        'validate textbox fields
        For i = 0 To gvStocks.Rows.Count - 1
            Dim t As TextBox = DirectCast(gvStocks.Rows(i).Cells(4).FindControl("txtPrice"), TextBox)
            'lblMessage.Text = t.ToString
            If t.Text <> "" Then
                If valid.CheckDecimal(t.Text) = -1 Then
                    lblMessage.Text = "ERROR: Please enter a valid sales price."
                    Exit Sub
                End If
            Else
                lblMessage.Text = "ERROR: Sales Prices may not be left blank."
                Exit Sub
            End If
        Next
        Dim strDescriptiveMessage As String
        strDescriptiveMessage = ""

        'make changes to db if sales price is changed
        For j = 0 To gvStocks.Rows.Count - 1
            'whats in textbox
            'Dim j As Integer
            'j = 0
            Dim y As TextBox = DirectCast(gvStocks.Rows(j).Cells(4).FindControl("txtPrice"), TextBox)

            'find corresponding ticker
            Dim strTick As String = gvStocks.Rows(j).Cells(0).Text
            Dim strPrice As String
            'now find the sales price in accordance with this ticker
            DBStocks.GetAllStocks()
            DBStocks.GetByTickerSymbol(strTick)
            strPrice = DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString
            ''if t is equal to whats in db according to ticker move on, otherwise edit the db
            If y.Text <> DBStocks.StocksDataset.Tables("tblStocks").Rows(0).Item("SalesPrice").ToString Then
                'edit db
                DBStocks.ModifyStockPrice(y.Text, strTick)
                'add to descriptive string
                strDescriptiveMessage = strDescriptiveMessage.ToString + strTick & " price was changed to $" & y.Text & "<br/>"
            End If
        Next

        'output descriptive string
        lblDescriptiveMessage.Text = strDescriptiveMessage


    End Sub
End Class