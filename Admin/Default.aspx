<%@ Page Language="VB" Title="Admin" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:panel id="pnlAdminDefaultEditor" runat="server" Visible="False" Width="49%" CssClass="FloatLeft">
		<p style="margin-bottom:4px;">�<a href="PravidlaEditace.aspx"><b>Pravidla pro editaci z�znam�!!</b></a></p>
		<ul><li id="liAdminDefaultVtipy" runat="server"><a href="Editace.aspx?sekce=Vtipy">Vtipy</a></li>
			<li id="liAdminDefaultZabava" runat="server"><a href="Editace.aspx?sekce=Zabava">Z�bavn� Texty</a> ... (<a href="Komentare.aspx?sekce=Zabava">koment��e</a>)</li>
			<li id="liAdminDefaultPrani" runat="server"><a href="Editace.aspx?sekce=Prani">P��n�</a></li>
			<li id="liAdminDefaultRomantika" runat="server"><a href="Editace.aspx?sekce=Romantika">Romantika</a> ... (<a href="Komentare.aspx?sekce=Romantika">koment��e</a>)</li>
			<li id="liAdminDefaultBasne" runat="server"><a href="Editace.aspx?sekce=Basne">B�sn�</a> ... (<a href="Komentare.aspx?sekce=Basne">koment��e</a>)</li>
			<li id="liAdminDefaultPovidky" runat="server"><a href="Editace.aspx?sekce=Povidky">Pov�dky</a> ... (<a href="Komentare.aspx?sekce=Povidky">koment��e</a>)</li>
			<li id="liAdminDefaultUvahy" runat="server"><a href="Editace.aspx?sekce=Uvahy">�vahy</a> ... (<a href="Komentare.aspx?sekce=Uvahy">koment��e</a>)</li>
			<li id="liAdminDefaultPohadky" runat="server"><a href="Editace.aspx?sekce=Pohadky">Poh�dky</a> ... (<a href="Komentare.aspx?sekce=Pohadky">koment��e</a>)</li>
			<li id="liAdminDefaultFejetony" runat="server"><a href="Editace.aspx?sekce=Fejetony">Fejetony</a> ... (<a href="Komentare.aspx?sekce=Fejetony">koment��e</a>)</li>
			<li id="liAdminDefaultRomany" runat="server"><a href="Editace.aspx?sekce=Romany">Rom�ny</a> ... (<a href="Komentare.aspx?sekce=Romany">koment��e</a>)</li>
			<li id="liAdminDefaultReportaze" runat="server"><a href="Editace.aspx?sekce=Reportaze">Report�e</a> ... (<a href="Komentare.aspx?sekce=Reportaze">koment��e</a>)</li>
			<li id="liAdminDefaultErotickePovidky" runat="server"><a href="Editace.aspx?sekce=ErotickePovidky">ErotickePovidky</a> ... (<a href="Komentare.aspx?sekce=ErotickePovidky">koment��e</a>)</li>
			<li id="liAdminDefaultCitaty" runat="server"><a href="Editace.aspx?sekce=Citaty">Cit�ty</a> / <a href="Citaty_Autori.aspx">Auto�i cit�t�</a></li>
			<li id="liAdminDefaultPrislovi" runat="server"><a href="Editace.aspx?sekce=Prislovi">P��slov�</a></li>
			<li id="liAdminDefaultMotta" runat="server"><a href="Editace.aspx?sekce=Motta">Motta</a></li>
			<li id="liAdminDefaultZamysleni" runat="server"><a href="Editace.aspx?sekce=Zamysleni">Zamy�len�</a> ... (<a href="Komentare.aspx?sekce=Zamysleni">koment��e</a>)</li>
			<li id="liAdminDefaultSeznamka" runat="server"><a href="/Seznamka/kat-all.aspx">Seznamka</a></li>
		</ul>
	</asp:panel>

	<div style="float:right; width:49%">
		<asp:panel id="pnlAdminDefaultSql" runat="server" Visible="False">
			<p>Start aplikace: <%=CDate(Application("StartTime")).ToString("d.M. HH:mm:ss")%> (<%=WriteRunTime()%>)</p>
			<p>Requests: Aktivn� <%=Application("RequestsActive") & " / Max " & Application("RequestsActiveMax")%> / Celkem <%=Application("RequestsCount")%></p>
			<p>Server: <%=Server.MachineName%></p>
			<p>Chyby (<%=CInt(Application("ErrorsCount"))%>) ... generuj <a href="/abcdef.aspx">404</a>, <a href="/Err.aspx?akce=generuj">500</a></p>

			<p style="margin-top:8px;"><b>Datab�ze:</b></p>
			<ul type="disc" style="margin-bottom:8px;">
				<li><a href="DB_Csv.aspx">Export/Import CSV</a></li>
				<li><a href="DB_Udrzba.aspx">�dr�ba</a></li>
				<li><a href="DB_Size.aspx">Velikost datab�ze</a></li>
				<li><a href="DBtoFileConversion.aspx">Texty z datab�ze ulo�� do textov�ch soubor�</a></li>
			</ul>
			<ul type="square" style="margin-bottom:8px;">
				<li><a href="Cache.aspx">Cache</a></li>
				<li><a href="/Set.aspx">Set.aspx (Startovac� prost�ed�)</a></li>
			</ul>
		</asp:panel>
		
		<ul type="circle" style="margin-bottom:8px;">
			<li id="liBlokovatUzivatele" runat="server"><a href="BlokovatUzivatele.aspx">Blokovat u�ivatele</a></li>
			<li id="liUsersSharingPC" runat="server"><a href="UsersSharingPC.aspx">Duplicitn� nicky</a></li>
		</ul>

	</div>

</asp:Content>