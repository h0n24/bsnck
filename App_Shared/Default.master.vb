Class Default_MasterPage
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
		aLogo.Visible = False
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
		If MyIni.Web.ID = "prejcz" Then
			Me.pnlMenuPrej.Visible = True
			Me.menuDopPrej.Visible = False
			Me.menuDopIfun.Visible = False
			Me.hDiskusniForum.Visible = False
			If Sekce = "Prani" Then
				ZvyraznitMenu(Me.menuPrejPrani)
				Me.menuPrejPraniOpen.Visible = True
			ElseIf Sekce = "Pohlednice" Or URL.IndexOf("/Pohlednice") <> -1 Then
				ZvyraznitMenu(Me.menuPrejPohlednice)
				Me.menuPrejPohledniceOpen.Visible = True
			ElseIf Sekce = "Vtipy" Or URL.IndexOf("/Vtipy") <> -1 Then
				ZvyraznitMenu(Me.menuPrejVtipy)
				Me.menuPrejVtipyOpen.Visible = True
			ElseIf Sekce = "Romantika" Then
				ZvyraznitMenu(Me.menuPrejRomantika)
				Me.menuPrejRomantikaOpen.Visible = True
			End If
		ElseIf MyIni.Web.ID = "citujcz" Then
			Me.pnlMenuCitaty.Visible = True
			Me.menuDopCitaty.Visible = False
			Me.hDiskusniForum.Visible = False
			If Sekce = "Citaty" Then
				ZvyraznitMenu(Me.menuCitatyCitaty)
				Me.menuCitatyCitatyOpen.Visible = True
			ElseIf Sekce = "Prislovi" Then
				ZvyraznitMenu(Me.menuCitatyPrislovi)
				Me.menuCitatyPrisloviOpen.Visible = True
			ElseIf Sekce = "Motta" Then
				ZvyraznitMenu(Me.menuCitatyMotta)
				Me.menuCitatyMottaOpen.Visible = True
			ElseIf Sekce = "Zamysleni" Then
				ZvyraznitMenu(Me.menuCitatyZamysleni)
				Me.menuCitatyZamysleniOpen.Visible = True
			End If
		ElseIf MyIni.Web.ID = "basnecz" Or MyIni.Web.ID = "basnickycz" Then
			menuPremiumMember.Visible = True
			If MyIni.Web.ID = "basnecz" Then
				menuLiterBasnicky.Visible = False
			Else
				menuLiterBasne.Visible = False
			End If
			Me.pnlMenuLiter.Visible = True
			Me.hDiskusniForum.Visible = True
			Me.hKnihaNavstev.Visible = False
			Me.menuDopBasnicky.Visible = False
			'Me.phMenuSluzbyRss.Visible = True
			If Sekce = "Basne" Then ZvyraznitMenu(Me.menuLiterBasne)
			If Sekce = "Basnicky" Then ZvyraznitMenu(Me.menuLiterBasnicky)
			If Sekce = "Povidky" Then ZvyraznitMenu(Me.menuLiterPovidky)
			If Sekce = "Uvahy" Then ZvyraznitMenu(Me.menuLiterUvahy)
			If Sekce = "Pohadky" Then ZvyraznitMenu(Me.menuLiterPohadky)
			If Sekce = "Fejetony" Then ZvyraznitMenu(Me.menuLiterFejetony)
			If Sekce = "Romany" Then ZvyraznitMenu(Me.menuLiterRomany)
			If Sekce = "Reportaze" Then ZvyraznitMenu(Me.menuLiterReportaze)
		End If
		If MyUser.isLogged Then
			Me.pnlRightLogin.Visible = False
			Me.pnlRightUser.Visible = True
			If NoveVzkazy() = False Then Me.spanVzkazNew.Visible = False
			If MyUser.isAdmin Then
				Me.pnlRightAdmin.Visible = True
			End If
			If MyUser.Autor And Array.IndexOf("litercz,basnecz,basnickycz".Split(","), MyIni.Web.ID) <> -1 Then
				Me.pnlRightAutor.Visible = True
				Me.pnlRightAutor.DataBind()
			End If
		End If
		If MyIni.Web.ID = "basnecz" Or MyIni.Web.ID = "basnickycz" Then
			Me.pnlOnlineUsers.Visible = True
			Me.lblOnlineUsers.Text = MyUser.OnlineAutoriHtmlList
		Else
			Me.menuMojiOblibeniAutori.Visible = False
			Me.menuVzkazy.Visible = False
		End If
		If MyIni.Web.ID = "prejcz" Then
			Me.pnlRightSvatky.Visible = True
			Me.pRightSvatkyDnes.InnerText = "Dnes je " & Now.ToString("dddd", New System.Globalization.CultureInfo("cs-CZ")) & " " & Now.ToString("d.M.")
			Dim dt As DataTable = FN.Cache.dtSvatky
			Me.spanRightSvatkyVcera.InnerHtml = dt.Select("Den=" & Now.AddDays(-1).ToString("MMdd"))(0)("Svatek")
			Me.spanRightSvatkyDnes.InnerHtml = dt.Select("Den=" & Now.ToString("MMdd"))(0)("Svatek")
			Me.spanRightSvatkyZitra.InnerHtml = dt.Select("Den=" & Now.AddDays(1).ToString("MMdd"))(0)("Svatek")
			Me.spanRightSvatkyPozitri.InnerHtml = dt.Select("Den=" & Now.AddDays(2).ToString("MMdd"))(0)("Svatek")
		End If
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