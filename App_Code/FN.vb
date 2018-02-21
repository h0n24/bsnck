Imports System.Web

Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class FN

	Class Cookies
		Class GeoCredits
			Public Shared CacheName As String = "GeoCreditsCache"
			Public Shared Sub Delete()
				FN.Cookies.Delete(CacheName)
			End Sub
		End Class

		Public Shared Sub WriteKeySession(ByRef Key As String, ByRef Value As String)
			WriteKey("Session", Key, Value)
		End Sub
		Public Shared Sub WriteKeyWeb(ByRef Key As String, ByRef Value As String)
			WriteKey("Web", Key, Value, Now.AddMonths(13))
		End Sub
		Public Shared Sub WriteKeyToday(ByRef Key As String, ByRef Value As String)
			WriteKey("Today", Key, Value, DateTime.Today.AddDays(1))
		End Sub

		Public Shared Sub Write(ByVal CookieName As String, ByVal CookieValue As String, Optional ByVal CookieExpires As Object = Nothing)
			Dim objCookie As New HttpCookie(CookieName)
			objCookie.Value = System.Web.HttpContext.Current.Server.UrlEncode(CookieValue)
			If Not CookieExpires Is Nothing Then
				objCookie.Expires = CookieExpires
			End If
			System.Web.HttpContext.Current.Response.Cookies.Set(objCookie)
		End Sub

		Public Shared Sub WriteKey(ByVal CookieName As String, ByVal CookieKey As String, ByVal CookieValue As String, Optional ByVal CookieExpires As Object = Nothing)
			Dim objCookie As New HttpCookie(CookieName)
			If Not System.Web.HttpContext.Current.Request.Cookies(CookieName) Is Nothing Then
				objCookie = System.Web.HttpContext.Current.Request.Cookies(CookieName)
			End If
			objCookie(CookieKey) = System.Web.HttpContext.Current.Server.UrlEncode(CookieValue)			 'Z·kÛdovat kv˘li problÈm˘m s Ëeötinou na ostr˝m serveru Forpsi
			If Not CookieExpires Is Nothing Then
				objCookie.Expires = CookieExpires
			End If
			System.Web.HttpContext.Current.Response.Cookies.Add(objCookie)
		End Sub

		Public Shared Function Read(ByVal CookieName As String, Optional ByVal CookieKey As String = Nothing) As String
			If Not System.Web.HttpContext.Current.Request.Cookies(CookieName) Is Nothing Then
				If CookieKey Is Nothing Then
					Return System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.Cookies(CookieName).Value)
				Else
					If Not System.Web.HttpContext.Current.Request.Cookies(CookieName)(CookieKey) Is Nothing Then
						Return System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.Cookies(CookieName)(CookieKey))
					Else
						Return ""
					End If
				End If
			Else
				Return ""
			End If
		End Function

		Public Shared Sub Delete(ByVal CookieName As String, Optional ByVal CookieKey As String = "")
			If CookieKey = "" Then
				Dim objCookie As New HttpCookie(CookieName)
				objCookie.Expires = DateTime.Now.AddDays(-1)
				System.Web.HttpContext.Current.Response.Cookies.Set(objCookie)
			Else
				Dim objCookie As HttpCookie = System.Web.HttpContext.Current.Request.Cookies(CookieName)
				objCookie.Values.Remove(CookieKey)
				System.Web.HttpContext.Current.Response.Cookies.Add(objCookie)
			End If
		End Sub

	End Class


	Class DB
		Class SQLBuilder
			Public Class [Select]
				Private _columns As String
				Private _from As String
				Private _where As String
				Private _orderby As String
				Private _top As Int64
				Public Sub New()
				End Sub
				Public Sub New(ByVal Columns As String)
					_columns = Columns
				End Sub
				Public Sub New(ByVal Columns As String, ByVal FROM As String)
					_columns = Columns
					_from = [FROM]
				End Sub
				Public Sub New(ByVal Columns As String, ByVal FROM As String, ByVal WHERE As String)
					_columns = Columns
					_from = [FROM]
					_where = WHERE
				End Sub
				Public Sub New(ByVal Columns As String, ByVal FROM As String, ByVal WHERE As String, ByVal ORDERBY As String)
					_columns = Columns
					_from = [FROM]
					_where = WHERE
					_orderby = ORDERBY
				End Sub
				Public Sub New(ByVal Columns As String, ByVal FROM As String, ByVal WHERE As String, ByVal ORDERBY As String, ByVal TOP As Int64)
					_columns = Columns
					_from = [FROM]
					_where = WHERE
					_orderby = ORDERBY
					_top = TOP
				End Sub
				Public Function BuildSQL() As String
					Dim S As String = "SELECT"
					If _top > 0 Then S &= " TOP " & _top
					If _columns <> "" Then S &= " " & _columns
					If _from <> "" Then S &= " FROM " & _from
					If _where <> "" Then S &= " WHERE " & _where
					If _orderby <> "" Then S &= " ORDER BY " & _orderby
					Return S
				End Function
				Public Property Columns() As String
					Get
						Return _columns
					End Get
					Set(ByVal value As String)
						_columns = value
					End Set
				End Property
				Public Property TOP() As Int64
					Get
						Return _top
					End Get
					Set(ByVal value As Int64)
						_top = value
					End Set
				End Property
			End Class
		End Class

		Class SqlWhere
			Public Text As String
			Sub New()
			End Sub
			Sub New(ByVal Where As String)
				Add(Where)
			End Sub
			Function Add(ByRef Where As String) As String
				If Text = "" Then
					Text = " WHERE " & Where
				Else
					Text &= " AND " & Where
				End If
				Return Text
			End Function
		End Class
		Class SqlSet
			Public Text As String
			Public Columns As String
			Function Add(ByRef SetItem As String) As String
				If Text = "" Then
					Text = " SET " & SetItem
				Else
					Text &= ", " & SetItem
				End If
				If Columns <> "" Then Columns &= ","
				Columns &= SetItem.Substring(0, SetItem.IndexOf("="))
				Return Text
			End Function
		End Class
		Class SqlInsert
			Public IdentityInsert As Boolean
			Dim Columns As String
			Dim Values As String
			Public ReadOnly Property Text() As String
				Get
					Return " (" & Columns & ") Values (" & Values & ")"
				End Get
			End Property
			Function Add(ByRef Column As String, ByRef Value As String) As String
				If Values <> "" Then Values &= ", "
				Values &= Value
				If Columns <> "" Then Columns &= ","
				Columns &= Column
				Return Text
			End Function

		End Class

		Class ODBC

			Public Shared Function GetConnectionString(Optional ByVal dbName As String = "abuxnet") As String
				If dbName = "prejcz" Or dbName = "prejcz1" Or dbName = "abuxnet" Then
					Return "DRIVER={SQL Server}; " & FN.DB.GetConnectionString(dbName)
				ElseIf dbName.IndexOf(".mdb") <> -1 Then
					Return "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Fog.Ini.PhysicalPaths.AppData & "\mdb\" & dbName & ";"
				End If
			End Function

			Public Shared Function Open(Optional ByVal dbName As String = "abuxnet") As System.Data.Odbc.OdbcConnection
				Dim DB As New System.Data.Odbc.OdbcConnection
				DB.ConnectionString = FN.DB.ODBC.GetConnectionString(dbName)
				DB.Open()
				Return DB
			End Function

		End Class

		Public Shared Function GetConnectionString(Optional ByVal dbName As String = "abuxnet") As String
			Dim url As New Fog.URL(HttpContext.Current.Request.Url)
			If url.isLocal Then
				Return System.Configuration.ConfigurationManager.ConnectionStrings("abuxnet.local").ConnectionString
			Else
				Return System.Configuration.ConfigurationManager.ConnectionStrings("abuxnet").ConnectionString
			End If
		End Function

		Public Shared Function Open(Optional ByVal dbName As String = "abuxnet") As System.Data.SqlClient.SqlConnection
			Dim DB As New System.Data.SqlClient.SqlConnection
			DB.ConnectionString = GetConnectionString(dbName)
			DB.Open()
			Return DB
		End Function

		Public Shared Function GetVal(ByRef Value As Object) As String
			Dim T As Val.Types.ValueTypes = Val.Types.Get(Value)
			Select Case T
				Case Val.Types.ValueTypes.Number
					Return Value
				Case Val.Types.ValueTypes.Text
					Return "'" & Value.Replace("'", "''") & "'"
				Case Val.Types.ValueTypes.Date
					Return "'" & CType(Value, Date).ToString("yyyyMMdd HH\:mm\:ss") & "'"
				Case Val.Types.ValueTypes.Nothing
					Return "NULL"
				Case Val.Types.ValueTypes.Boolean
					'NenÌ zatÌm pouûito
				Case Val.Types.ValueTypes.Object
					'NenÌ zatÌm pouûito
			End Select
		End Function

		Public Shared Function GetText(ByRef Value As String, Optional ByVal Length As Integer = -1) As String
			If Value Is Nothing Then
				Return "''"
			Else
				If Length = -1 Then
					Return "'" & Value.Replace("'", "''") & "'"
				Else
					Return "'" & Left(Value, Length).Replace("'", "''") & "'"
				End If
			End If
		End Function

		Public Shared Function GetDateTime(ByRef Value As DateTime, Optional ByVal Format As String = "MSSQL") As String
			Select Case Format
				Case "MSSQL", "MSDE" : Return "'" & Value.ToString("yyyyMMdd HH\:mm\:ss") & "'"
				Case "MDB" : Return "#" & Value.ToString("yyyy\/MM\/dd HH\:mm\:ss") & "#"
			End Select
		End Function

		Public Shared Function DatabaseTables() As DataTable
			Dim xmldoc As New System.Xml.XmlDataDocument
			xmldoc.DataSet.ReadXml(Fog.Ini.PhysicalPaths.AppData & "\DatabaseTables.xml")
			Dim dt As DataTable
			dt = xmldoc.DataSet.Tables("item")
			Return dt
		End Function

	End Class


	Public Class URL
		Public Class PredefinedURL	'Odhl·öenÌ uûivatele
			Public Shared Function UserLogOut() As String
				Return "/Users_Login.aspx?akce=logout"
			End Function
		End Class

		Public Shared Function GetRootUrl() As String
			Dim RootUrl As String = "http://" & My.Request.Url.Host
			If Not My.Request.Url.IsDefaultPort Then RootUrl &= ":" & My.Request.Url.Port
			If Not My.Request.ApplicationPath = "/" Then RootUrl &= My.Request.ApplicationPath
			Return RootUrl
		End Function

		Public Shared Function Referer() As String
			If System.Web.HttpContext.Current.Request.UrlReferrer Is Nothing Then
				Return ""
			Else
				Return System.Web.HttpContext.Current.Request.UrlReferrer.ToString()
			End If
		End Function

		Public Shared Function SetQueryFolder(ByVal URL As String, ByVal Nazev As String, ByVal Hodnota As String) As String
			URL = RemoveQueryFolder(URL, Nazev)
			Dim Start As Integer = InStrRev(URL, "/", URL.IndexOf(".aspx"))
			If Start <> -1 Then
				URL = URL.Insert(Start, Nazev & "-" & Hodnota & "/")
			End If
			Return URL
		End Function

		Public Shared Function SetQuery(ByVal URL As String, ByVal Nazev As String, ByVal Hodnota As String) As String
			URL = RemoveQuery(URL, Nazev)
			If URL.IndexOf("?") <> -1 Then
				URL &= "&" & Nazev & "=" & System.Web.HttpContext.Current.Server.UrlEncode(Hodnota)
			Else
				URL &= "?" & Nazev & "=" & System.Web.HttpContext.Current.Server.UrlEncode(Hodnota)
			End If
			Return URL
		End Function

		Public Shared Function RemoveQueryFolder(ByVal URL As String, ByVal Nazev As String) As String
			Dim Start As Integer = URL.IndexOf("/" & Nazev & "-")
			If Start <> -1 Then
				Dim Konec As Integer = URL.IndexOf("/", Start + 1)
				URL = URL.Remove(Start, Konec - Start)
			End If
			Return URL
		End Function

		Public Shared Function RemoveQuery(ByVal URL As String, ByVal Nazev As String) As String
			If URL.IndexOf("?") <> -1 Then
				Dim sQuery As String = "&" & URL.Substring(URL.IndexOf("?") + 1, URL.Length - URL.IndexOf("?") - 1)
				Dim Start As Integer = sQuery.IndexOf("&" & Nazev & "=")
				If Start <> -1 Then
					Dim Konec As Integer = sQuery.IndexOf("&", Start + 1)
					If Konec = -1 Then Konec = sQuery.Length
					sQuery = sQuery.Remove(Start, Konec - Start)
					URL = URL.Substring(0, URL.IndexOf("?"))
					If sQuery <> "" Then
						URL &= "?" & sQuery.Substring(1, sQuery.Length - 1)
					End If
				End If
			End If
			Return URL
		End Function

	End Class


	Class Users

		Class Prava
			Public Shared Function StatistikyOff() As Boolean
				If FN.Cookies.Read("Web", "StatOff") = Boolean.TrueString Then
					Return True
				Else
					Return False
				End If
			End Function
			Public Shared Function AuditOff() As Boolean
				If FN.Cookies.Read("Web", "AuditOff") = Boolean.TrueString Then
					Return True
				Else
					Return False
				End If
			End Function
			Public Shared Function ReklamaOff() As Boolean
				If FN.Cookies.Read("Web", "ReklOff") = Boolean.TrueString Then
					Return True
				Else
					Return False
				End If
			End Function
		End Class

	End Class

	Class Text

		Public Shared Function RandomString(ByVal Lenght As Integer) As String
			Dim S As String
			Randomize()
			For f As Integer = 1 To Lenght
				S &= Chr(Int(26 * Rnd()) + 65)
			Next
			Return S
		End Function

		Public Shared Function ShortThreeCRLF(ByRef S As String)
			Do While InStr(S, vbCrLf & vbCrLf & vbCrLf) > 0
				S = S.Replace(vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
			Loop
			Return S
		End Function

		Public Shared Function ShortFourEndings(ByRef S As String)
			Do While Right(S, 4) = Right(S, 1) & Right(S, 1) & Right(S, 1) & Right(S, 1)
				S = S.Remove(S.Length - 1, 1)
			Loop
			Return S
		End Function

		Public Shared Function ShortTwoSpaces(ByRef S As String)
			Do While InStr(S, "  ") > 0
				S = S.Replace("  ", " ")
			Loop
			Return S
		End Function

		Public Shared Function RemoveSpaceBeforeCRLF(ByRef S As String)
			S = S.Replace(" " & vbCrLf, vbCrLf)
			Return S
		End Function

		Class Test

			Public Shared Function VelkaPismena(ByVal Txt As String) As Integer
				'-- Vr·tÌ % velk˝ch pÌsmen
				Dim PocetVelkych As Integer = 0
				Dim f As Integer
				Dim Delka As Integer = Txt.Length
				If Delka > 0 Then
					For f = 0 To Txt.Length - 1
						If Txt.Substring(f, 1) <> Txt.Substring(f, 1).ToLower Then PocetVelkych += 1
					Next
				Else
					Delka = 1
				End If
				Return Int(PocetVelkych / Delka * 100)
			End Function

			Public Shared Function Diakritika(ByRef Txt As String) As Integer
				'-- Vr·tÌ % Ëesk˝ch znak˘
				Dim Cesky As Integer
				Dim Delka As Integer = Txt.Length
				If Delka > 0 Then
					Dim sDiakritika As String = "ÏöË¯û˝·ÌÈ˘˙ÔùÚÛÃä»ÿé›¡Õ…Ÿ⁄œç“”"
					Dim f As Integer
					For f = 0 To Delka - 1
						If sDiakritika.IndexOf(Txt.Substring(f, 1)) <> -1 Then Cesky += 1
					Next
					Return Int(Cesky / Delka * 100)
				Else
					Return 0
				End If
			End Function

		End Class

	End Class

	Class Datum

		Public Shared Function FormInput2Time(ByVal TimeString As Object)
			Dim sTime As String = "" & TimeString
			Dim TimeOut As Date
			Dim arr() As String = sTime.ToString.Split(":")
			If arr.Length = 2 Then
				Try
					If arr.Length = 3 Then
						TimeOut = TimeSerial(arr(0), arr(1), arr(2))
					ElseIf arr.Length = 2 Then
						TimeOut = TimeSerial(arr(0), arr(1), 0)
					End If
					Return TimeOut
				Catch
				End Try
			End If
		End Function

		Public Shared Function FormInput2Date(ByVal DatumString As Object)
			Dim Datum As String = "0" & DatumString
			Datum = Datum.Replace(" ", "")
			Dim DatumOut As Date
			Dim arr() As String = Datum.Split(".")
			If arr.Length = 3 Then
				If arr(2) = "" Then arr(2) = Now.Year
				Try
					DatumOut = DateSerial(arr(2), arr(1), arr(0))
					If Day(DatumOut) = CInt(arr(0)) And Month(DatumOut) = CInt(arr(1)) Then
						Return DatumOut
					End If
				Catch
				End Try
			End If
		End Function

	End Class


	Class Array

		Public Shared Function PreskupitArrayList(ByVal arrSrc As ArrayList, ByVal Vynechat As Integer)
			Dim arrDest As New ArrayList
			Dim r As Integer
			Randomize()
			While arrSrc.Count > Vynechat
				r = Int(arrSrc.Count * Rnd())
				arrDest.Add(arrSrc(r))
				arrSrc.RemoveAt(r)
			End While
			Return arrDest
		End Function

	End Class


	Class Cache

		Public Shared Function dtGeocacheTypes() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtGeocacheTypes"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("CacheTypes")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT TypeID,TypeName,TypeCredits FROM GeocacheTypes", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtGeocacheTypes", dt, Nothing, Now.AddMinutes(5), TimeSpan.Zero, Caching.CacheItemPriority.NotRemovable, Nothing)
			End If
			Return dt
		End Function

		Public Shared Function dtBlokovatUzivatele() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtBlokovatUzivatele"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("BlokovatUzivatele")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT Uzivatel FROM BlokovatUzivatele", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtBlokovatUzivatele", dt, Nothing, Now.AddMinutes(4), TimeSpan.Zero, Caching.CacheItemPriority.NotRemovable, Nothing)
			End If
			Return dt
		End Function

		Public Shared Function dtBlokovatIP() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtBlokovatIP"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("BlokovatIP")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT IP FROM BlokovatIP", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtBlokovatIP", dt, Nothing, Now.AddMinutes(5), TimeSpan.Zero, Caching.CacheItemPriority.NotRemovable, Nothing)
			End If
			Return dt
		End Function

		Public Shared Function dtSvatky() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtSvatky"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("Svatky")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT Den,Svatek FROM Svatky", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtSvatky", dt)
			End If
			Return dt
		End Function

		Public Shared Function dtSekce() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtSekce"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("Sekce")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Sekce ORDER BY SekceAlias", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtSekce", dt)
			End If
			Return dt
		End Function

		Public Shared Function dtKategorie() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtKategorie"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("Kategorie")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Kategorie", FN.DB.GetConnectionString)
				DBDA.Fill(dt)
				DBDA.Dispose()
				System.Web.HttpContext.Current.Cache.Insert("dtKategorie", dt)
			End If
			Return dt
		End Function

		Public Shared Function dtReklamaBannery() As DataTable
			Dim dt As DataTable = DirectCast(System.Web.HttpContext.Current.Cache.Get("dtReklamaBannery"), DataTable)
			If dt Is Nothing Then
				dt = New DataTable("dtReklamaBannery")
				dt.Columns.Add("BannerID", System.Type.GetType("System.Int16"))
				dt.Columns.Add("BannerKlient", System.Type.GetType("System.Int16"))
				dt.Columns.Add("BannerTyp", System.Type.GetType("System.String"))
				dt.Columns.Add("BannerRozmery", System.Type.GetType("System.String"))
				dt.Columns.Add("BannerURL", System.Type.GetType("System.String"))
				dt.Columns.Add("BannerSoubor", System.Type.GetType("System.String"))
				dt.Columns.Add("BannerTxt", System.Type.GetType("System.String"))
				'System.Web.HttpContext.Current.Cache.Insert("dtReklamaBannery", dt)
			End If
			Return dt
		End Function

		Public Shared Function dsReklamaXml() As DataSet
			Dim ds As DataSet = DirectCast(System.Web.HttpContext.Current.Cache.Get("dsReklamaXml"), DataSet)
			If ds Is Nothing Then
				Dim xmldoc As New System.Xml.XmlDataDocument
				xmldoc.DataSet.ReadXml(Fog.Ini.PhysicalPaths.AppData & "/ReklamaPozice.xml")
				ds = xmldoc.DataSet
				System.Web.HttpContext.Current.Cache.Insert("dsReklamaXml", ds, Nothing, Now.AddMinutes(10), TimeSpan.Zero, Caching.CacheItemPriority.High, Nothing)
			End If
			Return ds
		End Function

		Public Shared Function dsSeznamkaDataXml() As DataSet
			Dim ds As DataSet = DirectCast(System.Web.HttpContext.Current.Cache.Get("dsSeznamkaDataXml"), DataSet)
			If ds Is Nothing Then
				Dim xmldoc As New System.Xml.XmlDataDocument
				xmldoc.DataSet.ReadXml(Fog.Ini.PhysicalPaths.AppData & "/SeznamkaData.xml")
				ds = xmldoc.DataSet
				System.Web.HttpContext.Current.Cache.Insert("dsSeznamkaDataXml", ds, Nothing, Now.AddDays(1), TimeSpan.Zero, Caching.CacheItemPriority.High, Nothing)
			End If
			Return ds
		End Function

		Public Shared Function dtChatUsersOnline() As DataTable
			System.Web.HttpContext.Current.Application.Lock()
			If System.Web.HttpContext.Current.Application("dtChatUsersOnline") Is Nothing Then
				Dim dt As New DataTable
				dt.Columns.Add("Room", System.Type.GetType("System.Int32"))
				dt.Columns.Add("UserID", System.Type.GetType("System.Int64"))
				dt.Columns.Add("UserNick", System.Type.GetType("System.String"))
				dt.Columns.Add("Expirace", System.Type.GetType("System.DateTime"))
				dt.Columns.Add("Zacatek", System.Type.GetType("System.DateTime"))
				System.Web.HttpContext.Current.Application("dtChatUsersOnline") = dt
				Return dt
			Else
				Return System.Web.HttpContext.Current.Application("dtChatUsersOnline")
			End If
			System.Web.HttpContext.Current.Application.UnLock()
		End Function

		Public Shared Sub ClearSekce(ByVal SekceAlias As String)
			Dim Sekce As New Fog.Sekce(SekceAlias)
			System.Web.HttpContext.Current.Cache.Remove("Kategorie." & SekceAlias)
			System.Web.HttpContext.Current.Cache.Remove("dtRss" & SekceAlias)
			If Sekce.Tabulka.isTxtDila Then
				System.Web.HttpContext.Current.Cache.Remove("dtRssDila")
			End If
		End Sub

	End Class


	Class Email

		Public Shared Function SendMessage(ByRef msg As System.Net.Mail.MailMessage) As Boolean
			msg.BodyEncoding = System.Text.Encoding.UTF8
			msg.SubjectEncoding = System.Text.Encoding.UTF8
			Dim MyIni As New Fog.Ini.Init
			Dim smtp As New System.Net.Mail.SmtpClient()
			Select Case System.Configuration.ConfigurationManager.AppSettings(IIf(MyIni.RunLocal, "Mail.Method.local", "Mail.Method"))
				Case "smtp"
					smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
					smtp.Host = System.Configuration.ConfigurationManager.AppSettings(IIf(MyIni.RunLocal, "Mail.SmtpServer.local", "Mail.SmtpServer"))
				Case "PickupDirectory"
					smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
					smtp.PickupDirectoryLocation = System.Configuration.ConfigurationManager.AppSettings(IIf(MyIni.RunLocal, "Mail.PickupDirectory.local", "Mail.PickupDirectory"))
			End Select
			Try
				smtp.Send(msg)
				Return True
			Catch ex As Exception
			End Try
		End Function

		Public Shared Function IsEmailValid(ByVal Email As String) As Boolean
			Dim blnValid As Boolean = System.Text.RegularExpressions.Regex.IsMatch(Email, "^([\w-\.]+)@([\w-\.]+)\.([a-zA-Z]{2,4})$")
			If Email.IndexOf(".@") <> -1 Then blnValid = False
			If Email.IndexOf(" ") <> -1 Then blnValid = False
			If FN.Text.Test.Diakritika(Email) > 0 Then blnValid = False
			Return blnValid
		End Function

	End Class


	Public Shared Sub EngineErrorLogWrite(ByVal Kod As Integer, Optional ByVal Zdroj As String = "", Optional ByVal Popis As String = "")
		HttpContext.Current.Application("ErrorLogCount") += 1
		'DodÏlat: Pouze jeden z·znam dennÏ + poËÌtadlo + referer&""
	End Sub

	Public Shared Sub Redir(ByRef RedirUrl As String, Optional ByRef RedirUrlElse As String = "/Default.aspx")
		If RedirUrl <> "" Then
			System.Web.HttpContext.Current.Response.Redirect(RedirUrl)
		Else
			If RedirUrlElse = "" Or RedirUrlElse Is Nothing Then Exit Sub
			System.Web.HttpContext.Current.Response.Redirect(RedirUrlElse)
		End If
	End Sub

	Class Crypting
		Private Shared Default_KEY_192() As Byte = {80, 26, 250, 155, 112, 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 2, 251, 11, 204, 119, 35, 184, 197}
		Private Shared Default_IV_192() As Byte = {62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 55, 103, 246, 79, 36, 99, 167, 3, 42, 247, 173, 10, 121, 222}

		Public Shared Function EncryptMD5(ByRef Text As String) As String
			Dim CSProvider As New System.Security.Cryptography.MD5CryptoServiceProvider
			Dim Hash() As Byte = CSProvider.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Text))
			Dim S As String
			For Each i As Byte In Hash
				S &= Hex(i)
			Next
			Return S
		End Function

		Public Shared Function EncryptTripleDES(ByRef OriginalString As String) As String
			Return EncryptTripleDES(OriginalString, Default_KEY_192, Default_IV_192)
		End Function
		Public Shared Function EncryptTripleDES(ByRef OriginalString As String, ByVal Key() As Byte, ByVal InitVectror() As Byte) As String
			If String.IsNullOrEmpty(OriginalString) Then Return Nothing
			Dim CSProvider As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
			Dim MStream As New System.IO.MemoryStream()
			Dim CStream As New System.Security.Cryptography.CryptoStream(MStream, CSProvider.CreateEncryptor(Key, InitVectror), System.Security.Cryptography.CryptoStreamMode.Write)
			Dim SWriter As New System.IO.StreamWriter(CStream)
			SWriter.Write(OriginalString)
			SWriter.Flush()
			CStream.FlushFinalBlock()
			MStream.Flush()
			Return Convert.ToBase64String(MStream.GetBuffer(), 0, MStream.Length)
		End Function

		Public Shared Function DecryptTripleDES(ByRef EncodedString As String) As String
			Return DecryptTripleDES(EncodedString, Default_KEY_192, Default_IV_192)
		End Function
		Public Shared Function DecryptTripleDES(ByVal EncodedString As String, ByVal Key() As Byte, ByVal InitVectror() As Byte) As String
			If String.IsNullOrEmpty(EncodedString) Then Return Nothing
			Dim CSProvider As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
			Dim MStream As New System.IO.MemoryStream(Convert.FromBase64String(EncodedString))
			Dim CStream As New System.Security.Cryptography.CryptoStream(MStream, CSProvider.CreateDecryptor(Key, InitVectror), System.Security.Cryptography.CryptoStreamMode.Read)
			Dim SReader As StreamReader = New StreamReader(CStream)
			Return SReader.ReadToEnd()
		End Function

	End Class

End Class