Imports System.Data.SqlClient
Public Class firstCall
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim successed As Boolean
    Dim clientId As String
    Dim firstCallId As Integer

    If Not (Page.IsPostBack) Then

      ' Get Lookup Data
      'GetFirstCallLookup()

      firstCallId = Convert.ToInt32(Request.QueryString.Get("FirstCallId"))
      clientId = Request.QueryString.Get("ClientId").ToString()

      If Not String.IsNullOrEmpty(clientId) Then
        successed = GetClient(clientId)

        If firstCallId > 0 Then
          'Message Exists


        End If
      End If

    End If
  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Function GetClient(Id As String) As Boolean

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM CompanyInfo WITH (NOLOCK) WHERE CustID = " + Id)

    Do While rsData.Read()
      ClientHeader.Text = rsData("CompanyName")
      clientId.Text = rsData("CustID").ToString()
      clientName.Text = rsData("CompanyName")
    Loop

    Return True

  End Function

  Private Function GetFirstCall(id As String, fcId As Integer) As Boolean
    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM CompanyInfo WITH (NOLOCK) WHERE CustID = " + id)

    Do While rsData.Read()

      If fcId = 0 Then
        msgDate.Text = DateTime.Now()
        msgTime.Text = FormatDateTime(DateTime.Now, DateFormat.LongTime)
      Else

      End If

    Loop

    Return True
  End Function

End Class