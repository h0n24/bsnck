Class _Seznamka_Detaily
	Inherits System.Web.UI.Page

	Public sDetaily, Txt As String
	Public iID As Integer
	Dim MyIni As New Fog.Ini.Init

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case ""
				If Page.IsPostBack Then
					FormPostBack()
				Else
					FormInit()
				End If
			Case "ok"
				Me.Form1.Visible = False
				Dim Report As New Renderer.Report
				Report.Title = "Vaše odpovìï byla uložena."
				Report.Text = "Pøejeme mnoho úspìchù pøi seznamování."
				Me.phReport.Controls.Add(Report.Render)
		End Select
	End Sub

	Sub FormInit()
		Dim SQL As String = "SELECT * FROM Seznamka WHERE ID=" & Val.ToInt(Request.QueryString("id"))
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.Read() Then
			iID = DR("ID")
			Txt = DR("Txt")
			sDetaily &= "<b>Jméno: </b>" & DR("Jmeno") & "<br/>"
			sDetaily &= "<b>Region: </b>" & GetSeznamkaXmlValue("Region", DR("Region")) & "<br/>"
			sDetaily &= "<b>Vìk: </b>" & DR("OsobaVek") & " let<br/>"
			sDetaily &= "<b>Dìti: </b>" & DR("OsobaDeti") & "<br/>"
			sDetaily &= "<b>Koníèky: </b>" & DR("OsobaKonicky") & "<br/>"
			sDetaily &= "<b>Vzdìlání: </b>" & GetSeznamkaXmlValue("Vzdelani", DR("OsobaVzdelani")) & "<br/>"
			sDetaily &= "<b>Znamení: </b>" & GetSeznamkaXmlValue("Znameni", DR("OsobaZnameni")) & "<br/>"
			sDetaily &= "<b>Postava: </b>" & GetSeznamkaXmlValue("Postava", DR("OsobaPostava")) & "<br/>"
			sDetaily &= "<b>Výška: </b>" & DR("OsobaVyska") & "<br/>"
			sDetaily &= "<b>Barva vlasù: </b>" & GetSeznamkaXmlValue("Vlasy", DR("OsobaVlasyBarva")) & "<br/>"
			sDetaily &= "<b>Kouøení: </b>" & GetSeznamkaXmlValue("Koureni", DR("OsobaKoureni")) & "<br/>"
			sDetaily &= "<b>Alkohol: </b>" & GetSeznamkaXmlValue("Alkohol", DR("OsobaAlkohol")) & "<br/>"
			sDetaily &= "<b>Èas podání: </b>" & DR("Datum") & "<br/>"
			Me.DataBind()
			Me.lblOchrana.Text = FN.Text.RandomString(3)
		Else
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Záznam nenalezen !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub FormPostBack()
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim Email As String = Me.tbEmail.Text.Trim
		Dim Txt As String = Me.tbTxt.Text.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then err.Add("Zadejte své jméno")
		If Email <> "" And Not (FN.Email.IsEmailValid(Email)) Then err.Add("Špatnì zadaný email")
		If Txt = "" Then err.Add("Chybí Text")
		If Me.lblOchrana.Text.ToLower <> Me.tbOchrana.Text.Trim.ToLower Then err.Add("Nesouhlasí znaky pro ochranu")
		If err.Count = 0 Then
			Dim SQL As String = "SELECT ID, Seznamka.UserID AS UserID, UserEmail, PoslatOdpovedi FROM Seznamka LEFT JOIN Users ON Seznamka.UserID=Users.UserID WHERE ID=0" & Request.QueryString("id")
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read() Then
				iID = DR("ID")
				Dim AdresatUser As Integer = DR("UserID")
				Dim AdresatEmail As String = "" & DR("UserEmail")
				Dim AdresatOdpovedi As Integer = DR("PoslatOdpovedi")
				DR.Close()
				If AdresatOdpovedi = 0 Or AdresatOdpovedi = 1 Then
					CMD.CommandText = "INSERT INTO SeznamkaOdpovedi (Inzerat, UserID, Jmeno, Email, Txt) Values (" & iID & ", " & AdresatUser & ", " & FN.DB.GetText(Jmeno) & ", '" & Email & "', " & FN.DB.GetText(Txt) & ")"
					CMD.ExecuteNonQuery()
				End If
				If (AdresatOdpovedi = 1 Or AdresatOdpovedi = 2) And AdresatEmail <> "" Then
					Dim sBody As String = "<html><head><title>" & MyIni.Web.Name & "</title></head><body>"
					If AdresatOdpovedi = 1 Then
						sBody &= "Pøejeme pøíjemný den,<br>" & vbCrLf
						sBody &= "na našem serveru máte uloženu odpovìï na váš seznamovací inzerát od èlovìka jménem """ & Jmeno & """.<br>" & vbCrLf
					ElseIf AdresatOdpovedi = 2 Then
						sBody &= "Pøejeme pøíjemný den,<br>" & vbCrLf
						sBody &= "èlovìk jménem """ & Server.HtmlEncode(Jmeno) & """ Vám odpovídá na seznamovací inzerát:<br>" & vbCrLf
						sBody &= "<i>" & Server.HtmlEncode(Txt).Replace(vbCrLf, "<br>" & vbCrLf) & "</i><br>" & vbCrLf
					End If
					sBody &= "<hr>Tento text byl zaslán ze serveru <a href=""" & MyIni.Web.URL & """>" & MyIni.Web.Name & "</a>. Na tento email neodpovídejte, byl zaslán automatem!<br>" & vbCrLf
					sBody &= "_____________________________________________<br>" & vbCrLf & Reklama.Generate(601)
					sBody &= "</body></html>"
					Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, AdresatEmail, "Odpoveï na inzerát", sBody)
					msg.IsBodyHtml = True
					FN.Email.SendMessage(msg)
					If Not FN.Users.Prava.StatistikyOff Then
						Dim sDatum As String = Now.ToString("yyyyMM01")
						CMD.CommandText = "UPDATE SendStat SET SendCount=SendCount+1 WHERE SendDay=" & sDatum & " AND SendWhat='SeznamkaOdpoved' AND SendHow='Email'"
						If CMD.ExecuteNonQuery() = 0 Then
							CMD.CommandText = "INSERT INTO SendStat (SendDay, SendWhat, SendHow, SendCount) Values (" & sDatum & ", 'SeznamkaOdpoved', 'Email', 1)"
							CMD.ExecuteNonQuery()
						End If
					End If
				End If
			Else
				DR.Close()
			End If
			CMD.Connection.Close()
			Response.Redirect("/Seznamka_Detaily.aspx?akce=ok")
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Public Shared Function GetSeznamkaXmlValue(ByVal Tabulka As String, ByVal Hodnota As Integer) As String
		Return FN.Cache.dsSeznamkaDataXml.Tables(Tabulka).Select("id='" & Hodnota & "'")(0)("txt")
	End Function

End Class