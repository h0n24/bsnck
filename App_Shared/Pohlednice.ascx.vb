Class _Pohlednice_Ascx
	Inherits System.Web.UI.UserControl

	Public sSoubor, sFormat, sRozmery, sSirka, sVyska As String
	Public Datum As Date

	Public Property Soubor() As String
		Get
			Return sSoubor
		End Get
		Set(ByVal Value As String)
			sSoubor = Value
		End Set
	End Property
	Public Property Format() As String
		Get
			Return sFormat
		End Get
		Set(ByVal Value As String)
			sFormat = Value
		End Set
	End Property
	Public Property Rozmery() As String
		Get
			Return sRozmery
		End Get
		Set(ByVal Value As String)
			sRozmery = Value
		End Set
	End Property

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If sFormat <> "" And sSoubor <> "" And sRozmery <> "" Then
			sSirka = sRozmery.Substring(0, sRozmery.IndexOf("x"))
			sVyska = sRozmery.Substring(sSirka.Length + 1, sRozmery.Length - sSirka.Length - 1)
			Select Case sFormat
				Case "gif", "jpg"
					Me.pnlPohledniceImg.Visible = True
					Me.pnlPohledniceImg.DataBind()
				Case "swf5", "swf6"
					Me.pnlPohledniceFlash.Visible = True
					Me.pnlPohledniceFlash.DataBind()
				Case Else
					Me.pnlPohledniceErr.Visible = True
			End Select
		End If
	End Sub

End Class