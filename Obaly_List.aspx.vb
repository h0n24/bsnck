Class _Obaly_List
	Inherits System.Web.UI.Page

	Public Sekce As String

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Sekce = "" & Request.QueryString("sekce")
		If Not Request.QueryString("znak") Is Nothing Then
			Page.Title = Sekce & " » " & Request.QueryString("znak")
		ElseIf Not Request.QueryString("autor") Is Nothing Then
			Page.Title = Sekce & " » " & Request.QueryString("autor")
		Else
			Page.Title = Sekce
		End If

		Select Case Sekce.ToLower
			Case "audio"
				If Request.QueryString("autor") Is Nothing Then
					ShowAutori()
				Else
					ShowNazvy()
				End If
			Case Else
				ShowNazvy()
		End Select
	End Sub

	Sub ShowNazvy()
		Dim SqlWhere As New FN.DB.SqlWhere
		SqlWhere.Add("ObalSekce='" & Sekce & "'")
		If Not Request.QueryString("znak") Is Nothing Then
			SqlWhere.Add("ObalNazev LIKE '" & Request.QueryString("znak") & "%'")
		ElseIf Not Request.QueryString("autor") Is Nothing Then
			SqlWhere.Add("ObalAutor='" & Request.QueryString("autor") & "'")
		End If
		Dim SQL As String = "SELECT ObalID, ObalNazev FROM Obaly" & SqlWhere.Text & " ORDER BY ObalNazev"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			Me.rptSeznam.DataSource = DR
			Me.rptSeznam.DataBind()
			Me.pnlObalyList.Visible = True
		Else
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub ShowAutori()
		Dim SqlWhere As New FN.DB.SqlWhere
		SqlWhere.Add("ObalSekce='" & Sekce & "'")
		SqlWhere.Add("ObalAutor LIKE '" & Request.QueryString("znak") & "%'")
		Dim SQL As String = "SELECT ObalAutor FROM Obaly" & SqlWhere.Text & "GROUP BY ObalAutor ORDER BY ObalAutor"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			'Dim Pocet As Integer = dt.Rows.Count
			'If Pocet > 0 Then
			Me.rptAutori.DataSource = DR
			Me.rptAutori.DataBind()
			Me.pnlObalyAutori.Visible = True
		Else
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nenalezeny žádné záznamy"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class