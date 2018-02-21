<%@ Page Title="Hodnocen�" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Hodnoceni.aspx.vb" Inherits="_Hodnoceni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<p style="margin-bottom: 0px;">Aktu�ln� hodnocen�: <%#Hodnoceni%>%<br/>Po�et hlas�: <%#HodnPocet%></p>
		<div id="divHodnoceni" runat="server" class="box-menu" style="margin-top: 8px; width: 210px; padding: 8px 2px 2px 2px;">
			<div style="padding-left: 10px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="0" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input type="image" name="Submit" src="/gfx/hodnoceni/hodnoceni0.gif" alt="0 % .. Katastrofa" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="1" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni1.gif" alt="20% .. �patn�" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="2" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni2.gif" alt="40% .. Uch�zej�c�" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="3" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni3.gif" alt="60% .. Dobr�" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="4" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni4.gif" alt="80% V�born�" /></form>
			</div>
			<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
				<input name="h" type="hidden" value="5" /><input name="Referer" type="hidden" value="<%#Referer%>" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni5.gif" alt="100 % .. Nejlep��" /></form>
			</div>
			&nbsp;
			<p style="clear:left; margin-top:3px; font-size:85%;">Hlasujte kliknut�m na obr�zek.<br/>Na kvalitu m�me vlastn� vzorec!</p>
		</div>
		<p style="clear:left; font-size:85%; margin-top:5px;">� Hlasujte pouze jednou.<br/>� Pro v�po�et hodnocen� m�me vlastn� algoritmus zv�hod�uj�c� kvalitn� z�znamy.</p>
		<hr/>
		<div id="divHodnoceniHtml" runat="server">#Html#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>