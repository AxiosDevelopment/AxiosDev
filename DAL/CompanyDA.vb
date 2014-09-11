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

  ''' <summary>
  ''' Insert Company
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertCompany(c As Company) As Integer Implements ICompanyDA.InsertCompany

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("NSERT INTO [dbo].[COMPANY]([CompanyNumber],[CompanyName],[CompanyTypeID],[CompanyPhoneAnswer],[CompanyPhoneForwarded],[CompanyMainTelephone],[CompanyMainTelephone2nd],[CompanyMainTelephone3rd],[CompanyBacklines]")
    SQL.Append(",[CompanyFax],[CompanyFax2],[CompanyEmail],[CompanyMailingAddress],[CompanyMailingAddress2],[CompanyMailingCity],[CompanyMailingState],[CompanyMailingZip],[CompanyMailingCountry],[CompanyBillingAddress]")
    SQL.Append(",[CompanyBillingAddress2],[CompanyBillingCity],[CompanyBillingState],[CompanyBillingZip],[CompanyBillingCountry],[CompanyBillBaseRate],[CompanyCallOutCharge],[CompanyCallOutLimit],[CompanyMsgLimit]")
    SQL.Append(",[CompanyFirstCallProc],[CompanyMsgProc],[CompanyInstructionSheet],[CompanyClActive],[CompanyMenuNotes],[CompanyTransferNo],[CompanyEmployeeList],[CompanyAdditionalNotes])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & c.Number & ",")
    SQL.Append("'" & c.Name & "',")
    SQL.Append("" & c.TypeID & ",")
    SQL.Append("'" & c.PhoneAnswer & "',")
    SQL.Append("'" & c.PhoneForwarded & "',")
    SQL.Append("'" & c.MainTelephone & "',")
    SQL.Append("'" & c.MainTelephone2nd & "',")
    SQL.Append("'" & c.MainTelephone3rd & "',")
    SQL.Append("'" & c.Backlines & "',")
    SQL.Append("'" & c.Fax & "',")
    SQL.Append("'" & c.Fax2 & "',")
    SQL.Append("'" & c.Email & "',")
    SQL.Append("'" & c.MailingAddress & "',")
    SQL.Append("'" & c.MailingAddress2 & "',")
    SQL.Append("'" & c.MailingCity & "',")
    SQL.Append("'" & c.MailingState & "',")
    SQL.Append("'" & c.MailingZip & "',")
    SQL.Append("'" & c.MailingCountry & "',")
    SQL.Append("'" & c.BillingAddress & "',")
    SQL.Append("'" & c.BillingAddress2 & "',")
    SQL.Append("'" & c.BillingCity & "',")
    SQL.Append("'" & c.BillingState & "',")
    SQL.Append("'" & c.BillingZip & "',")
    SQL.Append("'" & c.BillingCountry & "',")
    SQL.Append("" & c.BillBaseRate & ",")
    SQL.Append("" & c.CallOutCharge & ",")
    SQL.Append("" & c.CallOutLimit & ",")
    SQL.Append("" & c.MsgLimit & ",")
    SQL.Append("'" & c.FirstCallProc & "',")
    SQL.Append("'" & c.MsgProc & "',")
    SQL.Append("'" & c.InstructionSheet & "',")
    SQL.Append("" & c.CIActive & ",")
    SQL.Append("'" & c.MenuNotes & "',")
    SQL.Append("'" & c.TransferNo & "',")
    SQL.Append("'" & c.EmployeeList & "',")
    SQL.Append("'" & c.AdditionalNotes & "')")

    Return db.GetID(SQL.ToString())

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
    SQL.Append("[CompanyNumber] = " & c.CompanyID & ",")
    SQL.Append("[CompanyName] = '" & c.Name & "',")
    SQL.Append("[CompanyTypeID] = " & c.TypeID & ",")
    SQL.Append("[CompanyPhoneAnswer] = '" & c.PhoneAnswer & "',")
    SQL.Append("[CompanyPhoneForwarded] = '" & c.PhoneForwarded & "',")
    SQL.Append("[CompanyMainTelephone] = '" & c.MainTelephone & "',")
    SQL.Append("[CompanyMainTelephone2nd] = '" & c.MainTelephone2nd & "',")
    SQL.Append("[CompanyMainTelephone3rd] = '" & c.MainTelephone3rd & "',")
    SQL.Append("[CompanyBacklines] = '" & c.Backlines & "',")
    SQL.Append("[CompanyFax] = '" & c.Fax & "',")
    SQL.Append("[CompanyFax2] = '" & c.Fax2 & "',")
    SQL.Append("[CompanyEmail] = '" & c.Email & "',")
    SQL.Append("[CompanyMailingAddress] = '" & c.MailingAddress & "',")
    SQL.Append("[CompanyMailingAddress2] = '" & c.MailingAddress2 & "',")
    SQL.Append("[CompanyMailingCity] = '" & c.MailingCity & "',")
    SQL.Append("[CompanyMailingState] = '" & c.MailingState & "',")
    SQL.Append("[CompanyMailingZip] = '" & c.MailingZip & "',")
    SQL.Append("[CompanyMailingCountry] = '" & c.MailingCountry & "',")
    SQL.Append("[CompanyBillingAddress] = '" & c.BillingAddress & "',")
    SQL.Append("[CompanyBillingAddress2] = '" & c.BillingAddress2 & "',")
    SQL.Append("[CompanyBillingCity] = '" & c.BillingCity & "',")
    SQL.Append("[CompanyBillingState] = '" & c.BillingState & "',")
    SQL.Append("[CompanyBillingZip] = '" & c.BillingZip & "',")
    SQL.Append("[CompanyBillingCountry] = '" & c.BillingCountry & "',")
    SQL.Append("[CompanyBillBaseRate] = " & c.BillBaseRate & ",")
    SQL.Append("[CompanyCallOutCharge] = " & c.CallOutCharge & ",")
    SQL.Append("[CompanyCallOutLimit] = " & c.CallOutLimit & ",")
    SQL.Append("[CompanyMsgLimit] = " & c.MsgLimit & ",")
    SQL.Append("[CompanyFirstCallProc] = '" & c.FirstCallProc & "',")
    SQL.Append("[CompanyMsgProc] = '" & c.MsgProc & "',")
    SQL.Append("[CompanyInstructionSheet] = '" & c.InstructionSheet & "',")
    SQL.Append("[CompanyClActive] = " & c.CIActive & ",")
    SQL.Append("[CompanyMenuNotes] = '" & c.MenuNotes & "',")
    SQL.Append("[CompanyTransferNo] = '" & c.TransferNo & "',")
    SQL.Append("[CompanyEmployeeList] = '" & c.EmployeeList & "',")
    SQL.Append("[CompanyAdditionalNotes] = '" & c.AdditionalNotes & "'")
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

    sql.Append("SELECT [CompanyID],[CompanyNumber],[CompanyName],[CompanyTypeID],[CompanyPhoneAnswer],[CompanyPhoneForwarded],[CompanyMainTelephone],[CompanyMainTelephone2nd],[CompanyMainTelephone3rd],[CompanyBacklines]")
    sql.Append(",[CompanyFax],[CompanyFax2],[CompanyEmail],[CompanyMailingAddress],[CompanyMailingAddress2],[CompanyMailingCity],[CompanyMailingState],[CompanyMailingZip],[CompanyMailingCountry],[CompanyBillingAddress]")
    sql.Append(",[CompanyBillingAddress2],[CompanyBillingCity],[CompanyBillingState],[CompanyBillingZip],[CompanyBillingCountry],[CompanyBillBaseRate],[CompanyCallOutCharge],[CompanyCallOutLimit],[CompanyMsgLimit]")
    sql.Append(",[CompanyFirstCallProc],[CompanyMsgProc],[CompanyInstructionSheet],[CompanyClActive],[CompanyMenuNotes],[CompanyTransferNo],[CompanyEmployeeList],[CompanyAdditionalNotes]")
    sql.Append("FROM [dbo].[COMPANY] WITH (NOLOCK) WHERE CompanyName LIKE '%" & search & "%' ORDER BY CompanyName ASC")

    rsData = db.GetDataReader(sql.ToString())

    If rsData.HasRows Then

      Do While rsData.Read()
        Dim company As New Company()
        company.CompanyID = Convert.ToInt32(rsData("CompanyID"))
        company.Number = Convert.ToInt32(rsData("CompanyNumber"))
        company.Name = rsData("CompanyName")
        company.PhoneAnswer = rsData("CompanyPhoneAnswer")
        company.PhoneForwarded = rsData("CompanyPhoneForwarded")
        company.MainTelephone = rsData("CompanyMainTelephone")
        company.MainTelephone2nd = rsData("CompanyMainTelephone2nd")
        company.MainTelephone3rd = rsData("CompanyMainTelephone3rd")
        company.Backlines = rsData("CompanyBacklines")
        company.Fax = rsData("CompanyFax")
        company.Fax2 = rsData("CompanyFax2")
        company.Email = rsData("CompanyEmail")
        company.MailingAddress = rsData("CompanyMailingAddress")
        company.MailingAddress2 = rsData("CompanyMailingAddress2")
        company.MailingCity = rsData("CompanyMailingCity")
        company.MailingState = rsData("CompanyMailingState")
        company.MailingZip = rsData("CompanyMailingZip")
        company.MailingCountry = rsData("CompanyMailingCountry")
        company.BillingAddress = rsData("CompanyBillingAddress")
        company.BillingAddress2 = rsData("CompanyBillingAddress2")
        company.BillingCity = rsData("CompanyBillingCity")
        company.BillingState = rsData("CompanyBillingState")
        company.BillingZip = rsData("CompanyBillingZip")
        company.BillingCountry = rsData("CompanyBillingCountry")
        company.BillBaseRate = rsData("CompanyBillBaseRate")
        company.CallOutCharge = rsData("CompanyCallOutCharge")
        company.CallOutLimit = rsData("CompanyCallOutLimit")
        company.MsgLimit = rsData("CompanyMsgLimit")
        company.FirstCallProc = rsData("CompanyFirstCallProc")
        company.MsgProc = rsData("CompanyMsgProc")
        company.InstructionSheet = rsData("CompanyInstructionSheet")
        company.CIActive = Convert.ToByte(rsData("CompanyClActive"))
        company.MenuNotes = rsData("CompanyMenuNotes")
        company.TransferNo = rsData("CompanyTransferNo")
        company.EmployeeList = rsData("CompanyEmployeeList")
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
    sql.Append("FROM [dbo].[COMPANY] WITH (NOLOCK) WHERE CompanyName LIKE '%" & search & "%' ORDER BY CompanyName ASC")

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
