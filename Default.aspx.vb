Class _Default
	Inherits System.Web.UI.Page

	Public PageTitle As String
	Public CitatAutor, CitatTxt As String

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyIni As New Fog.Ini.Init
		Dim URL As New Fog.URL(Request.Url)
		Dim sRedir As String
		Select Case URL.Domain
			Case "prej.cz" : ShowPannel(Me.pnlPrej)
			Case "citaty-osobnosti.cz" : Server.Transfer("~/App_Shared/Default_Citaty.aspx")
				'Case "cituj.cz", "citaty-osobnosti.cz" : ShowPannel(Me.pnlCitaty) : GenerujCitat()
			Case "basne.cz", "basnicky.cz" : ShowPannel(Me.pnlBasne)
				'Case "basnicky.cz" : Server.Transfer("/TxtKat.aspx?sekce=Basnicky")
			Case "abux.net"
				Server.Transfer("~/App_Shared/Default_abux.net.aspx")
				'Case Else			 'abux.net
				'	Server.Transfer("~/App_Shared/Default_abuxnet.aspx")
			Case "dosol.cz" : Server.Transfer("~/App_Shared/Default_dosol.cz.aspx")
		End Select
		If sRedir <> "" Then
			Dim Referer As String = "" & Request.ServerVariables("HTTP_REFERER")
			If Referer.IndexOf("/Admin/MyWebs.aspx") = -1 Then
			Else
				Response.Write("<p>Redir je zakázán ze stránky '/Admin/MyWebs.aspx' !!</p><p>" & sRedir & "</p>")
				Response.End()
			End If
		End If

		Page.Title = MyIni.Web.Slogan

		If FN.Cookies.Read("Web", "IsLocalSettings") = "" And MyIni.RunLocal Then
			Server.Transfer("/set.aspx")
		End If
	End Sub

	Sub ShowPannel(ByRef PannelID As System.Web.UI.WebControls.Panel)
		PannelID.Visible = True
	End Sub

	Sub GenerujCitat()
		Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("DnesCitaty"), DataTable)
		If dt Is Nothing Then
			Dim SQL As String = "SELECT ID FROM TxtCitaty LEFT JOIN Kategorie ON Kat=KatID WHERE KatFunkce<>2 AND KatFunkce<>1"
			Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
			dt = New DataTable("ID")
			Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(SQL, DBConn)
			DBDA.Fill(dt)
			If dt.Rows.Count > 0 Then
				Randomize()
				Dim NahodneID As Integer = dt.Rows(Int(Rnd() * dt.Rows.Count))("ID")
				SQL = "SELECT ID,AutorJmeno,Txt FROM TxtCitaty LEFT JOIN TxtCitatyAutori ON Autor=TxtCitatyAutori.AutorID WHERE ID=" & NahodneID
				dt = New DataTable("DnesCitaty")
				DBDA = New System.Data.SqlClient.SqlDataAdapter(SQL, DBConn)
				DBDA.Fill(dt)
				Cache.Insert("DnesCitaty", dt, Nothing, DateTime.Today.AddHours(24), TimeSpan.Zero, Caching.CacheItemPriority.AboveNormal, Nothing)
			End If
			DBConn.Close()
		End If
		If dt.Rows.Count > 0 Then
			CitatAutor = dt.Rows(0)("AutorJmeno")
			CitatTxt = dt.Rows(0)("Txt")
			Me.pnlCitaty.DataBind()
		End If
	End Sub

End Class