<%@ Page Language="VB" Title="�dr�ba datab�ze" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="DB_Udrzba.aspx.vb" Inherits="Admin_DB_Udrzba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<form runat="server" id="form1">
		<asp:CheckBox ID="chbNemazat" runat="server" AutoPostBack="true" Text="Demo - Nemazat z�znamy !!" />
	</form>

	<h3 style="margin-top:8px;">Nutno udr�ovat</h3>
	<ul style="margin-bottom:4px;"><li><a href="DB_Udrzba.aspx?akce=go">��V�e</a></li></ul>
	<ul><li><a href="DB_Udrzba.aspx?akce=Seznamka">Seznamka</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Pohlednice">Pohlednice</a></li>
		<li><a href="DB_Udrzba.aspx?akce=SmazaneTexty">Smazan� Texty trvale odstranit</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Vzkazy">Vzkazy (star�� 6 m�s�c�)</a></li>
	</ul>
	
	<h3 style="margin-top:8px;">Ji� o�et�eno triggerem - tud� jen pro kontrolu.</h3>
	<ul>
		<li><a href="DB_Udrzba.aspx?akce=Komentare">Komentare - odstran�n�ch [TxtDila,TxtLong,Sbirky]</a></li>
		<li><a href="DB_Udrzba.aspx?akce=Tipy">Tipy - odstran�n�ch [TxtDila,Sbirky]</a></li>
		<li><a href="DB_Udrzba.aspx?akce=SbirkyObsah">SbirkyObsah - odstran�n�ch</a></li>
		<li><a href="DB_Udrzba.aspx?akce=AdminEditHistory">AdminEditHistory odstran�n�ch [TxtDila,TxtLong,TxtShort,TxtCitaty]</a></li>
	</ul>

<br />		do�e�it (nezohled�uje sekci): <a href="DB_Udrzba.aspx?akce=Hodnoceni">Hodnoceni - odstran�n�ch [TxtLong,........]</a>

	<p id="pDBUdrazbaReport" runat="server" style="margin-top:8px;"></p>

</asp:Content>