Partial Class Invitation
	Inherits System.Web.UI.Page
	Public U As New Geo.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		U.FillFromCookies()
		If Not U.Exists Then
			phInvitation.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "ERROR !!"
			Report.Text = "Only for registered users. Please log in."
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
	End Sub

End Class