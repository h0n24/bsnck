<%@ Page Title="Nov� koment��" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Komentare_Add.aspx.vb" Inherits="_Komentare_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" />
		<p style="font-size: 90%; margin-bottom: 10px;"><b>D�le�it� upozorn�n�:</b><br/>&nbsp;&middot; pou��vejte �esk� znaky (��������).<br/>&nbsp;&middot; nepou��vejte html zna�ky, nebudou pou�ity<br/>&nbsp;&middot; p�ed odesl�n�m zkontrolujte pravopis.<br/>&nbsp;&middot; <span class="povinne">!</span> ..jsou povinn� polo�ky.</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p class="text-nadpis">Text sd�len�: <span class="povinne">!</span></p>
		<p style="margin-bottom: 6px;"><asp:TextBox id="tbText" runat="server" Width="99%" cssclass="input-text" Rows="10" Columns="54" TextMode="MultiLine"></asp:TextBox></p>
		<asp:Button id="btSubmit" text="� Ulo�it �" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>