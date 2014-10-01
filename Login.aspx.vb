Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Public strMessage As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As dbUtil = New dbUtil()
        Dim rsLogin As SqlDataReader
        'Dim dc As dbContext
        If Not Session("AxiosUser") Is Nothing Then
            Session.Contents.RemoveAll() 'Release any previous session data ...
            Session("LoggedOut") = "Y"
        End If

        Dim UN As String = ""
        Dim PW As String = ""

        If String.IsNullOrEmpty(Request.Form("username")) Or String.IsNullOrEmpty(Request.Form("password")) Then
            'strMessage = "Welcome to the HIVE"
        Else
            UN = Request.Form("username")
            PW = Request.Form("password")
            rsLogin = db.GetDataReader("select LoginID from LOGIN where LoginUserName='" & UN & "' and LoginUserPassword='" & PW & "'")
            If rsLogin.HasRows Then
                rsLogin.Read()
                db.ExecSQL("Update LOGIN set LastLogin='" & Now() & "' where LoginID=" & rsLogin(0))
                Session("AxiosUser") = "XX"
                Response.Redirect("Main.aspx")
            Else

            End If
        End If
    End Sub


End Class