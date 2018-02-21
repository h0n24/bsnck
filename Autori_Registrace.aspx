<%@ Page Title="Registrace autora" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Autori_Registrace.aspx.vb" Inherits="_Autori_Registrace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p>Potøebujeme ještì znát další údaje o autorovi! Zadané údaje budou uloženy ve vašem uživatelském profilu.</p>
		<p style="margin-bottom:6px;">Každý autor se zavazuje dodržovat <a href="Autori_Pravidla.aspx" target="_blank" style="font-weight:bold">Pravidla pro autory</a> !!</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="margin-bottom:6px;"><span class="povinne">!</span> ..jsou povinné položky.</p>
		<p class="text-nadpis">Datum narození:<span class="povinne"> !</span><span class="text-komentar"> (pøíklad: 27.8.1983)</span></p>
		<p><asp:TextBox id="tbNarozen" cssclass="input-text" Columns="11" runat="server" /></p>
		<p class="text-komentar" style="margin-bottom: 6px;">(datum narození je nutný kvùli zobrazení vìku autora.)</p>
		<p class="text-nadpis">Další informace:<span class="povinne"> !</span><span class="text-komentar"> (život, úspìchy, záliby, oblíbený autor, ...)</span></p>
		<p style="margin-bottom:6px;"><asp:TextBox id="tbInfo" runat="server" Width="99%" cssclass="input-text" Rows="10" Columns="53" TextMode="MultiLine"></asp:TextBox></p>
		<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>