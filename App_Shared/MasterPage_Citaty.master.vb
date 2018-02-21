Class MasterPage_Citaty
	Inherits System.Web.UI.MasterPage

	Public MyIni As New Fog.Ini.Init
	Public MyUser As New Fog.User
	Public Visits, Pages As Integer

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		If MyIni.RunLocal Then Page.Title = "###" & Page.Title
		Dim c As New HtmlControls.HtmlGenericControl("meta")
		c.Attributes("name") = "Description"
		c.Attributes("content") = MyIni.Web.Description
		Me.Page.Header.Controls.Add(c)
		c.Attributes("name") = "Keywords"
		c.Attributes("content") = MyIni.Web.Keywords
		Me.Page.Header.Controls.Add(c)
		'Me.aLogo.HRef = FN.URL.GetRootUrl
		'Me.imgLogo.Src = "/gfx/logo/" & MyIni.Web.ID & ".gif"
		'Me.imgLogo.Alt = MyIni.Web.Slogan
		Dim htmlLink As New HtmlLink()
		htmlLink.Attributes.Add("rel", "shortcut icon")
		If MyIni.Web.ID = "basnecz" Or MyIni.Web.ID = "basnickycz" Or MyIni.Web.ID = "citujcz" Then
			htmlLink.Href = "/gfx/ico/pero.ico"
		Else
			htmlLink.Href = "/gfx/ico/srdce.ico"
		End If
		Page.Header.Controls.Add(htmlLink)

		Dim Sekce As String = Request.QueryString("sekce")
		Dim URL As String = Request.RawUrl
		Pages = Val.ToInt(FN.Cookies.Read("Today", "Pages")) + 1
		FN.Cookies.WriteKey("Today", "Pages", Pages, DateTime.Today.AddDays(1))
		Visits = Val.ToInt(FN.Cookies.Read("Web", "Visits"))
		If Pages = 1 Then
			Visits += 1
			FN.Cookies.WriteKeyWeb("Visits", Visits)
		End If
	End Sub

	Function NoveVzkazy() As Boolean
		Dim VzkazyChecked As DateTime = Val.ToDate(FN.Cookies.Read("Session", "VzkazyChecked"))
		If IsNothing(VzkazyChecked) Or VzkazyChecked < Now.AddSeconds(-120) Then
			Dim SQL As String = "SELECT VzkazID FROM Vzkazy WHERE VzkazKomu=" & MyUser.ID & " AND VzkazPrecteno IS NULL"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			NoveVzkazy = DR.Read
			DR.Close()
			CMD.Connection.Close()
			FN.Cookies.WriteKeySession("VzkazyChecked", Now)
			FN.Cookies.WriteKeySession("VzkazyNew", NoveVzkazy)
		Else
			NoveVzkazy = Val.ToBoolean(FN.Cookies.Read("Session", "VzkazyNew"))
		End If
	End Function

	Sub ZvyraznitMenu(ByRef Menu As System.Web.UI.HtmlControls.HtmlGenericControl)
		Menu.Attributes.Add("style", "font-weight: bold;")
	End Sub

End Class