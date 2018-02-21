Partial Class Geocaches
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim SQL As String = "SELECT TOP 30 CacheID,CacheCode,CacheName,CacheDate,CacheType,CacheUser,CacheLat,CacheLon FROM Geocaches ORDER BY CacheID DESC"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			dgGeocaches.DataSource = DR
			dgGeocaches.DataBind()
		Else
			phGeocaches.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Geocaches not found !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
		DR.Close()
		CMD.Connection.Close()

		'Dim U As New Geo.User
		'U.FillFromCookies()
		'Dim CacheID As Integer = Val.ToInt32(Request.QueryString("id"))

		'Dim Coord As New Geo.Coordinates()
		'CMD.CommandText = "SELECT FinalID FROM FinalCoords WHERE FinalCache=" & CacheID & " AND FinalLat=" & Coord.LatToSQL & " AND FinalLon=" & Coord.LonToSQL

	End Sub



End Class