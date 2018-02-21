<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Obaly_List.aspx.vb" Inherits="_Obaly_List" %>
<%@ Register TagPrefix="MOJE" TagName="ObalyPismena" Src="~/App_Shared/ObalyPismena.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<MOJE:ObalyPismena runat="server" />

	<p style="font-weight:bold; margin-bottom:2px; font-style:italic; text-decoration:underline" id="pTitulek" runat="server">## Titulek ##</p>
		
	<asp:Panel ID="pnlObalyList" Runat="server" Visible="False">
		<asp:Repeater ID="rptSeznam" Runat="server">
			<ItemTemplate>• <a href='/Obaly_View.aspx?sekce=<%#Sekce%>&amp;id=<%#Container.DataItem("ObalID")%>'><%#Container.DataItem("ObalNazev")%></a><br/></ItemTemplate>
		</asp:Repeater>
	</asp:Panel>

	<asp:Panel ID="pnlObalyAutori" Runat="server" Visible="False">
		<asp:Repeater ID="rptAutori" Runat="server">
			<ItemTemplate>• <a href='/Obaly_List.aspx?sekce=Audio&amp;autor=<%#Server.UrlEncode(Container.DataItem("ObalAutor"))%>'><%#Container.DataItem("ObalAutor")%></a><br/></ItemTemplate>
		</asp:Repeater>
	</asp:Panel>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>