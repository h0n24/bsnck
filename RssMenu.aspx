<%@ Page Title="RSS kan�ly" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="RssMenu.aspx.vb" Inherits="_RssMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p style="margin-bottom:8px;">RSS slou�� k na��t�n� novinek ze serveru.</p>
	<asp:Panel id="pnlRssMenuLiter" Runat="server" Visible="False">
		<p style="margin-bottom:6px;">� <a href="/Rss.aspx?sekce=Dila">V�echna d�la</a></p>
		<p>� <a href="/Rss.aspx?sekce=Basne">B�sn�</a></p>
		<p>� <a href="/Rss.aspx?sekce=Povidky">Pov�dky</a></p>
		<p>� <a href="/Rss.aspx?sekce=Uvahy">�vahy</a></p>
		<p>� <a href="/Rss.aspx?sekce=Pohadky">Poh�dky</a></p>
		<p>� <a href="/Rss.aspx?sekce=Fejetony">Fejetony</a></p>
		<p>� <a href="/Rss.aspx?sekce=Romany">Rom�ny</a></p>
		<p>� <a href="/Rss.aspx?sekce=Reportaze">Report�e</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuBasne" Runat="server" Visible="False">
		<p>� <a href="/Rss.aspx?sekce=Basne">B�sn�</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuIfun" Runat="server" Visible="False">
		<p>� <a href="/Rss.aspx?sekce=Zabava">Z�bavn� texty</a></p>
	</asp:Panel>
	<asp:Panel id="pnlRssMenuCituj" Runat="server" Visible="False">
		<p>� <a href="/Rss.aspx?sekce=Zamysleni">Texty k zamy�len�</a></p>
	</asp:Panel>

</asp:Content>