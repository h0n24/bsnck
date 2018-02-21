Class Admin_DB_Csv
	Inherits System.Web.UI.Page
	Dim TimeOut As Int16 = System.Configuration.ConfigurationManager.AppSettings("Page.TimeOut") - 10
	Dim Report As New Renderer.Report
	Dim MyIni As New Fog.Ini.Init
	Public SqlInserts As String

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case "export" : ExportCsv()
			Case "import" : ImportCsv()
			Case "" : ShowMenu()
		End Select
	End Sub

	Sub ShowMenu()
		Me.pnlMenu.Visible = True
		If IsPostBack Then
			FN.Cookies.WriteKey("Import", "FirstLineColumnsNames", Me.cbFirstLineColumnsNames.Checked.ToString)
			FN.Cookies.WriteKey("Import", "SkipIdentityColumns", Me.cbSkipIdentityColumns.Checked.ToString)
			FN.Cookies.WriteKey("Import", "TruncteTable", Me.cbTruncateTable.Checked.ToString)
			FN.Cookies.WriteKey("Import", "ShowSQL", Me.cbShowSQL.Checked.ToString)
			Me.lblSettingsReport.Text = "Uloženo do cookies !!"
		Else
			Me.cbFirstLineColumnsNames.Checked = IsChecked_FirstLineColumnsNames()
			Me.cbSkipIdentityColumns.Checked = IsChecked_SkipIdentityColumns()
			Me.cbTruncateTable.Checked = IsChecked_TruncateTable()
			Me.cbShowSQL.Checked = IsChecked_ShowSQL()
		End If
		Me.rptMenu.DataSource = FN.DB.DatabaseTables
		Me.rptMenu.DataBind()
	End Sub

	Public Function FileChangeDate(ByVal Table As String) As String
		Dim FileName As String = Fog.Ini.PhysicalPaths.Data & "\Export\" & Table & ".csv"
		Dim fi As New IO.FileInfo(FileName)
		Return fi.LastWriteTime.ToString("yyyy-MMdd HH:mm")
	End Function

	Sub ExportCsv()
		Dim StartTime As DateTime = Now
		Dim ColumnsTypes As String
		Dim Table As String = Request.QueryString("table")
		Dim FileName As String = Fog.Ini.PhysicalPaths.Data & "\Export\" & Table & ".csv"
		Dim S As String
		Dim f As Integer
		Dim sWhere As String
		If Not Request.QueryString("where") Is Nothing Then
			sWhere = " WHERE " & Request.QueryString("where")
		End If
		Dim SQL As String = "SELECT * FROM " & Table & sWhere
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader(CommandBehavior.SchemaOnly)
		For f = 0 To DR.FieldCount - 1
			S &= """" & DR.GetName(f) & """"
			ColumnsTypes &= DR.GetName(f) & " ... " & DR.GetFieldType(f).ToString & "<br/>"
			If f <> DR.FieldCount - 1 Then S &= ";"
		Next
		DR.Close()
		Dim SW As System.IO.StreamWriter
		If Request.QueryString("where") Is Nothing Then
			SW = System.IO.File.CreateText(FileName)
			If IsChecked_FirstLineColumnsNames() Then
				SW.WriteLine(S)
				SW.Flush()
			End If
		Else
			SW = My.Computer.FileSystem.OpenTextFileWriter(FileName, True)
		End If
		DR = CMD.ExecuteReader
		Dim Pocet As Int32
		While DR.Read()
			Pocet += 1
			S = ""
			For f = 0 To DR.FieldCount - 1
				If DR(f).GetType Is GetType(System.DBNull) Then
				ElseIf DR.GetFieldType(f) Is GetType(System.String) Then
					S &= """" & Replace(DR(f), """", """""") & """"
				Else
					S &= DR(f)
				End If
				If f <> DR.FieldCount - 1 Then S &= ";"
			Next
			SW.WriteLine(S)
			SW.Flush()
			If Now.CompareTo(StartTime.AddSeconds(TimeOut)) > 0 Then
				sWhere = DR.GetName(0) & ">" & DR(0)
				Report.Status = Renderer.Report.Statusy.Err
				Report.Title = "Timeout " & TimeOut & " sec !!"
				Report.Text &= "<a href='" & FN.URL.SetQuery(Request.Url.ToString, "where", sWhere) & "' style='font-weight:bold; padding:2px; background-color:#EEEEEE; border: #888888 1px solid;'>»»» POKRAÈOVAT »»»</a><br/><br/>"
				Exit While
			End If
		End While
		SW.Close()
		DR.Close()
		CMD.Connection.Close()
		Report.Text &= "Tabulka: " & Table & "<br/>"
		Report.Text &= "Záznamù: " & Pocet & "<br/>"
		Report.Text &= "Èas: " & Int(Now.Subtract(StartTime).TotalMilliseconds / 100) / 10 & " s<br/>"
		Report.Text &= "Konec: " & Now.ToString("HH:mm:ss") & "<br/>"
		Dim FI As New System.IO.FileInfo(FileName)
		Report.Text &= "Velikost souboru: " & Int(FI.Length / 100) / 10 & " kB<br/>"
		Report.Text &= "<br/>"
		Report.Text &= "Sloupce:<br/>"
		Report.Text &= ColumnsTypes
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Sub ImportCsv()
		Me.pnlImport.Visible = True
		Dim StartTime As Date = Now
		Dim Table As String = Request.QueryString("table")
		Dim FileName As String = Fog.Ini.PhysicalPaths.Data & "\Export\" & Table & ".csv"
		Dim SR As System.IO.StreamReader
		Dim IdentityInsertExecuted As Boolean
		Try
			SR = New System.IO.StreamReader(FileName)
		Catch ex As Exception
			Report.Title = "Soubor nenalezen !!"
			Report.Status = Renderer.Report.Statusy.Err
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End Try
		Dim S As String
		Dim f As Integer
		Dim ds As New DataSet("DDDD")
		Dim SQL As String = "SELECT TOP 1 * FROM " & Table
		Dim DB As System.Data.SqlClient.SqlConnection = FN.DB.Open()
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, DB)
		Dim DA As New System.Data.SqlClient.SqlDataAdapter(CMD)
		DA.FillSchema(ds, SchemaType.Source, Table)
		DA.Dispose()
		If IsChecked_TruncateTable() And (Request.QueryString("line") Is Nothing) Then
			CMD.CommandText = "TRUNCATE Table " & Table
			CMD.ExecuteNonQuery()
		End If
		Dim dt As DataTable = ds.Tables(Table)
		Dim Radek As Int32 = 1
		Dim PocetZaznamu As Int32
		Dim Pozice As Int32
		Dim RawValues As New Specialized.StringCollection
		Dim ColumnEnd As Integer
		Dim ColumnValue As String
		If IsChecked_FirstLineColumnsNames() Then
			Radek += 1
			SR.ReadLine()
		End If
		While Radek < Val.ToInt(Request.QueryString("line"))
			Radek += 1
			SR.ReadLine()
		End While

		Do While SR.Peek <> -1
			S &= SR.ReadLine
			While Pozice <= S.Length
				ColumnEnd = FindColumnEnd(S, Pozice)
				If ColumnEnd = -1 Then
					Exit While
				Else
					RawValues.Add(S.Substring(Pozice, ColumnEnd - Pozice))
				End If
				Pozice = ColumnEnd + 1
			End While
			If ColumnEnd = -1 Then
				'-- Neukonèený text, pøièti nový øádek
				S &= vbCrLf
				Radek += 1
			Else
				'-- Konec øádku, zapiš hodnoty
				PocetZaznamu += 1
				Dim SqlInsert As New FN.DB.SqlInsert
				For f = 0 To dt.Columns.Count - 1
					Dim c As DataColumn = ds.Tables(Table).Columns(f)
					Select Case Val.Types.GetFromType(c.DataType)
						Case Val.Types.ValueTypes.Number
							If c.AllowDBNull And RawValues(f) = "" Then
								ColumnValue = "NULL"
							Else
								ColumnValue = RawValues(f)
							End If
							If c.ReadOnly And c.AutoIncrement Then
								If Not IsChecked_SkipIdentityColumns() Then
									SqlInsert.IdentityInsert = True
									SqlInsert.Add(c.ColumnName, ColumnValue)
								End If
							ElseIf c.ReadOnly Then							  'Poèítaný sloupec, nedìlat nic
							Else
								SqlInsert.Add(c.ColumnName, ColumnValue)
							End If
						Case Val.Types.ValueTypes.Text
							If c.AllowDBNull And RawValues(f) = "" Then
								SqlInsert.Add(c.ColumnName, "NULL")
							Else
								SqlInsert.Add(c.ColumnName, "'" & RawValues(f).Substring(1, RawValues(f).Length - 2).Replace("'", "''").Replace("""""", """") & "'")
							End If
						Case Val.Types.ValueTypes.Date
							If c.AllowDBNull And RawValues(f) = "" Then
								SqlInsert.Add(c.ColumnName, "NULL")
							Else
								SqlInsert.Add(c.ColumnName, FN.DB.GetDateTime(DateTime.Parse(RawValues(f))))
							End If
						Case Val.Types.ValueTypes.Boolean
							If c.AllowDBNull And RawValues(f) = "" Then
								SqlInsert.Add(c.ColumnName, "NULL")
							Else
								SqlInsert.Add(c.ColumnName, Val.ToInt(RawValues(f)))
							End If
					End Select
				Next
				If SqlInsert.IdentityInsert Then
					If Not IdentityInsertExecuted Then
						IdentityInsertExecuted = True
						CMD.CommandText = "SET IDENTITY_INSERT " & Table & " ON"
						CMD.ExecuteNonQuery()
					End If
				End If
				CMD.CommandText = "INSERT INTO " & Table & SqlInsert.Text
				If IsChecked_ShowSQL() Then
					SqlInserts &= CMD.CommandText & vbCrLf
				End If
				CMD.ExecuteNonQuery()
				S = ""
				Pozice = 0
				RawValues.Clear()
				Radek += 1
				If Now.Subtract(StartTime).TotalSeconds > TimeOut Then
					Report.Status = Renderer.Report.Statusy.Err
					Report.Title = "Timeout " & TimeOut & " sec !!"
					Report.Text &= "<a href='" & FN.URL.SetQuery(Request.Url.ToString, "line", Radek) & "' style='font-weight:bold; padding:2px; background-color:#EEEEEE; border: #888888 1px solid;'>»»» POKRAÈOVAT »»»</a><br/><br/>"
					Exit Do
				End If
			End If
		Loop
		If IdentityInsertExecuted Then
			CMD.CommandText = "SET IDENTITY_INSERT " & Table & " OFF"
			CMD.ExecuteNonQuery()
		End If
		CMD.Connection.Close()
		SR.Close()
		Report.Text &= "Tabulka: " & Table & "<br/>"
		Report.Text &= "Záznamù: " & PocetZaznamu & "<br/>"
		Report.Text &= "Èas: " & Int(Now.Subtract(StartTime).TotalMilliseconds / 100) / 10 & " s<br/>"
		Report.Text &= "Konec: " & Now.ToString("HH:mm:ss") & "<br/>"
		Dim FI As New System.IO.FileInfo(FileName)
		Report.Text &= "Velikost souboru: " & Int(FI.Length / 100) / 10 & " kB<br/>"
		Report.Text &= "Identity Insert: " & IdentityInsertExecuted & "<br/>"
		Me.phReport.Controls.Add(Report.Render)
	End Sub

	Function XXXFindColumnEnd(ByRef S As String, ByVal Pozice As Int32) As Int32
		Dim OpenQuotation As Boolean
		Dim Znak, ZnakNext As String
		Dim Delka As Int32 = S.Length
		While Pozice < Delka
			Znak = S.Substring(Pozice, 1)
			If Znak = ";" And OpenQuotation = False Then
				Return Pozice				'Pozice pøed støedníkem
			ElseIf Znak = """" Then
				If OpenQuotation Then
					'ZnakNext = IIf(Pozice + 1 < Delka, S.Substring(Pozice + 1, 1), "")
					ZnakNext = IIf(Pozice + 1 < Delka, S.Substring(Pozice + 1, 1), "")

					'If Pozice + 1 < Delka Then
					'	ZnakNext = S.Substring(Pozice + 1, 1)
					'Else
					'	ZnakNext = ""
					'End If

					If ZnakNext = """" Then
						Pozice += 1
					Else
						Return Pozice + 1						'Uvozovky na konci øádku
					End If
				Else
					OpenQuotation = True
				End If
			End If
			Pozice += 1
		End While
		If OpenQuotation Then
			Return -1			 'Neukonèené uvozovky
		Else
			Return Pozice			 'Konec na posledním sloupci
		End If
	End Function

	Function FindColumnEnd(ByRef S As String, ByVal Pozice As Int32) As Int32
		Dim Znak As String
		Dim PoziceUvozovek As Int32
		Dim Delka As Int32 = S.Length
		While Pozice < Delka
			Znak = S.Substring(Pozice, 1)
			If Znak = ";" Then
				Return Pozice				'Pozice pøed støedníkem
			ElseIf Znak = """" Then
				Pozice += 1
				Do While Pozice < Delka
					PoziceUvozovek = S.IndexOf("""", Pozice)
					If PoziceUvozovek = -1 Then
						Return -1						 'Nejsou konèící uvozovky
					Else
						If PoziceUvozovek = S.IndexOf("""""", PoziceUvozovek) Then
							Pozice = PoziceUvozovek + 2					  'Dvojté uvozovky ""
						Else
							Return PoziceUvozovek + 1							 'Zavírací uvozovky
						End If
					End If
				Loop
				Return -1					'Otevøené " 
			End If
			Pozice += 1
		End While
		Return Pozice		  'Konec na posledním sloupci
	End Function

	Function IsChecked_FirstLineColumnsNames() As Boolean
		Return FN.Cookies.Read("Import", "FirstLineColumnsNames") <> "False"
	End Function
	Function IsChecked_SkipIdentityColumns() As Boolean
		Return FN.Cookies.Read("Import", "SkipIdentityColumns") = "True"
	End Function
	Function IsChecked_TruncateTable() As Boolean
		Return FN.Cookies.Read("Import", "TruncteTable") <> "False"
	End Function
	Function IsChecked_ShowSQL() As Boolean
		Return FN.Cookies.Read("Import", "ShowSQL") = "True"
	End Function

End Class