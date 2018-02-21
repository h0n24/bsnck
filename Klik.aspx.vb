Class _Klik
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyIni As New Fog.Ini.Init
		Dim BannerID As Integer = Val.ToInt(Request.QueryString("banner"))
		Dim BannerUrl, RedirFrom, RedirIP As String
		Dim SQL As String = "SELECT BannerURL FROM ReklamaBannery WHERE BannerID=" & BannerID
		Dim dt As DataTable = New DataTable("ReklamaBannery")
		Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(SQL, DBConn)
		DBDA.Fill(dt)
		DBDA.Dispose()
		If dt.Rows.Count > 0 Then
			BannerUrl = dt.Rows(0)("BannerUrl")
			RedirIP = Request.UserHostAddress
			RedirFrom = Request.ServerVariables("HTTP_REFERER")
			'	RedirIP = Request.ServerVariables("REMOTE_ADDR")
			'	RedirFrom = Request.QueryString("from")	- lze použít pro rozlišení napø. EmailBasne
			If Not (FN.Users.Prava.StatistikyOff) Then
				If Application("Reklama.PosledniRedir") <> (BannerID & "-" & RedirIP) Then
					Application("Reklama.PosledniRedir") = BannerID & "-" & RedirIP
					SQL = "INSERT INTO ReklamaRedir (RedirDatum,RedirIP,RedirBanner,RedirFrom) Values (" & FN.DB.GetDateTime(Now) & ", '" & RedirIP & "', " & BannerID & ", '" & RedirFrom & "')"
					Dim DBCmd As New System.Data.SqlClient.SqlCommand(SQL, DBConn)
					DBCmd.ExecuteNonQuery()
				End If
				DBConn.Close()
				Response.Redirect(BannerUrl)
			Else
				Dim Report As New Renderer.Report
				Report.Title = "Vyplé statistiky !!"
				Report.Text = "Cílová stránka: <a href=""" & BannerUrl & """>" & BannerUrl & "</a>"
				Report.Text &= "<br/>Referer:" & RedirFrom
				Me.phReport.Controls.Add(Report.Render)
			End If
		End If
		DBConn.Close()
	End Sub

End Class