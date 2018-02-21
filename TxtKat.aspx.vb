Class _TxtKat
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim sHtml As String
		Dim sb As New System.Text.StringBuilder
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		If Sekce.Alias <> "Pohlednice" Then
			Page.Title = Sekce.Nazev
			If Sekce.EditorNeni Then
				sHtml &= "<p align=""right""><a href=""/Spolupracovnik.aspx?akce=Editor"">(Hledáme editory této oblasti)</a></p>"
			End If
		Else
			Page.Title = "Pohlednice"
			sHtml &= "<p align=""right""><a href=""/Spolupracovnik.aspx?akce=Pohlednice"">Hledáme nové autory pohlednic!</a></p>"
		End If
		If Sekce.Alias = "Basnicky" Then
			Sekce.Alias = "Basne"
		End If
		If Cache.Item("Kategorie." & Sekce.Alias) Is Nothing Then
			Dim SQL As String = "SELECT KatID, KatNazev, KatSkupina, count(*) AS Pocet FROM Kategorie INNER JOIN " & Sekce.Tabulka.Nazev & " ON Kat=KatID WHERE KatSekce=" & Sekce.ID & " AND KatFunkce<>2 GROUP BY KatID,KatPriorita,KatNazev,KatSkupina ORDER BY KatPriorita, KatID"
			Dim Skupina, SkupinaOld, sCache As String
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.HasRows Then
				If Sekce.Alias <> "Pohlednice" Then
					sCache = "<p style='margin-bottom:3px;'>» <a href='/" & Sekce.Alias & "/kat-all.aspx'>Vše (bez èlenìní dle sekce)</a></p>"
				End If
				While DR.Read()
					Skupina = DR("KatSkupina").ToString
					If SkupinaOld <> Skupina Then
						SkupinaOld = Skupina
						sCache &= "<p><b>" & DR("KatSkupina") & "</b></p>"
					End If
					sCache &= "<p>&nbsp;• <a href='/" & Sekce.Alias & "/kat-" & DR("KatID") & ".aspx'>" & DR("KatNazev") & "</a><span style=""color: #808080;"">&nbsp;(" & DR("Pocet") & ")</span></p>"
				End While
				DR.Close()
				CMD.CommandText = "SELECT UserNick FROM UsersAdmins LEFT JOIN Users ON AdminID=Users.UserID WHERE AdminSekce LIKE '%" & Sekce.Alias & "%'"
				DR = CMD.ExecuteReader
				If DR.HasRows Then
					Dim sEditori As String
					While DR.Read()
						sEditori &= DR("UserNick") & ", "
					End While
					sEditori = Left(sEditori, Len(sEditori) - 2)
					sCache &= "<p class='font08' style='text-align:right;'>Editoøi:<br /><i>" & sEditori & "</i></p>"
				End If
				DR.Close()
				'Cache.Insert("Kategorie." & Sekce, sCache, Nothing, Now.Today.AddDays(1), TimeSpan.Zero)
				Me.divViewKatHtml.InnerHtml = sHtml & sCache
			Else
				Me.phMain.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Nenalezeny žádné záznamy"
				Me.phReport.Controls.Add(Report.Render)
			End If
			CMD.Connection.Close()
		Else
			Me.divViewKatHtml.InnerHtml = sHtml & Cache.Item("Kategorie." & Sekce.Alias)
		End If

	End Sub

End Class