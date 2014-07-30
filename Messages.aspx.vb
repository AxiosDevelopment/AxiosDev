Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic

Public Class Messages
  Inherits System.Web.UI.Page

  Protected clientGreeting As String = ""
  Public fromNames As String = ""

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim successed As Boolean
    Dim clientId As String
    Dim msgId As Integer
    Dim autoComplete As Boolean = False
    Dim businessName As String = ""

    If Not IsNothing(Request.QueryString.Get("query")) Then
      autoComplete = True
      businessName = Request.QueryString.Get("query").ToString()
    End If

    If Not (Page.IsPostBack) And Not (autoComplete) Then

      ' Get Lookup Data
      GetMessageLookup()
      ' Get AutoComplete Data for From textbox
      'AutoCompleteFrom()

      msgId = Convert.ToInt32(Request.QueryString.Get("MsgId"))
      clientId = Request.QueryString.Get("ClientId").ToString()

      If Not String.IsNullOrEmpty(clientId) Then
        successed = GetClient(clientId)

        If msgId > 0 Then
          'Message Exists


        End If
      End If

    Else 'AutoCompletes
      Response.Write(GetBusinessAutoComplete(businessName))
    End If


  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetMessageLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT QwkMsgID, QwkMsg FROM QwkMsg WITH (NOLOCK) ORDER BY QwkMsg ASC")

    Do While rsData.Read()
      quickMessage.Items.Add(New ListItem(rsData("QwkMsg").ToString(), rsData("QwkMsgID").ToString()))
    Loop

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Function GetClient(clientId As String) As Boolean

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM ClientUpdate WITH (NOLOCK) WHERE CustID = " + clientId)

    Do While rsData.Read()
      clientMessageId.InnerHtml = rsData("CustID").ToString()
      clientName.InnerText = rsData("CompanyName")
      clientGreeting = rsData("ClientAnswer")
            clientMainInfo.Value = db.ClearNull(rsData("ClientData"))
    Loop

    Return True

  End Function

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="msgId"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function GetMessage(msgId As Integer) As Boolean

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CustID, CompanyName, Contact, ClientType, ClientAnswer, ClientData FROM ClientUpdate WITH (NOLOCK) WHERE CustID = " + clientId)

    Do While rsData.Read()

    Loop

    Return True

  End Function

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="prefix"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetBusinessAutoComplete(ByVal prefix As String) As String
    Dim businessNames As String = ""

    If Not String.IsNullOrEmpty(prefix) Then
      Dim db As dbUtil 'access to db functions
      Dim rsData As SqlDataReader

      db = New dbUtil()
      rsData = db.GetDataReader("SELECT BusID, BusinessName FROM BusinessNames WITH (NOLOCK) WHERE BusinessName IS NOT NULL AND BusinessName LIKE '" + prefix + "%' ORDER BY BusinessName ASC")

      Do While rsData.Read()
        businessNames = businessNames + "<li>" + rsData("BusinessName") + "</li>"
      Loop

    End If

    Return businessNames

  End Function


End Class