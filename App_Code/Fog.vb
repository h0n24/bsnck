Namespace Fog

	Public Class Kategorie
		Private _sekce As Sekce
		Private dRow As DataRow
		Public Sub New()
		End Sub
		Public Sub New(ByVal Kategorie As Object)
			Init(Kategorie)
		End Sub
		Private Sub Init(ByVal Kategorie As Object)
			_sekce = Nothing
			Dim dRows As DataRow() = FN.Cache.dtKategorie.Select("KatID=" & Val.ToInt(Kategorie))
			If dRows.Length > 0 Then dRow = dRows(0)
		End Sub
		Public Property ID() As Short
			Get
				If Valid Then Return dRow("KatID")
			End Get
			Set(ByVal value As Short)
				Init(value)
			End Set
		End Property
		Public ReadOnly Property Nazev() As String
			Get
				If Valid Then Return dRow("KatNazev")
			End Get
		End Property
		Public ReadOnly Property Priorita() As Short
			Get
				If Valid Then Return dRow("KatPriorita")
			End Get
		End Property
		Public ReadOnly Property Skupina() As String
			Get
				If Valid Then Return dRow("KatSkupina")
			End Get
		End Property
		Public ReadOnly Property Funkce() As Short
			Get
				If Valid Then Return dRow("KatFunkce")
			End Get
		End Property
		Public ReadOnly Property Valid() As Boolean
			Get
				Return Not (dRow Is Nothing)
			End Get
		End Property
		Public Property Sekce() As Sekce
			Get
				If _sekce Is Nothing Then
					Dim SekceAlias As String
					If Valid Then SekceAlias = dRow("KatSekce")
					_sekce = New Sekce(SekceAlias)
				End If
				Return _sekce
			End Get
			Set(ByVal value As Sekce)
				_sekce = value
			End Set
		End Property
		Public Function isNezarazene() As Boolean
			If Valid Then Return Funkce = 1
		End Function
		Public Function isSmazane() As Boolean
			If Valid Then Return Funkce = 2
		End Function

	End Class

	Public Class Sekce
		Private _tabulka As Tabulka
		Private dRow As DataRow
		Public Sub New()
		End Sub
		Public Sub New(ByVal Sekce As Object)
			Init(Sekce)
		End Sub
		Private Sub Init(ByVal Sekce As Object)
			_tabulka = Nothing
			Dim dRows As DataRow()
			If IsNumeric(Sekce) Then
				dRows = FN.Cache.dtSekce.Select("SekceID=" & Sekce)
			Else
				dRows = FN.Cache.dtSekce.Select("SekceAlias='" & Sekce & "'")
			End If
			If dRows.Length > 0 Then dRow = dRows(0)
		End Sub
		Public Property ID() As Short
			Get
				If Valid Then Return dRow("SekceID")
			End Get
			Set(ByVal value As Short)
				Init(value)
			End Set
		End Property
		Public Property [Alias]() As String
			Get
				If Valid Then Return dRow("SekceAlias")
			End Get
			Set(ByVal value As String)
				Init(value)
			End Set
		End Property
		Public ReadOnly Property Nazev() As String
			Get
				If Valid Then Return dRow("SekceNazev")
			End Get
		End Property
		Public ReadOnly Property J1P() As String
			Get
				If Valid Then Return dRow("SekceJ1P")
			End Get
		End Property
		Public ReadOnly Property J4P() As String
			Get
				If Valid Then Return dRow("SekceJ4P")
			End Get
		End Property
		Public ReadOnly Property Valid() As Boolean
			Get
				Return Not (dRow Is Nothing)
			End Get
		End Property
		Public Property Tabulka() As Tabulka
			Get
				If _tabulka Is Nothing Then
					Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceID=" & ID)
					Dim TabulkaID As Integer
					If dRows.Length > 0 Then TabulkaID = dRows(0)("SekceTabulka")
					_tabulka = New Tabulka(TabulkaID)
				End If
				Return _tabulka
			End Get
			Set(ByVal value As Tabulka)
				_tabulka = value
			End Set
		End Property
		Public Function EditorNeni() As Boolean
			If Valid Then Return Array.IndexOf(System.Configuration.ConfigurationManager.AppSettings("Sekce.EditorNeni").Split(","), [Alias]) <> -1
		End Function
		Public Function FindKategorieNew() As Short
			Dim dRows As DataRow() = FN.Cache.dtKategorie.Select("KatSekce=" & ID & " AND KatFunkce=1")
			If dRows.Length > 0 Then Return dRows(0)("KatID")
		End Function
		Public Function FindKategorieDeleted() As Short
			Dim dRows As DataRow() = FN.Cache.dtKategorie.Select("KatSekce=" & ID & " AND KatFunkce=2")
			If dRows.Length > 0 Then Return dRows(0)("KatID")
		End Function
		Public Shared Function NazevToID(ByVal Nazev As String) As String
			Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceAlias='" & Nazev & "'")
			Return dRows(0)("SekceID")
		End Function
	End Class

	Public Class Tabulka
		Private dRow As DataRow
		Public Sub New()
		End Sub
		Public Sub New(ByVal Tabulka As Object)
			Init(Tabulka)
		End Sub
		Private Sub Init(ByVal Tabulka As Object)
			Dim dRows As DataRow()
			If IsNumeric(Tabulka) Then
				dRows = FN.Cache.dtSekce.Select("SekceTabulka=" & Tabulka)
			Else
				dRows = FN.Cache.dtSekce.Select("SekceTable='" & Tabulka & "'")
			End If
			If dRows.Length > 0 Then dRow = dRows(0)
		End Sub
		Public Property ID() As Short
			Get
				If Valid Then Return dRow("SekceTabulka")
			End Get
			Set(ByVal value As Short)
				Init(value)
			End Set
		End Property
		Public Property Nazev() As String
			Get
				If Valid Then Return dRow("SekceTable")
			End Get
			Set(ByVal value As String)
				Init(value)
			End Set
		End Property
		Public ReadOnly Property Valid() As Boolean
			Get
				Return Not (dRow Is Nothing)
			End Get
		End Property
		Public Function isTxtDila() As Boolean
			If Valid Then Return dRow("SekceTable") = "TxtDila"
		End Function
		Public Function isTxtLong() As Boolean
			If Valid Then Return dRow("SekceTable") = "TxtLong"
		End Function
		Public Function isTxtShort() As Boolean
			If Valid Then Return dRow("SekceTable") = "TxtShort"
		End Function
		Public Function isTxtCitaty() As Boolean
			If Valid Then Return dRow("SekceTable") = "TxtCitaty"
		End Function
		Public Function isSbirky() As Boolean
			If Valid Then Return dRow("SekceTable") = "Sbirky"
		End Function
		Public Function isPohlednice() As Boolean
			If Valid Then Return dRow("SekceTable") = "Pohlednice"
		End Function
		Public Shared Function NazevToID(ByVal Nazev As String) As String
			Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceTable='" & Nazev & "'")
			Return dRows(0)("SekceTabulka")
		End Function
		Public Function hasKomentare() As Boolean
			If Valid Then Return isTxtLong() Or isTxtDila() Or isSbirky()
		End Function
		Public Function hasTipy() As Boolean
			If Valid Then Return isTxtDila() Or isSbirky()
		End Function
		Public Function hasTitulek() As Boolean
			If Valid Then Return isTxtLong() Or isTxtDila() Or isSbirky()
		End Function
		Public Function hasAnotace() As Boolean
			If Valid Then Return isTxtDila()
		End Function
		Public Function hasHodnoceni() As Boolean
			If Valid Then Return isTxtLong() Or isTxtShort() Or isTxtCitaty() Or isPohlednice()
		End Function

	End Class

	Public Class Ini

		Public Const EmailAdmin As String = "web@liter.cz"

		Public Class Init
			Public RunLocal As Boolean
			Dim PageData As New Ini.Page
			Dim WebData As New Ini.Web
			Dim ColorsData As New Ini.Colors
			Sub New()
				InitValues()
			End Sub

			Private Sub InitValues()
				Dim URL As New Fog.URL(HttpContext.Current.Request.Url)
				If URL.isLocal Then
					RunLocal = True
				End If

				Select Case URL.Domain2
					Case "prej.cz", "citaty-osobnosti.cz"
						If URL.Domain2 = "prej.cz" Then
							WebData.ID = "prejcz"
							WebData.Name = "Přej.cz"
							WebData.Slogan = "Elektronické Pohlednice, Přáníčka, SMS Přání"
							WebData.Description = "Elektronické pohlednice a pohledy,SMS přání a přáníčka k různým příležitostem: vánoční,velikonoční,k narozeninám,k svátku,novoroční,svatební aj."
							WebData.Keywords = "pohlednice,elektronická přání,přáníčka,pohledy,elektronické pohlednice,internetové,erotické,virtuální,animované,SMS,smsky,MMS,k svátku,k narozeninám,vánoce,vánoční,PF,novoroční,svatební,z lásky,kondolence,texty"
							WebData.URL = "http://prej.cz"
							WebData.Email = "web@prej.cz"
						ElseIf URL.Domain2 = "citaty-osobnosti.cz" Then
							WebData.ID = "citujcz"
							WebData.Name = "Citáty-Osobností.cz"
							WebData.Slogan = "Citáty, Motta, Přísloví, Výroky, Myšlenky"
							WebData.Description = "Citáty, Motta, Přísloví, Výroky, Myšlenky, Aforismy"
							WebData.Keywords = "citáty,motta,přísloví,výroky,myšlenky,aforismy"
							WebData.URL = "http://citaty-osobnosti.cz"
							WebData.Email = "web@citaty-osobnosti.cz"
						End If
						ColorsData.Bg = "#E0F0FF"
						ColorsData.Text = "#000000"
						ColorsData.AHref = "#3333AA"
						ColorsData.AHrefOver = "#993333"
						ColorsData.AHrefVisited = "#774444"
						ColorsData.Box1Bg = "#B0D0E0"
						ColorsData.Box1Title = "#99BBBB"
						ColorsData.Box1Border = "#333333"
						ColorsData.Box2Bg = ColorsData.Bg
						ColorsData.Box2Title = "#C0D0E0"
						ColorsData.Box2Border = "#777777"
						ColorsData.Anketa = "#B8D8E8,#C8D8E8"
					Case "basne.cz"
						WebData.ID = "basnecz"
						WebData.Name = "Básně.cz"
						WebData.Slogan = "Básně, Povídky, Úvahy, Pohádky, Fejetony, Romány, Reportáže"
						WebData.Description = "Literární server: Básně,Básničky,Povídky,Poezie,Úvahy,Pohádky,Romány,elektronické knihy, recenze knih"
						WebData.Keywords = "básně,básničky,poezie,povídky,poezie,úvahy,pohádky,romány,elektronické knihy"
						WebData.URL = "http://basne.cz"
						WebData.Email = "web@basne.cz"
						ColorsData.Bg = "#002757"
						ColorsData.Text = "#FFFFFF"
						ColorsData.AHref = "#FFA244"
						ColorsData.AHrefOver = "#FF4020"
						ColorsData.AHrefVisited = "#AAAAAA"
						ColorsData.Box1Bg = "#001D49"
						ColorsData.Box1Title = "#001541"
						ColorsData.Box1Border = "#777777"
						ColorsData.Box2Bg = ColorsData.Bg
						ColorsData.Box2Title = "#002048"
						ColorsData.Box2Border = "#777777"
						ColorsData.Anketa = "#002860,#003373"
					Case "basnicky.cz", "cituj.cz"
						ColorsData.Bg = "#FFFFFF"
						ColorsData.Text = "#000000"
						ColorsData.AHref = "#773333"
						ColorsData.AHrefOver = "#333399"
						ColorsData.AHrefVisited = "#555588"
						ColorsData.Box1Bg = "#F1F0D9"
						ColorsData.Box1Title = "#E5E2C8"
						ColorsData.Box1Border = "#999999"
						ColorsData.Box2Bg = ColorsData.Bg
						ColorsData.Box2Title = ColorsData.Box1Bg
						ColorsData.Box2Border = ColorsData.Box1Border
						ColorsData.Anketa = "#00387D,#003373"
						If URL.Domain2 = "basnicky.cz" Then
							WebData.ID = "basnickycz"
							WebData.Name = "Básničky.cz"
							WebData.Slogan = "Básničky, Poezie, Verše"
							WebData.Description = "Nejen milostné básně,zamilované básničky,verše"
							WebData.Keywords = "milostné básně,zamilované básničky,romantické básničky,poezie,verše,basne,basnicky"
							WebData.URL = "http://basnicky.cz"
							WebData.Email = "web@basnicky.cz"
						ElseIf URL.Domain2 = "cituj.cz" Then
							WebData.ID = "citujcz"
							WebData.Name = "Cituj.cz"
							WebData.Slogan = "Citáty, Motta, Přísloví, Výroky, Myšlenky"
							WebData.Description = "Citáty, Motta, Přísloví, Výroky, Myšlenky, Aforismy"
							WebData.Keywords = "citáty,motta,přísloví,výroky,myšlenky,aforismy"
							WebData.URL = "http://cituj.cz"
							WebData.Email = "web@cituj.cz"
						End If
					Case "abux.net"
						WebData.ID = "abuxnet"
						WebData.Name = "ABUX.net"
						WebData.Slogan = "Geocache Final Coordinates and Solutions"
						WebData.Description = "Geocache Final Coordinates and Solutions"
						WebData.Keywords = "Geocaching, Geocache, Final, Locations, Coordinates, Solution, Unknown, Mystery, Multi"
						WebData.URL = "http://abux.net"
						WebData.Email = "web@abux.net"
						ColorsData.Bg = "#FFFFFF"
						ColorsData.Text = "#000000"
						ColorsData.AHref = "#BB3333"
						ColorsData.AHrefOver = "#333399"
						ColorsData.AHrefVisited = "#995588"
						ColorsData.Box1Bg = "#F2EDE6"
						ColorsData.Box1Title = "#E5DCCF"
						ColorsData.Box1Border = "#777777"
						ColorsData.Box2Bg = ColorsData.Bg
						ColorsData.Box2Title = "#D9CBB8"
						ColorsData.Box2Border = "#777777"
						ColorsData.Anketa = "#00387D,#003373"
					Case Else
						WebData.ID = "dosolcz"
						WebData.Name = "Dosol.cz"
						WebData.Slogan = "Dosol.cz"
						WebData.Description = "Dosol"
						WebData.Keywords = "Dosol"
						WebData.URL = "http://dosol.cz"
						WebData.Email = "web@dosol.cz"
						ColorsData.Bg = "#FFFFFF"
						ColorsData.Text = "#000000"
						ColorsData.AHref = "#BB3333"
						ColorsData.AHrefOver = "#333399"
						ColorsData.AHrefVisited = "#995588"
						ColorsData.Box1Bg = "#F2EDE6"
						ColorsData.Box1Title = "#E5DCCF"
						ColorsData.Box1Border = "#777777"
						ColorsData.Box2Bg = ColorsData.Bg
						ColorsData.Box2Title = "#D9CBB8"
						ColorsData.Box2Border = "#777777"
						ColorsData.Anketa = "#00387D,#003373"
				End Select
				WebData.EmailAutomat = "automat@" & WebData.Email.Split("@")(1)
			End Sub

			Public ReadOnly Property Page() As Ini.Page
				Get
					Return PageData
				End Get
			End Property

			Public ReadOnly Property Web() As Ini.Web
				Get
					Return WebData
				End Get
			End Property

			Public ReadOnly Property Colors() As Ini.Colors
				Get
					Return ColorsData
				End Get
			End Property

		End Class

		Public Class PhysicalPaths

			Public Shared Function Root() As String
				Return System.Web.HttpContext.Current.Server.MapPath("~/")
			End Function

			Public Shared Function Data() As String
				Dim Dir As New IO.DirectoryInfo(Fog.Ini.PhysicalPaths.Root & "\..\data")
				Return Dir.ToString
			End Function

			Public Shared Function AppData() As String
				Dim Dir As New IO.DirectoryInfo(Fog.Ini.PhysicalPaths.Root & "\App_Data")
				Return Dir.ToString
			End Function

		End Class

		Public Class Page
			Public BeginRequest As Date

			Public Property Title() As String
				Get
					Return "" & HttpContext.Current.Items("Ini.Page.Title")
				End Get
				Set(ByVal Value As String)
					HttpContext.Current.Items("Ini.Page.Title") = Value
				End Set
			End Property
			Public Function TitleHead() As String
				Dim Ini As New Ini.Init
				If Ini.RunLocal Then
					Return "## " & Ini.Web.Name & " » " & Title
				Else
					Return Ini.Web.Name & " » " & Title
				End If
			End Function
		End Class

		Public Class Web
			Public ID As String
			Public Name As String
			Public URL As String
			Public Email As String
			Public EmailAutomat As String
			Public Slogan As String
			Public Description As String
			Public Keywords As String
		End Class

		Public Class Colors
			Public Bg As String
			Public Text As String
			Public AHref As String
			Public AHrefOver As String
			Public AHrefVisited As String
			Public Box1Bg As String
			Public Box1Title As String
			Public Box1Border As String
			Public Box2Bg As String
			Public Box2Title As String
			Public Box2Border As String
			Public Anketa As String
		End Class

	End Class

	Public Class Seznam
		Private _Text As String
		Public Property Text() As String
			Get
				Return _Text
			End Get
			Set(ByVal Value As String)
				_Text = Value
			End Set
		End Property
		Public ReadOnly Property Count() As Int16
			Get
				If _Text = "" Then
					Return 0
				Else
					Return _Text.Split(",").Length
				End If
			End Get
		End Property
		Sub New()
			_Text = ""
		End Sub
		Sub New(ByVal Text As String)
			_Text = Text
		End Sub

		Public Sub Add(ByVal Value As String)
			If _Text.Length = 0 Then
				_Text = Value
			ElseIf System.Array.IndexOf(_Text.Split(","), Value) = -1 Then
				_Text &= "," & Value
			End If
		End Sub

		Public Sub Remove(ByVal Index As String)
			Dim IndexStart As Int16
			While Index > 0
				IndexStart = _Text.IndexOf(",", IndexStart) + 1
				Index = Index - 1
			End While
			Dim IndexEnd As Integer = (_Text & ",").IndexOf(",", IndexStart)
			If IndexStart <> 0 Then IndexStart = IndexStart - 1 'První záznam nemá čárku
			If IndexEnd = _Text.Length Then IndexEnd = IndexEnd - 1 'Poslední záznam nemá čárku
			_Text = _Text.Remove(IndexStart, IndexEnd + 1 - IndexStart)
		End Sub

		Public Function IndexOf(ByVal ValueToFind As String) As Int16
			Return System.Array.IndexOf(_Text.Split(","), ValueToFind)
		End Function

	End Class

	Public Class User
		Public ID As Integer
		Public Nick As String = ""
		Public Jmeno As String = ""
		Public Email As String = ""
		Public EmailVerified As Boolean
		Public Autor As Boolean
		Public AdminID As Integer
		Public AdminSekce As String = ""
		Public AdminAkce As String = ""
		Private _premium As Boolean

		Public Property Premium() As Boolean
			Get
				Return _premium
			End Get
			Set(ByVal value As Boolean)
				_premium = value
			End Set
		End Property
		Sub New()
			ReadUserFromCookies()
		End Sub

		Public Function isAdmin() As Boolean
			If AdminID > 0 Then Return True
		End Function
		Public Function isLogged() As Boolean
			If ID > 0 Then Return True
		End Function

		Private Sub ReadUserFromCookies()
			If IsNothing(System.Web.HttpContext.Current.Request.Cookies("User")) Then
				Exit Sub
			End If
			Dim S As String = FN.Cookies.Read("User", "ID") & FN.Cookies.Read("User", "Email") & FN.Cookies.Read("User", "Nick") & FN.Cookies.Read("User", "Jmeno") & FN.Cookies.Read("User", "Autor") & FN.Cookies.Read("User", "AdminID") & FN.Cookies.Read("User", "AdminSekce") & FN.Cookies.Read("User", "AdminAkce") & FN.Cookies.Read("User", "Premium")
			If FN.Cookies.Read("User", "H") = FN.Crypting.EncryptMD5(S & "Salt inside User Cookies!") Then
				ID = FN.Cookies.Read("User", "ID")
				Jmeno = FN.Cookies.Read("User", "Jmeno")
				Nick = FN.Cookies.Read("User", "Nick")
				Email = FN.Cookies.Read("User", "Email")
				Autor = Val.ToBoolean(FN.Cookies.Read("User", "Autor"))
				AdminID = Val.ToInt(FN.Cookies.Read("User", "AdminID"))
				AdminSekce = FN.Cookies.Read("User", "AdminSekce")
				AdminAkce = FN.Cookies.Read("User", "AdminAkce")
				Premium = Val.ToBoolean(FN.Cookies.Read("User", "Premium"))
				OnlineAdd()
			End If
		End Sub

		Public Overloads Sub WriteUserCookies()
			WriteUserCookies(Today.AddDays(1).AddHours(3))
		End Sub
		Public Overloads Sub WriteUserCookies(ByVal Expirace As Date)
			FN.Cookies.WriteKey("User", "ID", ID)
			FN.Cookies.WriteKey("User", "Email", Email)
			FN.Cookies.WriteKey("User", "Nick", Nick)
			FN.Cookies.WriteKey("User", "Jmeno", Jmeno)
			FN.Cookies.WriteKey("User", "Autor", Autor.ToString)
			FN.Cookies.WriteKey("User", "AdminID", AdminID)
			FN.Cookies.WriteKey("User", "AdminSekce", AdminSekce)
			FN.Cookies.WriteKey("User", "AdminAkce", AdminAkce)
			FN.Cookies.WriteKey("User", "Premium", Premium)
			FN.Cookies.WriteKey("User", "H", FN.Crypting.EncryptMD5(ID & Email & Nick & Jmeno & Autor.ToString & AdminID & AdminSekce & AdminAkce & Premium & "Salt inside User Cookies!"))
			FN.Cookies.WriteKey("User", "Expire", Expirace, Expirace)
		End Sub

		Public Function isAdminSekce(ByRef Sekce As String) As Boolean
			If Array.IndexOf(AdminSekce.Split(","), Sekce) <> -1 Or AdminSekce = "ALL" Then Return True
		End Function

		Public ReadOnly Property OnlineUsers() As DataTable
			Get
				System.Web.HttpContext.Current.Application.Lock()
				If System.Web.HttpContext.Current.Application("OnlineUsers") Is Nothing Then
					Dim dt As New DataTable
					dt.Columns.Add("UserID", System.Type.GetType("System.Int64"))
					dt.Columns.Add("UserNick", System.Type.GetType("System.String"))
					dt.Columns.Add("Expire", System.Type.GetType("System.DateTime"))
					dt.Columns.Add("Autor", System.Type.GetType("System.Boolean"))
					dt.Columns.Add("Domain", System.Type.GetType("System.String"))
					System.Web.HttpContext.Current.Application("OnlineUsers") = dt
					OnlineUsers = dt
				Else
					OnlineUsers = System.Web.HttpContext.Current.Application("OnlineUsers")
					OnlineUsers.BeginLoadData()
					'-- Promaže neaktivní uživatelé online
					If System.Web.HttpContext.Current.Application("OnlineUsersExpireCheck") Is Nothing Then
						System.Web.HttpContext.Current.Application("OnlineUsersExpireCheck") = Now.AddSeconds(5)
					Else
						Dim CheckTime As DateTime = System.Web.HttpContext.Current.Application("OnlineUsersExpireCheck")
						If CheckTime < Now Then
							System.Web.HttpContext.Current.Application("OnlineUsersExpireCheck") = Now.AddSeconds(5)							'-- Promazávat po X vteřinách
							Dim dRows() As DataRow = OnlineUsers.Select(Nothing, "Expire")
							If dRows.Length > 0 Then
								For Each row As DataRow In dRows
									If row("Expire") < Now Then
										OnlineUsers.Rows.Remove(row)
									Else
										Exit For
									End If
								Next
								System.Web.HttpContext.Current.Application("OnlineUsers") = OnlineUsers
							End If
						End If
					End If
					OnlineUsers.EndLoadData()
				End If
				System.Web.HttpContext.Current.Application.UnLock()
			End Get
		End Property

		Public Sub OnlineAdd()
			Dim dt As DataTable = OnlineUsers
			Dim URL As New Fog.URL(HttpContext.Current.Request.Url)
			System.Web.HttpContext.Current.Application.Lock()
			Dim dRows() As DataRow = dt.Select("UserID=" & ID)
			Dim Expire As Date = Now.AddSeconds(60 * 5)			  '-- Online po dobu X vteřin
			If dRows.Length = 0 Then
				Dim row As DataRow = dt.NewRow
				row("UserID") = ID
				row("UserNick") = Nick
				row("Expire") = Expire
				row("Autor") = Autor
				row("Domain") = URL.Domain2
				dt.Rows.Add(row)
			Else
				dRows(0)("Expire") = Expire
				dRows(0)("Domain") = URL.Domain2
			End If
			System.Web.HttpContext.Current.Application("OnlineUsers") = dt
			System.Web.HttpContext.Current.Application.UnLock()
		End Sub

		Public Function OnlineAutoriHtmlList() As String
			Const LimitOutput As Short = 20
			Dim s As String
			Dim dRows() As DataRow = OnlineUsers.Select("Autor=True AND (Domain='basne.cz' OR Domain='basnicky.cz')", "UserNick")
			If dRows.Length > 0 Then
				Dim dtChat As DataTable = FN.Cache.dtChatUsersOnline
				Dim f As Int16 = Math.Min(dRows.Length - 1, LimitOutput - 1)
				For Each Row As DataRow In dRows
					s &= "<a href=""/Autori/" & Row("UserID") & "-info.aspx"">" & System.Web.HttpContext.Current.Server.HtmlEncode(Row("UserNick")) & "</a>"
					If dtChat.Select("UserID=" & Row("UserID") & " AND Expirace>" & Val.DateForDatatableSelect(Now)).Length = 1 Then s &= "<img src='gfx/chatuje.gif' title='chatuje' alt='chatuje' />"
					s &= ", "
					If f = 0 Then Exit For
				Next
				If s.EndsWith(", ") Then s = s.Remove(s.Length - 2, 2)
			Else
				s = "-"
			End If
			Return s
		End Function
	End Class

	Public Class URL
		Public Uri As Uri
		Public Sub New()
		End Sub
		Public Sub New(ByVal URL As String)
			Init(URL)
		End Sub
		Public Sub New(ByVal Uri As Uri)
			Me.Uri = Uri
		End Sub
		Private Sub Init(ByVal URL As String)
			Me.Uri = New Uri(URL)
		End Sub
		Public Property URL() As String
			Get
				Return Uri.ToString
			End Get
			Set(ByVal value As String)
				Init(value)
			End Set
		End Property
		Public ReadOnly Property Domain() As String
			Get
				Return Uri.Host.Replace("proxy.", "")
			End Get
		End Property
		Public ReadOnly Property DomainLevel1() As String
			Get
				Dim arr As String() = Uri.Host.Split(".")
				If arr.Length >= 1 Then Return arr(arr.Length - 1)
			End Get
		End Property
		Public ReadOnly Property Domain2() As String
			Get
				Dim arr As String() = Uri.Host.Split(".")
				If arr.Length >= 2 Then Return arr(arr.Length - 2) & "." & arr(arr.Length - 1)
			End Get
		End Property
		Public Function isLocal() As Boolean
			Return Uri.Host.StartsWith("proxy.")
		End Function
	End Class

End Namespace