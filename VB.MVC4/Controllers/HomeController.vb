Imports Enterprise.Core.Interface.Log
Imports Enterprise.Component.Log4Net

Public Class HomeController
    Inherits System.Web.Mvc.Controller
    Public Shared MyExceptionHelper As ILogExceptionHelper = New LogExceptionHelper

    Function Index() As ActionResult
        ViewData("Message") = "Modify this template to jump-start your ASP.NET MVC application."
        MyExceptionHelper.LogException("", "", New Exception("asasas"), Nothing)
        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your app description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
End Class
