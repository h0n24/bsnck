Class Admin_DB_Udrzba
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Val.ToBoolean(FN.Cookies.Read("Web", "Admin_DBUdrzbaNemazat")) = True Then
			Me.form1.Attributes.Add("style", "background-color:#ee2222")
		End If
		If Page.IsPostBack Then
			FN.Cookies.WriteKeyWeb("Admin_DBUdrzbaNemazat", Me.chbNemazat.Checked)
			Response.Redirect(Request.Url.AbsolutePath)
		Else
			Me.chbNemazat.Checked = Val.ToBoolean(FN.Cookies.Read("Web", "Admin_DBUdrzbaNemazat"))
			Dim Akce As String = Request.QueryString("akce")
			Select Case Akce
				Case "go"
					'AkceSeznamka()
					AkcePohlednice()
					AkceSmazaneTexty()
					AkceVzkazy()
				Case "Seznamka" : AkceSeznamka()
				Case "Pohlednice" : AkcePohlednice()
				Case "SmazaneTexty" : AkceSmazaneTexty()
				Case "Komentare" : AkceKomentare()
				Case "Vzkazy" : AkceVzkazy()
				Case "AdminEditHistory" : AkceAdminEditHistory()
				Case "Tipy" : AkceTipy()
				Case "SbirkyObsah" : AkceSbirkyObsah()
			End Select
		End If
	End Sub

	Sub AkceSeznamka()
		Promazat("SeznamkaOdpovedi", "starší 60 dní", " WHERE Datum < " & FN.DB.GetDateTime(Now.AddDays(-60)))
		Promazat("Seznamka", "starší 30 dní", " WHERE Datum < " & FN.DB.GetDateTime(Now.AddDays(-30)))
	End Sub
	Sub AkcePohlednice()
		Promazat("PohledniceSend", "starší 90 dní", " WHERE SendDatum < " & FN.DB.GetDateTime(Now.AddDays(-90)))
		Promazat("PohledniceSend", "10 dní po pøeètení", " WHERE SendPrecteni < " & FN.DB.GetDateTime(Now.AddDays(-10)))
	End Sub
	Sub AkceSmazaneTexty()
		Promazat("TxtDila", "smazané", " WHERE Kat IN (SELECT KatID FROM Kategorie WHERE KatFunkce=2)")
		Promazat("TxtLong", "smazané", " WHERE Kat IN (SELECT KatID FROM Kategorie WHERE KatFunkce=2)")
		Promazat("TxtShort", "smazané", " WHERE Kat IN (SELECT KatID FROM Kategorie WHERE KatFunkce=2)")
		Promazat("TxtCitaty", "smazané", " WHERE Kat IN (SELECT KatID FROM Kategorie WHERE KatFunkce=2)")
	End Sub
	Sub AkceVzkazy()
		Promazat("Vzkazy", "starší 6 mìsícù", " WHERE VzkazDatum < " & FN.DB.GetDateTime(Now.AddMonths(-6)))
	End Sub
	Sub AkceAdminEditHistory()
		Promazat("AdminEditHistory", "smazané nadøazené v TxtDila", " WHERE HistoryDBID NOT IN (SELECT ID FROM TxtDila) AND HistorySekce IN (SELECT SekceAlias FROM Sekce WHERE SekceTable='TxtDila')")
		Promazat("AdminEditHistory", "smazané nadøazené v TxtLong", " WHERE HistoryDBID NOT IN (SELECT ID FROM TxtLong) AND HistorySekce IN (SELECT SekceAlias FROM Sekce WHERE SekceTable='TxtLong')")
		Promazat("AdminEditHistory", "smazané nadøazené v TxtShort", " WHERE HistoryDBID NOT IN (SELECT ID FROM TxtShort) AND HistorySekce IN (SELECT SekceAlias FROM Sekce WHERE SekceTable='TxtShort')")
		Promazat("AdminEditHistory", "smazané nadøazené v TxtCitaty", " WHERE HistoryDBID NOT IN (SELECT ID FROM TxtCitaty) AND HistorySekce IN (SELECT SekceAlias FROM Sekce WHERE SekceTable='TxtCitaty')")
	End Sub
	Sub AkceKomentare()
		Promazat("TxtDilaKomentare", "smazané nadøazené v TxtDila", " WHERE KomentDBID NOT IN (SELECT ID FROM TxtDila)")
		Promazat("TxtLongKomentare", "smazané nadøazené v TxtLong", " WHERE KomentDBID NOT IN (SELECT ID FROM TxtLong)")
		Promazat("SbirkyKomentare", "smazané nadøazené v Sbirky", " WHERE KomentDBID NOT IN (SELECT ID FROM Sbirky)")
	End Sub
	Sub AkceTipy()
		Promazat("TxtDilaTipy", "smazané nadøazené v TxtDila", " WHERE TipDBID NOT IN (SELECT ID FROM TxtDila)")
		Promazat("SbirkyTipy", "smazané nadøazené v Sbirky", " WHERE TipDBID NOT IN (SELECT ID FROM Sbirky)")
	End Sub
	Sub AkceSbirkyObsah()
		Promazat("SbirkyObsah", "smazané nadøazené v Sbirky", " WHERE ObsahSbirka NOT IN (SELECT ID FROM Sbirky)")
	End Sub

	Sub Promazat(ByVal Tabulka As String, ByVal Popis As String, ByVal Where As String)
		Dim Pocet, Celkem As Integer
		Dim SQL As String = "SELECT (SELECT count(*) FROM " & Tabulka & Where & ") AS Pocet, (SELECT count(*) FROM " & Tabulka & ") AS Celkem"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		DR.Read()
		Pocet = DR("Pocet")
		Celkem = DR("Celkem")
		DR.Close()
		If Val.ToBoolean(FN.Cookies.Read("Web", "Admin_DBUdrzbaNemazat")) = False Then
			CMD.CommandText = "DELETE FROM " & Tabulka & Where
			CMD.ExecuteNonQuery()
		End If
		CMD.Connection.Close()
		LogZapis(DateTime.Now.ToString("yyyyMMdd hh:mm:ss") & " | " & Tabulka & " | Delete | " & Pocet & "/" & Celkem & " | " & Popis)
	End Sub

	Sub LogZapis(ByRef s As String)
		Me.pDBUdrazbaReport.InnerHtml &= s & "<br/>"
		If Val.ToBoolean(FN.Cookies.Read("Web", "Admin_DBUdrzbaNemazat")) = False Then
			Dim SW As IO.StreamWriter = IO.File.AppendText(Fog.Ini.PhysicalPaths.Data & "\Log\UdrzbaDB.txt")
			SW.WriteLine(s)
			SW.Flush()
			SW.Close()
		End If
	End Sub

End Class