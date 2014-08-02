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
    'Get list of messages
    GetMessages()

  End Sub

  ''' <summary>
  ''' Gets the list of clients in ClientUpdate table
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClients()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
        rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact FROM CompanyInfo WITH (NOLOCK) ORDER BY CustID ASC")

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor

    If rsData.HasRows Then
      Do While rsData.Read()
        row = New HtmlTableRow()
        cell = New HtmlTableCell
        a = New HtmlAnchor With {.HRef = "~/Messages.aspx?MsgId=0&ClientId=" + rsData("CustID").ToString()}
        a.InnerText = rsData("CustID")
        cell.Controls.Add(a)
        row.Controls.Add(cell)
        cell = New HtmlTableCell
        a = New HtmlAnchor With {.HRef = "~/Messages.aspx??MsgId=0&ClientId=" + rsData("CustID").ToString()}
        a.InnerText = rsData("CompanyName")
        cell.Controls.Add(a)
        row.Controls.Add(cell)
        If Not IsDBNull(rsData("Contact")) Then
          cell = New HtmlTableCell With {.InnerText = rsData("Contact")}
        Else
          cell = New HtmlTableCell With {.InnerText = ""}
        End If
        row.Controls.Add(cell)
        ClientTable.Controls.Add(row)
      Loop
    Else
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "No Clients Found"}
      cell.ColSpan = 3
      row.Controls.Add(cell)
      ClientTable.Controls.Add(row)
    End If


  End Sub

  ''' <summary>
  ''' Gets the list of messages in Msg table
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetMessages()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT m.MsgID, 'N/A' AS Status, 'N/A' AS FirstCall, m.MsgCustID, cu.CompanyName FROM Msg m WITH (NOLOCK) LEFT JOIN ClientUpdate cu ON m.MsgCustID = cu.CustID WHERE m.MsgDeliver = 0 ORDER BY m.MsgID ASC")

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor

    If rsData.HasRows Then

      Do While rsData.Read()
        row = New HtmlTableRow()
        'Message ID
        cell = New HtmlTableCell
        a = New HtmlAnchor With {.HRef = "~/Messages.aspx?MsgId=" + rsData("MsgID").ToString() + "&ClientId=" + rsData("MsgCustID").ToString()}
        a.InnerText = rsData(0)
        cell.Controls.Add(a)
        row.Controls.Add(cell)
        'Status
        cell = New HtmlTableCell With {.InnerText = rsData("Status")}
        row.Controls.Add(cell)
        'First Call
        cell = New HtmlTableCell With {.InnerText = rsData("FirstCall")}
        row.Controls.Add(cell)
        'Company Name
        If Not IsDBNull(rsData("CompanyName")) Then
          cell = New HtmlTableCell With {.InnerText = rsData("CompanyName")}
        Else
          cell = New HtmlTableCell With {.InnerText = ""}
        End If
        row.Controls.Add(cell)
        MessageTable.Controls.Add(row)
      Loop

    Else
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "No Messages Found"}
      cell.ColSpan = 4
      row.Controls.Add(cell)
      MessageTable.Controls.Add(row)
    End If

  End Sub

End Class