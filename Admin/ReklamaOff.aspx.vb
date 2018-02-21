Class Admin_ReklamaOff
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim blnReklOff As Boolean = False
		If FN.Cookies.Read("Web", "ReklOff") = Boolean.TrueString Then
			blnReklOff = True
		End If
		blnReklOff = blnReklOff.ToString = Boolean.FalseString
		Me.lblResult.Text &= IIf(blnReklOff, "NE", "ANO")
		FN.Cookies.WriteKeyWeb("ReklOff", blnReklOff.ToString)
		FN.Redir(FN.URL.Referer)
	End Sub

End Class