Class _Seznamka_Add
	Inherits System.Web.UI.Page
	Dim MyUser As New Fog.User

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		If MyUser.isLogged Then
			Select Case Akce
				Case ""
					If IsPostBack Then
						FormPostBack()
					Else
						FormInit()
					End If
				Case "Delete"
					Delete()
				Case "AddOk"
					Me.Form1.Visible = False
					Dim Report As New Renderer.Report
					Report.Title = "Inzerát úspìšnì uložen."
					Report.Text = "Dìkujeme za váš inzerát a pøejeme hodnì úspìchù pøi seznamování."
					Me.phReport.Controls.Add(Report.Render)
			End Select
		Else
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Nejsi pøihlášen !!"
			Report.Text = "Musíte se zaregistrovat, nebo pøihlásit. Inzeráty nelze pøidávat bez registrace."
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Sub FormInit()
		Dim SQL As String = "SELECT RubID,RubNazev FROM SeznamkaRubriky ORDER BY RubID"
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim dt As DataTable = New DataTable("Rubriky")
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		DA.Fill(dt)
		DA.Dispose()
		DB.Close()
		Dim dRow As DataRow = dt.NewRow
		dRow("RubID") = 0
		dRow("RubNazev") = "- - - vyberte rubriku - - -"
		dt.Rows.InsertAt(dRow, 0)
		Me.lbRubrika.DataSource = dt
		Me.lbRubrika.DataValueField = "RubID"
		Me.lbRubrika.DataTextField = "RubNazev"
		Dim ds As DataSet = FN.Cache.dsSeznamkaDataXml
		Me.lbRegion.DataSource = ds.Tables("Region")
		Me.lbRegion.DataValueField = "id"
		Me.lbRegion.DataTextField = "txt"
		Me.lbVzdelani.DataSource = ds.Tables("Vzdelani")
		Me.lbVzdelani.DataValueField = "id"
		Me.lbVzdelani.DataTextField = "txt"
		Me.lbZnameni.DataSource = ds.Tables("Znameni")
		Me.lbZnameni.DataValueField = "id"
		Me.lbZnameni.DataTextField = "txt"
		Me.lbPostava.DataSource = ds.Tables("Postava")
		Me.lbPostava.DataValueField = "id"
		Me.lbPostava.DataTextField = "txt"
		Me.lbVlasy.DataSource = ds.Tables("Vlasy")
		Me.lbVlasy.DataValueField = "id"
		Me.lbVlasy.DataTextField = "txt"
		Me.lbKoureni.DataSource = ds.Tables("Koureni")
		Me.lbKoureni.DataValueField = "id"
		Me.lbKoureni.DataTextField = "txt"
		Me.lbAlkohol.DataSource = ds.Tables("Alkohol")
		Me.lbAlkohol.DataValueField = "id"
		Me.lbAlkohol.DataTextField = "txt"
		Me.DataBind()
	End Sub

	Sub FormPostBack()
		Dim Rubrika As Integer = Me.lbRubrika.SelectedValue
		Dim Region As Integer = Me.lbRegion.SelectedValue
		Dim Jmeno As String = Me.tbJmeno.Text.Trim
		Dim Vek As Integer = Val.ToInt(Me.tbVek.Text)
		Dim Vyska As Integer = Val.ToInt(Me.tbVyska.Text)
		Dim Deti As Integer = Val.ToInt(Me.tbDeti.Text)
		Dim Vzdelani As Integer = Me.lbVzdelani.SelectedValue
		Dim Znameni As Integer = Me.lbZnameni.SelectedValue
		Dim Postava As Integer = Me.lbPostava.SelectedValue
		Dim Vlasy As Integer = Me.lbVlasy.SelectedValue
		Dim Koureni As Integer = Me.lbKoureni.SelectedValue
		Dim Alkohol As Integer = Me.lbAlkohol.SelectedValue
		Dim Konicky As String = Me.tbKonicky.Text.Trim
		Dim Txt As String = Me.tbTxt.Text.Trim
		Dim Odpovedi
		If Me.rbOdpovedi1.Checked Then Odpovedi = 0
		If Me.rbOdpovedi2.Checked Then Odpovedi = 1
		'If Me.rbOdpovedi3.Checked Then Odpovedi = 2
		Dim err As New Renderer.FormErrors
		If Rubrika = 0 Then err.Add("Vyberte rubriku")
		If Jmeno = "" Then err.Add("Zadejte své jméno")
		If Vek = 0 Then
			err.Add("Zadajte svùj Vìk")
		ElseIf Vek < 7 Or Vek > 90 Then
			err.Add("Vìk je špatnì zadaný")
		End If
		If Me.tbVyska.Text <> "" AndAlso Vyska < 50 Or Vyska > 250 Then
			err.Add("Výška je pravdìpodobnì špatnì zadaná")
		End If
		If Konicky.Length > 500 Then err.Add("Koníèky mohou obsahovat maximálnì 500 znakù")
		If Txt = "" Then
			err.Add("Chybí Text")
		Else
			If Txt.Length > 600 Then err.Add("Koníèky mohou obsahovat maximálnì 500 znakù")
			If FN.Text.Test.Diakritika(Txt) < 2 Then err.Add("používejte diakritiku (ìšèøžýáíéù) - texty bez diakritiky nepøidáváme")
			If FN.Text.Test.VelkaPismena(Txt) > 30 Then err.Add("Velká písmena pište pouze na zaèátku vìty nebo ve jménech")
			If ContainsTelefon(Txt) Then err.Add("Není povoleno zadávat telefonní èísla")
			If ContainsEmail(Txt) Then err.Add("Není povoleno zadávat email")
		End If
		If err.Count = 0 Then
			Do While InStr(Txt, "  ") > 0
				Txt = Replace(Txt, "  ", " ")
			Loop
			Txt = Replace(Txt, " " & vbCrLf, vbCrLf)
			Do While InStr(Txt, vbCrLf & vbCrLf & vbCrLf) > 0
				Txt = Replace(Txt, vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
			Loop
			Do While Right(Txt, 4) = Right(Txt, 1) & Right(Txt, 1) & Right(Txt, 1) & Right(Txt, 1)
				Txt = Left(Txt, Len(Txt) - 1)
			Loop
			Dim VyskaSql As String = IIf(Me.tbVyska.Text.Trim = Vyska.ToString, Vyska, "NULL")
			Dim DetiSql As String = IIf(Me.tbDeti.Text.Trim = Deti.ToString, Deti, "NULL")
			Dim SQL As String = "INSERT INTO Seznamka (UserID,Jmeno,IP,Rubrika,Region,PoslatOdpovedi,OsobaVek,OsobaZnameni,OsobaVzdelani,OsobaPostava,OsobaVyska,OsobaVlasyBarva,OsobaKonicky,OsobaKoureni,OsobaAlkohol,OsobaDeti,Txt) Values (" & MyUser.ID & "," & FN.DB.GetText(Jmeno) & ",'" & Request.UserHostAddress & "'," & Rubrika & "," & Region & "," & Odpovedi & "," & Vek & "," & Znameni & "," & Vzdelani & "," & Postava & "," & VyskaSql & "," & Vlasy & "," & FN.DB.GetText(Konicky) & "," & Koureni & "," & Alkohol & "," & DetiSql & "," & FN.DB.GetText(Txt) & ")"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			CMD.ExecuteNonQuery()
			CMD.Connection.Close()
			'CacheClear(Sekce)
			Response.Redirect("/Seznamka_Add.aspx?akce=AddOk")
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Sub Delete()
		Dim iID As Integer = Val.ToInt(Request.QueryString("id"))
		Dim SQL As String = "DELETE FROM Seznamka WHERE ID=" & iID
		If MyUser.isAdminSekce("Seznamka") = False Then SQL &= " AND UserID=" & MyUser.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		If CMD.ExecuteNonQuery() = 1 Then
			CMD.Connection.Close()
			'CacheClear(Sekce)
			FN.Redir(FN.URL.Referer)
		Else
			CMD.Connection.Close()
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Chyba pøi mazání!"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub

	Function ContainsEmail(ByVal Text As String) As Boolean
		Return System.Text.RegularExpressions.Regex.IsMatch(Text, "([\w-\.]+)@([\w-\.]+)\.([a-zA-Z]{2,4})")
	End Function

	Function ContainsTelefon(ByVal Text As String) As Boolean
		Dim bln As Boolean = False
		Text = System.Text.RegularExpressions.Regex.Replace(Text, "\W+", "")
		Dim Delka As Integer
		Dim Znak As Char
		For f As Integer = 0 To Text.Length - 1
			Znak = Text.Substring(f, 1)
			If System.Text.RegularExpressions.Regex.IsMatch(Znak, "[0-9]+") Then
				Delka += 1
				If Delka >= 7 Then bln = True
			Else
				Delka = 0
			End If
		Next
		Dim Pocet As Integer
		Dim arr() As String = {"nula", "jedna", "dva", "tøi", "ètyøi", "pìt", "šest", "sedm", "osm", "devìt"}
		For Each Slovo As String In arr
			Pocet += System.Text.RegularExpressions.Regex.Matches(Text, Slovo).Count
		Next
		If Pocet >= 7 Then bln = True
		Return bln
	End Function

End Class