Class _TxtList_Citaty
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim MyUser As New Fog.User
		Dim iLimitPage As Integer = 20
		Dim sHtml, sWhereSeznam As String
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim Kat As New Fog.Kategorie(Request.QueryString("kat"))
		Dim Autor As String = "" & Request.QueryString("autor")
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg")) = 0, 1, Val.ToInt(Request.QueryString("pg")))
		Dim f, PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo As Int64
		Dim Limit As Int64 = 5000
		Dim SqlWhere As New FN.DB.SqlWhere
		If Not Kat.Valid And Autor = "" Then
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Kategorie neexistuje"
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		If Request.QueryString("kat") = "all" Then
			Page.Title = Sekce.Nazev & " » Vše"
		ElseIf Autor <> "" Then		'Vybrán Autor
			Page.Title = "Citáty autora"
			SqlWhere.Add("Autor=" & Autor)
		ElseIf Kat.Valid Then
			Page.Title = Sekce.Nazev & " » " & Kat.Nazev
			SqlWhere.Add("Kat=" & Kat.ID)
		ElseIf Not Kat.Valid Then
		End If
		If Sekce.Alias <> "Citaty" Then
			SqlWhere.Add("Sekce='" & Sekce.Alias & "'")
		End If

		Dim ctrlSortBy As New Renderer.TxtSortBy(Sekce.Tabulka.Nazev)
		'Me.phSortBy.Controls.Add(ctrlSortBy.Render)
		Dim OrderBy As String
		Select Case Request.QueryString("sort")
			Case "sent" : OrderBy = " ORDER BY Odeslano Desc"
			Case "hodn" : OrderBy = " ORDER BY Hodnoceni Desc"
			Case Else : OrderBy = " ORDER BY ID Desc"
		End Select

		Dim SQL As String = "SELECT TOP " & Limit & " ID FROM " & Sekce.Tabulka.Nazev & SqlWhere.Text & OrderBy
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		Dim dtID As New DataTable("MojeId")
		DA.Fill(dtID)
		DA.Dispose()
		CelkemZaznamu = dtID.Rows.Count
		If CelkemZaznamu > 0 Then
			ZobrazOd = (Stranka - 1) * iLimitPage + 1
			ZobrazDo = Math.Min(CelkemZaznamu, ZobrazOd + iLimitPage - 1)
			PocetStranek = Int((CelkemZaznamu - 1) / iLimitPage) + 1
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
				sHtml &= "<p style='margin-bottom:5px; font-style:italic'>Nalezeno " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & ".</p>"
			End If
			Dim iID As Integer
			If Sekce.Alias = "Citaty" Then
				SQL = "SELECT ID,Datum,Kat,Autor,AutorJmeno,Poslal,Odeslano,Hodnoceni,Txt FROM TxtCitaty LEFT JOIN TxtCitatyAutori ON Autor=TxtCitatyAutori.AutorID WHERE ID IN(" & sWhereSeznam & ")" & OrderBy
			Else
				SQL = "SELECT ID,Datum,Kat,Poslal,Odeslano,Hodnoceni,Txt FROM " & Sekce.Tabulka.Nazev & " WHERE ID IN(" & sWhereSeznam & ")" & OrderBy
			End If
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			Dim Txt, sPoslal, sPrecteno As String
			f = 1
			While DR.Read()
				If f = 4 Then					  ' *** Reklama ***
					sHtml &= Reklama.GenerateFull501
				End If
				iID = DR("ID")
				Txt = Server.HtmlEncode(DR("Txt"))
				If Sekce.Alias = "Citaty" Then
					Txt = "(<a href=""/Citaty/autor-" & DR("Autor") & ".aspx"">" & Server.HtmlEncode(DR("AutorJmeno")) & "</a>)" & vbCrLf & Txt
				End If
				sHtml &= "<div style=""clear:both; margin-bottom:1em;"">"
				If MyUser.isAdminSekce(Sekce.Alias) Then
					sHtml &= "<span style='float:right; margin-right:2px;'><a href=""/Admin/Editace.aspx?sekce=" & Sekce.Alias & "&amp;id=" & iID & "&nav=False"" class=""pamatovatklik"">[admin]</a></span>"
				End If
				sHtml &= "<p>" & Replace(Txt, vbCrLf, "<br/>" & vbCrLf) & "</p>"
				sHtml &= "</div>"
				f += 1
			End While
			DR.Close()
			CMD.Connection.Close()
			Me.divTxtList.InnerHtml = sHtml
			Me.divPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek)
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If

	End Sub

End Class