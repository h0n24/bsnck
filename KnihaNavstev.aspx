<%@ Page Title="Kniha n�v�t�v" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="KnihaNavstev.aspx.vb" Inherits="_KnihaNavstev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="padding-bottom:4px;"><b>N�v�t�vn� kniha slou�� k zas�l�n� vzkaz� autor�m str�nek - kritika, chv�la, p�ipom�nky.</b></p>
	<asp:PlaceHolder ID="phMain" runat="server">
		<p style="padding-bottom:5px;"><a href="/KnihaNavstev_Add.aspx">++ Podepi�te se ++</a></p>
		<div id="divGuestBookHtml" runat="server">#Zobraz� data p�ipraven� v k�du#</div>
		<div id="divGuestBookPagesBox" runat="server" class="ListPagesBox">#1111111#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>