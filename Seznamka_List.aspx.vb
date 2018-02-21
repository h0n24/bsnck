Class _Seznamka_List
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyUser As New Fog.User
		Dim iLimitPage As Integer = 15
		Dim sHtml, sWhereSeznam As String
		Dim Kat As String = "" & Request.QueryString("kat")
		Dim Akce As String = "" & Request.QueryString("akce")
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg") = 0), 1, Val.ToInt(Request.QueryString("pg")))
		Dim f, PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo, Limit As Integer
		Dim SqlWhere As New FN.DB.SqlWhere
		If Kat <> "" Then		  'Vybrána Kategorie
			If Kat <> "all" Then
				SqlWhere.Add("Rubrika=" & Kat)
			End If
		End If
		If Akce = "moje" Then		  'Vybrán Uživatel
			If MyUser.isLogged Then
				SqlWhere.Add("UserID=" & MyUser.ID)
			Else
				Me.phMain.Visible = False
				Dim Report As New Renderer.Report
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Nejsi pøihlášen !!"
				Me.phReport.Controls.Add(Report.Render)
				Exit Sub
			End If
		End If

		Dim SQL As String = "SELECT" & IIf(Limit > 0, " TOP " & Limit, "") & " ID FROM Seznamka" & SqlWhere.Text & " ORDER BY ID Desc"
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
			sHtml &= "<p style=""margin-bottom: 1px;""><i>Nalezeno " & CelkemZaznamu & " záznamù. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & ".</i></p>"
			PocetStranek = Int((CelkemZaznamu - 1) / iLimitPage) + 1
			SQL = "SELECT ID,Datum,UserID,Rubrika,Region,Jmeno,OsobaVek,Txt FROM Seznamka WHERE ID IN(" & sWhereSeznam & ") ORDER BY ID Desc"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			Dim Txt As String
			Dim iID As Integer
			f = 1
			While DR.Read()
				If f = 4 Then				 ' *** Reklama ***
					sHtml &= Reklama.GenerateFull501
				End If
				iID = DR("ID")
				sHtml &= "<div class=""box2"" style=""clear:both;"">"
				If MyUser.isAdminSekce("Seznamka") Or (DR("UserID") = MyUser.ID And MyUser.isLogged) Then
					sHtml &= "<span style=""float:right; font-size: 80%;""><a href=""/Seznamka_Add.aspx?akce=Delete&amp;id=" & DR("ID") & """>[Smazat]</a></span>"
				End If
				sHtml &= "<p>" & Replace(Server.HtmlEncode(DR("Txt")), vbCrLf, "<br/>" & vbCrLf) & "</p>"
				sHtml &= "<div class=""title""><div style=""float:left; font-size:90%;""><a href=""/Seznamka_Detaily.aspx?id=" & iID & """ class=""pamatovatklik"">" & DR("Jmeno") & ", " & DR("OsobaVek") & " let, " & GetSeznamkaXmlValue("Region", DR("Region")) & " ...více info</a></div>"
				sHtml &= "	<div style=""text-align:right; font-size:90%;"">" & CDate(DR("Datum")).Date.ToString("d.M.yyyy") & "</div>"
				sHtml &= "</div>"
				sHtml &= "</div>"
				f += 1
			End While
			DR.Close()
			Me.divSeznamkaListHtml.InnerHtml = sHtml
			Me.divSeznamkaListPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek)
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DB.Close()
	End Sub

	Public Shared Function GetSeznamkaXmlValue(ByVal Tabulka As String, ByVal Hodnota As Integer) As String
		Return FN.Cache.dsSeznamkaDataXml.Tables(Tabulka).Select("id='" & Hodnota & "'")(0)("txt")
	End Function

End Class