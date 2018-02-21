Public Class Reklama

	Public Class Banner
		Dim _id As Integer
		Dim _client As Integer
		Dim _format As String
		Dim _width As Integer
		Dim _height As Integer
		Dim _url As String
		Dim _file As String
		Dim _text As String
		Public Property ID() As Integer
			Get
				Return _id
			End Get
			Set(ByVal value As Integer)
				_id = value
			End Set
		End Property
		Public Property Client() As Integer
			Get
				Return _client
			End Get
			Set(ByVal value As Integer)
				_client = value
			End Set
		End Property
		Public Property Format() As String
			Get
				Return _format
			End Get
			Set(ByVal value As String)
				_format = value
			End Set
		End Property
		Public Property Width() As Integer
			Get
				Return _width
			End Get
			Set(ByVal value As Integer)
				_width = value
			End Set
		End Property
		Public Property Height() As Integer
			Get
				Return _height
			End Get
			Set(ByVal value As Integer)
				_height = value
			End Set
		End Property
		Public Property URL() As String
			Get
				Return _url
			End Get
			Set(ByVal value As String)
				_url = value
			End Set
		End Property
		Public Property File() As String
			Get
				Return _file
			End Get
			Set(ByVal value As String)
				_file = value
			End Set
		End Property
		Public Property Text() As String
			Get
				Return _text
			End Get
			Set(ByVal value As String)
				_text = value
			End Set
		End Property
		Public ReadOnly Property isLabelIncluded() As Boolean
			Get
				If Client = 9 Then Return True
			End Get
		End Property
		Sub New()
		End Sub
		Sub New(ByVal BannerID As Integer)
			If BannerID = 0 Then Exit Sub
			Dim dt As DataTable = FN.Cache.dtReklamaBannery
			Dim dRows() As DataRow = dt.Select("BannerID=" & BannerID)
			If dRows.Length = 0 Then
				Dim dtBannery As DataTable = New DataTable("ReklamaBannery")
				Dim DBDA As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM ReklamaBannery WHERE BannerID=" & BannerID, FN.DB.GetConnectionString())
				DBDA.Fill(dtBannery)
				DBDA.Dispose()
				If dtBannery.Rows.Count > 0 Then
					Dim dRow As DataRow
					dRow = dt.NewRow
					dRow("BannerID") = BannerID
					dRow("BannerKlient") = dtBannery.Rows(0)("BannerKlient")
					dRow("BannerTyp") = dtBannery.Rows(0)("BannerTyp")
					dRow("BannerRozmery") = dtBannery.Rows(0)("BannerRozmery")
					dRow("BannerURL") = dtBannery.Rows(0)("BannerURL")
					dRow("BannerSoubor") = dtBannery.Rows(0)("BannerSoubor")
					dRow("BannerTxt") = dtBannery.Rows(0)("BannerTxt")
					dt.Rows.Add(dRow)
					dRows = dt.Select("BannerID=" & BannerID)
				End If
			End If
			If dRows.Length > 0 Then
				ID = dRows(0)("BannerID")
				Client = dRows(0)("BannerKlient")
				Format = dRows(0)("BannerTyp")
				URL = dRows(0)("BannerURL")
				File = dRows(0)("BannerSoubor")
				Text = dRows(0)("BannerTxt")
				Dim arr() As String = dRows(0)("BannerRozmery").ToString.Split("x")
				If arr.Length = 2 Then
					Width = Val.ToInt(arr(0))
					Height = Val.ToInt(arr(1))
				End If
			End If
		End Sub

		Public Function Generate() As String
			If Val.ToBoolean(FN.Cookies.Read("User", "Premium")) Then
				Return ""
			End If
			Dim S As String
			Select Case Format
				Case "html" : S = GenerateHtml()
				Case "img" : S = GenerateImg()
				Case "txt" : S = GenerateTxt()
			End Select
			Return S
		End Function
		Function GenerateHtml() As String
			Dim S As String
			If FN.Users.Prava.ReklamaOff Then
				S = "<div style=""width:" & Width & "px; height:" & Height & "px; border: 1px solid #888888;"" title=""" & System.Web.HttpContext.Current.Server.HtmlEncode(Text) & """></div>"
			Else
				S = Text
			End If
			Return S
		End Function
		Function GenerateImg() As String
			Return "<a href='" & FN.URL.GetRootUrl & "/r/b" & ID & ".aspx'><img alt='" & System.Web.HttpContext.Current.Server.HtmlEncode(Text) & "' title='" & System.Web.HttpContext.Current.Server.HtmlEncode(Text) & "' src='/gfx/reklama/" & File & "' /></a>"
		End Function
		Function GenerateTxt() As String
			Dim Poloha As Integer
			Dim arr() As String = Text.Split(vbCrLf)
			If arr.Length > 1 Then
				Poloha = Int(Rnd() * arr.Length)
			End If
			Return "<a href='" & FN.URL.GetRootUrl & "/r/b" & ID & ".aspx'>" & System.Web.HttpContext.Current.Server.HtmlEncode(arr(Poloha).Trim) & "</a>"
		End Function

	End Class

	Public Class Pozice
		Dim _id As Integer
		Dim _banner As Banner

		Public Property ID() As Integer
			Get
				Return _id
			End Get
			Set(ByVal value As Integer)
				_id = value
			End Set
		End Property
		Public Property Banner() As Reklama.Banner
			Get
				If _banner Is Nothing Then
					_banner = New Reklama.Banner	'NULL Ochrana kvùli nedefinované pozici nebo chybìjícím bannerùm na pozici
				End If
				Return _banner
			End Get
			Set(ByVal value As Reklama.Banner)
				_banner = value
			End Set
		End Property

		Sub New()
		End Sub
		Sub New(ByVal PoziceID As Integer)
			ID = PoziceID
			Dim ds As DataSet = FN.Cache.dsReklamaXml
			Dim dtPozice As DataTable = ds.Tables("pozice")
			Dim dRows() As DataRow
			dRows = dtPozice.Select("id=" & ID)
			Dim PoziceIndex As Integer
			If dRows.Length <> 0 Then
				PoziceIndex = dRows(0)("pozice_id")
			Else
				Exit Sub
			End If
			Dim f As Integer
			Dim sBannery As String
			Dim blnOK As Boolean
			Dim obj As Object
			Dim MyIni As New Fog.Ini.Init
			Dim dtData As DataTable = ds.Tables("data")
			Dim URL As New Fog.URL(System.Web.HttpContext.Current.Request.Url)
			Dim sWhere As String = "pozice_id=" & PoziceIndex & " AND (domena='" & URL.Domain & "'"
			Dim S As String = URL.Domain
			While S.IndexOf(".") <> -1
				S = S.Substring(S.IndexOf(".") + 1, S.Length - S.IndexOf(".") - 1)
				sWhere &= " OR domena='" & S & "'"
			End While
			sWhere &= " OR domena='*')"
			dRows = dtData.Select(sWhere)
			If dRows.Length <> 0 Then
				For f = dRows.Length - 1 To 0 Step -1
					blnOK = True
					obj = FN.Datum.FormInput2Time(dRows(f)("t1"))
					If Not (obj Is Nothing) Then
						If obj >= TimeOfDay Then blnOK = False
					End If
					obj = FN.Datum.FormInput2Time(dRows(f)("t2"))
					If Not (obj Is Nothing) Then
						If obj < TimeOfDay Then blnOK = False
					End If
					obj = FN.Datum.FormInput2Date(dRows(f)("d1"))
					If Not (obj Is Nothing) Then
						If obj > Now Then blnOK = False
					End If
					obj = FN.Datum.FormInput2Date(dRows(f)("d2"))
					If Not (obj Is Nothing) Then
						If obj.AddDays(1) < Now Then blnOK = False
					End If
					If blnOK Then
						sBannery = dRows(f)("bannery")
						Exit For
					End If
				Next
			End If
			If Not (sBannery Is Nothing) Then
				If sBannery.IndexOf(",") = -1 Then
					Banner = New Reklama.Banner(Val.ToInt(sBannery))
				Else
					Dim PoziceRotator As Integer = CInt(System.Web.HttpContext.Current.Application("ReklamaPozice" & ID & "Rotator"))
					Dim arr() As String = sBannery.Split(",")
					Banner = New Reklama.Banner(Val.ToInt(arr(PoziceRotator)))
					PoziceRotator += 1
					If PoziceRotator > arr.Length - 1 Then PoziceRotator = 0
					System.Web.HttpContext.Current.Application("ReklamaPozice" & ID & "Index") = PoziceRotator
				End If
			End If
		End Sub

	End Class

	Public Shared Function Generate(ByVal PoziceID As Integer) As String
		Dim Pozice As New Reklama.Pozice(PoziceID)
		Return Pozice.Banner.Generate
	End Function

	Public Shared Function GenerateFull501() As String
		Dim Pozice As New Reklama.Pozice(501)
		If Pozice.Banner.isLabelIncluded Then
			Return "<div style=""margin-bottom:6px;"">" & Pozice.Banner.Generate & "</div>"
		Else
			Return "<div style=""font-size:80%;""><u>Reklama:</u></div><div style=""margin-bottom:6px;"">" & Pozice.Banner.Generate & "</div>"
		End If
	End Function

End Class