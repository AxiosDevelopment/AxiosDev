Public Interface IFirstCallDA
  Function GetFirstCall(id As String) As FirstCall
  Function GetFirstCalls() As List(Of FirstCall)
  Function GetFirstCalls(search As String) As List(Of FirstCall)
  Function InsertFirstCall(fc As FirstCall) As Integer
  Function UpdateFirstCall(fc As FirstCall) As Integer
End Interface
