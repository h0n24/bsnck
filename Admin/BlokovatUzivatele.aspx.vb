Partial Class Admin_BlokovatUzivatele
	Inherits System.Web.UI.Page

	Dim Report As New Renderer.Report
	Dim MyUser As New Fog.User

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If MyUser.AdminAkce.IndexOf("BlokovatUzivatele") = -1 Then
			Me.form1.Visible = False
			Exit Sub
		End If
		ShowTable()
		If Page.IsPostBack Then
			FormSubmit()
		Else
			Dim akce As String = Request.QueryString("akce")
			If akce = "delete" Then
				Delete()
			ElseIf akce = "edit" Then
				Edit()
			End If
		End If
	End Sub

	Sub Delete()
		Dim Uzivatel As Integer = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "DELETE FROM BlokovatUzivatele WHERE Uzivatel=" & Uzivatel
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If CMD.ExecuteNonQuery = 0 Then
			Report.Title = "CHYBA pøi mazání uživatele."
		Else
			Report.Title = "Vymazán uživatel."
		End If
		CMD.Connection.Close()
		Report.Status = Renderer.Report.Statusy.Err
		Me.phReport.Controls.Add(Report.Render)
		ShowTable()
	End Sub

	Sub Edit()
		Dim Uzivatel As Integer = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "SELECT * FROM BlokovatUzivatele WHERE Uzivatel=" & Uzivatel
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.Read Then
			Me.literalTitulek.Text = "Editace záznamu"
			Me.tbUzivatel.Text = DR("Uzivatel")
			Me.tbUzivatel.Enabled = False
			Me.tbDovod.Text = DR("Duvod")
			Me.tbZprava.Text = DR("ZpravaUzivateli")
			Me.chbEdit.Checked = True
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Záznam neexistuje !!!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub FormSubmit()
		Dim Uzivatel As Int64 = Val.ToInt(Me.tbUzivatel.Text)
		Dim Duvod As String = Me.tbDovod.Text
		Dim ZpravaUzivateli As String = Me.tbZprava.Text
		Dim err As New Renderer.FormErrors
		If Uzivatel = 0 Then err.Add("Špatnì zadané ID uživatele")
		If Duvod = "" Then err.Add("Dùvod blokování chybí")
		If ZpravaUzivateli = "" Then err.Add("Zpráva pro uživatele chybí")
		If err.Count = 0 Then
			Dim MyUser As New Fog.User
			Dim SQL As String
			If Me.chbEdit.Checked = True Then
				SQL = "UPDATE BlokovatUzivatele SET Duvod=" & FN.DB.GetText(Duvod) & ", ZpravaUzivateli=" & FN.DB.GetText(ZpravaUzivateli) & " WHERE Uzivatel=" & Uzivatel
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				CMD.ExecuteNonQuery()
				CMD.Connection.Close()
			Else
				SQL = "SELECT * FROM BlokovatUzivatele WHERE Uzivatel=" & Uzivatel
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
				If DR.Read Then
					DR.Close()
					CMD.Connection.Close()
					err.Add("Uživatel již existuje")
					Me.phReport.Controls.Add(err.Render)
					Exit Sub
				Else
					DR.Close()
					CMD.CommandText = "INSERT INTO BlokovatUzivatele (Uzivatel,Admin,Duvod,ZpravaUzivateli) VALUES (" & Uzivatel & "," & MyUser.ID & "," & FN.DB.GetText(Duvod) & "," & FN.DB.GetText(ZpravaUzivateli) & ")"
					CMD.ExecuteNonQuery()
					CMD.Connection.Close()
				End If
			End If
			Response.Redirect("BlokovatUzivatele.aspx")
		Else
			Me.phReport.Controls.Add(err.Render)
		End If
	End Sub

	Sub ShowTable()
		Dim SQL As String = "SELECT Uzivatel,Datum,Duvod,ZpravaUzivateli,Users.UserNick FROM BlokovatUzivatele LEFT JOIN Users ON Uzivatel=UserID ORDER BY Datum DESC"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.HasRows Then
			Me.GridView1.DataSource = DR
			Me.GridView1.DataBind()
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Databáze je prázdná !!!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class