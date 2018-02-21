<%@ Page Title="Hledáme pomocníky" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Spolupracovnik.aspx.vb" Inherits="_Spolupracovnik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p>Hledáme:</p>
	<p>• <a href="/Spolupracovnik.aspx?akce=Editor">Editory</a></p>
	<p>• <a href="/Spolupracovnik.aspx?akce=Grafik">Grafika</a></p>
	<p>• <a href="/Spolupracovnik.aspx?akce=Pohlednice">Autory pohlednic</a></p>
	<p style="margin-bottom:4px; padding-bottom:4px; border-bottom: 1px solid #888888;">• <a href="/Spolupracovnik.aspx?akce=Jine">Jiné...</a></p>
	
	<asp:Panel id="pnlSpolupracovnikEditor" Runat="server" Visible="False">
		<p>Hledáme editory pro sekce:</p>
		<ul type="square" id="ulEditoriSekce" runat="server"></ul>
		<p style="margin-top:4px;">Úkolem je pravidelnì procházet nové pøíspìvky, øadit je do kategorií, opravovat pravopis, kontrola duplicity, mazání nevhodných pøíspìvkù apod.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikGrafik" Runat="server" Visible="False">
		<p>Hledáme šikovného èlovíèka na vylepšení v grafických èástí webu.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikPohlednice" Runat="server" Visible="False">
		<p>Pro sekci Pohlednice hledáme autory elektronických pohlednic (grafiky, kreslíøe, animátory). Pokud chcete utváøet tento web a uplatnit svùj talent, staòte se našim spolupracovníkem.</p>
		<p style="margin-top: 6px;">Ukázky vaši práce (nejlépe návrh nìjakého pøáníèka) oèekáváme na našem emailu.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikJine" Runat="server" Visible="False">
		<p>Máte-li nám co nabídnout, budeme rádi za každou pomocnou ruku.</p>
	</asp:Panel>
	<p style="margin-top:6px;">Èinnost není honorována! Proto hledáme <b>dobrovolníky a nadšence</b>, kteøí se chtìjí stát našimi spolupracovníky. </p>

</asp:Content>