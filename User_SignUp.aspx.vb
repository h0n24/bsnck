Partial Class User_SignUp
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
			FormSubmit()
		Else
			FormInit()
		End If
	End Sub

	Sub FormInit()
		If Request.QueryString("task") = "edit" Then
			tbReferrer.Text = FN.URL.Referer
			phAgree.Visible = False
			phCaptcha.Visible = False
			Dim User As New Geo.User
			User.FillFromCookies()
			If User.Exists Then
				tbEmail.Text = User.Email
				tbUserName.Text = User.UserName
				tbHomeCoord.Text = User.HomeCoord.ToString
				cbNewsletter.Checked = User.NewsLetter
				cbHideUserName.Checked = User.Anononymous
			Else
				Response.Redirect("/")
			End If
		Else
			GanerateCaptcha()
		End If
	End Sub

	Sub FormSubmit()
		Dim Task As String = Request.QueryString("task")
		Dim User As New Geo.User
		User.FillFromCookies()
		Dim Email As String = tbEmail.Text.Trim.ToLower
		Dim UserName As String = tbUserName.Text.Trim
		Dim Password As String = tbPassword.Text
		Dim HomeCoord As New Geo.Coordinates()
		Dim err As New Renderer.FormErrors
		If Email = "" Then err.Add("Email is missing")
		If Email <> "" And FN.Email.IsEmailValid(Email) = False Then err.Add("Email has wrong format")
		If UserName = "" Then
			err.Add("User Name is missing")
		ElseIf UserName.Length < 2 Or UserName.Length > 50 Then
			err.Add("User Name should be between 2 and 50 characters long")
		End If
		If Not HomeCoord.Parse(tbHomeCoord.Text.Trim) Then err.Add("Home Coordinates have wrong format")
		If Password = "" Then err.Add("Password is missing")
		If Password <> tbPassword2.Text Then err.Add("Passwords do not match")
		If Task <> "edit" Then
			If Not cbAgree.Checked Then err.Add("Only users who agree to the Terms of Use and Privacy Statement are allowed")
			If tbCaptchaHash.Text <> FN.Crypting.EncryptMD5("SaltForSecureCaptcha!" & tbCaptcha.Text) Then err.Add("Wrong Captcha")
		End If
		Dim SQL As String = "SELECT UserEmail,UserNick FROM GeoUsers WHERE (UserEmail=" & FN.DB.GetText(Email) & " OR UserNick=" & FN.DB.GetText(UserName) & ")"
		If Task = "edit" Then SQL &= " AND UserID<>" & User.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If Email <> "" And UserName <> "" Then
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() Then
				If UserName.ToLower = DR("UserNick").ToString.ToLower Then err.Add("User Name is already used")
				If Email = DR("UserEmail").ToString.ToLower Then err.Add("Email is used by '" & DR("UserNick") & "'")
			End If
			DR.Close()
		End If
		If err.Count <> 0 Then
			CMD.Connection.Close()
			phErrors.Controls.Add(err.Render)
			GanerateCaptcha()
		Else
			If Task = "edit" Then
				Dim RedirURL As String = tbReferrer.Text
				CMD.CommandText = "UPDATE GeoUsers SET UserNick=" & FN.DB.GetText(UserName) & ", UserPassword=" & FN.DB.GetText(Password) & ", UserLat=" & HomeCoord.LatToSQL & ", UserLon=" & HomeCoord.LonToSQL & ", UserAnonymous=" & Val.ToInt16(cbHideUserName.Checked) & ", UserNews=" & Val.ToInt16(cbNewsletter.Checked)
				If Email <> User.Email Then
					RedirURL = "/User_Login.aspx?task=sendcode&user=" & User.ID
					CMD.CommandText &= ", UserEmail=" & FN.DB.GetText(Email) & ", UserVerified=0"
				End If
				CMD.CommandText &= " WHERE UserID=" & User.ID
				CMD.ExecuteNonQuery()
				CMD.Connection.Close()
				FN.Redir(RedirURL)
			Else
				CMD.CommandText = "INSERT INTO GeoUsers (UserDate,UserNick,UserEmail,UserPassword,UserLat,UserLon,UserVerified,UserAnonymous,UserNews) Values (" & FN.DB.GetDateTime(Now) & ", " & FN.DB.GetText(UserName) & ", " & FN.DB.GetText(Email) & ", " & FN.DB.GetText(Password) & ", " & HomeCoord.LatToSQL & ", " & HomeCoord.LonToSQL & ", 0, " & Val.ToInt(cbHideUserName.Checked) & "," & Val.ToInt(cbNewsletter.Checked) & ")"
				CMD.CommandText &= "; SELECT @@IDENTITY AS Identita"
				Dim UserID As Int64 = CMD.ExecuteScalar
				Dim InvitedBy As Integer = Val.ToInt(FN.Cookies.Read("InvitedBy"))
				If InvitedBy > 0 Then
					CMD.CommandText = "INSERT INTO UserInvitations (InviteReceiver,InviteSender) Values (" & UserID & ", " & InvitedBy & ")"
					CMD.ExecuteNonQuery()
				End If
				CMD.Connection.Close()
				Response.Redirect("/User_Login.aspx?task=sendcode&user=" & UserID)
			End If
		End If
	End Sub

	Sub GanerateCaptcha()
		Randomize()
		Dim a As Int16 = 50 * Rnd()
		Dim b As Int16 = 1 + 10 * Rnd()
		lblCaptcha.Text = "Captcha: " & a & " + " & b & " = "
		tbCaptchaHash.Text = FN.Crypting.EncryptMD5("SaltForSecureCaptcha!" & (a + b))
	End Sub

End Class