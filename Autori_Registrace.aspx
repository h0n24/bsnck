<%@ Page Title="Registrace autora" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Autori_Registrace.aspx.vb" Inherits="_Autori_Registrace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p>Pot�ebujeme je�t� zn�t dal�� �daje o autorovi! Zadan� �daje budou ulo�eny ve va�em u�ivatelsk�m profilu.</p>
		<p style="margin-bottom:6px;">Ka�d� autor se zavazuje dodr�ovat <a href="Autori_Pravidla.aspx" target="_blank" style="font-weight:bold">Pravidla pro autory</a> !!</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="margin-bottom:6px;"><span class="povinne">!</span> ..jsou povinn� polo�ky.</p>
		<p class="text-nadpis">Datum narozen�:<span class="povinne"> !</span><span class="text-komentar"> (p��klad: 27.8.1983)</span></p>
		<p><asp:TextBox id="tbNarozen" cssclass="input-text" Columns="11" runat="server" /></p>
		<p class="text-komentar" style="margin-bottom: 6px;">(datum narozen� je nutn� kv�li zobrazen� v�ku autora.)</p>
		<p class="text-nadpis">Dal�� informace:<span class="povinne"> !</span><span class="text-komentar"> (�ivot, �sp�chy, z�liby, obl�ben� autor, ...)</span></p>
		<p style="margin-bottom:6px;"><asp:TextBox id="tbInfo" runat="server" Width="99%" cssclass="input-text" Rows="10" Columns="53" TextMode="MultiLine"></asp:TextBox></p>
		<asp:Button id="btSubmit" text="� Ulo�it �" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>