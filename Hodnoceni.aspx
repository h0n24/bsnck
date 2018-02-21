<%@ Page Title="Hodnocení" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Hodnoceni.aspx.vb" Inherits="_Hodnoceni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<p style="margin-bottom: 0px;">Aktuální hodnocení: <%#Hodnoceni%>%<br/>Poèet hlasù: <%#HodnPocet%></p>
		<div id="divHodnoceni" runat="server" class="box-menu" style="margin-top: 8px; width: 210px; padding: 8px 2px 2px 2px;">
			<div style="padding-left: 10px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="0" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input type="image" name="Submit" src="/gfx/hodnoceni/hodnoceni0.gif" alt="0 % .. Katastrofa" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="1" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni1.gif" alt="20% .. Špatné" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="2" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni2.gif" alt="40% .. Ucházející" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="3" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni3.gif" alt="60% .. Dobré" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="4" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni4.gif" alt="80% Výborné" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="5" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni5.gif" alt="100 % .. Nejlepší" /></form>
			</div>
			&nbsp;
			<p style="clear:left; margin-top:3px; font-size:85%;">Hlasujte kliknutím na obrázek.<br/>Na kvalitu máme vlastní vzorec!</p>
		</div>
		<p style="clear:left; font-size:85%; margin-top:5px;">• Hlasujte pouze jednou.<br/>• Pro výpoèet hodnocení máme vlastní algoritmus zvýhodòující kvalitní záznamy.</p>
		<hr/>
		<div id="divHodnoceniHtml" runat="server">#Html#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>