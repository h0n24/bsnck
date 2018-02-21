<%@ Page Language="VB" Title="Admin" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:panel id="pnlAdminDefaultEditor" runat="server" Visible="False" Width="49%" CssClass="FloatLeft">
		<p style="margin-bottom:4px;">»<a href="PravidlaEditace.aspx"><b>Pravidla pro editaci záznamù!!</b></a></p>
		<ul><li id="liAdminDefaultVtipy" runat="server"><a href="Editace.aspx?sekce=Vtipy">Vtipy</a></li>
			<li id="liAdminDefaultZabava" runat="server"><a href="Editace.aspx?sekce=Zabava">Zábavné Texty</a> ... (<a href="Komentare.aspx?sekce=Zabava">komentáøe</a>)</li>
			<li id="liAdminDefaultPrani" runat="server"><a href="Editace.aspx?sekce=Prani">Pøání</a></li>
			<li id="liAdminDefaultRomantika" runat="server"><a href="Editace.aspx?sekce=Romantika">Romantika</a> ... (<a href="Komentare.aspx?sekce=Romantika">komentáøe</a>)</li>
			<li id="liAdminDefaultBasne" runat="server"><a href="Editace.aspx?sekce=Basne">Básnì</a> ... (<a href="Komentare.aspx?sekce=Basne">komentáøe</a>)</li>
			<li id="liAdminDefaultPovidky" runat="server"><a href="Editace.aspx?sekce=Povidky">Povídky</a> ... (<a href="Komentare.aspx?sekce=Povidky">komentáøe</a>)</li>
			<li id="liAdminDefaultUvahy" runat="server"><a href="Editace.aspx?sekce=Uvahy">Úvahy</a> ... (<a href="Komentare.aspx?sekce=Uvahy">komentáøe</a>)</li>
			<li id="liAdminDefaultPohadky" runat="server"><a href="Editace.aspx?sekce=Pohadky">Pohádky</a> ... (<a href="Komentare.aspx?sekce=Pohadky">komentáøe</a>)</li>
			<li id="liAdminDefaultFejetony" runat="server"><a href="Editace.aspx?sekce=Fejetony">Fejetony</a> ... (<a href="Komentare.aspx?sekce=Fejetony">komentáøe</a>)</li>
			<li id="liAdminDefaultRomany" runat="server"><a href="Editace.aspx?sekce=Romany">Romány</a> ... (<a href="Komentare.aspx?sekce=Romany">komentáøe</a>)</li>
			<li id="liAdminDefaultReportaze" runat="server"><a href="Editace.aspx?sekce=Reportaze">Reportáže</a> ... (<a href="Komentare.aspx?sekce=Reportaze">komentáøe</a>)</li>
			<li id="liAdminDefaultErotickePovidky" runat="server"><a href="Editace.aspx?sekce=ErotickePovidky">ErotickePovidky</a> ... (<a href="Komentare.aspx?sekce=ErotickePovidky">komentáøe</a>)</li>
			<li id="liAdminDefaultCitaty" runat="server"><a href="Editace.aspx?sekce=Citaty">Citáty</a> / <a href="Citaty_Autori.aspx">Autoøi citátù</a></li>
			<li id="liAdminDefaultPrislovi" runat="server"><a href="Editace.aspx?sekce=Prislovi">Pøísloví</a></li>
			<li id="liAdminDefaultMotta" runat="server"><a href="Editace.aspx?sekce=Motta">Motta</a></li>
			<li id="liAdminDefaultZamysleni" runat="server"><a href="Editace.aspx?sekce=Zamysleni">Zamyšlení</a> ... (<a href="Komentare.aspx?sekce=Zamysleni">komentáøe</a>)</li>
			<li id="liAdminDefaultSeznamka" runat="server"><a href="/Seznamka/kat-all.aspx">Seznamka</a></li>
		</ul>
	</asp:panel>

	<div style="float:right; width:49%">
		<asp:panel id="pnlAdminDefaultSql" runat="server" Visible="False">
			<p>Start aplikace: <%=CDate(Application("StartTime")).ToString("d.M. HH:mm:ss")%> (<%=WriteRunTime()%>)</p>
			<p>Requests: Aktivní <%=Application("RequestsActive") & " / Max " & Application("RequestsActiveMax")%> / Celkem <%=Application("RequestsCount")%></p>
			<p>Server: <%=Server.MachineName%></p>
			<p>Chyby (<%=CInt(Application("ErrorsCount"))%>) ... generuj <a href="/abcdef.aspx">404</a>, <a href="/Err.aspx?akce=generuj">500</a></p>

			<p style="margin-top:8px;"><b>Databáze:</b></p>
			<ul type="disc" style="margin-bottom:8px;">
				<li><a href="DB_Csv.aspx">Export/Import CSV</a></li>
				<li><a href="DB_Udrzba.aspx">Údržba</a></li>
				<li><a href="DB_Size.aspx">Velikost databáze</a></li>
				<li><a href="DBtoFileConversion.aspx">Texty z databáze uloží do textových souborù</a></li>
			</ul>
			<ul type="square" style="margin-bottom:8px;">
				<li><a href="Cache.aspx">Cache</a></li>
				<li><a href="/Set.aspx">Set.aspx (Startovací prostøedí)</a></li>
			</ul>
		</asp:panel>
		
		<ul type="circle" style="margin-bottom:8px;">
			<li id="liBlokovatUzivatele" runat="server"><a href="BlokovatUzivatele.aspx">Blokovat uživatele</a></li>
			<li id="liUsersSharingPC" runat="server"><a href="UsersSharingPC.aspx">Duplicitní nicky</a></li>
		</ul>

	</div>

</asp:Content>