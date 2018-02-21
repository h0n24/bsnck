Partial Class Final_New
	Inherits System.Web.UI.Page

	Dim U As New Geo.User
	Dim CacheID As Integer

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		U.FillFromCookies()
		If Not U.Exists Then
			Me.Form1.Visible = False
			Dim Report As New Renderer.Report
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "ERROR !!"
			Report.Text = "Only for registered users. Please log in."
			Me.phReport.Controls.Add(Report.Render)
			Exit Sub
		End If
		If Page.IsPostBack Then

			If U.Email.EndsWith("spambog.com") Then	'Spoiler protection
				Response.Write("ERROR!<br/>Database is full.")
				Response.End()
			End If


			FormSubmit()
		Else
			FormInit()
		End If
	End Sub

	Sub FormInit()
		lbCacheType.DataSource = FN.Cache.dtGeocacheTypes
		lbCacheType.DataValueField = "TypeID"
		lbCacheType.DataTextField = "TypeName"
		lbCacheType.DataBind()
		lbCacheType.Items.Insert(0, New ListItem("--- Choose Geocache Type ---", 0))
	End Sub

	Sub FormSubmit()
		Dim err As New Renderer.FormErrors
		Dim CacheCode As String = tbCacheCode.Text.ToUpper.Trim
		Dim CacheName As String = tbCacheName.Text.Trim
		Dim FinalCoord As New Geo.Coordinates()
		Dim CacheCoord As New Geo.Coordinates()
		If CacheCode = "" Then
			err.Add("Geocache Code is missing")
		ElseIf CacheCode.Length < 4 Or CacheCode.Length > 8 Then
			err.Add("Geocache Code is not valid")
		ElseIf Not CacheCode.StartsWith("GC") Then
			err.Add("Geocache Code is not valid")
		End If
		If CacheName = "" Then err.Add("Geocache Name is missing")
		If Val.ToInt16(lbCacheType.SelectedValue) = 0 Then err.Add("Select Type of Geocache")
		If tbCacheCoord.Text.Trim = "" Then
			err.Add("Geocache Coordinates are missing")
		ElseIf Not CacheCoord.Parse(tbCacheCoord.Text.Trim) Then
			err.Add("Geocache Coordinates have wrong format")
		End If
		If tbFinalCoord.Text.Trim = "" Then
			err.Add("Final Coordinates are missing")
		ElseIf Not FinalCoord.Parse(tbFinalCoord.Text.Trim) Then
			err.Add("Final Coordinates have wrong format")
		End If

		If err.Count = 0 Then
			Dim blnInsertFI, blnInsertCredits As Boolean
			Dim FinalID As Integer
			Dim SQL As String = "SELECT CacheID,CacheType FROM Geocaches WHERE CacheCode=" & FN.DB.GetText(CacheCode)
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader()
			If DR.Read Then
				Dim CacheType As New Geo.GeocacheType(DR("CacheType"))
				CacheID = DR("CacheID")
				DR.Close()
				CMD.CommandText = "SELECT FinalID FROM FinalCoords WHERE FinalCache=" & CacheID & " AND FinalUser=" & U.ID
				If IsNothing(CMD.ExecuteScalar) Then	'=User doesn't have FI for this Cache
					CMD.CommandText = "SELECT IPUser FROM UserIPs WHERE IPUser=" & U.ID & " AND IPIP IN (SELECT IPIP FROM UserIPs WHERE IPUser IN (SELECT FinalUser FROM FinalCoords WHERE FinalCache=" & CacheID & ") )"
					If IsNothing(CMD.ExecuteScalar) Then	'=User IP is unique within the first Publisher and all other validators
						blnInsertFI = True
						CMD.CommandText = "SELECT FinalID FROM FinalCoords WHERE FinalCache=" & CacheID & " AND FinalLat=" & FinalCoord.LatToSQL & " AND FinalLon=" & FinalCoord.LonToSQL
						If Not IsNothing(CMD.ExecuteScalar) Then	'=FI exists - validation
							blnInsertCredits = CacheType.GainCredits
						End If
					Else
						err.Add("Your IP address and identity must be unique within all cache publishers and validators")
					End If
				Else		'=My FI already exists
					err.Add("You have already published coordinates for this Geocache")
				End If
			Else
				DR.Close()
				CMD.CommandText = "INSERT INTO Geocaches (CacheCode,CacheName,CacheType,CacheUser,CacheLat,CacheLon) Values (" & FN.DB.GetText(CacheCode) & ", " & FN.DB.GetText(CacheName) & ", " & Val.ToInt16(lbCacheType.SelectedValue) & ", " & U.ID & ", " & CacheCoord.LatToSQL & ", " & CacheCoord.LonToSQL & ")"
				CMD.CommandText &= "; SELECT @@IDENTITY AS Identita"
				CacheID = CMD.ExecuteScalar
				blnInsertFI = True
				blnInsertCredits = False
			End If
			If blnInsertFI Then
				CMD.CommandText = "INSERT INTO FinalCoords (FinalCache,FinalUser,FinalLat,FinalLon) Values (" & CacheID & ", " & U.ID & ", " & FinalCoord.LatToSQL & ", " & FinalCoord.LonToSQL & ")"
				CMD.CommandText &= "; SELECT @@IDENTITY AS Identita"
				FinalID = CMD.ExecuteScalar
				If blnInsertCredits Then
					CMD.CommandText = "INSERT INTO Credits (CreditUser,CreditAmount,CreditSource,CreditParent) Values (" & U.ID & ", " & Geo.CreditPrices.ValidateCoordinates & ", " & Geo.CreditSources.ValidateCoordinates & ", " & FinalID & ")"
					CMD.ExecuteNonQuery()
					FN.Cookies.GeoCredits.Delete()
				End If
			End If
			CMD.Connection.Close()
		End If

		If err.Count <> 0 Then
			phErrors.Controls.Add(err.Render)
		Else
			Response.Redirect("/Geocache.aspx?id=" & CacheID)
		End If
	End Sub

End Class