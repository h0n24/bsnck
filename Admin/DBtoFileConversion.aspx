<%@ Page Language="VB" Title="DB->Text" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="DBtoFileConversion.aspx.vb" Inherits="Admin_DBtoFileConversion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<p>Texty z databáze uloží do textových souborù.<br />
	<br />
	Minimální délka je <%=MinimalTextLenght%> znakù.</p>
	<p style="margin-top:10px; margin-bottom:10px;">Vyber tabulku:<br />
	TxtDila: <a href="?akce=go&amp;table=TxtDila">od zaèátku</a><br />
	<br />
	Zaèít od ID>XYZ pomoci parametru &amp;FromID=XYZ<br />
	</p>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>