Imports System.Data.SqlClient

Public Class CompanyDA
  Implements ICompanyDA

  Public Function GetCompanies() As List(Of Company) Implements ICompanyDA.GetCompanies

  End Function

  Public Function GetCompany(id As String) As Company Implements ICompanyDA.GetCompany

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    Dim company As New Company()

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT c.CompanyID, c.CompanyNumber, c.CompanyName, ct.ClientType, c.CompanyPhoneAnswer, ci.CompanyInformation, c.CompanyAdditionalNotes FROM COMPANY c WITH (NOLOCK) INNER JOIN COMPANY_TYPE ct ON ct.ClientTypeID = c.CompanyTypeID INNER JOIN COMPANY_INFO ci ON ci.CompanyID = c.CompanyID WHERE c.CompanyID = " + id)

    Do While rsData.Read()
      company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
      company.Number = rsData("CompanyNumber").ToString()
      company.Name = rsData("CompanyName")
      company.PhoneAnswer = rsData("CompanyPhoneAnswer")
      company.ClientInfo = db.ClearNull(rsData("CompanyInformation"))
      company.AdditionalNotes = db.ClearNull(rsData("CompanyAdditionalNotes"))
    Loop

    Return company

  End Function

  Public Function InsertCompany(c As Company) As Integer Implements ICompanyDA.InsertCompany

  End Function

  Public Function UpdateCompany(c As Company) As Integer Implements ICompanyDA.UpdateCompany

  End Function
End Class
