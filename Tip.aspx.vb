Class _Tip
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Select Case Request.QueryString("akce")
			Case "tipuj"
				Tipuj()
			Case ""
			Case "ok"
				Dim Report As New Renderer.Report
				Report.Title = "Tip byl p�id�n."
				Report.Text = "Nyn� se m��ete vr�tit zp�t a pokra�ovat v �innosti."
				Me.phReport.Controls.Add(Report.Render)
		End Select

	End Sub

	Sub Tipuj()
		Dim MyUser As New Fog.User
		Dim Report As New Renderer.Report
		Dim iID = Request.QueryString("id")
		Dim Sekce As New Fog.Sekce(Request.QueryString("sekce"))
		Dim Hodnota = Val.ToInt(Request.QueryString("val"))
		If Hodnota < 1 Or Hodnota > 2 Then
			Report.Text = "�patn� hodnota hlasov�n�."
		ElseIf MyUser.ID = 0 Then
			'-- Z�rove� ochrana proti robot�m
			Report.Text = "P�id�len� tipu je pouze pro registrovan�."
		Else
			Dim CMD As New System.Data.SqlClient.SqlCommand("", FN.DB.Open)
			If Sekce.Tabulka.hasTipy Then
				CMD.CommandText = "SELECT TipID FROM " & Sekce.Tabulka.Nazev & "Tipy WHERE TipDBID=" & iID & " AND TipUser = " & MyUser.ID
				Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader(CommandBehavior.SingleRow)
				If DR.Read Then Report.Text = "V� tip ji� byl v minulosti ud�len"
				DR.Close()
				CMD.CommandText = "SELECT Autor FROM " & Sekce.Tabulka.Nazev & " WHERE ID=" & iID & " AND Autor=" & MyUser.ID
				DR = CMD.ExecuteReader
				If DR.Read Then Report.Text = "Tip nelze zapo��tat pro vlastn� d�lo."
				DR.Close()
				If Report.Text = "" Then
					CMD.CommandText = "INSERT INTO " & Sekce.Tabulka.Nazev & "Tipy (TipDBID,TipUser,TipHodnota) VALUES (" & iID & "," & MyUser.ID & "," & Hodnota & ")"
					CMD.ExecuteNonQuery()
					'CMD.CommandText = "UPDATE TxtDila SET Tipy=Tipy+" & Hodnota & " WHERE ID=" & iID
					'CMD.ExecuteNonQuery()
				End If
			End If
			CMD.Connection.Close()
		End If
		If Report.Text <> "" Then
			Report.Status = Renderer.Report.Statusy.Err
			Report.Title = "CHYBA !!"
			Me.phReport.Controls.Add(Report.Render)
		Else
			If FN.URL.Referer.IndexOf("/Tip.aspx") <> -1 Then
				'-- V p��pad� p�ihla�en� ze str�nky Tip.aspx by do�lo k zacyklen� p�i redirectu na referera
				Response.Redirect("/Tip.aspx?akce=ok&sekce=" & Sekce.Alias & "&id=" & iID)
			Else
				FN.Redir(FN.URL.Referer, "/Tip.aspx?akce=ok&sekce=" & Sekce.Alias & "&id=" & iID)
			End If
		End If
	End Sub

End Class