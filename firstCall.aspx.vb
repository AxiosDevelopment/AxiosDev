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
          CompanyID.Value = cid.ToString()
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
    rsData = db.GetDataReader("SELECT c.CompanyID, c.CompanyNumber, c.CompanyName, ct.ClientType, c.CompanyPhoneAnswer, ci.CompanyInformation, c.CompanyAdditionalNotes FROM COMPANY c WITH (NOLOCK) INNER JOIN COMPANY_TYPE ct ON ct.ClientTypeID = c.CompanyTypeID INNER JOIN COMPANY_INFO ci ON ci.CompanyID = c.CompanyID WHERE c.CompanyID = " + Id)

    Do While rsData.Read()
      ClientHeader.Text = rsData("CompanyName")
      clientId.Text = rsData("CompanyNumber").ToString()
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

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    Sql.Append("INSERT INTO [dbo].[FIRST_CALL]([FirstCompanyID],[FirstCallDateTime],[FirstReportingParty],[FirstRPRelationshipID],[FirstPersonAuthorizingRemoval],[FirstPARelationship],[FirstDeceasedName],[FirstPrefix],[FirstDateTimeofDeath]")
    Sql.Append(",[FirstPlaceOfDeath],[FirstSSN],[FirstWeight],[FirstDOB],[FirstAddress],[FirstLocationType],[FirstCity],[FirstState],[FirstCounty],[FirstZip],[FirstPhone],[FirstExt],[FirstNextofKin],[FirstRelationshipID]")
    Sql.Append(",[FirstTelephoneofInforKin],[FirstWorkPhoneForKin],[FirstWorkExt],[FirstDoctor],[FirstDoctorPhone],[FirstDatePatientSeen],[FirstCoroner],[FirstFileNumber],[FirstCounselorContacted],[FirstDateContacted]")
    Sql.Append(",[FirstNotes],[FirstOperatorCallNotes],[FirstDelivered],[FirstDateTimeDelivered],[FirstMedNoteBox],[FirstCustCallInfo],[FirstHold])")
    Sql.Append(" VALUES ")
    SQL.Append("(" & CompanyID.Value & ",") '[FirstCompanyID]
    Sql.Append("'" & msgDateTime.Value & "',") '[FirstCallDateTime]
    SQL.Append("'" & reportingName.Text & "',") '[FirstReportingParty]
    SQL.Append("NULL,") '[FirstRPRelationshipID]
    SQL.Append("NULL,") '[FirstPersonAuthorizingRemoval] *****
    SQL.Append("NULL,") '[FirstPARelationship] *****
    SQL.Append("'" & deceasedName.Text & "',") '[FirstDeceasedName]
    SQL.Append("NULL,") '[FirstPrefix] ******
    SQL.Append("'" & dDate.Text & " " & dTime.Text & "',") '[FirstDateTimeofDeath] *****
    SQL.Append("'" & placeOfDeath.Text & "',") '[FirstPlaceOfDeath]
    SQL.Append("'" & ssn.Text & "',") '[FirstSSN]
    SQL.Append("'" & weight.Text & "',") '[FirstWeight]
    SQL.Append("'" & dob.Text & "',") '[FirstDOB]
    SQL.Append("'" & facilityAddr.Text & "',") '[FirstAddress]
    SQL.Append("NULL,") '[FirstLocationType] *****
    SQL.Append("'" & facCity.Text & "',") '[FirstCity]
    SQL.Append("'" & facState.Text & "',") '[FirstState]
    SQL.Append("'" & facilityCounty.Text & "',") '[FirstCounty]
    SQL.Append("'" & facilityZip.Text & "',") '[FirstZip]
    SQL.Append("'" & facilityPhone.Text & "',") '[FirstPhone]
    SQL.Append("'" & phoneExt.Text & "',") '[FirstExt]
    SQL.Append("'" & partyName.Text & "',") '[FirstNextofKin]
    SQL.Append(relationship.SelectedValue & ",") '[FirstRelationshipID]
    SQL.Append("'" & responsiblePhone.Text & "',") '[FirstTelephoneofInforKin]
    SQL.Append("NULL,") '[FirstWorkPhoneForKin]
    SQL.Append("'" & responsiblePhoneExt.Text & "',") '[FirstWorkExt]
    SQL.Append("'" & physicianName.Text & "',") '[FirstDoctor]
    SQL.Append("'" & physicianPhone.Text & "',") '[FirstDoctorPhone]
    SQL.Append("'" & physicianDate.Text & "',") '[FirstDatePatientSeen]
    SQL.Append("'" & coronerName.Text & "',") '[FirstCoroner]
    SQL.Append("'" & caseNumber.Text & "',") '[FirstFileNumber]
    SQL.Append("'" & counselorName.Text & "',") '[FirstCounselorContacted]
    SQL.Append("'" & coronerDate.Text & " " & coronerTime.Text & "',") '[FirstDateContacted]
    SQL.Append("NULL,") '[FirstNotes] *****
    SQL.Append("NULL,") '[FirstOperatorCallNotes] *****
    SQL.Append("0,") '[FirstDelivered] *****
    SQL.Append("NULL,") '[FirstDateTimeDelivered] *****
    SQL.Append("NULL,") '[FirstMedNoteBox] *****
    SQL.Append("NULL,") '[FirstCustCallInfo] *****
    SQL.Append("0)") '[FirstHold] *****

    returnedID = db.GetID(SQL.ToString())

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