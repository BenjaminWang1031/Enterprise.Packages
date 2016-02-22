Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim ss = New Enterprise.Component.Log4Net.LogExceptionHelper
        ss.LogException("", "", New Exception("asas"), Nothing)
    End Sub
End Class