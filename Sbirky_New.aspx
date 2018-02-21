<%@ Page Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Sbirky_New.aspx.vb" Inherits="Sbirky_New"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p style="font-size: 90%; margin-bottom: 10px;">Sbírky slouží k sloučení děl do jednoho celku (sbírka / kniha).<br/>&nbsp;&middot; <span class="povinne">!</span>...jsou povinné položky</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<a id="aDelete" runat="server" href="" onclick="if(!confirm('Smazat sbírku?')) return false;" style="float:right; color:#FFFFFF; background-color:#880000; padding: 0px 10px 0px 10px; margin-right:5px; border: 2px outset #888888;">!! SMAZAT sbírku !!</a>
		<div id="divKategorie" runat="server" style="margin-bottom:4px;">
			<span class="text-nadpis">Vyberte kategorii:</span><span class="povinne"> !</span><br/>
			<asp:listbox id="lbKategorie" runat="server" rows="1"><asp:ListItem Value="-" Text="- - - - - - - - -"/></asp:listbox>
		</div>
		<div id="divTitulek" runat="server" style="margin-bottom: 4px;">
			<p class="text-nadpis">Titulek:<span class="povinne"> !</span><span class="text-komentar"> (krátký název, max. 100 znaků)</span></p>
			<p><asp:textbox id="tbTitulek" runat="server" cssclass="input-text" Columns="40" /></p>
		</div>
		<div id="divProlog" runat="server" style="margin-bottom: 4px;">
			<p class="text-nadpis">Prolog:<span class="povinne"> !</span><span class="text-komentar"> (stručný předmluva (popis), max. 2000 znaků, zbývá <span id="charsLeft">?</span>)</span></p>
			<p><textarea rows="4" id="inpProlog" runat="server" cols="54" class="input-text" style="width:99%" onblur="CountTxt()" onchange="CountTxt()" onclick="CountTxt()" onfocus="CountTxt()" onkeyup="CountTxt()"></textarea></p>
		</div>
		<div id="divItems" runat="server" style="margin-bottom: 4px;">
			<div style="float:left"><span class="text-nadpis">Obsah sbírky:</span><br /><asp:ListBox ID="lbSbirka" runat="server" Rows="10" Width="200" /></div>
			<div style="float:right"><span class="text-nadpis">Moje díla:</span><br /><asp:ListBox ID="lbDila" runat="server" Rows="10" Width="200" /></div>
			<div style="text-align:center; padding-top:30px;">
				<div style="margin-bottom:3px;"><asp:Button ID="btnUp" runat="server" cssclass="submit" Text="&uarr;" ToolTip="posunout nahoru" UseSubmitBehavior="false" /></div>
				<div style="margin-bottom:10px;"><asp:Button ID="btnDown" runat="server" cssclass="submit" Text="&darr;" ToolTip="posunout dolu" UseSubmitBehavior="false" /></div>
				<div style="margin-bottom:3px;"><asp:Button ID="btnAdd" runat="server" cssclass="submit" Text="&laquo;" ToolTip="přidat do sbírky" UseSubmitBehavior="false" /></div>
				<div><asp:Button ID="btnRemove" runat="server" cssclass="submit" Text="&raquo;" ToolTip="odebrat ze sbírky" UseSubmitBehavior="false" /></div>
			</div>
		</div>
		<div id="divDokonceno" runat="server" style="margin-bottom: 4px; clear:both">
			<p class="text-nadpis"><asp:CheckBox ID="chbDokonceno" runat="server"/> Dokončeno<span class="text-komentar"> - publikovat jako dokočené s dnešním datem</span></p>
		</div>
		<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" OnClick="FormSubmit"/>

		<script type="text/javascript">
			<!--
			function CountTxt() {
				var str = document.forms[0].<%=inpProlog.ClientID%>.value;
				if (str.length < 2000) {
					document.getElementById("charsLeft").innerHTML = 2000 - str.length;
				} else {
					document.getElementById("charsLeft").innerHTML = '<b>0 !!</b>';
					document.forms[0].<%=inpProlog.ClientID%>.value = str.substr(0, 2000);
				} 
			}
			-->
		</script>

	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>