Class Master_Admin
	Inherits System.Web.UI.MasterPage
	Public MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User
	Public Visits, Pages As Integer

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Page.Title = "Admin » " & Page.Title
		If MyIni.RunLocal Then Page.Title = "###" & Page.Title
		If MyUser.isAdmin = False Then
			Me.lblUserNick.Text = "Nejsi pøihlášen(a) !!! .... not logged"
			Me.ContentPlaceHolder1.Visible = False
			Exit Sub
		End If
		Me.lblUserNick.Text = Server.HtmlEncode(MyUser.Nick)
	End Sub

End Class