Partial Class _GSearch
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyIni As New Fog.Ini.Init
		body.Attributes.Add("style", "background-color:" & MyIni.Colors.Bg)
	End Sub

End Class