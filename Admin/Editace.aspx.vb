Class Admin_Editace
	Inherits System.Web.UI.Page

	Public iID As Integer
	Dim SQL As String
	Dim Sekce As Fog.Sekce
	Dim KatFilter As Integer
	Dim Autor As Integer
	Dim MyUser As New Fog.User
	Dim Akce As String
	Dim ZaznamCislo, ZaznamuCelkem As Integer
	Dim f As Integer

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Akce = Request.QueryString("akce")
		Sekce = New Fog.Sekce(Request.QueryString("sekce"))
		Page.Title = Sekce.Nazev
		iID = Val.ToInt(Request.QueryString("id"))
		KatFilter = Val.ToInt(Request.QueryString("filter"))
		If Not (Page.IsPostBack) Then
			FormInit()
		End If
	End Sub

	Sub FormInit()
		ShowReport()
		inpReferer.Value = FN.URL.Referer
		Dim SqlWhere As New FN.DB.SqlWhere
		If KatFilter = 0 Then
			SqlWhere.Add("Kat IN (SELECT KatID FROM Kategorie WHERE KatSekce=" & Sekce.ID & ")")
		Else
			SqlWhere.Add("Kat=" & KatFilter)
		End If
		SQL = "SELECT Count(*) FROM " & Sekce.Tabulka.Nazev & SqlWhere.Text
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		DR.Read()
		ZaznamuCelkem = DR(0)
		DR.Close()
		Select Case Akce
			Case "GoFirst"
				SQL = "SELECT TOP 1 * FROM " & Sekce.Tabulka.Nazev & SqlWhere.Add("ID>0")
			Case "GoPrevious"
				SQL = "SELECT TOP 1 * FROM " & Sekce.Tabulka.Nazev & SqlWhere.Add("ID<" & iID) & " ORDER BY ID Desc"
			Case "GoNext"
				SQL = "SELECT TOP 1 * FROM " & Sekce.Tabulka.Nazev & SqlWhere.Add("ID>" & iID)
			Case "GoLast"
				SQL = "SELECT TOP 1 * FROM " & Sekce.Tabulka.Nazev & SqlWhere.Text & " ORDER BY ID Desc"
			Case Else
				SQL = "SELECT TOP 1 * FROM " & Sekce.Tabulka.Nazev & SqlWhere.Add("ID>=" & iID)
		End Select
		CMD.CommandText = SQL
		DR = CMD.ExecuteReader()
		If DR.HasRows Then
			DR.Read()
			iID = DR("ID")
			tbTxt.Text = SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias)
			Dim Kat As New Fog.Kategorie(DR("Kat"))
			lblDatum.Text = "(" & DR("Datum") & ")"
			litOdeslano.Text = DR("Odeslano")
			If Sekce.Tabulka.hasHodnoceni Then
				pnlHodnoceni.Visible = True
				lblHodnoceni.Text = DR("Hodnoceni") & "%"
			End If
			If Sekce.Tabulka.hasKomentare Then
				pnlKomentare.Visible = True
				hlKoment.NavigateUrl = String.Format(hlKoment.NavigateUrl, Sekce.Alias, iID)
				hlKomentNew.NavigateUrl = String.Format(hlKomentNew.NavigateUrl, Sekce.Alias, iID)
				'Komentare = DR("Koment")
			End If
			If Sekce.Tabulka.hasTitulek Then
				tbTitulek.Text = DR("Titulek")
			Else
				pnlTitulek.Visible = False
			End If
			If Sekce.Tabulka.hasAnotace Then
				tbAnotace.Text = DR("Anotace")
			Else
				pnlAnotace.Visible = False
			End If
			If Sekce.Tabulka.isTxtDila Then
				Autor = DR("Autor")
				SQL = "SELECT UserNick FROM Users WHERE UserID=" & Autor
				Dim CMD2 As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				Dim DR2 As System.Data.SqlClient.SqlDataReader = CMD2.ExecuteReader
				If DR2.Read Then lblPoslal.Text = "<a href='/Autori/" & Autor & "-info.aspx'>" & Server.HtmlEncode(DR2("UserNick")) & "</a>"
				DR2.Close()
				CMD2.Connection.Close()
			ElseIf Sekce.Tabulka.isTxtLong Then
				lblPoslal.Text = Server.HtmlEncode(DR("Poslal"))
			Else
				lblPoslal.Text = Server.HtmlEncode(DR("Poslal"))
				If Sekce.Tabulka.isTxtCitaty Then Autor = DR("Autor")
			End If
			DR.Close()

			Select Case Akce
				Case "", "GoFirst"
					ZaznamCislo = 1
				Case "GoLast"
					ZaznamCislo = ZaznamuCelkem
				Case "GoNext", "GoTo"
					CMD.CommandText = "SELECT Count(*) AS Pocet FROM " & Sekce.Tabulka.Nazev & SqlWhere.Text
					DR = CMD.ExecuteReader
					DR.Read()
					ZaznamCislo = ZaznamuCelkem - DR("Pocet") + 1
					DR.Close()
				Case "GoPrevious"
					CMD.CommandText = "SELECT Count(*) FROM " & Sekce.Tabulka.Nazev & SqlWhere.Text
					DR = CMD.ExecuteReader
					DR.Read()
					ZaznamCislo = DR(0)
					DR.Close()
				Case Else
			End Select

			Dim Where As New FN.DB.SqlWhere
			Where.Add("KatSekce=" & Sekce.ID)
			If Not Kat.isSmazane Then Where.Add("KatFunkce<>2")
			CMD.CommandText = "SELECT KatID,KatNazev FROM Kategorie " & Where.Text & " ORDER BY KatPriorita,KatNazev"
			DR = CMD.ExecuteReader
			lbKat.DataSource = DR
			lbKat.DataValueField = "KatID"
			lbKat.DataTextField = "KatNazev"
			lbKat.DataBind()

			lbKat.Items.FindByValue(Kat.ID).Selected = True
			DR.Close()

			If Request.QueryString("nav") = "False" Then
				pnlNavigace.Visible = False
			Else
				CMD.CommandText = "SELECT KatID,KatNazev FROM Kategorie WHERE KatSekce=" & Sekce.ID & " ORDER BY KatPriorita,KatNazev"
				DR = CMD.ExecuteReader
				lbFilter.DataSource = DR
				lbFilter.DataValueField = "KatID"
				lbFilter.DataTextField = "KatNazev"
				lbFilter.DataBind()
				lbFilter.Items.Insert(0, New ListItem("----- všechny záznamy -----", 0))
				If KatFilter <> 0 Then lbFilter.Items.FindByValue(KatFilter).Selected = True
				DR.Close()
			End If

			CMD.CommandText = "SELECT SekceID,SekceNazev FROM Sekce WHERE SekceTabulka=" & Sekce.Tabulka.ID & " ORDER BY SekceNazev"
			DR = CMD.ExecuteReader
			lbSekce.DataSource = DR
			lbSekce.DataValueField = "SekceID"
			lbSekce.DataTextField = "SekceNazev"
			lbSekce.DataBind()
			With lbSekce.Items.FindByValue(Sekce.ID)
				.Selected = True
				.Text = "»" & .Text
			End With
			DR.Close()

			CMD.CommandText = "SELECT HistoryDatum,HistoryJmeno,HistoryTxt FROM AdminEditHistory WHERE HistoryDBID=" & iID & " AND HistorySekce IN (SELECT SekceAlias FROM Sekce WHERE SekceTabulka=" & Sekce.Tabulka.ID & ") ORDER BY HistoryID Desc"
			DR = CMD.ExecuteReader
			rptAdminEditaceEditHistory.DataSource = DR
			rptAdminEditaceEditHistory.DataBind()
			DR.Close()

			If Sekce.Tabulka.isTxtCitaty Then
				CMD.CommandText = "SELECT AutorID,AutorJmeno FROM TxtCitatyAutori ORDER BY AutorJmeno"
				DR = CMD.ExecuteReader
				lbCitatyAutor.DataSource = DR
				lbCitatyAutor.DataValueField = "AutorID"
				lbCitatyAutor.DataTextField = "AutorJmeno"
				lbCitatyAutor.DataBind()
				lbCitatyAutor.Items.FindByValue(Autor).Selected = True
			Else
				lbCitatyAutor.Visible = False
			End If

			litZaznamCislo.Text = ZaznamCislo
			litZaznamuCelkem.Text = ZaznamuCelkem
			lblSekce.Text = Sekce.Nazev
			inpID.Value = iID
		Else
			DR.Close()
			pnlAdminEditace.Visible = False
			Dim err As New Renderer.FormErrors
			err.Add("Záznam nenalezen")
			phErrors.Controls.Add(err.Render)
		End If

		CMD.Connection.Close()

		If ZaznamCislo <> 1 Then
			aGoFirst.HRef = "Editace.aspx?akce=GoFirst&amp;sekce=" & Sekce.Alias & "&amp;filter=" & KatFilter
			aGoPrevious.HRef = "Editace.aspx?akce=GoPrevious&amp;sekce=" & Sekce.Alias & "&amp;ilter=" & KatFilter & "&amp;id=" & iID
		End If
		If ZaznamCislo <> ZaznamuCelkem Then
			aGoNext.HRef = "Editace.aspx?akce=GoNext&amp;sekce=" & Sekce.Alias & "&amp;filter=" & KatFilter & "&amp;id=" & iID
			aGoLast.HRef = "Editace.aspx?akce=GoLast&amp;sekce=" & Sekce.Alias & "&amp;filter=" & KatFilter
		End If
	End Sub

	Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
		Response.Redirect("Editace.aspx?sekce=" & Sekce.Alias & "&filter=" & lbFilter.Value)
	End Sub

	Private Sub btAdminEditaceSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdminEditaceSave.Click
		Dim err As New Renderer.FormErrors
		iID = inpID.Value
		Dim SqlSet As New FN.DB.SqlSet
		SQL = "SELECT * FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If Not DR.Read() Then Exit Sub
		If tbTxt.Text.Trim = "" Then
			err.Add("Chybí Text")
		Else
			If DR("Txt") <> tbTxt.Text.Trim Then SqlSet.Add("Txt=" & FN.DB.GetText(tbTxt.Text.Trim))
		End If
		If DR("Kat") <> lbKat.Value Then SqlSet.Add("Kat=" & lbKat.Value)
		If Sekce.Tabulka.hasTitulek Then
			If tbTitulek.Text.Trim = "" Then
				err.Add("Chybí Titulek")
			Else
				If DR("Titulek") <> tbTitulek.Text.Trim Then SqlSet.Add("Titulek=" & FN.DB.GetText(tbTitulek.Text.Trim))
			End If
		End If
		If Sekce.Tabulka.hasAnotace Then
			If tbAnotace.Text.Trim = "" Then
				err.Add("Chybí Anotace")
			Else
				If DR("Anotace") <> tbAnotace.Text.Trim Then SqlSet.Add("Anotace=" & FN.DB.GetText(tbAnotace.Text.Trim))
			End If
		End If
		If Sekce.Tabulka.isTxtCitaty Then
			If DR("Autor") <> lbCitatyAutor.SelectedValue Then SqlSet.Add("Autor=" & lbCitatyAutor.SelectedValue)
		End If
		DR.Close()
		If err.Count = 0 Then
			Dim Url As String = "Editace.aspx?akce=GoTo&sekce=" & Sekce.Alias & "&id=" & iID & "&filter=" & KatFilter
			If SqlSet.Text <> "" Then
				CMD.CommandText = "UPDATE " & Sekce.Tabulka.Nazev & SqlSet.Text & " WHERE ID=" & iID
				If CMD.ExecuteNonQuery = 1 Then
					Dim Zprava As String = "EDIT [" & SqlSet.Columns & "]"
					CMD.CommandText = "INSERT INTO AdminEditHistory (HistorySekce,HistoryDBID,HistoryJmeno,HistoryTxt) Values (" & FN.DB.GetText(Sekce.Alias) & ", " & iID & ", " & FN.DB.GetText(MyUser.Nick) & ", " & FN.DB.GetText(Zprava) & ")"
					CMD.ExecuteNonQuery()
				End If
				MakeAdminStat("StatEdit", MyUser.AdminID, Sekce.Alias)
				FN.Cache.ClearSekce(Sekce.Alias)
				Url = FN.URL.SetQuery(Url, "report", "Ulozeno")
			Else
				Url = FN.URL.SetQuery(Url, "report", "NeniZmena")
			End If
			CMD.Connection.Close()
			If Request.QueryString("nav") = "False" Then
				FN.Redir(inpReferer.Value)
			Else
				Response.Redirect(Url)
			End If
		Else
			CMD.Connection.Close()
			phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Private Sub btnSekce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSekce.Click
		iID = inpID.Value
		Dim SekceNew As New Fog.Sekce(lbSekce.SelectedValue)
		If SekceNew.ID <> Sekce.ID Then
			SQL = "UPDATE " & SekceNew.Tabulka.Nazev & " SET Kat=" & SekceNew.FindKategorieNew & ", Sekce='" & SekceNew.Alias & "' WHERE ID=" & iID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			If CMD.ExecuteNonQuery = 1 Then
				Dim Zprava As String = "EDIT [Sekce]=" & SekceNew.Alias
				CMD.CommandText = "INSERT INTO AdminEditHistory (HistorySekce,HistoryDBID,HistoryJmeno,HistoryTxt) Values ('" & SekceNew.Alias & "'," & iID & ",'" & MyUser.Nick & "','" & Zprava.Replace("'", "''") & "')"
				CMD.ExecuteNonQuery()
			End If
			CMD.Connection.Close()
		End If
		If Request.QueryString("nav") = "False" Then
			FN.Redir(inpReferer.Value)
		Else
			Response.Redirect("Editace.aspx?sekce=" & Sekce.Alias & "&id=" & iID & "&filter=" & KatFilter)
		End If
	End Sub

	Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
		iID = inpID.Value
		Dim SqlWhere As New FN.DB.SqlWhere
		SqlWhere.Add("ID=" & iID)
		SQL = "UPDATE " & Sekce.Tabulka.Nazev & " SET Kat=" & Sekce.FindKategorieDeleted & " WHERE ID=" & iID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If CMD.ExecuteNonQuery = 1 Then
			Dim Zprava As String = "DEL"
			If inpAdminEditaceDelete.Value.Trim <> "" Then Zprava &= " {" & inpAdminEditaceDelete.Value.Trim & "}"
			CMD.CommandText = "INSERT INTO AdminEditHistory (HistorySekce,HistoryDBID,HistoryJmeno,HistoryTxt) Values ('" & Sekce.Alias & "'," & iID & ",'" & MyUser.Nick & "','" & Zprava.Replace("'", "''") & "')"
			CMD.ExecuteNonQuery()
		End If
		MakeAdminStat("StatDel", MyUser.AdminID, Sekce.Alias)
		FN.Cache.ClearSekce(Sekce.Alias)
		If Request.QueryString("nav") = "False" Then
			FN.Redir(inpReferer.Value)
		Else
			Response.Redirect("Editace.aspx?akce=GoTo&report=deleted&sekce=" & Sekce.Alias & "&id=" & iID & "&filter=" & KatFilter)
		End If
	End Sub

	Private Sub btnGoTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoTo.Click
		Response.Redirect("Editace.aspx?akce=GoTo&sekce=" & Sekce.Alias & "&id=" & Val.ToInt(tbGoTo.Text) & "&filter=" & KatFilter)
	End Sub

	Sub ShowReport()
		If Not IsNothing(Request.QueryString("report")) Then
			Select Case Request.QueryString("report")
				Case "NeniZmena" : lblReport.Text = "Nebyla zjištìna žádná zmìna!"
				Case "Ulozeno" : lblReport.Text = "Uloženo! (è." & Request.QueryString("id") & ")"
				Case "deleted" : lblReport.Text = "Urèeno ke smazání! (è." & Request.QueryString("id") & ")"
				Case "sekce" : lblReport.Text = "Sekce zmìnìna!! (è." & Request.QueryString("id") & ")"
			End Select
			pnlReport.Visible = True
		End If
	End Sub

	Sub MakeAdminStat(ByVal sColumn As String, ByVal AdminID As Integer, ByVal SekceAlias As String)
		Dim Obdobi As String = Now.ToString("yyyMM")
		Dim SQL As String = "UPDATE AdminStat SET StatAdmin=" & AdminID & ", StatObdobi=" & Obdobi & ", StatSekce='" & SekceAlias & "', " & sColumn & "=" & sColumn & "+1 WHERE StatAdmin=" & AdminID & " AND StatObdobi=" & Obdobi & " AND StatSekce='" & SekceAlias & "'"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If CMD.ExecuteNonQuery() = 0 Then
			CMD.CommandText = "INSERT INTO AdminStat (StatAdmin, StatObdobi, StatSekce, " & sColumn & ") Values (" & AdminID & ", " & Obdobi & ", '" & SekceAlias & "', 1)"
			CMD.ExecuteNonQuery()
		End If
		CMD.Connection.Close()
	End Sub

End Class