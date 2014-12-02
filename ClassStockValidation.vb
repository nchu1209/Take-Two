Public Class ClassStockValidation

    'Purpose: to check if the string only has digits
    'Argument: one string
    'return: True or false
    'Author: Leah Carroll
    'Date: 9/23/2014
    Public Function CheckDigits(strIn As String) As Boolean
        'check to see that each character is 0-9
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIn) - 1
            'get one character from the string
            strOne = strIn.Substring(i, 1)
            Select Case strOne
                'if the character is 0-9, then keep going 
                Case "0" To "9"
                    'if the character is anything else, stop looking and return false
                Case Else
                    'if bad data, return false
                    Return False
            End Select
        Next

        'if we made it through the loop, return true
        Return True

    End Function


    Public Function CheckDecimal(ByVal strInput As String) As Decimal
        'Purpose: validating the string input to make sure it is a positive decimal value
        'Input: any string
        'Returns: either the positive decimal value or -1 if it is NOT a positive decimal number
        'Author: Catherine King
        'Date: 7/30/2014

        'declare variables
        Dim decInput As Decimal

        'check for numeric 
        Try
            decInput = Decimal.Parse(strInput, Globalization.NumberStyles.Currency)
        Catch ex As Exception
            'If code runs the statement, the input is not a decimal number, so return -1
            Return -1
        End Try


        'Check for positive
        If decInput < 0 Then
            'Return -1 if value is less than or equal to zero
            Return -1
        End If

        'if the code gets this far is it a valid input, so return the input
        Return decInput

    End Function






    Public Function CheckLetters(ByVal strInput As String) As Boolean
        'Purpose: checks to make sure every character in a string is a letter   
        'Arguments: a string    
        'Returns: true if all letters, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        'check to see that each char is a-z
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strInput) - 1
            'i, where to start, 1 - how many
            strOne = strInput.Substring(i, 1)
            Select Case strOne.ToLower
                Case "a" To "z"
                    'if the char is a-z, then keep going
                Case Else
                    'if the character is anything else, stop looking and return false
                    Return False
            End Select
        Next

        'if we made it thru the loop, return true
        Return True
    End Function



    Public Function CheckTickerSymbol(ByVal strInput As String) As Boolean

        If strInput.Length > 5 Then
            'the symbol needs to be less than 5 characters
            Return False
        End If

        If CheckLetters(strInput) = False Then
            'the symbol needs to be letters, so return false
            Return False
        End If

        'the symbol meets requirements
        Return True
    End Function



    Public Function CheckPrice(strIn As String) As Boolean
        'check however large amount of money and make sure it has two decimals

        If strIn.Length < 0 Then
            Return False
        End If

        If CheckDecimal(strIn) = False Then
            Return False
        End If

        'check other digits to see it they are numbers
        Dim intCountString As Integer = (Len(strIn))
        If CheckDigits(strIn.Substring(intCountString - 2, 2)) = False Then
            Return False
        End If


        'Len(strIn)-4
        If (strIn.Substring((intCountString - 3), 1)) <> "." Then
            Return False
        End If

        Return True

    End Function
End Class
