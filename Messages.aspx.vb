Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic

Public Class Messages
  Inherits System.Web.UI.Page

  Protected clientGreeting As String = ""
  Protected cid As String = ""
  Protected PrimaryContactName As String = ""
  Protected PrimaryContactInfo As String = ""
  Protected SecondaryContactName As String = ""
  Protected SecondaryContactInfo As String = ""
  Protected AdditionalNotes As String = ""
  Protected ClientInformation As String = ""
  'Private _companyDA As ICompanyDA

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
          ' GetContact(clientId)

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
    Loop

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClient(id As String)

    Dim cDA As New CompanyDA
    Dim company As Company
    company = cDA.GetCompany(id)

    CompanyID.Value = company.CompanyID
    clientMessageId.InnerHtml = company.Number
    clientName.InnerText = company.Name
    clientGreeting = company.PhoneAnswer
    ClientInformation = company.ClientInfo
    AdditionalNotes = company.AdditionalNotes

    For Each c As Contact In company.Contacts
      Select Case c.Type
        Case 1 'Primary Contact
          PrimaryContactName = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
          PrimaryContactInfo = If(Not String.IsNullOrEmpty(c.Information), c.Information, "")
        Case 2 'Secondary Contact
          SecondaryContactName = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
          SecondaryContactInfo = If(Not String.IsNullOrEmpty(c.Information), c.Information, "")

      End Select
    Next

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="id"></param>
  ''' <remarks></remarks>
  Private Sub GetContact(id As String)

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim strSQL As New StringBuilder()

    strSQL.Append("SELECT t1.ContactID, t1.CompanyID, t1.ContactName, t1.ContactInfo, t1.ContactType ")
    strSQL.Append("FROM CONTACT t1 WITH (NOLOCK) ")
    strSQL.Append("WHERE t1.CREATEDATETIME = ")
    strSQL.Append("(SELECT MAX(t2.CREATEDATETIME) ")
    strSQL.Append("FROM CONTACT t2 WITH (NOLOCK) ")
    strSQL.Append("WHERE t2.CompanyID = t1.CompanyID AND t2.ContactType = t1.ContactType) ")
    strSQL.Append("AND t1.CompanyID = " & id + " ")
    strSQL.Append("ORDER BY t1.ContactType ASC")

    db = New dbUtil()
    rsData = db.GetDataReader(strSQL.ToString())
    If rsData.HasRows Then
      Do While rsData.Read()

        Select Case rsData("ContactType")
          Case 1 'Primary Contact
            PrimaryContactName = If(Not String.IsNullOrEmpty(rsData("ContactName").ToString()), rsData("ContactName"), "")
            PrimaryContactInfo = If(Not String.IsNullOrEmpty(rsData("ContactInfo").ToString()), rsData("ContactInfo"), "")
          Case 2 'Secondary Contact
            SecondaryContactName = If(Not String.IsNullOrEmpty(rsData("ContactName").ToString()), rsData("ContactName"), "")
            SecondaryContactInfo = If(Not String.IsNullOrEmpty(rsData("ContactInfo").ToString()), rsData("ContactInfo"), "")

        End Select

      Loop
    End If

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
      MsgTo.Text = If(Not String.IsNullOrEmpty(rsData("MsgTo").ToString()), rsData("MsgTo").ToString(), String.Empty)
      MsgFrom.Text = If(Not String.IsNullOrEmpty(rsData("MsgFrom").ToString()), rsData("MsgFrom"), String.Empty)
      nMsgPhone.Text = If(Not String.IsNullOrEmpty(rsData("MsgPhone").ToString()), rsData("MsgPhone"), String.Empty)
      nMsgPhoneX.Text = If(Not String.IsNullOrEmpty(rsData("MsgExt").ToString()), rsData("MsgExt"), String.Empty)
      nMsgAlt.Text = If(Not String.IsNullOrEmpty(rsData("MsgAltPhone").ToString()), rsData("MsgAltPhone"), String.Empty)
      If Not IsNothing(QwkMessage.Items.FindByText(rsData("MsgQwkMsgs").ToString())) Then
        QwkMessage.Items.FindByText(rsData("MsgQwkMsgs").ToString()).Selected = True
      Else
        QwkMessage.SelectedValue = -1
      End If
      Message.Text = If(Not String.IsNullOrEmpty(rsData("MsgMessage").ToString()), rsData("MsgMessage"), String.Empty)
      Notes.Text = If(Not String.IsNullOrEmpty(rsData("MsgOperatorNotes").ToString()), rsData("MsgOperatorNotes"), String.Empty)

      If (rsData("MsgHoldMsg") = 1) Then
        RBMessageStatus.SelectedValue = "Hold"
      ElseIf (rsData("MsgDeliver") = 1) Then
        RBMessageStatus.SelectedValue = "Deliver"
      End If


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
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "HOLD", "1,", "0,")) 'MsgHoldMsg
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "'" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',", "NULL,")) 'MsgDelDate
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "'" & FormatDateTime(DateTime.Now, DateFormat.LongTime) & "',", "NULL,")) 'MsgDelTime
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "1,", "0,")) 'MsgDeliver
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
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "HOLD", "MsgHoldMsg = 1,", "MsgHoldMsg = 0,")) 'MsgHoldMsg
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "MsgDelDate = '" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',", "MsgDelDate = NULL,")) 'MsgDelDate
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "MsgDelTime = '" & FormatDateTime(DateTime.Now, DateFormat.LongTime) & "',", "MsgDelTime = NULL,")) 'MsgDelTime
    SQL.Append(If(RBMessageStatus.SelectedValue.ToUpper() = "DELIVER", "MsgDeliver = 1 ", "MsgDeliver = 0 ")) 'MsgDeliver
    SQL.Append("WHERE MsgID = " & MessageID.Value)

    returnedID = db.GetID(SQL.ToString())

  End Sub

End Class