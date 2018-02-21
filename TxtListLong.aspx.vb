Class _TxtListLong
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim MyUser As New Fog.User
		Dim Limit As Int64 = 5000
		Dim iLimitPage As Integer = 20
		Dim sHtml, sWhereSeznam As String
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim Tabulka As New Fog.Tabulka
		If Sekce.Valid Then Tabulka = Sekce.Tabulka
		Dim Kat As New Fog.Kategorie(Val.ToInt(Request.QueryString("kat")))
		Dim Autor As UInteger = Val.ToInt(Request.QueryString("autor"))
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg")) = 0, 1, Val.ToInt(Request.QueryString("pg")))
		Dim f, PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo As Int64
		Dim sTable2 As String
		Dim SqlWhere As New FN.DB.SqlWhere
		If Request.QueryString("kat") = "all" Then
			SqlWhere.Add("Kat IN (SELECT KatID FROM Kategorie WHERE KatSekce=" & Sekce.ID & ")")
			Page.Title = Sekce.Nazev & " » Vše"
		ElseIf Kat.Valid Then
			SqlWhere.Add("Kat=" & Kat.ID)
			Page.Title = Sekce.Nazev & " » " & Kat.Nazev
		ElseIf Autor <> 0 And Sekce.Valid Then	'--Díla autora v sekci
			Page.Title = "Díla autora"
			SqlWhere.Add("Autor=" & Autor)
			SqlWhere.Add("Kat IN (SELECT KatID FROM Kategorie WHERE KatSekce=" & Sekce.ID & ")")
			Tabulka.Nazev = "TxtDila"
		ElseIf Autor <> 0 Then '--Díla autora
			Page.Title = "Díla autora"
			SqlWhere.Add("Autor=" & Autor)
			Tabulka.Nazev = "TxtDila"
		ElseIf Val.ToBoolean(Request.QueryString("oblibenci")) Then
			Page.Title = "Oblíbení autoøi"
			Tabulka.Nazev = "TxtDila"
			sTable2 = "OblibeniAutori"
			SqlWhere.Add("OblibUser=" & MyUser.ID)
			SqlWhere.Add("Autor=OblibeniAutori.OblibAutor")
		ElseIf Not Kat.Valid Then
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Kategorie neexistuje"
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If

		Dim ctrlSortBy As New Renderer.TxtSortBy(Tabulka.Nazev)
		Me.phSortBy.Controls.Add(ctrlSortBy.Render)
		Dim OrderBy As String
		Select Case Request.QueryString("sort")
			Case "sent" : OrderBy = " ORDER BY Odeslano Desc"
			Case "hodn" : OrderBy = " ORDER BY Hodnoceni Desc"
			Case "tipy" : OrderBy = " ORDER BY (SELECT SUM(TipHodnota) FROM TxtDilaTipy WHERE TipDBID=ID) Desc"
			Case Else : OrderBy = " ORDER BY ID Desc"
		End Select

		Dim SQL As String = "SELECT TOP " & Limit & " ID FROM " & Tabulka.Nazev & IIf(sTable2 = "", "", "," & sTable2) & SqlWhere.Text & OrderBy
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		Dim dtID As New DataTable("MojeId")
		DA.Fill(dtID)
		DA.Dispose()
		CelkemZaznamu = dtID.Rows.Count
		If CelkemZaznamu > 0 Then
			ZobrazOd = (Stranka - 1) * iLimitPage + 1
			ZobrazDo = Math.Min(CelkemZaznamu, ZobrazOd + iLimitPage - 1)
			f = ZobrazOd
			While f <= ZobrazDo
				sWhereSeznam &= "," & dtID.Rows(f - 1)(0)
				f += 1
			End While
		End If
		dtID = Nothing
		If sWhereSeznam <> "" Then
			sWhereSeznam = sWhereSeznam.Remove(0, 1)
			Dim sWhere As String = " WHERE ID IN(" & sWhereSeznam & ")"
			If CelkemZaznamu = Limit Then
				sHtml &= "<p style='margin-bottom:1px; font-style:italic'>Pøekroèeno maximum " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & ".</p>"
			Else
				sHtml &= "<p style='margin-bottom:1px; font-style:italic'>Nalezeno " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & ".</p>"
			End If
			PocetStranek = Int((CelkemZaznamu - 1) / iLimitPage) + 1
			Dim iID As Integer
			If Tabulka.isTxtDila Then
				SQL = "SELECT ID,Datum,Kat,Sekce,UserID,UserNick,Odeslano,Titulek,Precteno,Anotace,(SELECT SUM(TipHodnota) FROM TxtDilaTipy WHERE TipDBID=ID) AS TipySuma FROM TxtDila LEFT JOIN Users ON Autor=Users.UserID" & sWhere & OrderBy
			Else
				SQL = "SELECT ID,Datum,Kat,Poslal,Odeslano,Titulek,Hodnoceni,CAST(Txt AS varchar(150)) AS Txt FROM " & Tabulka.Nazev & sWhere & OrderBy
			End If
			Dim Kat2 As New Fog.Kategorie
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			Dim Txt, sPoslal, sPrecteno, sHodnoceni As String
			f = 1
			While DR.Read()
				If f = 4 Then					  ' *** Reklama ***
					sHtml &= Reklama.GenerateFull501
				End If
				Kat2.ID = DR("Kat")
				iID = DR("ID")
				sHtml &= "<div class='box2' style='clear:both;'>"
				If Tabulka.isTxtDila Then
					If MyUser.ID = DR("UserID") Then
						sHtml &= "<span style='float:right; margin-right:2px;'><a href='/Add.aspx?akce=edit&amp;sekce=" & Kat2.Sekce.Alias & "&amp;id=" & iID & "' class='pamatovatklik'>[Edit]</a></span>"
					End If
				End If
				sHtml &= "<p class='nadpis'><a href='/" & Kat2.Sekce.Alias & "/" & iID & "-view.aspx' class='pamatovatklik'><u>" & Server.HtmlEncode(DR("Titulek")) & "</u></a></p>"
				If Not Kat.Valid Then					'Když není zadána kategorie ani sekce, zobraz odkaz (napø. pøehled dìl autora)
					sHtml &= "<p style='font-size: 0.9em;'><a href='/" & Kat2.Sekce.Alias & "/kat-" & Kat2.ID & ".aspx'>(" & Kat2.Sekce.Nazev & " » " & Kat2.Nazev & ")</a></p>"
				End If
				If Tabulka.hasAnotace Then
					sHtml &= "<p><b><i>Anotace:</i></b> " & Server.HtmlEncode(DR("Anotace")) & "</p>"
				Else
					Txt = Replace(DR("Txt"), vbCrLf, " ")
					If Txt.Length >= 118 Then
						Do While Right(Txt, 1) <> " " And Txt.Length > 50
							Txt = Left(Txt, Len(Txt) - 1)
						Loop
						Txt &= "..."
					End If
					sHtml &= "<p style='clear: both;'><b>Úryvek:</b> " & Server.HtmlEncode(Txt) & "</p>"
				End If
				If Tabulka.isTxtDila Then
					sPoslal = "<a href='/Autori/" & DR("UserID") & "-info.aspx'>" & Server.HtmlEncode(DR("UserNick")) & "</a>"
					sPrecteno = " | Èteno " & DR("Precteno") & "x"
					sHodnoceni = "Tipy " & Val.ToInt(DR("TipySuma"))
				Else
					sPrecteno = ""
					sPoslal = Server.HtmlEncode(DR("Poslal"))
					sHodnoceni = "Hodnocení " & DR("Hodnoceni") & "%"
				End If
				sHtml &= "<p class='title'><span style='float:left;'>" & sPoslal & ", " & CType(DR("Datum"), Date).ToString("d.M.yyyy") & "</span><span style='float: right;'>" & sHodnoceni & " | Posláno " & DR("Odeslano") & "x" & sPrecteno & "</span>&nbsp;</p>"
				sHtml &= "</div>"
				f += 1
			End While
			DR.Close()
			CMD.Connection.Close()
			Me.divTxtList.InnerHtml = sHtml
			Me.divPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek)
		Else
			Page.Title = "Nenalezeny žádné záznamy"
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If

	End Sub

End Class