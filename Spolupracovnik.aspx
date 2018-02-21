<%@ Page Title="Hled�me pomocn�ky" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Spolupracovnik.aspx.vb" Inherits="_Spolupracovnik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<p>Hled�me:</p>
	<p>� <a href="/Spolupracovnik.aspx?akce=Editor">Editory</a></p>
	<p>� <a href="/Spolupracovnik.aspx?akce=Grafik">Grafika</a></p>
	<p>� <a href="/Spolupracovnik.aspx?akce=Pohlednice">Autory pohlednic</a></p>
	<p style="margin-bottom:4px; padding-bottom:4px; border-bottom: 1px solid #888888;">� <a href="/Spolupracovnik.aspx?akce=Jine">Jin�...</a></p>
	
	<asp:Panel id="pnlSpolupracovnikEditor" Runat="server" Visible="False">
		<p>Hled�me editory pro sekce:</p>
		<ul type="square" id="ulEditoriSekce" runat="server"></ul>
		<p style="margin-top:4px;">�kolem je pravideln� proch�zet nov� p��sp�vky, �adit je do kategori�, opravovat pravopis, kontrola duplicity, maz�n� nevhodn�ch p��sp�vk� apod.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikGrafik" Runat="server" Visible="False">
		<p>Hled�me �ikovn�ho �lov��ka na vylep�en� v grafick�ch ��st� webu.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikPohlednice" Runat="server" Visible="False">
		<p>Pro sekci Pohlednice hled�me autory elektronick�ch pohlednic (grafiky, kresl��e, anim�tory). Pokud chcete utv��et tento web a uplatnit sv�j talent, sta�te se na�im spolupracovn�kem.</p>
		<p style="margin-top: 6px;">Uk�zky va�i pr�ce (nejl�pe n�vrh n�jak�ho p��n��ka) o�ek�v�me na na�em emailu.</p>
	</asp:Panel>
	<asp:Panel id="pnlSpolupracovnikJine" Runat="server" Visible="False">
		<p>M�te-li n�m co nab�dnout, budeme r�di za ka�dou pomocnou ruku.</p>
	</asp:Panel>
	<p style="margin-top:6px;">�innost nen� honorov�na! Proto hled�me <b>dobrovoln�ky a nad�ence</b>, kte�� se cht�j� st�t na�imi spolupracovn�ky. </p>

</asp:Content>