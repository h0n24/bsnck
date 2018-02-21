Class _Add
	Inherits System.Web.UI.Page

	Dim IDx As UInteger
	Dim Akce As String
	Dim Sekce As New Fog.Sekce
	Dim Kategorie As New Fog.Kategorie
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Akce = Request.QueryString("akce")
		If Request.QueryString("sekce") = "Dila" Then
			Sekce.Tabulka = New Fog.Tabulka("TxtDila")
		Else
			Sekce = New Fog.Sekce(Request.QueryString("sekce"))
		End If
		IDx = Val.ToInt(Request.QueryString("id"))
		Select Case Akce
			Case "", "edit"
				If Akce = "edit" Then
					aAddDelete.HRef = "/Add.aspx?akce=delete&sekce=" & Sekce.Alias & "&id=" & IDx
					If Page.IsPostBack Then
						aAddDelete.HRef &= "&referer=" & Server.UrlEncode(inpReferer.Value)
					Else
						aAddDelete.HRef &= "&referer=" & Server.UrlEncode(FN.URL.Referer)
					End If
					Page.Title = "Editace Díla"
				Else
					If Sekce.Tabulka.isTxtDila Then
						Page.Title = "Pøidej nový záznam (Díla)"
					Else
						Page.Title = "Pøidej nový záznam (" & Sekce.Nazev & ")"
					End If
					aAddDelete.Visible = False
				End If
				If Sekce.Tabulka.isTxtDila Then
					If MyUser.isLogged = False Then
						Report.Title = "Nejste pøihlášený autor !!"
						Report.Text = "Musíte se zaregistrovat, nebo pøihlásit. Díla lze pøidávat pouze jako registrovaný autor."
					ElseIf MyUser.Autor = False Then
						Response.Redirect("/Autori_Registrace.aspx")
					End If
				End If
				If Report.Title <> "" Then
					ShowError(Report.Title, Report.Text)
				Else
					If Not Page.IsPostBack Then FormInit()
				End If
			Case "delete"
				Delete()
			Case "addok"
				ShowAddOK()
		End Select
	End Sub

	Sub FormInit()
		inpReferer.Value = FN.URL.Referer
		tbJmeno.Text = MyUser.Nick
		If Not Sekce.Tabulka.hasTitulek Then divAddTitulek.Visible = False
		If Not Sekce.Tabulka.hasAnotace Then divAddAnotace.Visible = False
		If Request.QueryString("sekce") = "ErotickePovidky" Then
			divAddJmeno.Visible = False
			lbSekce.Items.Add(New ListItem("Erotické Povídky", "ErotickePovidky"))
			lbSekce.Enabled = False
		ElseIf Sekce.Tabulka.isTxtDila Then
			divAddJmeno.Visible = False
			lbSekce.Items.Add(New ListItem("- - - - - - - - -", "-"))
			Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceTable='TxtDila'")
			For Each Row As DataRow In dRows
				If Row("SekceAlias") <> "ErotickePovidky" Then
					lbSekce.Items.Add(New ListItem(Row("SekceNazev"), Row("SekceID")))
					If Row("SekceAlias") = Request.QueryString("sekce") Then lbSekce.SelectedIndex = lbSekce.Items.Count - 1
				End If
			Next
		Else
			divAddSekce.Visible = False
		End If

		If Sekce.Tabulka.isTxtDila Then
			Dim SQL As String = "SELECT ID,Titulek FROM Sbirky WHERE Autor=" & MyUser.ID
			Dim dt As DataTable = New DataTable("Sbirky")
			Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, FN.DB.GetConnectionString)
			DA.Fill(dt)
			DA.Dispose()
			lbSbirka.Items.Add(New ListItem("-- Nechci pøidat do sbírky --", "-"))
			lbSbirka.Items.Add(New ListItem("-- Vytvoøit novou sbírku --", "new"))
			For Each row As DataRow In dt.Rows
				lbSbirka.Items.Add(New ListItem(row("Titulek"), row("ID")))
			Next
		Else
			divSbirka.Visible = False
		End If
		If Request.QueryString("sekce") = "Citaty" Then
			tbTxt.Text = "{do závorek napište autora citátu}" & vbCrLf & vbCrLf & "... a tady napište citát"
		End If

		If Akce = "edit" Then
			Dim SQL As String = "SELECT * FROM TxtDila WHERE ID=" & IDx & " AND Autor=" & MyUser.ID
			Dim dt As DataTable = New DataTable()
			Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, FN.DB.GetConnectionString)
			DA.Fill(dt)
			DA.Dispose()
			If dt.Rows.Count > 0 Then
				Kategorie.ID = dt.Rows(0)("Kat")
				If Kategorie.Funkce = 2 Then
					ShowError("Smazané záznamy nelze editovat !!")
					Exit Sub
				End If
				tbTitulek.Text = dt.Rows(0)("Titulek")
				tbTxt.Text = SharedFunctions.TextFromFile.Read(dt.Rows(0)("Txt"), Sekce.Alias)
				inpAnotace.Value = dt.Rows(0)("Anotace")
			Else
				'--Chyba - pøíspìvek neexistuje
				Form1.Visible = False
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Záznam neexistuje !!"
				phReport.Controls.Add(Report.Render)
				Exit Sub
			End If
		End If
		InitKategorie()
	End Sub

	Sub FormSubmit(ByVal obj As Object, ByVal e As EventArgs)
		Dim Jmeno As String
		Dim Titulek As String = tbTitulek.Text.Trim
		Dim Anotace As String = inpAnotace.InnerText.Trim
		Dim Txt As String = tbTxt.Text.Trim
		Dim err As New Renderer.FormErrors
		If lbSekce.Visible Then
			If lbSekce.SelectedValue = "-" Then
				err.Add("Zvolte Sekci")
			Else
				Sekce.ID = lbSekce.SelectedValue
			End If
		End If
		If Not Sekce.Valid Then err.Add("Sekce není platná")
		If tbJmeno.Visible Then
			Jmeno = tbJmeno.Text.Trim()
			If Jmeno = "" Then err.Add("Zadejte své jméno")
		End If
		If tbTitulek.Visible Then
			If Titulek = "" Then err.Add("Chybí Titulek")
			If Titulek.Length > 100 Then err.Add("Délka titulku je omezena na 100 znakù")
		End If
		If Anotace = "" And inpAnotace.Visible Then err.Add("Chybí Anotace")
		If Anotace.Length > 255 And inpAnotace.Visible Then err.Add("Délka Anotace je maximálnì 255 znakù")
		If Txt = "" Then
			err.Add("Chybí Text")
		Else
			If FN.Text.Test.Diakritika(Txt) < 2 Then err.Add("Používejte diakritiku (ìšèøžýáíéù) - texty bez diakritiky nepøidáváme")
			If FN.Text.Test.VelkaPismena(Txt) > 30 Then err.Add("Velká písmena pište pouze na zaèátku vìty nebo ve jménech")
			If (Sekce.Tabulka.isTxtShort Or Sekce.Tabulka.isTxtCitaty) And Txt.Length > 7000 Then err.Add("Délka textu je omezena na 7000 znakù")
		End If

		Dim SQL As String
		Dim CMD As New System.Data.SqlClient.SqlCommand
		Dim DR As System.Data.SqlClient.SqlDataReader
		If MyUser.Autor And Akce = "" And Sekce.Tabulka.isTxtDila And MyUser.Premium = False Then
			CMD.CommandText = "SELECT count(*) AS Pocet FROM TxtDila WHERE Autor=" & MyUser.ID & " AND Datum>" & FN.DB.GetDateTime(Now.Today)
			CMD.Connection = FN.DB.Open
			DR = CMD.ExecuteReader
			If DR.Read Then
				If DR("Pocet") >= 1 Then err.Add("Povoleno je pøidat maximálnì 1 dílo dennì. Pøidávat více dìl umožní Premium úèet.")
			End If
			DR.Close()
			CMD.Connection.Close()
		End If

		If err.Count > 0 Then
			phErrors.Controls.Add(err.Render)
			Exit Sub
		End If

		If Sekce.Tabulka.hasAnotace Then
			Anotace = Replace(Anotace, vbCrLf, " ")
			FN.Text.ShortTwoSpaces(Anotace)
		End If
		If Not Sekce.Tabulka.isTxtDila Then
			FN.Text.ShortTwoSpaces(Txt)
			FN.Text.RemoveSpaceBeforeCRLF(Txt)
			FN.Text.ShortThreeCRLF(Txt)
			FN.Text.ShortFourEndings(Txt)
		End If
		Dim Zprava As String = tbVzkaz.Text.Trim
		If Zprava <> "" Then Zprava = " {" & Zprava & "}"
		Dim Kategorie As New Fog.Kategorie
		If lbKategorie.Visible Then
			Try				'-- ošetøení chyby pøi vyplém javascript
				Kategorie.ID = lbKategorie.SelectedValue
			Catch ex As Exception
				Kategorie.ID = Sekce.FindKategorieNew
			End Try
		Else
			Kategorie.ID = Sekce.FindKategorieNew
		End If
		CMD.Connection = FN.DB.Open
		If Akce = "edit" Then
			CMD.CommandText = "SELECT * FROM TxtDila WHERE ID=" & IDx & " AND Autor=" & MyUser.ID
			DR = CMD.ExecuteReader()
			If DR.Read Then
				Dim SqlSet As New FN.DB.SqlSet
				If DR("Sekce") <> Sekce.Alias Then
					SqlSet.Add("Sekce='" & Sekce.Alias & "'")
					SqlSet.Add("Kat=" & Kategorie.ID)
				ElseIf DR("Kat") <> Kategorie.ID Then
					SqlSet.Add("Kat=" & Kategorie.ID)
				End If
				If DR("Titulek") <> Titulek Then SqlSet.Add("Titulek=" & FN.DB.GetText(Titulek))
				If DR("Anotace") <> Anotace Then SqlSet.Add("Anotace=" & FN.DB.GetText(Anotace))
				If DR("Txt") <> Txt Then SqlSet.Add("Txt=" & FN.DB.GetText(Txt))
				DR.Close()
				If SqlSet.Text <> "" Then
					Zprava = "EDIT [" & SqlSet.Columns & "] " & Zprava
					CMD.CommandText = "UPDATE " & Sekce.Tabulka.Nazev & SqlSet.Text & " WHERE ID=" & IDx & " AND Autor=" & MyUser.ID
					CMD.ExecuteNonQuery()
				End If
			Else
				DR.Close()
			End If
		Else
			Dim SqlInsert As New FN.DB.SqlInsert
			SqlInsert.Add("Datum", FN.DB.GetDateTime(Now))
			SqlInsert.Add("Kat", Kategorie.ID)
			SqlInsert.Add("Txt", FN.DB.GetText(Txt))
			If Sekce.Tabulka.isTxtShort Then
				SqlInsert.Add("Poslal", FN.DB.GetText(Jmeno))
				SqlInsert.Add("Sekce", FN.DB.GetText(Sekce.Alias))
			ElseIf Sekce.Tabulka.isTxtCitaty Then
				SqlInsert.Add("Poslal", FN.DB.GetText(Jmeno))
				SqlInsert.Add("Autor", 1)
			Else
				SqlInsert.Add("Sekce", FN.DB.GetText(Sekce.Alias))
				SqlInsert.Add("Titulek", FN.DB.GetText(Titulek))
				If Sekce.Tabulka.isTxtDila And MyUser.Autor Then
					SqlInsert.Add("Autor", MyUser.ID)
					SqlInsert.Add("Anotace", FN.DB.GetText(Anotace))
				Else
					SqlInsert.Add("Poslal", FN.DB.GetText(Jmeno))
				End If
			End If
			SQL = "INSERT INTO " & Sekce.Tabulka.Nazev & SqlInsert.Text
			SQL &= "; SELECT @@IDENTITY AS Identita"
			CMD.CommandText = SQL
			DR = CMD.ExecuteReader()
			DR.Read()
			IDx = DR("Identita")
			DR.Close()
		End If
		If Zprava <> "" Then
			CMD.CommandText = "INSERT INTO AdminEditHistory (HistorySekce,HistoryDBID,HistoryJmeno,HistoryTxt) Values ('" & Sekce.Alias & "', " & IDx & ", '~', " & FN.DB.GetText(Zprava) & ")"
			CMD.ExecuteNonQuery()
		End If
		Dim RedirURL As String
		If Akce = "edit" Then
			RedirURL = inpReferer.Value
		Else
			RedirURL = "/Add.aspx?akce=addok&sekce=" & Sekce.Alias
		End If
		If Sekce.Tabulka.isTxtDila Then
			If lbSbirka.SelectedValue = "-" Then
			ElseIf lbSbirka.SelectedValue = "new" Then
				RedirURL = "/Sbirky_New.aspx?dilo=" & IDx
			Else
				Dim Sbirka As Int64 = Val.ToInt(lbSbirka.SelectedValue)
				RedirURL = "/Sbirky_New.aspx?akce=edit&sbirka=" & Sbirka & "&dilo=" & IDx
			End If
		End If
		CMD.Connection.Close()
		FN.Cache.ClearSekce(Sekce.Alias)
		FN.Redir(RedirURL)
	End Sub

	Sub ShowError(ByVal Title As String, Optional ByVal Text As String = "")
		Form1.Visible = False
		Report.Status = Renderer.Report.Statusy.Err
		Report.Title = Title
		Report.Text = Text
		phReport.Controls.Add(Report.Render)
	End Sub

	Sub ShowAddOK()
		Form1.Visible = False
		Report.Status = Renderer.Report.Statusy.OK
		Report.Title = "Váš pøíspìvek byl uložen."
		Report.Text &= "Pokud máte webovou stránku, dejte ostatním na vìdomí, že pøispíváte na " & MyIni.Web.Name & ". Pøíklad HTML kódu:<br/>"
		Report.Text &= "<i>Pøispívám na &lt;a href=""" & MyIni.Web.URL & """&gt;" & MyIni.Web.Name & "&lt;/a&gt;.</i>"
		phReport.Controls.Add(Report.Render)
	End Sub

	Sub InitKategorie()
		If Sekce.Tabulka.isTxtDila Then
			If Sekce.Valid Then
				If Not Kategorie.Valid Then Kategorie.ID = Sekce.FindKategorieNew
				Dim dRows() As DataRow = FN.Cache.dtKategorie.Select("KatSekce=" & Sekce.ID & " AND KatFunkce<>2", "KatPriorita")
				Dim li As New ListItem
				lbKategorie.Items.Clear()
				For Each dRow As DataRow In dRows
					li = New ListItem(dRow("KatNazev"), dRow("KatID"))
					If Kategorie.ID = dRow("KatID") Then li.Selected = True
					lbKategorie.Items.Add(li)
				Next
				lbKategorie.DataBind()
				lbKategorie.Visible = True
			Else
				lbKategorie.Visible = False
			End If
		Else
			lbKategorie.Visible = False
		End If
	End Sub

	Sub SekceChanged(ByVal obj As Object, ByVal e As EventArgs)
		Sekce.Alias = lbSekce.SelectedValue
		InitKategorie()
	End Sub

	Sub Delete()
		Dim SQL As String = "DELETE FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & IDx & " AND Autor=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		CMD.ExecuteNonQuery()
		CMD.Connection.Close()
		FN.Cache.ClearSekce(Sekce.Alias)
		FN.Redir(Request.QueryString("referer"))
	End Sub

End Class