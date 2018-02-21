<%@ Page Language="VB" Title="Mùj inzerát" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Seznamka_Add.aspx.vb" Inherits="_Seznamka_Add"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<p><b>Dùležité upozornìní:</b></p>
		<ul style="font-size:90%; margin-bottom:10px;">
			<li>komerèní inzeráty a nabídky prostituce jsou zakázány</li>
			<li>používejte èeské znaky (ìšèøžýáíé)</li>
			<li>inzeráty nesmí obsahovat žádná telefonní èísla (možnost zneužití)</li>
			<li>musí se jednat o inzerát za úèelem seznámení jednotlivce nebo skupin</li>
			<li>inzerát musí být v souladu se zákony Èeské republiky</li>
			<li><span class="povinne">!</span>...jsou povinné položky</li>
		</ul>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr><td height="1" width="100"></td><td></td></tr>
			<tr><td align="right">Rubrika:<span class="povinne">!</span></td><td><asp:listbox id="lbRubrika" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Region:<span class="povinne">!</span></td><td><asp:listbox id="lbRegion" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Jméno:<span class="povinne">!</span></td><td><asp:textbox id="tbJmeno" cssclass="input-text" runat="server" columns="30" /></td></tr>
			<tr><td></td><td><span class="text-komentar">zadávejte pouze jméno bez pøíjmení</span></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Vìk:<span class="povinne">!</span></td><td><asp:textbox id="tbVek" cssclass="input-text" runat="server" columns="4" /> rokù</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Výška:</td><td><asp:textbox id="tbVyska" cssclass="input-text" runat="server" columns="4" /> cm</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Poèet dìtí:</td><td><asp:textbox id="tbDeti" cssclass="input-text" runat="server" columns="4" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Odpovìdi:</td><td><asp:radiobutton id="rbOdpovedi1" runat="server" groupname="Odpovedi" Text="pouze uložit, nic neposílej" /><br/>
				<asp:radiobutton id="rbOdpovedi2" runat="server" groupname="Odpovedi" checked="True" Text="pošli upozornìní o odpovìdi na email" />
				<%REM Èasem možná povolit, pokud bude email potvrzen. <br/><asp:radiobutton id="rbOdpovedi3" runat="server" groupname="Odpovedi" Text="pošli odpovìï na email" />%>
			</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Vzdìlání:</td><td><asp:listbox id="lbVzdelani" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Znamení:</td><td><asp:listbox id="lbZnameni" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Postava:</td><td><asp:listbox id="lbPostava" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Barva vlasù:</td><td><asp:listbox id="lbVlasy" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Kouøení:</td><td><asp:listbox id="lbKoureni" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Pití alkohlu:</td><td><asp:listbox id="lbAlkohol" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Koníèky:</td><td><asp:textbox id="tbKonicky" cssclass="input-text" runat="server" columns="43" width="99%" rows="2" textmode="MultiLine" />
				<p class="text-komentar">Vypište krátce vaše hlavní koníèky (sport, poèítaèe, pøíroda, ...)</p></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Text:<span class="povinne">!</span></td><td><asp:textbox id="tbTxt" cssclass="input-text" runat="server" columns="43" width="99%" rows="5" textmode="MultiLine" />
				<p class="text-komentar" style="color:red; font-style:italic;">Do textu inzerátu je zakázáno vkládat e-mailovou adresu, telefonní èíslo, komerèní nabídky, nabídky odmìn za služby nebo nabídky a potkávky prostituce.</p></td></tr>
			<tr><td></td><td><asp:button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" /></td></tr>
		</table>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>