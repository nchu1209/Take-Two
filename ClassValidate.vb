Option Strict On
'Author: Catherine King 
'Assignment: VB1
'Date: 10/4/2014
'Program Description: Modification system for K-Technology

Public Class ClassValidate
    'create constant so as not to use literals


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
            'Return -1 if value is less than zero
            Return -1
        End If

        'if the code gets this far is it a valid input, so return the input
        Return decInput

    End Function

    Public Function CheckInteger(ByVal strInput As String) As Integer
        'Purpose: validating the string input to make sure it is a positive decimal value
        'Input: any string
        'Returns: either the positive decimal value (or 0 if blank) or -1 if it is NOT a positive decimal number
        'Author: Catherine King
        'Date: 7/30/2014


        'Check for empty strings -- acceptable
        If strInput = "" Then
            Return 0
        End If

        'declare variables
        Dim intInput As Integer

        'check for numeric
        Try
            intInput = Convert.ToInt32(strInput)
        Catch ex As Exception
            'If code runs the statement, the input is not a decimal number, so return -1
            Return -1
        End Try

        If intInput < 0 Then
            'Return -1 if value is less than or equal to zero
            Return -1
        End If

        'if the code gets this far, it is a valid input, so return the input
        Return intInput
    End Function


   

    'code on page 8 of course packet***
    Public Function CheckDigits(ByVal strInput As String) As Boolean
        'Purpose: checks that what is passed into function is numbers btw 0-9
        'Arguments: any string
        'Returns: true if characters are all 0-9, false otherwise. 
        'Author: Catherine King
        'Date: 9/23/2014

        'check to see that each char is 0-9
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strInput) - 1
            'i, where to start, 1 - how many
            strOne = strInput.Substring(i, 1)
            Select Case strOne
                'if the char is 0-9, keep going
                Case "0" To "9"
                    'if the character is anything else, stop looking and return false
                Case Else
                    Return False
            End Select
        Next

        'if we made it thru the loop, return true
        Return True
    End Function

    

    Public Function CheckZip(strInput As String) As Boolean
        'Purpose: checks to make sure input is 5 or 9 digit numbers
        'Arguments: the string input
        'Returns:  true if 5 or 9 character numbers, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        'check length to make sure it is 5 or 9 chara
        If strInput.Length <> 5 Then
            Return False
        End If

        'if correct length, check to make sure each character is a digit
        If CheckDigits(strInput) = False Then
            Return False

        End If

        'If code gets to this point the number is 10 digits and all char are numbers.
        Return True
    End Function

    Public Function CheckDOB(strInput As String) As Boolean
        'Purpose: checks to make sure input is 5 or 9 digit numbers
        'Arguments: the string input
        'Returns:  true if 5 or 9 character numbers, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        'check length to make sure it is 5 or 9 chara
        If strInput.Length <> 4 Then
            Return False
        End If

        'if correct length, check to make sure each character is a digit
        If CheckDigits(strInput) = False Then
            Return False

        End If

        'If code gets to this point the number is 10 digits and all char are numbers.
        Return True
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

    Public Function PrivitizeAccountNumber(ByVal strInput As String) As Boolean
        'Purpose: checks to make sure every character in a string is a letter   
        'Arguments: a string    
        'Returns: true if all letters, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        'check to see that each char is a-z
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strInput) - 5
            'i, where to start, 1 - how many
            strOne = strInput.Substring(i, 1)

        Next

        'if we made it thru the loop, return true
        Return True
    End Function


    Public Function CheckInitial(ByVal strInput As String) As Boolean
        'Purpose: check the initial textbox input to make sure it is one letter
        'Arguments: string input
        'Returns: true if one letter character, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        If strInput.Length <> 1 Then
            'if the input length is more / less than 1, return false
            Return False
        End If

        If CheckLetters(strInput) = False Then
            'if the character input is not a letter, return false
            Return False
        End If

        'input must be 1 letter, return true
        Return True

    End Function


    Public Function CheckPhone(strInput As String) As Boolean
        'Purpose: check that there are 10 digits in the phone number box
        'Arguments:  the string of characters
        'Returns: true if length of phone number is 10 and is all numbers, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        'check length
        If strInput.Length <> 10 Then
            Return False
        End If

        'check to make sure its all digits
        If CheckDigits(strInput) = False Then
            Return False

        End If

        'If code gets to this point the number is 10 digits and all char are numbers.
        Return True
    End Function

    Public Function CheckState(ByVal strInput As String) As Boolean
        'Purpose: checks to see if the input in State textbox is 2 letters
        'Arguments: string input
        'Returns: True if the string is 2 letters, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014

        If strInput.Length <> 2 Then
            'the state needs to be exactly 2 characters
            Return False
        End If

        If CheckLetters(strInput) = False Then
            'the state needs to be letters, so return false
            Return False
        End If

        'the state is 2 letters, so return true.
        Return True
    End Function


    Public Function CheckPasswordCombination(strInput As String) As Boolean
        'Purpose: checks to see if the format is correct... 6-10 digits, starting with a letter, at least one letter and one number
        'Arguments: string input
        'Returns: True if the password is the correct format, false otherwise
        'Author: Catherine King
        'Date: 9/23/2014


        'check length is between 6 and 10 characters
        If strInput.Length < 6 Or strInput.Length > 10 Then
            Return False
        End If

        'check that first chara is letter
        If CheckLetters(strInput.Substring(0, 1)) = False Then
            'not a letter, so return false
            Return False
        End If


        'Check to see that remainder of string has one digit
        Dim i As Integer
        Dim strOne As String
        Dim intDigitCount As Integer

        For i = 1 To strInput.Length - 1
            'i to end of the string, check each character (i, 1) to see if it is a letter/ number or other
            strOne = strInput.Substring(i, 1)
            Select Case strOne.ToLower
                Case "0" To "9"
                    'count the number of digits
                    intDigitCount += 1
                Case "a" To "z"
                    'what goes here?? nothing... keep going
                    'another case to make sure it is a-z
                    'if had to have, say, 3 letters, we would add a count here
                Case Else
                    'to count other characters... can do intSpecialChar
                    Return False
            End Select
        Next


        'was there at least one digit present in the string?
        If intDigitCount > 0 Then
            Return True
        Else
            'will return false if the number of digits is 0
            Return False
        End If



    End Function


    Public Function ExtractNumber(strInput As String) As String
        'Purpose: extracts all numbers from a string and excludes other characters
        'Arguments: string input
        'Returns: only the numbers from a string
        'Author: Catherine King
        'Date: 9/23/2014

        Dim i As Integer
        Dim strOne As String
        Dim strPhoneNumber As String

        strPhoneNumber = ""

        For i = 0 To strInput.Length - 1
            strOne = strInput.Substring(i, 1)
            Select Case strOne
                Case "0" To "9"
                    strPhoneNumber = strPhoneNumber + strOne
                Case Else
                    'do nothing
            End Select

        Next

        Return strPhoneNumber

    End Function


End Class
