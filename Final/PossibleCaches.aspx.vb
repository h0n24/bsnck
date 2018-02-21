Partial Class Final_PossibleCaches
	Inherits System.Web.UI.Page

	Const Chars As String = "0123456789ABCDEFGHIJKLMNPQRSTUVWXZY"

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim Base As String = Request.QueryString("base")
		Page.Title = Base
		If Base.Length = "GC1234".Length Then
			hlGC.Text = Base
			hlGC.NavigateUrl = "Cache.aspx?cache=" & Base
			phCacheDirectLink.Visible = True
		End If
		Dim sb As New StringBuilder
		For f As Int16 = 0 To Chars.Length - 2
			If Base.Length < "GC1234".Length Then
				sb.Append(String.Format("<a href='?base={0}'>{0}</a> ", Base & Chars.Substring(f, 1)))
			Else
				sb.Append(String.Format("Cache <a href='Cache.aspx?cache={0}'>{0}</a><br/>", Base & Chars.Substring(f, 1)))
			End If
		Next
		litGroups.Text = sb.ToString

	End Sub

End Class