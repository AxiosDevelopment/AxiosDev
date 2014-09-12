
Imports System.Data.SqlClient
Public Class Login
    Inherits System.Web.UI.Page
    Public strMessage As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As dbUtil = New dbUtil()
        Dim dc As dbContext
        If Not Session("BHUser") Is Nothing Then
            Session.Contents.RemoveAll() 'Release any previous session data ...
            Session("LoggedOut") = "Y"
        End If

        dc = New dbContext(Session, Request, db) '... and establish a new Data Context

        Dim UN As String = ""
        Dim PW As String = ""

        If String.IsNullOrEmpty(Request.Form("UN")) Or String.IsNullOrEmpty(Request.Form("PW")) Then
            strMessage = "Welcome to the HIVE"
        Else
            UN = Request.Form("UN")
            PW = Request.Form("PW")
            If dc.UserIsAuthorized(UN, PW) Then
                dc.Save()
                Response.Redirect("Beehive_Buzzbox.aspx")
            Else
                strMessage = "Enter the correct  User Name and Password"
            End If
        End If
    End Sub
End Class
