<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Vzkaz_View.aspx.vb" Inherits="_Vzkaz_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<div style="font-size:85%;"><span>Poslal(a): </span><asp:Label ID="lblOd" Runat="server" Font-Italic="True" /> | <asp:Label ID="lblDatum" Runat="server" Font-Italic="True" /></div>
		<div style="font-size:85%; margin-bottom:3px; padding-bottom:3px; border-bottom: 1px dotted #808080"><span>Pøedmìt: </span><asp:Label ID="lblPredmet" Runat="server" Font-Italic="True" /></div>
		<div style="font-style:italic"><asp:Label ID="lblTxt" Runat="server" /></div>
		<div style="margin-top:12px;">
			<a id="aReply" runat="server" href="/Vzkazy_Add.aspx?reply={0}" title="Odpovìdìt" class="box-menu" style="margin-right:4px">Odpovìdìt</a>
			<a id="aDelete" runat="server" href="/Vzkazy.aspx?akce=delete&amp;id={0}" title="Smazat" class="box-menu" style="margin-right:4px" onclick="if(!confirm('Smazat vzkaz?')) return false;">Smazat</a>
			<a href="/Vzkazy_Add.aspx" class="box-menu">Napiš nový vzkaz</a>
		</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>