Imports System.Data.SqlClient

Public Class CompanyDA
  Implements ICompanyDA

  Public Function GetCompanies() As List(Of Company) Implements ICompanyDA.GetCompanies

  End Function

  ''' <summary>
  ''' Gets company data based on company id.
  ''' Also gets the contacts associated to this company
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetCompany(id As String) As Company Implements ICompanyDA.GetCompany

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    Dim company As New Company()

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT c.CompanyID, c.CompanyNumber, c.CompanyName, ct.ClientType, c.CompanyPhoneAnswer, ci.CompanyInformation, c.CompanyAdditionalNotes FROM COMPANY c WITH (NOLOCK) INNER JOIN COMPANY_TYPE ct ON ct.ClientTypeID = c.CompanyTypeID INNER JOIN COMPANY_INFO ci ON ci.CompanyID = c.CompanyID WHERE c.CompanyID = " + id)

    If rsData.HasRows Then

      Do While rsData.Read()
        company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
        company.Number = rsData("CompanyNumber").ToString()
        company.Name = rsData("CompanyName")
        company.PhoneAnswer = rsData("CompanyPhoneAnswer")
        company.ClientInfo = db.ClearNull(rsData("CompanyInformation"))
        company.AdditionalNotes = db.ClearNull(rsData("CompanyAdditionalNotes"))
      Loop

      rsData.Close()

    End If

    'GET CONTACTS FOR COMPANY
    Dim contacts As New List(Of Contact)
    Dim cDA As New ContactDA

    company.Contacts = cDA.GetContactsByCompany(id)

    Return company

  End Function

  Public Function InsertCompany(c As Company) As Integer Implements ICompanyDA.InsertCompany

  End Function

  Public Function UpdateCompany(c As Company) As Integer Implements ICompanyDA.UpdateCompany

  End Function

  ''' <summary>
  ''' Updates the Additional Notes for this Company
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateCompanyAdditionalNotes(c As Company) As Integer Implements ICompanyDA.UpdateCompanyAdditionalNotes

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY SET ")
    SQL.Append("CompanyAdditionalNotes = " & If(Not String.IsNullOrEmpty(c.AdditionalNotes), "'" & c.AdditionalNotes & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & c.CompanyID)

    returnedID = db.GetID(SQL.ToString())

  End Function

  ''' <summary>
  ''' Updates the Client Info in COMPANY_INFO table
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateCompanyInfo(c As Company) As Integer Implements ICompanyDA.UpdateCompanyInfo

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY_INFO SET ")
    SQL.Append("CompanyInformation = " & If(Not String.IsNullOrEmpty(c.ClientInfo), "'" & c.ClientInfo & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & c.CompanyID)

    returnedID = db.GetID(SQL.ToString())

  End Function

End Class
