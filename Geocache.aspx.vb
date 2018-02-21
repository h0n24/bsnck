Partial Class Geocache
	Inherits System.Web.UI.Page
	Dim CacheID As Integer
	Dim blnShowFinalCoordinates As Boolean

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim sWhere As String
		If Not IsNothing(Request.QueryString("code")) Then
			sWhere = " WHERE CacheCode=" & FN.DB.GetText(Request.QueryString("code").ToString)
		Else
			CacheID = Val.ToInt32(Request.QueryString("id"))
			sWhere = " WHERE CacheID=" & CacheID
		End If
		Dim U As New Geo.User
		U.FillFromCookies()
		Dim SQL As String = "SELECT CacheID,CacheCode,CacheName,CacheDate,CacheType,CacheUser,CacheLat,CacheLon FROM Geocaches" & sWhere
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If Not DR.Read Then
			DR.Close()
			CMD.Connection.Close()
			phGeocache.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "Geocache not found !!"
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		CacheID = DR("CacheID")
		lblCode.Text = Server.HtmlEncode(DR("CacheCode"))
		hlGeocachingCom.NavigateUrl = String.Format(hlGeocachingCom.NavigateUrl, DR("CacheCode"))
		lblName.Text = Server.HtmlEncode(DR("CacheName"))
		Dim GeocacheType As New Geo.GeocacheType(DR("CacheType"))
		lblType.Text = GeocacheType.Name
		Dim Coord As New Geo.Coordinates(DR("CacheLat") / Geo.Coordinates.SQLmultiplier, DR("CacheLon") / Geo.Coordinates.SQLmultiplier)
		lblCoordinates.Text = Coord.ToString
		DR.Close()
		CMD.CommandText = "SELECT OrderDate FROM Orders WHERE OrderUser=" & U.ID & " AND OrderCache = " & CacheID
		DR = CMD.ExecuteReader
		If DR.Read Then
			blnShowFinalCoordinates = True
			hlDecode.Visible = False
		Else
			hlDecode.NavigateUrl = String.Format(hlDecode.NavigateUrl, CacheID)
		End If
		DR.Close()
		CMD.CommandText = "SELECT FinalCache, FinalLat, FinalLon, COUNT(*) as Count, MIN(FinalID) as FistFinalID, MIN(FinalDate) as FirstFinalDate, MAX(FinalDate) as LastFinalDate FROM FinalCoords WHERE FinalCache=" & CacheID & " GROUP BY FinalCache,FinalLat,FinalLon"
		DR = CMD.ExecuteReader()
		rptFinals.DataSource = DR
		rptFinals.DataBind()
		CMD.Connection.Close()
	End Sub

	Public Function ShowFinalCoordinates(ByVal LatSQL As Integer, ByVal LonSQL As Integer) As String
		Dim Coord As New Geo.Coordinates(LatSQL / Geo.Coordinates.SQLmultiplier, LonSQL / Geo.Coordinates.SQLmultiplier)
		If blnShowFinalCoordinates Then
			Return Coord.ToString
		Else
			Return Coord.ToStringMasked
		End If
	End Function

End Class