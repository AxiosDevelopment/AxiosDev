Imports System.Data.SqlClient

Public Class BusinessDA
  Implements IBusinessDA

  Public Function GetBusiness(id As String) As Business Implements IBusinessDA.GetBusiness

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim business As New Business

    rsData = db.GetDataReader("SELECT BusID, BusinessName, BusAddress, BusCity, BusState, BusZip, BusCounty, BusPhone, BusExt FROM BusinessNames WITH (NOLOCK) WHERE BusID = " + id)

    If rsData.HasRows Then

      While rsData.Read
        business.BusinessID = rsData("BusID")
        business.Name = rsData("BusinessName")
        business.Address = db.ClearNull(rsData("BusAddress"))
        business.City = db.ClearNull(rsData("BusCity"))
        business.State = db.ClearNull(rsData("BusState"))
        business.Zip = db.ClearNull(rsData("BusZip"))
        business.County = db.ClearNull(rsData("BusCounty"))
        business.Phone = db.ClearNull(rsData("BusPhone"))
        business.PhoneExt = db.ClearNull(rsData("BusExt"))
      End While

    End If

    Return business

  End Function

  Public Function GetBusinesses() As List(Of Business) Implements IBusinessDA.GetBusinesses

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    'Dim strBusiness As String
    'Dim strHTML As New StringBuilder()

    Dim businesses As New List(Of Business)

    rsData = db.GetDataReader("SELECT BusID, BusinessName, BusCity FROM BusinessNames ORDER BY BusinessName ASC")

    If rsData.HasRows Then

      While rsData.Read
        Dim business As New Business
        business.BusinessID = rsData("BusID")
        business.Name = db.ClearNull(rsData("BusinessName"))
        business.City = db.ClearNull(rsData("BusCity"))
        businesses.Add(business)
        'strBusiness = If(Not String.IsNullOrEmpty(rsData("BusCity").ToString()), rsData("BusinessName") & " - " & rsData("BusCity"), rsData("BusinessName"))
        'strHTML.Append("<li><input type=""hidden"" class=""busId"" value=" & rsData("BusID") & " />" & strBusiness & "</li>")
      End While

      rsData.Close()
      'Response.Write(strHTML.ToString())

    End If

    Return businesses

  End Function

  Public Function GetBusinesses(search As String) As List(Of Business) Implements IBusinessDA.GetBusinesses

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim businesses As New List(Of Business)

    rsData = db.GetDataReader("SELECT BusID, BusinessName, BusCity FROM BusinessNames where BusinessName like '%" & search & "%' ORDER BY BusinessName ASC")

    If rsData.HasRows Then

      While rsData.Read
        Dim business As New Business
        business.BusinessID = rsData("BusID")
        business.Name = db.ClearNull(rsData("BusinessName"))
        business.City = db.ClearNull(rsData("BusCity"))
        businesses.Add(business)
      End While

      rsData.Close()

    End If

    Return businesses

  End Function

  Public Function InsertBusiness(c As Business) As Integer Implements IBusinessDA.InsertBusiness

  End Function

  Public Function UpdateBusiness(c As Business) As Integer Implements IBusinessDA.UpdateBusiness

  End Function

End Class
