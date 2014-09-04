Imports System.Data.SqlClient

Public Class MessageFirstCallBaseDA

  Public Function GetAllMessagesFirstCallsNotDelivered() As List(Of MessageFirstCallBase)

    Dim allMessagesFirstCalls As New List(Of MessageFirstCallBase)

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim strSQL As New StringBuilder()


    strSQL.Append("SELECT m.MsgID AS MessageID, 'N/A' AS Status, 'No' AS FirstCall, c.CompanyID AS CompanyID, c.CompanyName AS CompanyName, m.MsgDateTime as CreatedDateTime ")
    strSQL.Append("FROM MESSAGE m WITH (NOLOCK) ")
    strSQL.Append("LEFT JOIN COMPANY c ON m.MsgCompanyID = c.CompanyID ")
    strSQL.Append("WHERE m.MsgDeliver = 0 ")
    strSQL.Append("UNION ")
    strSQL.Append("SELECT fc.FirstCallID as MessageID, 'N/A' AS Status, 'Yes' AS FirstCall, fc.FirstCompanyID AS CompanyID, CompanyName AS CompanyName, fc.FirstCallDateTime as CreatedDateTime ")
    strSQL.Append("FROM FIRST_CALL fc WITH (NOLOCK) ")
    strSQL.Append("LEFT JOIN COMPANY c ON fc.FirstCompanyID = c.CompanyID ")
    strSQL.Append("WHERE fc.FirstDelivered = 0 ")
    strSQL.Append("ORDER BY CreatedDateTime DESC")

    db = New dbUtil()
    rsData = db.GetDataReader(strSQL.ToString())


    If rsData.HasRows Then

      Do While rsData.Read()

        If String.Format(rsData("FirstCall")).ToUpper() = "YES" Then

          Dim fc As New FirstCall
          fc.ID = rsData("MessageID")
          fc.Status = "N/A"
          fc.MessageType = "FirstCall"
          fc.CompanyID = If(Not IsDBNull(rsData("CompanyID")), rsData("CompanyID"), 0)
          fc.CreatedDateTime = FormatDateTime(rsData("CreatedDateTime"), DateFormat.LongDate)
          fc.CompanyName = If(Not IsDBNull(rsData("CompanyName")), rsData("CompanyName"), "No Client Name")

          allMessagesFirstCalls.Add(fc)

        Else

          Dim m As New Message
          m.ID = rsData("MessageID")
          m.Status = "N/A"
          m.MessageType = "Message"
          m.CompanyID = If(Not IsDBNull(rsData("CompanyID")), rsData("CompanyID"), 0)
          m.CreatedDateTime = FormatDateTime(rsData("CreatedDateTime"), DateFormat.LongDate)
          m.CompanyName = If(Not IsDBNull(rsData("CompanyName")), rsData("CompanyName"), "No Client Name")

          allMessagesFirstCalls.Add(m)

        End If

      Loop

      rsData.Close()

    End If

    Return allMessagesFirstCalls

  End Function

End Class
