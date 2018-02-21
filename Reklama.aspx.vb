Class _Reklama
	Inherits System.Web.UI.Page

	Public MyIni As New Fog.Ini.Init

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Page.DataBind()
	End Sub

End Class