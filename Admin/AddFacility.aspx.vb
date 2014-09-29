Imports System.Data.SqlClient

Public Class AddFacility
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If Not (Page.IsPostBack) Then

      Try

        'Get Lookup Data
        GetFacilityTypeLookup()

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LoadingMessagePagePopupError", "messageLoadError()", True)
      End Try

    End If

  End Sub

  ''' <summary>
  ''' Event Handler on Submit Facility button
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub SubmitFacility_Click(sender As Object, e As EventArgs) Handles SubmitFacility.Click

    If Page.IsValid Then

      Try
        Dim business As New Business
        business = FillFacility(facilityId.Value)
        If facilityId.Value = "0" Then
          InsertFacility(business)
        Else
          UpdateFacility(business)
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
  Private Sub InsertFacility(b As Business)

    Dim bDA As New BusinessDA
    Dim id As Integer

    id = bDA.InsertBusiness(b)

  End Sub

  ''' <summary>
  ''' Update Existing Facility
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateFacility(b As Business)

    Dim bDA As New BusinessDA
    Dim id As Integer

    id = bDA.UpdateBusiness(b)

  End Sub

  ''' <summary>
  ''' Fill Facility (Business) object
  ''' </summary>
  ''' <param name="fId"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FillFacility(fId As Integer) As Business

    Dim business As New Business

    business.BusinessID = fId
    business.TypeID = FacilityType.SelectedItem.Value
    business.Name = placeOfDeath.Text
    business.Address = facilityAddr.Text
    business.City = facCity.Text
    business.State = facState.Text
    business.County = facilityCounty.Text
    business.Zip = facilityZip.Text
    business.Phone = facilityPhone.Text
    business.PhoneExt = phoneExt.Text
    business.Notes = facilityNotesA.Text

    Return business

  End Function

  ''' <summary>
  ''' Get Client Types for Dropdown control
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetFacilityTypeLookup()

    Dim db As dbUtil 'access to db functions
    Dim rsData As SqlDataReader

    db = New dbUtil()
    rsData = db.GetDataReader("SELECT BusinessTypeID, BusinessType FROM LK_BUSINESS_TYPE WITH (NOLOCK) ORDER BY BusinessType ASC")

    Do While rsData.Read()
      FacilityType.Items.Add(New ListItem(rsData("BusinessType").ToString(), rsData("BusinessTypeID").ToString()))
    Loop

  End Sub
End Class