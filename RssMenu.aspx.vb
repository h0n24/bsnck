Class _RssMenu
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyIni As New Fog.Ini.Init
		If MyIni.Web.ID = "litercz" Then
			Me.pnlRssMenuLiter.Visible = True
		ElseIf MyIni.Web.ID = "basnecz" Or MyIni.Web.ID = "basnickycz" Then
			Me.pnlRssMenuBasne.Visible = True
		ElseIf MyIni.Web.ID = "citujcz" Then
			Me.pnlRssMenuCituj.Visible = True
		ElseIf MyIni.Web.ID = "ifuncz" Then
			Me.pnlRssMenuIfun.Visible = True
		End If
	End Sub

End Class