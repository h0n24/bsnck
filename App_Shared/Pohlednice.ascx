<%@ Control Language="vb" AutoEventWireup="false" Inherits="_Pohlednice_Ascx" CodeFile="Pohlednice.ascx.vb" %>

<asp:Panel ID="pnlPohledniceImg" runat="server" HorizontalAlign="Center" Visible="False">
	<img style="border: 1px solid #888888;" width="<%#sSirka%>" height="<%#sVyska%>" alt="Pohlednice" src="/data/pohledy/<%#sSoubor & "." & sFormat%>" />
</asp:Panel>
<asp:Panel ID="pnlPohledniceFlash" runat="server" HorizontalAlign="Center" Visible="False">
	<object width="<%#sSirka%>" height="<%#sVyska%>" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0">
		<param name="_cx" value="26" />
		<param name="_cy" value="26" />
		<param name="FlashVars" value="26" />
		<param name="Movie" value="data/pohledy/<%#sSoubor%>.swf" />
		<param name="Src" value="data/pohledy/<%#sSoubor%>.swf" />
		<param name="WMode" value="Window" />
		<param name="Play" value="-1" />
		<param name="Loop" value="-1" />
		<param name="Quality" value="High" />
		<param name="SAlign" value="" />
		<param name="Menu" value="-1" />
		<param name="Base" value="" />
		<param name="AllowScriptAccess" value="always" />
		<param name="Scale" value="ShowAll" />
		<param name="DeviceFont" value="0" />
		<param name="EmbedMovie" value="0" />
		<param name="BGColor" value="" />
		<param name="SWRemote" value="" />
		<embed src="/data/pohledy/<%#sSoubor%>.swf" quality="high" bgcolor="white" width="<%=sSirka%>" height="<%=sVyska%>" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"></embed>
	</object>
</asp:Panel>
<asp:Panel ID="pnlPohledniceErr" runat="server" Visible="False">CHYBA !!</asp:Panel>
