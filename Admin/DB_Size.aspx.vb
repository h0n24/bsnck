Partial Class Admin_DB_Size
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim dtTables As DataTable = FN.DB.DatabaseTables
		Dim dtSizes As New DataTable("Sizes")
		Dim dRow As DataRow
		dtSizes.Columns.Add("Name", Type.GetType("System.String"))
		dtSizes.Columns.Add("Rows", Type.GetType("System.String"))
		dtSizes.Columns.Add("Reserved", Type.GetType("System.String"))
		dtSizes.Columns.Add("Data", Type.GetType("System.String"))
		dtSizes.Columns.Add("Index_Size", Type.GetType("System.String"))
		dtSizes.Columns.Add("Unused", Type.GetType("System.String"))
		Dim CMD As New System.Data.SqlClient.SqlCommand("sp_spaceused", FN.DB.Open)
		Dim DR As System.Data.SqlClient.SqlDataReader = CMD.ExecuteReader
		Me.dgDatabase.DataSource = DR
		Me.dgDatabase.DataBind()
		DR.Close()
		For t As Int16 = 0 To dtTables.Rows.Count - 1
			CMD.CommandText = "sp_spaceused " & dtTables.Rows(t)(0)
			DR = CMD.ExecuteReader()
			If DR.Read Then
				dRow = dtSizes.NewRow
				For f As Int16 = 0 To DR.FieldCount - 1
					dRow(f) = DR(f)
				Next
				dtSizes.Rows.Add(dRow)
			End If
			DR.Close()
		Next
		Me.dgTables.DataSource = dtSizes
		Me.dgTables.DataBind()
		CMD.Connection.Close()
	End Sub

End Class
