Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

Public Class AddClient
  Inherits System.Web.UI.Page

  Dim qId As String
  Dim searchString As String = ""

  ''' <summary>
  ''' Event Handler - Page_Load
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If Not (Page.IsPostBack) Then

      Try

        'Get Lookup Data
        GetClientTypeLookup()
        GetContactTypeLookup()
        Session.Remove("Contacts")

        'If Session("ClientID") IsNot Nothing Then
        '  'If Session("ClientID") <> "0" Then
        '  Dim contacts As New List(Of Contact)
        '  If Session("Contacts") IsNot Nothing Then
        '    contacts = CType(Session("Contacts"), List(Of Contact))
        '  End If
        '  Session("Contacts") = contacts
        '  BindContacts(contacts)
        '  'End If
        'End If

      Catch ex As Exception
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LoadingMessagePagePopupError", "messageLoadError()", True)
    End Try

    End If

  End Sub

  ''' <summary>
  ''' Event Handler to Update or Insert a Client
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub SubmitClient_Click(sender As Object, e As EventArgs) Handles SubmitClient.Click

    If Page.IsValid Then

      Try

        Dim company As New Company
        company = FillClient(ClientIDText.Text)
        If ClientIDText.Text = "0" Then
          InsertClient(company)
        Else
          UpdateClient(company)
        End If
        'Session.Remove("ClientID")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopup", "messagedSaved()", True)

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopupError", "messagedSavedError()", True)

      End Try
    End If

  End Sub

  ''' <summary>
  ''' Save New Facility
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub InsertClient(c As Company)

    Dim cDA As New CompanyDA
    Dim id As Integer
    Dim contacts As New List(Of Contact)

    If Session("Contacts") IsNot Nothing Then
      contacts = CType(Session("Contacts"), List(Of Contact))
    End If

    c.Contacts = contacts

    id = cDA.InsertCompany(c)

  End Sub

  ''' <summary>
  ''' Update Existing Facility
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateClient(c As Company)

    Dim cDA As New CompanyDA
    Dim id As Integer

    id = cDA.UpdateCompany(c)

  End Sub

  ''' <summary>
  ''' Fill Facility (Business) object
  ''' </summary>
  ''' <param name="fId"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FillClient(fId As Integer) As Company

    Dim company As New Company

    company.CompanyID = fId
    company.Name = nClientName.Text
    company.Number = nClientNumber.Text
    company.TypeID = ClientType.SelectedItem.Value
    company.Address = nClientAddress.Text
    company.City = nClientCity.Text
    company.State = nClientState.Text
    company.Zip = nClientZip.Text
    company.MainTelephone = nClientPhone.Text
    company.MainTelephone2nd = nClientPhone2.Text
    company.Fax = nClientFax.Text
    company.PhoneAnswer = nClientGreeting.Text
    company.HoursOfOperation = nClientHours.Text
    company.AdditionalNotes = nClientAdditionalInformation.Text
    company.InstructionSheet = nClientSpecialInstructions.Text

    Return company

  End Function

  ''' <summary>
  ''' Get Client Types for Dropdown control
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetClientTypeLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT ClientTypeID, ClientType FROM COMPANY_TYPE WITH (NOLOCK) ORDER BY ClientType ASC")

    Do While rsData.Read()
      ClientType.Items.Add(New ListItem(rsData("ClientType").ToString(), rsData("ClientTypeID").ToString()))
    Loop

  End Sub

  ''' <summary>
  ''' Gets Contact Types of Dropdown control
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetContactTypeLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT ContactTypeID, ContactType FROM LK_CONTACT_TYPE WITH (NOLOCK) ORDER BY ContactTypeID ASC")

    Do While rsData.Read()
      ddContactType.Items.Add(New ListItem(rsData("ContactType").ToString(), rsData("ContactTypeID").ToString()))
    Loop

  End Sub

  ''' <summary>
  ''' Trigger event that is needed to bind the contacts when selecting a client from the ajax call
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub btnTriggerUpdatePanel_Click(sender As Object, e As EventArgs)

    Dim cDA As New CompanyDA
    Dim company As Company

    company = cDA.GetCompany(ClientIDText.Text)
    Session("Contacts") = company.Contacts

    BindContacts(company.Contacts)

    'Session("ClientID") = ClientIDText.Text

  End Sub

  ''' <summary>
  ''' When Inserting or Updating a Contact
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub SubmitContact_Click(sender As Object, e As EventArgs) Handles SubmitContact.Click

    Dim contacts As New List(Of Contact)
    Dim ctDA As New ContactDA
    Dim resultId As New Integer

    If ClientIDText.Text = "0" Then

      'We will only cache the contacts if the Client is a new cient (hasn't been saved and doesn't have an ID yet)
      If Session("Contacts") IsNot Nothing Then
        contacts = CType(Session("Contacts"), List(Of Contact))
      End If
      Dim contact = FillContact(contacts.Count + 1, ClientIDText.Text)
      contacts.Add(contact)

    Else
      'Insert or Update new Contact for existing client
      If hContactID.Value = "0" Then
        'Insert New Contact
        Dim contact = FillContact(0, ClientIDText.Text)
        resultId = ctDA.InsertContact(contact)
      Else
        'Update Existing Contact
        Dim contact = FillContact(hContactID.Value, ClientIDText.Text)
        resultId = ctDA.UpdateContact(contact)
      End If

      Dim cDA As New ContactDA
      contacts = cDA.GetContactsByCompany(ClientIDText.Text)

    End If

    'Add contacts to Session
    Session("Contacts") = contacts

    'Bind contacts to gridview
    BindContacts(contacts)

    If ClientIDText.Text = "0" And contacts.Count = 1 Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MustSaveClientToSaveContacts", "MustSaveClientToSaveContacts()", True)
    End If

  End Sub

  ''' <summary>
  ''' Event Handler on selecting Edit for a Contact Row
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub grvContacts_RowEditing(sender As Object, e As GridViewEditEventArgs)

    Dim index = Convert.ToInt32(e.NewEditIndex)
    Dim row As GridViewRow
    row = grvContacts.Rows(index)

    hContactID.Value = row.Cells(2).Text
    If Not IsNothing(ddContactType.Items.FindByValue(row.Cells(4).Text)) Then
      ddContactType.SelectedValue = row.Cells(4).Text
    Else
      ddContactType.SelectedValue = -1
    End If
    nContactName.Text = row.Cells(6).Text
    nContactJobTitle.Text = If(row.Cells(7).Text = "&nbsp;", "", row.Cells(7).Text)
    nContactPhone.Text = If(row.Cells(8).Text = "&nbsp;", "", row.Cells(8).Text)
    nContactEmail.Text = If(row.Cells(9).Text = "&nbsp;", "", row.Cells(9).Text)
    nContactAdditionalInformation.Text = If(row.Cells(10).Text = "&nbsp;", "", row.Cells(10).Text)

    e.Cancel = True

  End Sub

  ''' <summary>
  ''' Event Handler on selecting Delete for a Contact Row
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub grvContacts_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

    Dim index = Convert.ToInt32(e.RowIndex)
    Dim row As GridViewRow
    Dim isThisThePrimary As Contact
    Dim contacts As New List(Of Contact)
    Dim cDA As New ContactDA

    row = grvContacts.Rows(index)

    'Contacts will only be in session for new client
    If Session("Contacts") IsNot Nothing Then
      contacts = CType(Session("Contacts"), List(Of Contact))

      Dim numberOfPrimaries = (From p In contacts
                               Where p.TypeID = 1
                               Select p).Count()

      isThisThePrimary = (From pc In contacts
                               Where pc.ContactID = row.Cells(2).Text
                               Select pc).SingleOrDefault()

      If numberOfPrimaries = 1 Then
        If isThisThePrimary IsNot Nothing Then
          If isThisThePrimary.TypeID = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NoPrimaryContact", "NoPrimaryContact()", True)
          Else
            'DELETE CONTACT HERE
            Dim resultID = cDA.DeleteContact(isThisThePrimary.ContactID)
            contacts.Remove(isThisThePrimary)
            Session("Contacts") = contacts
            BindContacts(contacts)
          End If
        End If
      Else
        'DELETE CONTACT HERE
        Dim resultID = cDA.DeleteContact(isThisThePrimary.ContactID)
        contacts.Remove(isThisThePrimary)
        Session("Contacts") = contacts
        BindContacts(contacts)
      End If
    Else

    End If
  End Sub

  ''' <summary>
  ''' Clears controls for the Contact section within the UpdatePanel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub ClearContact_Click(sender As Object, e As EventArgs) Handles ClearContact.Click

    ClearContactControls()
    If resetContactSession.Value = "1" Then
      Session.Remove("Contacts")
      resetContactSession.Value = "0"
      grvContacts.DataSource = Nothing
      grvContacts.DataBind()
    End If

  End Sub

  ''' <summary>
  ''' Fill a Contact object with data from aspx page
  ''' </summary>
  ''' <param name="ctId"></param>
  ''' <param name="cid"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FillContact(ctId As Integer, cid As Integer) As Contact

    Dim contact As New Contact

    contact.ContactID = ctId
    contact.CompanyID = cid
    contact.TypeID = ddContactType.SelectedValue
    contact.Type = ddContactType.SelectedItem.Text
    contact.Name = nContactName.Text
    contact.Title = nContactJobTitle.Text
    contact.Phone = nContactPhone.Text
    contact.Email = nContactEmail.Text
    contact.AdditionalInformation = nContactAdditionalInformation.Text

    Return contact

  End Function

  ''' <summary>
  ''' Clear the controls for the contact information
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub ClearContactControls()

    hContactID.Value = "0"
    ddContactType.SelectedValue = -1
    nContactName.Text = ""
    nContactJobTitle.Text = ""
    nContactPhone.Text = ""
    nContactEmail.Text = ""
    nContactAdditionalInformation.Text = ""

  End Sub

  ''' <summary>
  ''' Bind List of Contacts to GridView
  ''' Must show all columns first, then bind, and then hide them - each time
  ''' If not, then the values of the columns that are hidden won't be available
  ''' </summary>
  ''' <param name="contacts"></param>
  ''' <remarks></remarks>
  Private Sub BindContacts(contacts As List(Of Contact))

    grvContacts.Columns(2).Visible = True 'Contact ID
    grvContacts.Columns(3).Visible = True 'Company ID
    grvContacts.Columns(4).Visible = True 'Contact Type ID
    grvContacts.Columns(10).Visible = True 'Additional Information
    grvContacts.Columns(11).Visible = True 'IsActive
    grvContacts.Columns(12).Visible = True 'UpdatedDateTime
    grvContacts.Columns(13).Visible = True 'CreatedDateTime

    grvContacts.DataSource = contacts
    grvContacts.DataBind()

    grvContacts.Columns(2).Visible = False 'Contact ID
    grvContacts.Columns(3).Visible = False 'Company ID
    grvContacts.Columns(4).Visible = False 'Contact Type ID
    grvContacts.Columns(10).Visible = False 'Additional Information
    grvContacts.Columns(11).Visible = False 'IsActive
    grvContacts.Columns(12).Visible = False 'UpdatedDateTime
    grvContacts.Columns(13).Visible = False 'CreatedDateTime

    ClearContactControls()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub resetForm_Click(sender As Object, e As EventArgs)
    Session.Remove("Contacts")
    grvContacts.DataSource = Nothing
    grvContacts.DataBind()
    resetContactSession.Value = "1"
  End Sub

End Class