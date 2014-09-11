Public Class AddClient
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

  Protected Sub SubmitClient_Click(sender As Object, e As EventArgs) Handles SubmitClient.Click

    If Page.IsValid Then

      Try
        Dim company As New Company
        company = FillClient(clientId.Value)
        If clientId.Value = "0" Then
          InsertClient(company)
        Else
          UpdateClient(company)
        End If
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

    'id = cDA.InsertCompany(c)

  End Sub

  ''' <summary>
  ''' Update Existing Facility
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateClient(c As Company)

    Dim cDA As New CompanyDA
    Dim id As Integer

    'id = cDA.UpdateCompany(c)

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
    company.TypeID = nClientType.Text 'NEED TO FIX
    company.MailingAddress = nClientAddress.Text
    company.MailingCity = nClientCity.Text
    company.MailingState = nClientState.Text
    company.MailingZip = nClientZip.Text
    company.MainTelephone = nClientPhone.Text
    'company = nClientExt.Text 'NOT IN DB - we will change MainTelephone3rd to MainTelephoneExt in DB
    company.MainTelephone2nd = nClientPhone2.Text 'NOT IN DB
    company.Fax = nClientFax.Text
    company.PhoneAnswer = nClientGreeting.Text
    'company. = nClientHours.Text 'NOT IN DB - need to add field
    Dim contact As New Contact
    contact.ContactID = -1
    contact.CompanyID = fId
    contact.Name = nClientPrimary.Text
    contact.Type = "1"
    contact.CreateDateTime = FormatDateTime(DateTime.Now, DateFormat.GeneralDate)
    'contact. = nClientPrimaryTitle.Text 'NOT IN DB
    company.Contacts.Add(contact)
    'company = nClientBillingContact.Text 'NOT IN DB
    'company = nClientBillingPhone.Text 'NOT IN DB
    'company = nClientBillingEmail.Text 'NOT IN DB
    company.InstructionSheet = nClientSpecialInstructions.Text

    Return company

  End Function

End Class