<%@ Page Language="VB" Title="Vaše kontakty" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Users_Kontakty.aspx.vb" Inherits="_Users_Kontakty"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phKontakty" runat="server">
		<p style="margin-bottom: 6px;">» <a href="/Users_Kontakty.aspx?akce=new">Pøidej nový kontakt</a></p>
		<div id="divUsersKontaktyHtml" runat="server" style="margin-top: 6px;">#Zobrazí data pøipravená v kódu#</div>
	</asp:PlaceHolder>

	<form id="Form1" runat="server">
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="margin-bottom: 6px;"><span class="povinne">!</span> ..jsou povinné položky.</p>
		<p class="text-nadpis">Jméno:<span class="povinne"> !</span></p>
		<p style="margin-bottom: 6px;"><asp:TextBox id="tbJmeno" runat="server" cssclass="input-text" Columns="40" /></p>
		<p class="text-nadpis">Email:</p>
		<p style="margin-bottom: 6px;"><asp:TextBox id="tbEmail" runat="server" cssclass="input-text" Columns="40" /></p>
		<p class="text-nadpis">Telefon:<span class="text-komentar"> (zadejte ve tvaru 603123456 nebo +420 603123456)</span></p>
		<p style="margin-bottom: 8px;"><asp:TextBox id="tbTel" cssclass="input-text" Columns="20" runat="server" /></p>
		<input type="submit" value="» Uložit «" class="submit" style="float:left;" />
		<a id="aKontaktyDelete" runat="server" href="/Users_Kontakty.aspx?akce=delete&id=" style="float:right; color:#FFFFFF; background-color:#880000; padding: 0px 10px 0px 10px; border: 2px outset #888888;">!! SMAZAT !!</a>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>