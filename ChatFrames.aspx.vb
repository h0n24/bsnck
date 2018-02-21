Class _ChatFrames
	Inherits System.Web.UI.Page

	Const DefaultLimitRows As Integer = 30
	Const DefaultReloadTime As Integer = 30
	Const TimeoutSilent = 1200	'Sekund
	Public Const TimeoutPassive As Integer = 30 'po expiraci
	Public Const TimeoutDelete As Integer = 60  'po expiraci
	Public MyUser As New Fog.User
	Public RoomID As Integer
	Public RoomName As String
	Public Menu1Class, Menu2Class As String
	Dim Akce As String
	Dim ReloadTime, LimitRows, iSilent As Integer
	Dim ShowDate, ShowTime As Boolean


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		RoomID = Val.ToInt(Request.QueryString("room"))
		Akce = Request.QueryString("akce")
		iSilent = CInt("0" & Request.QueryString("silent"))
		If iSilent > TimeoutSilent Then Akce = "TimeOut"
		ReloadTime = Val.ToInt(FN.Cookies.Read("Chat", "Reload"))
		If ReloadTime = 0 Then ReloadTime = DefaultReloadTime
		LimitRows = Val.ToInt(FN.Cookies.Read("Chat", "Rows"))
		If LimitRows = 0 Then LimitRows = DefaultLimitRows
		ShowDate = Val.ToBoolean(FN.Cookies.Read("Chat", "ShowDate"))
		ShowTime = Val.ToBoolean(FN.Cookies.Read("Chat", "ShowTime"))
		Select Case Akce
			Case "ShowTop" : ShowTop()
			Case "ShowChat" : ShowChat()
			Case "ShowBottom" : ShowBottom()
			Case "ShowRight" : ShowRight()
			Case "TimeOut" : ShowTimeoutSilent()
			Case "add" : AddChat()
			Case "history" : ShowChat()
			Case "odejit" : Odejit()
		End Select
	End Sub

	Sub ShowBottom()
		Me.pnlBottom.Visible = True
		Me.Body.Attributes.Add("class", "Bottom")
	End Sub

	Sub ShowTop()
		Dim SQL As String = "SELECT RoomNazev FROM ChatRooms WHERE RoomID=" & RoomID
		Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(SQL, DBConn)
		Dim dt As New DataTable
		DBDA.Fill(dt)
		DBConn.Close()
		If dt.Rows.Count > 0 Then
			RoomName = dt.Rows(0)("RoomNazev")
			Me.pnlTop.DataBind()
		End If
		Me.pnlTop.Visible = True
		Me.Body.Attributes.Add("class", "Top")
	End Sub

	Sub ShowChat()
		If Akce = "ShowChat" Then
			Me.litMetaRefresh.Text = "<meta http-equiv=""Refresh"" content=""" & ReloadTime & "; URL=ChatFrames.aspx?akce=ShowChat&room=" & RoomID & "&silent=" & iSilent + ReloadTime & """ />"
		Else
			LimitRows = 1000
		End If
		Me.pnlChat.Visible = True
		Me.Body.Attributes.Add("class", "Chat")
		Dim SQL As String = "SELECT TOP " & LimitRows & " ChatDatum,ChatJmeno,ChatTxt FROM Chat WHERE ChatRoom=" & RoomID & " ORDER BY ChatID Desc"
		Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DBDA As New System.Data.SqlClient.SqlDataAdapter(SQL, DBConn)
		Dim dt As New DataTable
		DBDA.Fill(dt)
		DBConn.Close()
		Dim sHtml, S As String
		Dim sDatum, sClass As String
		Dim Datum As Date
		For Each dr As DataRow In dt.Rows
			Datum = dr("ChatDatum")
			S = "<div title='" & Server.HtmlEncode(dr("ChatJmeno")) & " (" & Datum.ToString("d.M. HH:mm") & ")'>"
			If ShowDate Or ShowTime Then
				If ShowDate And ShowTime Then
					sDatum = "d.M. HH:mm"
				ElseIf ShowDate Then
					sDatum = "d.M."
				Else
					sDatum = "HH:mm"
				End If
				S &= "<span class=""datum"">(" & Datum.ToString(sDatum) & ")</span>"
			End If
			If dr("ChatJmeno") = MyUser.Nick Then
				sClass = " you"
			Else
				sClass = ""
			End If
			S &= "<span class='jmeno" & sClass & "'>[" & Server.HtmlEncode(dr("ChatJmeno")) & "]&nbsp;</span>"
			S &= "<span class='txt" & sClass & "'>" & Server.HtmlEncode(dr("ChatTxt")) & "</span></div>" & vbCrLf
			sHtml = S & sHtml
		Next
		Me.litChatHtml.Text = sHtml
	End Sub

	Sub ShowTimeoutSilent()
		DeleteUserFromRoom()
		Response.Write("<html><head></head><body>")
		Response.Write("<p>Došlo k odhlášení z dùvodu neèinnosti !</p>" & vbCrLf)
		Response.Write("<script language=""JavaScript""><!-- Begin" & vbCrLf)
		Response.Write("window.top.location = '/ChatRooms.aspx';" & vbCrLf)
		Response.Write("//  End --></script></body></html>")
		Response.End()
	End Sub

	Sub Odejit()
		DeleteUserFromRoom()
		Response.Redirect("ChatRooms.aspx")
	End Sub

	Sub DeleteUserFromRoom()
		Application.Lock()
		Dim dt As DataTable = FN.Cache.dtChatUsersOnline
		Dim dRows() As DataRow = dt.Select("UserID=" & MyUser.ID & " AND Room=" & RoomID)
		If dRows.Length > 0 Then
			dt.Rows.Remove(dRows(0))
			Application("dtChatUsersOnline") = dt
		End If
		Application.UnLock()
	End Sub

	Sub AddChat()
		Dim sError As String
		Dim Jmeno As String = MyUser.Nick
		Dim Txt As String = Request.Form("Txt").Trim
		If Txt = "" Then sError &= "<li>Chybí text !!</li>"
		If Jmeno = "" Then sError &= "<li>Nemohu získat vaše jméno !!</li>"
		If sError = "" Then
			Do While InStr(Txt, "  ") > 0
				Txt = Replace(Txt, "  ", " ")
			Loop
			Do While Right(Txt, 4) = Right(Txt, 1) & Right(Txt, 1) & Right(Txt, 1) & Right(Txt, 1)
				Txt = Left(Txt, Len(Txt) - 1)
			Loop
			If FN.Text.Test.VelkaPismena(Txt) > 40 And Txt.Length > 5 Then Txt = Txt.ToLower
			If Txt.Length > 3 Then
				For f As Integer = Txt.Length - 4 To 0 Step -1
					If Txt.Substring(f) & Txt.Substring(f) & Txt.Substring(f) & Txt.Substring(f) = Txt.Substring(f, 4) Then Txt = Txt.Remove(f, 1)
				Next
			End If
			Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
			Dim SQL As String = "INSERT INTO Chat (ChatDatum,ChatRoom,ChatJmeno,ChatTxt) Values (" & FN.DB.GetDateTime(Now) & ", " & RoomID & ", " & FN.DB.GetText(Jmeno) & ", " & FN.DB.GetText(Txt) & ")"
			Dim DBCmd As New System.Data.SqlClient.SqlCommand(SQL, DBConn)
			DBCmd.ExecuteNonQuery()
			DBConn.Close()
			'		If Application("ChatRoomLimit" & Room) > 0 Then
			'			strSQL = "SELECT Count(*) AS Pocet FROM Chat WHERE ChatRoom=" & Room
			'			rstDB = ConnDB.Execute(strSQL)
			'			Pocet = rstDB("Pocet")
			'			rstDB.Close()
			'			SmazatPocet = Pocet - Application("ChatRoomLimit" & Room) + 1
			'			If SmazatPocet > 0 Then
			'				strSQL = "SELECT TOP 1 ChatID FROM Chat WHERE ChatRoom=" & Room & " ORDER BY ChatID"
			'				rstDB = ConnDB.Execute(strSQL)
			'				SmazatID = rstDB("ChatID")
			'				rstDB.Close()
			'				ConnDB.Execute("DELETE FROM Chat WHERE ChatID=" & SmazatID)
			'			End If
			'			rstDB = Nothing
			'		End If
		End If
		Response.Redirect("Chat.aspx?room=" & RoomID)
	End Sub

	Sub ShowRight()
		Me.pnlRight.Visible = True
		Me.Body.Attributes.Add("class", "Right")
		If Request.QueryString("right") = "setup" Then
			Menu1Class = "passive" : Menu2Class = "active"
			Me.divRightMenu.DataBind()
			ShowRightSetup()
		Else
			Me.litMetaRefresh.Text = "<meta http-equiv=""Refresh"" content=""" & ReloadTime & "; URL=ChatFrames.aspx?akce=ShowRight&room=" & RoomID & "&silent=" & iSilent + ReloadTime & """ />"
			Menu1Class = "active" : Menu2Class = "passive"
			Me.divRightMenu.DataBind()
			ShowRightUsers()
		End If
	End Sub

	Sub ShowRightUsers()
		Me.pnlRightUsers.Visible = True
		Application.Lock()
		Dim dt As DataTable = FN.Cache.dtChatUsersOnline
		Dim dRows() As DataRow = dt.Select("UserID=" & MyUser.ID & " AND Room=" & RoomID)
		Dim Expirace As Date = Now.AddSeconds(ReloadTime + 5)
		If dRows.Length = 0 Then
			Dim dr As DataRow = dt.NewRow
			dr("Room") = RoomID
			dr("UserID") = MyUser.ID
			dr("UserNick") = MyUser.Nick
			dr("Expirace") = Expirace
			dr("Zacatek") = Now
			dt.Rows.Add(dr)
		Else
			dRows(0)("Expirace") = Expirace
		End If
		dRows = dt.Select("Room=" & RoomID, "UserID DESC")
		Dim str, Status As String
		For Each dr As DataRow In dRows
			Expirace = dr("Expirace")
			If Now < Expirace Then
				Status = "online"
			ElseIf Now > Expirace And Now <= Expirace.AddSeconds(TimeoutPassive) Then
				Status = "offline"
			ElseIf Now > Expirace.AddSeconds(TimeoutPassive) And Now <= Expirace.AddSeconds(TimeoutDelete) Then
				Status = "off"
			Else
				Status = "delete"
				dt.Rows.Remove(dr)
			End If
			If Status <> "delete" Then
				If dr("UserID") = MyUser.ID Then Status &= " you"
				str &= "<li class='" & Status & "' title='Online od " & dr("Zacatek") & "'>•" & Server.HtmlEncode(dr("UserNick")) & "</li>"
			End If
		Next
		Application("dtChatUsersOnline") = dt
		Application.UnLock()
		Me.litUsersHtml.Text = str
		Me.litUsersCount.Text = dRows.Length
	End Sub

	Sub ShowRightSetup()
		If Page.IsPostBack Then
			If Val.ToInt(Me.selReloadTime.Value) <> 0 Then
				ReloadTime = Val.ToInt(Me.selReloadTime.Value)
			End If
			LimitRows = Val.ToInt(Me.tbLimitRows.Text)
			FN.Cookies.WriteKey("Chat", "Reload", ReloadTime)
			FN.Cookies.WriteKey("Chat", "Rows", LimitRows)
			FN.Cookies.WriteKey("Chat", "ShowDate", Me.chbShowDate.Checked)
			FN.Cookies.WriteKey("Chat", "ShowTime", Me.chbShowTime.Checked, Now.AddDays(500))
			Response.Redirect("Chat.aspx?room=" & RoomID)
		Else
			Me.pnlRightSetup.Visible = True
			For f As Integer = 0 To Me.selReloadTime.Items.Count - 1
				If ReloadTime = Me.selReloadTime.Items(f).Value Then
					Me.selReloadTime.SelectedIndex = f
				End If
			Next
			Me.tbLimitRows.Text = LimitRows
			Me.chbShowDate.Checked = ShowDate
			Me.chbShowTime.Checked = ShowTime
		End If
	End Sub

End Class
