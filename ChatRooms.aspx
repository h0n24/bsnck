<%@ Page Title="Chat » místnosti" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="ChatRooms.aspx.vb" Inherits="_ChatRooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p style="padding-bottom:3px;">Nabízíme tyto místnosti pro konverzaci:</p>
	<p id="pDiskuzeEditoru" runat="server" style="padding-bottom:8px;">• <a href="Chat.aspx?room=734">Diskuze Editorù</a> (<%=PocetAktivnichLidi(734)%>)</p>
	<p style="padding-bottom:2px;">• <a href="Chat.aspx?room=2">Liter.cz - nápady, chyby, atd.</a> (<%=PocetAktivnichLidi(2)%>)</p>
	<p style="padding-bottom:2px;">• <a href="Chat.aspx?room=3">Názory a kritiky na díla</a> (<%=PocetAktivnichLidi(3)%>)</p>
	<p style="padding-bottom:2px;">• <a href="Chat.aspx?room=4">Pokec</a> (<%=PocetAktivnichLidi(4)%>)</p>

</asp:Content>