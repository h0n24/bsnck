Partial Class Final_User_Login
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Task As String = Request.QueryString("task")
		Select Case Task
			Case "login"
				Login()
			Case "checkcookies"
				CheckCookies()
			Case "logout"
				Logout()
				'Case "notlogged"
				'	NotLogged()
			Case "sendcode"
				SendCode()
			Case "verify"
				VerifyForm()
		End Select

	End Sub


	Sub Login()
		Dim Report As New Renderer.Report
		Dim Email As String = Request("Login")
		Dim Password As String = Request("Password")
		If Email = "" Or Password = "" Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "ERROR !!"
			Report.Text = "Please enter Email and Password."
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		Dim User As New Geo.User(Email)
		If User.Email = Email.ToLower And User.Password = Password Then
			If User.Verified Then
				User.WriteCookies()
				Dim sIP As String = Request.UserHostAddress
				Dim CMD As New System.Data.SqlClient.SqlCommand("UPDATE UserIPs SET IPDate=" & FN.DB.GetDateTime(Now) & " WHERE IPUser=" & User.ID & " AND IPIP=" & FN.DB.GetText(sIP), FN.DB.Open)
				If CMD.ExecuteNonQuery() = 0 Then
					CMD.CommandText = "INSERT INTO UserIPs (IPDate, IPUser, IPIP) Values (" & FN.DB.GetDateTime(Now) & "," & User.ID & "," & FN.DB.GetText(sIP) & ")"
					CMD.ExecuteNonQuery()
				End If
				CMD.Connection.Close()
				Dim RedirURL As String = FN.URL.Referer
				If RedirURL.ToLower.IndexOf("/user_login") <> -1 Then RedirURL = "/"
				Response.Redirect("/User_Login.aspx?task=checkcookies&referer=" & Server.UrlEncode(RedirURL))
			Else
				Response.Redirect("/User_Login.aspx?task=verify&user=" & User.ID)
			End If
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Wrong Password or Email !!"
			Report.Text = "Revise the data you entered and try again. "
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub CheckCookies()
		If FN.Cookies.Read("User", "ID") = "" Then
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "SORRY !!"
			Report.Text = "You have successfully logged in but your web browser doesn't accept cookies. Turn it on or ask your computer administrator for help."
			Me.phReport.Controls.Add(Report.Render)
		Else
			FN.Redir(Request.QueryString("Referer"))
		End If
	End Sub

	Sub Logout()
		FN.Cookies.Delete("User")
		FN.Redir("/")
	End Sub

	'Sub NotLogged()
	'Dim Report As New Renderer.Report
	'Report.Title = "Nejste přihlášený uživatel !!"
	'Report.Status = Renderer.Report.Statusy.Err
	'Report.Text = "Akce vyžaduje registrovaného uživatele. Musíte se zaregistrovat, nebo přihlásit."
	'Me.phReport.Controls.Add(Report.Render)
	'End Sub


	Sub SendCode()
		Dim MyIni As New Fog.Ini.Init
		Dim User As New Geo.User(Val.ToInt32(Request.QueryString("user")))
		If User.ID = 0 Then
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "ERROR !!"
			Report.Text = "User does not exist."
			Me.phReport.Controls.Add(Report.Render)
		Else
			Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, User.Email)
			msg.Subject = MyIni.Web.Name & " Account"
			msg.Body &= "Hello," & vbCrLf & "thank you for signing up." & vbCrLf & vbCrLf
			msg.Body &= "----------------------------" & vbCrLf
			msg.Body &= "UserName: " & User.UserName & vbCrLf
			msg.Body &= "Password: " & User.Password & vbCrLf
			msg.Body &= "Verification code: " & User.VerificationCode & vbCrLf
			msg.Body &= "----------------------------" & vbCrLf
			msg.Body &= "Type your verification code to the form." & vbCrLf
			msg.Body &= MyIni.Web.URL & vbCrLf
			FN.Email.SendMessage(msg)
			Response.Redirect("/User_Login.aspx?task=verify&msg=sent&user=" & User.ID)
		End If
	End Sub

	Sub VerifyForm()
		Dim User As New Geo.User(Val.ToInt32(Request.QueryString("user")))
		If Not User.Exists Then
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Error !!"
			Report.Text = "User does not exist."
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		Form1.Visible = True
		Page.Title = "Verify Email"
		hlResend.NavigateUrl = String.Format(hlResend.NavigateUrl, Val.ToInt(Request.QueryString("user")))
		Dim err As New Renderer.FormErrors
		If Page.IsPostBack Then
			If tbCode.Text.ToLower <> User.VerificationCode.ToLower Then
				err.Add("Wrong code")
				phErrors.Controls.Add(err.Render)
			Else
				Form1.Visible = False
				Dim CMD As New System.Data.SqlClient.SqlCommand("UPDATE GeoUsers SET UserVerified=1 WHERE UserID=" & User.ID, FN.DB.Open)
				CMD.ExecuteNonQuery()
				CMD.Connection.Close()
				Dim Report As New Renderer.Report
				Report.Title = "Thank you."
				Report.Text = "Your account is now verified. You can log in."
				Me.phReport.Controls.Add(Report.Render)
			End If
		Else
			If Request.QueryString("msg") = "sent" Then
				Dim Report As New Renderer.Report
				Report.Title = "Info: Email was sent " & Now
				Me.phReport.Controls.Add(Report.Render)
			End If
		End If
	End Sub

End Class