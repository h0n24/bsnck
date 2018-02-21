Class _SendEmail
	Inherits System.Web.UI.Page

	Dim Akce As String
	Dim iID As Integer
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		iID = Val.ToInt(Request.QueryString("id"))
		Akce = Request.QueryString("akce")
		Select Case Akce
			Case ""
				If IsPostBack Then
					FormPostBack()
				Else
					FormInit()
				End If
			Case "SendOk"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Email odesl�n."
				Report.Text = "Pokud m�te webovou str�nku, dejte ostatn�m na v�dom�, �e pou��v�te n� web. P��klad HTML k�du:<br/>"
				Report.Text &= "<i>Doporu�uji &lt;a href=""" & MyIni.Web.URL & """&gt;" & MyIni.Web.Name & " - " & MyIni.Web.Description & "&lt;/a&gt;.</i>"
				Me.phReport.Controls.Add(Report.Render)
			Case "SendErr"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "CHYBA - nelze odeslat!!"
				Report.Text = "Zkontrolujte je�t� jednou zadan� emaily a dal�� parametry."
				Me.phReport.Controls.Add(Report.Render)
		End Select
	End Sub

	Sub FormPostBack()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim EmailFrom As String = Me.tbEmailFrom.Text.Trim
		Dim EmailTo As String = Me.tbEmailTo.Text.Trim
		Dim Txt As String = Me.tbTxt.Text.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then
			err.Add("Zadejte sv� jm�no")
		ElseIf Jmeno.IndexOf("""") <> -1 Then
			err.Add("Jm�no obsahuje nepovolen� znak ("")")
		End If
		If EmailTo = "" Then
			err.Add("Chyb� email adres�ta")
		ElseIf Not FN.Email.IsEmailValid(EmailTo) Then
			err.Add("Email adres�ta nen� platn� emailov� adresa")
		End If
		If Txt = "" Then err.Add("Chyb� Text")
		If err.Count = 0 Then
			Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, EmailTo)
			msg.Body = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
			msg.Body &= "P�ejeme p��jemn� den,<br>" & vbCrLf
			msg.Body &= "�lov�k jm�nem """ & Server.HtmlEncode(Jmeno) & """ V�m pos�l� " & Sekce.J4P & ":<br><br>" & vbCrLf & vbCrLf
			msg.Body &= "<i>" & Server.HtmlEncode(Txt).Replace(vbCrLf, "<br>" & vbCrLf) & "</i><br>" & vbCrLf
			msg.Body &= "<hr>Tento text byl zasl�n ze serveru <a href=""" & MyIni.Web.URL & """>" & MyIni.Web.Name & "</a> (" & MyIni.Web.Slogan & ").<br>" & vbCrLf
			msg.Body &= "_____________________________________________<br>" & vbCrLf & Reklama.Generate(601)
			msg.Body &= "</body></html>"
			msg.Subject = Sekce.J1P
			msg.IsBodyHtml = True
			If FN.Email.SendMessage(msg) Then
				If Not FN.Users.Prava.StatistikyOff Then
					Dim SQL As String = "UPDATE " & Sekce.Tabulka.Nazev & " SET Odeslano=Odeslano+1 WHERE ID=" & iID
					Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
					CMD.ExecuteNonQuery()
					CMD.CommandText = "UPDATE SendStat SET SendCount=SendCount+1 WHERE SendDay=" & Now.ToString("yyyyMMdd") & " AND SendWhat='" & Sekce.Alias & "' AND SendHow='Email'"
					If CMD.ExecuteNonQuery() = 0 Then
						CMD.CommandText = "INSERT INTO SendStat (SendDay, SendWhat, SendHow, SendCount) Values (" & Now.ToString("yyyyMMdd") & ", '" & Sekce.Alias & "', 'Email', 1)"
						CMD.ExecuteNonQuery()
					End If
					CMD.Connection.Close()
				End If
				Response.Redirect("/SendEmail.aspx?akce=SendOk&sekce=" & Sekce.Alias)
			Else
				Response.Redirect("/SendEmail.aspx?akce=SendErr&sekce=" & Sekce.Alias)
			End If
		Else
			VyberKontakty()
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Sub FormInit()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim Txt As String
		Me.tbJmeno.Text = MyUser.Nick
		Me.tbEmailFrom.Text = MyUser.Email
		If Not Sekce.Valid Then
			'-- testov�no kv�li n�jak�mu robotovi, kter� nepou��val parametr &sekce=
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Report.Text = "Z�znam neexistuje."
			Response.StatusCode = 400
			Response.StatusDescription = "Bad Request"
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		Dim SQL As String
		If Sekce.Tabulka.isTxtDila Then
			SQL = "SELECT UserNick,Titulek,Txt FROM TxtDila LEFT JOIN Users ON Autor=Users.UserID WHERE ID=" & iID
		ElseIf Sekce.Tabulka.hasTitulek Then
			SQL = "SELECT Titulek,Txt FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID
		ElseIf Sekce.Tabulka.isTxtCitaty Then
			SQL = "SELECT AutorJmeno,Txt FROM TxtCitaty LEFT JOIN TxtCitatyAutori ON Autor=AutorID WHERE ID=" & iID
		Else
			SQL = "SELECT Txt FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read Then
			If Sekce.Tabulka.isTxtDila Then
				Txt = DR("Titulek") & " [" & DR("UserNick") & "]"
				Txt &= vbCrLf & StrDup(Len(Txt), "-") & vbCrLf & SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias)
			ElseIf Sekce.Tabulka.hasTitulek Then
				Txt = DR("Titulek") & vbCrLf & StrDup(Len(DR("Titulek")), "-") & vbCrLf & SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias)
			ElseIf Sekce.Tabulka.isTxtCitaty Then
				Txt = "[" & DR("AutorJmeno") & "]" & vbCrLf & DR("Txt")
			Else
				Txt = SharedFunctions.TextFromFile.Read(DR("Txt"), Sekce.Alias)
			End If
			Me.tbTxt.Text = Txt
			VyberKontakty()
		Else
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Z�znam nenalezen !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub VyberKontakty()
		If MyUser.isLogged Then
			Dim SQL As String = "SELECT (KontaktJmeno + ' / ' + KontaktEmail) AS KontaktJmeno, KontaktEmail FROM UsersKontakty WHERE KontaktUser=" & MyUser.ID & " AND KontaktEmail<>''"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			Me.selKontakty.DataSource = DR
			Me.selKontakty.DataValueField = "KontaktEmail"
			Me.selKontakty.DataTextField = "KontaktJmeno"
			Me.selKontakty.DataBind()
			DR.Close()
			CMD.Connection.Close()
		Else
			Me.selKontakty.Disabled = True
		End If
		Me.selKontakty.Items.Insert(0, New ListItem("--- Va�e kontakty ---", ""))
	End Sub

End Class