Imports System.Data.SqlClient

Public Class ContactDA
  Implements IContactDA

  Public Function GetContact(id As String) As Contact Implements IContactDA.GetContact

  End Function

  Public Function GetContacts() As List(Of Contact) Implements IContactDA.GetContacts

  End Function

  ''' <summary>
  ''' Gets list of contacts for a company
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetContactsByCompany(id As String) As List(Of Contact) Implements IContactDA.GetContactsByCompany

    Dim rsData As SqlDataReader
    Dim db As dbUtil = New dbUtil()
    Dim strSQL As New StringBuilder()
    Dim contacts As New List(Of Contact)

    strSQL.Append("SELECT t1.ContactID, t1.CompanyID, t1.ContactType AS TypeID, lct.ContactType, t1.ContactName, t1.ContactPhone, t1.ContactTitle, t1.ContactEmail, t1.ContactAdditionalInformation, t1.IsActive ")
    strSQL.Append("FROM CONTACT t1 WITH (NOLOCK)  ")
    strSQL.Append("INNER JOIN LK_CONTACT_TYPE lct ON lct.ContactTypeID = t1.ContactType ")
    strSQL.Append("WHERE t1.CompanyID = " & id & " AND IsActive = 1 ")
    strSQL.Append("ORDER BY t1.ContactType ASC")

    rsData = db.GetDataReader(strSQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read
        Dim contact As New Contact
        contact.ContactID = rsData("ContactID")
        contact.CompanyID = rsData("CompanyID")
        contact.TypeID = rsData("TypeID")
        contact.Type = rsData("ContactType")
        contact.Name = db.ClearNull(rsData("ContactName"))
        contact.Title = db.ClearNull(rsData("ContactTitle"))
        contact.Phone = db.ClearNull(rsData("ContactPhone"))
        contact.Email = db.ClearNull(rsData("ContactEmail"))
        contact.AdditionalInformation = db.ClearNull(rsData("ContactAdditionalInformation"))
        contact.IsActive = Convert.ToByte(rsData("IsActive"))
        contacts.Add(contact)
      Loop

      rsData.Close()

    End If

    Return contacts

  End Function

  ''' <summary>
  ''' Inserts a new contact record even if you are just updating
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function InsertContact(c As Contact) As Integer Implements IContactDA.InsertContact

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()

    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("INSERT INTO [dbo].[CONTACT]([CompanyID],[ContactType],[ContactName],[ContactTitle],[ContactPhone],[ContactEmail],[ContactAdditionalInformation],[IsActive],[UpdatedDateTime],[CreatedDateTime])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & Convert.ToInt32(c.CompanyID) & ",") 'CompanyID
    SQL.Append(c.TypeID & ",") 'ContactType
    SQL.Append(If(Not String.IsNullOrEmpty(c.Name), "'" & c.Name & "',", "NULL,")) 'ContactName
    SQL.Append(If(Not String.IsNullOrEmpty(c.Title), "'" & c.Title & "',", "NULL,")) 'Title
    SQL.Append(If(Not String.IsNullOrEmpty(c.Phone), "'" & c.Phone & "',", "NULL,")) 'ContactPhone
    SQL.Append(If(Not String.IsNullOrEmpty(c.Email), "'" & c.Email & "',", "NULL,")) 'Email
    SQL.Append(If(Not String.IsNullOrEmpty(c.AdditionalInformation), "'" & c.AdditionalInformation & "',", "NULL,")) 'AdditionalInformation
    SQL.Append("1,") 'IsActive
    SQL.Append("'" & DateTime.Now & "',") 'UpdatedDateTIme
    SQL.Append("'" & DateTime.Now & "')") 'CreatedDateTIme

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  ''' <summary>
  ''' Delegates to the INSERT Function
  ''' All updates will just insert a new record
  ''' </summary>
  ''' <param name="c"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function UpdateContact(c As Contact) As Integer Implements IContactDA.UpdateContact

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE CONTACT SET ")
    SQL.Append("[ContactType] = " & c.TypeID & ",")
    SQL.Append("[ContactName] = '" & c.Name & "',")
    SQL.Append("[ContactTitle] = '" & c.Title & "',")
    SQL.Append("[ContactPhone] = '" & c.Phone & "',")
    SQL.Append("[ContactEmail] = '" & c.Email & "',")
    SQL.Append("[ContactAdditionalInformation] = '" & c.AdditionalInformation & "',")
    SQL.Append("[UpdatedDateTime] = '" & Date.Now & "' ")
    SQL.Append("WHERE ContactID = " & c.ContactID)

    Return db.GetID(SQL.ToString())

  End Function

  ''' <summary>
  ''' Sets the flag IsActive to 0
  ''' </summary>
  ''' <param name="id"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function DeleteContact(id As Integer) As Integer Implements IContactDA.DeleteContact

    Dim returnedID As Integer
    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE CONTACT SET ")
    SQL.Append("IsActive = 0, ") 'IsActive
    SQL.Append("UpdatedDateTime = '" & Date.Now & "' ")
    SQL.Append("WHERE ContactID = " & id)

    returnedID = db.GetID(SQL.ToString())

    Return returnedID

  End Function

  Public Function UpdateContactSimple(c As Contact) As Integer Implements IContactDA.UpdateContactSimple

    Dim SQL As New StringBuilder()
    Dim db As dbUtil 'access to db functions
    db = New dbUtil()

    SQL.Append("UPDATE CONTACT SET ")
    SQL.Append("[ContactName] = " & c.Name & ",")
    SQL.Append("[ContactPhone] = '" & c.Phone & "',")
    SQL.Append("[UpdatedDateTime] = '" & Date.Now & "' ")
    SQL.Append("WHERE ContactID = " & c.ContactID)

    Return db.GetID(SQL.ToString())


  End Function
End Class
