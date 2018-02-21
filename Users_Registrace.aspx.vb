Class _Users_Registrace
	Inherits System.Web.UI.Page

	Dim Akce As String
	Dim MyUser As New Fog.User

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Akce = Request.QueryString("akce")
		If Page.IsPostBack Then
			FormSubmit()
		Else
			If Akce = "RegOK" Then
				Me.Form1.Visible = False
				Me.pnlRegOK.Visible = True
			Else
				FormInit()
			End If
		End If
	End Sub

	Sub FormInit()
		Me.inpReferer.Value = FN.URL.Referer
		If Akce = "edit" Then
			Dim SQL As String = "SELECT * FROM Users WHERE UserID=" & MyUser.ID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read Then
				Me.inpEmail.Value = DR("UserEmail")
				Me.inpJmeno.Value = DR("UserJmeno")
				Me.inpNick.Value = DR("UserNick")
				Me.txtRegistraceHeslo.InnerText = "(Mus�te znovu zadat sv� heslo, nebo si zvolte nov�)"
				Me.chbNews.Checked = (DR("UserNews") = 1)
				DR.Close()
				CMD.Connection.Close()
			Else
				DR.Close()
				CMD.Connection.Close()
				Response.Redirect(FN.URL.PredefinedURL.UserLogOut())
			End If
		Else
			Me.pnlHeslo.Visible = False
		End If
	End Sub

	Sub FormSubmit()
		Dim Email As String = Me.inpEmail.Value.Trim.ToLower
		Dim Jmeno As String = Me.inpJmeno.Value.Trim
		Dim Nick As String = Me.inpNick.Value.Trim
		Dim Heslo As String = Me.inpHeslo.Value
		Dim SouhlasNews As Integer = IIf(Me.chbNews.Checked, 1, 0)
		Dim err As New Renderer.FormErrors
		If Email = "" Then err.Add("Mus�te zadat sv�j email.")
		If Email <> "" And FN.Email.IsEmailValid(Email) = False Then err.Add("�patn� zadan� email")
		If Jmeno.Length < 4 Or Jmeno.IndexOf(" ") = -1 Then err.Add("Mus�te zadat sv� jm�no i p��jmen�")
		If Nick = "" Then
			err.Add("Mus�te zadat p�ezd�vku")
		ElseIf Nick.Length > 40 Then
			err.Add("P�ezd�vka je omezena na 40 znak�")
		ElseIf NickCharactersPassed(Nick) = False Then
			err.Add("P�ezd�vka m��e obsahovat pouze A-�, 0-9, mezeru, znaky &quot;._'-&amp;&quot;")
		ElseIf Char.IsLetterOrDigit(Nick.Chars(0)) = False Then
			err.Add("P�ezd�vka m��e za��nat pouze A-�, 0-9")
		End If
		If Akce = "edit" Then
			If Heslo = "" Then err.Add("Mus�te zadat heslo")
			If Heslo <> Me.inpHeslo2.Value Then err.Add("Heslo nebylo spr�vn� potvrzeno")
		End If
		Dim SQL As String = "SELECT UserEmail,UserJmeno,UserNick FROM Users WHERE (UserEmail='" & Email & "' OR UserNick=" & FN.DB.GetText(Nick) & ")"
		If Akce = "edit" Then SQL &= " AND UserID<>" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If Email <> "" And Nick <> "" Then
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() Then
				If Nick.ToLower = LCase(DR("UserNick")) Then err.Add("Nick <i>'" & Nick & "'</i> ji� pou��v� <i>'" & DR("UserJmeno") & "'</i>")
				If Email = DR("UserEmail") Then
					err.Add("Email <i>'" & Email & "'</i> ji� pou��v� <i>'" & DR("UserJmeno") & "' [" & DR("UserNick") & "]</i>")
					err.Add("Pokud jste pouze zapomn�l(a) heslo, m��ete si ho nechat zaslat na email (odkaz 'Zapomenut� heslo')")
				End If
			End If
			DR.Close()
		End If
		If err.Count <> 0 Then
			CMD.Connection.Close()
			Me.phErrors.Controls.Add(err.Render)
		Else
			If Akce = "edit" Then
				CMD.CommandText = "UPDATE Users SET UserEmail='" & Email & "', UserNick=" & FN.DB.GetText(Nick) & ", UserJmeno=" & FN.DB.GetText(Jmeno) & ", UserHeslo=" & FN.DB.GetText(Heslo) & ", UserNews=" & SouhlasNews & " WHERE UserID=" & MyUser.ID
				CMD.ExecuteNonQuery()
				CMD.Connection.Close()
				Response.Redirect("/Users_Login.aspx?akce=login&Login=" & Email & "&Heslo=" & Heslo & "&Referer=" & Server.UrlEncode(Me.inpReferer.Value))
			Else
				Heslo = FN.Text.RandomString(4).ToLower
				Dim MyIni As New Fog.Ini.Init
				Dim sBody As String = "D�kujeme za registraci na serveru " & MyIni.Web.URL & "." & vbCrLf & vbCrLf
				sBody &= "Pro prvn� p�ihl�en� pou�ijte sv�j email a n�sleduj�c� heslo:" & vbCrLf & Heslo & vbCrLf & vbCrLf
				sBody &= "Va�i registraci m��ete vyu��t na v�ech serverech v na�� spr�v�." & vbCrLf
				sBody &= "Pokud jste tento email obrd�eli omylem, nemus�te nic podnikat - z�ejm� n�kdo �patn� zadal sv�j email na na�ich str�nk�ch."
				Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, Email, "Registrace na " & MyIni.Web.URL, sBody)
				If FN.Email.SendMessage(msg) Then
					CMD.CommandText = "INSERT INTO Users (UserDatum,UserEmail,UserNick,UserJmeno,UserHeslo,UserNews) Values (" & FN.DB.GetDateTime(Now) & ", '" & Email & "', " & FN.DB.GetText(Nick) & ", " & FN.DB.GetText(Jmeno) & ", " & FN.DB.GetText(Heslo) & ", " & SouhlasNews & ")"
					CMD.ExecuteNonQuery()
					If IsPodezrelyNick(Nick) Then
						msg = New System.Net.Mail.MailMessage(MyIni.Web.Email, MyIni.Web.Email, "Varov�n�: Registrace u�ivatele " & Nick, "Nick registrovan�ho u�ivatele '" & Nick & "' obsahuje sledovan� text.")
						FN.Email.SendMessage(msg)
					End If
					CMD.Connection.Close()
					Response.Redirect("Users_Registrace.aspx?akce=RegOK")
				Else
					err.Add("Nelze odeslat email s heslem")
					Me.phErrors.Controls.Add(err.Render)
					CMD.Connection.Close()
				End If
			End If
		End If
	End Sub

	Function IsPodezrelyNick(ByVal Nick As String) As Boolean
		Dim arr() As String = System.Configuration.ConfigurationManager.AppSettings("Users.WarningNick").Split(",")
		For Each S As String In arr
			If Nick.ToLower.IndexOf(S) <> -1 Then Return True
		Next
	End Function

	Function NickCharactersPassed(ByVal Nick As String) As Boolean
		For Each S As Char In Nick
			If Not Char.IsLetterOrDigit(S) Then
				If S <> " " And S <> "." And S <> "_" And S <> "-" And S <> "'" And S <> "&" Then
					Return False
				End If
			End If
		Next
		Return True
	End Function

End Class