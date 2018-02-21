<%@ Page Language="VB" Title="Kategorie" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="TxtKat.aspx.vb" Inherits="_TxtKat"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phMain" runat="server">
		<div id="divViewKatHtml" runat="server" style="margin-top: 6px;">#Zobrazí data pøipravená v kódu#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>