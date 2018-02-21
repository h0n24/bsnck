<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Pohlednice_Preview.aspx.vb" Inherits="_Pohlednice_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<p id="BoxSortBy" runat="server" style="margin-bottom: 7px;">#Seøadit podle#</p>
		<p id="pPohlednicePreviewZaznamu" runat="server" style="margin-bottom: 3px;">#Poèet záznamù#</p>
		<asp:Repeater id="repPohledniceCompact" Runat="server">
			<ItemTemplate><a href='/Pohlednice_Poslat.aspx?id=<%#Container.DataItem("ID")%>'><img style="margin:3px; border: 1px solid #777777;" src='/Data/Pohledy/<%#Container.DataItem("Soubor")%>p.gif' alt="Pohlednice" /></a></ItemTemplate>
		</asp:Repeater>
		<asp:Repeater id="repPohledniceDetails" Runat="server">
			<ItemTemplate>
				<div style="clear:left; height:80px; margin:2px; padding-top:4px; padding-bottom:3px; border-top: 1px solid #999999;">
					<a href='/Pohlednice_Poslat.aspx?id=<%#Container.DataItem("ID")%>'><img style="float:left; margin-right:6px; border: 1px solid #777777;" src='/Data/Pohledy/<%#Container.DataItem("Soubor")%>p.gif' alt="Pohlednice"/></a>
					Název: <%#Container.DataItem("Titulek")%><br/>Autor: <%#Container.DataItem("AutorNick")%><br/>Pøidáno: <%#Container.DataItem("Datum")%><br/>Posláno: <%#Container.DataItem("Odeslano")%>x<br/>Hodnocení: <%#Container.DataItem("Hodnoceni")%>%
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>