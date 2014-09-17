Imports System.Data.SqlClient
Imports System.Linq

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
    Dim SQL As New StringBuilder()
    Dim company As New Company()

    db = New dbUtil()

    SQL.Append("SELECT c.CompanyID, c.CompanyNumber, c.CompanyName, c.CompanyTypeID, ct.ClientType AS CompanyType, c.CompanyPhoneAnswer, c.CompanyAddress, c.CompanyCity, c.CompanyState, c.CompanyZip, c.CompanyMainTelephone")
    SQL.Append(", c.CompanyMainTelephone2nd, c.CompanyFax, c.CompanyEmail, c.CompanyInstructionSheet, c.CompanyHoursOfOperation, ci.CompanyInformation, c.CompanyAdditionalNotes ")
    SQL.Append("FROM COMPANY c WITH (NOLOCK) INNER JOIN COMPANY_TYPE ct ON ct.ClientTypeID = c.CompanyTypeID LEFT JOIN COMPANY_INFO ci ON ci.CompanyID = c.CompanyID WHERE c.CompanyID = " + id)

    rsData = db.GetDataReader(SQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
        company.Number = rsData("CompanyNumber").ToString()
        company.Name = rsData("CompanyName")
        company.TypeID = rsData("CompanyTypeID")
        company.PhoneAnswer = db.ClearNull(rsData("CompanyPhoneAnswer"))
        company.Address = db.ClearNull(rsData("CompanyAddress"))
        company.City = db.ClearNull(rsData("CompanyCity"))
        company.State = db.ClearNull(rsData("CompanyState"))
        company.Zip = db.ClearNull(rsData("CompanyZip"))
        company.MainTelephone = db.ClearNull(rsData("CompanyMainTelephone"))
        company.MainTelephone2nd = db.ClearNull(rsData("CompanyMainTelephone2nd"))
        company.Fax = db.ClearNull(rsData("CompanyFax"))
        company.Email = db.ClearNull(rsData("CompanyEmail"))
        company.InstructionSheet = db.ClearNull(rsData("CompanyInstructionSheet"))
        company.HoursOfOperation = db.ClearNull(rsData("CompanyHoursOfOperation"))
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

  ''' <summary>
  ''' Insert Company
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertCompany(c As Company) As Integer Implements ICompanyDA.InsertCompany

    Dim SQL As New StringBuilder()
    'Dim db As dbUtil 'access to db functions
    'db = New dbUtil()
    Dim newId As Integer

    SQL.Append("INSERT INTO [dbo].[COMPANY]([CompanyNumber],[CompanyName],[CompanyTypeID],[CompanyPhoneAnswer],[CompanyAddress],[CompanyCity],[CompanyState],[CompanyZip]")
    SQL.Append(",[CompanyMainTelephone],[CompanyMainTelephone2nd],[CompanyFax],[CompanyEmail],[CompanyInstructionSheet],[CompanyHoursOfOperation],[CompanyAdditionalNotes])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & c.Number & ",")
    SQL.Append("'" & c.Name & "',")
    SQL.Append("" & c.TypeID & ",")
    SQL.Append("'" & c.PhoneAnswer & "',")
    SQL.Append("'" & c.Address & "',")
    SQL.Append("'" & c.City & "',")
    SQL.Append("'" & c.State & "',")
    SQL.Append("'" & c.Zip & "',")
    SQL.Append("'" & c.MainTelephone & "',")
    SQL.Append("'" & c.MainTelephone2nd & "',")
    SQL.Append("'" & c.Fax & "',")
    SQL.Append("'" & c.Email & "',")
    SQL.Append("'" & c.InstructionSheet & "',")
    SQL.Append("'" & c.HoursOfOperation & "',")
    SQL.Append("'" & c.AdditionalNotes & "')")
    SQL.Append(" SELECT SCOPE_IDENTITY()")
    'Return db.GetID(SQL.ToString())

    Dim m_ConStr As String = ""
    m_ConStr = ConfigurationManager.ConnectionStrings("ConnectionString").ToString()
    Dim iRet As Integer = 0
    Dim conn As SqlConnection = New SqlConnection(m_ConStr)
    Try
      conn.Open()
      Dim cmd As SqlCommand = New SqlCommand(SQL.ToString(), conn)
      newId = CInt(cmd.ExecuteScalar())

      'NEED TO LOOP THRU CONTACTS AND SAVE CONTACTS FOR NEW CLIENT
      Dim cDA As New ContactDA
      Dim contacts As New List(Of Contact)
      contacts = c.Contacts
      Dim resultId As Integer
      For Each contact As Contact In contacts
        contact.CompanyID = newId
        resultId = cDA.InsertContact(contact)
      Next

    Catch ex As Exception
      conn.Close()

    Finally
      conn.Close()

    End Try

    Return newId

  End Function

  ''' <summary>
  ''' Update Company
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateCompany(c As Company) As Integer Implements ICompanyDA.UpdateCompany

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY SET ")
    SQL.Append("[CompanyNumber] = " & c.Number & ",")
    SQL.Append("[CompanyName] = '" & c.Name & "',")
    SQL.Append("[CompanyTypeID] = " & c.TypeID & ",")
    SQL.Append("[CompanyPhoneAnswer] = '" & c.PhoneAnswer & "',")
    SQL.Append("[CompanyAddress] = '" & c.Address & "',")
    SQL.Append("[CompanyCity] = '" & c.City & "',")
    SQL.Append("[CompanyState] = '" & c.State & "',")
    SQL.Append("[CompanyZip] = '" & c.Zip & "',")
    SQL.Append("[CompanyMainTelephone] = '" & c.MainTelephone & "',")
    SQL.Append("[CompanyMainTelephone2nd] = '" & c.MainTelephone2nd & "',")
    SQL.Append("[CompanyFax] = '" & c.Fax & "',")
    SQL.Append("[CompanyEmail] = '" & c.Email & "',")
    SQL.Append("[CompanyInstructionSheet] = '" & c.InstructionSheet & "',")
    SQL.Append("[CompanyHoursOfOperation] = '" & c.HoursOfOperation & "',")
    SQL.Append("[CompanyAdditionalNotes] = '" & c.AdditionalNotes & "' ")
    SQL.Append("WHERE CompanyID = " & c.CompanyID)

    Return db.GetID(SQL.ToString())

  End Function

  ''' <summary>
  ''' Updates the Additional Notes for this Company
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateCompanyAdditionalNotes(c As Company) As Integer Implements ICompanyDA.UpdateCompanyAdditionalNotes

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY SET ")
    SQL.Append("CompanyAdditionalNotes = " & If(Not String.IsNullOrEmpty(c.AdditionalNotes), "'" & c.AdditionalNotes & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & c.CompanyID)

    Return db.GetID(SQL.ToString())

  End Function

  ''' <summary>
  ''' Updates the Client Info in COMPANY_INFO table
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateCompanyInfo(c As Company) As Integer Implements ICompanyDA.UpdateCompanyInfo

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE COMPANY_INFO SET ")
    SQL.Append("CompanyInformation = " & If(Not String.IsNullOrEmpty(c.ClientInfo), "'" & c.ClientInfo & "' ", "NULL "))
    SQL.Append("WHERE CompanyID = " & c.CompanyID)

    Return db.GetID(SQL.ToString())

  End Function

  ''' <summary>
  ''' Get All Companies base on search criteria on name
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetCompanies(search As String) As List(Of Company) Implements ICompanyDA.GetCompanies

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim companies = New List(Of Company)
    Dim sql As New StringBuilder()

    db = New dbUtil()

    sql.Append("SELECT [CompanyID],[CompanyNumber],[CompanyName],[CompanyTypeID],[CompanyPhoneAnswer],[CompanyAddress],[CompanyCity],[CompanyState],[CompanyZip]")
    sql.Append(",[CompanyMainTelephone],[CompanyMainTelephone2nd],[CompanyFax],[CompanyEmail],[CompanyInstructionSheet],[CompanyHoursOfOperation],[CompanyAdditionalNotes]")
    sql.Append("FROM [dbo].[COMPANY] WITH (NOLOCK) WHERE CompanyName LIKE '" & search & "%' ORDER BY CompanyName ASC")

    rsData = db.GetDataReader(sql.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        Dim company As New Company()
        company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
        company.Number = Convert.ToInt32(rsData("CompanyNumber"))
        company.Name = rsData("CompanyName")
        company.PhoneAnswer = rsData("CompanyPhoneAnswer")
        company.Address = rsData("CompanyAddress")
        company.City = rsData("CompanyCity")
        company.State = rsData("CompanyState")
        company.Zip = rsData("CompanyZip")
        company.MainTelephone = rsData("CompanyMainTelephone")
        company.MainTelephone2nd = rsData("CompanyMainTelephone2nd")
        company.Fax = rsData("CompanyFax")
        company.Email = rsData("CompanyEmail")
        company.InstructionSheet = rsData("CompanyInstructionSheet")
        company.HoursOfOperation = rsData("CompanyHoursOfOperation")
        company.AdditionalNotes = rsData("CompanyAdditionalNotes")
        'company.ClientInfo = db.ClearNull(rsData("CompanyInformation")) //From different table
        companies.Add(company)
      Loop

      rsData.Close()

    End If

    Return companies

  End Function

  ''' <summary>
  ''' Get Companies for the autocomplete feature on the Add Client page
  ''' </summary>
  ''' <param name="search"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetCompaniesForAutoComplete(search As String) As List(Of Company) Implements ICompanyDA.GetCompaniesForAutoComplete

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader
    Dim companies = New List(Of Company)
    Dim sql As New StringBuilder()

    db = New dbUtil()

    sql.Append("SELECT [CompanyID],[CompanyNumber],[CompanyName] ")
    sql.Append("FROM [dbo].[COMPANY] WITH (NOLOCK) WHERE CompanyName LIKE '" & search & "%' ORDER BY CompanyName ASC")

    rsData = db.GetDataReader(sql.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        Dim company As New Company()
        company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
        company.Number = Convert.ToInt32(rsData("CompanyNumber"))
        company.Name = rsData("CompanyName")
        companies.Add(company)
      Loop

      rsData.Close()

    End If

    Return companies

  End Function

End Class
