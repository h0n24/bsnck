<%@ Page Title="Kontakt" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Kontakt.aspx.vb" Inherits="_Kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<p style="margin-bottom:8px;">Pište na emailovou adresu <%=MyIni.Web.Email.Replace("@", "#")%> <i>(zamìòte "#" za "@")</i>.</p>
		<p style="font-size:85%;">Zámìrnì neuvádíme celý email, jelikož nám chodilo spousta spamu od robotù hledajích adresy po webu.</p>
	
</asp:Content>