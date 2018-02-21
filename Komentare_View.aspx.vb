Class _Komentare_View
	Inherits System.Web.UI.Page

	Public SB As New System.Text.StringBuilder

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim MyUser As New Fog.User
		Dim Limit As ULong = 5000
		Dim LimitPage As UShort = 15
		Dim SQL, SQL2 As String
		Dim IDx As ULong = Request.QueryString("id")
		Dim ViceDBID As Boolean
		Dim Filtr As String = Request.QueryString("filtr")
		Select Case Filtr
			Case "odeslane"
				Page.Title &= " odeslané"
				ViceDBID = True
				SQL = "SELECT TOP " & Limit / 2 & " KomentID FROM ( SELECT KomentID,KomentUser,KomentDatum FROM TxtDilaKomentare UNION SELECT KomentID,KomentUser,KomentDatum FROM SbirkyKomentare ) AS K WHERE KomentUser=" & IDx & " ORDER BY KomentDatum DESC"
				SQL2 = "SELECT K.*,Titulek,UserNick FROM ( SELECT KomentID, KomentDatum, KomentDBID, KomentUser, KomentTxt,Kat,Titulek FROM TxtDilaKomentare INNER JOIN TxtDila ON KomentDBID=ID " & _
				" UNION  SELECT KomentID, KomentDatum, KomentDBID, KomentUser, KomentTxt,Kat,Titulek FROM SbirkyKomentare INNER JOIN Sbirky ON KomentDBID=ID ) AS K" & _
				" LEFT JOIN Users ON KomentUser=Users.UserID {WHERE} ORDER BY KomentDatum Desc"
				pnlNew.Visible = False
			Case "prijate"
				Page.Title &= " pøijaté"
				ViceDBID = True
				SQL = "SELECT TOP " & Limit / 2 & " KomentID FROM ( SELECT KomentID,KomentUser,KomentDatum,Autor FROM TxtDilaKomentare INNER JOIN TxtDila ON KomentDBID=ID UNION SELECT KomentID,KomentUser,KomentDatum,Autor FROM SbirkyKomentare INNER JOIN Sbirky ON KomentDBID=ID ) AS K WHERE Autor=" & IDx & " ORDER BY KomentDatum DESC"
				SQL2 = "SELECT K.*,Titulek,UserNick FROM ( SELECT KomentID, KomentDatum, KomentDBID, KomentUser, KomentTxt,Kat,Titulek FROM TxtDilaKomentare INNER JOIN TxtDila ON KomentDBID=ID " & _
				" UNION  SELECT KomentID, KomentDatum, KomentDBID, KomentUser, KomentTxt,Kat,Titulek FROM SbirkyKomentare INNER JOIN Sbirky ON KomentDBID=ID ) AS K" & _
				" LEFT JOIN Users ON KomentUser=Users.UserID {WHERE} ORDER BY KomentDatum Desc"
				pnlNew.Visible = False
			Case Else
				Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
				SQL = "SELECT TOP " & Limit & " KomentID FROM " & Sekce.Tabulka.Nazev & "Komentare WHERE KomentDBID=" & IDx & " ORDER BY KomentID DESC"
				SQL2 = "SELECT KomentID,KomentDatum,KomentDBID,KomentUser,KomentTxt,UserNick,Kat,Titulek FROM " & Sekce.Tabulka.Nazev & "Komentare " & _
				" INNER JOIN " & Sekce.Tabulka.Nazev & " AS t ON KomentDBID=t.ID" & _
				" LEFT JOIN Users ON KomentUser=Users.UserID {WHERE} ORDER BY KomentID Desc"
				hlNew.NavigateUrl = String.Format(hlNew.NavigateUrl, Sekce.Alias, IDx)
		End Select
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg") = 0), 1, Val.ToInt(Request.QueryString("pg")))
		Dim PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo As Integer
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open
		Dim dtID As New DataTable("MojeId")
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		DA.Fill(dtID)
		CelkemZaznamu = dtID.Rows.Count
		Dim Seznam As New Fog.Seznam
		If CelkemZaznamu > 0 Then
			ZobrazOd = (Stranka - 1) * LimitPage + 1
			ZobrazDo = Math.Min(CelkemZaznamu, ZobrazOd + LimitPage - 1)
			PocetStranek = Int((CelkemZaznamu - 1) / LimitPage) + 1
			Dim f As ULong = ZobrazOd
			While f <= ZobrazDo
				Seznam.Add(dtID.Rows(f - 1)(0))
				f += 1
			End While
		End If
		dtID = Nothing
		If Seznam.Count > 0 Then
			litZaznamu.Text = "Nalezeno " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & "."
			Dim sWhere As String = " WHERE KomentID IN (" & Seznam.Text & ")"
			Dim Kat As New Fog.Kategorie
			Dim Poslal, Txt As String
			Dim KoID As ULong
			Dim f As UShort
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL2.Replace("{WHERE}", sWhere), DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			While DR.Read()
				KoID = DR("KomentID")
				Kat.ID = DR("Kat")
				If ViceDBID = False And f = 0 Then Page.Title &= " » " & DR("Titulek")
				If f = 3 Then				 ' *** Reklama ***
					SB.Append(Reklama.GenerateFull501)
				End If
				Txt = Server.HtmlEncode(DR("KomentTxt"))
				SB.Append("<div class='box2' style='clear:both;'>")
				If ViceDBID Then
					If IsDBNull(DR("Titulek")) Then
						SB.Append("<span style='float:right;'>dílo již neexistuje!</span>")
					Else
						SB.Append("<div><a href='/" & Kat.Sekce.Alias & "/" & DR("KomentDBID") & "-view.aspx'>" & Server.HtmlEncode(Kat.Sekce.J1P & " - " & DR("Titulek")) & "</a></div>")
					End If
				End If
				SB.Append("<p>" & Replace(Txt, vbCrLf, "<br/>" & vbCrLf) & "</p>")
				SB.Append("<p class='title'>")
				If MyUser.isAdminSekce(Kat.Sekce.Alias) Then
					SB.Append("<a style='float:right; margin-left:8px;' href='/Admin/Komentare.aspx?sekce=" & Kat.Sekce.Alias & "&amp;id=" & KoID & "' class='pamatovatklik'>[Admin]</a></span>")
				End If
				If DR("KomentUser") = 0 Then
					Poslal = "úživatel neexistuje"
				Else
					Poslal = "<a href='/Autori/" & DR("KomentUser") & "-info.aspx'>" & Server.HtmlEncode(DR("UserNick")) & "</a>"
				End If
				SB.Append(CType(DR("KomentDatum"), Date).ToString("d.M.yyyy H:mm") & " | " & Poslal & "</p>")
				SB.Append("</div>")
				f += 1
			End While
			DR.Close()
			Me.divKomentareViewPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek)
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Nenalezen žádný komentáø"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DB.Close()
	End Sub

End Class