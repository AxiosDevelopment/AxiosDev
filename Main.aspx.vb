Imports System.Data.SqlClient

Public Class Main
  Inherits System.Web.UI.Page
  Public DList As String = ""
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    DList = ""
    'rsData = db.GetDataReader("select Top 5 * from DoctorList")
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact FROM ClientUpdate WITH (NOLOCK)")

    'If rsData.HasRows Then
    '  Do While rsData.Read()
    '    DList = DList & rsData(1)
    '  Loop
    'End If

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor
    Do While rsData.Read()
      row = New HtmlTableRow()
      cell = New HtmlTableCell 'With {.InnerText = rsData(0)}
      a = New HtmlAnchor With {.HRef = "#"}
      a.InnerText = rsData(0)
      cell.Controls.Add(a)
      row.Controls.Add(cell)
      cell = New HtmlTableCell 'With {.InnerText = rsData(1)}
      a = New HtmlAnchor With {.HRef = "#"}
      a.InnerText = rsData(1)
      cell.Controls.Add(a)
      row.Controls.Add(cell)
      If Not IsDBNull(rsData(2)) Then
        cell = New HtmlTableCell With {.InnerText = rsData(2)}
      Else
        cell = New HtmlTableCell With {.InnerText = ""}
      End If
      row.Controls.Add(cell)
      ClientTable.Controls.Add(row)
    Loop

  End Sub

End Class