Class _Autori_Registrace
	Inherits System.Web.UI.Page

	Dim Akce As String
	Dim MyUser As New Fog.User

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim sError As String
		Akce = Request.QueryString("akce")
		If MyUser.isLogged = False Then sError = "Autorem se m��e st�t pouze registrovan� u�ivatel.<br/>Pokud se opravdu chcete st�t autorem, mus�te se zaregistrovat. Registrovan� u�ivatel� se mus� p�ihl�sit."
		If Akce <> "edit" And MyUser.Autor Then sError = "Ji� jsi registrovan� autor."
		If sError <> "" Then
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = sError
			Me.phReport.Controls.Add(Report.Render)
		Else
			If Page.IsPostBack Then
				FormPostBack()
			Else
				FormInit()
			End If
		End If
	End Sub

	Sub FormInit()
		Me.inpReferer.Value = FN.URL.Referer
		If Akce = "edit" Then
			Dim SQL As String = "SELECT * FROM UsersAutori WHERE AutorUser=" & MyUser.ID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read() Then
				Dim Datum As Date = DR("AutorNarozen")
				Me.tbNarozen.Text = Datum.ToString("d.M.yyyy")
				Me.tbInfo.Text = DR("AutorInfo")
			End If
			DR.Close()
			CMD.Connection.Close()
		End If
	End Sub

	Sub FormPostBack()
		Dim err As New Renderer.FormErrors
		Dim sNarozen As String = Me.tbNarozen.Text.Trim
		Dim AutorNarozen As Date = FN.Datum.FormInput2Date(sNarozen)
		Dim AutorInfo As String = Me.tbInfo.Text.Trim
		If sNarozen = "" Then
			err.Add("Zadejte datum narozen�")
		Else
			If Not System.Text.RegularExpressions.Regex.IsMatch(sNarozen, "^\d{1,2}\.\d{1,2}\.\d{4}$") Then
				err.Add("�patn� form�t data narozen�")
			ElseIf FN.Datum.FormInput2Date(sNarozen) Is Nothing Then
				err.Add("�patn� zadan� datum narozen�")
			ElseIf Now.AddYears(-100) > AutorNarozen Or Now.AddYears(-3) < AutorNarozen Then
				err.Add("Va�e datum narozen� neodpov�d� skute�nosti")
			End If
		End If
		If AutorInfo = "" Then err.Add("Zadejte informace o v�s")
		If err.Count = 0 Then
			Dim CMD As New System.Data.SqlClient.SqlCommand("", FN.DB.Open)
			If Akce = "edit" Then
				CMD.CommandText = "UPDATE UsersAutori SET  AutorNarozen=" & FN.DB.GetDateTime(AutorNarozen) & ", AutorInfo=" & FN.DB.GetText(AutorInfo) & " WHERE AutorUser=" & MyUser.ID
				CMD.ExecuteNonQuery()
			Else
				CMD.CommandText = "INSERT INTO UsersAutori (AutorUser, AutorDatum, AutorNarozen, AutorInfo) Values (" & MyUser.ID & ", " & FN.DB.GetDateTime(Now) & ", " & FN.DB.GetDateTime(AutorNarozen) & ", '" & AutorInfo.Replace("'", "''") & "')"
				CMD.ExecuteNonQuery()
			End If
			CMD.Connection.Close()
			MyUser.Autor = True
			MyUser.WriteUserCookies()
			If Akce = "edit" Then
				FN.Redir(Me.inpReferer.Value)
			Else
				Response.Redirect("/Add.aspx?sekce=Dila")
			End If
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

End Class