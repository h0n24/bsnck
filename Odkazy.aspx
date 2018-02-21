<%@ Page Title="Spøíznìné weby" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Empty.Master" AutoEventWireup="False" CodeFile="Odkazy.aspx.vb" Inherits="_Odkazy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<p><%=Request.Url.Host%>, <%=Now%></p>

	<div style="margin-top:20px;">
		<asp:Repeater ID="rptOdkazy1" Runat="server">
			<ItemTemplate><%#Container.DataItem%></ItemTemplate>
		</asp:Repeater>
	</div>

	<asp:Panel ID="pnlPrejCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Elektronické Pohlednice, vánoèní pohlednice, e pohlednice, animované pohlednice, internetové pohlednice, velikonoèní pohlednice, virtuální pohlednice</h2>
		<h2>pøání, vánoèní pøání, pøání k narozeninám, pøání k svátku, novoroèní pøání, svatební pøání, sms pøání, velikonoèní pøání, elektronická pøání, pøání k vánocùm, narozeninová pøání, pøání narozeniny, pøáníèka, elektronická pøáníèka</h2>
		<p>Mùete èíst a posílat pøáníèka a pohlednnice.</p>
	</asp:Panel>
	<asp:Panel ID="pnlLiterCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Knihy, Elektronické knihy, e knihy, Kníky a Recenze knih</h2>
		<h2>Sci-fi, Fantasy povídky, Pohádky, Úvahy, Fejetony Román Reportáe</h2>
		<h2>Milostné básnì, romantické básnì, zamilované básnì, zamilované básnièky, romantické básnièky, milostná poezie</h2>
		<p>U nás si vyberete. Mùete èíst rùzné ánry a publikovat vlatní díla.</p>
	</asp:Panel>
	<asp:Panel ID="pnlCitaty" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Citáty, Aforismy, Motta, Myšlenky, Pøísloví</h2>
		<h2>Nejhledanìjší citáty slavnıch: citáty o lásce, o ivotì, o pøátelství, o smutku, o smrti, o zklamání, o bolesti, o nešastné lásce, smutné.</h2>
		<p>Mùete èíst a posílat citáty a další texty k zamyšlení.</p>
	</asp:Panel>
	<asp:Panel ID="pnlBasneCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Milostné básnì, romantické básnì, zamilované básnì, Verše a poezie</h2>
		<p>Mùete èíst mnoho krásnıch básní.</p>
	</asp:Panel>
	<asp:Panel ID="pnlBasnickyCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Zamilované básnièky, romantické básnièky, milostná poezie</h2>
		<p>Mnoho básnièek je nachystáno a vy mùete èíst.</p>
	</asp:Panel>
	<p>Tajnı kód: <%=FN.Text.RandomString(50 + Int(50 * Rnd))%></p>

</asp:Content>