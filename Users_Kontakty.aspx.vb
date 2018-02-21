Class _Users_Kontakty
	Inherits System.Web.UI.Page
	Dim iID As Integer
	Dim Akce As String
	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		iID = Val.ToInt(Request.QueryString("id"))
		Akce = Request.QueryString("akce")
		If MyUser.isLogged Then
			If Akce = "" Then
				RenderKontakty()
				Me.Form1.Visible = False
			Else
				Me.phKontakty.Visible = False
				Select Case Akce
					Case "new"
						If IsPostBack Then
							FormPostBack()
						End If
						Me.aKontaktyDelete.Visible = False
					Case "edit"
						If IsPostBack Then
							FormPostBack()
						Else
							FormInit()
						End If
						Me.aKontaktyDelete.HRef &= iID
					Case "delete"
						KontaktDelete()
				End Select
			End If
		Else
			Me.phKontakty.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nejste správnì pøihlášený uživatel !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub RenderKontakty()
		Dim sHtml As String
		Dim SQL = "SELECT KontaktID,KontaktDatum,KontaktJmeno,KontaktEmail,KontaktTel FROM UsersKontakty WHERE KontaktUser=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			Dim Jmeno, Email, Telefon As String
			Dim Datum As Date
			Dim iID As Integer
			While DR.Read()
				iID = DR("KontaktID")
				Datum = DR("KontaktDatum")
				Jmeno = DR("KontaktJmeno")
				Email = DR("KontaktEmail")
				Telefon = DR("KontaktTel")
				sHtml &= "<div class=""box2"" style=""clear:both;"">"
				sHtml &= "<p><a style=""font-weight:bold;"" href=""/Users_Kontakty.aspx?akce=edit&id=" & iID & """>" & Server.HtmlEncode(Jmeno) & "</a></p>"
				If Email <> "" Then
					sHtml &= "<p>Email: " & Server.HtmlEncode(Email) & "</p>"
				End If
				If Telefon <> "" Then
					sHtml &= "<p>Tel: " & Server.HtmlEncode(Telefon) & "</p>"
				End If
				sHtml &= "<p class=""title"">Poslední zmìna: " & Datum.Date.ToString("d.M.yyyy") & "</p>"
				sHtml &= "</div>"
			End While
		Else
			sHtml = "<b>Nemáte žádné kontakty !!</b>"
		End If
		DR.Close()
		CMD.Connection.Close()
		Me.divUsersKontaktyHtml.InnerHtml = sHtml
	End Sub

	Sub FormInit()
		Dim sError As String
		Dim SQL As String = "SELECT * FROM UsersKontakty WHERE KontaktID=" & iID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			If DR("KontaktUser") = MyUser.ID Then
				Me.tbJmeno.Text = DR("KontaktJmeno")
				Me.tbEmail.Text = DR("KontaktEmail")
				Me.tbTel.Text = DR("KontaktTel")
			Else
				sError &= "Toto není váš kontakt !!"
			End If
		Else
			sError &= "Kontakt neexistuje !!"
		End If
		DR.Close()
		CMD.Connection.Close()
		If sError <> "" Then
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = sError
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub FormPostBack()
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim Email As String = Me.tbEmail.Text.Trim
		Dim Telefon As String = Me.tbTel.Text.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then err.Add("Zadejte jméno")
		If Email <> "" And Not FN.Email.IsEmailValid(Email) Then err.Add("Špatnì zadaný email")
		If Email = "" And Telefon = "" Then err.Add("Zadejte alespoò jeden kontakt")
		If err.Count = 0 Then
			Dim SQL As String
			If Akce = "edit" Then
				SQL = "UPDATE UsersKontakty SET KontaktDatum=" & FN.DB.GetDateTime(Now) & ", KontaktJmeno='" & Jmeno.Replace("'", "''") & "', KontaktEmail='" & Email.Replace("'", "''") & "', KontaktTel='" & Telefon.Replace("'", "''") & "' WHERE KontaktID=" & iID & " AND KontaktUser=" & MyUser.ID
			Else
				SQL = "INSERT INTO UsersKontakty (KontaktUser,KontaktDatum,KontaktJmeno,KontaktEmail,KontaktTel) Values (" & MyUser.ID & ", " & FN.DB.GetDateTime(Now) & ", '" & Jmeno.Replace("'", "''") & "', '" & Email.Replace("'", "''") & "', '" & Telefon.Replace("'", "''") & "')"
			End If
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			CMD.ExecuteNonQuery()
			CMD.Connection.Close()
			Response.Redirect("/Users/kontakty.aspx")
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Sub KontaktDelete()
		Dim SQL As String = "DELETE FROM UsersKontakty WHERE KontaktID=" & iID & " AND KontaktUser=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		CMD.ExecuteNonQuery()
		CMD.Connection.Close()
		Response.Redirect("/Users/kontakty.aspx")
	End Sub

End Class