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
          cid = Request.QueryString.Get("clientId").ToString()
        End If

        If Not String.IsNullOrEmpty(updateId) Then
          Select Case updateId
            Case "updateMainCounselor"
              UpdateCounselors(cid, contactName, contactNumber, 1)
            Case "updateSecondaryCounselor"
              UpdateCounselors(cid, contactName, contactNumber, 2)
            Case "updateAdditionalNotes"
              UpdateAdditionalNotes(cid, dataInfo)
            Case "updateClientInfo"
              UpdateCompanyInformation(cid, dataInfo)
            Case Else

          End Select
        Else

        End If

        Response.Write("Updated Successfully")

      Catch ex As Exception
        Response.StatusCode = 500 'Set STATUSCODE to trigger error on ajax call
        Response.Write(ex.Message)
        Response.End()
      End Try

    End If
  End Sub

  ''' <summary>
  ''' Updates the Counselors on Call (Contact Information)
  ''' Can be primary, secondary, etc... (conType)
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateCounselors(id As String, cName As String, cNumber As String, conType As Integer)

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()

    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[CONTACT] ([CompanyID],[ContactName],[ContactInfo],[ContactType],[CreateDateTIme])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & Convert.ToInt32(id) & ",") 'CompanyID
    SQL.Append(If(Not String.IsNullOrEmpty(cName), "'" & cName & "',", "NULL,")) 'ContactName
    SQL.Append(If(Not String.IsNullOrEmpty(cNumber), "'" & cNumber & "',", "NULL,")) 'ContactInfo
    SQL.Append(conType & ",") 'ContactType
    SQL.Append("'" & DateTime.Now & "')") 'CreateDateTIme

    returnedID = db.GetID(SQL.ToString())

  End Sub

  ''' <summary>
  ''' Updates the Additional Notes that is assigned to the client (company)
  ''' </summary>
  ''' <param name="id"></param>
  ''' <param name="note"></param>
  ''' <remarks></remarks>
  Private Sub UpdateAdditionalNotes(id As String, note As String)

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY SET ")
    SQL.Append("CompanyAdditionalNotes = " & If(Not String.IsNullOrEmpty(note), "'" & note & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & id)

    returnedID = db.GetID(SQL.ToString())

  End Sub

  ''' <summary>
  ''' Updates the CompanyInformation field that is assigned to the client (company) in COMPANY_INFO table
  ''' </summary>
  ''' <param name="id"></param>
  ''' <param name="data"></param>
  ''' <remarks></remarks>
  Private Sub UpdateCompanyInformation(id As String, data As String)

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    data = Replace(data, "'", "`")
    SQL.Append("UPDATE COMPANY_INFO SET ")
    SQL.Append("CompanyInformation = " & If(Not String.IsNullOrEmpty(data), "'" & data & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & id)

    returnedID = db.GetID(SQL.ToString())

  End Sub

End Class