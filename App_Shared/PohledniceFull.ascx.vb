Class _PohledniceFull_Ascx
	Inherits System.Web.UI.UserControl

	Public iID, iHodnoceni, iOdeslano, iAutorID As Integer
	Public sTitulek, sSoubor, sFormat, sRozmery, sSirka, sVyska, sAutorNick As String
	Public Property Pohlednice() As Integer
		Get
			Return iID
		End Get
		Set(ByVal Value As Integer)
			iID = Value
		End Set
	End Property

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim SQL As String = "SELECT ID,Titulek,Soubor,Format,Rozmery,Odeslano,Hodnoceni,AutorNick,AutorID FROM Pohlednice LEFT JOIN PohledniceAutori ON Autor=AutorID WHERE ID=" & iID
		Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim DBCmd As New System.Data.SqlClient.SqlCommand(SQL, DBConn)
		Dim DBDR As System.Data.SqlClient.SqlDataReader = DBCmd.ExecuteReader()
		If DBDR.HasRows Then
			DBDR.Read()
			iHodnoceni = DBDR("Hodnoceni")
			iOdeslano = DBDR("Odeslano")
			iAutorID = DBDR("AutorID")
			sTitulek = DBDR("Titulek")
			sSoubor = DBDR("Soubor")
			sFormat = DBDR("Format")
			sRozmery = DBDR("Rozmery")
			sSirka = sRozmery.Substring(0, sRozmery.IndexOf("x"))
			sVyska = sRozmery.Substring(sSirka.Length + 1, sRozmery.Length - sSirka.Length - 1)
			sAutorNick = DBDR("AutorNick")
			Me.pnlPohlednice.DataBind()
		End If
	End Sub

End Class