Class _OblibeniAutori
	Inherits System.Web.UI.Page

	Dim Akce As String
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Report As New Renderer.Report
		If MyUser.ID = 0 Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Tato funkce vy�aduje p�ihl�en�ho u�ivatele !!!"
			Me.phReport.Controls.Add(Report.Render)
			Me.pnlAkce.Visible = False
		Else
			Akce = Request.QueryString("akce")
			Select Case Akce
				Case "delete" : Delete()
				Case "add" : Add()
			End Select
			ShowList()
		End If
	End Sub

	Sub ShowList()
		Dim SQL As String = "SELECT OblibID,OblibAutor,OblibDatum,UserNick FROM OblibeniAutori LEFT JOIN Users ON OblibAutor=UserID WHERE OblibUser=" & MyUser.ID & " ORDER BY UserNick"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		Me.pnlList.Visible = True
		Me.repeaterList.DataSource = DR
		Me.repeaterList.DataBind()
		CMD.Connection.Close()
	End Sub

	Sub Add()
		Dim Autor As Int64 = Val.ToInt(Request.QueryString("autor"))
		Dim Report As New Renderer.Report
		Dim SQL As String = "SELECT * FROM OblibeniAutori WHERE OblibUser=" & MyUser.ID & " AND OblibAutor=" & Autor
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.Read Then
			DR.Close()
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!!"
			Report.Text = "Autor ji� je za�azen mezi obl�ben�mi."
		Else
			DR.Close()
			CMD.CommandText = "INSERT INTO OblibeniAutori (OblibUser,OblibAutor) VALUES (" & MyUser.ID & "," & Autor & ")"
			If CMD.ExecuteNonQuery = 0 Then
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "CHYBA !!!"
				Report.Text = "Nastala nezn�m� chyba p�i vkl�d�n� z�znamu do datab�ze."
			Else
				Report.Title = "Obl�ben� autor byl p�id�n do va�eho seznamu."
			End If
		End If
		CMD.Connection.Close()
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Sub Delete()
		Dim IDx As Long = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "DELETE FROM OblibeniAutori WHERE OblibID=" & IDx & " AND OblibUser=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim Report As New Renderer.Report
		If CMD.ExecuteNonQuery = 0 Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!!"
			Report.Text = "P�i maz�n� z�znamu do�lo k chyb�."
		Else
			Report.Title = "Obl�ben� autor byl odstran�n z va�eho seznamu."
		End If
		CMD.Connection.Close()
		Me.phReport.Controls.Add(Report.Render)
	End Sub

End Class