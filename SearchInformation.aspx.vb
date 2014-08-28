Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

Public Class SearchInformation
  Inherits System.Web.UI.Page

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim qId As String
    Dim searchString As String = ""
    Dim companyNumber As Integer

    If Not (Page.IsPostBack) Then

      Try

        qId = Request.QueryString.Get("queryId")
        If String.IsNullOrEmpty(qId) Then
          Response.StatusCode = 500 'Set STATUSCODE to trigger error on ajax call
          Response.Write("No Query ID Sent")
        Else

          Select Case qId
            Case "BUSSEARCH" 'Typing in the Place of Death Textbox
              searchString = Request.QueryString.Get("query").ToString()
              GetBusinesses(searchString)

            Case "DOCSEARCH" 'Typing in the Attending Phyisician Textbox
              searchString = Request.QueryString.Get("query").ToString()
              GetPhysicians(searchString)

            Case "podAuto" 'When clicking an item on the Place of Death autocomplete list
              searchString = Request.QueryString.Get("busId").ToString()
              GetBusiness(searchString)

            Case "physicianAuto" 'When clicking an item on the Attending Physician autocomplete list
              searchString = Request.QueryString.Get("busId").ToString()
              GetPhysician(searchString)

            Case "ALLMESSAGES"
              searchString = Request.QueryString.Get("cid").ToString()
              ''GET CompanyNumber with cid(CompanyID) until Msg table is changed
              companyNumber = GetCompanyNumber(searchString)
              GetMessages(companyNumber, searchString)

            Case Else


          End Select
        End If

      Catch ex As Exception
        Response.StatusCode = 500 'Set STATUSCODE to trigger error on ajax call
        Response.Write(ex.Message)
      End Try

    End If

  End Sub

  ''' <summary>
  ''' This will return the businesses for the place of dealth autocomplete textbox
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetBusinesses(search As String)

    Dim bDA As New BusinessDA
    Dim businesses As List(Of Business)
    Dim js As New JavaScriptSerializer()

    businesses = bDA.GetBusinesses(search)
    Dim json = js.Serialize(businesses)

    'Dim test = From b In businesses
    '           Where b.BusinessID = 8
    '           Select b

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

  ''' <summary>
  ''' This will return the business information based on the business selected from the autocomplete
  ''' We need to return the other information (address) to fill in the other fields on the page
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetBusiness(search As String)

    Dim bDA As New BusinessDA
    Dim business As Business
    Dim js As New JavaScriptSerializer()

    business = bDA.GetBusiness(search)
    Dim json = js.Serialize(business)

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

  ''' <summary>
  ''' This will return the doctors for the attending physician autocomplete
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetPhysicians(search As String)

    Dim dDA As New DoctorDA
    Dim doctors As List(Of Doctor)
    Dim js As New JavaScriptSerializer()

    doctors = dDA.GetDoctors(search)
    Dim json = js.Serialize(doctors)

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

  ''' <summary>
  ''' This will return the physician information based on the physician selected from the autocomplete
  ''' We need to return the other information to fill in the other fields on the page
  ''' </summary>
  ''' <param name="search"></param>
  ''' <remarks></remarks>
  Private Sub GetPhysician(search As String)

    Dim dDA As New DoctorDA
    Dim doctor As Doctor
    Dim js As New JavaScriptSerializer()

    doctor = dDA.GetDoctor(search)
    Dim json = js.Serialize(doctor)

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

  ''' <summary>
  ''' This will return all messages for a client (company) when clicking on All Messages tab (Message page)
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetMessages(search As String, companyId As String)

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim strHTML As New StringBuilder()

    rsData = db.GetDataReader("SELECT MsgId, MsgCustID, MsgTo, MsgFrom, MsgDateTime FROM Msg WITH (NOLOCK) WHERE MsgCustID = " & search)
    If rsData.HasRows Then
      While rsData.Read
        strHTML.Append("<li><a href=""Messages.aspx?MsgId=" & rsData("MsgId").ToString() & "&ClientId=" & companyId & """><span class=""to"">To: " & rsData("MsgTo").ToString() & "</span><span class=""from"">From: " & rsData("MsgFrom").ToString() & "</span><span class=""date"">Date: " & FormatDateTime(rsData("MsgDateTime").ToString(), DateFormat.ShortDate) & "</span><span class=""time"">Time: " & FormatDateTime(rsData("MsgDateTime").ToString(), DateFormat.ShortTime) & "</span></a></li>")
      End While
      rsData.Close()
      Response.Write(strHTML.ToString())
    End If

  End Sub

  ''' <summary>
  ''' ONLY NEEDED UNTIL MSG TABLE IS FIXED AND HAS COMPANYID FIELD
  ''' </summary>
  ''' <remarks></remarks>
  Private Function GetCompanyNumber(id As String) As String

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT CompanyID, CompanyNumber FROM COMPANY WITH (NOLOCK) WHERE CompanyID = " + id)

    Do While rsData.Read()
      Return rsData("CompanyNumber").ToString()
    Loop

    Return ""

  End Function

End Class