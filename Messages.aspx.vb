Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic

Public Class Messages
  Inherits System.Web.UI.Page

  Protected clientGreeting As String = ""
    Protected cid As String = ""
    Public fromNames As String = ""

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim successed As Boolean
        Dim clientId As String
        Dim msgId As Integer

        If Not (Page.IsPostBack) Then

            ' Get Lookup Data
            GetMessageLookup()

            msgId = Convert.ToInt32(Request.QueryString.Get("MsgId"))
            clientId = Request.QueryString.Get("ClientId").ToString()

            If Not String.IsNullOrEmpty(clientId) Then
                cid = clientId
                successed = GetClient(clientId)

                If msgId > 0 Then
                    'Message Exists


                End If
            End If

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
    Private Function GetClient(id As String) As Boolean

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

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="msgId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMessage(msgId As Integer) As Boolean

        Dim db As dbUtil 'access to db functions
        Dim rsData As SqlDataReader

        db = New dbUtil()
        rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM ClientUpdate WITH (NOLOCK) WHERE CustID = " + clientId)

        Do While rsData.Read()

        Loop

        Return True

    End Function

    ''' <summary>
    ''' Event Handler on Submit Message button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub submitMessage_Click(sender As Object, e As EventArgs) Handles submitMessage.Click

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
        Response.Redirect("Main.aspx")

    End Sub

  ''' <summary>
  ''' Save New Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SaveMessage()

  End Sub

  ''' <summary>
  ''' Update Existing Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateMessage()

  End Sub

End Class