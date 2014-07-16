Imports System.Data.SqlClient

Public Class Main
    Inherits System.Web.UI.Page
    Public DList As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As dbUtil 'access to db functions
        Dim rsData As SqlDataReader

        db = New dbUtil()
        Dlist = ""
        rsData = db.GetDataReader("select Top 5 * from DoctorList")
        If rsData.HasRows Then
            Do While rsData.Read()
                DList = DList & rsData(1)
            Loop
        End If
    End Sub

End Class