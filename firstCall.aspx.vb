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
            'First Call Exists
            GetFirstCall(fcId)
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

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim SQL As New StringBuilder()

    db = New dbUtil()

    SQL.Append("SELECT [FirstCallID],[FirstCompanyID],[FirstCallDateTime],[FirstReportingParty],[FirstRPRelationshipID],[FirstPersonAuthorizingRemoval],[FirstPARelationship],[FirstDeceasedName],[FirstPrefix],[FirstDateTimeofDeath]")
    SQL.Append(",[FirstPlaceOfDeath],[FirstSSN],[FirstWeight],[FirstDOB],[FirstAddress],[FirstLocationType],[FirstCity],[FirstState],[FirstCounty],[FirstZip],[FirstPhone],[FirstExt],[FirstNextofKin],[FirstRelationshipID]")
    SQL.Append(",[FirstTelephoneofInforKin],[FirstWorkPhoneForKin],[FirstWorkExt],[FirstDoctor],[FirstDoctorPhone],[FirstDatePatientSeen],[FirstCoroner],[FirstFileNumber],[FirstCounselorContacted],[FirstDateContacted]")
    SQL.Append(",[FirstNotes],[FirstOperatorCallNotes],[FirstDelivered],[FirstDateTimeDelivered],[FirstMedNoteBox],[FirstCustCallInfo],[FirstHold]")
    SQL.Append("FROM [FIRST_CALL] WITH (NOLOCK) ")
    SQL.Append("WHERE FirstCallID = " & fcId.ToString())

    rsData = db.GetDataReader(SQL.ToString())

    Do While rsData.Read()

      msgDate.Text = FormatDateTime(rsData("FirstCallDateTime"), DateFormat.ShortDate)
      msgTime.Text = FormatDateTime(rsData("FirstCallDateTime"), DateFormat.LongTime)
      reportingName.Text = If(Not String.IsNullOrEmpty(rsData("FirstReportingParty")), rsData("FirstReportingParty"), String.Empty)
      deceasedName.Text = If(Not String.IsNullOrEmpty(rsData("FirstDeceasedName")), rsData("FirstDeceasedName"), String.Empty)
      dDate.Text = FormatDateTime(rsData("FirstDateTimeofDeath"), DateFormat.ShortDate)
      dTime.Text = FormatDateTime(rsData("FirstDateTimeofDeath"), DateFormat.LongTime)
      ssn.Text = If(Not String.IsNullOrEmpty(rsData("FirstSSN")), rsData("FirstSSN"), String.Empty)
      dob.Text = If(Not String.IsNullOrEmpty(rsData("FirstDOB")), rsData("FirstDOB"), String.Empty)
      weight.Text = If(Not String.IsNullOrEmpty(rsData("FirstWeight")), rsData("FirstWeight"), String.Empty)
      placeOfDeath.Text = If(Not String.IsNullOrEmpty(rsData("FirstPlaceOfDeath")), rsData("FirstPlaceOfDeath"), String.Empty)
      facilityAddr.Text = If(Not String.IsNullOrEmpty(rsData("FirstAddress")), rsData("FirstAddress"), String.Empty)
      facCity.Text = If(Not String.IsNullOrEmpty(rsData("FirstCity")), rsData("FirstCity"), String.Empty)
      facState.Text = If(Not String.IsNullOrEmpty(rsData("FirstState")), rsData("FirstState"), String.Empty)
      facilityCounty.Text = If(Not String.IsNullOrEmpty(rsData("FirstCounty")), rsData("FirstCounty"), String.Empty)
      facilityZip.Text = If(Not String.IsNullOrEmpty(rsData("FirstZip")), rsData("FirstZip"), String.Empty)
      facilityPhone.Text = If(Not String.IsNullOrEmpty(rsData("FirstPhone")), rsData("FirstPhone"), String.Empty)
      phoneExt.Text = If(Not String.IsNullOrEmpty(rsData("FirstExt")), rsData("FirstExt"), String.Empty)
      partyName.Text = If(Not String.IsNullOrEmpty(rsData("FirstNextofKin")), rsData("FirstNextofKin"), String.Empty)
      relationship.SelectedValue = relationship.Items.FindByValue(rsData("FirstRelationshipID")).Value
      responsiblePhone.Text = If(Not String.IsNullOrEmpty(rsData("FirstTelephoneofInforKin")), rsData("FirstTelephoneofInforKin"), String.Empty)
      responsiblePhoneExt.Text = If(Not String.IsNullOrEmpty(rsData("FirstWorkExt")), rsData("FirstWorkExt"), String.Empty)
      physicianName.Text = If(Not String.IsNullOrEmpty(rsData("FirstDoctor")), rsData("FirstDoctor"), String.Empty)
      physicianPhone.Text = If(Not String.IsNullOrEmpty(rsData("FirstDoctorPhone")), rsData("FirstDoctorPhone"), String.Empty)
      physicianDate.Text = If(Not String.IsNullOrEmpty(rsData("FirstDatePatientSeen")), rsData("FirstDatePatientSeen"), String.Empty)
      coronerName.Text = If(Not String.IsNullOrEmpty(rsData("FirstCoroner")), rsData("FirstCoroner"), String.Empty)
      caseNumber.Text = If(Not String.IsNullOrEmpty(rsData("FirstFileNumber")), rsData("FirstFileNumber"), String.Empty)
      counselorName.Text = If(Not String.IsNullOrEmpty(rsData("FirstCounselorContacted")), rsData("FirstCounselorContacted"), String.Empty)
      coronerDate.Text = FormatDateTime(rsData("FirstDateContacted"), DateFormat.ShortDate)
      coronerTime.Text = FormatDateTime(rsData("FirstDateContacted"), DateFormat.LongTime)

    Loop

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

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE FIRST_CALL SET ")
    SQL.Append("FirstReportingParty = '" & reportingName.Text & "',") '[FirstReportingParty]
    SQL.Append("FirstRPRelationshipID = NULL,") '[FirstRPRelationshipID]
    SQL.Append("FirstPersonAuthorizingRemoval = NULL,") '[FirstPersonAuthorizingRemoval] *****
    SQL.Append("FirstPARelationship = NULL,") '[FirstPARelationship] *****
    SQL.Append("FirstDeceasedName = '" & deceasedName.Text & "',") '[FirstDeceasedName]
    SQL.Append("FirstPrefix = NULL,") '[FirstPrefix] ******
    SQL.Append("FirstDateTimeofDeath = '" & dDate.Text & " " & dTime.Text & "',") '[FirstDateTimeofDeath] *****
    SQL.Append("FirstPlaceOfDeath = '" & placeOfDeath.Text & "',") '[FirstPlaceOfDeath]
    SQL.Append("FirstSSN = '" & ssn.Text & "',") '[FirstSSN]
    SQL.Append("FirstWeight = '" & weight.Text & "',") '[FirstWeight]
    SQL.Append("FirstDOB = '" & dob.Text & "',") '[FirstDOB]
    SQL.Append("FirstAddress = '" & facilityAddr.Text & "',") '[FirstAddress]
    SQL.Append("FirstLocationType = NULL,") '[FirstLocationType] *****
    SQL.Append("FirstCity = '" & facCity.Text & "',") '[FirstCity]
    SQL.Append("FirstState = '" & facState.Text & "',") '[FirstState]
    SQL.Append("FirstCounty = '" & facilityCounty.Text & "',") '[FirstCounty]
    SQL.Append("FirstZip = '" & facilityZip.Text & "',") '[FirstZip]
    SQL.Append("FirstPhone = '" & facilityPhone.Text & "',") '[FirstPhone]
    SQL.Append("FirstExt = '" & phoneExt.Text & "',") '[FirstExt]
    SQL.Append("FirstNextofKin = '" & partyName.Text & "',") '[FirstNextofKin]
    SQL.Append("FirstRelationshipID = '" & relationship.SelectedValue & "',") '[FirstRelationshipID]
    SQL.Append("FirstTelephoneofInforKin = '" & responsiblePhone.Text & "',") '[FirstTelephoneofInforKin]
    SQL.Append("FirstWorkPhoneForKin = NULL,") '[FirstWorkPhoneForKin]
    SQL.Append("FirstWorkExt = '" & responsiblePhoneExt.Text & "',") '[FirstWorkExt]
    SQL.Append("FirstDoctor = '" & physicianName.Text & "',") '[FirstDoctor]
    SQL.Append("FirstDoctorPhone = '" & physicianPhone.Text & "',") '[FirstDoctorPhone]
    SQL.Append("FirstDatePatientSeen = '" & physicianDate.Text & "',") '[FirstDatePatientSeen]
    SQL.Append("FirstCoroner = '" & coronerName.Text & "',") '[FirstCoroner]
    SQL.Append("FirstFileNumber = '" & caseNumber.Text & "',") '[FirstFileNumber]
    SQL.Append("FirstCounselorContacted = '" & counselorName.Text & "',") '[FirstCounselorContacted]
    SQL.Append("FirstDateContacted = '" & coronerDate.Text & " " & coronerTime.Text & "',") '[FirstDateContacted]
    SQL.Append("FirstNotes = NULL,") '[FirstNotes] *****
    SQL.Append("FirstOperatorCallNotes = NULL,") '[FirstOperatorCallNotes] *****
    SQL.Append("FirstDelivered = 0, ") '[FirstDelivered] *****
    SQL.Append("FirstDateTimeDelivered = NULL,") '[FirstDateTimeDelivered] *****
    SQL.Append("FirstMedNoteBox = NULL,") '[FirstMedNoteBox] *****
    SQL.Append("FirstCustCallInfo = NULL,") '[FirstCustCallInfo] *****
    SQL.Append("FirstHold = 0 ") '[FirstHold] *****
    SQL.Append("WHERE FirstCallID = " & FirstCallID.Value)

    returnedID = db.GetID(SQL.ToString())

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