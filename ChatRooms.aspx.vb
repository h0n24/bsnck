Class _ChatRooms
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyUser As New Fog.User
		If MyUser.isAdmin = False Then
			Me.pDiskuzeEditoru.Visible = False
		End If
	End Sub

	Function PocetAktivnichLidi(ByVal RoomID As Integer)
		Dim dt As DataTable = FN.Cache.dtChatUsersOnline
		Dim dRows() As DataRow = dt.Select("Room=" & RoomID & " AND Expirace>" & Val.DateForDatatableSelect(Now))
		Return dRows.Length
	End Function

End Class