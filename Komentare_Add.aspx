<%@ Page Title="Nový komentáø" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Komentare_Add.aspx.vb" Inherits="_Komentare_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p style="font-size: 90%; margin-bottom: 10px;"><b>Dùležité upozornìní:</b><br/>&nbsp;&middot; používejte èeské znaky (ìšèøžýáíé).<br/>&nbsp;&middot; nepoužívejte html znaèky, nebudou použity<br/>&nbsp;&middot; pøed odesláním zkontrolujte pravopis.<br/>&nbsp;&middot; <span class="povinne">!</span> ..jsou povinné položky.</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p class="text-nadpis">Text sdìlení: <span class="povinne">!</span></p>
		<p style="margin-bottom: 6px;"><asp:TextBox id="tbText" runat="server" Width="99%" cssclass="input-text" Rows="10" Columns="54" TextMode="MultiLine"></asp:TextBox></p>
		<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>