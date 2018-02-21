Partial Class OrderGeocache
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim CacheID As Integer = Val.ToInt32(Request.QueryString("id"))
		Dim U As New Geo.User
		U.FillFromCookies()
		If Not U.Exists Then
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report(Renderer.Report.Statusy.Err, "Registered users only !!", "Please log in first.")
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If

		Dim SQL As String = "SELECT SUM(CreditAmount) AS CreditsSum FROM Credits WHERE CreditUser=" & U.ID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim CreditsSum As Integer = Val.ToInt32(CMD.ExecuteScalar)
		If CreditsSum + Geo.CreditPrices.OrderCache < 0 Then
			CMD.Connection.Close()
			Form1.Visible = False
			Dim Report As New Renderer.Report(Renderer.Report.Statusy.Err, "You don't have enough credits !!", "Share or validate some coordinates to gain more credits.")
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		CMD.CommandText = "SELECT OrderID FROM Orders WHERE OrderUser=" & U.ID & " AND OrderCache=" & CacheID
		If Not IsNothing(CMD.ExecuteScalar) Then
			CMD.Connection.Close()
			Form1.Visible = False
			Dim Report As New Renderer.Report(Renderer.Report.Statusy.Err, "You have already bought this cache !!")
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If

		If Page.IsPostBack Then
			CMD.CommandText = "INSERT INTO Credits (CreditUser,CreditAmount,CreditSource,CreditParent) VALUES (" & U.ID & "," & Geo.CreditPrices.OrderCache & "," & Geo.CreditSources.OrderCache & "," & CacheID & ")"
			CMD.ExecuteNonQuery()
			CMD.CommandText = "INSERT INTO Orders (OrderUser,OrderCache) VALUES (" & U.ID & "," & CacheID & ")"
			CMD.ExecuteNonQuery()
			Response.Redirect("/Geocache.aspx?id=" & CacheID)
		Else
			CMD.CommandText = "SELECT CacheCode,CacheName FROM Geocaches WHERE CacheID=" & CacheID
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read Then
				lblCacheCode.Text = Server.HtmlEncode(DR("CacheCode"))
				lblCacheName.Text = Server.HtmlEncode(DR("CacheName"))
				DR.Close()
			Else
				DR.Close()
				CMD.Connection.Close()
				Form1.Visible = False
				Dim Report As New Renderer.Report(Renderer.Report.Statusy.Err, "Geocache not found !!", "This geocacache doesn't exist.")
				Me.phReport.Controls.Add(Report.Render)
				Exit Sub
			End If
			CMD.Connection.Close()
			lblPrice.Text = Math.Abs(Geo.CreditPrices.OrderCache)
		End If
	End Sub

End Class