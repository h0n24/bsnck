<%@ Page Language="VB" Title="Detail inzer�tu" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Seznamka_Detaily.aspx.vb" Inherits="_Seznamka_Detaily"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<p style="margin-bottom:3px; font-size:80%"><b>Text: </b><%#Server.HtmlEncode(Txt)%></p>
		<p style="margin-bottom:6px"><%#sDetaily%></p>
		<p style="font-style:italic; margin-bottom:3px; font-size:85%"><b>Pozor!</b> Neodpov�dejte na inzer�ty obsahuj�c� telefonn� ��slo nebo email, proto�e se patrn� jedn� o n�jakou formu podvodu.</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr><td></td><td><span class="povinne">!</span>...jsou povinn� polo�ky</td></tr>
			<tr><td align="right">Va�e&nbsp;jm�no:<span class="povinne">!</span></td><td><asp:textbox id="tbJmeno" cssclass="input-text" runat="server" columns="40" AutoCompleteType="FirstName" /></td></tr>
			<tr><td align="right">Email:</td><td><asp:textbox id="tbEmail" cssclass="input-text" runat="server" columns="40" AutoCompleteType="Email" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Odpov��:<span class="povinne">!</span></td><td><asp:textbox id="tbTxt" cssclass="input-text" runat="server" columns="43" width="99%" rows="5" textmode="MultiLine" /></td></tr>
			<tr><td></td><td><div class="text-komentar">Nezapome�te p�ipsat alespo� jeden kontakt na v�s!<br/>Tip: pokud chcete ohromit, zkuste se inspirovat mezi p��n��ky nebo cit�ty.</div></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Ochrana:<span class="povinne">!</span></td><td><asp:textbox id="tbOchrana" AutoCompleteType="disabled" cssclass="input-text" runat="server" columns="3" textmode="SingleLine" /> <asp:label id="lblOchrana" runat="server" style="font-weight:bold"></asp:label><br/><span class="text-komentar"> ... opi�te 3 znaky do pol��ka (ochrana proti robot�m)</span></td></tr>
			<tr><td height="4"></td></tr>
			<tr><td></td><td><asp:button id="btSubmit" text="� Odpov�d�t �" runat="server" cssclass="submit" /></td></tr>
		</table>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>