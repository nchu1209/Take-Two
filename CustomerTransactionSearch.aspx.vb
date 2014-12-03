Public Class CustomerTransactionSearch
    Inherits System.Web.UI.Page
    Dim DBTransactions As New ClassDBTransactions
    Dim DBAccount As New ClassDBAccounts
    Dim DBDate As New ClassDBDate
    Dim Val As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request.QueryString("ID") Is Nothing Then
                DBTransactions.GetAllTransactions(Session("AccountNumber"))
                DBAccount.GetAccountNameByAccountNumber(Session("AccountNumber"))
                lblAccountName.Text = DBAccount.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountName")
                Search()
            Else
                Dim strAccountNumber As String
                strAccountNumber = "100000" & Request.QueryString("ID").Substring(6, 4)
                Session("AccountNumber") = strAccountNumber

                DBTransactions.GetAllTransactions(Session("AccountNumber"))
                DBAccount.GetAccountNameByAccountNumber(Session("AccountNumber"))
                lblAccountName.Text = DBAccount.AccountsDataset5.Tables("tblAccounts").Rows(0).Item("AccountName")
                Search()
            End If  
        End If
        DBTransactions.GetAllTransactions(Session("AccountNumber").ToString)
    End Sub

    Public Sub Search()
        DBTransactions.DoSort()

        'bind the gridview to the dataview
        gvTransactionSearch.DataSource = DBTransactions.MyView
        gvTransactionSearch.DataBind()

        'error message & table disappears if there are no matching records
        If DBTransactions.MyView.Count = 0 Then
            lblNumberOfTransactions.Text = "No records found"
        Else
            lblNumberOfTransactions.Text = "There are " & DBTransactions.MyView.Count.ToString & " records"
        End If
    End Sub

    Public Sub Clear()
        txtDescriptionSearch.Text = ""
        rblDescription.SelectedValue = Nothing
        ddlSearchByTransactionType.SelectedIndex = 0
        rblPrice.SelectedValue = Nothing
        txtSearchByOtherPriceMax.Text = ""
        txtSearchByOtherPriceMin.Text = ""
        txtSearchByTransactionNumber.Text = ""
        rblDate.SelectedValue = Nothing
        txtCustomDateMin.Text = ""
        For i As Integer = 0 To cblParameters.Items.Count - 1
            cblParameters.Items(i).Selected = Nothing
        Next
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strDescriptionPartialCode1 As String
        Dim strDescriptionPartialCode2 As String
        Dim strDescriptionKeywordCode1 As String
        Dim strDescriptionKeywordCode2 As String
        strDescriptionPartialCode1 = "Description like '%"
        strDescriptionPartialCode2 = "'"
        strDescriptionKeywordCode1 = "Description like '%"
        strDescriptionKeywordCode2 = "%'"

        Dim strTransactionTypeCode1 As String
        Dim strTransactionTypeCode2 As String
        strTransactionTypeCode1 = "[Transaction Type] like '"
        strTransactionTypeCode2 = "'"

        Dim str0to100 As String
        Dim str101to200 As String
        Dim str201to300 As String
        Dim str300orMore As String
        Dim strPriceCode1 As String
        Dim strPriceCode2 As String
        Dim strPriceCode3 As String
        str0to100 = "Amount > 0 and Amount <=100"
        str101to200 = "Amount > 100 and Amount <=200"
        str201to300 = "Amount > 200 and amount <=300"
        str300orMore = "Amount > 300"
        strPriceCode1 = "Amount >= '"
        strPriceCode2 = "' AND Amount <= '"
        strPriceCode3 = "'"

        Dim strTransactionNumberCode1 As String
        Dim strTransactionNumberCode2 As String
        strTransactionNumberCode1 = "[Transaction Number] like '%"
        strTransactionNumberCode2 = "%'"

        DBDate.GetDate()
        Dim datSystemDate As Date
        datSystemDate = CDate(DBDate.DateDataset.Tables("tblSystemDate").Rows(0).Item("Date"))
        Dim strDateCode15 As String
        Dim strDateCode30 As String
        Dim strDateCode60 As String
        Dim strDateCodeAll As String
        Dim datBlank As New Date(1500, 1, 1, 1, 1, 1)
        strDateCode15 = "Date >= #" & datSystemDate.AddDays(-2) & "#"
        strDateCode30 = "Date >= #" & datSystemDate.AddDays(-30) & "#"
        strDateCode60 = "Date >= #" & datSystemDate.AddDays(-60) & "#"
        strDateCodeAll = "Date <> '" & datBlank & "'"
        
        Dim strIn1 As String
        Dim strIn2 As String
        Dim strIn3 As String
        Dim strIn4 As String
        Dim strIn5 As String

        If cblParameters.Items(0).Selected = True Then
            If txtDescriptionSearch.Text = "" Then
                lblError.Text = "If you are searching by description, please enter a description into the Search by Description box"
                Exit Sub
            End If
            If rblDescription.SelectedIndex = -1 Then
                lblError.Text = "If you are searching by description, please select either a partial or a keyword search"
                Exit Sub
            End If
        End If

        If cblParameters.Items(0).Selected = True And rblDescription.SelectedIndex = 0 Then
            strIn1 = strDescriptionPartialCode1 & txtDescriptionSearch.Text & strDescriptionPartialCode2 & " And "
        End If

        If cblParameters.Items(0).Selected = True And rblDescription.SelectedIndex = 1 Then
            strIn1 = strDescriptionKeywordCode1 & txtDescriptionSearch.Text & strDescriptionKeywordCode2 & " And "
        End If

        If cblParameters.Items(0).Selected = False Then
            strIn1 = "Description <> '""' And "
        End If

        If cblParameters.Items(1).Selected = True Then
            strIn2 = strTransactionTypeCode1 & ddlSearchByTransactionType.SelectedValue.ToString & strTransactionTypeCode2 & "And "
        End If

        If cblParameters.Items(1).Selected = False Then
            strIn2 = "[Transaction Type] <> '""' And "
        End If

        If cblParameters.Items(2).Selected = True Then
            If rblPrice.SelectedIndex = -1 Then
                lblError.Text = "If you want to search by price, please select a price range to search by"
                Exit Sub
            End If
            If rblPrice.SelectedValue = "Custom Range" Then
                If txtSearchByOtherPriceMax.Text = "" Or txtSearchByOtherPriceMin.Text = "" Then
                    lblError.Text = "Please enter a minimum and a maximum transaction amount"
                    Exit Sub
                End If
                If Val.CheckDecimal(txtSearchByOtherPriceMax.Text) = -1 Then
                    lblError.Text = "Please enter a positive numeric amount as your maximum price"
                    Exit Sub
                End If
                If Val.CheckDecimal(txtSearchByOtherPriceMin.Text) = -1 Then
                    lblError.Text = "Please enter a positive numeric amount as your minimum price"
                    Exit Sub
                End If
                If CDec(txtSearchByOtherPriceMin.Text) >= CDec(txtSearchByOtherPriceMax.Text) Then
                    lblError.Text = "Please enter a minimum value that is less than your maximum value"
                    Exit Sub
                End If
            End If
        End If

        If cblParameters.Items(2).Selected = True Then
            If rblPrice.SelectedIndex = 0 Then
                strIn3 = str0to100 & "And "
            End If
            If rblPrice.SelectedIndex = 1 Then
                strIn3 = str101to200 & " And "
            End If
            If rblPrice.SelectedIndex = 2 Then
                strIn3 = str201to300 & " And "
            End If
            If rblPrice.SelectedIndex = 3 Then
                strIn3 = str300orMore & " And "
            End If
            If rblPrice.SelectedIndex = 4 Then
                strIn3 = strPriceCode1 & txtSearchByOtherPriceMin.Text & strPriceCode2 & txtSearchByOtherPriceMax.Text & strPriceCode3 & "And "
            End If
        End If

        If cblParameters.Items(2).Selected = False Then
            strIn3 = "Amount <> '" & 0.0 & "' And "
        End If

        If cblParameters.Items(3).Selected = True Then
            If txtSearchByTransactionNumber.Text = "" Then
                lblError.Text = "Please enter a transaction number to search by if you want to search by transaction number"
                Exit Sub
            End If
            If Val.CheckDigits(txtSearchByTransactionNumber.Text) = False Then
                lblError.Text = "Please enter a numeric transaction number to search"
                Exit Sub
            End If
        End If
        If cblParameters.Items(3).Selected = True Then
            strIn4 = strTransactionNumberCode1 & txtSearchByTransactionNumber.Text & strTransactionNumberCode2 & " And "
        End If

        If cblParameters.Items(3).Selected = False Then
            strIn4 = "[Transaction Number] <> '" & 0.0 & "' And "
        End If

        'systemdate vs date in row
        If cblParameters.Items(4).Selected = True Then
            If rblDate.SelectedIndex = -1 Then
                lblError.Text = "If you want to search by date, please select a date range to search by"
                Exit Sub
            End If
            If rblDate.SelectedValue = "Custom Date Range" Then
                If txtCustomDateMax.Text = "" Or txtCustomDateMin.Text = "" Then
                    lblError.Text = "Please enter a minimum and a maximum date amount"
                    Exit Sub
                End If
                If Val.CheckDigits(txtCustomDateMax.Text) = -1 Then
                    lblError.Text = "Please enter a positive numeric amount as your maximum date"
                    Exit Sub
                End If
                If Val.CheckDecimal(txtCustomDateMin.Text) = -1 Then
                    lblError.Text = "Please enter a positive numeric amount as your minimum date"
                    Exit Sub
                End If
                If CDec(txtCustomDateMin.Text) >= CDec(txtCustomDateMax.Text) Then
                    lblError.Text = "Please enter a minimum value that is less than your maximum value"
                    Exit Sub
                End If
            End If
        End If

        If cblParameters.Items(4).Selected = True Then
            If rblDate.SelectedIndex = 0 Then
                strIn5 = strDateCode15
            End If
            If rblDate.SelectedIndex = 1 Then
                strIn5 = strDateCode30
            End If
            If rblDate.SelectedIndex = 2 Then
                strIn5 = strDateCode60
            End If
            If rblDate.SelectedIndex = 3 Then
                strIn5 = strDateCodeAll
            End If
            If rblDate.SelectedIndex = 4 Then
                strIn5 = "Date >= #" & datSystemDate.AddDays(-CInt(txtCustomDateMax.Text)) & "# and Date <= #" & datSystemDate.AddDays(-CInt(txtCustomDateMin.Text)) & "#"

            End If
        End If

        If cblParameters.Items(4).Selected = False Then
            strIn5 = strDateCodeAll
        End If

        DBTransactions.Go(strIn1, strIn2, strIn3, strIn4, strIn5)
        Search()
        Clear()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
        'reload gv to original
        Search()
    End Sub

    Protected Sub gvTransactionSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTransactionSearch.SelectedIndexChanged
        Session("TransactionID") = gvTransactionSearch.SelectedRow.Cells(1).Text
        Response.Redirect("CustomerTransactionDetail.aspx")
    End Sub
End Class