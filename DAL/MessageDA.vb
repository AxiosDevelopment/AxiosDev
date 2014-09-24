Imports System.Data.SqlClient

Public Class MessageDA
  Implements IMessage

  ''' <summary>
  ''' Returns a Message based on Message ID
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetMessage(id As String) As Message Implements IMessage.GetMessage

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim SQL As New StringBuilder()
    Dim message As New Message

    db = New dbUtil()

    SQL.Append("SELECT MsgID, MsgDateTime, MsgCompanyID, MsgTo, MsgFrom, MsgBusiness, MsgPhone, MsgAltPhone, MsgQwkMsgs, MsgMessage, MsgOperatorNotes, MsgHoldMsg, MsgDelDateTime, MsgDeliver, MsgOnCall, MsgProcedure ")
    SQL.Append("FROM MESSAGE WITH (NOLOCK) ")
    SQL.Append("WHERE MsgID = " & id.ToString())

    rsData = db.GetDataReader(SQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        message.ID = rsData("MsgID")
        message.CreatedDateTime = FormatDateTime(rsData("MsgDateTime"), DateFormat.LongDate)
        message.CompanyID = rsData("MsgCompanyID")
        message.MsgTo = If(Not String.IsNullOrEmpty(rsData("MsgTo").ToString()), rsData("MsgTo").ToString(), String.Empty)
        message.MsgFrom = If(Not String.IsNullOrEmpty(rsData("MsgFrom").ToString()), rsData("MsgFrom"), String.Empty)
        message.Business = If(Not String.IsNullOrEmpty(rsData("MsgBusiness").ToString()), rsData("MsgBusiness"), String.Empty)
        message.Phone = If(Not String.IsNullOrEmpty(rsData("MsgPhone").ToString()), rsData("MsgPhone"), String.Empty)
        message.AltPhone = If(Not String.IsNullOrEmpty(rsData("MsgAltPhone").ToString()), rsData("MsgAltPhone"), String.Empty)
        message.QwkMsgs = rsData("MsgQwkMsgs")
        message.MsgMessage = If(Not String.IsNullOrEmpty(rsData("MsgMessage").ToString()), rsData("MsgMessage"), String.Empty)
        message.OperatorNotes = If(Not String.IsNullOrEmpty(rsData("MsgOperatorNotes").ToString()), rsData("MsgOperatorNotes"), String.Empty)
        message.Hold = rsData("MsgHoldMsg")
        message.Delivered = Convert.ToByte(rsData("MsgDeliver"))
        If message.Delivered = 1 Then
          message.DelDateTime = FormatDateTime(rsData("MsgDelDateTime"), DateFormat.GeneralDate)
        End If
        message.OnCall = If(Not String.IsNullOrEmpty(rsData("MsgOnCall").ToString()), rsData("MsgOnCall"), String.Empty)
        message.Procedure = If(Not String.IsNullOrEmpty(rsData("MsgProcedure").ToString()), rsData("MsgProcedure"), String.Empty)
      Loop

      rsData.Close()

    End If

    Return message

  End Function

  Public Function GetMessages() As List(Of Message) Implements IMessage.GetMessages

  End Function

  ''' <summary>
  ''' Returns a list of Messages by Company ID
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetMessages(search As String) As List(Of Message) Implements IMessage.GetMessages

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim messages As New List(Of Message)

    rsData = db.GetDataReader("SELECT MsgID, MsgDateTime, MsgCompanyID, MsgTo, MsgFrom, MsgBusiness, MsgPhone, MsgAltPhone, MsgQwkMsgs, MsgMessage, MsgOperatorNotes, MsgHoldMsg, MsgDelDateTime, MsgDeliver, MsgOnCall, MsgProcedure FROM MESSAGE WITH (NOLOCK) WHERE MsgCompanyID = " & search)
    If rsData.HasRows Then

      Do While rsData.Read

        Dim message As New Message

        message.ID = rsData("MsgID")
        message.CreatedDateTime = FormatDateTime(rsData("MsgDateTime"), DateFormat.LongDate)
        message.CompanyID = rsData("MsgCompanyID")
        message.MsgTo = If(Not String.IsNullOrEmpty(rsData("MsgTo").ToString()), rsData("MsgTo").ToString(), String.Empty)
        message.MsgFrom = If(Not String.IsNullOrEmpty(rsData("MsgFrom").ToString()), rsData("MsgFrom"), String.Empty)
        message.Business = If(Not String.IsNullOrEmpty(rsData("MsgBusiness").ToString()), rsData("MsgBusiness"), String.Empty)
        message.Phone = If(Not String.IsNullOrEmpty(rsData("MsgPhone").ToString()), rsData("MsgPhone"), String.Empty)
        message.AltPhone = If(Not String.IsNullOrEmpty(rsData("MsgAltPhone").ToString()), rsData("MsgAltPhone"), String.Empty)
        message.QwkMsgs = If(Not String.IsNullOrEmpty(rsData("MsgQwkMsgs").ToString()), rsData("MsgQwkMsgs"), String.Empty)
        message.MsgMessage = If(Not String.IsNullOrEmpty(rsData("MsgMessage").ToString()), rsData("MsgMessage"), String.Empty)
        message.OperatorNotes = If(Not String.IsNullOrEmpty(rsData("MsgOperatorNotes").ToString()), rsData("MsgOperatorNotes"), String.Empty)
        message.Hold = rsData("MsgHoldMsg")
        message.Delivered = Convert.ToByte(rsData("MsgDeliver"))
        If message.Delivered = 1 Then
          message.DelDateTime = FormatDateTime(rsData("MsgDelDateTime"), DateFormat.GeneralDate)
        End If
        message.OnCall = If(Not String.IsNullOrEmpty(rsData("MsgOnCall").ToString()), rsData("MsgOnCall"), String.Empty)
        message.Procedure = If(Not String.IsNullOrEmpty(rsData("MsgProcedure").ToString()), rsData("MsgProcedure"), String.Empty)

        messages.Add(message)

      Loop

      rsData.Close()

    End If

    Return messages

  End Function

  ''' <summary>
  ''' Inserts a new Message
  ''' </summary>
  ''' <param name="m"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertMessage(m As Message) As Integer Implements IMessage.InsertMessage

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[MESSAGE] ([MsgDateTime],[MsgCompanyID],[MsgTo],[MsgFrom],[MsgBusiness],[MsgPhone],[MsgAltPhone],[MsgQwkMsgs],[MsgMessage],[MsgOperatorNotes],[MsgHoldMsg],[MsgDelDateTime],[MsgDeliver],[MsgOnCall],[MsgProcedure])")
    SQL.Append(" VALUES ")
    SQL.Append("('" & DateTime.Now & "',") 'MsgDateTime
    SQL.Append(m.CompanyID & ",") 'MsgCompanyID
    SQL.Append("'" & m.MsgTo & "',") 'MsgTo
    SQL.Append("'" & m.MsgFrom & "',") 'MsgFrom
    SQL.Append("'" & String.Empty & "',") 'MsgBusiness
    SQL.Append("'" & m.Phone & "',") 'MsgPhone
    SQL.Append("'" & m.AltPhone & "',") 'MsgAltPhone
    SQL.Append("'" & m.QwkMsgs & "',") 'MsgQwkMsgs
    SQL.Append("'" & m.MsgMessage & "',") 'MsgMessage
    SQL.Append("'" & m.OperatorNotes & "',") 'MsgOperatorNotes
    SQL.Append("'" & m.Hold & "',") 'MsgHoldMsg
    If m.Delivered = 1 Then
      SQL.Append("'" & m.DelDateTime & "',") '[FirstDateTimeDelivered]
    Else
      SQL.Append("NULL,") '[FirstDateTimeDelivered]
    End If
    SQL.Append(m.Delivered & ",") '[FirstDelivered]
    SQL.Append("NULL,") 'MsgOnCall
    SQL.Append("NULL)") 'MsgProcedure

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  ''' <summary>
  ''' Updates an existing Message
  ''' </summary>
  ''' <param name="m"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateMessage(m As Message) As Integer Implements IMessage.UpdateMessage

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE MESSAGE SET ")
    SQL.Append("MsgTo = '" & m.MsgTo & "',")
    SQL.Append("MsgFrom = '" & m.MsgFrom & "',")
    SQL.Append("MsgPhone = '" & m.Phone & "',")
    SQL.Append("MsgAltPhone = '" & m.AltPhone & "',")
    SQL.Append("MsgQwkMsgs = '" & m.QwkMsgs & "',")
    SQL.Append("MsgMessage = '" & m.MsgMessage & "',")
    SQL.Append("MsgOperatorNotes = '" & m.OperatorNotes & "',")
    SQL.Append("MsgHoldMsg = '" & m.Hold & "',") 'MsgHoldMsg
    If m.Delivered = 1 Then
      SQL.Append("MsgDelDateTime = '" & m.DelDateTime & "',") '[MsgDelDateTime]
    Else
      SQL.Append("MsgDelDateTime = NULL,") '[MsgDelDateTime]
    End If
    SQL.Append("MsgDeliver = " & m.Delivered & " ") 'MsgDeliver
    SQL.Append("WHERE MsgID = " & m.ID)

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

End Class
