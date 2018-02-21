Class _Rss
	Inherits System.Web.UI.Page
	Public DatumPosledniho As Date
	Public MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Response.ContentType = "text/xml"
		Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8")
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim sSQL As String
		If Request.QueryString("sekce") = "Dila" Then
			sSQL = "SELECT TOP 15 ID, Datum, Sekce, Titulek AS Title, Anotace AS Description FROM TxtDila ORDER BY ID Desc"
		ElseIf Sekce.Tabulka.isTxtDila Then
			sSQL = "SELECT TOP 15 ID, Datum, Sekce, Titulek AS Title, Anotace AS Description FROM " & Sekce.Tabulka.Nazev & " WHERE Sekce='" & Sekce.Alias & "' ORDER BY ID Desc"
		ElseIf Sekce.Tabulka.isTxtLong Then
			sSQL = "SELECT TOP 15 ID, Datum, Sekce, Titulek AS Title, CAST(Txt AS varchar(150)) AS Description FROM " & Sekce.Tabulka.Nazev & " WHERE Sekce='" & Sekce.Alias & "' ORDER BY ID Desc"
		Else
			Response.End()
		End If

		Dim dt As DataTable = DirectCast(Cache.Get("dtRss" & Request.QueryString("sekce")), DataTable)
		If dt Is Nothing Then
			dt = New DataTable()
			Dim DA As New System.Data.SqlClient.SqlDataAdapter(sSQL, FN.DB.GetConnectionString())
			DA.Fill(dt)
			DA.Dispose()
			Cache.Insert("dtRss" & Request.QueryString("sekce"), dt)
		End If
		If dt.Rows.Count > 0 Then
			DatumPosledniho = dt.Rows(0)("Datum")
		End If
		Me.rptRssItems.DataSource = dt
		Page.DataBind()
	End Sub

End Class