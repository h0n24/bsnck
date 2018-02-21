Partial Class Users_Premium
	Inherits System.Web.UI.Page

	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If MyUser.isLogged Then
			lblVS.Text = MyUser.ID
			If MyUser.Premium Then
				Dim SQL As String = "SELECT PremiumExpires FROM UsersPremium WHERE PremiumUser=" & MyUser.ID
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
				If DR.Read Then
					lblExpires.Text = CDate(DR("PremiumExpires")).ToString
					pnlInfo.Visible = True
				End If
				DR.Close()
				CMD.Connection.Close()
			End If
		Else
			pnlPlatba.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Detailní informace se zobrazí pouze přihlášeným uživatelům !!"
			Me.phReport.Controls.Add(Report.Render)
		End If

	End Sub

End Class