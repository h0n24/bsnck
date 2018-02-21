<?xml version="1.0" encoding="UTF-8" ?>
<%@ Page Language="vb" ContentType="text/xml" AutoEventWireup="false" Inherits="_Rss" enableViewState="False" CodeFile="Rss.aspx.vb" %>
<rss version="2.0">
	<channel>
		<title><%#Server.HtmlEncode(MyIni.Web.Name)%></title>
		<link><%#MyIni.Web.Url%></link>
		<description><%#Server.HtmlEncode(MyIni.Web.Slogan)%></description>
		<pubDate><%#DatumPosledniho.AddHours(-1).ToString("ddd, dd MMM yyyy HH\:mm\:ss \G\M\T", System.Globalization.DateTimeFormatInfo.InvariantInfo)%></pubDate>
		<language>cs</language>
		<ttl>15</ttl>
		<image>
			<url><%#MyIni.Web.Url%>/gfx/reklama/moje/<%#MyIni.Web.ID%>_88x31.gif</url>
			<title><%#Server.HtmlEncode(MyIni.Web.Name & " - " & MyIni.Web.Slogan)%></title>
			<link><%#MyIni.Web.Url%></link>
			<width>88</width>
			<height>31</height>
		</image>
		<asp:repeater id="rptRssItems" runat="server">
			<itemtemplate>
				<item>
					<title><%#Server.HtmlEncode(Container.DataItem("Title"))%></title>
					<link><%#MyIni.Web.Url%>/<%#Container.DataItem("Sekce")%>/<%#Container.DataItem("ID")%>-view.aspx</link>
					<description><%#Server.HtmlEncode(Container.DataItem("Description"))%></description>
					<pubdate><%#CDate(Container.DataItem("Datum")).AddHours(-1).ToString("ddd, dd MMM yyyy HH\:mm\:ss \G\M\T", System.Globalization.DateTimeFormatInfo.InvariantInfo)%></pubdate>
				</item>
			</itemtemplate>
		</asp:repeater>
	</channel>
</rss>
