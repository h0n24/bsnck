Class _Anketa_Hlasuj
	Inherits System.Web.UI.Page

	Dim Report As New Renderer.Report

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim IDx As Int16 = Val.ToInt(Request.QueryString("id"))
		Dim Vyber As Int16 = Val.ToInt(Request.Form("AnketaHlas"))
		If Vyber = 0 Then
			Report.Text = "Není vybrána volba pro hlasování."
		ElseIf FN.Cookies.Read("Ankety", IDx) = Boolean.TrueString Then
			Report.Text = "Již jsi hlasoval."
		Else
			Dim SQL As String = "UPDATE Ankety SET Hlasy" & Vyber & "=Hlasy" & Vyber & "+1 WHERE ID=" & IDx
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			If CMD.ExecuteNonQuery <> 1 Then
				Report.Text = "Neznámá chyba pøi zápisu hlasu do databáze."
			End If
			CMD.Connection.Close()
		End If
		If Report.Text = "" Then
			Cache.Remove("Anketa")
			FN.Cookies.WriteKey("Ankety", IDx, Boolean.TrueString)
			FN.Redir(Request.Form("Referer"))
		Else
			ShowError()
		End If
	End Sub

	Sub ShowError()
		Report.Status = Renderer.Report.Statusy.Err
		Report.Title = "Chyba !!"
		Me.phReport.Controls.Add(Report.Render)
	End Sub

End Class