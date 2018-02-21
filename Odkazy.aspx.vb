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
		arr.Add("<h1>P�ej.cz <a href='http://prej.cz'>P��n�, P��n��ka, Elektronick� Pohlednice</a></h1>")
		arr.Add("<h1>Citaty-Osobnosti.cz <a href='http://citaty-osobnosti.cz'>Cit�ty, motta, moudra, my�lenky, osobnosti</a></h1>")
		arr.Add("<h1>B�sn�.cz <a href='http://basne.cz'>B�sni�ky, Poezie, zamilovan� ver�e</a></h1>")
		arr.Add("<h1>B�sni�ky.cz <a href='http://basnicky.cz'>B�sn� Poezie Ver�e</a></h1>")
		arr.Add("<h1>ABUX.net <a href='http://abux.net'>Geocaching, Cache</a></h1>")
		Me.rptOdkazy1.DataSource = FN.Array.PreskupitArrayList(arr, 0)
		Me.rptOdkazy1.DataBind()

	End Sub

End Class