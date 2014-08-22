Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

Public Class FromAutoSearch
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim qId As String
    Dim searchString As String = ""

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

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim strBusiness As String
    Dim strHTML As New StringBuilder()

    rsData = db.GetDataReader("SELECT BusID, BusinessName, BusCity FROM BusinessNames where BusinessName like '%" & search & "%' ORDER BY BusinessName ASC")
    If rsData.HasRows Then
      While rsData.Read
        strBusiness = ""
        strBusiness = If(Not String.IsNullOrEmpty(rsData("BusCity").ToString()), rsData("BusinessName") & " - " & rsData("BusCity"), rsData("BusinessName"))
        strHTML.Append("<li><input type=""hidden"" class=""busId"" value=" & rsData("BusID") & " />" & strBusiness & "</li>")
      End While
      rsData.Close()
      Response.Write(strHTML.ToString())
    End If

  End Sub

  ''' <summary>
  ''' This will return the business information based on the business selected from the autocomplete
  ''' We need to return the other information (address) to fill in the other fields on the page
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetBusiness(search As String)

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim TheJson As New StringBuilder()

    rsData = db.GetDataReader("SELECT BusID, BusinessName, BusAddress, BusCity, BusState, BusZip, BusCounty, BusPhone, BusExt FROM BusinessNames WITH (NOLOCK) WHERE BusID = " + search)
    If rsData.HasRows Then
      Dim columnCount As Integer
      columnCount = rsData.FieldCount

      TheJson.Append("{")
      While rsData.Read
        For x = 0 To columnCount - 1
          TheJson.Append(String.Format("{0}{1}{0} : {0}{2}{0}", Chr(34), rsData.GetName(x), rsData(x)))
          If x <> columnCount - 1 Then
            TheJson.Append(",")
          End If
        Next
      End While
      TheJson.Append("}")
      rsData.Close()
      Response.Write(TheJson)
      Context.ApplicationInstance.CompleteRequest()
    End If

  End Sub

  ''' <summary>
  ''' This will return the doctors for the attending physician autocomplete
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetPhysicians(search As String)

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim strHTML As New StringBuilder()

    rsData = db.GetDataReader("SELECT ID, DrName FROM DoctorListQ WHERE DrName LIKE '%" & search & "%' ORDER BY DrName ASC")
    If rsData.HasRows Then
      While rsData.Read
        strHTML.Append("<li><input type=""hidden"" class=""docId"" value=" & rsData("ID") & " />" & rsData("DrName").ToString() & "</li>")
      End While
      rsData.Close()
      Response.Write(strHTML.ToString())
    End If

  End Sub

  ''' <summary>
  ''' This will return the physician information based on the physician selected from the autocomplete
  ''' We need to return the other information to fill in the other fields on the page
  ''' </summary>
  ''' <param name="search"></param>
  ''' <remarks></remarks>
  Private Sub GetPhysician(search As String)

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim TheJson As New StringBuilder()

    rsData = db.GetDataReader("SELECT ID, DrName, DrWorkPhone FROM DoctorListQ WHERE ID = " + search)
    If rsData.HasRows Then
      Dim columnCount As Integer
      columnCount = rsData.FieldCount

      TheJson.Append("{")
      While rsData.Read
        For x = 0 To columnCount - 1
          TheJson.Append(String.Format("{0}{1}{0} : {0}{2}{0}", Chr(34), rsData.GetName(x), rsData(x)))
          If x <> columnCount - 1 Then
            TheJson.Append(",")
          End If
        Next
      End While
      TheJson.Append("}")
      rsData.Close()
      Response.Write(TheJson)
      Context.ApplicationInstance.CompleteRequest()
    End If


  End Sub

End Class