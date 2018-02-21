Partial Class Sbirky_New
	Inherits System.Web.UI.Page
	Dim Akce As String
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not MyUser.isLogged Then
			Report.Title = "Nejste přihlášený uživatel !!"
			Report.Status = Renderer.Report.Statusy.Err
			Report.Text = "Musíte se zaregistrovat, nebo přihlásit. Sbírky jsou určeny pouze pro registrované autory."
			phReport.Controls.Add(Report.Render)
			Form1.Visible = False
			Exit Sub
		ElseIf MyUser.Autor = False Then
			Response.Redirect("/Autori_Registrace.aspx")
		End If
		Akce = "" & Request.QueryString("akce")
		If Akce = "edit" Then
			Page.Title = "Editace sbírky"
		Else
			Page.Title = "Nová sbírka"
			aDelete.Visible = False
		End If

		Select Case Akce
			Case "", "edit"
				If Not Page.IsPostBack Then FormInit()
			Case "delete"
				Delete()
			Case "saved"
				ShowSaved()
		End Select
	End Sub

	Sub FormInit()
		Dim SbirkaID As UInt64 = Val.ToInt(Request.QueryString("sbirka"))
		Dim DiloID As Integer = Val.ToInt(Request.QueryString("dilo"))
		Dim CMD As New System.Data.SqlClient.SqlCommand("SELECT ID,Titulek FROM TxtDila WHERE Autor=" & MyUser.ID, FN.DB.Open())
		Dim DR As System.Data.SqlClient.SqlDataReader

		If Akce = "edit" Then
			CMD.CommandText = "SELECT ID,Dokonceno,Kat,Titulek,Prolog FROM Sbirky WHERE ID=" & SbirkaID & " AND Autor=" & MyUser.ID
			DR = CMD.ExecuteReader
			If DR.Read Then
				lbKategorie.SelectedValue = DR("Kat")
				tbTitulek.Text = DR("Titulek")
				Page.Title &= " " & DR("Titulek")
				inpProlog.InnerText = DR("Prolog")
				If Not IsDBNull(DR("Dokonceno")) Then
					chbDokonceno.Checked = True
					chbDokonceno.Enabled = False
				End If
			Else
				Me.Form1.Visible = False
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "CHYBA !!!"
				Report.Text = "Záznam nebyl nalezen."
				Me.phReport.Controls.Add(Report.Render)
			End If
			DR.Close()
			CMD.CommandText = "SELECT ID,Titulek FROM SbirkyObsah LEFT JOIN TxtDila ON ObsahDilo=TxtDila.ID WHERE ObsahSbirka=" & SbirkaID & " AND Autor=" & MyUser.ID & " ORDER BY ObsahPoradi"
			DR = CMD.ExecuteReader
			If DR.HasRows Then
				lbSbirka.DataSource = DR
				lbSbirka.DataTextField = "Titulek"
				lbSbirka.DataValueField = "ID"
				lbSbirka.DataBind()
			End If
			DR.Close()
			Me.aDelete.HRef = "/Sbirky_New.aspx?akce=delete&amp;sbirka=" & SbirkaID
		End If

		Dim dRows() As DataRow = FN.Cache.dtKategorie.Select("KatSekce=" & Fog.Sekce.NazevToID("Sbirky"))
		Dim li As ListItem
		For Each dRow As DataRow In dRows
			li = New ListItem(dRow("KatNazev"), dRow("KatID"))
			lbKategorie.Items.Add(li)
		Next
		Me.lbKategorie.DataBind()
		CMD.CommandText = "SELECT ID,Titulek FROM TxtDila WHERE Autor=" & MyUser.ID & " ORDER BY Titulek"
		DR = CMD.ExecuteReader
		If DR.HasRows Then
			lbDila.DataSource = DR
			lbDila.DataTextField = "Titulek"
			lbDila.DataValueField = "ID"
			lbDila.DataBind()
			If DiloID = 0 Then
				lbDila.SelectedIndex = lbDila.Items.Count - 1
			Else
				lbDila.SelectedValue = DiloID
			End If
		Else
			btnAdd.Enabled = False
		End If
		DR.Close()

		CMD.Connection.Close()
		If DiloID <> 0 Then
			li = lbDila.Items.FindByValue(DiloID)
			If Not li Is Nothing Then
				If lbSbirka.Items.FindByValue(li.Value) Is Nothing Then lbSbirka.Items.Add(New ListItem(li.Text, li.Value))
			End If
			lbSbirka.SelectedValue = DiloID
		End If
		lbSbirka.Items.Add(New ListItem("--- KONEC ---", "END"))
		If lbSbirka.SelectedItem Is Nothing Then lbSbirka.SelectedValue = "END"
		SbirkaReindex()
		CheckButtonRemove()
	End Sub

	Sub FormSubmit(ByVal obj As Object, ByVal e As EventArgs)
		Dim SbirkaID As UInt64 = Val.ToInt(Request.QueryString("sbirka"))
		Dim Dokonceno As DateTime = Now
		Dim Titulek As String = tbTitulek.Text.Trim
		Dim Prolog As String = inpProlog.InnerText.Trim
		Dim Kategorie As UInt16 = Val.ToInt(lbKategorie.SelectedValue)
		Dim err As New Renderer.FormErrors
		If Kategorie = 0 Then err.Add("Zvolte Kategorii")
		If Titulek = "" Then err.Add("Chybí Název")
		If Prolog = "" Then err.Add("Chybí Prolog")
		If Prolog.Length > 2000 And inpProlog.Visible Then err.Add("Délka Prologu je maximálně 2000 znaků")
		If chbDokonceno.Checked And lbSbirka.Items.Count < 3 Then err.Add("Dokončené sbírky musí obsahovat alespoň 2 díla.")
		If err.Count > 0 Then phErrors.Controls.Add(err.Render) : Exit Sub
		FN.Text.ShortTwoSpaces(Prolog)
		FN.Text.RemoveSpaceBeforeCRLF(Prolog)
		FN.Text.ShortThreeCRLF(Prolog)
		Dim SQL As String
		If Akce = "edit" Then
			SQL = "SELECT ID,Datum,Dokonceno FROM Sbirky WHERE Autor=" & MyUser.ID & " AND Titulek=" & FN.DB.GetText(Titulek) & " AND ID<>" & SbirkaID
		Else
			SQL = "SELECT ID,Datum,Dokonceno FROM Sbirky WHERE Autor=" & MyUser.ID & " AND Titulek=" & FN.DB.GetText(Titulek)
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.Read Then
			err.Add("Sbírku s tímto názvem již máte založenou")
			DR.Close()
			phErrors.Controls.Add(err.Render)
			CMD.Connection.Close()
			Exit Sub
		Else
			DR.Close()
		End If

		If Akce = "edit" Then
			Dim SqlSet As New FN.DB.SqlSet
			SqlSet.Add("Kat=" & Kategorie)
			SqlSet.Add("Titulek=" & FN.DB.GetText(Titulek))
			SqlSet.Add("Prolog=" & FN.DB.GetText(Prolog))
			If chbDokonceno.Enabled = True And chbDokonceno.Checked Then SqlSet.Add("Dokonceno=" & FN.DB.GetDateTime(Now))
			CMD.CommandText = "UPDATE Sbirky" & SqlSet.Text & " WHERE ID=" & SbirkaID & " AND Autor=" & MyUser.ID
			If CMD.ExecuteNonQuery() = 1 Then
				CMD.CommandText = "DELETE FROM SbirkyObsah WHERE ObsahSbirka=" & SbirkaID
				CMD.ExecuteNonQuery()
				If lbSbirka.Items.Count > 1 Then
					For f As UInt16 = 0 To lbSbirka.Items.Count - 2
						CMD.CommandText = "INSERT INTO SbirkyObsah (ObsahSbirka,ObsahDilo,ObsahPoradi) VALUES (" & SbirkaID & "," & Val.ToInt(lbSbirka.Items(f).Value) & "," & f + 1 & ")"
						CMD.ExecuteNonQuery()
					Next
				End If
			Else
				CMD.Connection.Close()
				ShowNotSaved()
				Exit Sub
			End If
		Else
			Dim SqlInsert As New FN.DB.SqlInsert
			SqlInsert.Add("Datum", FN.DB.GetDateTime(Now))
			SqlInsert.Add("Kat", Kategorie)
			SqlInsert.Add("Titulek", FN.DB.GetText(Titulek))
			SqlInsert.Add("Autor", MyUser.ID)
			SqlInsert.Add("Prolog", FN.DB.GetText(Prolog))
			If chbDokonceno.Checked Then SqlInsert.Add("Dokonceno", FN.DB.GetDateTime(Now))
			CMD.CommandText = "INSERT INTO Sbirky " & SqlInsert.Text
			CMD.CommandText &= "; SELECT @@IDENTITY AS Identita"
			DR = CMD.ExecuteReader()
			DR.Read()
			SbirkaID = DR("Identita")
			DR.Close()
			If lbSbirka.Items.Count > 1 Then
				For f As UInt16 = 0 To lbSbirka.Items.Count - 2
					CMD.CommandText = "INSERT INTO SbirkyObsah (ObsahSbirka,ObsahDilo,ObsahPoradi) VALUES (" & SbirkaID & "," & Val.ToInt(lbSbirka.Items(f).Value) & "," & f + 1 & ")"
					CMD.ExecuteNonQuery()
				Next
			End If
		End If
		CMD.Connection.Close()
		Response.Redirect("/Sbirky_Show.aspx?sbirka=" & SbirkaID)
	End Sub

	Sub Delete()
		Dim SbirkaID As UInt64 = Val.ToInt(Request.QueryString("sbirka"))
		Dim CMD As New System.Data.SqlClient.SqlCommand("DELETE FROM Sbirky WHERE ID=" & SbirkaID & " AND Autor=" & MyUser.ID, FN.DB.Open)
		If CMD.ExecuteNonQuery() = 1 Then
			Report.Title = "Sbírka byla smazána."
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!!"
			Report.Text = "Při mazání nastala chyba, záznam již neexistuje."
		End If
		CMD.Connection.Close()
		Me.phReport.Controls.Add(Report.Render)
		Me.Form1.Visible = False
	End Sub

	Sub ShowSaved()
		Me.Form1.Visible = False
		Report.Status = Renderer.Report.Statusy.OK
		Report.Title = "Váš příspěvek byl uložen."
		Report.Text = "Pokud máte webovou stránku, dejte ostatním na vědomí, že přispíváte na " & MyIni.Web.Name & ". Příklad HTML kódu:<br/>"
		Report.Text &= "<i>Přispívám na &lt;a href=""" & MyIni.Web.URL & """&gt;" & MyIni.Web.Name & "&lt;/a&gt;.</i>"
		Me.phReport.Controls.Add(Report.Render)
	End Sub
	Sub ShowNotSaved()
		Me.Form1.Visible = False
		Report.Status = Renderer.Report.Statusy.Err
		Report.Title = "Váš příspěvek nemohl být uložen."
		Report.Text = "Při ukládání nastala neznámá chyba."
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
		If lbSbirka.SelectedItem Is Nothing Then Exit Sub
		If lbSbirka.SelectedValue = "END" Or lbSbirka.SelectedIndex = 0 Then Exit Sub
		Dim i As UInt16 = lbSbirka.SelectedIndex
		Dim li As ListItem = lbSbirka.SelectedItem
		lbSbirka.Items.Remove(li)
		lbSbirka.Items.Insert(i - 1, li)
		lbSbirka.SelectedIndex = i - 1
		SbirkaReindex()
	End Sub

	Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
		If lbSbirka.SelectedItem Is Nothing Then Exit Sub
		Dim i As UInt16 = lbSbirka.SelectedIndex
		If lbSbirka.Items.Count <= i + 2 Then Exit Sub
		Dim li As ListItem = lbSbirka.SelectedItem
		lbSbirka.Items.Remove(li)
		lbSbirka.Items.Insert(i + 1, li)
		lbSbirka.SelectedIndex = i + 1
		SbirkaReindex()
	End Sub

	Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
		If lbDila.SelectedItem Is Nothing Then Exit Sub
		If lbSbirka.SelectedItem Is Nothing Then Exit Sub
		If Not lbSbirka.Items.FindByValue(lbDila.SelectedValue) Is Nothing Then Exit Sub
		Dim li As New ListItem(lbDila.SelectedItem.Text, lbDila.SelectedValue)
		lbSbirka.Items.Insert(Val.ToInt(lbSbirka.SelectedIndex), li)
		CheckButtonRemove()
		SbirkaReindex()
	End Sub

	Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
		If lbSbirka.SelectedItem Is Nothing Then Exit Sub
		If lbSbirka.SelectedValue = "END" Then Exit Sub
		Dim i As UInt16 = lbSbirka.SelectedIndex
		lbSbirka.Items.Remove(lbSbirka.SelectedItem)
		lbSbirka.SelectedIndex = i
		CheckButtonRemove()
		SbirkaReindex()
	End Sub

	Sub CheckButtonRemove()
		If lbSbirka.Items.Count > 1 Then
			btnRemove.Enabled = True
		Else
			btnRemove.Enabled = False
		End If
	End Sub

	Sub SbirkaReindex()
		If lbSbirka.Items.Count < 2 Then Exit Sub
		Dim S As String
		Dim i As Int16
		For f As UInt16 = 0 To lbSbirka.Items.Count - 2
			S = lbSbirka.Items(f).Text
			i = S.IndexOf(".")
			S = S.Substring(i + 1, S.Length - i - 1)
			lbSbirka.Items(f).Text = f + 1 & "." & S
		Next
	End Sub

End Class