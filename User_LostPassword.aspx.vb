Partial Class User_LostPassword
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
			FormSubmit()
		ElseIf Request.QueryString("task") = "sent" Then
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Email was sent successfully."
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub FormSubmit()
		Dim Email As String = tbEmail.Text.Trim
		Dim err As New Renderer.FormErrors
		If Email = "" Then
			err.Add("Email is missing")
		ElseIf Not FN.Email.IsEmailValid(Email) Then
			err.Add("Email has wrong format")
		Else
			Dim SQL As String = "SELECT UserEmail,UserPassword FROM GeoUsers WHERE UserEmail=" & FN.DB.GetText(Email)
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() Then
				Dim MyIni As New Fog.Ini.Init()
				Dim sBody As String = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
				sBody &= "Hello,<br/>" & vbCrLf
				sBody &= "Lost Password was requested from server " & MyIni.Web.Name & ".<br/>" & vbCrLf
				sBody &= "Please ignore this email if you don't require it.<br/>" & vbCrLf
				sBody &= "Your Email: " & Email & "<br/>" & vbCrLf
				sBody &= "Your Password: " & Server.HtmlEncode(DR("UserPassword")) & "<br/>" & vbCrLf
				sBody &= "Thank you for using our service.<br/>" & vbCrLf
				sBody &= "_____________________________________________<br/>" & vbCrLf & Reklama.Generate(601)
				sBody &= "</body></html>"
				Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, Email, "Lost Password", sBody)
				msg.IsBodyHtml = True
				If FN.Email.SendMessage(msg) Then
					DR.Close()
					CMD.Connection.Close()
					Response.Redirect("/User_LostPassword.aspx?task=sent")
				Else
					err.Add("Email cannot be sent due to unknown reason")
				End If
			Else
				err.Add("Email doesn't exist in database")
			End If
			DR.Close()
			CMD.Connection.Close()
		End If

		If err.Count <> 0 Then
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

End Class