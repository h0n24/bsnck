Class Admin_DBtoFileConversion
	Inherits System.Web.UI.Page
	Dim LeadingText As String = "##UsingFile##="
	Public Const MinimalTextLenght As Int32 = 8000

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Dim Akce As String = Request.QueryString("akce")
		If Akce = "go" Then
			Spustit()
		End If
	End Sub

	Sub Spustit()
		Dim StartTime As Date = Now
		Dim Table As String = Request.QueryString("table")
		Dim FromID As UInt64 = Request.QueryString("FromID")
		Dim Path As String = Fog.Ini.PhysicalPaths.Data & "\" & Table
		Dim TimeOut As Integer = System.Configuration.ConfigurationManager.AppSettings("Page.TimeOut") - 10
		Dim Report As New Renderer.Report
		Dim SQL As String = "SELECT * FROM " & Table & " WHERE ID>" & FromID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
		Dim Pocet, SkipedLength As Int32
		Dim IDs As String = ""
		While DR.Read()
			Dim iID As UInt64 = iID = DR("ID")
			If Len(DR("Txt")) > MinimalTextLenght Then
				Dim SW As System.IO.StreamWriter = System.IO.File.CreateText(Path & "\" & iID & ".txt")
				SW.Write(DR("Txt"))
				SW.Flush()
				SW.Close()
				Pocet += 1
				IDs &= iID & ","
			Else
				SkipedLength += 1
			End If
			If Now.Subtract(StartTime).TotalSeconds > TimeOut Then
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Timeout " & TimeOut & " sec !!"
				Report.Text = "Poslední testovaný záznam mìl ID=" & iID & "<br/>"
				Exit While
			Else
				Report.Title = "Hotovo"
			End If
		End While
		DR.Close()
		If Pocet <> 0 Then
			CMD.CommandText = "UPDATE " & Table & " SET Txt='" & LeadingText & "' + Convert(nvarchar(10), ID)  WHERE ID IN (" & IDs.Substring(0, IDs.Length - 1) & ")"
			CMD.ExecuteNonQuery()
		End If
		CMD.Connection.Close()
		Report.Text &= "Do souborù uloženo " & Pocet & " záznamù.<br/>"
		Report.Text &= "Pøeskoèeno kvùli velikosti textu pod  " & MinimalTextLenght & " bytù: " & SkipedLength & " záznamù."
		Me.phReport.Controls.Add(Report.Render)
	End Sub

End Class