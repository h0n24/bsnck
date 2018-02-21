Class _Default_Citaty
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim sHtml As String
		Dim sb As New System.Text.StringBuilder
		Dim Sekce As New Fog.Sekce("Citaty")
		Page.Title = Sekce.Nazev
		If Cache.Item("Kategorie." & Sekce.Alias) Is Nothing Then
			Dim SQL As String = "SELECT KatID, KatNazev, KatSkupina, count(*) AS Pocet FROM Kategorie INNER JOIN " & Sekce.Tabulka.Nazev & " ON Kat=KatID WHERE KatSekce=" & Sekce.ID & " AND KatFunkce<>2 GROUP BY KatID,KatPriorita,KatNazev,KatSkupina ORDER BY KatPriorita, KatID"
			Dim Skupina, SkupinaOld, sCache As String
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.HasRows Then
				'If Sekce.Alias <> "Pohlednice" Then
				'	sCache = "<p style='margin-bottom:3px;'>» <a href='/" & Sekce.Alias & "/kat-all.aspx'>Vše najednou</a></p>"
				'End If
				While DR.Read()
					Skupina = DR("KatSkupina").ToString
					If SkupinaOld <> Skupina Then
						SkupinaOld = Skupina
						sCache &= "<p><b>" & DR("KatSkupina") & "</b></p>"
					End If
					sCache &= "<p>&nbsp;<a href='/" & Sekce.Alias & "/kat-" & DR("KatID") & ".aspx'>Citáty " & DR("KatNazev") & "</a><span style=""color: #808080;"">&nbsp;(" & DR("Pocet") & ")</span></p>"
				End While
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