Public Class ClassEmployeeFormat



    'Purpose: to format the phone number correctly
    'Arguments: strInput (the phone number)
    'Returns: str with the phone number formatted properly
    'Author: Leah Carroll
    'Date: 9-18-2014
    Public Function FormatPhone(strInput As String) As String

        Dim DB As New ClassDBEmployee

        'get strIN into dblPhone
        Dim dblPhone As Double = Convert.ToDouble(strInput)
        Dim strResultPhone As String

        'Format dbl phone using .TO String
        strResultPhone = dblPhone.ToString("(###) ###-####")

        'return result
        Return strResultPhone

    End Function

    Public Function FormatPhoneDown(strIn As String) As String
        Dim strPhone As String = ""
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIn) - 1
            'get one character from the string
            strOne = strIn.Substring(i, 1)
            Select Case strOne.ToLower
                'if the character is a-z, then keep going 
                Case "0" To "9"
                    'show that there is a digit present
                    strPhone = strPhone + strOne

                Case Else

            End Select
        Next

        Return strPhone
    End Function

End Class
