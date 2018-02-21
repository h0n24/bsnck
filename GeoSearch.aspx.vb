Partial Class GeoSearch
	Inherits System.Web.UI.Page
	Dim U As New Geo.User
	Dim Coord As New Geo.Coordinates

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		U.FillFromCookies()
		If Not Page.IsPostBack Then
			If U.Exists Then
				lblHomeCoordinates.Text = U.HomeCoord.ToString
			Else
				pnlHomeCoord.Visible = False
			End If

		Else

		End If
	End Sub

	Public Function CountDistance(ByVal Lat As Double, ByVal Lon As Double) As String
		Return Val.ToInt32(Coord.DistanceTo(Lat, Lon) / 1000) & " km"
	End Function

	Protected Sub btnCacheCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCacheCode.Click
		If tbCacheCode.Text <> "" Then
			'SearchCacheCode()
		ElseIf tbNearCacheCode.Text <> "" Then
			SearchNearCacheCode()
		ElseIf tbCoord.Text <> "" Then
			SearchNearCoordinates()
		Else
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Missing value !!"
			Me.phReport.Controls.Add(Report.Render)
		End If
	End Sub
	Protected Sub btnNearCacheCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNearCacheCode.Click
		SearchNearCacheCode()
	End Sub
	Protected Sub btnCoordinates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCoordinates.Click
		SearchNearCoordinates()
	End Sub

	Sub SearchNearCacheCode()
		Dim Limit As Short = 30
		Dim report As New Renderer.Report
		Dim CacheCode As String = tbNearCacheCode.Text.Trim
		If CacheCode = "" Then
			report.Status = Renderer.Report.Statusy.Err
			report.Title = "Missing Geocache code !!"
			Me.phReport.Controls.Add(report.Render)
			Exit Sub
		End If
		Dim SQL As String = "SELECT CacheID,CacheCode,CacheLat,CacheLon FROM Geocaches WHERE CacheCode=" & FN.DB.GetText(CacheCode)
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If Not DR.Read Then
			DR.Close()
			CMD.Connection.Close()
			report.Status = Renderer.Report.Statusy.Err
			report.Title = "Geocache code not found !!"
			Me.phReport.Controls.Add(report.Render)
			Exit Sub
		End If
		Dim CacheID As Integer = DR("CacheID")
		Coord = New Geo.Coordinates(DR("CacheLat") / Geo.Coordinates.SQLmultiplier, DR("CacheLon") / Geo.Coordinates.SQLmultiplier)
		DR.Close()
		report.Text = "Geocaches near Geocache " & CacheCode & " (" & Coord.ToString & ")"
		Me.phReport.Controls.Add(report.Render)
		Form1.Visible = False
		REM pomocí koule select DEGREES(ACOS(SIN(RADIANS(@Lat1)) * SIN(RADIANS(@Lat2)) + COS(RADIANS(@Lat1)) * COS(RADIANS(@Lat2)) * COS(RADIANS(@Lon1 - @Lon2)))) * 111.3
		Dim DistanceSQL As String = "SQRT(SQUARE(" & Coord.LatToSQL & "-CacheLat) + SQUARE(" & Coord.LonToSQL & "-CacheLon))"
		CMD.CommandText = "SELECT TOP " & Limit & " CacheID,CacheCode,CacheName,CacheDate,CacheType,CacheUser,CacheLat,CacheLon, " & DistanceSQL & " AS Diagonal FROM Geocaches ORDER BY " & DistanceSQL
		DR = CMD.ExecuteReader()
		If DR.HasRows Then
			dgGeocaches.DataSource = DR
			dgGeocaches.DataBind()
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub SearchNearCoordinates()
		Dim Limit As Short = 30
		Dim report As New Renderer.Report
		If Not Coord.Parse(tbCoord.Text) Then
			report.Status = Renderer.Report.Statusy.Err
			report.Title = "Wrong coordinates format !!"
			Me.phReport.Controls.Add(report.Render)
			Exit Sub
		Else
			report.Text = "Geocaches near coordinates " & Coord.ToString
			Me.phReport.Controls.Add(report.Render)
			Form1.Visible = False
		End If
		Dim DistanceSQL As String = "SQRT(SQUARE(ABS(" & Coord.LatToSQL & "-CacheLat)) + SQUARE(ABS(" & Coord.LonToSQL & "-CacheLon)))"
		Dim SQL As String = "SELECT TOP " & Limit & " CacheID,CacheCode,CacheName,CacheDate,CacheType,CacheUser,CacheLat,CacheLon, " & DistanceSQL & " AS Diagonal FROM Geocaches ORDER BY " & DistanceSQL
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			dgGeocaches.DataSource = DR
			dgGeocaches.DataBind()
		End If
		DR.Close()
		CMD.Connection.Close()
	End Sub

End Class