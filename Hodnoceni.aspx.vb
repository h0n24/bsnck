Class _Hodnoceni
	Inherits System.Web.UI.Page

	Public Hodnoceni, HodnPocet As Integer
	Public HodnoceniAction As String
	Public Referer As String
	Dim iID As Integer
	Dim Akce As String
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Akce = Request.QueryString("akce")
		iID = Request.QueryString("id")
		Select Case Akce
			Case "hodnotit"
				Hodnotit()
			Case ""
				FormInit()
			Case "ok"
				Me.phMain.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Záznam byl ohodnocen."
				Report.Text = "Nyní se mùžete vrátit zpìt a pokraèovat v èinnosti."
				Me.phReport.Controls.Add(Report.Render)
		End Select

	End Sub

	Sub Hodnotit()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim iHodnoceni = Val.ToInt(Request.Form("h"))
		If iHodnoceni >= 0 And iHodnoceni <= 5 Then
			If Not Request.UrlReferrer Is Nothing Then			 'Kontrola robotù + zadání adresy pøímo do prohlížeèe
				Dim ItemApp As String = Sekce.Alias & iID & Request.UserHostAddress
				Dim ItemCookie As String = Sekce.Alias & iID
				Dim SeznamApp As New Fog.Seznam("" & Application("CacheHodnoceni"))
				Dim text As String = FN.Cookies.Read("Hodnoceni")
				Dim SeznamCookie As New Fog.Seznam(FN.Cookies.Read("Hodnoceni"))
				If SeznamApp.IndexOf(ItemApp) = -1 And SeznamCookie.IndexOf(ItemCookie) = -1 Then
					Dim CMD As New System.Data.SqlClient.SqlCommand("", FN.DB.Open)
					Dim DatabaseRecordDuplicity As Boolean
					'If Sekce.Tabulka.isTxtDila Then
					'	CMD.CommandText = "SELECT HodID FROM Hodnoceni WHERE HodDBID=" & iID & " AND HodUser = " & MyUser.ID
					'	Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader(CommandBehavior.SingleRow)
					'	DatabaseRecordDuplicity = DR.Read
					'	DR.Close()
					'End If
					If DatabaseRecordDuplicity = False Then
						Dim SQL As String = "UPDATE " & Sekce.Tabulka.Nazev & " SET HodnSuma=HodnSuma+" & iHodnoceni & ", HodnPocet=HodnPocet+1 WHERE ID=" & iID
						'If Sekce.Tabulka.isTxtDila And MyUser.Autor Then SQL &= " AND Autor<>" & MyUser.ID
						CMD.CommandText = SQL
						If CMD.ExecuteNonQuery() = 1 Then
							SeznamCookie.Add(ItemCookie)
							SeznamApp.Add(ItemApp)
							If SeznamCookie.Count > 50 Then SeznamCookie.Remove(0)
							If SeznamApp.Count > 200 Then SeznamApp.Remove(0)
							FN.Cookies.Write("Hodnoceni", SeznamCookie.Text)
							Application("CacheHodnoceni") = SeznamApp.Text
							If Sekce.Tabulka.isTxtDila Then
								CMD.CommandText = "INSERT INTO Hodnoceni (HodDBID,HodUser,HodHodnota) VALUES (" & iID & "," & MyUser.ID & "," & iHodnoceni & ")"
								CMD.ExecuteNonQuery()
							End If
						End If
					End If
					CMD.Connection.Close()
				End If
			End If
		End If
		If Not Request.Form("Referer") Is Nothing Then
			Referer = Request.Form("Referer")
		Else
			Referer = FN.URL.Referer
		End If
		FN.Redir(Referer, "/Hodnoceni.aspx?akce=ok&sekce=" & Sekce.Alias & "&id=" & iID)
	End Sub

	Sub FormInit()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		HodnoceniAction = "/Hodnoceni.aspx?akce=hodnotit&sekce=" & Sekce.Alias & "&id=" & iID
		Dim SQL As String
		If Sekce.Alias = "Pohlednice" Then
			SQL = "SELECT HodnPocet,Hodnoceni,Soubor FROM Pohlednice WHERE ID=" & iID
		Else
			SQL = "SELECT HodnPocet,Hodnoceni,Txt FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			If Sekce.Alias = "Pohlednice" Then
				Me.divHodnoceniHtml.InnerHtml = "<img border=""0"" alt=""Pohlednice"" src=""/data/pohledy/" & DR("Soubor") & "p.gif"">"
			Else
				Me.divHodnoceniHtml.InnerHtml = DR("Txt")
			End If
			Hodnoceni = DR("Hodnoceni")
			HodnPocet = DR("HodnPocet")
			Referer = FN.URL.Referer
			Me.phMain.DataBind()
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "Záznam v databázi neexistuje, pravdìpodobnì byl smazán."
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class