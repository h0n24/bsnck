<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Audit.ascx.vb" Inherits="App_Shared_Audit" %>

<asp:Label ID="lblText" runat="server" Visible="false" />

<asp:PlaceHolder ID="phGoogleAnalytics" runat="server">
<script type="text/javascript">
	var _gaq = _gaq || [];
	_gaq.push(['_setAccount', '<%=GoogleAnalyticsID %>']);
	_gaq.push(['_trackPageview']);

	(function() {
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	})();
</script>
</asp:PlaceHolder>

<asp:PlaceHolder ID="phToplist" runat="server">
<script language="JavaScript" type="text/javascript">
<!--
	document.write('<img src="http://toplist.cz/dot.asp?id=<%=ToplistID %>&amp;http=' + escape(document.referrer) + '" width="1" height="1" border=0 alt="TOPlist" />');
//--></script>
<noscript><img src="http://toplist.cz/dot.asp?id=<%=ToplistID %>" border="0" alt="TOPlist" width="1" height="1" /></noscript>
</asp:PlaceHolder>