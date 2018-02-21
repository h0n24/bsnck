<%@ Page Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Add.aspx.vb" Inherits="_Add"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p style="font-size: 90%; margin-bottom: 10px;"><b>Dùležité upozornìní:</b><br/>&nbsp;&middot; používejte èeské znaky (ìšèøžýáíé)<br/>&nbsp;&middot; nepoužívejte html znaèky, nebudou použity<br/>&nbsp;&middot; pøed odesláním zkontrolujte pravopis<br/>&nbsp;&middot; redakce si vyhrazuje právo odmítnout nevhodné pøíspìvky<br/>&nbsp;&middot; <span class="povinne">!</span>...jsou povinné položky</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<a id="aAddDelete" runat="server" href="" onclick="if(!confirm('Smazat dílo?')) return false;" style="float:right; margin-right:5px;" class="ButtonDelete">!! SMAZAT !!</a>
		<div id="divAddSekce" runat="server">
			<p class="text-nadpis">Vyberte sekci:<span class="povinne"> !</span></p>
			<div style="margin-bottom: 4px;"><asp:listbox id="lbSekce" runat="server" rows="1" onselectedindexchanged="SekceChanged" autopostback="true" />
				&nbsp;<asp:listbox id="lbKategorie" runat="server" rows="1" Visible="False" />
			</div>
		</div>
		<div id="divAddJmeno" runat="server">
			<p class="text-nadpis">Vaše Jméno:<span class="povinne"> !</span></p>
			<p style="margin-bottom: 4px;"><asp:textbox id="tbJmeno" runat="server" cssclass="input-text" Columns="40" /></p>
		</div>
		<div id="divAddTitulek" runat="server">
			<p class="text-nadpis">Titulek:<span class="povinne"> !</span><span class="text-komentar"> (krátký název, max. 100 znakù)</span></p>
			<p style="margin-bottom: 4px;"><asp:textbox id="tbTitulek" runat="server" cssclass="input-text" Columns="40" /></p>
		</div>
		<div id="divAddAnotace" runat="server">
			<p class="text-nadpis">Anotace:<span class="povinne"> !</span><span class="text-komentar"> (struèný popis, max. 255 znakù, zbývá <span id="charsLeft">?</span>)</span></p>
			<p style="margin-bottom: 4px;"><textarea rows="3" id="inpAnotace" runat="server" cols="54" class="input-text" style="width:99%" onblur="CountTxt()" onchange="CountTxt()" onclick="CountTxt()" onfocus="CountTxt()" onkeyup="CountTxt()"></textarea></p>
		</div>
		<p class="text-nadpis">Text:<span class="povinne"> !</span></p>
		<p style="margin-bottom: 4px;"><asp:TextBox id="tbTxt" cssclass="input-text" runat="server" Columns="54" Width="99%" Rows="10" TextMode="MultiLine" /></p>
		<div id="divSbirka" runat="server">
			<p class="text-nadpis">Zaøadit do sbírky/knihy: <span class="text-komentar"> (slouèí díla do jednoho celku)</span></p>
			<div style="margin-bottom: 4px;"><asp:listbox id="lbSbirka" runat="server" rows="1"/></div>
		</div>
		<p class="text-nadpis">Vzkaz editorùm:<span class="text-komentar"> (napø. mùžete doporuèit kategorii pro zaøazení)</span></p>
		<p style="margin-bottom: 4px;"><asp:TextBox id="tbVzkaz" runat="server" cssclass="input-text" Columns="60" Width="99%" /></p>
		<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" OnClick="FormSubmit" />

		<script type="text/javascript">
			<!--
			function CountTxt() {
				var str = document.forms[0].<%=inpAnotace.ClientID%>.value;
				if (str.length < 255) {
					document.getElementById("charsLeft").innerHTML = 255 - str.length;
				} else {
					document.getElementById("charsLeft").innerHTML = '<b>0</b>';
					document.forms[0].<%=inpAnotace.ClientID%>.value = str.substr(0, 255);
				} 
			}
			-->
		</script>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>