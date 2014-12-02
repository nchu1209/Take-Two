Option Strict On

'Author: Catherine King
'Assignment: ASP2
'Date: 10/4/2014
'Description: Modify customers

Public Class ClassFormat
    Public Function FormatPhone(strIN As String) As String
        'Purpose: Format the phone number output
        'Arguments: string of the phone number from db
        'Returns: the formatted string in phone number format   
        'Author: Catherine King
        'Date: 9/13/14

        Dim dblPhone As Double
        Dim strResult As String

        dblPhone = CDbl(strIN)

        strResult = dblPhone.ToString("(###) ###-####")

        Return strResult
    End Function
End Class
