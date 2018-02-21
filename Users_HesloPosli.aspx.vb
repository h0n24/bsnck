Class _Users_HesloPosli
	Inherits System.Web.UI.Page
	Dim MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case ""
				If Page.IsPostBack Then
					FormPostBack()
				End If
			Case "SendOk"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Heslo bylo zasláno."
				Report.Text = "Váš email: " & Request.QueryString("email")
				Me.phReport.Controls.Add(Report.Render)
			Case "SendErr"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "CHYBA !!"
				Report.Text = "Pøi odesílání došlo k chybì - zøejmì chybný email."
				Me.phReport.Controls.Add(Report.Render)
		End Select
	End Sub

	Sub FormPostBack()
		Dim sEmail, sHeslo, sNick As String
		Dim sInput As String = Me.inpInput.Value.Trim
		Dim err As New Renderer.FormErrors
		If sInput = "" Then
			err.Add("Zadejte požadované údaje")
		Else
			Dim SQL As String = "SELECT UserEmail,UserHeslo,UserNick FROM Users WHERE UserNick='" & sInput.Replace("'", "''") & "' OR UserEmail='" & sInput.Replace("'", "''") & "'"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() Then
				sEmail = "" & DR("UserEmail")
				sNick = "" & DR("UserNick")
				sHeslo = DR("UserHeslo")
				If sInput = sEmail Or sInput = sNick Then
					If sEmail = "" Then
						err.Add("Ke jménu není registrovaný email !!")
					ElseIf Not (FN.Email.IsEmailValid(sEmail)) Then
						err.Add("Váš email není platná emailová adresa !!")
					End If
				End If
			Else
				err.Add("Uživatel neexistuje !!")
			End If
			DR.Close()
			CMD.Connection.Close()
		End If

		If err.Count = 0 Then
			Dim sBody As String = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
			sBody &= "Na serveru " & MyIni.Web.Name & " nìkdo požádal o zaslání hesla.<br>" & vbCrLf
			sBody &= "Pokud o tom nic nevíte, pak prosím tento email ignorujte.<br>" & vbCrLf
			sBody &= "Váš email: " & sEmail & "<br>" & vbCrLf
			sBody &= "Vaše heslo: " & Server.HtmlEncode(sHeslo) & "<br>" & vbCrLf
			sBody &= "Dìkujeme, že využíváte naše služby.<br>" & vbCrLf
			sBody &= "_____________________________________________<br>" & vbCrLf & Reklama.Generate(601)
			sBody &= "</body></html>"
			Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, sEmail, "Zapomenuté heslo", sBody)
			msg.IsBodyHtml = True
			If FN.Email.SendMessage(msg) Then
				Response.Redirect("/Users_HesloPosli.aspx?akce=SendOk&email=" & Server.UrlEncode(sEmail))
			Else
				Response.Redirect("/Users_HesloPosli.aspx?akce=SendErr&email=" & Server.UrlEncode(sEmail))
			End If
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

End Class
