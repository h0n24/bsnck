<%@ Page Language="VB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Doèasnì mimo provoz! Service temporarily unavailable!</title>
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
		<h1>Probíhá údržba a server je doèasì mimo provoz!</h1>
		<p>Pøedpokládána doba pøerušení je asi 5 minut.</p>
		<asp:Panel id="pnlUdrzba" runat="server" Visible="false">Jakmile bude server opìt funkèní, staèí pro dokonèení rozpracovaného požadavku na stránku použít v prohlížeèi funkci "aktualizovat".
			Takto lze také prùbìžnì zkoušet zda již server pracuje.
		</asp:Panel>
		<asp:Panel id="pnlZmena" runat="server" Visible="false">
			Doporuèujeme po obnovení provozu zaèít od hlavní stránky <%=HtmlOdkazRoot()%>
		</asp:Panel>
	</div>
</body>
</html>
