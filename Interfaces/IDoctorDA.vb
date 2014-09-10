Public Interface IDoctorDA
  Function GetDoctor(id As String) As Doctor
  Function GetDoctors() As List(Of Doctor)
  Function GetDoctors(search As String) As List(Of Doctor)
  Function InsertDoctor(d As Doctor) As Integer
  Function UpdateDoctor(d As Doctor) As Integer
End Interface
