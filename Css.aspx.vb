Class _Css
	Inherits System.Web.UI.Page

	Protected MyIni As New Fog.Ini.Init

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		If MyIni.Web.ID = "litercz" Then phBillboard.Visible = True
	End Sub

End Class