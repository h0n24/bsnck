<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:panel id="pnlPrej" runat="server" visible="False">
		<p style="margin-bottom: 8px;">Vítáme Vás na stránce nabízející texty pøání, pozdravy a elektronické pohlednice.</p>
		<p style="margin-bottom: 8px;">Jedinì u nás naleznete nejširší vıbìr z rùznıch druhù pøáníèek a pohlednic. Pøej.cz je nejvìtší server tohoto druhu u nás, kde mùete zasílat textová pøáníèka na email nebo SMS.</p>
		<p>Urèitì jste dostali nìkolik pøání, pohledù nebo dopisù od svıch blízkıch, pøíbuznıch, kolegù, kamarádù a kamarádek k rùznım pøíleitostem. Myslíte, e jejich text byl zajímavı nebo legraèní? A my ho v databázi nemáme? Pøidejte ho.</p>
		<p style="margin-bottom: 8px;">Dìkujeme.</p> <a href="https://www.autodoc.cz/">www.Autodoc.Cz</a>
		<p style="font-size: 0.8em;">Struènì obsah: vánoèní pøání, pøání k narozeninám, pøání k svátku, novoroèní pøání, svatební pøání, sms pøání, velikonoèní pøání, elektronické pohlednice, internetové pohlednice, vánoèní pohlednice, animované pohlednice, velikonoèní pohlednice.</p>
	</asp:panel>
	<asp:panel id="pnlCitaty" runat="server" visible="False">
		<p>Vítejte na <b>nejvìtší</b> èeské stránce obsahující Citáty, Motta, Myšlenky, Aforismy, Vıroky, Pøísloví a Pranostiky.<br/><br/>
			Nejhledanìjší citáty: citáty o lásce, o ivotì, o pøátelství, o smutku, o smrti, o zklamání, o bolesti, o nešastné lásce, smutné.<br/><br/>
			Nejhledanìjší motta: o ivotì, ivotní motta, motta o lásce, o pøátelství, o smutku, o smrti, o zklamání.<br/><br/>
			Nejhledanìjší pøísloví a moudra: èeská pøísloví, latinská, anglická, japonská.<br/><br/>
			Pokud máte nìkde poznamenán Váš oblíbenı Citát, kterı nám chybí, pošlete nám ho a pomozte nám vytvoøit opravdu kompletní archiv citátù. Dìkujeme.
		</p>
		<p style="margin-top: 8px;"><b>Citát dne:</b></p>
		<p><!-- Citat Text1 --><%#CitatTxt%><!-- Citat Text2 --></p>
		<p>[<!-- Citat Autor1 --><%#CitatAutor%><!-- Citat Autor2-->]</p>
	</asp:panel>
	<asp:panel id="pnlBasne" runat="server" visible="False">
		Vítá Vás nejnavštìvovanìjší amatérskı literární server na èeském internetu.<br/>
		U nás naleznete nejširší vıbìr z vıtvorù amatérskıch spisovatelù, tj. návštìvníkù našich stránek.<br/><br/>
		Pokud jste napsali nìjaké pìkné básnì, povídky, pohádky, romány, úvahy, fejetony, které by mohly zaujmout naše ètenáøe, urèitì je vlote do naši databáze. 
		Sami se pak na základì hlasování èi poètu poslání mùete pøesvìdèit o kvalitì svého díla.<br/>
		<br/>
		Pro milovníky modré jsme pøiravili web doménu basne.cz<br/>
		Zastánci bílé jistì vyuijí spíše basnicky.cz<br/>
		<br/>
		Z obsahu vybíráme: zamilované básnièky o lásce, básnì o jaru, o pøátelství, k narozeninám, k svátku, na dobrou noc, na dobré ráno, smutné, pro maminku, pro dìti.<br/><a href="https://www.euautodily.cz/originalni-dily/opel">www.EuAutoDily.cz</a>
	</asp:panel>
	
</asp:Content>
