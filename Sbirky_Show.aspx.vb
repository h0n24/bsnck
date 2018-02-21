Partial Class Sbirky_Show
	Inherits System.Web.UI.Page
	Dim ObsahPoradi As Short

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Sbirka As UInt64 = Val.ToInt(Request.QueryString("sbirka"))
		Dim SQL As String = "SELECT Datum,Dokonceno,Kat,Titulek,Autor,Prolog,UserNick,KatNazev, (SELECT count(*) FROM SbirkyKomentare WHERE KomentDBID = ID) AS Koment" & _
		",(SELECT SUM(TipHodnota) FROM SbirkyTipy WHERE TipDBID=ID) AS TipySuma " & _
		" FROM Sbirky LEFT JOIN Users On Autor=UserID LEFT JOIN Kategorie ON Kat=KatID WHERE ID=" & Sbirka
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			Page.Title = DR("Titulek")
			txtProlog.InnerHtml = Server.HtmlEncode(DR("Prolog")).Replace(vbCrLf, "<br/>")
			txtTitulek.InnerText = DR("Titulek")
			hlKategorie.Text = "Sbírky » " & DR("KatNazev")
			hlKategorie.NavigateUrl = String.Format(hlKategorie.NavigateUrl, DR("Kat"))
			hlAutor.Text = Server.HtmlEncode(DR("UserNick"))
			hlAutor.NavigateUrl = String.Format(hlAutor.NavigateUrl, DR("Autor"))
			lblDatum.Text = " (" & CType(DR("Datum"), Date).ToShortDateString & " - " & ZobrazitDokonceno(DR("Dokonceno")) & ")"
			litKomentare.Text = DR("Koment")
			hlKomentare.NavigateUrl = String.Format(hlKomentare.NavigateUrl, Sbirka)
			litTipy.Text = Val.ToInt(DR("TipySuma"))
			hlTip.NavigateUrl = String.Format(hlTip.NavigateUrl, Sbirka)
			hlTip2.NavigateUrl = String.Format(hlTip2.NavigateUrl, Sbirka)
			DR.Close()
			CMD.CommandText = "SELECT ObsahDilo,ObsahPoradi,TxtDila.Sekce,TxtDila.Titulek FROM SbirkyObsah" & _
			" INNER JOIN TxtDila ON ObsahDilo=TxtDila.ID WHERE ObsahSbirka=" & Sbirka & " ORDER BY ObsahPoradi"
			DR = CMD.ExecuteReader
			rptObsah.DataSource = DR
			rptObsah.DataBind()
			DR.Close()
			CMD.CommandText = "SELECT TOP 10 UserNick FROM SbirkyTipy LEFT JOIN Users ON TipUser=UserID WHERE TipDBID=" & Sbirka & " ORDER BY TipID DESC"
			DR = CMD.ExecuteReader
			If DR.HasRows Then
				While DR.Read
					Me.lblTipujici.Text &= Server.HtmlEncode(DR("UserNick")) & ", "
				End While
				Me.lblTipujici.Text = Me.lblTipujici.Text.Substring(0, Me.lblTipujici.Text.Length - 2)
			Else
				Me.pnlTipujici.Visible = False
			End If
		Else
			Page.Title = "Sbírka neexistuje"
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Sbírka neexistuje."
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Public Function ZobrazitDokonceno(ByVal Dokonceno As Object) As String
		If IsDBNull(Dokonceno) Then
			Return "rozpracováno"
		Else
			Return CType(Dokonceno, Date).ToShortDateString
		End If
	End Function

	Public Function OdkazText(ByVal Titulek As String) As String
		ObsahPoradi += 1
		Return ObsahPoradi & ". " & Titulek
	End Function

End Class