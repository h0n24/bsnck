<%@ Page Language="VB" Title="Údržba databáze" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="DB_Udrzba.aspx.vb" Inherits="Admin_DB_Udrzba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<form runat="server" id="form1">
		<asp:CheckBox ID="chbNemazat" runat="server" AutoPostBack="true" Text="Demo - Nemazat záznamy !!" />
	</form>

	<h3 style="margin-top:8px;">Nutno udržovat</h3>
	<ul style="margin-bottom:4px;"><li><a href="DB_Udrzba.aspx?akce=go">»»Vše</a></li></ul>
	<ul><li><a href="DB_Udrzba.aspx?akce=Seznamka">Seznamka</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Pohlednice">Pohlednice</a></li>
		<li><a href="DB_Udrzba.aspx?akce=SmazaneTexty">Smazané Texty trvale odstranit</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Vzkazy">Vzkazy (starší 6 mìsícù)</a></li>
	</ul>
	
	<h3 style="margin-top:8px;">Již ošetøeno triggerem - tudíž jen pro kontrolu.</h3>
	<ul>
		<li><a href="DB_Udrzba.aspx?akce=Komentare">Komentare - odstranìných [TxtDila,TxtLong,Sbirky]</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Tipy">Tipy - odstranìných [TxtDila,Sbirky]</a></li>
		<li><a href="DB_Udrzba.aspx?akce=SbirkyObsah">SbirkyObsah - odstranìných</a></li>
		<li><a href="DB_Udrzba.aspx?akce=AdminEditHistory">AdminEditHistory odstranìných [TxtDila,TxtLong,TxtShort,TxtCitaty]</a></li>
	</ul>

<br />		doøešit (nezohledòuje sekci): <a href="DB_Udrzba.aspx?akce=Hodnoceni">Hodnoceni - odstranìných [TxtLong,........]</a>

	<p id="pDBUdrazbaReport" runat="server" style="margin-top:8px;"></p>

</asp:Content>