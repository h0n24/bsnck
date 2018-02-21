Partial Class Admin_UsersSharingPC
	Inherits System.Web.UI.Page
	Private Const BufferSize As Int16 = 20
	Dim arr As New ArrayList
	Dim Pocet As Integer

	Private Class Item
		Public id As Int64
		Public datum As Date
		Public nickNew As String
		Public nickOld As String
		Public Sub New(ByVal ID As Int64, ByVal Datum As Date, ByVal NickNew As String, ByVal NickOld As String)
			Me.id = ID
			Me.datum = Datum
			Me.nickNew = NickNew
			Me.nickOld = NickOld
		End Sub
	End Class

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim SQL As String = "SELECT * FROM UsersSharingPC WHERE Datum>" & FN.DB.GetDateTime(Now.AddMonths(-6))
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		For f As Integer = 1 To BufferSize
			If DR.Read() Then
				arr.Add(New Item(DR("ID"), DR("Datum"), DR("NickNew"), DR("NickOld")))
			End If
		Next
		While DR.Read()
			arr.Add(New Item(DR("ID"), DR("Datum"), DR("NickNew"), DR("NickOld")))
			CheckArray()
		End While
		For f As Integer = 1 To arr.Count - 1
			CheckArray()
		Next
		lbl1.Text &= "<br/>Celkem záznamů:" & Pocet
	End Sub

	Private Sub CheckArray()
		Dim item0 As Item = arr(0)
		arr.RemoveAt(0)
		For Each item As Item In arr
			If (item0.nickNew = item.nickOld) And (item0.datum.AddMinutes(20) > item.datum) Then
				lbl1.Text &= Server.HtmlEncode(item0.nickOld & " | " & item0.nickNew & " | " & item.nickNew & " = " & item0.datum.ToString("d.M.yyy HH:mm") & " | " & item.datum & " | " & item.datum.Subtract(item0.datum).Minutes & "min.") & "<br/>"
				Pocet += 1
				Exit For
			End If
		Next
	End Sub

End Class