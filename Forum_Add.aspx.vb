Class _Forum_Add
	Inherits System.Web.UI.Page

	Public IDx As Integer
	Public TemaID As Integer
	Public ParentUID As String
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If MyUser.isLogged = False Then
			Report.Title = "Nejsi p�ihl�en !!"
			Report.Text = "P�id�vat nov� p��sp�vky mohou pouze p�ihl�en� u�ivatel�. Nejprve se p�ihla�, p��padn� zaregistruj."
			ShowReport()
		Else
			IDx = Val.ToInt(Request.QueryString("id"))
			TemaID = Val.ToInt(Request.QueryString("tema"))
			ParentUID = Request.QueryString("reply")
			If Page.IsPostBack Then
				FormSubmit()
			Else
				FormInit()
			End If
		End If
	End Sub

	Sub FormInit()
		Dim CookieReply As String = FN.Cookies.Read("Forum", "Reply")
		If CookieReply <> "" Then Me.cbReply.Checked = (CookieReply = "1")
		If ParentUID <> "" Then
			Dim xDoc As System.Xml.XmlDocument = LoadForumXml()
			If xDoc Is Nothing Then
				Report.Title = "Z�znam nebyl nalezen !!"
				Report.Text = "Pokou��te se odpov�d�t na p��sp�vek, kter� v datab�zi neexistuje."
				ShowReport()
				Exit Sub
			Else
				Dim xA As System.Xml.XmlNode = xDoc.SelectSingleNode("//item[@uid='" & ParentUID & "']")
				If xA Is Nothing Then
					Report.Title = "Nad�azen� z�znam nebyl nalezen !!"
					Report.Text = "Pokou��te se odpov�d�t na p��sp�vek, kter� v datab�zi neexistuje."
					ShowReport()
					Exit Sub
				Else
					Me.tbSubject.Text = xA.Attributes("subject").Value
					If Not Me.tbSubject.Text.StartsWith("RE: ") Then Me.tbSubject.Text = "RE: " & Me.tbSubject.Text
				End If
			End If
		End If
	End Sub

	Sub FormSubmit()
		FN.Cookies.WriteKey("Forum", "Reply", IIf(Me.cbReply.Checked, "1", "0"), Now.AddDays(499))
		Dim xDoc As System.Xml.XmlDocument = LoadForumXml()
		If xDoc Is Nothing Then
			Report.Title = "Z�znam nebyl nalezen !!"
			Report.Text = "Pokou��te se odpov�d�t na p��sp�vek, kter� v datab�zi neexistuje."
			ShowReport()
			Exit Sub
		End If

		Dim Predmet As String = Me.tbSubject.Text.Trim
		Dim Txt As String = Me.tbText.Text.Trim
		'######### dod�lat �pravu textu - duplicity apod.!!!!
		Dim err As New Renderer.FormErrors
		If Predmet = "" Then
			err.Add("Chyb� P�edm�t")
		ElseIf Predmet.Length > 60 Then
			err.Add("Maxim�ln� d�lka p�edm�tu je 60 znak�")
		End If
		If Txt = "" Then
			err.Add("Chyb� Text")
		Else
			If Txt.Length > 900 Then err.Add("Maxim�ln� d�lka textu je 900 znak�")
			If FN.Text.Test.VelkaPismena(Txt) > 30 Then err.Add("Velk� p�smena pi�te pouze na za��tku v�ty nebo ve jm�nech")
		End If
		If err.Count > 0 Then
			Me.phErrors.Controls.Add(err.Render)
			Exit Sub
		End If

		Dim UID As String = System.Guid.NewGuid.ToString("N").ToLower
		Dim xA As System.Xml.XmlNode = xDoc.CreateElement("item")
		xA.Attributes.Append(xDoc.CreateAttribute("date")).Value = Now.ToString("yyyyMMddHHmmss")
		xA.Attributes.Append(xDoc.CreateAttribute("uid")).Value = UID
		xA.Attributes.Append(xDoc.CreateAttribute("subject")).Value = Predmet
		xA.AppendChild(xDoc.CreateElement("text")).InnerText = Txt
		Dim xB As System.Xml.XmlNode = xA.AppendChild(xDoc.CreateElement("from"))
		xB.Attributes.Append(xDoc.CreateAttribute("user")).Value = MyUser.ID
		xB.Attributes.Append(xDoc.CreateAttribute("name")).Value = MyUser.Nick
		If Me.cbReply.Checked Then
			xB.Attributes.Append(xDoc.CreateAttribute("email")).Value = MyUser.Email
		End If

		If ParentUID = "" Then
			xDoc.DocumentElement.AppendChild(xA)
		Else
			Dim xC As System.Xml.XmlNode = xDoc.SelectSingleNode("//item[@uid='" & ParentUID & "']")
			If xC Is Nothing Then
				Report.Title = "Nad�azen� z�znam nebyl nalezen !!"
				Report.Text = "Pokou��te se odpov�d�t na p��sp�vek, kter� v datab�zi neexistuje."
				ShowReport()
				Exit Sub
			Else
				xC.PrependChild(xA)
				Dim xFrom As System.Xml.XmlNode = xC.SelectSingleNode("from")
				If Not xFrom.Attributes("email") Is Nothing Then
					Dim ParentEmail As String = xFrom.Attributes("email").Value
					Dim ParentDatum As Date = DateTime.ParseExact(xC.Attributes("date").Value, "yyyyMMddHHmmss", Nothing)
					Dim ParentPredmet As String = xC.Attributes("subject").Value
					Dim SB As New System.Text.StringBuilder
					SB.Append("Dobr� den," & vbCrLf)
					SB.Append("k va�emu p��sp�vku """ & ParentPredmet & """ z " & ParentDatum.ToString("d.M.yy HH:mm") & " byla naps�na odpov��." & vbCrLf)
					SB.Append("��st:      http://" & Request.Url.Host & "/Forum.aspx?id=" & IDx & "#" & UID & vbCrLf)
					SB.Append("Odpov�d�t: http://" & Request.Url.Host & "/Forum_Add.aspx?id=" & IDx & "&reply=" & UID & vbCrLf)
					SB.Append("P�edm�t:   " & Predmet & vbCrLf)
					SB.Append("Autor:     " & MyUser.Nick & vbCrLf)
					SB.Append("Text:      " & Txt)
					Dim msg As New System.Net.Mail.MailMessage(MyIni.Web.EmailAutomat, ParentEmail, "Odpove� na v� p��sp�vek: " & xC.Attributes("subject").Value, SB.ToString)
					FN.Email.SendMessage(msg)
				End If
			End If
		End If

		Dim SQL As String
		Dim sRedir As String
		If ParentUID = "" Then
			SQL = "INSERT INTO Forum (Predmet,Pocet,Tema,Jmeno,Xml) VALUES (" & FN.DB.GetText(Predmet) & ", 1, " & TemaID & ", " & FN.DB.GetText(MyUser.Nick) & ", " & FN.DB.GetText(xDoc.OuterXml) & ")"
			sRedir = "/Forum.aspx?tema=" & TemaID
		Else
			Dim Pocet As Integer = xDoc.SelectNodes("//item").Count
			SQL = "UPDATE Forum SET Pocet=" & Pocet & ",  Posledni=" & FN.DB.GetDateTime(Now) & ", Xml=" & FN.DB.GetText(xDoc.OuterXml) & " WHERE ID=" & IDx
			sRedir = "/Forum.aspx?id=" & IDx & "&#" & UID
		End If
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		CMD.ExecuteNonQuery()
		CMD.Connection.Close()
		Response.Redirect(sRedir)
	End Sub

	Function LoadForumXml() As System.Xml.XmlDocument
		If ParentUID <> "" Then
			Dim dt As DataTable = New DataTable("dt")
			Dim DA As New System.Data.SqlClient.SqlDataAdapter("SELECT TOP 1 Xml FROM Forum WHERE ID=" & IDx, FN.DB.GetConnectionString)
			DA.Fill(dt)
			DA.Dispose()
			If dt.Rows.Count > 0 Then
				Dim xDoc As New System.Xml.XmlDocument
				xDoc.LoadXml(dt.Rows(0)("Xml"))
				Return xDoc
			Else
				Return Nothing
			End If
		Else
			Dim xDoc As New System.Xml.XmlDocument
			xDoc.LoadXml("<root />")
			Return xDoc
		End If
	End Function

	Sub ShowReport()
		Me.Form1.Visible = False
		Report.Status = Renderer.Report.Statusy.Err
		Me.phReport.Controls.Add(Report.Render)
	End Sub

End Class