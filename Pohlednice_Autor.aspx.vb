Class _Pohlednice_Autor
	Inherits System.Web.UI.Page

	Public AutorID As Integer
	Public DR As System.Data.SqlClient.SqlDataReader

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		AutorID = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "SELECT * FROM PohledniceAutori WHERE AutorID=" & AutorID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		DR = CMD.ExecuteReader()
		If DR.Read Then
			If DR("AutorJmeno") = "" Then Me.pPohledniceAutoriJmeno.Visible = False
			If DR("AutorAdresa") = "" Then Me.pPohledniceAutoriAdresa.Visible = False
			If DR("AutorEmail") = "" Then Me.pPohledniceAutoriEmail.Visible = False
			If DR("AutorWeb") = "" Then Me.pPohledniceAutoriWeb.Visible = False
			If DR("AutorPopis") = "" Then Me.pPohledniceAutoriPopis.Visible = False
			Me.phMain.DataBind()
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "Autor nenalezen."
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class