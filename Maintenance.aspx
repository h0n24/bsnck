<%@ Page Language="VB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Do�asn� mimo provoz! Service temporarily unavailable!</title>
</head>
<script runat="server">
	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		Response.StatusCode = 503
		'Response.StatusDescription = ""

		'pnlUdrzba.Visible = True
		pnlZmena.Visible = True
	End Sub
	
	Function HtmlOdkazRoot() As String
		Dim Domena As String = Request.Url.Host
		Return "<a href='http://" & Domena & "/'>" & Domena & "</a>"
	End Function
</script>
<body>
	<div style="background-color:#FFBBBB; border: 1px solid black; padding:5px;">
		<h1>Prob�h� �dr�ba a server je do�as� mimo provoz!</h1>
		<p>P�edpokl�d�na doba p�eru�en� je asi 5 minut.</p>
		<asp:Panel id="pnlUdrzba" runat="server" Visible="false">Jakmile bude server op�t funk�n�, sta�� pro dokon�en� rozpracovan�ho po�adavku na str�nku pou��t v prohl�e�i funkci "aktualizovat".
			Takto lze tak� pr�b�n� zkou�et zda ji� server pracuje.
		</asp:Panel>
		<asp:Panel id="pnlZmena" runat="server" Visible="false">
			Doporu�ujeme po obnoven� provozu za��t od hlavn� str�nky <%=HtmlOdkazRoot()%>
		</asp:Panel>
	</div>
</body>
</html>
