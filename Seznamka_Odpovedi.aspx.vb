Class _Seznamka_Odpovedi
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyUser As New Fog.User
		Dim dt As New DataTable
		Dim Report As New Renderer.Report
		If MyUser.isLogged Then
			Dim SQL As String = "SELECT Datum,Jmeno,Email,Txt FROM SeznamkaOdpovedi WHERE UserID=" & MyUser.ID & " ORDER BY ID Desc"
			Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
			Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
			DA.Fill(dt)
			DA.Dispose()
			DB.Close()
			Me.rptSeznamkaOdpovedi.DataSource = dt
			Me.rptSeznamkaOdpovedi.DataBind()
			Report.Title = "<i>Poèet odpovìdí: " & dt.Rows.Count & "</i>"
			Me.phReport.Controls.Add(Report.Render)
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nejsi pøihlášen !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

End Class