Class _Svatky
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim dt As DataTable = New DataTable("Svatky")
		Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Svatky", FN.DB.GetConnectionString())
		DBDA.Fill(dt)
		DBDA.Dispose()
		Me.rptSvatkyMesic1.DataSource = dt.Select("Den>0 AND Den<201")
		Me.rptSvatkyMesic2.DataSource = dt.Select("Den>131 AND Den<301")
		Me.rptSvatkyMesic3.DataSource = dt.Select("Den>231 AND Den<401")
		Me.rptSvatkyMesic4.DataSource = dt.Select("Den>331 AND Den<501")
		Me.rptSvatkyMesic5.DataSource = dt.Select("Den>431 AND Den<601")
		Me.rptSvatkyMesic6.DataSource = dt.Select("Den>531 AND Den<701")
		Me.rptSvatkyMesic7.DataSource = dt.Select("Den>631 AND Den<801")
		Me.rptSvatkyMesic8.DataSource = dt.Select("Den>731 AND Den<901")
		Me.rptSvatkyMesic9.DataSource = dt.Select("Den>831 AND Den<1001")
		Me.rptSvatkyMesic10.DataSource = dt.Select("Den>931 AND Den<1101")
		Me.rptSvatkyMesic11.DataSource = dt.Select("Den>1031 AND Den<1201")
		Me.rptSvatkyMesic12.DataSource = dt.Select("Den>1131")
		Page.DataBind()
	End Sub

End Class