Class _Citaty_Autori
	Inherits System.Web.UI.Page

	Public PocetAutoru As Integer
	Public dt As New DataTable

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case ""
				ShowSeznamAutoru()
			Case "info"
				ShowAutorInfo()
		End Select
	End Sub

	Sub ShowSeznamAutoru()
		Me.pnlAutoriSeznam.Visible = True
		Dim SQL As String = "SELECT AutorID,AutorJmeno,count(*) AS Pocet FROM TxtCitatyAutori INNER JOIN TxtCitaty ON Autor=AutorID GROUP BY AutorJmeno,AutorID ORDER BY AutorJmeno"
		Dim Cmd As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = Cmd.ExecuteReader()
		DR.Read()
		PocetAutoru = DR("Pocet")
		Me.rptAutoriSeznam.DataSource = DR
		Me.pnlAutoriSeznam.DataBind()
		DR.Close()
		Cmd.Connection.Close()
	End Sub

	Sub ShowAutorInfo()
		Dim AutorID = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "SELECT AutorID,AutorJmeno,AutorZivot,AutorPopis,AutorVyznam FROM TxtCitatyAutori WHERE AutorID=" & AutorID
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		DA.Fill(dt)
		DB.Close()
		If dt.Rows.Count = 1 Then
			Me.pnlAutorInfo.Visible = True
			Me.pnlAutorInfo.DataBind()
		Else
			Dim Report As New Renderer.Report
			Report.Title = "Autor neexistuje !!"
			Report.Status = Renderer.Report.Statusy.Err
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

End Class