Partial Class GeoFinal_MasterPage
	Inherits System.Web.UI.MasterPage

	Public MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If MyIni.RunLocal Then Page.Title = "###" & Page.Title
		Dim c As New HtmlControls.HtmlGenericControl("meta")
		c.Attributes("name") = "Description"
		c.Attributes("content") = MyIni.Web.Description
		Page.Header.Controls.Add(c)
		c.Attributes("name") = "Keywords"
		c.Attributes("content") = MyIni.Web.Keywords
		Page.Header.Controls.Add(c)
		aLogo.HRef = FN.URL.GetRootUrl
		imgLogo.Alt = MyIni.Web.Slogan
		lblLogo.InnerText = MyIni.Web.Slogan

		Dim IP As String = Request.UserHostAddress
		If IP = "x.y.z.a" Then
			Response.End()
		End If

		Dim User As New Geo.User
		User.FillFromCookies()
		If User.Exists Then


			If User.ID = 75 Or User.ID = 76 Or User.ID = 78 Or User.ID = 79 Then
				Response.End()
			End If

			pnlUser.Visible = True
			pnlGeocaching.Visible = True
			If User.ID = 9 Then pnlAdmin.Visible = True
			Dim SQL As String = "SELECT SUM(CreditAmount) FROM Credits WHERE CreditUser=" & User.ID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim Credits As Integer = Val.ToInt(CMD.ExecuteScalar)
			lblCredits.Text = "Credits: " & Credits
		Else
			pnlLogin.Visible = True
		End If

		Dim Visits, Pages As Integer
		Pages = Val.ToInt(FN.Cookies.Read("Today", "Pages")) + 1
		FN.Cookies.WriteKey("Today", "Pages", Pages, DateTime.Today.AddDays(1))
		Visits = Val.ToInt(FN.Cookies.Read("Web", "Visits"))
		If Pages = 1 Then
			Visits += 1
			FN.Cookies.WriteKeyWeb("Visits", Visits)
		End If

	End Sub

End Class