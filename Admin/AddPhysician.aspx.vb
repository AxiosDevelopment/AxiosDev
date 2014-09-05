Public Class AddPhysician
  Inherits System.Web.UI.Page

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim docId As String

    If Not (Page.IsPostBack) Then

      Try

        GetPhysicians()

        'docId = Request.QueryString.Get("docId").ToString()

        If Not String.IsNullOrEmpty(Request.QueryString.Get("docId")) Then

          docId = Request.QueryString.Get("docId").ToString()

          If docId > 0 Then
            'Physician Exists
            GetPhysician(docId)
          End If

        End If

      Catch ex As Exception
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LoadingMessagePagePopupError", "messageLoadError()", True)
      End Try

    End If

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetPhysicians()

    'Dim dDA As New DoctorDA
    'Dim doctors As List(Of Doctor)

    'doctors = dDA.GetDoctors()

    'Dim row As HtmlTableRow
    'Dim cell As HtmlTableCell
    'Dim a As HtmlAnchor
    'Dim counter As Integer = 0

    'If doctors.Count > 0 Then

    '  For Each d As Doctor In doctors

    '    row = New HtmlTableRow()

    '    'Name
    '    cell = New HtmlTableCell
    '    a = New HtmlAnchor With {.HRef = "AddPhysician.aspx?docId=" + d.DoctorID.ToString()}
    '    a.InnerText = d.Name
    '    cell.Controls.Add(a)
    '    row.Controls.Add(cell)

    '    'Phone
    '    cell = New HtmlTableCell With {.InnerText = d.Phone}
    '    row.Controls.Add(cell)

    '    'Phone Ext
    '    cell = New HtmlTableCell With {.InnerText = "#345"}
    '    row.Controls.Add(cell)

    '    PhysiciansTable.Controls.Add(row)

    '    counter += 1

    '    If counter > 1000 Then
    '      Exit For
    '    End If

    '  Next

    'Else
    '  row = New HtmlTableRow()
    '  cell = New HtmlTableCell With {.InnerText = "No Physicians Found"}
    '  cell.ColSpan = 3
    '  row.Controls.Add(cell)
    '  PhysiciansTable.Controls.Add(row)
    'End If

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="id"></param>
  ''' <remarks></remarks>
  Private Sub GetPhysician(id As String)
    Dim dDA As New DoctorDA
    Dim doctor As Doctor

    doctor = dDA.GetDoctor(id)

  End Sub

End Class