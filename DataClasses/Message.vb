Public Class Message
  Inherits MessageFirstCallBase

  'Public Property MessageID As Integer
  'Public Property CreatedDateTime As Date
  'Public Property CompanyID As Integer
  Public Property MsgTo As String
  Public Property MsgFrom As String
  Public Property Business As String
  Public Property Phone As String
  Public Property AltPhone As String
  Public Property QwkMsgs As String
  Public Property MsgMessage As String
  Public Property OperatorNotes As String
  Public Property Hold As String
  Public Property DelDateTime As Date
  Public Property Delivered As Byte
  Public Property OnCall As String
  Public Property Procedure As String

  Public Sub New()
  End Sub

End Class
