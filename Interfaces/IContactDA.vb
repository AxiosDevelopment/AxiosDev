Public Interface IContactDA
  Function GetContact(id As String) As Contact
  Function GetContacts() As List(Of Contact)
  Function GetContactsByCompany(id As String) As List(Of Contact)
  Function InsertContact(c As Contact) As Integer
  Function UpdateContact(c As Contact) As Integer
End Interface
