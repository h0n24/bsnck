<%@ Page Title="Registrace uživatele" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Users_Registrace.aspx.vb" Inherits="_Users_Registrace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<asp:panel id="pnlRegOK" runat="server" visible="False">
			<h5 style="margin-bottom:6px">Registrace uložena.</h5>
			<div>Na zadaný email bìhem nìkolika minut pøijde heslo pro první pøihlášení.<br/>
				Doporuèujeme heslo ihned po pøihlášení zmìnit na vlastní kvùli zapamatování - odkaz "Oprava údajù".</div>
		</asp:panel>

		<form id="Form1" runat="server">
			<input type="hidden" id="inpReferer" runat="server" />
			<div style="margin-bottom:6px;">Registrací získáte nìkolik výhod: Využití osobních kontaktù, vyplnìní jména, publikování vlastních textù, pøístup do uzavøených sekcí aj.</div>
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<div style="margin-bottom:6px;"><span class="povinne">!</span> ..jsou povinné položky.</div>
 			<div class="text-nadpis">Email: <span class="povinne">!</span></div>
 			<div><input type="text" id="inpEmail" class="input-text" size="40" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(používá se zároveò jako pøihlašovací jméno)</div>
			<div class="text-nadpis">Jméno a pøíjmení: <span class="povinne">!</span></div>
			<div><input type="text" id="inpJmeno" class="input-text" size="40" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(vaše celé jméno a pøíjmení)</div>
			<div class="text-nadpis">Pøezdívka: <span class="povinne">!</span></div>
			<div><input type="text" id="inpNick" class="input-text" size="30" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(takto vás budou vidìt ostatní návštìvníci, klidnì mùžete znovu zadat své jméno èi pøíjmení; pøezdívka musí být jedineèná v naši databázi)</div>
			<asp:panel id="pnlHeslo" runat="server">
				<div class="text-nadpis">Heslo: <span class="povinne">!</span></div>
				<div><input type="password" id="inpHeslo" class="input-text" size="15" runat="server" /></div>
				<div class="text-komentar" style="margin-bottom:6px;" id="txtRegistraceHeslo" runat="server">(zvolte si heslo)</div>
				<div class="text-nadpis">Heslo znovu: <span class="povinne">!</span></div>
				<div><input type="password" id="inpHeslo2" class="input-text" size="15" runat="server" /></div>
				<div class="text-komentar" style="margin-bottom:6px;">(zopakujte heslo - pro kontrolu pøeklepu)</div>
			</asp:panel>
			<div style="margin-bottom:6px;"><input type="checkbox" id="chbNews" checked runat="server" /> <span style="font-size: 0.8em;"> Chci dostávat informace o novinkách na tomto webu.</span></div>
			<input type="submit" value="» Registrovat «" class="submit" />
		</form>

</asp:Content>