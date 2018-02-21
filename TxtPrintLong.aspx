<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Empty.master" AutoEventWireup="False" CodeFile="TxtPrintLong.aspx.vb" Inherits="_TxtPrintLong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<h2 id="txtPrintTitulek" runat="server" style="font-size: 1.3em; margin: 0px 0px 3px 0px; text-decoration: underline;">Titulek</h2>
		<p style="margin: 0px 0px 8px 0px; font-size: 0.85em;">
			(<span id="txtPrintAutor" runat="server">Autor</span>
			<span id="txtPrintDatum" runat="server">, Datum</span>
			<span id="txtPrintSekce" runat="server">, Sekce</span>)
		</p>
		<p id="txtPrintText" runat="server" style="margin:0px;">Text jednoho pøíspìvku</p>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>
