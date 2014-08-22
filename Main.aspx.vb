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
    rsData = db.GetDataReader("SELECT ci.CustID, ci.CompanyName, c.ContactName, MAX(c.CREATEDATETIME) AS CreatedDate FROM CompanyInfo ci WITH (NOLOCK) INNER JOIN CONTACT c ON c.CustID = ci.CustID WHERE ContactType = 1 GROUP BY ci.CustID, ci.CompanyName, c.ContactName ORDER BY CustID ASC")

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor

    If rsData.HasRows Then
      Do While rsData.Read()
        row = New HtmlTableRow()
        'Create Company column
        cell = New HtmlTableCell
        a = New HtmlAnchor With {.HRef = "Messages.aspx?MsgId=0&ClientId=" + rsData("CustID").ToString()}
        a.InnerText = rsData("CompanyName")
        cell.Controls.Add(a)
        row.Controls.Add(cell)
        'Create ContactName column
        If Not IsDBNull(rsData("ContactName")) Then
          cell = New HtmlTableCell With {.InnerText = rsData("ContactName")}
        Else
          cell = New HtmlTableCell With {.InnerText = ""}
        End If
        row.Controls.Add(cell)
        'Create CreatedDate column
        cell = New HtmlTableCell With {.InnerText = rsData("CreatedDate")}
        row.Controls.Add(cell)
        'Add row to table
        ClientTable.Controls.Add(row)
      Loop
    Else
      'Create empty row if no records found
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
    Dim strSQL As New StringBuilder()

    strSQL.Append("SELECT m.MsgID AS MessageID, 'N/A' AS Status, 'No' AS FirstCall, m.MsgCustID AS CustId, ci.CompanyName AS CompanyName ")
    strSQL.Append("FROM Msg m WITH (NOLOCK) ")
    strSQL.Append("LEFT JOIN CompanyInfo ci ")
    strSQL.Append("ON m.MsgCustID = ci.CustID ")
    strSQL.Append("WHERE m.MsgDeliver = 0 ")
    strSQL.Append("UNION ")
    strSQL.Append("SELECT First_CallID as MessageID, 'N/A' AS Status, 'Yes' AS FirstCall, First_CustID AS CustId, First_CompanyName AS CompanyName ")
    strSQL.Append("FROM FirstCall WITH (NOLOCK) ")
    strSQL.Append("WHERE First_Delivered = 0 ")
    strSQL.Append("ORDER BY MessageID ASC")

    db = New dbUtil()
    rsData = db.GetDataReader(strSQL.ToString())

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor

    If rsData.HasRows Then

      Do While rsData.Read()
        row = New HtmlTableRow()
        'Status
        cell = New HtmlTableCell With {.InnerText = rsData("Status")}
        row.Controls.Add(cell)
        'First Call
        cell = New HtmlTableCell With {.InnerText = rsData("FirstCall")}
        row.Controls.Add(cell)
        'Company Name
        If Not IsDBNull(rsData("CompanyName")) Then
          cell = New HtmlTableCell
          If String.Format(rsData("FirstCall")).ToUpper() = "YES" Then
            a = New HtmlAnchor With {.HRef = "firstCall.aspx?FirstCallId=" + rsData("MessageID").ToString() + "&ClientId=" + rsData("CustID").ToString()}
          Else
            a = New HtmlAnchor With {.HRef = "Messages.aspx?MsgId=" + rsData("MessageID").ToString() + "&ClientId=" + rsData("CustID").ToString()}
          End If
          a.InnerText = rsData("CompanyName")
          cell.Controls.Add(a)
        Else
          cell = New HtmlTableCell With {.InnerText = "No Client Assigned"}
        End If
        row.Controls.Add(cell)
        MessageTable.Controls.Add(row)
      Loop

    Else
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "No Messages Found"}
      cell.ColSpan = 3
      row.Controls.Add(cell)
      MessageTable.Controls.Add(row)
    End If

  End Sub

End Class