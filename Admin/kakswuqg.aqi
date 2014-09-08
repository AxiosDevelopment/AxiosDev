Public Class AddPhysician
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub SubmitPhysician_Click(sender As Object, e As EventArgs) Handles SubmitPhysician.Click

    If Page.IsValid Then

      Try
        Dim doctor As New Doctor
        doctor = FillPhysician(physicianId.Value)
        If physicianId.Value = "0" Then
          InsertPhysician(doctor)
        Else
          UpdatePhysician(doctor)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopup", "messagedSaved()", True)

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveMessagePopupError", "messagedSavedError()", True)

      End Try
    End If

  End Sub

  ''' <summary>
  ''' Save New Physician
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub InsertPhysician(d As Doctor)

    Dim dDA As New DoctorDA
    Dim id As Integer

    id = dDA.InsertDoctor(d)

  End Sub

  ''' <summary>
  ''' Update Existing Physician
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdatePhysician(d As Doctor)

    Dim dDA As New DoctorDA
    Dim id As Integer

    id = dDA.UpdateDoctor(d)

  End Sub

  ''' <summary>
  ''' Fill Physician (Doctor) object
  ''' </summary>
  ''' <param name="dId"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FillPhysician(dId As Integer) As Doctor

    Dim doctor As New Doctor

    doctor.DoctorID = dId
    doctor.Name = physicianName.Text
    doctor.Phone = physicianPhone.Text
    doctor.PhoneExt = physicianPhoneExt.Text

    Return doctor

  End Function

End Class