Public Class ClassEmployeeValidation


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


    'Purpose: to check if the zipcode follows standards
    'Argument: one string
    'return: True or false
    'Author: Leah Carroll
    'Date: 9/23/2014
    Public Function CheckZip(strIn As String) As Boolean
        'all digits and 10 digits long
        ' check length
        'check for the bad
        If strIn.Length = 5 Or strIn.Length = 9 Then
            Return True
        Else
            Return False
        End If

        'check all digits
        'call check 
        If CheckDigits(strIn) = True Then
            Return True
        Else
            Return False
        End If

    End Function


    'Purpose: to check if the string follows the standards for a phone number
    'Argument: one string
    'return: True or false
    'Author: Leah Carroll
    'Date: 9/23/2014
    Public Function CheckPhone(strIn As String) As Boolean
        'all digits and 10 digits long
        ' check length
        'check for the bad
        If strIN.Length <> 10 Then
            Return False
        End If

        'check all digits
        'call check 
        If CheckDigits(strIN) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function CheckEmpType(strIn As String) As Boolean
        'all digits and 10 digits long
        ' check length
        'check for the bad
        If strIn.Length = 3 Then
            Return True
        Else
            Return False
        End If

        'check all digits
        'call check 
        If CheckDigits(strIn) = True Then
            Return True
        Else
            Return False
        End If

        If strIn = "101" Or strIn = "102" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function CheckSSN(strIn As String) As Boolean
        'all digits and 10 digits long
        ' check length
        'check for the bad
        If strIn.Length = 9 Then
            Return True
        Else
            Return False
        End If

        'check all digits
        'call check 
        If CheckDigits(strIn) = True Then
            Return True
        Else
            Return False
        End If

    End Function



End Class
