Class _Master_Empty
	Inherits System.Web.UI.MasterPage

	Public MyIni As New Fog.Ini.Init

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If MyIni.RunLocal Then Page.Title = "###" & Page.Title
	End Sub

End Class