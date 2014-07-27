Imports System.Data.SqlClient
Public Class FromAutoSearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rsData As SqlDataReader
        Dim db As dbUtil = New dbUtil()
        Dim strContacts As String = ""
        Dim strDataContacts = ""
        rsData = db.GetDataReader("SELECT CustID, CompanyName FROM ClientUpdate where CompanyName like '%" & Request.QueryString("query") & "%' ORDER BY CompanyName ASC")
        If rsData.HasRows Then
            strContacts = "{""query"":""" & Request.QueryString("query") & ""","
            While rsData.Read
                strDataContacts &= ",{""value"":""" & rsData(1) & """,""data"":""" & rsData(0) & """}"
            End While
            rsData.Close()
            strContacts &= """suggestions"":[" & strDataContacts & "]  }"
            Response.Write(strContacts)
            'Else
            '    Response.Write("{""query"":""Los Angeles"",""suggestions"":[{""value"":""Los Angeles"",""data"":""C-15546""}]  }")
        End If

    End Sub

End Class