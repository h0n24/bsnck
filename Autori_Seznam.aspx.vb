Class _Autori_Seznam
	Inherits System.Web.UI.Page
	Public PocetAutoru As Integer

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim Akce As String = Request.QueryString("Akce")
		If Page.IsPostBack Then
			Page.Validate()
			If Page.IsValid Then
				ShowSeznam()
			End If
		End If
		If Akce = "seznam" Then
			ShowSeznam()
		End If
		If pnlAutoriSeznamMenu.Visible = True Then
			ShowMenu()
		End If
	End Sub

	Sub ShowSeznam()
		Me.pnlAutoriSeznamMenu.Visible = False
		Me.pnlAutoriSeznamShow.Visible = True
		Dim sWhere As String
		If Page.IsPostBack Then
			sWhere = " WHERE UserNick LIKE '%" & Me.tbFiltr.Text.Replace("'", "''") & "%'"
		Else
			Dim Filtr As String = Request.QueryString("filtr")
			If Not Filtr Is Nothing Then
				Select Case Filtr
					Case "a" : sWhere = " WHERE Left(UserNick,1) IN ('a','·')"
					Case "c" : sWhere = " WHERE Left(UserNick,2)<>'ch' AND Left(UserNick,1) IN ('c','Ë')"
					Case "d" : sWhere = " WHERE Left(UserNick,1) IN ('d','Ô')"
					Case "e" : sWhere = " WHERE Left(UserNick,1) IN ('e','È','Ï')"
					Case "i" : sWhere = " WHERE Left(UserNick,1) IN ('i','Ì')"
					Case "n" : sWhere = " WHERE Left(UserNick,1) IN ('n','Ú')"
					Case "o" : sWhere = " WHERE Left(UserNick,1) IN ('o','Û')"
					Case "r" : sWhere = " WHERE Left(UserNick,1) IN ('r','¯')"
					Case "s" : sWhere = " WHERE Left(UserNick,1) IN ('s','ö')"
					Case "t" : sWhere = " WHERE Left(UserNick,1) IN ('t','ù')"
					Case "u" : sWhere = " WHERE Left(UserNick,1) IN ('u','˘','˙')"
					Case "y" : sWhere = " WHERE Left(UserNick,1) IN ('y','˝')"
					Case "z" : sWhere = " WHERE Left(UserNick,1) IN ('z','û')"
					Case Else : sWhere = " WHERE Left(UserNick," & Filtr.Length & ")=" & FN.DB.GetText(Filtr)
				End Select
			End If
		End If
		Dim SQL As String = "SELECT AutorUser, UserNick FROM UsersAutori LEFT JOIN Users ON AutorUser=Users.UserID" & sWhere & " ORDER BY UserNick"
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(SQL, DB)
		Dim dSet As DataSet = New DataSet
		DA.Fill(dSet, "UsersAutori")
		DA.Dispose()
		DB.Close()
		PocetAutoru = dSet.Tables("UsersAutori").Rows.Count()
		Me.RepeaterAutori.DataSource = dSet.Tables("UsersAutori")
		Page.DataBind()
	End Sub

	Sub ShowMenu()
		Dim dSet As New DataSet("MyDS")
		Dim dTable As New DataTable("Pismena")
		Dim dRow As DataRow
		dTable.Columns.Add("Pismeno", System.Type.GetType("System.String"))
		dSet.Tables.Add(dTable)
		Dim f As Integer
		For f = 0 To 25
			If f = 8 Then
				dRow = dTable.NewRow
				dRow("Pismeno") = "ch"
				dTable.Rows.Add(dRow)
			End If
			dRow = dTable.NewRow
			dRow("Pismeno") = Chr(f + 97)
			dTable.Rows.Add(dRow)
		Next
		RepeaterPismena.DataSource = dSet.Tables("Pismena")
		Page.DataBind()
	End Sub

End Class