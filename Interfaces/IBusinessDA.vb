Public Interface IBusinessDA
  Function GetBusiness(id As String) As Business
  Function GetBusinesses() As List(Of Business)
  Function GetBusinesses(search As String) As List(Of Business)
  Function InsertBusiness(c As Business) As Integer
  Function UpdateBusiness(c As Business) As Integer
End Interface
