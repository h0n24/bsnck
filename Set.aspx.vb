Class _Set
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		FN.Cookies.WriteKeyWeb("IsLocalSettings", Boolean.TrueString)
		Me.lnkReferer.HRef = Request.ServerVariables("HTTP_REFERER")
		If Page.IsPostBack Then
			SaveSettings()
		Else
			Select Case Request.QueryString("akce")
				Case Nothing
					If FN.Cookies.Read("Web", "ReklOff") = Boolean.TrueString Then Me.cbReklama.Checked = True
					If FN.Cookies.Read("Web", "AuditOff") = Boolean.TrueString Then Me.cbAudit.Checked = True
					If FN.Cookies.Read("Web", "StatOff") = Boolean.TrueString Then Me.cbStatistiky.Checked = True
					If FN.Cookies.Read("Web", "ErrorDetails") = Boolean.TrueString Then Me.cbErrors.Checked = True
				Case "true"
					Me.cbReklama.Checked = True
					Me.cbAudit.Checked = True
					Me.cbStatistiky.Checked = True
					Me.cbErrors.Checked = True
					SaveSettings()
				Case "false"
					Me.cbReklama.Checked = False
					Me.cbAudit.Checked = False
					Me.cbStatistiky.Checked = False
					Me.cbErrors.Checked = False
					SaveSettings()
			End Select
		End If
	End Sub

	Sub SaveSettings()
		FN.Cookies.WriteKeyWeb("ReklOff", Me.cbReklama.Checked.ToString)
		FN.Cookies.WriteKeyWeb("AuditOff", Me.cbAudit.Checked.ToString)
		FN.Cookies.WriteKeyWeb("StatOff", Me.cbStatistiky.Checked.ToString)
		FN.Cookies.WriteKeyWeb("ErrorDetails", Me.cbErrors.Checked.ToString)
		Me.lblReport.Text = "Uloženo !!"
	End Sub

End Class