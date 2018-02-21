Class _Vzkaz_View
	Inherits System.Web.UI.Page

	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		If MyUser.ID = 0 Then
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Tato funkce vyžaduje pøihlášeného uživatele !!!"
			Me.phReport.Controls.Add(Report.Render)
		Else
			ShowVzkaz()
		End If
	End Sub

	Sub ShowVzkaz()
		Dim IDx As Int64 = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "SELECT VzkazDatum,VzkazOd,VzkazKomu,VzkazPredmet,VzkazTxt,Users.UserNick AS OdNick FROM Vzkazy LEFT JOIN Users ON VzkazOd=UserID WHERE VzkazID=" & IDx & " AND VzkazKomu=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.Read Then
			Dim OdNick As String = DR("OdNick") & ""
			Page.Title = "Vzkazy » " & DR("VzkazPredmet")
			Me.lblOd.Text = Server.HtmlEncode(OdNick)
			Me.lblDatum.Text = DR("VzkazDatum")
			Me.lblPredmet.Text = Server.HtmlEncode(DR("VzkazPredmet"))
			Me.lblTxt.Text = Server.HtmlEncode(DR("VzkazTxt")).Replace(vbCrLf, "<br/>" & vbCrLf)
			Me.aDelete.HRef = String.Format(aDelete.HRef.ToString, IDx)
			Me.aReply.HRef = String.Format(aReply.HRef, IDx)
			DR.Close()
			CMD.CommandText = "UPDATE Vzkazy SET VzkazPrecteno=" & FN.DB.GetDateTime(Now) & " WHERE VzkazID=" & IDx
			CMD.ExecuteNonQuery()
			SharedFunctions.VzkazyDeleteCookies()
		Else
			Page.Title = "Vzkaz neexistuje !!!"
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Vzkaz neexistuje !!!"
			Me.phReport.Controls.Add(Report.Render)
			DR.Close()
		End If
		CMD.Connection.Close()
	End Sub

End Class