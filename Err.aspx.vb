Public Class _Err
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim Akce As String = Request.QueryString("akce")
		If Akce = "generuj" Then GenerateError()

		Dim ErrorCode As Integer = CInt("0" & Request.QueryString("code"))
		Dim ErrorMsg, ErrorDetails As String
		Select Case ErrorCode
			Case 404 : ErrorMsg = "ERROR !!<br/><br/>Page doesn't exist !!<br/>Stránka neexistuje !!"
			Case 500 : ErrorMsg = "ERROR !!<br/><br/>Internal server error !!<br/>Interní chyba serveru !!"
			Case Else : ErrorMsg = "ERROR !!<br/><br/>Error code:" & ErrorCode
		End Select
		Me.pErrMsg.InnerHtml = ErrorMsg
		If Not (Application("ErrorLastData") Is Nothing) Then
			Dim Chyba As SharedFunctions.PageError = Application("ErrorLastData")
			ErrorDetails &= "<b>Code: </b> " & ErrorCode & "<br/>"
			ErrorDetails &= "<b>Url: </b>" & Chyba.Url & "<br/>"
			ErrorDetails &= "<b>Referer: </b>" & Chyba.Referer & "<br/>"
			If Not (Chyba.Exception Is Nothing) Then
				If ErrorCode <> 404 Then
					If FN.Cookies.Read("Web", "ErrorDetails") = Boolean.TrueString Then
						ErrorDetails &= "<b>Exception: </b>" & Chyba.Exception.ToString & "<br/>"
						'ErrorDetails &= "<b>Message: </b>" & Chyba.Exception.Message & Chyba.Exception.Source & "<br/>"
					End If
					Dim FormItems As String
					For Each Key As String In Chyba.Form					  'Výpis formuláøe
						FormItems &= " ;" & Key & "::" & Chyba.Form(Key)
					Next
					ErrorDetails &= FormItems
					Dim LogTime As Date = Application("ErrorLogTime")
					If LogTime.AddSeconds(20) < Now Then					'Zapsat pouze 1 Log za XXX sekund
						Try
							Dim SQL As String = "INSERT INTO ErrorLog (ErrCode,ErrPage,ErrReferer,ErrIP,ErrTxt) Values (" & ErrorCode & ", " & FN.DB.GetText(Chyba.Url, 200) & ", " & FN.DB.GetText(Chyba.Referer, 200) & ", " & FN.DB.GetText(Request.UserHostAddress) & ", " & FN.DB.GetText(Chyba.Exception.ToString & FormItems, 7000) & ")"
							Dim DBConn As System.Data.SqlClient.SqlConnection = FN.DB.Open()
							Dim DBCmd As New System.Data.SqlClient.SqlCommand(SQL, DBConn)
							DBCmd.ExecuteNonQuery()
							DBConn.Close()
							Application("ErrorLogTime") = Now
							ErrorDetails = "<b><i>Chyba byla zapsána do databáze:</i></b><br/><br/>" & ErrorDetails
						Catch ex As Exception
							ErrorDetails = "<b><i>Chybu nelze zapsat do databáze !!!</i></b><br/><br/>" & ErrorDetails
						End Try
					End If
				End If
				Application("ErrorsCount") += 1
			End If
			Me.pErrDetails.InnerHtml = ErrorDetails
			Application("ErrorLastData") = Nothing
		Else
			Me.pErrDetails.Visible = False
		End If
	End Sub

	Sub GenerateError()
		Dim X As Object = "XXXXXXXXX"
		X = CInt(X)
		Response.Write("Chyba byla generována.")
		Response.End()
	End Sub

End Class