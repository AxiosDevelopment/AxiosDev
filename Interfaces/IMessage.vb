Public Interface IMessage
  Function GetMessage(id As String) As Message
  Function GetMessages() As List(Of Message)
  Function GetMessages(search As String) As List(Of Message)
  Function InsertMessage(m As Message) As Integer
  Function UpdateMessage(m As Message) As Integer
End Interface
