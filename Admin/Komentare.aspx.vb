Class Admin_Komentare
	Inherits System.Web.UI.Page

	Dim IDx As Integer
	Dim Sekce As Fog.Sekce

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		IDx = Val.ToInt(Request.QueryString("id"))
		Sekce = New Fog.Sekce(Request.QueryString("sekce"))
		Select Case Request.QueryString("akce")
			Case ""
				If IDx <> 0 Then
					If Page.IsPostBack Then
						FormSubmit()
					Else
						FormInit()
					End If
				Else
					ViewList()
				End If
			Case "delete"
				DeleteKoment()
		End Select
	End Sub

	Sub FormInit()
		inpReferer.Value = FN.URL.Referer
		Dim SQL As String = "SELECT KomentID,KomentDatum,KomentDBID,KomentUser,KomentTxt,UserNick,Titulek,Kat FROM " & Sekce.Tabulka.Nazev & "Komentare " & _
		"LEFT JOIN Users ON KomentUser=UserID LEFT JOIN " & Sekce.Tabulka.Nazev & " ON KomentDBID=ID WHERE KomentID=" & IDx
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.Read Then
			hlDelete.NavigateUrl = String.Format(hlDelete.NavigateUrl, Sekce.Alias, IDx, Server.UrlEncode(inpReferer.Value))
			Dim MyUser As New Fog.User
			Dim Kat As New Fog.Kategorie(DR("Kat"))
			If MyUser.isAdminSekce(Kat.Sekce.Alias) Then
				pnlEdit.Visible = True
				lblDatum.Text = DR("KomentDatum")
				If IsDBNull(DR("UserNick")) Then
					lblJmeno.Text = "uživatel neexistuje"
				Else
					lblJmeno.Text = Server.HtmlEncode(DR("UserNick"))
				End If
				lblSekce.Text = Server.HtmlEncode(Kat.Sekce.Nazev & " » " & Kat.Nazev)
				hlTitulek.Text = Server.HtmlEncode(DR("Titulek"))
				hlTitulek.NavigateUrl = String.Format("/{0}/{1}-view.aspx", Kat.Sekce.Alias, DR("KomentDBID"))
				tbText.Text = DR("KomentTxt")
			Else
				Dim Report As New Renderer.Report
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Nemáš práva na patøiènou sekci !!"
				phReport.Controls.Add(Report.Render)
			End If
		Else
			Dim Report As New Renderer.Report
			Report.Title = "Nenalezen žádný komentáø"
			phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub FormSubmit()
		Dim Report As New Renderer.Report
		Dim err As New Renderer.FormErrors
		Dim Txt As String = tbText.Text.Trim
		If Txt = "" Then err.Add("Zadejte text komentáøe")
		If Txt.Length > 3000 Then err.Add("Délka komentáøe je omezena na 3000 znakù")
		If FN.Text.Test.VelkaPismena(Txt) > 30 Then err.Add("Velká písmena pište pouze na zaèátku vìty nebo jména")
		If err.Count = 0 Then
			Dim CMD As New System.Data.SqlClient.SqlCommand("UPDATE " & Sekce.Tabulka.Nazev & "Komentare SET KomentTxt=" & FN.DB.GetText(Txt) & " WHERE KomentID=" & IDx, FN.DB.Open)
			If CMD.ExecuteNonQuery() = 0 Then
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Chyba pøi ukládání !!"
				Report.Text = "Záznam již asi neexistuje."
			Else
				Report.Title = "Komentáø úspìšnì uložen."
				FN.Redir(inpReferer.Value, Nothing)
			End If
		Else
			phErrors.Controls.Add(err.Render)
		End If
		phReport.Controls.Add(Report.Render)
		pnlEdit.Visible = False
	End Sub

	Sub ViewList()
		pnlList.Visible = True
		Dim SQL As String = "SELECT TOP 500 KomentID,KomentDatum,KomentTxt,UserNick,Kat FROM " & Sekce.Tabulka.Nazev & "Komentare LEFT JOIN Users ON KomentUser=Users.UserID " & _
		" LEFT JOIN " & Sekce.Tabulka.Nazev & " ON KomentDBID=ID WHERE Kat IN ( SELECT KatID FROM Kategorie WHERE KatSekce=" & Sekce.ID & " ) ORDER BY KomentID Desc"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		dgKomentareList.DataSource = DR
		dgKomentareList.DataBind()
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub DeleteKoment()
		Dim Report As New Renderer.Report
		Dim CMD As New System.Data.SqlClient.SqlCommand("DELETE FROM " & Sekce.Tabulka.Nazev & "Komentare WHERE KomentID=" & IDx, FN.DB.Open)
		If CMD.ExecuteNonQuery() = 0 Then
			CMD.Connection.Close()
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Chyba pøi mazání !!"
			Report.Text = "Záznam již neexistuje."
		Else
			CMD.Connection.Close()
			Report.Title = "Komentáø úspìšnì smazán"
			FN.Redir(Request.QueryString("referer"), Nothing)
		End If
		phReport.Controls.Add(Report.Render)
	End Sub

End Class