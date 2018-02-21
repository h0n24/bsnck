<%@ Control Language="vb" AutoEventWireup="false" Inherits="_PohledniceFull_Ascx" CodeFile="PohledniceFull.ascx.vb" %>
<%@ Register TagPrefix="MOJE" TagName="Pohlednice" Src="~/App_Shared/Pohlednice.ascx" %>
<asp:Panel ID="pnlPohlednice" Runat="server" CssClass="box2">
	<p class="title" style="font-size:120%; text-align:center;"><%#sTitulek%></p>
	<MOJE:Pohlednice runat="server" Format="<%#sFormat%>" Soubor="<%#sSoubor%>" Rozmery="<%#sRozmery%>" />
	<p class="title">
		<span style="float: left;">Posláno <%#iOdeslano%>x, <a href="/Pohlednice/<%#iID%>-hodn.aspx" class="pamatovatklik">hodnocení</a> <%#iHodnoceni%>%</span>
		<span style="float: right;">Autor: <a href="/Pohlednice/autorinfo-<%#iAutorID%>.aspx" class="pamatovatklik"><%#sAutorNick%></a></span>&nbsp;
	</p>
</asp:Panel>
