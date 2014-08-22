Imports System.Data.SqlClient
Public Class firstCall
  Inherits System.Web.UI.Page

  Protected cid As String = ""

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim clientId As String
    Dim fcId As Integer

    If Not (Page.IsPostBack) Then

      Try

        ' Get Lookup Data
        'GetFirstCallLookup()

        fcId = Convert.ToInt32(Request.QueryString.Get("FirstCallId"))
        clientId = Request.QueryString.Get("ClientId").ToString()

        If Not String.IsNullOrEmpty(clientId) Then
          cid = clientId
          FirstCallID.Value = fcId.ToString()
          GetClient(clientId)

          If fcId > 0 Then
            'Message Exists


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
  Private Sub GetClient(Id As String)

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, ClientType, ClientAnswer, ClientData, AdditionalNotes FROM CompanyInfo WITH (NOLOCK) WHERE CustID = " + Id)

    Do While rsData.Read()
      ClientHeader.Text = rsData("CompanyName")
      clientId.Text = rsData("CustID").ToString()
      clientName.Text = rsData("CompanyName")
    Loop

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="id"></param>
  ''' <param name="fcId"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function GetFirstCall(id As String, fcId As Integer) As Boolean
    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, ClientType, ClientAnswer, ClientData, AdditionalNotes FROM CompanyInfo WITH (NOLOCK) WHERE CustID = " + id)

    Do While rsData.Read()

      If fcId = 0 Then
        msgDate.Text = DateTime.Now()
        msgTime.Text = FormatDateTime(DateTime.Now, DateFormat.LongTime)
      Else

      End If

    Loop

    Return True
  End Function

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub submitMessage_Click(sender As Object, e As EventArgs) Handles submitMessage.Click
    If Page.IsValid Then

      Try

        If FirstCallID.Value = "0" Then
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
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub InsertMessage()

    'Dim returnedID As Integer
    'Dim SQL As New StringBuilder()
    'Dim test As String = ""
    'Dim db As dbUtil 'access to db functions
    'db = New dbUtil()

    'SQL.Append("INSERT INTO [dbo].[FirstCall] ([MsgDateTime],[MsgCustID],[MsgDate],[MsgTime],[MsgTo],[MsgFrom],[MsgBusiness],[MsgPhone],[MsgExt],[MsgAltPhone],[MsgQwkMsgs],[MsgMessage],[MsgOperatorNotes],[MsgHoldMsg],[MsgDelDate],[MsgDelTime],[MsgDeliver],[MsgOnCall],[MsgProcedure])")
    'SQL.Append(" VALUES ")
    'SQL.Append("('" & DateTime.Now & "',") 'MsgDateTime
    'SQL.Append("'" & Convert.ToInt32(clientMessageId.InnerHtml) & "',") 'MsgCustID
    'SQL.Append("'" & Date.Now & "',") 'MsgDate
    'SQL.Append("'" & FormatDateTime(DateTime.Now, DateFormat.LongTime) & "',") 'MsgTime
    'SQL.Append("'" & MsgTo.Text & "',") 'MsgTo
    'SQL.Append("'" & MsgFrom.Text & "',") 'MsgFrom
    'SQL.Append("'" & String.Empty & "',") 'MsgBusiness
    'SQL.Append("'" & nMsgPhone.Text & "',") 'MsgPhone
    'SQL.Append("'" & nMsgPhoneX.Text & "',") 'MsgExt
    'SQL.Append("'" & nMsgAlt.Text & "',") 'MsgAltPhone
    'SQL.Append("'" & QwkMessage.SelectedItem.Text & "',") 'MsgQwkMsgs
    'SQL.Append("'" & Message.Text & "',") 'MsgMessage
    'SQL.Append("'" & Notes.Text & "',") 'MsgOperatorNotes
    'SQL.Append("1,") 'MsgHoldMsg
    'SQL.Append("NULL,") 'MsgDelDate
    'SQL.Append("NULL,") 'MsgDelTime
    'SQL.Append("0,") 'MsgDeliver
    'SQL.Append("NULL,") 'MsgOnCall
    'SQL.Append("NULL)") 'MsgProcedure

    'returnedID = db.GetID(SQL.ToString())

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateMessage()

    'Dim returnedID As Integer
    'Dim SQL As New StringBuilder()
    'Dim db As dbUtil 'access to db functions
    'db = New dbUtil()

    'SQL.Append("UPDATE Msg SET ")
    'SQL.Append("MsgTo = '" & MsgTo.Text & "',")
    'SQL.Append("MsgFrom = '" & MsgFrom.Text & "',")
    'SQL.Append("MsgPhone = '" & nMsgPhone.Text & "',")
    'SQL.Append("MsgExt = '" & nMsgPhoneX.Text & "',")
    'SQL.Append("MsgAltPhone = '" & nMsgAlt.Text & "',")
    'SQL.Append("MsgQwkMsgs = '" & QwkMessage.SelectedItem.Text & "',")
    'SQL.Append("MsgMessage = '" & Message.Text & "',")
    'SQL.Append("MsgOperatorNotes = '" & Notes.Text & "',")
    'SQL.Append("MsgHoldMsg = 1,")
    'SQL.Append("MsgDelDate = NULL,")
    'SQL.Append("MsgDelTime = NULL,")
    'SQL.Append("MsgDeliver = 0 ")
    'SQL.Append("WHERE MsgID = " & MessageID.Value)

    'returnedID = db.GetID(SQL.ToString())

  End Sub
End Class