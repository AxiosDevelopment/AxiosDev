Imports System.Data.SqlClient

Public Class DoctorDA
  Implements IDoctorDA

  ''' <summary>
  ''' Gets a doctor (physician) based on doctor id
  ''' Used for autocomplete feature on FirstCalls page when doctor is selected
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetDoctor(id As String) As Doctor Implements IDoctorDA.GetDoctor

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim doctor As New Doctor

    rsData = db.GetDataReader("SELECT DoctorID, DoctorName, DoctorPhone, DoctorPhoneExt FROM DOCTOR WHERE DoctorID = " + id)

    If rsData.HasRows Then

      Do While rsData.Read
        doctor.DoctorID = rsData("DoctorID")
        doctor.Name = rsData("DoctorName")
        doctor.Phone = db.ClearNull(rsData("DoctorPhone"))
        doctor.PhoneExt = db.ClearNull(rsData("DoctorPhoneExt"))
      Loop

      rsData.Close()

    End If

    Return doctor

  End Function

  ''' <summary>
  ''' Get All Doctors
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetDoctors() As List(Of Doctor) Implements IDoctorDA.GetDoctors

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim doctors As New List(Of Doctor)

    rsData = db.GetDataReader("SELECT DoctorID, DoctorName, DoctorPhone, DoctorPhoneExt FROM DOCTOR WITH (NOLOCK) ORDER BY DoctorName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim doctor As New Doctor
        doctor.DoctorID = rsData("DoctorID")
        doctor.Name = rsData("DoctorName")
        doctor.Phone = db.ClearNull(rsData("DoctorPhone"))
        doctor.PhoneExt = db.ClearNull(rsData("DoctorPhoneExt"))
        doctors.Add(doctor)
      Loop

      rsData.Close()

    End If

    Return doctors

  End Function

  ''' <summary>
  ''' Gets a list of doctors (physicians)
  ''' Used for autocomplete feature on FirstCalls page
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetDoctors(search As String) As List(Of Doctor) Implements IDoctorDA.GetDoctors

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim doctors As New List(Of Doctor)

    rsData = db.GetDataReader("SELECT DoctorID, DoctorName, DoctorPhone, DoctorPhoneExt FROM DOCTOR WHERE DoctorName LIKE '%" & search & "%' ORDER BY DoctorName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim doctor As New Doctor
        doctor.DoctorID = rsData("DoctorID")
        doctor.Name = rsData("DoctorName")
        doctor.Phone = db.ClearNull(rsData("DoctorPhone"))
        doctor.PhoneExt = db.ClearNull(rsData("DoctorPhoneExt"))
        doctors.Add(doctor)
      Loop

      rsData.Close()

    End If

    Return doctors

  End Function

  Public Function InsertDoctor(d As Doctor) As Integer Implements IDoctorDA.InsertDoctor

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[DOCTOR]([DoctorName],[DoctorPhone],[DoctorPhoneExt])")
    SQL.Append(" VALUES ")
    SQL.Append("('" & d.Name & "',") 'DoctorName
    SQL.Append("'" & d.Phone & "',") 'DoctorPhone
    SQL.Append("'" & d.PhoneExt & "')") 'DoctorPhoneExt

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  Public Function UpdateDoctor(d As Doctor) As Integer Implements IDoctorDA.UpdateDoctor

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE DOCTOR SET ")
    SQL.Append("DoctorName = '" & d.Name & "',") 'DoctorName
    SQL.Append("DoctorPhone = '" & d.Phone & "',") 'DoctorPhone
    SQL.Append("DoctorPhoneExt = '" & d.PhoneExt & "' ") 'DoctorPhoneExt
    SQL.Append("WHERE DoctorID = " & d.DoctorID)

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

End Class
