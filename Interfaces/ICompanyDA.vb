Public Interface ICompanyDA
  Function GetCompany(id As String) As Company
  Function GetCompanies() As List(Of Company)
  Function InsertCompany(c As Company) As Integer
  Function UpdateCompany(c As Company) As Integer
  Function UpdateCompanyInfo(c As Company) As Integer
  Function UpdateCompanyAdditionalNotes(c As Company) As Integer
End Interface
