Class Admin_Default
	Inherits System.Web.UI.Page
	Dim MyUser As New Fog.User

	Public Function WriteRunTime() As String
		Dim T As TimeSpan = Now.Subtract(CDate(Application("StartTime")))
		Return T.Days & "d " & T.Hours & "h " & T.Minutes & "m " & T.Seconds & "s"
	End Function

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		HideEditSekce(liAdminDefaultVtipy)
		HideEditSekce(liAdminDefaultZabava)
		HideEditSekce(liAdminDefaultPrani)
		HideEditSekce(liAdminDefaultRomantika)
		HideEditSekce(liAdminDefaultBasne)
		HideEditSekce(liAdminDefaultPovidky)
		HideEditSekce(liAdminDefaultUvahy)
		HideEditSekce(liAdminDefaultPohadky)
		HideEditSekce(liAdminDefaultFejetony)
		HideEditSekce(liAdminDefaultRomany)
		HideEditSekce(liAdminDefaultReportaze)
		HideEditSekce(liAdminDefaultErotickePovidky)
		HideEditSekce(liAdminDefaultCitaty)
		HideEditSekce(liAdminDefaultPrislovi)
		HideEditSekce(liAdminDefaultMotta)
		HideEditSekce(liAdminDefaultZamysleni)
		HideEditSekce(liAdminDefaultSeznamka)
		Dim Seznam As New Fog.Seznam(MyUser.AdminAkce)
		If Seznam.IndexOf("SQL") <> -1 Then Me.pnlAdminDefaultSql.Visible = True
		If Seznam.IndexOf("BlokovatUzivatele") = -1 Then Me.liBlokovatUzivatele.Visible = False
	End Sub

	Sub HideEditSekce(ByRef li As System.Web.UI.HtmlControls.HtmlGenericControl)
		Dim bln As Boolean
		If MyUser.AdminSekce.Length > 0 Then
			Me.pnlAdminDefaultEditor.Visible = True
			If MyUser.AdminSekce = "ALL" Then
				bln = True
			Else
				For Each Sekce As String In MyUser.AdminSekce.Split(",")
					If li.ID = "liAdminDefault" & Sekce Then bln = True
				Next
			End If
		End If
		li.Visible = bln
	End Sub

End Class