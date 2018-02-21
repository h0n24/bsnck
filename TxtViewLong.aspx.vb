Class _TxtViewLong
	Inherits System.Web.UI.Page

	Public HodnoceniAction As String
	Public iID As Integer
	Public Kat As New Fog.Kategorie

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim MyUser As New Fog.User
		iID = Val.ToInt(Request.QueryString("id"))
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim SQL As String
		If Sekce.Tabulka.isTxtDila Then
			SQL = "SELECT Datum,Kat,Autor,UserNick,Precteno,Odeslano,Titulek,Anotace,Txt, (SELECT COUNT(*) FROM " & Sekce.Tabulka.Nazev & "Komentare WHERE KomentDBID=ID) AS Koment, (SELECT SUM(TipHodnota) FROM " & Sekce.Tabulka.Nazev & "Tipy WHERE TipDBID=ID) AS TipySuma" & _
		 " FROM " & Sekce.Tabulka.Nazev & " LEFT JOIN Users ON Autor=Users.UserID" & _
		  " WHERE ID=" & iID
		Else
			SQL = "SELECT ID,Datum,Kat,Poslal,Odeslano,Titulek,Hodnoceni,HodnPocet,Txt,(SELECT COUNT(*) FROM " & Sekce.Tabulka.Nazev & "Komentare WHERE KomentDBID=ID) AS Koment FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			Kat.ID = DR("Kat")
			txtTitulek.InnerText = DR("Titulek")
			Page.Title = DR("Titulek")
			Dim Precteno, Autor As Integer
			Dim sPoslal As String
			If Sekce.Tabulka.isTxtDila Then
				pnlAnotace.Visible = True
				Autor = DR("Autor")
				hlAutor.Text = Server.HtmlEncode(DR("UserNick"))
				hlAutor.NavigateUrl = String.Format(hlAutor.NavigateUrl, Autor)
				hlSekce.Text = Server.HtmlEncode(Sekce.Nazev & " » " & Kat.Nazev)
				hlSekce.NavigateUrl = String.Format(hlSekce.NavigateUrl, Sekce.Alias, Kat.ID)
				txtAnotace.InnerText = DR("Anotace")
				Precteno = DR("Precteno")
				litTipy.Text = Val.ToInt(DR("TipySuma"))
				hlTip.NavigateUrl = String.Format(hlTip.NavigateUrl, Sekce.Alias, iID)
				hlTip2.NavigateUrl = String.Format(hlTip2.NavigateUrl, Sekce.Alias, iID)
				sPoslal = "Publikoval(a): <a href=""/Autori/" & Autor & "-info.aspx"">" & Server.HtmlEncode(DR("UserNick")) & "</a>"
			Else
				txtHodnoceni.InnerText &= DR("Hodnoceni") & "% (" & DR("HodnPocet") & " hlasù)"
				sPoslal = "Poslal(a): " & Server.HtmlEncode(DR("Poslal"))
			End If
			Dim sTxt As String = Server.HtmlEncode(SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias))
			txtText.InnerHtml = Replace(sTxt, vbCrLf, "<br/>" & vbCrLf)
			txtPoslal.InnerHtml = sPoslal & ", " & CType(DR("Datum"), Date).ToString("d.M.yyyy")
			aKomentare.HRef = String.Format(aKomentare.HRef, Sekce.Alias, iID)
			aDoporucit.HRef = String.Format(aDoporucit.HRef, Sekce.Alias, iID)
			aPrint.HRef = String.Format(aPrint.HRef, Sekce.Alias, iID)
			aAdmin.HRef = String.Format(aAdmin.HRef, Sekce.Alias, iID)
			lblKomentare.Text = "(" & DR("Koment") & ")"
			lblOdeslano.Text = "(" & DR("Odeslano") & "x)"
			pnlAdmin.Visible = MyUser.isAdminSekce(Sekce.Alias)

			DR.Close()
			HodnoceniAction = "/Hodnoceni.aspx?akce=hodnotit&amp;sekce=" & Sekce.Alias & "&amp;id=" & iID
			DataBind()
			If Sekce.Tabulka.isTxtDila Then
				CMD.CommandText = "SELECT TOP 10 UserNick FROM TxtDilaTipy LEFT JOIN Users ON TipUser=UserID WHERE TipDBID=" & iID & " ORDER BY TipID DESC"
				DR = CMD.ExecuteReader
				If DR.HasRows Then
					While DR.Read
						lblTipujici.Text &= Server.HtmlEncode(DR("UserNick")) & ", "
					End While
					lblTipujici.Text = lblTipujici.Text.Substring(0, lblTipujici.Text.Length - 2)
				Else
					pnlTipujici.Visible = False
				End If
				DR.Close()
				If Not FN.Users.Prava.StatistikyOff Then
					If Not Request.UrlReferrer Is Nothing Then					  'Kontrola robotù + zadání adresy pøímo do prohlížeèe
						Dim sAppItem As String = Sekce.Alias & iID & Request.UserHostAddress & "#"
						Dim sAppCache As String = "" & Application("CachePrecteno")
						Dim sCookieItem As String = Sekce.Alias & iID & "#"
						Dim sCookieCache As String = FN.Cookies.Read("Precteno")
						If sAppCache.IndexOf(sAppItem) = -1 Then						 'Kontrola v Application (vèetnì IP) [asi 200 záznamù]
							If Len(sAppCache) > 3000 Then sAppCache = Right(sAppCache, sAppCache.Length - sAppCache.IndexOf("#") - 1)
							If sCookieCache.IndexOf(sCookieItem) = -1 Then							  'Kontrola v Cookies [asi 20 záznamù]
								If MyUser.ID <> Autor And MyUser.isAdmin = False Then								 'Kontrola Autora + Admina
									If Len(sCookieCache) > 200 Then sCookieCache = Right(sCookieCache, sCookieCache.Length - sCookieCache.IndexOf("#") - 1)
									FN.Cookies.Write("Precteno", sCookieCache & sCookieItem)
									Application("CachePrecteno") = sAppCache & sAppItem
									Precteno = Precteno + 1
									CMD.CommandText = "UPDATE " & Sekce.Tabulka.Nazev & " SET Precteno=Precteno+1 WHERE ID=" & iID
									CMD.ExecuteNonQuery()
								End If
							End If
						End If
					End If
				End If
				phHodnoceniTxtLong.Visible = False
				lblPrecteno.Text = "(" & Precteno & "x)"
			Else
				phHodnoceniTxtDila.Visible = False
			End If
		Else
			Page.Title = "Záznam neexistuje !!"
			phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "Záznam v databázi neexistuje, pravdìpodobnì byl smazán."
			phReport.Controls.Add(Report.Render)
		End If
		CMD.Connection.Close()
	End Sub

End Class