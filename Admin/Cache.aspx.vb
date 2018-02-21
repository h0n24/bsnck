Class Admin_Cache
	Inherits System.Web.UI.Page
	Public CacheValue As String

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Akce As String = Request.QueryString("akce")
		Select Case Akce
			Case ""
				ShowList()
			Case "view"
				AkceView()
			Case "delete"
				Select Case Request.QueryString("type")
					Case "cache"
						Cache.Remove(Request.QueryString("name"))
					Case "application"
						Application.Remove(Request.QueryString("name"))
				End Select
				ShowList()
		End Select
	End Sub

	Public Function GetApplicationType(ByVal Name As String)
		'-- Funkce kvůli hodnotě ErrorLastData, které neobsahuje standardní data a házelo to chybu při GetType
		Dim S As String
		Try
			S = Application(Name).GetType.ToString()
		Catch ex As Exception
			S = "***"
		End Try
		Return S
	End Function

	Sub ShowList()
		Me.pnlAdminCacheList.Visible = True
		Dim CacheEnum As IDictionaryEnumerator = Cache.GetEnumerator()
		Dim arr As New ArrayList
		While CacheEnum.MoveNext()
			arr.Add(CacheEnum.Key)
		End While
		Me.rptAdminCacheList.DataSource = arr
		Me.rptAdminCacheList.DataBind()
		Me.rptAdminCacheListApp.DataSource = Application.AllKeys
		Me.rptAdminCacheListApp.DataBind()
	End Sub

	Sub AkceView()
		Me.pnlAdminCacheView.Visible = True
		Dim sName As String = Request.QueryString("name")
		Dim sType As String = Request.QueryString("type")
		Dim obj As Object
		Select Case sType
			Case "cache" : obj = Cache.Get(sName)
			Case "application" : obj = Application(sName)
		End Select
		If Not Request.QueryString("dt") Is Nothing Then
			Dim dt As DataSet = obj
			obj = dt.Tables(Request.QueryString("dt"))
		End If
		Select Case obj.GetType.ToString
			Case "System.String", "System.DateTime", "System.Int64", "System.Int32", "System.Int16"
				Me.litAdminCacheView.Visible = True
				Me.litAdminCacheView.Text = obj
				Me.pnlAdminCacheView.DataBind()
			Case "System.Data.DataTable"
				Me.dgAdminCacheView.Visible = True
				Me.dgAdminCacheView.DataSource = obj
				Me.dgAdminCacheView.DataBind()
			Case "System.Data.DataSet"
				Me.litAdminCacheView.Visible = True
				Me.litAdminCacheView.Text = "<b>DataSet " & sName & "</b>:<br/>"
				Dim ds As DataSet = obj
				For f As Integer = 0 To ds.Tables.Count - 1
					Me.litAdminCacheView.Text &= "» <a href=""Cache.aspx?akce=view&type=" & sType & "&name=" & sName & "&dt=" & ds.Tables(f).TableName & """>" & ds.Tables(f).TableName & "</a><br/>"
				Next
				Me.pnlAdminCacheView.DataBind()
		End Select
	End Sub

End Class