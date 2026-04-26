Imports Microsoft.VisualBasic

Public Class clsUtilPwValidator

    Public Function validatePw(ByVal _thisPw As String) As Boolean

        Dim _bol As Boolean = False


        Dim password As String = _thisPw

        Dim hasLength As Boolean = password.Length >= 8
        Dim hasUppercase As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]")
        Dim hasNumber As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[0-9]")
        Dim hasSpecial As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[^A-Za-z0-9]")

        If hasLength AndAlso hasUppercase AndAlso hasNumber AndAlso hasSpecial Then
            _bol = True
        End If


        Return _bol

    End Function

End Class
