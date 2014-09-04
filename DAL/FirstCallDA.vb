Imports System.Data.SqlClient

Public Class FirstCallDA
  Implements IFirstCallDA

  ''' <summary>
  ''' Gets First Call based on call id
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetFirstCall(id As String) As FirstCall Implements IFirstCallDA.GetFirstCall

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim SQL As New StringBuilder()
    Dim firstCall As New FirstCall

    db = New dbUtil()

    SQL.Append("SELECT [FirstCallID],[FirstCompanyID],[FirstCallDateTime],[FirstReportingParty],[FirstRPRelationshipID],[FirstPersonAuthorizingRemoval],[FirstPARelationship],[FirstDeceasedName],[FirstPrefix],[FirstDateTimeofDeath]")
    SQL.Append(",[FirstPlaceOfDeath],[FirstSSN],[FirstWeight],[FirstDOB],[FirstAddress],[FirstLocationType],[FirstCity],[FirstState],[FirstCounty],[FirstZip],[FirstPhone],[FirstExt],[FirstNextofKin],[FirstRelationshipID]")
    SQL.Append(",[FirstTelephoneofInforKin],[FirstWorkPhoneForKin],[FirstWorkExt],[FirstDoctor],[FirstDoctorPhone],[FirstDatePatientSeen],[FirstCoroner],[FirstFileNumber],[FirstCounselorContacted],[FirstDateContacted]")
    SQL.Append(",[FirstNotes],[FirstOperatorCallNotes],[FirstDelivered],[FirstDateTimeDelivered],[FirstMedNoteBox],[FirstCustCallInfo],[FirstHold]")
    SQL.Append("FROM [FIRST_CALL] WITH (NOLOCK) ")
    SQL.Append("WHERE FirstCallID = " & id.ToString())

    rsData = db.GetDataReader(SQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        firstCall.ID = rsData("FirstCallID")
        firstCall.CompanyID = rsData("FirstCompanyID")
        firstCall.CreatedDateTime = FormatDateTime(rsData("FirstCallDateTime"), DateFormat.LongDate)
        firstCall.ReportingParty = If(Not String.IsNullOrEmpty(rsData("FirstReportingParty")), rsData("FirstReportingParty"), String.Empty)
        firstCall.DeceasedName = If(Not String.IsNullOrEmpty(rsData("FirstDeceasedName")), rsData("FirstDeceasedName"), String.Empty)
        firstCall.DateTimeOfDeath = If(Not String.IsNullOrEmpty(rsData("FirstDateTimeofDeath").ToString()), FormatDateTime(rsData("FirstDateTimeofDeath"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.SSN = If(Not String.IsNullOrEmpty(rsData("FirstSSN").ToString()), rsData("FirstSSN"), String.Empty)
        firstCall.DateOfBirth = If(Not String.IsNullOrEmpty(rsData("FirstDOB").ToString()), FormatDateTime(rsData("FirstDOB"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.Weight = If(Not String.IsNullOrEmpty(rsData("FirstWeight").ToString()), rsData("FirstWeight"), 0)
        firstCall.PlaceOfDeath = If(Not String.IsNullOrEmpty(rsData("FirstPlaceOfDeath").ToString()), rsData("FirstPlaceOfDeath"), String.Empty)
        firstCall.Address = If(Not String.IsNullOrEmpty(rsData("FirstAddress").ToString()), rsData("FirstAddress"), String.Empty)
        firstCall.City = If(Not String.IsNullOrEmpty(rsData("FirstCity").ToString()), rsData("FirstCity"), String.Empty)
        firstCall.State = If(Not String.IsNullOrEmpty(rsData("FirstState").ToString()), rsData("FirstState"), String.Empty)
        firstCall.County = If(Not String.IsNullOrEmpty(rsData("FirstCounty").ToString()), rsData("FirstCounty"), String.Empty)
        firstCall.Zip = If(Not String.IsNullOrEmpty(rsData("FirstZip").ToString()), rsData("FirstZip"), String.Empty)
        firstCall.Phone = If(Not String.IsNullOrEmpty(rsData("FirstPhone").ToString()), rsData("FirstPhone"), String.Empty)
        firstCall.PhoneExt = If(Not String.IsNullOrEmpty(rsData("FirstExt").ToString()), rsData("FirstExt"), String.Empty)
        firstCall.NextOfKinName = If(Not String.IsNullOrEmpty(rsData("FirstNextofKin").ToString()), rsData("FirstNextofKin"), String.Empty)
        firstCall.NextOfKinRelationshipID = If(Not String.IsNullOrEmpty(rsData("FirstRelationshipID").ToString()), rsData("FirstRelationshipID"), -1)
        firstCall.NextOfKinPhone = If(Not String.IsNullOrEmpty(rsData("FirstTelephoneofInforKin").ToString()), rsData("FirstTelephoneofInforKin"), String.Empty)
        firstCall.NextOfKinWorkPhoneExt = If(Not String.IsNullOrEmpty(rsData("FirstWorkExt").ToString()), rsData("FirstWorkExt"), String.Empty)
        firstCall.Doctor = If(Not String.IsNullOrEmpty(rsData("FirstDoctor").ToString()), rsData("FirstDoctor"), String.Empty)
        firstCall.DoctorPhone = If(Not String.IsNullOrEmpty(rsData("FirstDoctorPhone").ToString()), rsData("FirstDoctorPhone"), String.Empty)
        firstCall.DatePatientSeen = If(Not String.IsNullOrEmpty(rsData("FirstDatePatientSeen").ToString()), rsData("FirstDatePatientSeen"), Date.MinValue)
        firstCall.Coroner = If(Not String.IsNullOrEmpty(rsData("FirstCoroner").ToString()), rsData("FirstCoroner"), String.Empty)
        firstCall.CaseNumber = If(Not String.IsNullOrEmpty(rsData("FirstFileNumber").ToString()), rsData("FirstFileNumber"), String.Empty)
        firstCall.CounselorContacted = If(Not String.IsNullOrEmpty(rsData("FirstCounselorContacted").ToString()), rsData("FirstCounselorContacted"), String.Empty)
        firstCall.DateCounselorContacted = If(Not String.IsNullOrEmpty(rsData("FirstDateContacted").ToString()), FormatDateTime(rsData("FirstDateContacted"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.Hold = Convert.ToByte(rsData("FirstHold"))
        firstCall.Delivered = Convert.ToByte(rsData("FirstDelivered"))
        If firstCall.Delivered = 1 Then
          firstCall.DeliveredDateTime = FormatDateTime(rsData("FirstDateTimeDelivered"), DateFormat.GeneralDate)
        End If
      Loop

      rsData.Close()

    End If

    Return firstCall

  End Function

  Public Function GetFirstCalls() As List(Of FirstCall) Implements IFirstCallDA.GetFirstCalls

  End Function

  ''' <summary>
  ''' Returns a list of First Calls by Company ID
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetFirstCalls(search As String) As List(Of FirstCall) Implements IFirstCallDA.GetFirstCalls

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim SQL As New StringBuilder()
    Dim firstCalls As New List(Of FirstCall)

    SQL.Append("SELECT [FirstCallID],[FirstCompanyID],[FirstCallDateTime],[FirstReportingParty],[FirstRPRelationshipID],[FirstPersonAuthorizingRemoval],[FirstPARelationship],[FirstDeceasedName],[FirstPrefix],[FirstDateTimeofDeath]")
    SQL.Append(",[FirstPlaceOfDeath],[FirstSSN],[FirstWeight],[FirstDOB],[FirstAddress],[FirstLocationType],[FirstCity],[FirstState],[FirstCounty],[FirstZip],[FirstPhone],[FirstExt],[FirstNextofKin],[FirstRelationshipID]")
    SQL.Append(",[FirstTelephoneofInforKin],[FirstWorkPhoneForKin],[FirstWorkExt],[FirstDoctor],[FirstDoctorPhone],[FirstDatePatientSeen],[FirstCoroner],[FirstFileNumber],[FirstCounselorContacted],[FirstDateContacted]")
    SQL.Append(",[FirstNotes],[FirstOperatorCallNotes],[FirstDelivered],[FirstDateTimeDelivered],[FirstMedNoteBox],[FirstCustCallInfo],[FirstHold]")
    SQL.Append("FROM [FIRST_CALL] WITH (NOLOCK) ")
    SQL.Append("WHERE FirstCompanyID = " & search.ToString())

    rsData = db.GetDataReader(SQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read

        Dim firstCall As New FirstCall

        firstCall.ID = rsData("FirstCallID")
        firstCall.CompanyID = rsData("FirstCompanyID")
        firstCall.CreatedDateTime = FormatDateTime(rsData("FirstCallDateTime"), DateFormat.LongDate)
        firstCall.ReportingParty = If(Not String.IsNullOrEmpty(rsData("FirstReportingParty")), rsData("FirstReportingParty"), String.Empty)
        firstCall.DeceasedName = If(Not String.IsNullOrEmpty(rsData("FirstDeceasedName")), rsData("FirstDeceasedName"), String.Empty)
        firstCall.DateTimeOfDeath = If(Not String.IsNullOrEmpty(rsData("FirstDateTimeofDeath").ToString()), FormatDateTime(rsData("FirstDateTimeofDeath"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.SSN = If(Not String.IsNullOrEmpty(rsData("FirstSSN").ToString()), rsData("FirstSSN"), String.Empty)
        firstCall.DateOfBirth = If(Not String.IsNullOrEmpty(rsData("FirstDOB").ToString()), FormatDateTime(rsData("FirstDOB"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.Weight = If(Not String.IsNullOrEmpty(rsData("FirstWeight").ToString()), rsData("FirstWeight"), 0)
        firstCall.PlaceOfDeath = If(Not String.IsNullOrEmpty(rsData("FirstPlaceOfDeath").ToString()), rsData("FirstPlaceOfDeath"), String.Empty)
        firstCall.Address = If(Not String.IsNullOrEmpty(rsData("FirstAddress").ToString()), rsData("FirstAddress"), String.Empty)
        firstCall.City = If(Not String.IsNullOrEmpty(rsData("FirstCity").ToString()), rsData("FirstCity"), String.Empty)
        firstCall.State = If(Not String.IsNullOrEmpty(rsData("FirstState").ToString()), rsData("FirstState"), String.Empty)
        firstCall.County = If(Not String.IsNullOrEmpty(rsData("FirstCounty").ToString()), rsData("FirstCounty"), String.Empty)
        firstCall.Zip = If(Not String.IsNullOrEmpty(rsData("FirstZip").ToString()), rsData("FirstZip"), String.Empty)
        firstCall.Phone = If(Not String.IsNullOrEmpty(rsData("FirstPhone").ToString()), rsData("FirstPhone"), String.Empty)
        firstCall.PhoneExt = If(Not String.IsNullOrEmpty(rsData("FirstExt").ToString()), rsData("FirstExt"), String.Empty)
        firstCall.NextOfKinName = If(Not String.IsNullOrEmpty(rsData("FirstNextofKin").ToString()), rsData("FirstNextofKin"), String.Empty)
        firstCall.NextOfKinRelationshipID = If(Not String.IsNullOrEmpty(rsData("FirstRelationshipID").ToString()), rsData("FirstRelationshipID"), -1)
        firstCall.NextOfKinPhone = If(Not String.IsNullOrEmpty(rsData("FirstTelephoneofInforKin").ToString()), rsData("FirstTelephoneofInforKin"), String.Empty)
        firstCall.NextOfKinWorkPhoneExt = If(Not String.IsNullOrEmpty(rsData("FirstWorkExt").ToString()), rsData("FirstWorkExt"), String.Empty)
        firstCall.Doctor = If(Not String.IsNullOrEmpty(rsData("FirstDoctor").ToString()), rsData("FirstDoctor"), String.Empty)
        firstCall.DoctorPhone = If(Not String.IsNullOrEmpty(rsData("FirstDoctorPhone").ToString()), rsData("FirstDoctorPhone"), String.Empty)
        firstCall.DatePatientSeen = If(Not String.IsNullOrEmpty(rsData("FirstDatePatientSeen").ToString()), rsData("FirstDatePatientSeen"), Date.MinValue)
        firstCall.Coroner = If(Not String.IsNullOrEmpty(rsData("FirstCoroner").ToString()), rsData("FirstCoroner"), String.Empty)
        firstCall.CaseNumber = If(Not String.IsNullOrEmpty(rsData("FirstFileNumber").ToString()), rsData("FirstFileNumber"), String.Empty)
        firstCall.CounselorContacted = If(Not String.IsNullOrEmpty(rsData("FirstCounselorContacted").ToString()), rsData("FirstCounselorContacted"), String.Empty)
        firstCall.DateCounselorContacted = If(Not String.IsNullOrEmpty(rsData("FirstDateContacted").ToString()), FormatDateTime(rsData("FirstDateContacted"), DateFormat.GeneralDate), Date.MinValue)
        firstCall.Hold = Convert.ToByte(rsData("FirstHold"))
        firstCall.Delivered = Convert.ToByte(rsData("FirstDelivered"))
        If firstCall.Delivered = 1 Then
          firstCall.DeliveredDateTime = FormatDateTime(rsData("FirstDateTimeDelivered"), DateFormat.GeneralDate)
        End If

        firstCalls.Add(firstCall)

      Loop

      rsData.Close()

    End If

    Return firstCalls

  End Function

  ''' <summary>
  ''' Inserts new first call
  ''' </summary>
  ''' <param name="fc"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertFirstCall(fc As FirstCall) As Integer Implements IFirstCallDA.InsertFirstCall

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[FIRST_CALL]([FirstCompanyID],[FirstCallDateTime],[FirstReportingParty],[FirstRPRelationshipID],[FirstPersonAuthorizingRemoval],[FirstPARelationship],[FirstDeceasedName],[FirstPrefix],[FirstDateTimeofDeath]")
    SQL.Append(",[FirstPlaceOfDeath],[FirstSSN],[FirstWeight],[FirstDOB],[FirstAddress],[FirstLocationType],[FirstCity],[FirstState],[FirstCounty],[FirstZip],[FirstPhone],[FirstExt],[FirstNextofKin],[FirstRelationshipID]")
    SQL.Append(",[FirstTelephoneofInforKin],[FirstWorkPhoneForKin],[FirstWorkExt],[FirstDoctor],[FirstDoctorPhone],[FirstDatePatientSeen],[FirstCoroner],[FirstFileNumber],[FirstCounselorContacted],[FirstDateContacted]")
    SQL.Append(",[FirstNotes],[FirstOperatorCallNotes],[FirstDelivered],[FirstDateTimeDelivered],[FirstMedNoteBox],[FirstCustCallInfo],[FirstHold])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & fc.CompanyID & ",") '[FirstCompanyID]
    SQL.Append("'" & fc.CreatedDateTime & "',") '[FirstCallDateTime]
    SQL.Append("'" & fc.ReportingParty & "',") '[FirstReportingParty]
    SQL.Append("NULL,") '[FirstRPRelationshipID]
    SQL.Append("NULL,") '[FirstPersonAuthorizingRemoval] *****
    SQL.Append("NULL,") '[FirstPARelationship] *****
    SQL.Append("'" & fc.DeceasedName & "',") '[FirstDeceasedName]
    SQL.Append("NULL,") '[FirstPrefix] ******
    SQL.Append("'" & fc.DateTimeOfDeath & "',") '[FirstDateTimeofDeath] *****
    SQL.Append("'" & fc.PlaceOfDeath & "',") '[FirstPlaceOfDeath]
    SQL.Append("'" & fc.SSN & "',") '[FirstSSN]
    SQL.Append("'" & fc.Weight & "',") '[FirstWeight]
    SQL.Append("'" & fc.DateOfBirth & "',") '[FirstDOB]
    SQL.Append("'" & fc.Address & "',") '[FirstAddress]
    SQL.Append("NULL,") '[FirstLocationType] *****
    SQL.Append("'" & fc.City & "',") '[FirstCity]
    SQL.Append("'" & fc.State & "',") '[FirstState]
    SQL.Append("'" & fc.County & "',") '[FirstCounty]
    SQL.Append("'" & fc.Zip & "',") '[FirstZip]
    SQL.Append("'" & fc.Phone & "',") '[FirstPhone]
    SQL.Append("'" & fc.PhoneExt & "',") '[FirstExt]
    SQL.Append("'" & fc.NextOfKinName & "',") '[FirstNextofKin]
    SQL.Append(fc.NextOfKinRelationshipID & ",") '[FirstRelationshipID]
    SQL.Append("'" & fc.NextOfKinPhone & "',") '[FirstTelephoneofInforKin]
    SQL.Append("NULL,") '[FirstWorkPhoneForKin]
    SQL.Append("'" & fc.NextOfKinWorkPhoneExt & "',") '[FirstWorkExt]
    SQL.Append("'" & fc.Doctor & "',") '[FirstDoctor]
    SQL.Append("'" & fc.DoctorPhone & "',") '[FirstDoctorPhone]
    SQL.Append("'" & fc.DatePatientSeen & "',") '[FirstDatePatientSeen]
    SQL.Append("'" & fc.Coroner & "',") '[FirstCoroner]
    SQL.Append("'" & fc.CaseNumber & "',") '[FirstFileNumber]
    SQL.Append("'" & fc.CounselorContacted & "',") '[FirstCounselorContacted]
    SQL.Append("'" & fc.DateCounselorContacted & "',") '[FirstDateContacted]
    SQL.Append("NULL,") '[FirstNotes] *****
    SQL.Append("NULL,") '[FirstOperatorCallNotes] *****
    SQL.Append(fc.Delivered & ",") '[FirstDelivered] *****
    If fc.Delivered = 1 Then
      SQL.Append("'" & fc.DeliveredDateTime & "',") '[FirstDateTimeDelivered] *****
    Else
      SQL.Append("NULL,") '[FirstDateTimeDelivered] *****
    End If
    SQL.Append("NULL,") '[FirstMedNoteBox] *****
    SQL.Append("NULL,") '[FirstCustCallInfo] *****
    SQL.Append(fc.Hold & ")") '[FirstHold] *****

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  ''' <summary>
  ''' Updates existing first call
  ''' </summary>
  ''' <param name="fc"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateFirstCall(fc As FirstCall) As Integer Implements IFirstCallDA.UpdateFirstCall

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE FIRST_CALL SET ")
    SQL.Append("FirstReportingParty = '" & fc.ReportingParty & "',") '[FirstReportingParty]
    SQL.Append("FirstRPRelationshipID = NULL,") '[FirstRPRelationshipID]
    SQL.Append("FirstPersonAuthorizingRemoval = NULL,") '[FirstPersonAuthorizingRemoval] *****
    SQL.Append("FirstPARelationship = NULL,") '[FirstPARelationship] *****
    SQL.Append("FirstDeceasedName = '" & fc.DeceasedName & "',") '[FirstDeceasedName]
    SQL.Append("FirstPrefix = NULL,") '[FirstPrefix] ******
    SQL.Append("FirstDateTimeofDeath = '" & fc.DateTimeOfDeath & "',") '[FirstDateTimeofDeath] *****
    SQL.Append("FirstPlaceOfDeath = '" & fc.PlaceOfDeath & "',") '[FirstPlaceOfDeath]
    SQL.Append("FirstSSN = '" & fc.SSN & "',") '[FirstSSN]
    SQL.Append("FirstWeight = '" & fc.Weight & "',") '[FirstWeight]
    SQL.Append("FirstDOB = '" & fc.DateOfBirth & "',") '[FirstDOB]
    SQL.Append("FirstAddress = '" & fc.Address & "',") '[FirstAddress]
    SQL.Append("FirstLocationType = NULL,") '[FirstLocationType] *****
    SQL.Append("FirstCity = '" & fc.City & "',") '[FirstCity]
    SQL.Append("FirstState = '" & fc.State & "',") '[FirstState]
    SQL.Append("FirstCounty = '" & fc.County & "',") '[FirstCounty]
    SQL.Append("FirstZip = '" & fc.Zip & "',") '[FirstZip]
    SQL.Append("FirstPhone = '" & fc.Phone & "',") '[FirstPhone]
    SQL.Append("FirstExt = '" & fc.PhoneExt & "',") '[FirstExt]
    SQL.Append("FirstNextofKin = '" & fc.NextOfKinName & "',") '[FirstNextofKin]
    SQL.Append("FirstRelationshipID = '" & fc.NextOfKinRelationshipID & "',") '[FirstRelationshipID]
    SQL.Append("FirstTelephoneofInforKin = '" & fc.NextOfKinPhone & "',") '[FirstTelephoneofInforKin]
    SQL.Append("FirstWorkPhoneForKin = NULL,") '[FirstWorkPhoneForKin]
    SQL.Append("FirstWorkExt = '" & fc.NextOfKinWorkPhoneExt & "',") '[FirstWorkExt]
    SQL.Append("FirstDoctor = '" & fc.Doctor & "',") '[FirstDoctor]
    SQL.Append("FirstDoctorPhone = '" & fc.DoctorPhone & "',") '[FirstDoctorPhone]
    SQL.Append("FirstDatePatientSeen = '" & fc.DatePatientSeen & "',") '[FirstDatePatientSeen]
    SQL.Append("FirstCoroner = '" & fc.Coroner & "',") '[FirstCoroner]
    SQL.Append("FirstFileNumber = '" & fc.CaseNumber & "',") '[FirstFileNumber]
    SQL.Append("FirstCounselorContacted = '" & fc.CounselorContacted & "',") '[FirstCounselorContacted]
    SQL.Append("FirstDateContacted = '" & fc.DateCounselorContacted & "',") '[FirstDateContacted]
    SQL.Append("FirstNotes = NULL,") '[FirstNotes] *****
    SQL.Append("FirstOperatorCallNotes = NULL,") '[FirstOperatorCallNotes] *****
    SQL.Append("FirstDelivered = " & fc.Delivered & ",") '[FirstDelivered] 
    If fc.Delivered = 1 Then
      SQL.Append("FirstDateTimeDelivered = '" & fc.DeliveredDateTime & "',") '[FirstDateTimeDelivered] 
    Else
      SQL.Append("FirstDateTimeDelivered = NULL,") '[FirstDateTimeDelivered] 
    End If
    SQL.Append("FirstMedNoteBox = NULL,") '[FirstMedNoteBox] *****
    SQL.Append("FirstCustCallInfo = NULL,") '[FirstCustCallInfo] *****
    SQL.Append("FirstHold = " & fc.Hold & " ") '[FirstHold] 
    SQL.Append("WHERE FirstCallID = " & fc.ID)

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

End Class
