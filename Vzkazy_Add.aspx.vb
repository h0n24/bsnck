Class _Vzkazy_Add
	Inherits System.Web.UI.Page

	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Report As New Renderer.Report

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim Report As New Renderer.Report
		If MyUser.ID = 0 Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Tato funkce vyžaduje pøihlášeného uživatele !!!"
			Me.phReport.Controls.Add(Report.Render)
			Me.Form1.Visible = False
		Else
			Dim Akce As String = Request.QueryString("akce")
			If Akce = "ok" Then
				Me.Form1.Visible = False
				Report.Title = "Vzkaz byl uložen do databáze"
				Me.phReport.Controls.Add(Report.Render)
			End If
			If Page.IsPostBack Then
				FormSubmit()
			Else
				FormInit()
			End If
		End If
	End Sub

	Sub FormInit()
		If Not IsNothing(Request.QueryString("komu")) Then Me.divNick.Visible = False
		If Not IsNothing(Request.QueryString("subj")) Then Me.tbPredmet.Text = Request.QueryString("subj")
		If Not IsNothing(Request.QueryString("reply")) Then
			Me.divNick.Visible = False
			Dim IDx As Int64 = Val.ToInt(Request.QueryString("reply"))
			Dim SQL As String = "SELECT VzkazDatum,VzkazOd,VzkazKomu,VzkazPredmet,VzkazTxt,Users.UserNick AS OdNick FROM Vzkazy LEFT JOIN Users ON VzkazOd=UserID WHERE VzkazID=" & IDx & " AND VzkazKomu=" & MyUser.ID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read Then
				Me.tbNick.Text = DR("VzkazOd")
				Me.tbPredmet.Text = PredmetRE(DR("VzkazPredmet"))
				Dim Txt As String = DR("VzkazTxt")
				Dim i As Int16 = Txt.IndexOf("> ------------ Pùvodní zpráva ------------")
				If i <> -1 Then
					Txt = Txt.Substring(0, i)
				End If
				Me.tbTxt.Text &= vbCrLf & vbCrLf & "> ------------ Pùvodní zpráva ------------" & vbCrLf
				Me.tbTxt.Text &= "> Autor: " & DR("OdNick") & vbCrLf & "> Èas: " & DR("VzkazDatum") & vbCrLf
				If Txt.EndsWith(vbCrLf) Then Txt = Txt.Substring(0, Txt.Length - 2)
				Me.tbTxt.Text &= "> ----------------------------------------" & vbCrLf & "> " & Txt.Replace(vbCrLf, vbCrLf & "> ")
			End If
			DR.Close()
			CMD.Connection.Close()
		End If
	End Sub

	Sub FormSubmit()
		Dim Txt As String = Me.tbTxt.Text.Trim
		Dim Predmet As String = Me.tbPredmet.Text.Trim
		Dim err As New Renderer.FormErrors
		If Predmet = "" Then err.Add("Zadejte pøedmìt vzkazu")
		If Predmet.Length > 100 Then err.Add("Délka pøedmìtu je omezena na 100 znakù")
		If Txt = "" Then err.Add("Zadejte text vzkazu")
		If Txt.Length > 7500 Then err.Add("Délka vzkazu je omezena na 7500 znakù")
		Dim Adresat As Int64
		If Not IsNothing(Request.QueryString("komu")) Then
			Adresat = Val.ToInt(Request.QueryString("komu"))
		ElseIf Me.tbNick.Visible = False Then
			Adresat = Val.ToInt(Me.tbNick.Text)
		Else
			Dim UserNick As String = Me.tbNick.Text.Trim
			If UserNick <> "" Then
				Dim SQL As String = "SELECT UserID FROM Users WHERE UserNick=" & FN.DB.GetText(UserNick)
				Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
				Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
				If DR.Read Then
					Adresat = DR(0)
				End If
				DR.Close()
				CMD.Connection.Close()
			End If
		End If
		If Adresat = 0 Then err.Add("Nenalezen pøíjemce vzkazu")
		If err.Count = 0 Then
			Dim SQL As String = "SELECT Count(*) FROM Vzkazy WHERE VzkazOd=" & MyUser.ID & " AND VzkazDatum>" & FN.DB.GetDateTime(Now.AddSeconds(-70))
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			DR.Read()
			Dim Pocet As Integer = DR(0)
			DR.Close()
			If Pocet > 3 Then
				CMD.Connection.Close()
				err.Add("Poèet vzkazù je omezen na 3 za minutu")
				Me.phErrors.Controls.Add(err.Render)
			Else
				CMD.CommandText = "INSERT INTO Vzkazy (VzkazOd,VzkazKomu,VzkazPredmet,VzkazTxt) Values (" & MyUser.ID & "," & Adresat & "," & FN.DB.GetText(Predmet) & "," & FN.DB.GetText(Txt) & ")"
				CMD.ExecuteNonQuery()
				CMD.Connection.Close()
				SharedFunctions.VzkazyDeleteCookies()
				Response.Redirect("Vzkazy_Add.aspx?akce=ok")
			End If
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Function PredmetRE(ByVal Predmet As String) As String
		If Not Predmet.StartsWith("RE: ") Then Predmet = "RE: " & Predmet
		Return Predmet
	End Function

End Class