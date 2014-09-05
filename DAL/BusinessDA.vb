Imports System.Data.SqlClient

Public Class BusinessDA
  Implements IBusinessDA

  ''' <summary>
  ''' Gets a business (facility) by Business Id
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetBusiness(id As String) As Business Implements IBusinessDA.GetBusiness

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim business As New Business

    rsData = db.GetDataReader("SELECT BusinessID, BusinessName, BusinessAddress, BusinessCity, BusinessState, BusinessZip, BusinessCounty, BusinessPhone, BusinessExt, BusinessAltPhone, BusinessOwnerSupVis, BusinessNotes FROM BUSINESS WITH (NOLOCK) WHERE BusinessID = " + id)

    If rsData.HasRows Then

      Do While rsData.Read
        business.BusinessID = rsData("BusinessID")
        business.Name = rsData("BusinessName")
        business.Address = db.ClearNull(rsData("BusinessAddress"))
        business.City = db.ClearNull(rsData("BusinessCity"))
        business.State = db.ClearNull(rsData("BusinessState"))
        business.Zip = db.ClearNull(rsData("BusinessZip"))
        business.County = db.ClearNull(rsData("BusinessCounty"))
        business.Phone = db.ClearNull(rsData("BusinessPhone"))
        business.PhoneExt = db.ClearNull(rsData("BusinessExt"))
        business.AltPhone = db.ClearNull(rsData("BusinessAltPhone"))
        business.OwnerSupVis = db.ClearNull(rsData("BusinessOwnerSupVis"))
        business.Notes = db.ClearNull(rsData("BusinessNotes"))
      Loop

      rsData.Close()

    End If

    Return business

  End Function

  ''' <summary>
  ''' Gets a list of businesses (facilities)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetBusinesses() As List(Of Business) Implements IBusinessDA.GetBusinesses

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim businesses As New List(Of Business)

    rsData = db.GetDataReader("SELECT BusinessID, BusinessName, BusinessCity FROM BUSINESS ORDER BY BusinessName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim business As New Business
        business.BusinessID = rsData("BusinessID")
        business.Name = db.ClearNull(rsData("BusinessName"))
        business.City = db.ClearNull(rsData("BusinessCity"))
        businesses.Add(business)
      Loop

      rsData.Close()

    End If

    Return businesses

  End Function

  ''' <summary>
  ''' Gets a list of businesses (facilities)
  ''' Used for autocomplete feature on FirstCalls page
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetBusinesses(search As String) As List(Of Business) Implements IBusinessDA.GetBusinesses

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim businesses As New List(Of Business)

    rsData = db.GetDataReader("SELECT BusinessID, BusinessName, BusinessCity FROM BUSINESS WHERE BusinessName LIKE '%" & search & "%' ORDER BY BusinessName ASC")

    If rsData.HasRows Then

      Do While rsData.Read
        Dim business As New Business
        business.BusinessID = rsData("BusinessID")
        business.Name = db.ClearNull(rsData("BusinessName"))
        business.City = db.ClearNull(rsData("BusinessCity"))
        businesses.Add(business)
      Loop

      rsData.Close()

    End If

    Return businesses

  End Function

  ''' <summary>
  ''' Insert a new Business (facility)
  ''' </summary>
  ''' <param name="b"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertBusiness(b As Business) As Integer Implements IBusinessDA.InsertBusiness

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[BUSINESS]([BusinessName],[BusinessAddress],[BusinessCity],[BusinessState],[BusinessZip],[BusinessCounty],[BusinessPhone],[BusinessExt],[BusinessAltPhone],[BusinessOwnerSupVis],[BusinessNotes])")
    SQL.Append(" VALUES ")
    SQL.Append("('" & b.Name & "',") 'BusinessName
    SQL.Append("'" & b.Address & "',") 'BusinessAddress
    SQL.Append("'" & b.City & "',") 'BusinessCity
    SQL.Append("'" & b.State & "',") 'BusinessState
    SQL.Append("'" & b.Zip & "',") 'BusinessZip
    SQL.Append("'" & b.County & "',") 'BusinessCounty
    SQL.Append("'" & b.Phone & "',") 'BusinessPhone
    SQL.Append("'" & b.PhoneExt & "',") 'BusinessExt
    SQL.Append("'" & b.AltPhone & "',") 'BusinessAltPhone
    SQL.Append("'" & b.OwnerSupVis & "',") 'BusinessOwnerSupVis
    SQL.Append("'" & b.Notes & "')") 'BusinessNotes

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  ''' <summary>
  ''' Update an existing Business (Facility)
  ''' </summary>
  ''' <param name="b"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateBusiness(b As Business) As Integer Implements IBusinessDA.UpdateBusiness

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE BUSINESS SET ")
    SQL.Append("BusinessName = '" & b.Name & "',") 'BusinessName
    SQL.Append("BusinessAddress = '" & b.Address & "',") 'BusinessAddress
    SQL.Append("BusinessCity = '" & b.City & "',") 'BusinessCity
    SQL.Append("BusinessState = '" & b.State & "',") 'BusinessState
    SQL.Append("BusinessZip = '" & b.Zip & "',") 'BusinessZip
    SQL.Append("BusinessCounty = '" & b.County & "',") 'BusinessCounty
    SQL.Append("BusinessPhone = '" & b.Phone & "',") 'BusinessPhone
    SQL.Append("BusinessExt = '" & b.PhoneExt & "',") 'BusinessExt
    SQL.Append("BusinessAltPhone = '" & b.AltPhone & "',") 'BusinessAltPhone
    SQL.Append("BusinessOwnerSupVis = '" & b.OwnerSupVis & "',") 'BusinessOwnerSupVis
    SQL.Append("BusinessNotes = '" & b.Notes & "' ") 'BusinessNotes
    SQL.Append("WHERE BusinessID = " & b.BusinessID)

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

End Class
