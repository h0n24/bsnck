Class AscxAnketa
	Inherits System.Web.UI.UserControl
	Public Anketa As Anketa_cls
	Public MyIni As New Fog.Ini.Init
	Dim IDx As Int16
	Dim blnClosed As Boolean
	Public Property AnketaID() As Int16
		Get
			Return IDx
		End Get
		Set(ByVal Value As Int16)
			IDx = Value
		End Set
	End Property
	Public Property Closed() As Boolean
		Get
			Return blnClosed
		End Get
		Set(ByVal Value As Boolean)
			blnClosed = Value
		End Set
	End Property

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Anketa = Cache.Get("Anketa")
		If Not Anketa Is Nothing Then
			If Anketa.IDx <> IDx Then Anketa = Nothing
		End If
		If Anketa Is Nothing Then
			Anketa = New Anketa_cls
			Dim SQL As String = "SELECT * FROM Ankety WHERE ID=" & IDx
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read Then
				Anketa.IDx = DR("ID")
				Anketa.Dotaz = DR("Dotaz")
				For f As Int16 = 1 To 7
					If IsDBNull(DR("Text" & f)) Then Exit For
					Anketa.Texty.Add(DR("Text" & f))
					Anketa.Hlasy.Add(DR("Hlasy" & f))
					Anketa.HlasySuma += DR("Hlasy" & f)
					If Anketa.HlasyMax < DR("Hlasy" & f) Then Anketa.HlasyMax = DR("Hlasy" & f)
				Next
				Anketa.HlasySuma = IIf(Anketa.HlasySuma = 0, 1, Anketa.HlasySuma)
				Anketa.HlasyMax = IIf(Anketa.HlasyMax = 0, 1, Anketa.HlasyMax)
				Cache.Insert("Anketa", Anketa)
			End If
			DR.Close()
			CMD.Connection.Close()
		End If
		If Closed Then
			Me.divHlasuj.Visible = False
			Me.divZprava.InnerText = "Hlasování ukonèeno."
		ElseIf FN.Cookies.Read("Ankety", AnketaID) = Boolean.TrueString Then
			Me.divZprava.InnerText = "Již jsi hlasoval."
			Me.divHlasuj.Visible = False
		Else
			Me.divZprava.Visible = False
		End If
	End Sub

	Public Class Anketa_cls
		Public IDx As Int16
		Public Dotaz As String
		Public Texty As New ArrayList
		Public Hlasy As New ArrayList
		Public HlasyMax As Int16
		Public HlasySuma As Int32
	End Class

End Class
