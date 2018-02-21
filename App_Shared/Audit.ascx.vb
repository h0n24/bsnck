Partial Class App_Shared_Audit
	Inherits System.Web.UI.UserControl

	Public ToplistID, GoogleAnalyticsID, GoogleWebmasterToolsCode As String

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim URL As New Fog.URL(Request.Url)
		Select Case URL.Domain2
			Case "prej.cz"
				ToplistID = "33113" : GoogleAnalyticsID = "UA-2706672-4" : GoogleWebmasterToolsCode = "qBODDUbqKNEChkEfwMw2zqvsia8pcvW1mkO0gQLJMCQ="
			Case "citaty-osobnosti.cz"
				ToplistID = "1565805" : GoogleAnalyticsID = "UA-2706672-16" : GoogleWebmasterToolsCode = "fOVSrJS7-LYZwF1jSLyoIuoziUSQ95kimwtJb7W8AmI"
			Case "basne.cz"
				ToplistID = "59691" : GoogleAnalyticsID = "UA-2706672-2" : GoogleWebmasterToolsCode = "M9+FEGiksQnlZQus3VgY85Izj8kFygVpd4ZYk35F9js="
			Case "basnicky.cz"
				ToplistID = "69014" : GoogleAnalyticsID = "UA-2706672-3" : GoogleWebmasterToolsCode = "sW7BKpmfC5krHaHlyXTvF_jWfetWyjXkS5wZayNEUYc"
			Case "abux.net"
				ToplistID = "962136" : GoogleAnalyticsID = "UA-2706672-14" : GoogleWebmasterToolsCode = "WA2FB1uXuZCHkWyOejYLBSempLj4P-TgfsYdz09iSgU"
			Case "dosol.cz"
				ToplistID = "" : GoogleAnalyticsID = "UA-2706672-15" : GoogleWebmasterToolsCode = ""
		End Select

		If FN.Users.Prava.AuditOff Then
			phGoogleAnalytics.Visible = False
			phToplist.Visible = False
			lblText.Text = "[AuditOff]"
			lblText.Visible = True
		End If

		If GoogleAnalyticsID Is Nothing Then phGoogleAnalytics.Visible = False
		If ToplistID Is Nothing Then phToplist.Visible = False
		If GoogleWebmasterToolsCode <> "" Then
			Dim c As New HtmlControls.HtmlGenericControl("meta")
			c.Attributes("name") = "google-site-verification"
			c.Attributes("content") = GoogleWebmasterToolsCode
			Page.Header.Controls.Add(c)
		End If

	End Sub

End Class