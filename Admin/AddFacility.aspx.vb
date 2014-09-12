Public Class AddFacility
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


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
    business.Name = placeOfDeath.Text
    business.Address = facilityAddr.Text
    business.City = facCity.Text
    business.State = facState.Text
    business.County = facilityCounty.Text
    business.Zip = facilityZip.Text
    business.Phone = facilityPhone.Text
    business.PhoneExt = phoneExt.Text

    Return business

  End Function

End Class