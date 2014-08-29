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

    strSQL.Append("SELECT t1.ContactID, t1.CompanyID, t1.ContactName, t1.ContactInfo, t1.ContactType ")
    strSQL.Append("FROM CONTACT t1 WITH (NOLOCK) ")
    strSQL.Append("WHERE t1.CREATEDATETIME = ")
    strSQL.Append("(SELECT MAX(t2.CREATEDATETIME) ")
    strSQL.Append("FROM CONTACT t2 WITH (NOLOCK) ")
    strSQL.Append("WHERE t2.CompanyID = t1.CompanyID AND t2.ContactType = t1.ContactType) ")
    strSQL.Append("AND t1.CompanyID = " & id + " ")
    strSQL.Append("ORDER BY t1.ContactType ASC")

    rsData = db.GetDataReader(strSQL.ToString())

    If rsData.HasRows Then

      Do While rsData.Read
        Dim contact As New Contact
        contact.ContactID = rsData("ContactID")
        contact.CompanyID = rsData("CompanyID")
        contact.Name = db.ClearNull(rsData("ContactName"))
        contact.Information = db.ClearNull(rsData("ContactInfo"))
        contact.Type = rsData("ContactType")
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

    SQL.Append("INSERT INTO [dbo].[CONTACT] ([CompanyID],[ContactName],[ContactInfo],[ContactType],[CreateDateTIme])")
    SQL.Append(" VALUES ")
    SQL.Append("(" & Convert.ToInt32(c.CompanyID) & ",") 'CompanyID
    SQL.Append(If(Not String.IsNullOrEmpty(c.Name), "'" & c.Name & "',", "NULL,")) 'ContactName
    SQL.Append(If(Not String.IsNullOrEmpty(c.Information), "'" & c.Information & "',", "NULL,")) 'ContactInfo
    SQL.Append(c.Type & ",") 'ContactType
    SQL.Append("'" & DateTime.Now & "')") 'CreateDateTIme

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

    Dim result As Integer
    result = InsertContact(c)
    Return result

  End Function

End Class
