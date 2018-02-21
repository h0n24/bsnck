<%@ Page Title="F�rum - nov� p��sp�vek" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Forum_Add.aspx.vb" Inherits="_Forum_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<h6>Pravidla:</h6>
		<ul style="font-size:85%; margin-bottom:6px;">
			<li>P�edm�t pi�te jasn� - �patn�: "<span style="font-style:italic;">pomoc</span>", dob�e: "<span style="font-style:italic;">Nefunguje mi odkaz na..</span>"</li>
			<li>Vulg�rn� slova a osobn� napad�n� nejsou tolerov�ny</li>
			<li>Soukrom� inzer�ty jen v bazaru - komer�n� inzer�ty pouze po dohod�</li>
			<li>Spr�vce a redakce si vyhrazuj� pr�vo odm�tnout nevhodn� p��sp�vky</li>
			<li><span class="povinne">!</span>...jsou povinn� polo�ky</li>
		</ul>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<table class="form">
			<tr><th>P�edm�t:<span class="povinne">!</span></th>
				<td class="mezera"><asp:textbox id="tbSubject" runat="server" width="380px" /></td>
			</tr>
			<tr><th style="vertical-align:top"><div>Text:<span class="povinne">!</span></div>
				<div style="margin-top:6px; font-weight:normal; font-size:80%">maximum 900 znak�, zb�v� <span id="charsLeft">?</span></div></th>
				<td><asp:textbox id="tbText" runat="server" textmode="MultiLine" width="380px" Rows="10" onblur="CountTxt()" onchange="CountTxt()" onclick="CountTxt()" onfocus="CountTxt()" onkeyup="CountTxt()" /></td>
			</tr>
			<tr><th></th><td class="mezera"><asp:checkbox id="cbReply" runat="server" text="" checked="True" /> pos�lat emailem odpov�di na m�j koment��</td></tr>
			<tr><th></th>
				<td><asp:button id="btSubmit" runat="server" text="� Odeslat �" CssClass="submit" /></td>
			</tr>
		</table>

		<script type="text/javascript">
			<!--
			function CountTxt() {
				var str = document.forms[0].<%=tbText.ClientID%>.value;
				if (str.length < 900) {
					document.getElementById("charsLeft").innerHTML = 900 - str.length;
				} else {
					document.getElementById("charsLeft").innerHTML = '<b>0</b>';
					document.forms[0].<%=tbText.ClientID%>.value = str.substr(0, 900);
				} 
			}
			-->
		</script>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>