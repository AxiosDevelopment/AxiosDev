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
      Select Case c.TypeID
        Case 1 'Primary Contact
          PrimaryContactName = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
          PrimaryContactInfo = If(Not String.IsNullOrEmpty(c.Phone), c.Phone, "")
          primaryContactId.Value = If(Not String.IsNullOrEmpty(c.ContactID), c.ContactID, "0")

        Case 2 'Secondary Contact
          SecondaryContactName = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
          SecondaryContactInfo = If(Not String.IsNullOrEmpty(c.Phone), c.Phone, "")
          secondaryContactId.Value = If(Not String.IsNullOrEmpty(c.ContactID), c.ContactID, "0")

      End Select
    Next

    Dim row As HtmlTableRow
    Dim cell As HtmlTableCell

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "Address:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.Address}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "City:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.City}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "State:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.State}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "Phone 1:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.MainTelephone}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "Phone 2:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.MainTelephone2nd}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)

    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "Hours:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.HoursOfOperation}
    row.Controls.Add(cell)
    cInformation.Controls.Add(row)


    row = New HtmlTableRow()
    cell = New HtmlTableCell With {.InnerText = "Client Instructions:"}
    cell.Style.Add("font-weight", "bold")
    row.Controls.Add(cell)
    cell = New HtmlTableCell With {.InnerText = company.InstructionSheet}
    row.Controls.Add(cell)
    cInstructions.Controls.Add(row)

    ' <tr>
    '    <td><strong>Address:</strong></td>
    '    <td>1234 Some Company Address</td>
    '</tr>
    '<tr>
    '    <td><strong>City:</strong></td>
    '    <td>Some Big City</td>
    '</tr>
    '<tr>
    '    <td><strong>State:</strong></td>
    '    <td>CA</td>
    '</tr>
    '  <tr>
    '    <td><strong>Phone 1:</strong></td>
    '    <td>(909)345-3456</td>
    '</tr>
    '  <tr>
    '    <td><strong>Phone 2:</strong></td>
    '    <td>(909)343-3456</td>
    '</tr>
    '  <tr>
    '    <td><strong>Hours:</strong></td>
    '    <td>Monday-Friday 8:00am - 9:00pm</td>
    '</tr>




  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="id"></param>
  ''' <remarks></remarks>
  Private Sub GetMessage(id As Integer)

    Dim mDA As New MessageDA
    Dim message As Message

    message = mDA.GetMessage(id)

    MessageID.Value = message.ID.ToString()
    CompanyID.Value = message.CompanyID.ToString()
    MsgTo.Text = If(Not String.IsNullOrEmpty(message.MsgTo), message.MsgTo, String.Empty)
    MsgFrom.Text = If(Not String.IsNullOrEmpty(message.MsgFrom), message.MsgFrom, String.Empty)
    nMsgPhone.Text = If(Not String.IsNullOrEmpty(message.Phone), message.Phone, String.Empty)
    nMsgAlt.Text = If(Not String.IsNullOrEmpty(message.AltPhone), message.AltPhone, String.Empty)
    If Not IsNothing(QwkMessage.Items.FindByText(message.QwkMsgs.ToString())) Then
      QwkMessage.Items.FindByText(message.QwkMsgs.ToString()).Selected = True
    Else
      QwkMessage.SelectedValue = -1
    End If
    MessageText.Text = If(Not String.IsNullOrEmpty(message.MsgMessage), message.MsgMessage, String.Empty)
    Notes.Text = If(Not String.IsNullOrEmpty(message.OperatorNotes), message.OperatorNotes, String.Empty)
    If (message.Hold = 1) Then
      RBMessageStatus.SelectedValue = "Hold"
    ElseIf (message.Delivered = 1) Then
      RBMessageStatus.SelectedValue = "Deliver"
    End If

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
        Dim message As New Message
        message = FillMessage(MessageID.Value)
        If MessageID.Value = "0" Then
          InsertMessage(message)
        Else
          UpdateMessage(message)
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
  Private Sub InsertMessage(m As Message)

    Dim mDA As New MessageDA
    Dim id As Integer

    id = mDA.InsertMessage(m)

  End Sub

  ''' <summary>
  ''' Update Existing Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateMessage(m As Message)

    Dim mDA As New MessageDA
    Dim id As Integer

    id = mDA.UpdateMessage(m)

  End Sub


  Private Function FillMessage(mId As Integer) As Message

    Dim message As New Message

    message.ID = mId
    message.CompanyID = CompanyID.Value
    message.MsgTo = MsgTo.Text
    message.MsgFrom = MsgFrom.Text
    message.Phone = nMsgPhone.Text
    message.AltPhone = nMsgAlt.Text
    message.QwkMsgs = QwkMessage.SelectedItem.Text
    message.MsgMessage = MessageText.Text
    message.OperatorNotes = Notes.Text
    message.Hold = (If(RBMessageStatus.SelectedValue.ToUpper() = "HOLD", "1", "0")) 'MsgHoldMsg
    If (RBMessageStatus.SelectedValue.ToUpper() = "DELIVER") Then
      message.Delivered = 1 '[MsgDelivered]
      message.DelDateTime = FormatDateTime(DateTime.Now, DateFormat.GeneralDate) '[MsgDelDateTime]
    Else
      message.Delivered = 0 '[MsgDelivered]
    End If

    Return message

  End Function

End Class