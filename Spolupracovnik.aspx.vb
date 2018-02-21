Class _Spolupracovnik
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case "Grafik"
				Me.pnlSpolupracovnikGrafik.Visible = True
			Case "Pohlednice"
				Me.pnlSpolupracovnikPohlednice.Visible = True
			Case "Jine"
				Me.pnlSpolupracovnikJine.Visible = True
			Case "Editor"
				Me.pnlSpolupracovnikEditor.Visible = True
				Dim Sekce As New Fog.Sekce
				For Each S As String In System.Configuration.ConfigurationManager.AppSettings("Sekce.EditorNeni").Split(",")
					Sekce.Alias = S
					Me.ulEditoriSekce.InnerHtml &= "<li>" & Sekce.Nazev & "</li>"
				Next
		End Select
	End Sub

End Class