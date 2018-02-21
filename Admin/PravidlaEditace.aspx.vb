Class Admin_PravidlaEditace
	Inherits System.Web.UI.Page

	Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim MyUser As New Fog.User
		Dim bln As Boolean
		Dim dRows As DataRow() = FN.Cache.dtSekce.Select("SekceTable='TxtDila'")
		For Each Row As DataRow In dRows
			bln = bln Or MyUser.isAdminSekce(Row("SekceAlias"))
		Next
		ulDila.Visible = bln

		bln = False
		dRows = FN.Cache.dtSekce.Select("SekceTable='TxtLong' OR SekceTable='TxtShort' OR SekceTable='TxtCitaty'")
		For Each Row As DataRow In dRows
			bln = bln Or MyUser.isAdminSekce(Row("SekceAlias"))
		Next
		ulTexty.Visible = bln

		phPrani.Visible = MyUser.isAdminSekce("Prani")
		ulSeznamka.Visible = MyUser.isAdminSekce("Seznamka")
	End Sub

End Class