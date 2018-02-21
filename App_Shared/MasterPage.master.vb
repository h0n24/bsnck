Partial Class Final_MasterPage
	Inherits System.Web.UI.MasterPage

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Request.Url.Host.IndexOf("proxy.") <> -1 Then
			phAnalytics.Visible = False
		End If

	End Sub

End Class