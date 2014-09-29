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
    Dim messageSource As String = ""

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
              messageSource = Request.QueryString.Get("messageSource").ToString()
              If messageSource.ToUpper() = "SEARCHFIRSTCALLS" Then ' First Calls
                GetFirstCalls(searchString)
              ElseIf messageSource.ToUpper() = "SEARCHMESSAGES" Then  'Messages
                GetMessages(searchString)
              End If

            Case "CLIENTSEARCH" 'Typing in the Client Search Textbox
              searchString = Request.QueryString.Get("query").ToString()
              GetClients(searchString)

            Case "clientAuto" 'When clicking an item on the client autocomplete textbox
              searchString = Request.QueryString.Get("clientId").ToString()
              GetClient(searchString)

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
  Private Sub GetMessages(search As String)

    Dim mDA As New MessageDA
    Dim messages As List(Of Message)
    Dim strHTML As New StringBuilder()
    Dim isDelivered As String

    messages = mDA.GetMessages(search)

    For Each m As Message In messages
      isDelivered = If(m.Delivered = 0, "On Hold", "Delivered")
      strHTML.Append("<li><a href=""Messages.aspx?MsgId=" & m.ID.ToString() & "&ClientId=" & m.CompanyID.ToString() & """><span class=""to"">Status: " & isDelivered & "</span><span class=""to"">To: " & m.MsgTo.ToString() & "</span><span class=""from"">From: " & m.MsgFrom.ToString() & "</span><span class=""date"">Date: " & FormatDateTime(m.CreatedDateTime.ToString(), DateFormat.ShortDate) & "</span><span class=""time"">Time: " & FormatDateTime(m.CreatedDateTime.ToString(), DateFormat.ShortTime) & "</span></a></li>")
    Next

    Response.Write(strHTML.ToString())

  End Sub

  ''' <summary>
  ''' This will return all First Calls for a client (company) when clicking on Search First Calls tab (First Calls page)
  ''' </summary>
  ''' <param name="search"></param>
  ''' <remarks></remarks>
  Private Sub GetFirstCalls(search As String)

    Dim fcDA As New FirstCallDA
    Dim firstCalls As List(Of FirstCall)
    Dim strHTML As New StringBuilder()
    Dim isDelivered As String

    firstCalls = fcDA.GetFirstCalls(search)

    For Each fc As FirstCall In firstCalls
      isDelivered = If(fc.Delivered = 0, "On Hold", "Delivered")
      strHTML.Append("<li><a href=""FirstCalls.aspx?FirstCallId=" & fc.ID.ToString() & "&ClientId=" & fc.CompanyID.ToString() & """><span class=""to"">Status: " & isDelivered & "</span><span class=""from"">Place Of Death: " & fc.PlaceOfDeath & "</span><span class=""date"">Date: " & FormatDateTime(fc.CreatedDateTime.ToString(), DateFormat.ShortDate) & "</span><span class=""time"">Time: " & FormatDateTime(fc.CreatedDateTime.ToString(), DateFormat.ShortTime) & "</span></a></li>")
    Next

    Response.Write(strHTML.ToString())

  End Sub

  ''' <summary>
  ''' This will return the clients for the autocomplete (Add Client page)
  ''' </summary>
  ''' <param name="search"></param>
  ''' <remarks></remarks>
  Private Sub GetClients(search As String)

    Dim cDA As New CompanyDA
    Dim companies As List(Of Company)
    Dim js As New JavaScriptSerializer()

    companies = cDA.GetCompaniesForAutoComplete(search)

    'LINQ statement to limit the information needed from the objects
    'Anonymous Types used - more efficient
    Dim comps = From c In companies
                Select New With {c.CompanyID, c.Name}

    Dim json = js.Serialize(comps)

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

  ''' <summary>
  ''' This will return the client information based on the client selected from the autocomplete
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClient(search As String)

    Dim cDA As New CompanyDA
    Dim company As Company
    Dim js As New JavaScriptSerializer()

    company = cDA.GetCompany(search)
    Dim json = js.Serialize(company)

    Response.Write(json)
    Context.ApplicationInstance.CompleteRequest() 'need this or the whole page will be sent back as well

  End Sub

End Class