﻿Imports System.Data.SqlClient

Public Class FirstCalls
  Inherits System.Web.UI.Page

  Protected cid As String = ""

  ''' <summary>
  ''' Page Load Event Handler
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim clientId As String
    Dim fcId As Integer
        If Session("AxiosUser") Is Nothing Then
            Session.Contents.RemoveAll() 'Release any previous session data ...
            Response.Redirect("Login.aspx")
        End If
    If Not (Page.IsPostBack) Then

      Try

        ' Get Lookup Data
        GetRelationshipsLookup()
        GetFacilityTypesLookup()
        GetAreaOfDeathTypesLookup()

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
  ''' Get Client object
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

    'Dim row As HtmlTableRow
    'Dim cell As HtmlTableCell

    If company.Contacts.Count > 1 Then
      'For Each c As Contact In company.Contacts
      '  Select Case c.TypeID
      '    Case 1 'Primary Contact
      '      PrimaryPerson.InnerText = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
      '      PrimaryPhone.InnerText = If(Not String.IsNullOrEmpty(c.Phone), c.Phone, "")

      '    Case 2 'Secondary Contact
      '      SecondaryPerson.InnerText = If(Not String.IsNullOrEmpty(c.Name), c.Name, "")
      '      SecondaryPhone.InnerText = If(Not String.IsNullOrEmpty(c.Phone), c.Phone, "")

      '  End Select
      'Next
      rpContacts.DataSource = company.Contacts
      rpContacts.DataBind()
    Else

    End If


  End Sub

  ''' <summary>
  ''' Get FirstCall object
  ''' </summary>
  ''' <param name="fcId"></param>
  ''' <remarks></remarks>
  Private Sub GetFirstCall(fcId As Integer)

    Dim fcDA As New FirstCallDA
    Dim firstCall As FirstCall

    firstCall = fcDA.GetFirstCall(fcId)

    FirstCallID.Value = firstCall.ID.ToString()
    CompanyID.Value = firstCall.CompanyID.ToString()
    msgDate.Text = FormatDateTime(firstCall.CreatedDateTime, DateFormat.ShortDate)
    msgTime.Text = FormatDateTime(firstCall.CreatedDateTime, DateFormat.LongTime)
    msgDateTime.Value = FormatDateTime(firstCall.CreatedDateTime, DateFormat.GeneralDate)
    reportingName.Text = If(Not String.IsNullOrEmpty(firstCall.ReportingParty), firstCall.ReportingParty, String.Empty)
    reportingPartyTitle.Text = If(Not String.IsNullOrEmpty(firstCall.PARelationship), firstCall.PARelationship, String.Empty)
    reportingPartyPhone.Text = If(Not String.IsNullOrEmpty(firstCall.CallBackPhone), firstCall.CallBackPhone, String.Empty)
    deceasedName.Text = If(Not String.IsNullOrEmpty(firstCall.DeceasedName), firstCall.DeceasedName, String.Empty)
    If FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.ShortDate) = "1/1/0001" Then
      dDate.Text = ""
    Else
      dDate.Text = FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.ShortDate)
    End If
    If FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.LongTime) = #12:00:00 AM# Then
      dTime.Text = ""
    Else
      dTime.Text = FormatDateTime(firstCall.DateTimeOfDeath, DateFormat.LongTime)
    End If
    ssn.Text = If(Not String.IsNullOrEmpty(firstCall.SSN), firstCall.SSN, String.Empty)
    If firstCall.DateOfBirth = #12:00:00 AM# Then
      dob.Text = ""
    Else
      dob.Text = If(Not String.IsNullOrEmpty(firstCall.DateOfBirth), firstCall.DateOfBirth, String.Empty)
    End If
    weight.Text = If(Not String.IsNullOrEmpty(firstCall.Weight), firstCall.Weight, String.Empty)
    placeOfDeath.Text = If(Not String.IsNullOrEmpty(firstCall.PlaceOfDeath), firstCall.PlaceOfDeath, String.Empty)
    facTypes.SelectedValue = facTypes.Items.FindByValue(firstCall.FacilityTypeID).Value
    facilityAddr.Text = If(Not String.IsNullOrEmpty(firstCall.Address), firstCall.Address, String.Empty)
    facCity.Text = If(Not String.IsNullOrEmpty(firstCall.City), firstCall.City, String.Empty)
    facState.Text = If(Not String.IsNullOrEmpty(firstCall.State), firstCall.State, String.Empty)
    facilityCounty.Text = If(Not String.IsNullOrEmpty(firstCall.County), firstCall.County, String.Empty)
    facilityZip.Text = If(Not String.IsNullOrEmpty(firstCall.Zip), firstCall.Zip, String.Empty)
    facilityPhone.Text = If(Not String.IsNullOrEmpty(firstCall.Phone), firstCall.Phone, String.Empty)
    areaOfDeathTypes.SelectedValue = areaOfDeathTypes.Items.FindByValue(firstCall.AreaOfDeathTypeID).Value
    'phoneExt.Text = If(Not String.IsNullOrEmpty(firstCall.PhoneExt), firstCall.PhoneExt, String.Empty)
    partyName.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinName), firstCall.NextOfKinName, String.Empty)
    relationship.SelectedValue = relationship.Items.FindByValue(firstCall.NextOfKinRelationshipID).Value
    responsiblePhone.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinPhone), firstCall.NextOfKinPhone, String.Empty)
    responsibleAltPhone.Text = If(Not String.IsNullOrEmpty(firstCall.NextOfKinWorkPhone), firstCall.NextOfKinWorkPhone, String.Empty)
    physicianName.Text = If(Not String.IsNullOrEmpty(firstCall.Doctor), firstCall.Doctor, String.Empty)
    physicianPhone.Text = If(Not String.IsNullOrEmpty(firstCall.DoctorPhone), firstCall.DoctorPhone, String.Empty)
    If firstCall.DatePatientSeen = #12:00:00 AM# Then
      physicianDate.Text = ""
    Else
      physicianDate.Text = If(Not String.IsNullOrEmpty(firstCall.DatePatientSeen), firstCall.DatePatientSeen, String.Empty)
    End If
    coronerName.Text = If(Not String.IsNullOrEmpty(firstCall.Coroner), firstCall.Coroner, String.Empty)
    caseNumber.Text = If(Not String.IsNullOrEmpty(firstCall.CaseNumber), firstCall.CaseNumber, String.Empty)
    counselorName.Text = If(Not String.IsNullOrEmpty(firstCall.CounselorContacted), firstCall.CounselorContacted, String.Empty)
    If FormatDateTime(firstCall.DateCounselorContacted, DateFormat.ShortDate) = "1/1/0001" Then
      coronerDate.Text = ""
    Else
      coronerDate.Text = FormatDateTime(firstCall.DateCounselorContacted, DateFormat.ShortDate)
    End If
    If FormatDateTime(firstCall.DateCounselorContacted, DateFormat.LongTime) = #12:00:00 AM# Then
      coronerTime.Text = ""
    Else
      coronerTime.Text = FormatDateTime(firstCall.DateCounselorContacted, DateFormat.LongTime)
    End If
    specialInstructionsR.Text = If(Not String.IsNullOrEmpty(firstCall.CustCallInfo), firstCall.CustCallInfo, String.Empty)
    operatorNotes.Text = If(Not String.IsNullOrEmpty(firstCall.OperatorCallNotes), firstCall.OperatorCallNotes, String.Empty)
    If (firstCall.Hold = 1) Then
      RBMessageStatus.SelectedValue = "Hold"
    ElseIf (firstCall.Delivered = 1) Then
      RBMessageStatus.SelectedValue = "Deliver"
    End If

  End Sub

  ''' <summary>
  ''' Event Handler on Submit Message button click
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub submitMessage_Click(sender As Object, e As EventArgs) Handles submitMessage.Click

    If Page.IsValid Then

      Try
        Dim firstCall As New FirstCall
        firstCall = FillFirstCall(FirstCallID.Value)
        'Always do an insert
        If firstCall.ID = "0" Then
          InsertFirstCall(firstCall)
        Else
          InsertFirstCall(firstCall)
          'UpdateFirstCall(firstCall)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopup", "messagedSaved()", True)

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopupError", "messagedSavedError()", True)

      End Try

    End If

  End Sub

  ''' <summary>
  ''' Insert new First Call Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub InsertFirstCall(firstCall As FirstCall)

    Dim fcDA As New FirstCallDA
    Dim id As Integer

    id = fcDA.InsertFirstCall(firstCall)

  End Sub

  ''' <summary>
  ''' Update a First Call Message
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateFirstCall(firstCall As FirstCall)

    Dim fcDA As New FirstCallDA
    Dim id As Integer

    id = fcDA.UpdateFirstCall(firstCall)

  End Sub

  ''' <summary>
  ''' Fill FirstCall object
  ''' </summary>
  ''' <remarks></remarks>
  Private Function FillFirstCall(fcID As Integer) As FirstCall

    Dim firstCall As New FirstCall

    firstCall.ID = fcID
    firstCall.CompanyID = CompanyID.Value '[FirstCompanyID]
    firstCall.CreatedDateTime = FormatDateTime(msgDate.Text + " " + msgTime.Text, DateFormat.GeneralDate) '[FirstCallDateTime]
    firstCall.ReportingParty = reportingName.Text '[FirstReportingParty]
    'SQL.Append("NULL,") '[FirstRPRelationshipID]
    'SQL.Append("NULL,") '[FirstPersonAuthorizingRemoval] *****
    firstCall.PARelationship = reportingPartyTitle.Text '[FirstPARelationship]
    firstCall.CallBackPhone = reportingPartyPhone.Text ' [FirstCallBackPhone]
    firstCall.DeceasedName = deceasedName.Text '[FirstDeceasedName]
    'SQL.Append("NULL,") '[FirstPrefix] ******
    If (Not String.IsNullOrEmpty(dDate.Text) And Not String.IsNullOrEmpty(dTime.Text)) Then
      firstCall.DateTimeOfDeath = FormatDateTime(dDate.Text & " " & dTime.Text, DateFormat.GeneralDate)  '[FirstDateTimeofDeath] *****
    ElseIf (Not String.IsNullOrEmpty(dDate.Text) And String.IsNullOrEmpty(dTime.Text)) Then
      firstCall.DateTimeOfDeath = FormatDateTime(dDate.Text, DateFormat.LongDate)  '[FirstDateTimeofDeath] *****
    End If
    firstCall.PlaceOfDeath = placeOfDeath.Text '[FirstPlaceOfDeath]
    firstCall.FacilityTypeID = facTypes.SelectedValue '[BusinessTypeID]
    firstCall.SSN = ssn.Text '[FirstSSN]
    firstCall.Weight = weight.Text '[FirstWeight]
    If (Not String.IsNullOrEmpty(dob.Text)) Then
      firstCall.DateOfBirth = dob.Text '[FirstDOB]
    End If
    firstCall.Address = facilityAddr.Text  '[FirstAddress]
    'SQL.Append("NULL,") '[FirstLocationType] *****
    firstCall.City = facCity.Text '[FirstCity]
    firstCall.State = facState.Text '[FirstState]
    firstCall.County = facilityCounty.Text '[FirstCounty]
    firstCall.Zip = facilityZip.Text  '[FirstZip]
    firstCall.Phone = facilityPhone.Text '[FirstPhone]
    firstCall.AreaOfDeathTypeID = areaOfDeathTypes.SelectedValue '[AreaOfDeathTypeID]
    'firstCall.PhoneExt = phoneExt.Text '[FirstExt]
    firstCall.NextOfKinName = partyName.Text '[FirstNextofKin]
    firstCall.NextOfKinRelationshipID = relationship.SelectedValue '[FirstRelationshipID]
    firstCall.NextOfKinPhone = responsiblePhone.Text '[FirstTelephoneofInforKin]
    'SQL.Append("NULL,") '[FirstWorkPhoneForKin]
    firstCall.NextOfKinWorkPhone = responsibleAltPhone.Text '[FirstWorkExt]
    firstCall.Doctor = physicianName.Text '[FirstDoctor]
    firstCall.DoctorPhone = physicianPhone.Text  '[FirstDoctorPhone]
    If (Not String.IsNullOrEmpty(physicianDate.Text)) Then
      firstCall.DatePatientSeen = physicianDate.Text '[FirstDatePatientSeen]
    End If
    firstCall.Coroner = coronerName.Text '[FirstCoroner]
    firstCall.CaseNumber = caseNumber.Text '[FirstFileNumber]
    firstCall.CounselorContacted = counselorName.Text '[FirstCounselorContacted]
    If (Not String.IsNullOrEmpty(coronerDate.Text) And Not String.IsNullOrEmpty(coronerTime.Text)) Then
      firstCall.DateCounselorContacted = FormatDateTime(coronerDate.Text & " " & coronerTime.Text, DateFormat.GeneralDate)  '[FirstDateContacted]
    End If
    'SQL.Append("NULL,") '[FirstNotes] *****
    firstCall.OperatorCallNotes = operatorNotes.Text '[FirstOperatorCallNotes]
    If (RBMessageStatus.SelectedValue.ToUpper() = "DELIVER") Then
      firstCall.Delivered = 1 '[FirstDelivered]
      firstCall.DeliveredDateTime = FormatDateTime(DateTime.Now, DateFormat.GeneralDate) '[FirstDateTimeDelivered]
    Else
      firstCall.Delivered = 0 '[FirstDelivered]
    End If
    'SQL.Append("NULL,") '[FirstMedNoteBox] *****
    firstCall.CustCallInfo = specialInstructionsR.Text '[FirstCustCallInfo]
    firstCall.Hold = If(RBMessageStatus.SelectedValue.ToUpper() = "HOLD", 1, 0) '[FirstHold]

    Return firstCall

  End Function

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

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetFacilityTypesLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT BusinessTypeID, BusinessType FROM LK_BUSINESS_TYPE WITH (NOLOCK) ORDER BY BusinessType ASC")

    Do While rsData.Read()
      facTypes.Items.Add(New ListItem(rsData("BusinessType").ToString(), rsData("BusinessTypeID").ToString()))
    Loop

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetAreaOfDeathTypesLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT AreaOfDeathTypeID, AreaOfDeathType FROM LK_AREA_OF_DEATH_TYPE WITH (NOLOCK) ORDER BY AreaOFDeathType ASC")

    Do While rsData.Read()
      areaOfDeathTypes.Items.Add(New ListItem(rsData("AreaOfDeathType").ToString(), rsData("AreaOfDeathTypeID").ToString()))
    Loop

  End Sub
End Class