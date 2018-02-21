<%@ Page Title="Kniha návštìv » Nový záznam" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="KnihaNavstev_Add.aspx.vb" Inherits="_KnihaNavstev_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<form runat="server" id="Form1">
			<p style="font-size: 90%; margin-bottom: 10px;"><b>Dùležité upozornìní:</b><br/>&nbsp;&middot; používejte èeské znaky (ìšèøžýáíé)<br/>&nbsp;&middot; nepoužívejte html znaèky, nebudou použity<br/>&nbsp;&middot; pøed odesláním zkontrolujte pravopis<br/>&nbsp;&middot; <span class="povinne">!</span>...jsou povinné položky</p>
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<p class="text-nadpis">Vaše Jméno:<span class="povinne"> !</span></p>
			<p style="margin-bottom: 4px;"><asp:textbox id="tbJmeno" runat="server" cssclass="input-text" Columns="40" /></p>
			<p class="text-nadpis">Váš email:</p>
			<p style="margin-bottom: 4px;"><asp:textbox id="tbEmail" runat="server" cssclass="input-text" Columns="40" /></p>
			<p class="text-nadpis">Text sdìlení:<span class="povinne"> !</span></p>
			<p><textarea rows="7" id="txtTxt" runat="server" cols="50" class="textarea-povinne" onblur="CountTxt()" onchange="CountTxt()" onclick="CountTxt()" onfocus="CountTxt()" onkeyup="CountTxt()"></textarea></p>
			<p class="text-komentar" style="margin-bottom: 6px;">Pištì struènì a jasnì. Máte k dispozici 500 znakù, zbývá <span id="charsLeft">?</span>.</p>
			<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" />
		</form>

		<script type="text/javascript">
		<!--
		function CountTxt() {
			var str = document.forms[0].<%=txtTxt.ClientID %>.value;
			if (str.length < 500) {
				document.getElementById("charsLeft").innerHTML = 500 - str.length;
			} else {
				document.getElementById("charsLeft").innerHTML = '<b>0</b>';
				document.forms[0].<%=txtTxt.ClientID %>.value = str.substr(0,500);
			} 
		}
		-->
		</script>

</asp:Content>
