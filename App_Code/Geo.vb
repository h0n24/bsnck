Public Class Geo

	Class User
		Dim _id As Int32
		Dim _date As DateTime
		Dim _username As String
		Dim _email As String
		Dim _password As String
		Dim _home As Geo.Coordinates
		Dim _verified As Boolean
		Dim _anonymous As Boolean
		Dim _newsletter As Boolean
		Dim _exists As Boolean

		Public Sub New()
		End Sub
		Public Sub New(ByVal UserID As Int32)
			FillFromDatabase("UserID=" & UserID)
		End Sub
		Public Sub New(ByVal Email As String)
			FillFromDatabase("UserEmail=" & FN.DB.GetText(Email))
		End Sub

		Public Property ID() As Int32
			Get
				Return _id
			End Get
			Set(ByVal value As Int32)
				_id = value
			End Set
		End Property
		Public Property [Date]() As DateTime
			Get
				Return _date
			End Get
			Set(ByVal value As DateTime)
				_date = value
			End Set
		End Property
		Public ReadOnly Property UserNamePublic() As String
			Get
				If Anononymous Then
					Return "@" + _id + "@"
				Else
					Return _username
				End If
			End Get
		End Property
		Public Property UserName() As String
			Get
				Return _username
			End Get
			Set(ByVal value As String)
				_username = value
			End Set
		End Property
		Public Property Email() As String
			Get
				Return _email
			End Get
			Set(ByVal value As String)
				_email = value
			End Set
		End Property
		Public Property Password() As String
			Get
				Return _password
			End Get
			Set(ByVal value As String)
				_password = value
			End Set
		End Property
		Public Property HomeCoord() As Geo.Coordinates
			Get
				Return _home
			End Get
			Set(ByVal value As Geo.Coordinates)
				_home = value
			End Set
		End Property
		Public Property Verified() As Boolean
			Get
				Return _verified
			End Get
			Set(ByVal value As Boolean)
				_verified = value
			End Set
		End Property
		Public Property Anononymous() As Boolean
			Get
				Return _anonymous
			End Get
			Set(ByVal value As Boolean)
				_anonymous = value
			End Set
		End Property
		Public Property NewsLetter() As Boolean
			Get
				Return _newsletter
			End Get
			Set(ByVal value As Boolean)
				_newsletter = value
			End Set
		End Property
		Public ReadOnly Property Exists() As Boolean
			Get
				Return ID <> 0
			End Get
		End Property

		Public Function VerificationCode() As String
			Dim i As Byte
			Dim arr(3) As Int64
			For Each c As Char In "Salt!" & UserName & ID & "Salt" & UserName
				arr(i) += Convert.ToInt16(c)
				i += 1
				If i = 3 Then i = 0
			Next
			Return Hex(arr(2) And 255) & Hex(arr(0) And 255) & Hex(arr(1) And 255)
		End Function

		Private Sub FillFromDatabase(ByVal WHERE As String)
			Dim CMD As New System.Data.SqlClient.SqlCommand("SELECT * FROM GeoUsers WHERE " & WHERE, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() Then
				ID = DR("UserID")
				UserName = DR("UserNick")
				Email = DR("UserEmail")
				[Date] = DR("UserDate")
				Password = DR("UserPassword")
				HomeCoord = New Geo.Coordinates(DR("UserLat") / Geo.Coordinates.SQLmultiplier, DR("UserLon") / Geo.Coordinates.SQLmultiplier)
				Verified = Val.ToBoolean(DR("UserVerified"))
				Anononymous = Val.ToBoolean(DR("UserAnonymous"))
				NewsLetter = Val.ToBoolean(DR("UserNews"))
			End If
			DR.Close()
			CMD.Connection.Close()
		End Sub

		Public Sub FillFromCookies()
			If IsNothing(System.Web.HttpContext.Current.Request.Cookies("User")) Then Exit Sub
			Dim sID As String = FN.Cookies.Read("User", "ID")
			Dim sHash As String = FN.Cookies.Read("User", "H")
			Dim CMD As New System.Data.SqlClient.SqlCommand("SELECT * FROM GeoUsers WHERE UserID=" & FN.DB.GetText(sID), FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			If DR.Read() And sHash = FN.Crypting.EncryptMD5(DR("UserID") & DR("UserEmail") & "Salt inside User Cookies!") Then
				ID = DR("UserID")
				UserName = DR("UserNick")
				Email = DR("UserEmail")
				[Date] = DR("UserDate")
				Password = DR("UserPassword")
				HomeCoord = New Geo.Coordinates(DR("UserLat") / Geo.Coordinates.SQLmultiplier, DR("UserLon") / Geo.Coordinates.SQLmultiplier)
				Verified = Val.ToBoolean(DR("UserVerified"))
				Anononymous = Val.ToBoolean(DR("UserAnonymous"))
				NewsLetter = Val.ToBoolean(DR("UserNews"))
			End If
			DR.Close()
			CMD.Connection.Close()
		End Sub

		Public Sub WriteCookies()
			Dim Expire As Date = Today.AddDays(1).AddHours(3)
			FN.Cookies.WriteKey("User", "ID", ID)
			FN.Cookies.WriteKey("User", "H", FN.Crypting.EncryptMD5(ID & Email & "Salt inside User Cookies!"))
		End Sub

	End Class

	Class GeocacheType
		Dim _ID As Short

		Public Sub New()
		End Sub
		Public Sub New(ByVal ID As Short)
			_ID = ID
		End Sub

		Public Property ID() As Short
			Get
				Return _ID
			End Get
			Set(ByVal value As Short)
				_ID = value
			End Set
		End Property
		Public ReadOnly Property Name() As String
			Get
				Dim dRows() As DataRow = FN.Cache.dtGeocacheTypes.Select("TypeID=" & ID)
				If dRows.Length = 1 Then Return dRows(0)("TypeName")
			End Get
		End Property
		Public ReadOnly Property GainCredits() As Boolean
			Get
				Dim dRows() As DataRow = FN.Cache.dtGeocacheTypes.Select("TypeID=" & ID)
				If dRows.Length = 1 Then Return Val.ToBoolean(dRows(0)("TypeCredits"))
			End Get
		End Property

	End Class

	Class Coordinates
		Dim _Lat As Double
		Dim _Lon As Double
		Dim _format As CoordinatesFormats
		Public Shared SQLmultiplier As Integer = 1000000

		Public Property Lat() As Double
			Get
				Return _Lat
			End Get
			Set(ByVal value As Double)
				_Lat = value
			End Set
		End Property
		Public Property Lon() As Double
			Get
				Return _Lon
			End Get
			Set(ByVal value As Double)
				_Lon = value
			End Set
		End Property
		Public Property Format() As CoordinatesFormats
			Get
				Return IIf(_format = 0, CoordinatesFormats.WGS84Minutes, _format)
			End Get
			Set(ByVal value As CoordinatesFormats)
				_format = value
			End Set
		End Property

		Public Sub New()
		End Sub
		Public Sub New(ByVal Latitude As Double, ByVal Longtitude As Double)
			Lat = Latitude
			Lon = Longtitude
		End Sub

		Public Function DistanceTo(ByVal Coordinates As Geo.Coordinates) As Double
			Return GeoDistance(Lat, Lon, Coordinates.Lat, Coordinates.Lon)
		End Function
		Public Function DistanceTo(ByVal Latitude As Double, ByVal Longtitude As Double) As Double
			Return GeoDistance(Lat, Lon, Latitude, Longtitude)
		End Function

		Public Overloads Function ToString() As String
			Return ToString(Format)
		End Function
		Public Overloads Function ToString(ByVal Format As Geo.CoordinatesFormats) As String
			Dim cultureEN As New System.Globalization.CultureInfo("en-US")
			Dim yDeg As Double = Math.Abs(Lat)
			Dim yMin As Double = (yDeg - Math.Truncate(yDeg)) * 60
			Dim ySec As Double = (yDeg - Math.Truncate(yDeg) - Math.Truncate(yMin) / 60) * 3600
			Dim xDeg As Double = Math.Abs(Lon)
			Dim xMin As Double = (xDeg - Math.Truncate(xDeg)) * 60
			Dim xSec As Double = (xDeg - Math.Truncate(xDeg) - Math.Truncate(xMin) / 60) * 3600
			Dim xSign As String = IIf(Lon < 0, "W ", "E ")
			Dim ySign As String = IIf(Lat < 0, "S ", "N ")
			Select Case Format
				Case CoordinatesFormats.WGS84Decimal
					Return Math.Round(Lat, 5).ToString(cultureEN) & " " & Math.Round(Lon, 5).ToString(cultureEN)
				Case CoordinatesFormats.WGS84Minutes
					Return String.Format("{0} {1}° {2} {3} {4}° {5}", ySign, Math.Truncate(yDeg), yMin.ToString("00.000", cultureEN), xSign, Math.Truncate(xDeg), xMin.ToString("00.000", cultureEN))
				Case CoordinatesFormats.WGS84Seconds
					Return String.Format("{0} {1}° {2}' {3}&quot; {4} {5}° {6}' {7}&quot;", ySign, Math.Truncate(yDeg), Math.Truncate(yMin), Math.Round(ySec, 2).ToString(cultureEN), xSign, Math.Truncate(xDeg), Math.Truncate(xMin), Math.Round(xSec, 2).ToString(cultureEN))
					'Case CoordinatesFormats.WGS84UTM
				Case Else
					Return Nothing
			End Select
		End Function

		Public Function ToStringMasked() As String
			Dim xSign As String = IIf(Lon < 0, "W ", "E ")
			Dim ySign As String = IIf(Lat < 0, "S ", "N ")
			Dim yDeg As Double = Math.Abs(Lat)
			Dim xDeg As Double = Math.Abs(Lon)
			Return String.Format("{0} {1}° ??.??? {2} {3}° ??.???", ySign, Math.Truncate(yDeg), xSign, Math.Truncate(xDeg))
		End Function

		Public Function LatToSQL() As String
			Return Val.ToInt32(Lat * SQLmultiplier)
		End Function
		Public Function LonToSQL() As String
			Return Val.ToInt32(Lon * SQLmultiplier)
		End Function

		Public Function Parse(ByVal Coordinates As String) As Boolean
			'-- Parse -49.5000 +123.5000 | +49.5000° -123.5000° | S 48° 40.265 W 16° 38.027 | N 48° 40' 15.89" E 16° 38' 1.61" | 49°31'23.45"N 16°23'45.67"W | N49d31m23.45s W16d23m45.67s
			Dim cultureEN As New System.Globalization.CultureInfo("en-US")
			Dim S As String = Coordinates.ToUpper.Replace("´", "'")
			If System.Text.RegularExpressions.Regex.Matches(S, "\,").Count = 2 Then S = S.Replace(",", ".") 'decimal in some regions
			Dim xDeg, yDeg, xMin, yMin, xSec, ySec As Double
			Dim matchesDeg As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(S, "[-+]?[0-9]*\.?[0-9]+\°")
			If matchesDeg.Count = 2 Then
				'-- Contains Char(°)
				yDeg = Double.Parse(matchesDeg(0).Value.Replace("°", ""), cultureEN.NumberFormat)
				xDeg = Double.Parse(matchesDeg(1).Value.Replace("°", ""), cultureEN.NumberFormat)
				S = System.Text.RegularExpressions.Regex.Replace(S, "[-+]?[0-9]*\.?[0-9]+\°", "")
				Dim matchesMin As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(S, "[0-9]*\.?[0-9]+\'")
				If matchesMin.Count = 2 Then
					'-- Contains Char(')
					yMin = Double.Parse(matchesMin(0).Value.Replace("'", ""), cultureEN.NumberFormat)
					xMin = Double.Parse(matchesMin(1).Value.Replace("'", ""), cultureEN.NumberFormat)
					S = System.Text.RegularExpressions.Regex.Replace(S, "[-+]?[0-9]*\.?[0-9]+\'", "")

					Dim matchesSec As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(S, "[-+]?[0-9]*\.?[0-9]+\""")
					If matchesSec.Count = 2 Then
						'-- Contains Char(") (e.g. N48°40'15.89" E16°38'1.61")
						ySec = Double.Parse(matchesSec(0).Value.Replace("""", ""), cultureEN.NumberFormat)
						xSec = Double.Parse(matchesSec(1).Value.Replace("""", ""), cultureEN.NumberFormat)
					Else
						matchesSec = System.Text.RegularExpressions.Regex.Matches(S, "[0-9]*\.?[0-9]+")
						If matchesSec.Count = 2 Then
							'-- Contains Seconds without Char(") (e.g.N48°40'15.89 E16°38'1.61)
							ySec = Double.Parse(matchesSec(0).Value, cultureEN.NumberFormat)
							xSec = Double.Parse(matchesSec(1).Value, cultureEN.NumberFormat)
						End If
					End If
				Else
					matchesMin = System.Text.RegularExpressions.Regex.Matches(S, "[0-9]*\.?[0-9]+")
					If matchesMin.Count = 2 Then
						'-- Contains Minutes without Char(') (e.g.N48°40.265 E16°38.027)
						yMin = Double.Parse(matchesMin(0).Value, cultureEN.NumberFormat)
						xMin = Double.Parse(matchesMin(1).Value, cultureEN.NumberFormat)
					End If
				End If
				Lat = yDeg + yMin / 60 + ySec / 3600
				Lon = xDeg + xMin / 60 + xSec / 3600
				If S.IndexOf("S") <> -1 Then Lat = -Lat
				If S.IndexOf("W") <> -1 Then Lon = -Lon
				Return True
			ElseIf System.Text.RegularExpressions.Regex.IsMatch(S, "^[-+]?[0-9]*\.?[0-9]+ [-+]?[0-9]*\.?[0-9]+$") Then
				'-- Decimal format without Char(°) (e.g. 48.671083 -16.633783)
				matchesDeg = System.Text.RegularExpressions.Regex.Matches(S, "[-+]?[0-9]*\.?[0-9]+")
				Lat = Double.Parse(matchesDeg(0).Value.Replace("°", ""), cultureEN.NumberFormat)
				Lon = Double.Parse(matchesDeg(1).Value.Replace("°", ""), cultureEN.NumberFormat)
				Return True
			End If
		End Function

	End Class

	Public Shared Function GeoDistance(ByVal Lat1 As Double, ByVal Lon1 As Double, ByVal Lat2 As Double, ByVal Lon2 As Double) As Double
		' Calculate geodesic distance (in m) between two points specified by latitude/longitude (in numeric degrees) using Vincenty inverse formula for ellipsoids
		Dim low_a As Double = 6378137
		Dim low_b As Double = 6356752.3142
		Dim f As Double = 1 / 298.257223563	'WGS-84 ellipsiod
		Dim L As Double
		Dim U1, U2 As Double
		Dim sinU1, sinU2, cosU1, cosU2 As Double
		Dim lambda As Double
		Dim lambdaP As Double
		Dim iterLimit As Integer
		Dim sinLambda, cosLambda, sinSigma, cosSigma As Double
		Dim sigma As Double
		Dim sinAlpha As Double
		Dim cosSqAlpha As Double
		Dim cos2SigmaM As Double
		Dim C As Double
		Dim uSq As Double
		Dim upper_A, upper_B As Double
		Dim deltaSigma As Double
		L = (Lon2 - Lon1) * Math.PI / 180
		U1 = Math.Atan((1 - f) * Math.Tan(Lat1 * Math.PI / 180))
		U2 = Math.Atan((1 - f) * Math.Tan(Lat2 * Math.PI / 180))
		sinU1 = Math.Sin(U1)
		cosU1 = Math.Cos(U1)
		sinU2 = Math.Sin(U2)
		cosU2 = Math.Cos(U2)
		lambda = L
		lambdaP = 2 * Math.PI
		iterLimit = 20
		While (Math.Abs(lambda - lambdaP) > 0.000000000001) And (iterLimit > 0)
			iterLimit = iterLimit - 1
			sinLambda = Math.Sin(lambda)
			cosLambda = Math.Cos(lambda)
			sinSigma = Math.Sqrt(((cosU2 * sinLambda) ^ 2) + ((cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) ^ 2))
			If sinSigma = 0 Then Return 0 'co-incident points
			cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda
			If sinSigma > 0 Then
				If cosSigma >= sinSigma Then
					sigma = Math.Atan(sinSigma / cosSigma)
				ElseIf cosSigma <= -sinSigma Then
					sigma = Math.Atan(sinSigma / cosSigma) + Math.PI
				Else
					sigma = Math.PI / 2 - Math.Atan(cosSigma / sinSigma)
				End If
			Else
				If cosSigma >= -sinSigma Then
					sigma = Math.Atan(sinSigma / cosSigma)
				ElseIf cosSigma <= sinSigma Then
					sigma = Math.Atan(sinSigma / cosSigma) - Math.PI
				Else
					sigma = -Math.Atan(cosSigma / sinSigma) - Math.PI / 2
				End If
			End If
			sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma
			cosSqAlpha = 1 - sinAlpha * sinAlpha
			If cosSqAlpha = 0 Then 'check for a divide by zero
				cos2SigmaM = 0 '2 points on the equator
			Else
				cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha
			End If
			C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha))
			lambdaP = lambda
			lambda = L + (1 - C) * f * sinAlpha * _
			 (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * (cos2SigmaM ^ 2))))
		End While
		If iterLimit < 1 Then Exit Function 'iteration limit has been reached, something didn't work
		uSq = cosSqAlpha * (low_a ^ 2 - low_b ^ 2) / (low_b ^ 2)
		upper_A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)))
		upper_B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))
		deltaSigma = upper_B * sinSigma * (cos2SigmaM + upper_B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM ^ 2) - upper_B / 6 * cos2SigmaM * (-3 + 4 * sinSigma ^ 2) * (-3 + 4 * cos2SigmaM ^ 2)))
		Return low_b * upper_A * (sigma - deltaSigma)
	End Function

	Public Enum CoordinatesFormats As Short
		WGS84Decimal = 1
		WGS84Minutes = 2
		WGS84Seconds = 3
		'WGS84UTM = 4
	End Enum

	Public Enum CreditPrices As Short
		ValidateCoordinates = 10		'=Coordinates already exist
		ConfirmedOrder = 10				'=Confirmation of bought caches
		OrderOfInvatedUser = 10			'=User Sign Up after invitation
		OrderCache = -50
	End Enum
	Public Enum CreditSources As Short '=Names Match CreditPrices
		OrderCache = 1
		ValidateCoordinates = 2
		ConfirmedOrder = 3
		OrderOfInvatedUser = 4
	End Enum

End Class