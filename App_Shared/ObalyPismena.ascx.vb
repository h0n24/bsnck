Class _ObalyPismena_Ascx
	Inherits System.Web.UI.UserControl

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim dt As New DataTable("Pismena")
		Dim dRow As DataRow
		dt.Columns.Add("Pismeno", System.Type.GetType("System.String"))
		dt.Columns.Add("Zobrazit", System.Type.GetType("System.String"))
		dRow = dt.NewRow
		dRow("Pismeno") = "1"
		dRow("Zobrazit") = "0-9"
		dt.Rows.Add(dRow)
		Dim f As Integer
		For f = 0 To 25
			If f = 8 Then
				dRow = dt.NewRow
				dRow("Pismeno") = "CH"
				dRow("Zobrazit") = "CH"
				dt.Rows.Add(dRow)
			End If
			dRow = dt.NewRow
			dRow("Pismeno") = Chr(f + 65)
			dRow("Zobrazit") = Chr(f + 65)
			dt.Rows.Add(dRow)
		Next
		Me.rptObalyPismena.DataSource = dt
		Me.rptObalyPismena.DataBind()
	End Sub

End Class