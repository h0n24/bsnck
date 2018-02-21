Class _Pohlednice_Preview
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim sHtml, sSQL, sWhere, sPageTitle As String
		Dim Kat As New Fog.Kategorie(Request.QueryString("kat"))
		Dim Autor As String = "" & Request.QueryString("autor")
		Dim Zobrazeni As String = "" & Request.QueryString("show")
		If Kat.Valid Then
			sWhere = "Kat=" & Kat.ID
			sPageTitle = " » " & Kat.Nazev
		ElseIf Autor <> "" Then
			sWhere = "Autor=" & Autor
		End If
		If Zobrazeni = "" Then Zobrazeni = "compact"

		If Zobrazeni = "compact" Then
			sSQL = "SELECT ID,Soubor,Odeslano,Hodnoceni FROM Pohlednice WHERE " & sWhere
		Else
			sSQL = "SELECT ID,Datum,Soubor,Titulek,Odeslano,AutorNick,Hodnoceni FROM Pohlednice LEFT JOIN PohledniceAutori ON Autor=AutorID WHERE " & sWhere
		End If
		Dim dtPohlednice As DataTable = New DataTable("Pohlednice")
		Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(sSQL, FN.DB.GetConnectionString)
		DBDA.Fill(dtPohlednice)
		DBDA.Dispose()
		Page.Title = "Pohlednice"
		Dim CelkemZaznamu As Integer = dtPohlednice.Rows.Count
		If CelkemZaznamu > 0 Then
			Page.Title &= sPageTitle
			Dim SortBy As String = Request.QueryString("sort")
			sHtml = "Øadit podle: "
			Dim sOrderBy As String
			Dim URLSortBy = FN.URL.RemoveQueryFolder(System.Web.HttpContext.Current.Request.RawUrl, "pg")			 'Odstranit stránky
			If SortBy = "date" Or SortBy = "" Then
				sOrderBy = "ID Desc"
				sHtml &= "<span class=""selected"">Nejnovìjších</span> | "
			Else
				sHtml &= "<a href='" & FN.URL.SetQueryFolder(URLSortBy, "sort", "date") & "'>Nejnovìjších</a> | "
			End If
			If SortBy = "send" Then
				sOrderBy = "Odeslano Desc"
				sHtml &= "<span class=""selected"">Poètu poslání</span> | "
			Else
				sHtml &= "<a href='" & FN.URL.SetQueryFolder(URLSortBy, "sort", "send") & "'>Poètu poslání</a> | "
			End If
			If SortBy = "hodn" Then
				sOrderBy = "Hodnoceni Desc"
				sHtml &= "<span class=""selected"">Hodnocení</span></p>"
			Else
				sHtml &= "<a href='" & FN.URL.SetQueryFolder(URLSortBy, "sort", "hodn") & "'>Hodnocení</a>"
			End If

			Me.BoxSortBy.InnerHtml = sHtml
			Dim sURL = Request.RawUrl
			If Zobrazeni = "compact" Then
				Me.pPohlednicePreviewZaznamu.InnerHtml = "Nalezeno " & CelkemZaznamu & " pohlednic." & " (<a href='" & FN.URL.SetQueryFolder(sURL, "show", "detail") & "'>zobrazit detaily</a>)"
				Me.repPohledniceCompact.DataSource = dtPohlednice.Select(Nothing, sOrderBy)
				Me.repPohledniceCompact.DataBind()
			Else
				Me.pPohlednicePreviewZaznamu.InnerHtml = "Nalezeno " & CelkemZaznamu & " pohlednic." & " (<a href='" & FN.URL.SetQueryFolder(sURL, "show", "compact") & "'>zobrazt náhledy</a>)"
				Me.repPohledniceDetails.DataSource = dtPohlednice.Select(Nothing, sOrderBy)
				Me.repPohledniceDetails.DataBind()
			End If
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

End Class