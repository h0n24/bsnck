Class _KnihaNavstev_Add
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If IsPostBack Then
			FormPostBack()
		Else
			FormInit()
		End If
	End Sub

	Sub FormInit()
		Dim MyUser As New Fog.User
		Me.tbJmeno.Text = MyUser.Jmeno
		Me.tbEmail.Text = MyUser.Email
	End Sub

	Sub FormPostBack()
		Dim MyIni As New Fog.Ini.Init
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim Email As String = Me.tbEmail.Text.Trim
		Dim Txt As String = Me.txtTxt.InnerText.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then err.Add("Zadejte své jméno")
		If Txt = "" Then err.Add("Chybí Text")
		If FN.Text.Test.Diakritika(Txt) = 0 Then err.Add("Kvùli zneužívání knihy návštìv zahraniènímy roboty musíte použít diakritiku (ìšè..)")
		If Txt.Length > 500 Then err.Add("Text je pøíliš dlouhý")
		If err.Count = 0 Then
			Dim SQL As String = "INSERT INTO GuestBook (GBookDate,GBookWeb,GbookName,GbookEmail,GbookText) Values (" & FN.DB.GetDateTime(Now) & ",'" & MyIni.Web.ID & "','" & Jmeno.Replace("'", "''") & "','" & Email & "','" & Txt.Replace("'", "''") & "')"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			CMD.ExecuteNonQuery()
			CMD.Connection.Close()
			Response.Redirect("/KnihaNavstev.aspx")
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

End Class