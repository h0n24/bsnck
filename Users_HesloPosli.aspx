<%@ Page Language="VB" Title="Zapomenut� heslo" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Users_HesloPosli.aspx.vb" Inherits="_Users_HesloPosli"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server"/>
		<p style="margin-bottom:6px;">Pokud jste p�i registraci zadali sv�j email, m��ete si nechat zaslat zapomenut� heslo na tento email.</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="margin-top:6px;" align="center" class="text-nadpis">V� email nebo p�ezd�vka:</p>
		<div align="center" style="margin-bottom:4px;"><input id="inpInput" runat="server" size="40" /></div>
		<div align="center"><input class="submit" type="submit" value="� Po�li heslo �" /></div>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>