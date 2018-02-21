Class _Autori_Info
	Inherits System.Web.UI.Page
	Public SbirekCelkem As Int32
	Public SbirekNedokoneno As Int32
	Public UserID As Integer

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		UserID = Val.ToInt(Request.QueryString("autor"))
		Dim SQL As String = "SELECT AutorDatum,AutorNarozen,AutorInfo,UserNick FROM UsersAutori RIGHT OUTER JOIN Users On AutorUser=Users.UserID WHERE UserID=" & UserID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			Page.Title = "Podrobnosti autora " & DR("UserNick")
			If IsDBNull(DR("AutorDatum")) Then
				Me.phAutorInfo.Visible = False
				Me.divOblibeniAutori.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Uživatel '" & DR("UserNick") & "' není autorem."
				Me.phReport.Controls.Add(Report.Render)
			Else
				Me.linkAutorInfoDila.HRef = "/Autori/" & UserID & "-dila.aspx"
				Me.txtAutorInfoJmeno.InnerText = DR("UserNick")
				Me.txtAutorInfoText.InnerHtml = Server.HtmlEncode(DR("AutorInfo")).Replace(vbCrLf, "<br/>" & vbCrLf)
				Me.spanAutorInfoVek.InnerText = Int(DateDiff(DateInterval.Day, DR("AutorNarozen"), Now) / 365) & " let"
				Dim AutorDatum As Date = DR("AutorDatum")
				DR.Close()
				Me.spanAutorInfoDatum.InnerText = AutorDatum.ToString("dd.MM.yyyy") & " (" & DateDiff(DateInterval.Day, AutorDatum, Now) & " dnù)"
				SQL = "SELECT Pocet, " & UserID & " AS Autor, Sekce, SekceNazev FROM (" & vbCrLf
				Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceTable='TxtDila'")
				For Each Row As DataRow In dRows
					SQL &= "(SELECT Count(*) AS Pocet, '" & Row("SekceAlias") & "' AS Sekce, (Select SekceNazev FROM Sekce WHERE SekceAlias='" & Row("SekceAlias") & "') AS SekceNazev FROM TxtDila WHERE Autor=" & UserID & " AND Sekce='" & Row("SekceAlias") & "')" & vbCrLf
					SQL &= " UNION "
				Next
				SQL = Left(SQL, SQL.Length - " UNION ".Length)
				SQL &= ") AS SekceStatistika WHERE Pocet>0"
				CMD.CommandText = SQL
				DR = CMD.ExecuteReader
				Me.rptAutorDila.DataSource = DR
				Me.rptAutorDila.DataBind()
				DR.Close()
				CMD.CommandText = "SELECT (SELECT count(*) FROM Sbirky WHERE Autor=" & UserID & ") AS Celkem, (SELECT count(*) FROM Sbirky WHERE Autor=" & UserID & " AND Dokonceno IS NULL) AS Nedokonceno"
				DR = CMD.ExecuteReader
				DR.Read()
				SbirekCelkem = DR("Celkem")
				SbirekNedokoneno = DR("Nedokonceno")
				DR.Close()
			End If
		Else
			Me.phAutorInfo.Visible = False
			Me.divAkce.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "Uživatel neexistuje."
			Page.Title = "Uživatel neexistuje"
			Me.phReport.Controls.Add(Report.Render)
		End If
		CMD.Connection.Close()
	End Sub

End Class