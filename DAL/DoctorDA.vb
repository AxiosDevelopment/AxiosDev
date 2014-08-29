Imports System.Data.SqlClient

Public Class DoctorDA
  Implements IDoctorDA

  Public Function GetDoctor(id As String) As Doctor Implements IDoctorDA.GetDoctor

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim doctor As New Doctor

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WHERE ID = " + id)

    If rsData.HasRows Then

      While rsData.Read
        doctor.DoctorID = rsData("ID")
        doctor.Name = rsData("DrName")
        doctor.WorkPhone = db.ClearNull(rsData("DrWorkPhone"))
      End While

    End If

    Return doctor

  End Function

  Public Function GetDoctors() As List(Of Doctor) Implements IDoctorDA.GetDoctors

  End Function

  Public Function GetDoctors(search As String) As List(Of Doctor) Implements IDoctorDA.GetDoctors

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim doctors As New List(Of Doctor)

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WHERE DrName LIKE '%" & search & "%' ORDER BY DrName ASC")

    If rsData.HasRows Then

      While rsData.Read
        Dim doctor As New Doctor
        doctor.DoctorID = rsData("ID")
        doctor.Name = rsData("DrName")
        doctor.WorkPhone = db.ClearNull(rsData("DrWorkPhone"))
        doctors.Add(doctor)
      End While

      rsData.Close()

    End If

    Return doctors

  End Function

  Public Function InsertDoctor(c As Doctor) As Integer Implements IDoctorDA.InsertDoctor

  End Function

  Public Function UpdateDoctor(c As Doctor) As Integer Implements IDoctorDA.UpdateDoctor

  End Function

End Class
