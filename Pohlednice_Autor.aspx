<%@ Page Title="Informace o autorovi" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Pohlednice_Autor.aspx.vb" Inherits="_Pohlednice_Autor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<p style="margin-bottom: 4px; font-size: 1.3em;"><%#DR("AutorNick")%></p>
		<p id="pPohledniceAutoriJmeno" runat="server" style="margin-bottom: 2px;"><b>Jméno: </b><%#DR("AutorJmeno")%></p>
		<p id="pPohledniceAutoriAdresa" runat="server" style="margin-bottom: 2px;"><b>Adresa: </b>aaa</p>
		<p id="pPohledniceAutoriEmail" runat="server" style="margin-bottom: 2px;"><b>Email: </b><%#DR("AutorEmail")%></p>
		<p id="pPohledniceAutoriWeb" runat="server" style="margin-bottom: 2px;"><b>Web: </b><%#DR("AutorWeb")%></p>
		<p id="pPohledniceAutoriPopis" runat="server" style="margin-bottom: 2px;"><b>Popis: </b><%#DR("AutorPopis")%></p>
		<p style="margin-top: 8px;">»<a href="/Pohlednice/autor-<%#AutorID%>.aspx">Pøehled pohlednic tohoto autora</a></p>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>