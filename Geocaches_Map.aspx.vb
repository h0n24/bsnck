Partial Class Geocaches_Map
	Inherits System.Web.UI.Page

	Dim EN As New Globalization.CultureInfo("en-US")

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim CenterCoord As Geo.Coordinates
		If IsNothing(Request.QueryString("lat")) And IsNothing(Request.QueryString("lon")) Then
			Dim U As New Geo.User
			U.FillFromCookies()
			If U.Exists Then
				CenterCoord = U.HomeCoord
			Else
				CenterCoord = New Geo.Coordinates(47, 10)
			End If
		Else
			CenterCoord = New Geo.Coordinates(Val.ToDecimal(Request.QueryString("lat")), Val.ToDecimal(Request.QueryString("lon")))
		End If
		lblCenter.Text = CenterCoord.ToString
		Dim sb As New StringBuilder()
		sb.Append("<script type=""text/javascript"">")
		sb.Append("var centerLatlng = new google.maps.LatLng(" & CenterCoord.Lat.ToString(EN) & ", " & CenterCoord.Lon.ToString(EN) & ");")

		Dim DistanceSQL As String = "SQRT(SQUARE(" & CenterCoord.LatToSQL & "-CacheLat) + SQUARE(" & CenterCoord.LonToSQL & "-CacheLon))"
		Dim SQL As String = "SELECT TOP 200 CacheID,CacheCode,CacheName,CacheDate,CacheType,CacheUser,CacheLat,CacheLon FROM Geocaches ORDER BY " & DistanceSQL
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		If DR.HasRows Then
			sb.Append("var Geocaches = [")
			While DR.Read
				sb.Append("['" & Server.HtmlEncode(DR("CacheCode")).Replace("'", "&apos;") & "', " & CoordSqlToDec(DR("CacheLat")) & ", " & CoordSqlToDec(DR("CacheLon")) & ", '" & Server.HtmlEncode(DR("CacheName")).Replace("'", "&apos;") & "', " & DR("CacheType") & "], ")
			End While
			sb.Remove(sb.Length - 2, 2)
			sb.Append("];")
		End If
		DR.Close()
		CMD.Connection.Close()

		sb.Append("</script>")
		litJavaScript.Text = sb.ToString
	End Sub

	Public Function CoordSqlToDec(ByVal numSQL As Integer) As String
		Return (numSQL / Geo.Coordinates.SQLmultiplier).ToString(EN)
	End Function
End Class