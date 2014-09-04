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

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WHERE ID = " + id)

    If rsData.HasRows Then

      Do While rsData.Read
        doctor.DoctorID = rsData("ID")
        doctor.Name = rsData("DrName")
        doctor.WorkPhone = db.ClearNull(rsData("DrWorkPhone"))
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

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WITH (NOLOCK) ORDER BY DrName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim doctor As New Doctor
        doctor.DoctorID = rsData("ID")
        doctor.Name = rsData("DrName")
        doctor.WorkPhone = db.ClearNull(rsData("DrWorkPhone"))
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

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WHERE DrName LIKE '%" & search & "%' ORDER BY DrName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim doctor As New Doctor
        doctor.DoctorID = rsData("ID")
        doctor.Name = rsData("DrName")
        doctor.WorkPhone = db.ClearNull(rsData("DrWorkPhone"))
        doctors.Add(doctor)
      Loop

      rsData.Close()

    End If

    Return doctors

  End Function

  Public Function InsertDoctor(c As Doctor) As Integer Implements IDoctorDA.InsertDoctor

  End Function

  Public Function UpdateDoctor(c As Doctor) As Integer Implements IDoctorDA.UpdateDoctor

  End Function

End Class
