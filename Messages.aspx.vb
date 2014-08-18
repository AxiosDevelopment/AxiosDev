﻿Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic

Public Class Messages
  Inherits System.Web.UI.Page

  Protected clientGreeting As String = ""
    Protected cid As String = ""

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clientId As String
        Dim msgId As Integer

        If Not (Page.IsPostBack) Then

      Try

        ' Get Lookup Data
        GetMessageLookup()

        msgId = Convert.ToInt32(Request.QueryString.Get("MsgId"))
        clientId = Request.QueryString.Get("ClientId").ToString()

        If Not String.IsNullOrEmpty(clientId) Then
          cid = clientId
          MessageID.Value = msgId.ToString()
          GetClient(clientId)

          If msgId > 0 Then
            'Message Exists
            GetMessage(msgId)
          End If
        End If

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LoadingMessagePagePopupError", "messageLoadError()", True)
      End Try

        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMessageLookup()

        Dim db As dbUtil 'access to db functions
        Dim rsData As SqlDataReader

        db = New dbUtil()
        rsData = db.GetDataReader("SELECT QwkMsgID, QwkMsg FROM QwkMsg WITH (NOLOCK) ORDER BY QwkMsg ASC")

        Do While rsData.Read()
            QwkMessage.Items.Add(New ListItem(rsData("QwkMsg").ToString(), rsData("QwkMsgID").ToString()))
            'quickMessage.Items.Add(New ListItem(rsData("QwkMsg").ToString(), rsData("QwkMsgID").ToString()))
        Loop

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
  Private Sub GetClient(id As String)

        Dim db As dbUtil 'access to db functions
        Dim rsData As SqlDataReader

        db = New dbUtil()
        'rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM ClientUpdate WITH (NOLOCK) WHERE CustID = " + clientId)
        rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM CompanyInfo WITH (NOLOCK) WHERE CustID = " + id)

        Do While rsData.Read()
            clientMessageId.InnerHtml = rsData("CustID").ToString()
            clientName.InnerText = rsData("CompanyName")
            clientGreeting = rsData("ClientAnswer")
            clientMainInfo.Value = db.ClearNull(rsData("ClientData"))
        Loop


  End Sub

    ''' <summary>
    ''' 
    ''' </summary>
  ''' <param name="id"></param>
    ''' <remarks></remarks>
  Private Sub GetMessage(id As Integer)

        Dim db As dbUtil 'access to db functions
        Dim rsData As SqlDataReader
    Dim SQL As New StringBuilder()

        db = New dbUtil()

    SQL.Append("SELECT MsgID, MsgDateTime, MsgCustID, MsgDate, MsgTime, MsgTo, MsgFrom, MsgPhone, MsgExt, MsgAltPhone, MsgQwkMsgs, MsgMessage, MsgOperatorNotes, MsgHoldMsg, MsgDeliver ")
    SQL.Append("FROM Msg WITH (NOLOCK) ")
    SQL.Append("WHERE MsgID = " & id.ToString())

    rsData = db.GetDataReader(SQL.ToString())

    Do While rsData.Read()
      MsgTo.Text = If(Not String.IsNullOrEmpty(rsData("MsgTo")), rsData("MsgTo"), String.Empty)
      MsgFrom.Text = If(Not String.IsNullOrEmpty(rsData("MsgFrom")), rsData("MsgFrom"), String.Empty)
      nMsgPhone.Text = If(Not String.IsNullOrEmpty(rsData("MsgPhone")), rsData("MsgPhone"), String.Empty)
      nMsgPhoneX.Text = If(Not String.IsNullOrEmpty(rsData("MsgExt")), rsData("MsgExt"), String.Empty)
      nMsgAlt.Text = If(Not String.IsNullOrEmpty(rsData("MsgAltPhone")), rsData("MsgAltPhone"), String.Empty)
      QwkMessage.SelectedValue = QwkMessage.Items.FindByText(rsData("MsgQwkMsgs")).Value
      Message.Text = If(Not String.IsNullOrEmpty(rsData("MsgMessage")), rsData("MsgMessage"), String.Empty)
      Notes.Text = If(Not String.IsNullOrEmpty(rsData("MsgOperatorNotes")), rsData("MsgOperatorNotes"), String.Empty)
    Loop

  End Sub

    ''' <summary>
    ''' Event Handler on Submit Message button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub submitMessage_Click(sender As Object, e As EventArgs) Handles submitMessage.Click

    If Page.IsValid Then

      Try

        If MessageID.Value = "0" Then
          InsertMessage()
        Else
          UpdateMessage()
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopup", "messagedSaved()", True)

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopupError", "messagedSavedError()", True)

      End Try
    End If

  End Sub

  ''' <summary>
  ''' Save New Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub InsertMessage()

        Dim returnedID As Integer
        Dim SQL As New StringBuilder()
        Dim test As String = ""
        Dim db As dbUtil 'access to db functions
        db = New dbUtil()

        SQL.Append("INSERT INTO [dbo].[Msg] ([MsgDateTime],[MsgCustID],[MsgDate],[MsgTime],[MsgTo],[MsgFrom],[MsgBusiness],[MsgPhone],[MsgExt],[MsgAltPhone],[MsgQwkMsgs],[MsgMessage],[MsgOperatorNotes],[MsgHoldMsg],[MsgDelDate],[MsgDelTime],[MsgDeliver],[MsgOnCall],[MsgProcedure])")
        SQL.Append(" VALUES ")
        SQL.Append("('" & DateTime.Now & "',") 'MsgDateTime
        SQL.Append("'" & Convert.ToInt32(clientMessageId.InnerHtml) & "',") 'MsgCustID
        SQL.Append("'" & Date.Now & "',") 'MsgDate
        SQL.Append("'" & FormatDateTime(DateTime.Now, DateFormat.LongTime) & "',") 'MsgTime
        SQL.Append("'" & MsgTo.Text & "',") 'MsgTo
        SQL.Append("'" & MsgFrom.Text & "',") 'MsgFrom
        SQL.Append("'" & String.Empty & "',") 'MsgBusiness
        SQL.Append("'" & nMsgPhone.Text & "',") 'MsgPhone
        SQL.Append("'" & nMsgPhoneX.Text & "',") 'MsgExt
        SQL.Append("'" & nMsgAlt.Text & "',") 'MsgAltPhone
        SQL.Append("'" & QwkMessage.SelectedItem.Text & "',") 'MsgQwkMsgs
        SQL.Append("'" & Message.Text & "',") 'MsgMessage
        SQL.Append("'" & Notes.Text & "',") 'MsgOperatorNotes
        SQL.Append("1,") 'MsgHoldMsg
        SQL.Append("NULL,") 'MsgDelDate
        SQL.Append("NULL,") 'MsgDelTime
        SQL.Append("0,") 'MsgDeliver
        SQL.Append("NULL,") 'MsgOnCall
        SQL.Append("NULL)") 'MsgProcedure

        returnedID = db.GetID(SQL.ToString())

  End Sub

  ''' <summary>
  ''' Update Existing Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateMessage()

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim test As String = ""
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE Msg SET ")
    SQL.Append("MsgTo = '" & MsgTo.Text & "',")
    SQL.Append("MsgFrom = '" & MsgFrom.Text & "',")
    SQL.Append("MsgPhone = '" & nMsgPhone.Text & "',")
    SQL.Append("MsgExt = '" & nMsgPhoneX.Text & "',")
    SQL.Append("MsgAltPhone = '" & nMsgAlt.Text & "',")
    SQL.Append("MsgQwkMsgs = '" & QwkMessage.SelectedItem.Text & "',")
    SQL.Append("MsgMessage = '" & Message.Text & "',")
    SQL.Append("MsgOperatorNotes = '" & Notes.Text & "',")
    SQL.Append("MsgHoldMsg = 1,")
    SQL.Append("MsgDelDate = NULL,")
    SQL.Append("MsgDelTime = NULL,")
    SQL.Append("MsgDeliver = 0 ")
    SQL.Append("WHERE MsgID = " & MessageID.Value)

    returnedID = db.GetID(SQL.ToString())

  End Sub

End Class