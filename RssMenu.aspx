<%@ Page Title="RSS kanály" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="RssMenu.aspx.vb" Inherits="_RssMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p style="margin-bottom:8px;">RSS slouží k naèítání novinek ze serveru.</p>
	<asp:Panel id="pnlRssMenuLiter" Runat="server" Visible="False">
		<p style="margin-bottom:6px;">• <a href="/Rss.aspx?sekce=Dila">Všechna díla</a></p>
		<p>• <a href="/Rss.aspx?sekce=Basne">Básnì</a></p>
		<p>• <a href="/Rss.aspx?sekce=Povidky">Povídky</a></p>
		<p>• <a href="/Rss.aspx?sekce=Uvahy">Úvahy</a></p>
		<p>• <a href="/Rss.aspx?sekce=Pohadky">Pohádky</a></p>
		<p>• <a href="/Rss.aspx?sekce=Fejetony">Fejetony</a></p>
		<p>• <a href="/Rss.aspx?sekce=Romany">Romány</a></p>
		<p>• <a href="/Rss.aspx?sekce=Reportaze">Reportáže</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuBasne" Runat="server" Visible="False">
		<p>• <a href="/Rss.aspx?sekce=Basne">Básnì</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuIfun" Runat="server" Visible="False">
		<p>• <a href="/Rss.aspx?sekce=Zabava">Zábavné texty</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuCituj" Runat="server" Visible="False">
		<p>• <a href="/Rss.aspx?sekce=Zamysleni">Texty k zamyšlení</a></p>
	</asp:Panel>

</asp:Content>