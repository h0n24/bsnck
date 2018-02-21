Public Class Renderer

	Public Class FormErrors
		Public Title As String = "CHYBA !!"
		Public Errors As New ArrayList
		Public ReadOnly Property Count() As Integer
			Get
				Return Errors.Count
			End Get
		End Property
		Public Sub Add(ByVal Text As String)
			Errors.Add(Text)
		End Sub
		Public Function Render() As HtmlGenericControl
			Dim C As New HtmlGenericControl
			C.TagName = "div"
			'C.ID = "pnlFormErrors"
			C.Attributes.Add("class", "FormErrors")
			C.InnerHtml = "<h5 style=""margin-bottom:2px;"">" & Title & "</h5>"
			C.InnerHtml &= "<ul>"
			For Each S As String In Errors
				C.InnerHtml &= "<li>" & S & "</li>"
			Next
			C.InnerHtml &= "</ul>"
			Return C
		End Function
	End Class

	Public Class Report
		Enum Statusy As Byte
			OK = 0
			Err = 1
		End Enum
		Dim _status As Statusy
		Dim _title As String
		Dim _text As String
		Public Sub New()
		End Sub
		Public Sub New(ByVal Status As Statusy, ByVal Title As String)
			_status = Status
			_title = Title
		End Sub
		Public Sub New(ByVal Status As Statusy, ByVal Title As String, ByVal Text As String)
			_status = Status
			_title = Title
			_text = Text
		End Sub
		Public Property Status() As Statusy
			Get
				Return _status
			End Get
			Set(ByVal value As Statusy)
				_status = value
			End Set
		End Property
		Public Property Title() As String
			Get
				Return _title
			End Get
			Set(ByVal value As String)
				_title = value
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
		Public Function Render() As LiteralControl
			Dim C As New LiteralControl
			If Title <> "" Then
				Dim sStyle As String
				If Status = Statusy.Err Then sStyle = "color:Red; "
				C.Text = "<h5 style='" & sStyle & "margin-bottom:4px;'>" & Title & "</h5>"
			End If
			C.Text &= "<p style='margin-bottom:4px;'>" & Text & "</p>"
			Return C
		End Function
	End Class

	Public Class TxtSortBy
		Dim _table As String

		Sub New(ByVal Table As String)
			_table = Table
		End Sub

		Public Function Render() As HtmlGenericControl
			Dim SortBy As String = System.Web.HttpContext.Current.Request.QueryString("sort")
			Dim URL = FN.URL.RemoveQuery(System.Web.HttpContext.Current.Request.RawUrl, "page")
			Dim S As String
			Dim aNew, aSent, aHodn, aTipy As String
			If SortBy = "" Then
				aNew = "<a class='selected'>Nejnovìjších</a>"
			Else
				aNew = "<a href='" & FN.URL.RemoveQuery(URL, "sort") & "'>Nejnovìjších</a>"
			End If
			If SortBy = "sent" Then
				aSent = "<a class='selected'>Poètu poslání</a>"
			Else
				aSent = "<a href='" & FN.URL.SetQuery(URL, "sort", "sent") & "'>Poètu poslání</a>"
			End If
			If SortBy = "hodn" Then
				aHodn = "<a class='selected'>Hodnocení</a>"
			Else
				aHodn = "<a href='" & FN.URL.SetQuery(URL, "sort", "hodn") & "'>Hodnocení</a>"
			End If
			If SortBy = "tipy" Then
				aTipy = "<a class='selected'>Tipù</a>"
			Else
				aTipy = "<a href='" & FN.URL.SetQuery(URL, "sort", "tipy") & "'>Tipù</a>"
			End If
			Select Case _table
				Case "TxtDila"
					S = "Øadit podle: " & aNew & " | " & aSent & " | " & aTipy
				Case Else
					S = "Øadit podle: " & aNew & " | " & aSent & " | " & aHodn
			End Select
			Dim C As New HtmlGenericControl
			C.TagName = "div"
			C.Attributes.Add("class", "BoxSortBy")
			C.Attributes.Add("style", "margin-bottom:2px;")
			C.InnerHtml = S
			Return C
		End Function

	End Class

	Public Shared Function PagesBox(ByVal Stranka As Integer, ByVal PocetStranek As Integer, Optional ByVal AsFolder As Boolean = False) As String
		Dim sPages As String = "<b>Stránky</b>: "
		Dim f As Integer
		Dim LimitStranek As Integer = 10
		Dim SkipPage As Boolean
		Dim sURL As String

		For f = 1 To PocetStranek
			If (f < Stranka - Int((LimitStranek - 3) / 2) And f <= PocetStranek - LimitStranek + 1 And f <> 1) Or (f > Stranka + Int((LimitStranek - 3) / 2) And f > LimitStranek - 1 And f <> PocetStranek) Then
				If Not SkipPage Then sPages &= ("... ")
				SkipPage = True
			Else
				SkipPage = False
			End If
			If Not SkipPage Then
				If AsFolder Then
					sURL = FN.URL.SetQueryFolder(System.Web.HttpContext.Current.Request.RawUrl, "pg", f)
				Else
					sURL = FN.URL.SetQuery(System.Web.HttpContext.Current.Request.RawUrl, "pg", f)
				End If
				If Stranka = f Then
					sPages &= "<a href='" & sURL & "'><b>[" & f & "]</b></a> "
				Else
					sPages &= "<a href='" & sURL & "'>" & f & "</a> "
				End If
			End If
		Next
		Return sPages
	End Function

End Class
