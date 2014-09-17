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

    If Not (Page.IsPostBack) Then

      'Gets list of clients
      GetClients()
      'Get list of messages
      GetMessages()

    End If

  End Sub

  ''' <summary>
  ''' Gets the list of clients in COMPANY table
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClients()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    Try

      db = New dbUtil()
      rsData = db.GetDataReader("SELECT c.CompanyID, c.CompanyNumber, c.CompanyName, ct.ContactName, ct.UpdatedDateTime AS UpdatedDate FROM COMPANY c WITH (NOLOCK) LEFT JOIN CONTACT ct ON ct.CompanyID = c.CompanyID AND ct.IsActive = 1 AND ct.ContactType = 1 ORDER BY c.CompanyNumber ASC")

      Dim row As HtmlTableRow
      Dim cell As HtmlTableCell
      Dim a As HtmlAnchor

      If rsData.HasRows Then
        Do While rsData.Read()
          row = New HtmlTableRow()
          'Create Company column
          cell = New HtmlTableCell
          a = New HtmlAnchor With {.HRef = "Messages.aspx?MsgId=0&ClientId=" + rsData("CompanyID").ToString()}
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
          If Not IsDBNull(rsData("ContactName")) Then
            cell = New HtmlTableCell With {.InnerText = rsData("UpdatedDate")}
          Else
            cell = New HtmlTableCell With {.InnerText = ""}
          End If
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

    Catch ex As Exception

      Dim row As HtmlTableRow
      Dim cell As HtmlTableCell
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "Error Loading Clients"}
      cell.ColSpan = 3
      row.Controls.Add(cell)
      ClientTable.Controls.Add(row)

    End Try

  End Sub

  ''' <summary>
  ''' Gets the list of messages in Msg table
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetMessages()

    Dim mfcDA As New MessageFirstCallBaseDA
    'Dim message As Message
    'Dim firstCall As FirstCall
    Dim allMessageFirstCalls As List(Of MessageFirstCallBase)

    allMessageFirstCalls = mfcDA.GetAllMessagesFirstCallsNotDelivered()

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell
    Dim a As HtmlAnchor

    If allMessageFirstCalls.Count > 0 Then

      For Each m As MessageFirstCallBase In allMessageFirstCalls
        'If TypeOf m Is Message Then

        'ElseIf TypeOf m Is FirstCall Then

        'End If
        row = New HtmlTableRow()
        'Status
        cell = New HtmlTableCell With {.InnerText = m.Status}
        row.Controls.Add(cell)
        'First Call or Message
        If m.MessageType.ToUpper() = "FIRSTCALL" Then
          cell = New HtmlTableCell With {.InnerText = "Yes"}
        Else
          cell = New HtmlTableCell With {.InnerText = "No"}
        End If
        row.Controls.Add(cell)
        'Company Name
        If Not IsDBNull(m.CompanyName) Then
          cell = New HtmlTableCell
          If m.MessageType.ToUpper() = "FIRSTCALL" Then
            a = New HtmlAnchor With {.HRef = "FirstCalls.aspx?FirstCallId=" + m.ID.ToString() + "&ClientId=" + m.CompanyID.ToString()}
          Else
            a = New HtmlAnchor With {.HRef = "Messages.aspx?MsgId=" + m.ID.ToString() + "&ClientId=" + m.CompanyID.ToString()}
          End If
          a.InnerText = m.CompanyName
          cell.Controls.Add(a)
        Else
          cell = New HtmlTableCell With {.InnerText = "No Client Assigned"}
        End If
        row.Controls.Add(cell)
        MessageTable.Controls.Add(row)
      Next

    Else
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "No Messages Found"}
      cell.ColSpan = 3
      row.Controls.Add(cell)
      MessageTable.Controls.Add(row)
    End If

  End Sub

  Private Sub GetMessagesOLD()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim strSQL As New StringBuilder()

    Try

      strSQL.Append("SELECT m.MsgID AS MessageID, 'N/A' AS Status, 'No' AS FirstCall, c.CompanyID AS CompanyID, c.CompanyName AS CompanyName, m.MsgDateTime as CreatedDateTime ")
      strSQL.Append("FROM Msg m WITH (NOLOCK) ")
      strSQL.Append("LEFT JOIN COMPANY c ON m.MsgCustID = c.CompanyNumber ")
      strSQL.Append("WHERE m.MsgDeliver = 0 ")
      strSQL.Append("UNION ")
      strSQL.Append("SELECT fc.FirstCallID as MessageID, 'N/A' AS Status, 'Yes' AS FirstCall, fc.FirstCompanyID AS CompanyID, CompanyName AS CompanyName, fc.FirstCallDateTime as CreatedDateTime ")
      strSQL.Append("FROM FIRST_CALL fc WITH (NOLOCK) ")
      strSQL.Append("LEFT JOIN COMPANY c ON fc.FirstCompanyID = c.CompanyID ")
      strSQL.Append("WHERE fc.FirstDelivered = 0 ")
      strSQL.Append("ORDER BY CreatedDateTime DESC")

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
              a = New HtmlAnchor With {.HRef = "FirstCalls.aspx?FirstCallId=" + rsData("MessageID").ToString() + "&ClientId=" + rsData("CompanyID").ToString()}
            Else
              a = New HtmlAnchor With {.HRef = "Messages.aspx?MsgId=" + rsData("MessageID").ToString() + "&ClientId=" + rsData("CompanyID").ToString()}
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

    Catch ex As Exception

      Dim row As HtmlTableRow
      Dim cell As HtmlTableCell
      row = New HtmlTableRow()
      cell = New HtmlTableCell With {.InnerText = "Error Loading Messages"}
      cell.ColSpan = 3
      row.Controls.Add(cell)
      MessageTable.Controls.Add(row)

    End Try

  End Sub

End Class