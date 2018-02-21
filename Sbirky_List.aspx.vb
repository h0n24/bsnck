Partial Class Sbirky_List
	Inherits System.Web.UI.Page
	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Dim Pozice As Int16 = 1

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Page.IsPostBack Then
			FN.Cookies.WriteKeyWeb("SbirkyShowNedokoncene", chbNedokoncene.Checked)
			Response.Redirect(Request.Url.AbsolutePath & "?sort=" & lbSort.SelectedValue)
		Else
			lbSort.Items.Add(New ListItem("Data dokončení", "dokonceno"))
			lbSort.Items.Add(New ListItem("Data založení", "id"))
			lbSort.Items.Add(New ListItem("Tipů", "tipy"))
			lbSort.SelectedValue = IIf(IsNothing(Request.QueryString("sort")), "dokonceno", Request.QueryString("sort"))
			chbNedokoncene.Checked = Val.ToBoolean(FN.Cookies.Read("Web", "SbirkyShowNedokoncene"))
		End If
		Dim Autor As UInt64 = Val.ToInt(Request.QueryString("autor"))
		Dim Kat As UInt64 = Val.ToInt(Request.QueryString("kat"))
		Dim Stranka As Integer = IIf(Val.ToInt(Request.QueryString("pg")) = 0, 1, Val.ToInt(Request.QueryString("pg")))
		Dim PocetStranek, CelkemZaznamu, ZobrazOd, ZobrazDo As Int64
		Dim Limit As Int64 = 5000
		Dim LimitPage As Int16 = 20
		Dim SqlWhere As New FN.DB.SqlWhere
		If Autor <> 0 Then SqlWhere.Add("Autor=" & Autor)
		If Kat <> 0 Then SqlWhere.Add("Kat=" & Kat)
		If Not chbNedokoncene.Checked Then SqlWhere.Add("Dokonceno IS NOT NULL")
		Dim OrderBy As String
		Select Case lbSort.SelectedValue
			Case "id" : OrderBy = " ORDER BY ID Desc"
			Case "tipy" : OrderBy = " ORDER BY (SELECT SUM(TipHodnota) FROM SbirkyTipy WHERE TipDBID=ID) Desc"
			Case Else : OrderBy = " ORDER BY Dokonceno Desc, ID Desc"
		End Select
		Dim SQL As String = "SELECT TOP " & Limit & " ID FROM Sbirky" & SqlWhere.Text & OrderBy
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		Dim dtID As New DataTable("MojeId")
		DA.Fill(dtID)
		DA.Dispose()
		CelkemZaznamu = dtID.Rows.Count
		Dim Seznam As New Fog.Seznam
		If CelkemZaznamu > 0 Then
			ZobrazOd = (Stranka - 1) * LimitPage + 1
			ZobrazDo = Math.Min(CelkemZaznamu, ZobrazOd + LimitPage - 1)
			PocetStranek = Int((CelkemZaznamu - 1) / LimitPage) + 1
			Dim f As Int64 = ZobrazOd
			While f <= ZobrazDo
				Seznam.Add(dtID.Rows(f - 1)(0))
				f += 1
			End While
		End If
		dtID = Nothing
		If Seznam.Count > 0 Then
			Dim sWhere As String = " WHERE ID IN(" & Seznam.Text & ")"
			If CelkemZaznamu = Limit Then
				litZaznamu.Text = "Překročeno maximum " & CelkemZaznamu & " záznamů. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & "."
			Else
				litZaznamu.Text = "Nalezeno " & CelkemZaznamu & " záznamů. Zobrazuji " & ZobrazOd & "-" & ZobrazDo & "."
			End If
			SQL = "SELECT ID,Datum,Dokonceno,Kat,Titulek,Autor,Prolog,Users.UserNick,Kategorie.KatNazev" & _
			",(SELECT COUNT(*) FROM SbirkyObsah WHERE ObsahSbirka = ID) AS Kapitol, (SELECT SUM(TipHodnota) FROM SbirkyTipy WHERE TipDBID=ID) AS Tipy" & _
			" FROM Sbirky LEFT JOIN Users On Autor=UserID LEFT JOIN Kategorie ON Kat=KatID" & sWhere & OrderBy
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			rptSbirky.DataSource = DR
			rptSbirky.DataBind()
			DR.Close()
		Else
			Me.phMain.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.OK
			Report.Title = "Žádné sbírky nebyly nalezeny."
			Report.Text = "Buďte první, kdo vytvoří sbírku."
			Me.phReport.Controls.Add(Report.Render)
		End If
		DB.Close()
		Me.divPagesBox.InnerHtml = Renderer.PagesBox(Stranka, PocetStranek)
	End Sub

	Sub rptSbirky_ItemCreated(ByVal Sender As Object, ByVal e As RepeaterItemEventArgs)
		Dim Autor As UInt64 = e.Item.DataItem("Autor")
		If MyUser.ID = Autor Then
			e.Item.FindControl("phEdit").Visible = True
		Else
			e.Item.FindControl("phEdit").Visible = False
		End If
		If Pozice = 4 Then
			Dim C As New UI.HtmlControls.HtmlGenericControl("div")
			C = New UI.HtmlControls.HtmlGenericControl("div")
			'C.Attributes.Add("style", "margin-bottom:4px;")
			C.InnerHtml = Reklama.GenerateFull501
			e.Item.FindControl("phReklamaInside").Controls.Add(C)
		End If
		Pozice += 1
	End Sub

	Public Function ZkratitProlog(ByVal Prolog As String) As String
		Const Limit As Int16 = 250
		If Prolog.Length > Limit Then
			Dim Length As Int16 = Prolog.LastIndexOf(" ", Limit, 30)
			Length = Math.Max(Length, Prolog.LastIndexOf(".", Limit, 30))
			Length = Math.Max(Length, Prolog.LastIndexOf(vbCrLf, Limit, 30))
			If Length = -1 Then Length = Limit
			Return Prolog.Substring(0, Length) & "..."
		Else
			Return Prolog
		End If
	End Function

	Public Function ZobrazitDokonceno(ByVal Dokonceno As Object) As String
		If IsDBNull(Dokonceno) Then
			Return "rozpracováno"
		Else
			Return CType(Dokonceno, Date).ToString("d.M.yyyy")
		End If
	End Function

End Class