Class _Pohlednice_Poslat
	Inherits System.Web.UI.Page

	Public iID As Integer
	Public MyUser As New Fog.User

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		iID = Val.ToInt(Request.QueryString("id"))
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case ""
				If Page.IsPostBack Then
					FormSubmit()
				Else
					Me.Form1.DataBind()
					FormInit()
				End If
			Case "SendOk"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Pohlednice odesl�na."
				Report.Status = Renderer.Report.Statusy.OK
				Report.Text = "Data budou ulo�ena po dobu 90dn�.<br/>"
				Report.Text &= "<i>Tip: M��ete se vr�tit <a href=""javascript:history.go(-1);"">zp�t</a> a poslat pohlednici n�komu dal��mu !</i>"
				Me.phReport.Controls.Add(Report.Render)
			Case "SendErr"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "CHYBA - nelze odeslat !!"
				Report.Text = "Zkontrolujte je�t� jednou zadan� emaily a dal�� parametry."
				Me.phReport.Controls.Add(Report.Render)
		End Select
	End Sub

	Sub FormSubmit()
		Dim MyIni As New Fog.Ini.Init
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim EmailFrom As String = Me.tbEmailFrom.Text.Trim
		Dim EmailTo As String = Me.tbEmailTo.Text.Trim
		Dim iPotvrzeni As Integer = IIf(Me.cbPotvrzeni.Checked And EmailFrom <> "", 1, 0)
		Dim Txt As String = Me.tbTxt.Text.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then
			err.Add("Zadejte sv� jm�no")
		ElseIf Jmeno.IndexOf("""") <> -1 Then
			err.Add("Jm�no obsahuje nepovolen� znak ("")")
		ElseIf Jmeno.Length > 50 Then
			err.Add("Maxim�ln� d�lka jm�na je 50 znak�")
		End If
		If EmailFrom <> "" And FN.Email.IsEmailValid(EmailFrom) = False Then
			err.Add("V� email nen� platn� emailov� adresa")
		End If
		If EmailTo = "" Then
			err.Add("Chyb� email adres�ta")
		ElseIf Not (FN.Email.IsEmailValid(EmailTo)) Then
			err.Add("Email adres�ta nen� platn� emailov� adresa")
		End If
		If Txt = "" Then
			err.Add("Chyb� Text")
		ElseIf Txt.Length > 6000 Then
			err.Add("Maxim�ln� d�lka textu je 6000 znak�")
		End If
		If Me.lblOchrana.Text.ToLower <> Me.tbOchrana.Text.Trim.ToLower Then err.Add("Nesouhlas� znaky pro ochranu")

		If err.Count > 0 Then
			Me.phErrors.Controls.Add(err.Render)
		Else
			Dim iUID As Integer = GenereteUID()
			Dim sUID As String = Right("000000000" & iUID, 9)
			sUID = sUID.Substring(0, 3) & "-" & sUID.Substring(3, 3) & "-" & sUID.Substring(6, 3)
			Dim sBody As String = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
			sBody &= "P�ejeme p��jemn� den,<br>" & vbCrLf
			sBody &= "�lov�k jm�nem """ & Jmeno & """ V�m pos�l� pohlednici ze serveru <a href=""http://prej.cz"">P�ej.cz</a> (www.prej.cz)<br><br>" & vbCrLf & vbCrLf
			sBody &= """" & sUID & """ je k�d va�� pohlednice.<br>" & vbCrLf
			sBody &= "Vyzvednout si ji m��ete na na�ich str�nk�ch. Pokud v� email podporuje html, m��ete kliknout na p��m� odkaz: <a href=""http://prej.cz/Pohlednice/read-" & iUID & ".aspx""><b>" & sUID & "</b></a><br><br>" & vbCrLf & vbCrLf
			sBody &= "__________________________________________________<br>" & vbCrLf & Reklama.Generate(601)
			sBody &= "</body></html>"
			Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, EmailTo, "Elektronick� pohlednice", sBody)
			msg.IsBodyHtml = True
			If FN.Email.SendMessage(msg) Then
				Dim SQL As String = "INSERT INTO PohledniceSend (SendUID,SendPohlednice,SendEmail,SendOdJmeno,SendOdEmail,SendPotvrzeni,SendTxt) Values (" & iUID & "," & iID & ",'" & EmailTo & "','" & Jmeno.Replace("'", "''") & "','" & EmailFrom & "'," & iPotvrzeni & ",'" & Txt.Replace("'", "''") & "')"
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				CMD.ExecuteNonQuery()
				If Not FN.Users.Prava.StatistikyOff Then
					CMD.CommandText = "UPDATE Pohlednice SET Odeslano=Odeslano+1 WHERE ID=" & iID
					CMD.ExecuteNonQuery()
					Dim sDatum As String = Now.ToString("yyyyMM01")
					CMD.CommandText = "UPDATE SendStat SET SendCount=SendCount+1 WHERE SendDay=" & sDatum & " AND SendWhat='Pohlednice' AND SendHow='Email'"
					If CMD.ExecuteNonQuery() = 0 Then
						CMD.CommandText = "INSERT INTO SendStat (SendDay, SendWhat, SendHow, SendCount) Values (" & sDatum & ", 'Pohlednice', 'Email', 1)"
						CMD.ExecuteNonQuery()
					End If
				End If
				CMD.Connection.Close()
				Response.Redirect("/Pohlednice_Poslat.aspx?akce=SendOk")
			Else
				Response.Redirect("/Pohlednice_Poslat.aspx?akce=SendErr")
			End If
		End If
	End Sub

	Sub FormInit()
		Me.tbJmeno.Text = MyUser.Nick
		Me.tbEmailFrom.Text = MyUser.Email
		Me.lblOchrana.Text = FN.Text.RandomString(3)
		'Me.tbOchrana.Attributes.Add("autocomplete", "off")
		KontaktyInit()
	End Sub

	Sub KontaktyInit()
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

	Function GenereteUID() As Integer
		Dim iUID As Integer
		Application.Lock()
		Dim i As Integer = Application("Pohlednice_Poslano")
		Application("Pohlednice_Poslano") += 1
		Application.UnLock()
		iUID = (Now.Month - 1) + (Now.Day - 1) * 12 + Now.Hour * 12 * 31 + Now.Minute * 12 * 31 * 24 + Now.Second * 12 * 31 * 24 * 60 + (i Mod 5) * 12 * 31 * 24 * 3600 + (Now.Millisecond Mod 5) * 12 * 31 * 24 * 3600 * 5
		Return (iUID Xor &H1786369)
	End Function

End Class