Class _Obaly_Start
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim sekce As String = Request.QueryString("sekce")
		Page.Title = "Obaly " & sekce
	End Sub

End Class