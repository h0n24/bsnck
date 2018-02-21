Class _Odkazy
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyIni As New Fog.Ini.Init
		Select Case Request.QueryString("domena")
			Case "prejcz" : Me.pnlPrejCZ.Visible = False
			Case "litercz" : Me.pnlLiterCZ.Visible = False
			Case "citujcz" : Me.pnlCitaty.Visible = False
			Case "basnecz" : Me.pnlBasneCZ.Visible = False
			Case "basnickycz" : Me.pnlBasnickyCZ.Visible = False
		End Select

		Dim arr As New ArrayList
		arr.Add("<h1>Pøej.cz <a href='http://prej.cz'>Pøání, Pøáníèka, Elektronické Pohlednice</a></h1>")
		arr.Add("<h1>Citaty-Osobnosti.cz <a href='http://citaty-osobnosti.cz'>Citáty, motta, moudra, myšlenky, osobnosti</a></h1>")
		arr.Add("<h1>Básnì.cz <a href='http://basne.cz'>Básnièky, Poezie, zamilované verše</a></h1>")
		arr.Add("<h1>Básnièky.cz <a href='http://basnicky.cz'>Básnì Poezie Verše</a></h1>")
		arr.Add("<h1>ABUX.net <a href='http://abux.net'>Geocaching, Cache</a></h1>")
		Me.rptOdkazy1.DataSource = FN.Array.PreskupitArrayList(arr, 0)
		Me.rptOdkazy1.DataBind()

	End Sub

End Class