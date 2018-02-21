Class _Users_Login
	Inherits System.Web.UI.Page
	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case "login"
				Login()
			Case "LoginOk"
				LoginOk()
			Case "logout"
				Logout()
			Case "notlogged"
				NotLogged()
			Case "VerifyEmail"
				VerifyEmail()
		End Select
	End Sub

	Sub Login()
		Dim MyIni As New Fog.Ini.Init
		Dim Report As New Renderer.Report
		Dim Login As String = LCase("" & Request("Login"))
		If Login = "" Then
			Report.Title = "CHYBA !!"
			Report.Text = "P�ihla�ovac� jm�no nesm� b�t pr�zdn�."
			Report.Status = Renderer.Report.Statusy.Err
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		Dim Heslo As String = "" & Request("Heslo")
		Dim RedirURL As String
		If Request.QueryString("Referer") Is Nothing Then
			RedirURL = FN.URL.Referer
		Else
			RedirURL = "" & Request.QueryString("Referer") 'pou��v� se nap�. p�i redir z UserRegistrace
		End If
		If RedirURL.ToLower.IndexOf("/users_") <> -1 Then RedirURL = ""
		Dim SQL As String = "SELECT UserID,UserHeslo,UserEmail,UserNick,UserJmeno,UserDomeny,AutorUser,AdminID,AdminSekce,AdminAkce,PremiumExpires FROM Users LEFT JOIN UsersAutori ON UserID=AutorUser LEFT JOIN UsersAdmins ON UserID=AdminID LEFT JOIN UsersPremium ON UserID=PremiumUser WHERE UserEmail=" & FN.DB.GetText(Login)
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read() Then
			If Login = LCase(DR("UserEmail")) And Heslo = DR("UserHeslo") Then
				MyUser.ID = DR("UserID")
				MyUser.Email = DR("UserEmail")
				MyUser.Nick = DR("UserNick")
				MyUser.Jmeno = DR("UserJmeno")
				Dim Domeny As New Fog.Seznam("" & DR("UserDomeny"))
				Dim URL As New Fog.URL(Request.Url)
				Domeny.Add(URL.Domain2)
				If IsDBNull(DR("AdminID")) = False Then
					MyUser.AdminID = DR("AdminID")
					MyUser.AdminAkce = "" & DR("AdminAkce")
					MyUser.AdminSekce = "" & DR("AdminSekce")
				End If
				If IsDBNull(DR("AutorUser")) = False Then
					MyUser.Autor = True
				End If
				If IsDBNull(DR("PremiumExpires")) = False Then
					If Today.CompareTo(DR("PremiumExpires")) <= 0 Then MyUser.Premium = True
				End If
				DR.Close()
				CMD.CommandText = "UPDATE Users SET UserDatumLog=" & FN.DB.GetDateTime(Now) & ", UserDomeny='" & Domeny.Text & "', UserIP='" & Request.UserHostAddress & "' WHERE UserID=" & MyUser.ID
				CMD.ExecuteNonQuery()
				MyUser.WriteUserCookies()

				Dim NickPrevious As String = FN.Cookies.Read("Web", "User")
				FN.Cookies.WriteKeyWeb("User", MyUser.Nick)
				If NickPrevious <> "" And NickPrevious <> MyUser.Nick Then
					CMD.CommandText = "INSERT INTO UsersSharingPC (NickNew,NickOld) Values (" & FN.DB.GetText(MyUser.Nick) & "," & FN.DB.GetText(NickPrevious) & ")"
					CMD.ExecuteNonQuery()
				End If

			Else
				DR.Close()
				Report.Title = "�patn� heslo nebo �patn� u�ivatel."
				Report.Text = "Zapomenut� heslo si m��ete nechat <a href='/Users_HesloPosli.aspx'>poslat</a> emailem."
			End If
		Else
			DR.Close()
			Report.Title = "Nezn�m� u�ivatel."
			Report.Text = "Zapomenut� heslo si m��ete nechat <a href='/Users_HesloPosli.aspx'>poslat</a> emailem."
		End If
		CMD.Connection.Close()
		If Report.Title <> "" Then
			Report.Status = Renderer.Report.Statusy.Err
			Me.phReport.Controls.Add(Report.Render)
		Else
			Response.Redirect("Users_Login.aspx?akce=LoginOk&Referer=" & Server.UrlEncode(RedirURL))
		End If
	End Sub

	Sub LoginOk()
		If FN.Cookies.Read("User", "ID") = "" Then
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "P�ihl�en� prob�hlo v po��dku, ale v� prohl�e� m� vypl� cookies. Zapn�te si cookies, nebo kontaktujte va�eho spr�vce po��ta�e."
			Me.phReport.Controls.Add(Report.Render)
		Else
			Dim S As String = Request.QueryString("Referer")
			FN.Redir(Request.QueryString("Referer"))
		End If
	End Sub

	Sub Logout()
		FN.Cookies.Delete("User")
		Dim RedirURL As String = FN.URL.Referer
		If RedirURL.ToLower.IndexOf("/users") <> -1 Then RedirURL = ""
		FN.Redir(RedirURL)
	End Sub

	Sub NotLogged()
		Dim Report As New Renderer.Report
		Report.Title = "Nejste p�ihl�en� u�ivatel !!"
		Report.Status = Renderer.Report.Statusy.Err
		Report.Text = "Akce vy�aduje registrovan�ho u�ivatele. Mus�te se zaregistrovat, nebo p�ihl�sit."
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Sub VerifyEmail()
		Dim Report As New Renderer.Report
		Report.Status = Renderer.Report.Statusy.Err
		Report.Title = "CHYBA !!"
		Report.Text = "TEST Ov��en�."
		Me.phReport.Controls.Add(Report.Render)
	End Sub

End Class