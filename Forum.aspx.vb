Class _Forum
	Inherits System.Web.UI.Page

	Public IDx As Integer
	Public TemaID As Integer
	Public Const PageSize As Int16 = 50
	Private MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		TemaID = Val.ToInt(Request.QueryString("tema"))
		IDx = Val.ToInt(Request.QueryString("id"))
		If TemaID <> 0 Then
			ShowNahled()
		ElseIf IDx <> 0 Then
			ShowPrispevek()
		Else
			ShowTemata()
		End If
	End Sub

	Sub ShowTemata()
		Dim CMD As New System.Data.SqlClient.SqlCommand("SELECT TemaID, TemaDatum, TemaNazev, COUNT(Pocet) as Pocet, SUM(Pocet) AS Suma, MAX(Datum) AS PosledniPrispevek, MAX(Posledni) AS PosledniOdpoved FROM ForumTemata LEFT JOIN Forum ON Tema=TemaID WHERE TemaPriorita<>0 AND (TemaWeb='|ALL|' OR TemaWeb LIKE '%|" & MyIni.Web.ID & "|%') GROUP BY TemaID,TemaDatum,TemaNazev,TemaPriorita ORDER BY TemaPriorita, TemaID", FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If DR.HasRows Then
			Me.pnlTemata.Visible = True
			Me.rptTemata.DataSource = DR
			Me.rptTemata.DataBind()
		Else
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Žádná témata nenalezena !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub ShowNahled()
		Me.pnlNahled.Visible = True
		Dim SQLB As New FN.DB.SQLBuilder.Select("count(*)", "Forum", "Tema=" & TemaID)
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQLB.BuildSQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		DR.Read()
		Dim Pocet As Int64 = DR(0)
		If Pocet <= PageSize Then Me.liShowAll.Visible = False
		DR.Close()
		SQLB = New FN.DB.SQLBuilder.Select("ID,Datum,Posledni,Predmet,Pocet,Jmeno", "Forum", "Tema=" & TemaID, "ID Desc")
		If Request.QueryString("showall") <> "true" Then
			SQLB.TOP = PageSize
		Else
			Me.liShowAll.Visible = False
		End If
		CMD.CommandText = SQLB.BuildSQL
		DR = CMD.ExecuteReader
		If DR.HasRows Then
			Me.rptNahled.DataSource = DR
		Else
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nejsou žádné pøíspìvky !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		Me.pnlNahled.DataBind()
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub ShowPrispevek()
		'FN.SetCookie("Comment", "View", "seq")
		Dim Where As New FN.DB.SqlWhere
		If IDx <> 0 Then Where.Add("ID=" & IDx)
		Dim CMD As New System.Data.SqlClient.SqlCommand("SELECT ID, Xml FROM Forum" & Where.Text, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		If Not DR.Read() Then
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Záznam nebyl nalezen !!"
			Me.phReport.Controls.Add(Report.Render)
		Else
			Me.pnlPrispevek.Visible = True
			Dim xDoc As New System.Xml.XmlDocument
			xDoc.LoadXml(DR("Xml").ToString.Replace(vbCrLf, "&#60;br/&#62;" & vbCrLf))
			Me.xmlForum.Document = xDoc
			'Dim view As String
			'If View = "seq" Then
			'	Me.xmlForum.TransformSource = "/_xslt/CommentViewSeq.xslt"
			'Else
			Me.xmlForum.TransformSource = "/App_Shared/xslt/Forum_Tree.xslt"
			'End If
			Me.xmlForum.TransformArgumentList = New System.Xml.Xsl.XsltArgumentList
			Me.xmlForum.TransformArgumentList.AddParam("IDx", "", IDx)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Public Function ShowDatumPosledni(ByVal obj As Object) As String
		Try
			Return ", poslední " & CType(obj, DateTime).ToString("d.M.yy HH:mm")
		Catch ex As Exception
		End Try
	End Function

End Class