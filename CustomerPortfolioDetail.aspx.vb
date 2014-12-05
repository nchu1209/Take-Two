Option Strict On

Public Class CustomerPortfolioDetail
    Inherits System.Web.UI.Page
    Dim DBAccounts As New ClassDBAccounts
    Dim dbstocks As New ClassDBStocks
    Dim mdecTotalStockValue As Decimal
    Dim mdecTotalPortfolioValue As Decimal
    Dim mdecTotalCashValue As Decimal



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim intOrdinary As Integer
        Dim intIndex As Integer
        Dim intMutual As Integer
        'Dim intETF As Integer


        If IsPostBack = False Then
            If Session("CustomerFirstName") Is Nothing Then
                Response.Redirect("CustomerLogin.aspx")
            End If

            DBAccounts.GetAccountByCustomerNumber(Session("CustomerNumber").ToString)

            If DBAccounts.AccountsDataset4.Tables("tblAccounts").Rows.Count = 0 Then
                Response.Redirect("CustomerCreateBankAccount.aspx")
            End If
        End If

        If DBAccounts.GetApprovedStatus(Session("CustomerNumber").ToString) = False Then
            Response.Redirect("CustomerHome.aspx")
        End If


        If gvStockPortion.Rows.Count = 0 Then
            mdecTotalStockValue = 0
            PanelStockPortion.Visible = False
            PanelNoStocks.Visible = True
        Else


            For i = 0 To gvStockPortion.Rows.Count - 1
                mdecTotalStockValue += CDec(gvStockPortion.Rows(i).Cells(4).Text)

                If gvStockPortion.Rows(i).Cells(3).Text = "Ordinary" Or gvStockPortion.Rows(i).Cells(3).Text = "Ordinary Stocks" Then
                    intOrdinary += 1
                End If

                If gvStockPortion.Rows(i).Cells(3).Text = "Index Fund" Then
                    intIndex += 1
                End If

                If gvStockPortion.Rows(i).Cells(3).Text = "Mutual Fund" Then
                    intMutual += 1
                End If

            Next
        End If

        'DBAccounts.GetStockAccountByCustomerNumber2(Session("CustomerNumber").ToString)

        'mdecTotalCashValue = CDec(DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows(0).Item("Balance").ToString)

        'lblBalanced.Text = mdecTotalCashValue.ToString

        If gvCash.Rows(0).Cells(0).Text Is DBNull.Value Then
            lblBalanced.Text = "nada"
            Exit Sub
        End If
        

        mdecTotalCashValue = CDec(gvCash.Rows(0).Cells(0).Text)

        'lblBalanced.Text = mdecTotalCashValue.ToString
        'Exit Sub

        If mdecTotalCashValue = 0 Then
            PanelCash.Visible = False
            PanelNoCash.Visible = True
            mdecTotalCashValue = 0

        End If

        mdecTotalPortfolioValue = mdecTotalStockValue + mdecTotalCashValue


        lblTotalValue.Text = mdecTotalPortfolioValue.ToString("C2")

        DBAccounts.GetStockAccountByCustomerNumber(Session("CustomerNumber").ToString)
        Dim intStockAccountNumber As Integer = CInt(DBAccounts.AccountsDataset.Tables("tblAccounts").Rows(0).Item("AccountNumber"))
        DBAccounts.GetPortfolioByAccountNumber(intStockAccountNumber)

        If intIndex >= 1 And intOrdinary >= 2 And intMutual >= 1 Then
            Session("Balanced") = True
            lblBalanced.Text = "Your account is balanced."
            If DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows.Count = 0 Then
                dbstocks.AddBalancedPortfolio(intStockAccountNumber, "True", mdecTotalStockValue)
            ElseIf DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows.Count = 1 Then
                dbstocks.UpdateBalancedPortfolio(intStockAccountNumber, "True", mdecTotalStockValue)
            End If
        Else
            Session("Balanced") = False
            lblBalanced.Text = "Your account is not balanced."
            If DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows.Count = 0 Then
                dbstocks.AddBalancedPortfolio(intStockAccountNumber, "False", mdecTotalStockValue)
            ElseIf DBAccounts.AccountsDataset10.Tables("tblAccounts").Rows.Count = 1 Then
                dbstocks.UpdateBalancedPortfolio(intStockAccountNumber, "False", mdecTotalStockValue)
            End If
        End If



    End Sub

End Class