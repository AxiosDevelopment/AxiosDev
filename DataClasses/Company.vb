Public Class Company

  Public Property CompanyID As Integer
  Public Property Number As Integer
  Public Property Name As String
  Public Property TypeID As Integer
  Public Property PhoneAnswer As String
  Public Property Address As String
  Public Property City As String
  Public Property State As String
  Public Property Zip As String
  Public Property MainTelephone As String
  Public Property MainTelephone2nd As String
  Public Property Fax As String
  Public Property Email As String
  Public Property InstructionSheet As String
  Public Property HoursOfOperation As String
  Public Property AdditionalNotes As String
  Public Property ClientInfo As String

  Public Property Contacts As List(Of Contact)

  Public Sub New()
  End Sub

End Class
