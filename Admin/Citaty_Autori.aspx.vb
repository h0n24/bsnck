Class Admin_Citaty_Autori
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not (Page.IsPostBack) Then
			FormInit()
		End If
	End Sub

	Sub FormInit()
		Dim SQL As String = "SELECT AutorID,AutorJmeno FROM TxtCitatyAutori ORDER BY AutorJmeno"
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		Me.lbAutori.DataSource = DR
		Me.lbAutori.DataValueField = "AutorID"
		Me.lbAutori.DataTextField = "AutorJmeno"
		Me.DataBind()
		Me.lbAutori.Items.Insert(0, New ListItem("++++ Nový autor ++++", "0"))
		Me.Form1.Visible = True
		DR.Close()
		CMD.Connection.Close()
	End Sub

	Sub ButtonSave_Click(ByVal obj As Object, ByVal e As EventArgs)
		Dim Jmeno As String = Me.inpJmeno.Value.Trim
		Dim Zivot As String = Me.inpZivot.Value.Trim
		Dim Vyznam As String = Me.inpVyznam.Value.Trim
		Dim Popis As String = Me.txtPopis.InnerText.Trim
		Dim err As New Renderer.FormErrors
		If Jmeno = "" Then err.Add("Zadej jméno autora")
		If err.Count = 0 Then
			Dim AutorID As Integer = Me.lbAutori.SelectedValue
			Dim SQL As String
			If AutorID = 0 Then
				SQL = "INSERT INTO TxtCitatyAutori (AutorDatum, AutorJmeno, AutorZivot, AutorVyznam, AutorPopis) Values (" & FN.DB.GetDateTime(Now) & "," & FN.DB.GetText(Jmeno) & "," & FN.DB.GetText(Zivot) & "," & FN.DB.GetText(Vyznam) & "," & FN.DB.GetText(Popis) & ")"
			Else
				SQL = "UPDATE TxtCitatyAutori SET AutorJmeno=" & FN.DB.GetText(Jmeno) & ", AutorZivot=" & FN.DB.GetText(Zivot) & ", AutorVyznam=" & FN.DB.GetText(Vyznam) & ", AutorPopis=" & FN.DB.GetText(Popis) & " WHERE AutorID=" & AutorID
			End If
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			CMD.ExecuteNonQuery()
			CMD.Connection.Close()
			Response.Redirect("Citaty_Autori.aspx")
		Else
			Me.phErrors.Controls.Add(err.Render)
		End If
	End Sub

	Sub ChangeAutor(ByVal obj As Object, ByVal e As EventArgs)
		Me.pnlMainForm.Visible = True
		Dim AutorID As Integer = Me.lbAutori.SelectedValue
		If AutorID = 0 Then
			Me.inpJmeno.Value = ""
			Me.inpZivot.Value = ""
			Me.inpVyznam.Value = ""
			Me.txtPopis.InnerText = ""
			Me.btSave.Visible = True
			Me.btDelete.Visible = False
		Else
			If AutorID = 1 Or AutorID = 2 Then
				Me.btSave.Visible = False
				Me.btDelete.Visible = False
			Else
				Me.btSave.Visible = True
				Me.btDelete.Visible = True
			End If
			Dim SQL As String = "SELECT * FROM TxtCitatyAutori WHERE AutorID=" & AutorID
			Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
			Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
			DR.Read()
			Me.inpJmeno.Value = DR("AutorJmeno")
			Me.inpZivot.Value = DR("AutorZivot")
			Me.inpVyznam.Value = DR("AutorVyznam")
			Me.txtPopis.InnerText = DR("AutorPopis")
			DR.Close()
			CMD.Connection.Close()
		End If
	End Sub

	Sub ButtonDelete_Click(ByVal obj As Object, ByVal e As EventArgs)
		Dim AutorID As Integer = Me.lbAutori.SelectedValue
		Dim SQL As String = "DELETE FROM TxtCitatyAutori WHERE AutorID=" & AutorID
		Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, FN.DB.Open)
		CMD.ExecuteNonQuery()
		CMD.CommandText = "DELETE FROM TxtCitaty WHERE Autor=" & AutorID
		CMD.ExecuteNonQuery()
		CMD.Connection.Close()
		Response.Redirect("Citaty_Autori.aspx")
	End Sub

End Class