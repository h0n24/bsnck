<%@ Page Title="Kontakt" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Kontakt.aspx.vb" Inherits="_Kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<p style="margin-bottom:8px;">Pi�te na emailovou adresu <%=MyIni.Web.Email.Replace("@", "#")%> <i>(zam��te "#" za "@")</i>.</p>
		<p style="font-size:85%;">Z�m�rn� neuv�d�me cel� email, jeliko� n�m chodilo spousta spamu od robot� hledaj�ch adresy po webu.</p>
	
</asp:Content>