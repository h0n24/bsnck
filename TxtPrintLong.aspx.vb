Class _TxtPrintLong
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim IDx As Integer = Val.ToInt(Request.QueryString("id"))
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim SQL As String
		If Sekce.Tabulka.isTxtDila Then
			SQL = "SELECT ID,Datum,Sekce,UserNick AS Poslal,Titulek,Txt FROM " & Sekce.Tabulka.Nazev & " LEFT JOIN Users ON Autor=Users.UserID WHERE ID=" & IDx
		Else
			SQL = "SELECT ID,Datum,Sekce,Poslal,Titulek,Txt FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & IDx
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			Me.txtPrintTitulek.InnerText = DR("Titulek")
			Page.Title = DR("Titulek")
			Me.txtPrintAutor.InnerText = DR("Poslal")
			Me.txtPrintDatum.InnerText = ", " & CType(DR("Datum"), Date).ToString("d.M.yyyy")
			Me.txtPrintSekce.InnerText = ", " & Sekce.Nazev
			Dim sTxt As String = Server.HtmlEncode(SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias))
			Me.txtPrintText.InnerHtml = Replace(sTxt, vbCrLf, "<br/>" & vbCrLf)
			DR.Close()
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Page.Title = "CHYBA !!"
			Report.Title = "CHYBA !!"
			Report.Text = "Záznam v databázi neexistuje, pravdìpodobnì byl smazán."
			Me.phReport.Controls.Add(Report.Render)
		End If
		CMD.Connection.Close()
	End Sub

End Class