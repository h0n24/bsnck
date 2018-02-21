Class _Komentare_Add
	Inherits System.Web.UI.Page

	Dim MyIni As New Fog.Ini.Init
	Dim MyUser As New Fog.User


	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.IsPostBack Then
			FormSubmit()
		Else
			FormInit()
		End If
	End Sub

	Sub FormInit()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		If Not MyUser.isLogged Then
			ShowError("Komentovat mohou pouze registrovaní a pøihlášení uživatelé.")
		ElseIf Not Sekce.Tabulka.hasKomentare Then
			ShowError("Sekce nemá komentáøe.")
		Else
			Me.inpReferer.Value = FN.URL.Referer
		End If
	End Sub

	Sub ShowError(ByVal Text As String)
		Me.Form1.Visible = False
		Dim Report As New Renderer.Report
		Report.Status = Renderer.Report.Statusy.Err
		Report.Title = "CHYBA !!"
		Report.Text = Text
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Sub FormSubmit()
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim DBID As Integer = Val.ToInt(Request.QueryString("dbid"))
		Dim Txt As String = Me.tbText.Text.Trim
		Dim err As New Renderer.FormErrors
		If Txt = "" Then err.Add("Zadejte text komentáøe")
		If Txt.Length > 3000 Then err.Add("Délka komentáøe je omezena na 3000 znakù")
		If FN.Text.Test.VelkaPismena(Txt) > 30 Then err.Add("Velká písmena pište pouze na zaèátku vìty nebo jména")
		If err.Count = 0 Then
			Txt = FN.Text.ShortTwoSpaces(Txt)
			Txt = FN.Text.RemoveSpaceBeforeCRLF(Txt)
			Txt = FN.Text.ShortThreeCRLF(Txt)
			Txt = FN.Text.ShortFourEndings(Txt)
			Dim SQL As String = "INSERT INTO " & Sekce.Tabulka.Nazev & "Komentare (KomentDBID,KomentUser,KomentTxt) Values (" & DBID & "," & MyUser.ID & "," & FN.DB.GetText(Txt) & ")"
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			CMD.ExecuteNonQuery()
			'If CMD.ExecuteNonQuery() > 0 Then
			'CMD.CommandText = "UPDATE " & Sekce.Tabulka.Nazev & " SET Koment=Koment+1 WHERE ID=" & DBID
			'CMD.ExecuteNonQuery()
			'End If
			CMD.Connection.Close()
			Dim Referer As String = Me.inpReferer.Value
			If Referer.IndexOf("Komentare_Add.aspx") <> -1 Then Referer = ""
			Dim RedirElse As String = "/" & Sekce.Alias & "/" & DBID & "-koment.aspx"
			FN.Redir(Referer, RedirElse)
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

End Class