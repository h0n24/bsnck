<%@ Page Title="Chat � m�stnosti" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="ChatRooms.aspx.vb" Inherits="_ChatRooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p style="padding-bottom:3px;">Nab�z�me tyto m�stnosti pro konverzaci:</p>
	<p id="pDiskuzeEditoru" runat="server" style="padding-bottom:8px;">� <a href="Chat.aspx?room=734">Diskuze Editor�</a> (<%=PocetAktivnichLidi(734)%>)</p>
	<p style="padding-bottom:2px;">� <a href="Chat.aspx?room=2">Liter.cz - n�pady, chyby, atd.</a> (<%=PocetAktivnichLidi(2)%>)</p>
	<p style="padding-bottom:2px;">� <a href="Chat.aspx?room=3">N�zory a kritiky na d�la</a> (<%=PocetAktivnichLidi(3)%>)</p>
	<p style="padding-bottom:2px;">� <a href="Chat.aspx?room=4">Pokec</a> (<%=PocetAktivnichLidi(4)%>)</p>

</asp:Content>