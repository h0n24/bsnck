<%@ Page Title="Kniha návštìv" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="KnihaNavstev.aspx.vb" Inherits="_KnihaNavstev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="padding-bottom:4px;"><b>Návštìvní kniha slouží k zasílání vzkazù autorùm stránek - kritika, chvála, pøipomínky.</b></p>
	<asp:PlaceHolder ID="phMain" runat="server">
		<p style="padding-bottom:5px;"><a href="/KnihaNavstev_Add.aspx">++ Podepište se ++</a></p>
		<div id="divGuestBookHtml" runat="server">#Zobrazí data pøipravená v kódu#</div>
		<div id="divGuestBookPagesBox" runat="server" class="ListPagesBox">#1111111#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>