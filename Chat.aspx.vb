Class _Chat
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyUser As New Fog.User
		If MyUser.isLogged = False Then
			Response.Redirect("ChatRooms.aspx")
		End If
	End Sub

End Class