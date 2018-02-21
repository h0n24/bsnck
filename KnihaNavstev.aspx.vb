Class _KnihaNavstev
	Inherits System.Web.UI.Page

	Dim MyIni As New Fog.Ini.Init

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If MyIni.Web.ID = "litercz" Then
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Kniha hostù zrušena !!"
			Report.Text = "Jelikož docházelo k zneužívání knihy jako diskuzní fórum (chat), byli jsme nuceni knihu uzavøít. Registrovaní uživatelé mohou spolu nadále komunikovat na našem <b>Chatu</b>.<br/>"
			Report.Text &= "Pøípadné dotazy, návrhy èi pøipomínky nám posílejte emailem."
			Me.phReport.Controls.Add(Report.Render)
		Else
			ShowGuestBook()
		End If
	End Sub

	Sub ShowGuestBook()
		Dim MyUser As New Fog.User
		Dim iLimitPage As Integer = 15
		Dim sHtml, sWhere, sWhereSeznam As String
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg") = 0), 1, Val.ToInt(Request.QueryString("pg")))
		Dim f, PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo As Integer
		Dim SQL As String = "SELECT GBookID FROM GuestBook WHERE GBookWeb='" & MyIni.Web.ID & "'"
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, FN.DB.GetConnectionString)
		Dim dtID As New DataTable("MojeId")
		DA.Fill(dtID)
		DA.Dispose()
		CelkemZaznamu = dtID.Rows.Count
		If CelkemZaznamu > 0 Then
			ZobrazOd = (Stranka - 1) * iLimitPage + 1
			ZobrazDo = Math.Min(CelkemZaznamu, ZobrazOd + iLimitPage - 1)
			f = ZobrazOd
			While f <= ZobrazDo
				sWhereSeznam &= "," & dtID.Rows(CelkemZaznamu - f)(0)
				f += 1
			End While
		End If
		dtID = Nothing
		If sWhereSeznam <> "" Then
			sWhereSeznam = sWhereSeznam.Remove(0, 1)
			sWhere = " WHERE GBookID IN(" & sWhereSeznam & ")"
			sHtml &= "<p style=""margin-bottom: 3px;""><i>Nalezeno " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & ".</i></p>"
			PocetStranek = Int((CelkemZaznamu - 1) / iLimitPage) + 1
			SQL = "SELECT GBookID,GBookName,GBookDate,GBookText FROM GuestBook" & sWhere & " ORDER BY GBookID Desc"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			Dim Txt As String
			Dim iID As Integer
			f = 1
			While DR.Read()
				iID = DR("GBookID")
				If f = 4 Then				 ' *** Reklama ***
					sHtml &= Reklama.GenerateFull501
				End If
				Txt = DR("GBookText")
				Dim ReplyPozice As Integer = InStr(Txt, "<RE>")
				Dim ReplyTxt, ReplyDatum, ReplyJmeno As String
				If ReplyPozice > 0 Then
					ReplyTxt = Right(Txt, Len(Txt) - ReplyPozice + 1)
					Txt = Left(Txt, ReplyPozice - 1)
					ReplyDatum = Mid(ReplyTxt, InStr(ReplyTxt, "<D>"), InStr(ReplyTxt, "</D>") - InStr(ReplyTxt, "<D>"))
					ReplyJmeno = Mid(ReplyTxt, InStr(ReplyTxt, "<J>"), InStr(ReplyTxt, "</J>") - InStr(ReplyTxt, "<J>"))
					ReplyTxt = Mid(ReplyTxt, InStr(ReplyTxt, "</J>") + 4, InStr(ReplyTxt, "</RE>") - InStr(ReplyTxt, "</J>") - 4)
				End If
				sHtml &= "<div class=""box2"" style=""clear:both;"">"
				sHtml &= "<p>" & Server.HtmlEncode(Txt) & "</p>"
				sHtml &= "<p class=""title"">"
				If MyUser.isAdmin Then
					Dim Seznam As New Fog.Seznam(MyUser.AdminSekce)
					If Seznam.IndexOf("GuestBook") <> -1 Then
						sHtml &= "<span style=""float:right; font-size: 80%;""><a href=""/Admin/guestbook_admin.asp"">[Edit]</a></span>"
					End If
				End If
				sHtml &= CType(DR("GBookDate"), Date).ToString("d.M.yyyy") & " | " & DR("GBookName")
				sHtml &= "</p>"
				If ReplyPozice > 0 Then
					sHtml &= "<p style=""padding-left: 20px; padding-top: 2px;""><i>" & Server.HtmlEncode(ReplyTxt) & "</i></p>"
					sHtml &= "<p style=""margin-left: 20px;"" class=""title"">" & ReplyDatum & " | " & Server.HtmlEncode(ReplyJmeno) & "</p>"
				End If
				sHtml &= "</div>"
				f += 1
			End While
			DR.Close()
			Me.divGuestBookHtml.InnerHtml = sHtml
			Me.divGuestBookPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek, False)
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nenalezeny žádné záznamy !!"
			Report.Text = "Buïte první, kdo sem nìco napíše."
			Me.phReport.Controls.Add(Report.Render)
		End If
		DB.Close()
	End Sub

End Class