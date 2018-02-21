<%@ Application Language="VB" %>

<script RunAt="server">
	Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
		Application("StartTime") = Now
		WriteApplicationLog(Now.ToString() & ";Start")
	End Sub

	Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
		WriteApplicationLog(Now.ToString() & ";End")
	End Sub

	Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
		'MaintenancePause()
		
		KontrolaSekciProDomenu(Request.QueryString("sekce"))
		Application.Lock()
		Application("RequestsCount") = Application("RequestsCount") + 1
		Application("RequestsActive") = Application("RequestsActive") + 1
		If Application("RequestsActive") > Application("RequestsActiveMax") Then Application("RequestsActiveMax") = Application("RequestsActive")
		Application.UnLock()

		Dim dTable As DataTable = FN.Cache.dtBlokovatUzivatele
		Dim UserID As Int64 = Val.ToInt(FN.Cookies.Read("User", "ID").ToString)
		If UserID <> 0 Then
			Dim dRows As DataRow() = dTable.Select("Uzivatel=" & UserID)
			If dRows.Length > 0 Then
				Response.Write("Váš úèet byl zablokován !!<br/><br/>")
				Response.Write("Kontaktujte správce serveru pro vyøešení situace.<br/>")
				Response.Write("Email: " & Fog.Ini.EmailAdmin & "<br/><br/>")
				Dim SQL As String = "SELECT ZpravaUzivateli FROM BlokovatUzivatele WHERE Uzivatel=" & UserID
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
				If DR.Read Then
					Response.Write("Zpráva pro Vás:" & "<br/>" & Server.HtmlEncode(DR("ZpravaUzivateli")) & "<br/><br/>")
				End If
				DR.Close()
				CMD.Connection.Close()
				Response.End()
			End If
		End If
		dTable = FN.Cache.dtBlokovatIP
		Dim UserIP As String = Request.UserHostAddress
		If dTable.Select("IP='" & UserIP & "'").Length > 0 Then
			Response.Write("Vaše IP adresa byla zablokována !!<br/><br/>")
			Response.Write("Kontaktujte správce serveru pro vyøešení situace.<br/>")
			Response.Write("Email: " & Fog.Ini.EmailAdmin & "<br/><br/>")
			Dim SQL As String = "SELECT ZpravaUzivateli FROM BlokovatIP WHERE IP=" & FN.DB.GetText(UserIP)
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read Then
				Response.Write("Zpráva pro Vás:" & "<br/>" & Server.HtmlEncode(DR("ZpravaUzivateli")) & "<br/><br/>")
			End If
			DR.Close()
			CMD.Connection.Close()
			Response.End()
		End If

		Dim ServerName As String = HttpContext.Current.Request.Url.Host
		If ServerName.IndexOf("localhost") <> -1 Or ServerName.IndexOf("127.0.0.1") <> -1 Or ServerName.IndexOf("10.20.") <> -1 Or ServerName.IndexOf("192.168.") <> -1 Then
			HttpContext.Current.Response.Write("Cíl pro localhost není definován!")
			'HttpContext.Current.Response.End()
		ElseIf ServerName.IndexOf("www.") <> -1 Then
			Dim sRedir As String = "http://" & ServerName.Replace("www.", "") & Request.Url.PathAndQuery
			sRedir = sRedir.Replace("/Default.aspx", "/")
			HttpContext.Current.Response.Redirect(sRedir)
		End If
		
		Dim sPath As String = Request.Path
		Dim f As Integer
		Dim URL As String
		Dim arrPath() As String = sPath.Split("/")
		Dim sScript As String = arrPath(arrPath.Length - 1)		'Název scriptu (malými písmeny)
		sScript = sScript.ToLower.Replace(".aspx", "")
		Dim arrScript() = sScript.Split("-")
		Dim Sekce As New Fog.Sekce(arrPath(1))
		If Sekce.Valid Then
			KontrolaSekciProDomenu(Sekce.Alias)
			If sPath.ToLower.StartsWith("/pohlednice/") Then
				If sScript = "kat" Then
					URL = "/TxtKat.aspx?sekce=" & Sekce.Alias
				ElseIf sScript.StartsWith("kat-") Then
					URL = "/Pohlednice_Preview.aspx?kat=" & arrScript(1)
				ElseIf sScript = "read" Then
					URL = "/Pohlednice_Read.aspx"
				ElseIf sScript.StartsWith("read-") Then
					URL = "/Pohlednice_Read.aspx?uid=" & arrScript(1)
				ElseIf sScript.StartsWith("autor-") Then
					URL = "/Pohlednice_Preview.aspx?autor=" & arrScript(1)
				ElseIf sScript.StartsWith("autorinfo-") Then
					URL = "/Pohlednice_Autor.aspx?id=" & arrScript(1)
				ElseIf sScript.EndsWith("-hodn") Then
					URL = "/Hodnoceni.aspx?sekce=" & Sekce.Alias & "&id=" & arrScript(0)
				End If
			ElseIf sScript = "kat" Then
				URL = "/TxtKat.aspx?sekce=" & Sekce.Alias
			ElseIf sScript = "autori" Then
				URL = "/Citaty_Autori.aspx"
			ElseIf sScript.StartsWith("autorinfo-") Then
				URL = "/Citaty_Autori.aspx?akce=info&id=" & arrScript(1)
			ElseIf sScript.StartsWith("kat-") Then
				If Sekce.Tabulka.isTxtCitaty Then
					URL = "/TxtList_Citaty.aspx?sekce=" & Sekce.Alias & "&kat=" & arrScript(1)
				ElseIf Sekce.Tabulka.isTxtShort Then
					URL = "/TxtListShort.aspx?sekce=" & Sekce.Alias & "&kat=" & arrScript(1)
				Else
					URL = "/TxtListLong.aspx?sekce=" & Sekce.Alias & "&kat=" & arrScript(1)
				End If
			ElseIf sScript.StartsWith("autor-") Then
				If Sekce.Alias = "Citaty" Then
					URL = "/TxtList_Citaty.aspx?sekce=Citaty&autor=" & arrScript(1)
				Else
					URL = "/TxtListLong.aspx?sekce=" & Sekce.Alias & "&autor=" & arrScript(1)
				End If
			ElseIf sScript.EndsWith("-view") Then
				If Sekce.Alias = "Sbirky" Then
					URL = "/Sbirky_Show.aspx?sbirka=" & arrScript(0)
				Else
					URL = "/TxtViewLong.aspx?sekce=" & Sekce.Alias & "&id=" & arrScript(0)
				End If
			ElseIf sScript.EndsWith("-print") Then
				URL = "/TxtPrintLong.aspx?sekce=" & Sekce.Alias & "&id=" & arrScript(0)
			ElseIf sScript.EndsWith("-koment") Then
				URL = "/Komentare_View.aspx?sekce=" & Sekce.Alias & "&id=" & arrScript(0)
			ElseIf sScript.EndsWith("-hodn") Then
				URL = "/Hodnoceni.aspx?sekce=" & Sekce.Alias & "&id=" & arrScript(0)
			End If
		ElseIf sPath.ToLower.StartsWith("/users/") Then
			If sScript = "kontakty" Then
				URL = "/Users_Kontakty.aspx"
			End If
		ElseIf sPath.ToLower.StartsWith("/autori/") Then
			If sScript = "seznam" Then
				URL = "/Autori_Seznam.aspx?akce=seznam"
			ElseIf sScript = "oblibenci-dila" Then
				URL = "/TxtListLong.aspx?oblibenci=true"
			ElseIf sScript.StartsWith("seznam-") Then
				URL = "/Autori_Seznam.aspx?akce=seznam&filtr=" & arrScript(1)
			ElseIf sScript.EndsWith("-info") Then
				URL = "/Autori_Info.aspx?autor=" & arrScript(0)
			ElseIf sScript.EndsWith("-dila") Then
				URL = "/TxtListLong.aspx?autor=" & arrScript(0)
			ElseIf sScript.EndsWith("-sbirky") Then
				URL = "/Sbirky_List.aspx?autor=" & arrScript(0)
			End If
		ElseIf sPath.ToLower.StartsWith("/seznamka/") Then
			If sScript = "kat" Then
				URL = "/Seznamka_Kat.aspx"
			ElseIf sScript.StartsWith("kat-") Then
				URL = "/Seznamka_List.aspx?kat=" & arrScript(1)
			ElseIf sScript = "odpovedi" Then
				URL = "/Seznamka_Odpovedi.aspx"
			ElseIf sScript = "moje" Then
				URL = "/Seznamka_List.aspx?akce=moje"
			End If
		ElseIf sPath.ToLower.StartsWith("/inv-") Then
			FN.Cookies.Write("InvitedBy", sPath.Replace("/inv-", "").Replace(".aspx", ""), Now.AddDays(2))
			Response.Redirect("/")
		ElseIf sPath.ToLower.StartsWith("/r/") Then
			If sScript.StartsWith("b") Then
				URL = "/Klik.aspx?banner=" & sScript.Substring(1, sScript.Length - 1)
			End If
		End If

		If URL <> "" Then
			Dim sQuery As String = Request.Url.Query
			If UBound(arrPath) > 2 Then
				Dim arrQuery()
				For f = 2 To UBound(arrPath) - 1
					arrQuery = arrPath(f).Split("-")
					If UBound(arrQuery) = 1 Then
						sQuery &= "&" & arrQuery(0) & "=" & arrQuery(1)
					End If
				Next
			End If
			If sQuery <> "" Then
				sQuery = sQuery.Remove(0, 1)
				If URL.IndexOf("?") = -1 Then
					URL &= "?" & sQuery
				Else
					URL &= "&" & sQuery
				End If
			End If
			System.Web.HttpContext.Current.RewritePath(URL)
		End If
	End Sub

	Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
		Dim Chyba As SharedFunctions.PageError
		Chyba.Url = Request.Url.ToString
		Chyba.Referer = Request.ServerVariables("HTTP_REFERER")
		Chyba.Exception = Server.GetLastError()
		Chyba.Form = Request.Form
		Application("ErrorLastData") = Chyba
	End Sub

	Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
		Application.Lock()
		Application("RequestsActive") = Application("RequestsActive") - 1
		Application.UnLock()
	End Sub

	Sub WriteApplicationLog(ByVal Text As String)
		Try
			Dim SW As IO.StreamWriter = IO.File.AppendText(Fog.Ini.PhysicalPaths.Data & "\Log\Application.log")
			SW.WriteLine(Text)
			SW.Flush()
			SW.Close()			
		Catch ex As Exception
		End Try
	End Sub
	
	Sub KontrolaSekciProDomenu(ByVal Sekce As String)
		If Request.Path.ToLower.StartsWith("/admin") Then
		ElseIf Sekce <> "" Then
			Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceAlias='" & Sekce & "'")
			If dRows.Length = 1 Then
				Dim Url As New Fog.URL(Request.Url)
				Dim Domains() As String = dRows(0)("SekceDomains").ToString.Split(",")
				Dim Found As Boolean = False
				For Each Domain As String In Domains
					If Domain = "*" Then
						Found = True
					ElseIf Domain = Url.Domain Then
						Found = True
					ElseIf Domain.StartsWith("*.") Then
						If Url.Domain.EndsWith(Domain.Replace("*.", "")) Then
							Found = True
						End If
					End If
				Next
				If Not Found Then
					Response.Redirect("http://" & dRows(0)("SekceDefaultDomain"))
				End If
			End If
		End If
	End Sub
	
	Public Overrides Function GetVaryByCustomString(ByVal context As HttpContext, ByVal arg As String) As String
		'-- Využité pro cache stránek (npø. css.aspx)
		If arg = "AbsolutePath" Then
			Dim Path As String = context.Request.Url.Host.ToLower()
			Return Path
		Else
			Return String.Empty
		End If
	End Function

	Sub MaintenancePause()
		Dim MaintainAllowIPs As New ArrayList
		MaintainAllowIPs.Add("88.103.97.84")
		Dim TimeStart As New DateTime(2010, 3, 25, 7, 54, 0)
		If Request.Url.ToString.ToLower.IndexOf("/admin") <> -1 Then Exit Sub
		If MaintainAllowIPs.IndexOf(Request.UserHostAddress) <> -1 Then Exit Sub
		If DateTime.Compare(Now, TimeStart) > 0 Then
			System.Web.HttpContext.Current.RewritePath("~/Maintenance.aspx")
		End If
	End Sub
		
</script>

