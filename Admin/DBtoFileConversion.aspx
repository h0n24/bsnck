<%@ Page Language="VB" Title="DB->Text" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="DBtoFileConversion.aspx.vb" Inherits="Admin_DBtoFileConversion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<p>Texty z datab�ze ulo�� do textov�ch soubor�.<br />
	<br />
	Minim�ln� d�lka je <%=MinimalTextLenght%> znak�.</p>
	<p style="margin-top:10px; margin-bottom:10px;">Vyber tabulku:<br />
	TxtDila: <a href="?akce=go&amp;table=TxtDila">od za��tku</a><br />
	<br />
	Za��t od ID>XYZ pomoci parametru &amp;FromID=XYZ<br />
	</p>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>