Imports System.Data.SqlClient

Public Class FirstCalls
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
          GetClient(clientId)

          If fcId > 0 Then
            'First Call Exists
            GetFirstCall(fcId)
          Else
            FirstCallID.Value = fcId.ToString()
            msgDate.Text = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
            msgTime.Text = FormatDateTime(DateTime.Now, DateFormat.LongTime)
            msgDateTime.Value = FormatDateTime(DateTime.Now, DateFormat.GeneralDate)
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

    Dim cDA As New CompanyDA
    Dim company As Company
    company = cDA.GetCompany(Id)

    CompanyID.Value = company.CompanyID
    ClientHeader.Text = company.Name
    clientId.Text = company.Number
    clientName.Text = company.Name

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="fcId"></param>
  ''' <remarks></remarks>
  Private Sub GetFirstCall(fcId As Integer)

    Dim fcDA As New FirstCallDA
    Dim firstCall As FirstCall

    firstCall = fcDA.GetFirstCall(fcId)

    FirstCallID.Value = firstCall.FirstCallID.ToString()
    CompanyID.Value = firstCall.CompanyID.ToString()
    msgDate.Text = FormatDateTime(firstCall.CreatedDateTime, DateFormat.ShortDate)
    msgTime.Text = FormatDateTime(firstCall.CreatedDateTime, DateFormat.LongTime)
    msgDateTime.Value = FormatDateTime(firstCall.CreatedDateTime, DateFormat.GeneralDate)
    reportingName.Text = If(Not String.IsNullOrEmpty(firstCall.ReportingParty), firstCall.ReportingParty, String.Empty)
    deceasedName.Text = If(Not String.IsNullOrEmpty(firstCall.DeceasedName), firstCall.DeceasedName, String.Empty)
    dDate.Text = FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.ShortDate)
    dTime.Text = FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.LongTime)
    ssn.Text = If(Not String.IsNullOrEmpty(firstCall.SSN), firstCall.SSN, String.Empty)
    dob.Text = If(Not String.IsNullOrEmpty(firstCall.DateOfBirth), firstCall.DateOfBirth, String.Empty)
    weight.Text = If(Not String.IsNullOrEmpty(firstCall.Weight), firstCall.Weight, String.Empty)
    placeOfDeath.Text = If(Not String.IsNullOrEmpty(firstCall.PlaceOfDeath), firstCall.PlaceOfDeath, String.Empty)
    facilityAddr.Text = If(Not String.IsNullOrEmpty(firstCall.Address), firstCall.Address, String.Empty)
    facCity.Text = If(Not String.IsNullOrEmpty(firstCall.City), firstCall.City, String.Empty)
    facState.Text = If(Not String.IsNullOrEmpty(firstCall.State), firstCall.State, String.Empty)
    facilityCounty.Text = If(Not String.IsNullOrEmpty(firstCall.County), firstCall.County, String.Empty)
    facilityZip.Text = If(Not String.IsNullOrEmpty(firstCall.Zip), firstCall.Zip, String.Empty)
    facilityPhone.Text = If(Not String.IsNullOrEmpty(firstCall.Phone), firstCall.Phone, String.Empty)
    phoneExt.Text = If(Not String.IsNullOrEmpty(firstCall.PhoneExt), firstCall.PhoneExt, String.Empty)
    partyName.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinName), firstCall.NextOfKinName, String.Empty)
    relationship.SelectedValue = relationship.Items.FindByValue(firstCall.NextOfKinRelationshipID).Value
    responsiblePhone.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinPhone), firstCall.NextOfKinPhone, String.Empty)
    responsiblePhoneExt.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinWorkPhoneExt), firstCall.NextOfKinWorkPhoneExt, String.Empty)
    physicianName.Text = If(Not String.IsNullOrEmpty(firstCall.Doctor), firstCall.Doctor, String.Empty)
    physicianPhone.Text = If(Not String.IsNullOrEmpty(firstCall.DoctorPhone), firstCall.DoctorPhone, String.Empty)
    physicianDate.Text = If(Not String.IsNullOrEmpty(firstCall.DatePatientSeen), firstCall.DatePatientSeen, String.Empty)
    coronerName.Text = If(Not String.IsNullOrEmpty(firstCall.Coroner), firstCall.Coroner, String.Empty)
    caseNumber.Text = If(Not String.IsNullOrEmpty(firstCall.CaseNumber), firstCall.CaseNumber, String.Empty)
    counselorName.Text = If(Not String.IsNullOrEmpty(firstCall.CounselorContacted), firstCall.CounselorContacted, String.Empty)
    coronerDate.Text = FormatDateTime(firstCall.DateCounselorContacted, DateFormat.ShortDate)
    coronerTime.Text = FormatDateTime(firstCall.DateCounselorContacted, DateFormat.LongTime)
    If (firstCall.Hold = 1) Then
      RBMessageStatus.SelectedValue = "Hold"
    ElseIf (firstCall.Delivered = 1) Then
      RBMessageStatus.SelectedValue = "Deliver"
    End If

  End Sub

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
          UpdateFirstCall(FirstCallID.Value)
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

    Dim fcDA As New FirstCallDA
    Dim firstCall As New FirstCall
    Dim id As Integer

    firstCall.CompanyID = CompanyID.Value '[FirstCompanyID]
    firstCall.CreatedDateTime = FormatDateTime(msgDateTime.Value, DateFormat.GeneralDate) '[FirstCallDateTime]
    firstCall.ReportingParty = reportingName.Text '[FirstReportingParty]
    'SQL.Append("NULL,") '[FirstRPRelationshipID]
    'SQL.Append("NULL,") '[FirstPersonAuthorizingRemoval] *****
    'SQL.Append("NULL,") '[FirstPARelationship] *****
    firstCall.DeceasedName = deceasedName.Text '[FirstDeceasedName]
    'SQL.Append("NULL,") '[FirstPrefix] ******
    firstCall.DateTimeOfDeath = FormatDateTime(dDate.Text & " " & dTime.Text, DateFormat.GeneralDate)  '[FirstDateTimeofDeath] *****
    firstCall.PlaceOfDeath = placeOfDeath.Text '[FirstPlaceOfDeath]
    firstCall.SSN = ssn.Text '[FirstSSN]
    firstCall.Weight = weight.Text '[FirstWeight]
    firstCall.DateOfBirth = dob.Text '[FirstDOB]
    firstCall.Address = facilityAddr.Text  '[FirstAddress]
    'SQL.Append("NULL,") '[FirstLocationType] *****
    firstCall.City = facCity.Text '[FirstCity]
    firstCall.State = facState.Text '[FirstState]
    firstCall.County = facilityCounty.Text '[FirstCounty]
    firstCall.Zip = facilityZip.Text  '[FirstZip]
    firstCall.Phone = facilityPhone.Text '[FirstPhone]
    firstCall.PhoneExt = phoneExt.Text '[FirstExt]
    firstCall.NextOfKinName = partyName.Text '[FirstNextofKin]
    firstCall.NextOfKinRelationshipID = relationship.SelectedValue '[FirstRelationshipID]
    firstCall.NextOfKinPhone = responsiblePhone.Text '[FirstTelephoneofInforKin]
    'SQL.Append("NULL,") '[FirstWorkPhoneForKin]
    firstCall.NextOfKinWorkPhoneExt = responsiblePhoneExt.Text '[FirstWorkExt]
    firstCall.Doctor = physicianName.Text '[FirstDoctor]
    firstCall.DoctorPhone = physicianPhone.Text  '[FirstDoctorPhone]
    firstCall.DatePatientSeen = physicianDate.Text '[FirstDatePatientSeen]
    firstCall.Coroner = coronerName.Text '[FirstCoroner]
    firstCall.CaseNumber = caseNumber.Text '[FirstFileNumber]
    firstCall.CounselorContacted = counselorName.Text '[FirstCounselorContacted]
    firstCall.DateCounselorContacted = FormatDateTime(coronerDate.Text & " " & coronerTime.Text, DateFormat.GeneralDate)  '[FirstDateContacted]
    'SQL.Append("NULL,") '[FirstNotes] *****
    'SQL.Append("NULL,") '[FirstOperatorCallNotes] *****
    If (RBMessageStatus.SelectedValue.ToUpper() = "DELIVER") Then
      firstCall.Delivered = 1 '[FirstDelivered]
      firstCall.DeliveredDateTime = FormatDateTime(DateTime.Now, DateFormat.GeneralDate) '[FirstDateTimeDelivered]
    Else
      firstCall.Delivered = 0 '[FirstDelivered]
    End If
    'SQL.Append("NULL,") '[FirstMedNoteBox] *****
    'SQL.Append("NULL,") '[FirstCustCallInfo] *****
    firstCall.Hold = If(RBMessageStatus.SelectedValue.ToUpper() = "HOLD", 1, 0) '[FirstHold]

    id = fcDA.InsertFirstCall(firstCall)

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateFirstCall(fcID As Integer)

    Dim fcDA As New FirstCallDA
    Dim firstCall As New FirstCall
    Dim id As Integer

    firstCall.FirstCallID = fcID
    firstCall.CompanyID = CompanyID.Value '[FirstCompanyID]
    firstCall.CreatedDateTime = FormatDateTime(msgDateTime.Value, DateFormat.GeneralDate) '[FirstCallDateTime]
    firstCall.ReportingParty = reportingName.Text '[FirstReportingParty]
    'SQL.Append("NULL,") '[FirstRPRelationshipID]
    'SQL.Append("NULL,") '[FirstPersonAuthorizingRemoval] *****
    'SQL.Append("NULL,") '[FirstPARelationship] *****
    firstCall.DeceasedName = deceasedName.Text '[FirstDeceasedName]
    'SQL.Append("NULL,") '[FirstPrefix] ******
    firstCall.DateTimeOfDeath = FormatDateTime(dDate.Text & " " & dTime.Text, DateFormat.GeneralDate)  '[FirstDateTimeofDeath] *****
    firstCall.PlaceOfDeath = placeOfDeath.Text '[FirstPlaceOfDeath]
    firstCall.SSN = ssn.Text '[FirstSSN]
    firstCall.Weight = weight.Text '[FirstWeight]
    firstCall.DateOfBirth = dob.Text '[FirstDOB]
    firstCall.Address = facilityAddr.Text  '[FirstAddress]
    'SQL.Append("NULL,") '[FirstLocationType] *****
    firstCall.City = facCity.Text '[FirstCity]
    firstCall.State = facState.Text '[FirstState]
    firstCall.County = facilityCounty.Text '[FirstCounty]
    firstCall.Zip = facilityZip.Text  '[FirstZip]
    firstCall.Phone = facilityPhone.Text '[FirstPhone]
    firstCall.PhoneExt = phoneExt.Text '[FirstExt]
    firstCall.NextOfKinName = partyName.Text '[FirstNextofKin]
    firstCall.NextOfKinRelationshipID = relationship.SelectedValue '[FirstRelationshipID]
    firstCall.NextOfKinPhone = responsiblePhone.Text '[FirstTelephoneofInforKin]
    'SQL.Append("NULL,") '[FirstWorkPhoneForKin]
    firstCall.NextOfKinWorkPhoneExt = responsiblePhoneExt.Text '[FirstWorkExt]
    firstCall.Doctor = physicianName.Text '[FirstDoctor]
    firstCall.DoctorPhone = physicianPhone.Text  '[FirstDoctorPhone]
    firstCall.DatePatientSeen = physicianDate.Text '[FirstDatePatientSeen]
    firstCall.Coroner = coronerName.Text '[FirstCoroner]
    firstCall.CaseNumber = caseNumber.Text '[FirstFileNumber]
    firstCall.CounselorContacted = counselorName.Text '[FirstCounselorContacted]
    firstCall.DateCounselorContacted = FormatDateTime(coronerDate.Text & " " & coronerTime.Text, DateFormat.GeneralDate)  '[FirstDateContacted]
    'SQL.Append("NULL,") '[FirstNotes] *****
    'SQL.Append("NULL,") '[FirstOperatorCallNotes] *****
    firstCall.Delivered = 0 '[FirstDelivered] *****
    'SQL.Append("NULL,") '[FirstDateTimeDelivered] *****
    'SQL.Append("NULL,") '[FirstMedNoteBox] *****
    'SQL.Append("NULL,") '[FirstCustCallInfo] *****
    firstCall.Hold = 0 '[FirstHold] *****

    id = fcDA.UpdateFirstCall(firstCall)

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