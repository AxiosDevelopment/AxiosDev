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
        GetRelationshipsLookup()

        fcId = Convert.ToInt32(Request.QueryString.Get("FirstCallId"))
        clientId = Request.QueryString.Get("ClientId").ToString()

        If Not String.IsNullOrEmpty(clientId) Then
          cid = clientId
          FirstCallID.Value = fcId.ToString()
          GetClient(clientId)

          If fcId > 0 Then
            'Message Exists

          Else
            msgDate.Text = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
            msgTime.Text = FormatDateTime(DateTime.Now, DateFormat.LongTime)
            msgDateTime.Value = DateTime.Now.ToString()
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
          InsertFirstCall()
        Else
          UpdateFirstCall()
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
  Private Sub InsertFirstCall()

    'Dim returnedID As Integer
    'Dim SQL As New StringBuilder()
    'Dim test As String = ""
    'Dim db As dbUtil 'access to db functions
    'db = New dbUtil()

    'SQL.Append("INSERT INTO [dbo].[FirstCall] ([First_CustID],[First_ClientID],[First_CompanyName],[First_CallDateTime],[First_DatRecieved],[First_TimRecieved],[First_TimRecived_AM],[First_TimRecived_PM],[First_ReportingParty],[First_RP_Relationship]")
    'SQL.Append(",[First_Person_Authorizing_Removal],[First_PA_Relationship],[First_DeceasedName],[First_Mr],[First_Mrs],[First_Miss],[First_Ms],[First_DatofDeath],[First_TimofDeath],[First_PlaceOfDeath],[First_SSN],[First_Weight],[First_DOB],[First_Address]")
    'SQL.Append(",[First_Residence],[First_Inpatient],[First_ER],[First_Nursing],[First_City],[First_State],[First_County],[First_Zip],[First_Phone],[First_Ext],[First_NextofKin],[First_Relationship],[First_TelephoneofInforKin],[First_WorkPhoneForKin],[First_WorkExt]")
    'SQL.Append(",[First_Doctor],[First_DoctorPhone],[First_DatPatientSeen],[First_Coroner],[First_FileNumber],[First_CounselorContacted],[First_DatContacted],[First_TimContacted],[First_Notes],[First_OperatorCallNotes],[First_Delivered],[First_DatDelivered]")
    'SQL.Append(",[First_TimDelivered],[First_MedNoteBox],[First_CustCallInfo],[First_Hold])")
    'SQL.Append(" VALUES ")
    'SQL.Append("('" & clientId.Text & "',") 'First_CustID
    'SQL.Append("('" & clientId.Text & "',") 'First_ClientID
    'SQL.Append("'" & clientName.Text & "',") 'First_CompanyName
    'SQL.Append("'" & msgDateTime.Value & "',") 'First_CallDateTime
    'SQL.Append("'" & msgDateTime.Value & "',") '[First_DatRecieved]
    'SQL.Append("'" & msgDateTime.Value & "',") '[First_TimRecieved]
    'SQL.Append("NULL,") '[First_TimRecived_AM]
    'SQL.Append("NULL,") '[First_TimRecived_PM]
    'SQL.Append("'" & reportingName.Text & "',") '[First_ReportingParty]
    'SQL.Append("NULL,") '[First_RP_Relationship]
    'SQL.Append("NULL,") '[First_Person_Authorizing_Removal] *****
    'SQL.Append("NULL,") '[First_PA_Relationship] *****
    'SQL.Append("'" & deceasedName.Text & "',") '[First_DeceasedName]
    'SQL.Append("0,") '[First_Mr]
    'SQL.Append("0,") '[First_Mrs]
    'SQL.Append("0,") '[First_Miss]
    'SQL.Append("0,") '[First_Ms]
    'SQL.Append("'" & dDate.Text & "',") '[First_DatofDeath]
    'SQL.Append("'" & dTime.Text & "',") '[First_TimofDeath]
    'SQL.Append("'" & placeOfDeath.Text & "',") '[First_PlaceOfDeath]
    'SQL.Append("'" & ssn.Text & "',") '[First_SSN]
    'SQL.Append("'" & weight.Text & "',") '[First_Weight]
    'SQL.Append("'" & dob.Text & "',") '[First_DOB]
    'SQL.Append("'" & facilityAddr.Text & "',") '[First_Address]
    'SQL.Append("0,") '[First_Residence]
    'SQL.Append("0,") '[First_Inpatient]
    'SQL.Append("0,") '[First_ER]
    'SQL.Append("0,") '[First_Nursing]
    'SQL.Append("'" & facCity.Text & "',") '[First_City]
    'SQL.Append("'" & facState.Text & "',") '[First_State]
    'SQL.Append("'" & facilityCounty.Text & "',") '[First_County]
    'SQL.Append("'" & facilityZip.Text & "',") '[First_Zip]
    'SQL.Append("'" & facilityPhone.Text & "',") '[First_Phone]
    'SQL.Append("'" & phoneExt.Text & "',") '[First_Ext]
    'SQL.Append("'" & partyName.Text & "',") '[First_NextofKin]
    'SQL.Append("'" & relationship.SelectedItem.Text & "',") '[First_Relationship]
    'SQL.Append("'" & responsiblePhone.Text & "',") '[First_TelephoneofInforKin]
    'SQL.Append("NULL,") '[First_WorkPhoneForKin]
    'SQL.Append("'" & responsiblePhoneExt.Text & "',") '[First_WorkExt]
    'SQL.Append("'" & physicianName.Text & "',") '[First_Doctor]
    'SQL.Append("'" & physicianPhone.Text & "',") '[First_DoctorPhone]
    'SQL.Append("'" & physicianDate.Text & "',") '[First_DatPatientSeen]
    'SQL.Append("'" & coronerName.Text & "',") '[First_Coroner]
    'SQL.Append("'" & caseNumber.Text & "',") '[First_FileNumber]
    'SQL.Append("'" & counselorName.Text & "',") '[First_CounselorContacted]
    'SQL.Append("'" & coronerDate.Text & "',") '[First_DatContacted]
    'SQL.Append("'" & coronerTime.Text & "',") '[First_TimContacted]
    'SQL.Append("NULL)") '[First_Notes]
    'SQL.Append("NULL)") '[First_OperatorCallNotes]
    'SQL.Append("NULL)") '[First_Delivered]
    'SQL.Append("NULL)") '[First_DatDelivered]
    'SQL.Append("NULL)") '[First_TimDelivered]
    'SQL.Append("NULL)") '[First_MedNoteBox]
    'SQL.Append("NULL)") '[First_CustCallInfo]
    'SQL.Append("NULL)") '[First_Hold]

    'returnedID = db.GetID(SQL.ToString())

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateFirstCall()

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

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetRelationshipsLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT RelationshipID, Relationship FROM LK_RELATIONSHIP WITH (NOLOCK) ORDER BY Relationship ASC")

    Do While rsData.Read()
      relationship.Items.Add(New ListItem(rsData("Relationship").ToString(), rsData("RelationshipID").ToString()))
    Loop

  End Sub

End Class