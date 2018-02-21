Public Class SharedFunctions

	Public Structure PageError
		'-- Používá se pøi výskytu chyby 
		Dim Code As Integer
		Dim Referer As String
		Dim Url As String
		Dim Exception As Exception
		Dim Form As System.Collections.Specialized.NameValueCollection
	End Structure

	Public Shared Sub VzkazyDeleteCookies()
		'-- Vymaže informaci o poslední kontrole nových vzkazù
		FN.Cookies.Delete("Session", "VzkazyChecked")
		FN.Cookies.Delete("Session", "VzkazyNew")
	End Sub

	Class TextFromFile
		Public Shared Function Read(ByRef Txt As String, ByVal SekceAlias As String) As String
			'-- Pøeète text ze souboru (ne z databáze), pokud text odkazuje na soubor
			Dim Sekce As New Fog.Sekce(SekceAlias)
			Dim LeadingText As String = "##UsingFile##="
			If Txt.StartsWith(LeadingText) Then
				Dim FileName As String = Txt.Substring(LeadingText.Length, Txt.Length - LeadingText.Length)
				Dim File As New IO.FileInfo(Fog.Ini.PhysicalPaths.Data & "\" & Sekce.Tabulka.Nazev & "/" & FileName & ".txt")
				If File.Exists Then
					Dim sr As New IO.StreamReader(File.FullName)
					Txt = sr.ReadToEnd
					sr.Close()
				Else
					Txt = "CHYBA !!! Text byl pøesunut do archivu, kde nebyl nalezen."
				End If
			End If
			Return Txt
		End Function
		Public Shared Function WriteXXXXXXXXXXXXXX(ByRef Txt As String, ByVal Sekce As String) As String
		End Function
		Public Shared Function DeleteFileXXXXXXXXXXXXXX(ByRef Txt As String, ByVal Sekce As String) As String
		End Function
	End Class

End Class