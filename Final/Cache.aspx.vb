Partial Class Final_Cache
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim GCid As String = Request.QueryString("cache")
		Dim FIid As String = "FI" & GCid.Substring(2, GCid.Length - 2)
		Page.Title = Page.Title & " " & GCid
		hGCCode.InnerText = GCid
		hFICode.InnerText = FIid
	End Sub

End Class