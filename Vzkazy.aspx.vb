Class _Vzkazy
	Inherits System.Web.UI.Page
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim Report As New Renderer.Report
		If MyUser.ID = 0 Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Tato funkce vyžaduje pøihlášeného uživatele !!!"
			Me.phReport.Controls.Add(Report.Render)
			Me.phMain.Visible = False
		Else
			If Request.QueryString("akce") = "delete" Then Delete()
			ShowList()
		End If
	End Sub

	Sub Delete()
		Dim IDx As Int64 = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "DELETE FROM Vzkazy WHERE VzkazID=" & IDx & " AND VzkazKomu=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If CMD.ExecuteNonQuery = 1 Then
			Report.Title = "Vzkaz byl smazán."
			SharedFunctions.VzkazyDeleteCookies()
		Else
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Vzkaz nelze smazat !!!"
		End If
		Me.phReport.Controls.Add(Report.Render)
		CMD.Connection.Close()
	End Sub

	Sub ShowList()
		Dim SQL As String = "SELECT VzkazID,VzkazDatum,VzkazPrecteno,VzkazOd,VzkazKomu,VzkazPredmet,Users.UserNick AS OdNick FROM Vzkazy LEFT JOIN Users ON VzkazOd=UserID WHERE VzkazKomu=" & MyUser.ID & " ORDER BY VzkazID DESC"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		Me.phMain.Visible = True
		Me.repeaterList.DataSource = DR
		Me.repeaterList.DataBind()
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class