Imports System.Data.SqlClient

Public Class UpdateInformation
  Inherits System.Web.UI.Page

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim updateId As String
    Dim contactName As String = ""
    Dim contactNumber As String = ""
    Dim contactId As String = ""
    Dim cid As String = ""
    Dim dataInfo As String = ""

    If Not (Page.IsPostBack) Then

      Try

        updateId = Request.QueryString.Get("updateId")
        If String.IsNullOrEmpty(updateId) Then
          'GET VARIABLES FOR ADDITIONAL NOTES AND CLIENT INFO TEXTAREAS
          updateId = Request.Form.Get("id").ToString()
          'updateId = Request.QueryString.Get("id").ToString()
          If Not String.IsNullOrEmpty(updateId) Then
            dataInfo = Request.Form.Get("info").ToString()
            cid = Request.Form.Get("clientId").ToString()
          End If
        Else
          'GET VARIABLES FOR COUNSELOR ON CALL AND SECONDARY ON CALL
          contactName = Request.QueryString.Get("contactName").ToString()
          contactNumber = Request.QueryString.Get("contactNumber").ToString()
          contactId = Request.QueryString.Get("contactId").ToString()

          cid = Request.QueryString.Get("clientId").ToString()
        End If

        If Not String.IsNullOrEmpty(updateId) Then
          Select Case updateId
            Case "updateMainCounselor"
              UpdateCounselors(cid, contactId, contactName, contactNumber, 1)
            Case "updateSecondaryCounselor"
              UpdateCounselors(cid, contactId, contactName, contactNumber, 2)
            Case "updateAdditionalNotes"
              UpdateAdditionalNotes(cid, dataInfo)
            Case "updateClientInfo"
              UpdateCompanyInformation(cid, dataInfo)
            Case Else

          End Select
        Else

        End If

      Catch ex As Exception
        Response.StatusCode = 500 'Set STATUSCODE to trigger error on ajax call
        Response.StatusDescription = "Update has failed. Please try again."
        Response.End()
      End Try

    End If
  End Sub

  ''' <summary>
  ''' Updates the Counselors on Call (Contact Information)
  ''' Can be primary, secondary, etc... (conType)
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateCounselors(id As String, cId As String, cName As String, cNumber As String, conType As Integer)

    Dim cDA As New ContactDA
    Dim contact As New Contact
    Dim result As Integer

    contact.ContactID = cId
    contact.CompanyID = id
    contact.Name = cName
    contact.Phone = cNumber
    contact.TypeID = conType

    If cId = 0 Then

      If contact.TypeID = 2 Then
        If contact.Name = "" And contact.Phone = "" Then
          Exit Sub
        End If
      End If
      result = cDA.InsertContact(contact)

    Else

      If contact.Name = "" And contact.Phone = "" Then
        If contact.TypeID = 1 Then
          Response.StatusCode = 500 'Set STATUSCODE to trigger error on ajax call
          Response.StatusDescription = "Primary Contact (Counselor On Call) is required."
          Response.End()
        ElseIf contact.TypeID = 2 Then
          result = cDA.DeleteContact(contact.ContactID)
        End If
      Else
        result = cDA.UpdateContactSimple(contact)
      End If

    End If

  End Sub

  ''' <summary>
  ''' Updates the Additional Notes that is assigned to the client (company)
  ''' </summary>
  ''' <param name="id"></param>
  ''' <param name="note"></param>
  ''' <remarks></remarks>
  Private Sub UpdateAdditionalNotes(id As String, note As String)

    Dim cDA As New CompanyDA
    Dim company As New Company
    Dim result As Integer

    company.CompanyID = id
    company.AdditionalNotes = note

    result = cDA.UpdateCompanyAdditionalNotes(company)

  End Sub

  ''' <summary>
  ''' Updates the CompanyInformation field that is assigned to the client (company) in COMPANY_INFO table
  ''' </summary>
  ''' <param name="id"></param>
  ''' <param name="data"></param>
  ''' <remarks></remarks>
  Private Sub UpdateCompanyInformation(id As String, data As String)

    Dim cDA As New CompanyDA
    Dim company As New Company
    Dim result As Integer

    company.CompanyID = id
    data = Replace(data, "'", "`")
    company.ClientInfo = data

    result = cDA.UpdateCompanyInfo(company)

  End Sub

End Class