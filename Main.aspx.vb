Imports System.Data.SqlClient

Public Class Main
  Inherits System.Web.UI.Page
  Public DList As String = ""

  ''' <summary>
  ''' Page_Load
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'Gets list of clients
    GetClients()

  End Sub

  ''' <summary>
  ''' Gets the list of clients in ClientUpdate table
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClients()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact FROM ClientUpdate WITH (NOLOCK)")

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor
    Do While rsData.Read()
      row = New HtmlTableRow()
      cell = New HtmlTableCell
      a = New HtmlAnchor With {.HRef = "#"}
      a.InnerText = rsData(0)
      cell.Controls.Add(a)
      row.Controls.Add(cell)
      cell = New HtmlTableCell
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