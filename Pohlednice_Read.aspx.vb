Class _Pohlednice_Read
	Inherits System.Web.UI.Page

	Public sUID As String
	Public sDatum, sFormat, sSoubor, sRozmery, sTxt As String
	Public dt As New DataTable("Pohlednice")
	Public Datum As Date
	Dim MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		sUID = Request.Form("UID") & Request.QueryString("uid")
		If sUID <> "" Then
			PohledniceVyzvedni()
		Else
			Me.pnlPohledniceReadUid.Visible = True
			Me.pPohledniceReadUidErr.Visible = False
		End If
	End Sub

	Sub PohledniceVyzvedni()
		Dim sError As String
		Dim iUID As Integer
		Try
			iUID = CInt("0" & sUID.Replace("-", "").Replace(" ", "").Replace("""", ""))
		Catch
			sError = "V èísle pohlednice jsou nepovolené znaky !!"
		End Try
		If sError = "" Then
			Dim sSQL As String = "SELECT SendDatum,SendPohlednice,SendEmail,SendOdJmeno,SendOdEmail,SendPotvrzeni,SendTxt,Soubor,Format,Rozmery FROM PohledniceSend INNER JOIN Pohlednice ON SendPohlednice=ID WHERE SendUID=" & iUID & ""
			Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
			Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(sSQL, DBConn)
			DBDA.Fill(dt)
			DBDA.Dispose()
			If dt.Rows.Count > 0 Then
				Datum = dt.Rows(0)("SendDatum")
				sDatum = Datum.ToString("d.M.")
				sRozmery = dt.Rows(0)("Rozmery")
				sFormat = dt.Rows(0)("Format")
				sSoubor = dt.Rows(0)("Soubor")
				sTxt = dt.Rows(0)("SendTxt")
				Me.pnlPohledniceRead.Visible = True
				Me.pnlPohledniceRead.DataBind()
				If dt.Rows(0)("SendPotvrzeni") = 1 Then
					Dim sBody As String = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
					sBody &= "Vaše pohlednice byla pøeètena !<br>" & vbCrLf
					sBody &= "Pøíjemce: " & dt.Rows(0)("SendEmail") & "<br>" & vbCrLf
					sBody &= "Odesláno: " & Datum.ToString("dd.MM.yyyy HH:mm") & "<br>" & vbCrLf
					sBody &= "Pøeèteno: " & Now.ToString("dd.MM.yyyy HH:mm") & "<br>" & vbCrLf & "<br>" & vbCrLf
					sBody &= "<hr>Odesláno ze serveru <a href=""http://prej.cz"">Pøej.cz</a> (www.prej.cz)<br>" & vbCrLf
					sBody &= "__________________________________________________<br>" & vbCrLf & Reklama.Generate(601)
					sBody &= "</body></html>"
					Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, dt.Rows(0)("SendOdEmail"), "Pohlednice vyzvednuta", sBody)
					msg.IsBodyHtml = True
					FN.Email.SendMessage(msg)
				End If
				sSQL = "UPDATE PohledniceSend SET SendPrecteni=" & FN.DB.GetDateTime(Now) & ", SendPotvrzeni=2 WHERE SendUID=" & iUID
				Dim DBCmd As New System.Data.SqlClient.SqlCommand(sSQL, DBConn)
				DBCmd.ExecuteNonQuery()
			Else
				sError = "Zadaná pohlednice neexistuje !!"
			End If
			DBConn.Close()
		End If
		If sError <> "" Then
			sUID = Request.Form("UID") & Request.QueryString("uid")
			Me.pnlPohledniceReadUid.DataBind()
			Me.pnlPohledniceReadUid.Visible = True
			Me.pPohledniceReadUidErr.Visible = True
			Me.pPohledniceReadUidErr.InnerText = sError
		End If
	End Sub

End Class