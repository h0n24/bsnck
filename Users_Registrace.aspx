<%@ Page Title="Registrace u�ivatele" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Users_Registrace.aspx.vb" Inherits="_Users_Registrace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<asp:panel id="pnlRegOK" runat="server" visible="False">
			<h5 style="margin-bottom:6px">Registrace ulo�ena.</h5>
			<div>Na zadan� email b�hem n�kolika minut p�ijde heslo pro prvn� p�ihl�en�.<br/>
				Doporu�ujeme heslo ihned po p�ihl�en� zm�nit na vlastn� kv�li zapamatov�n� - odkaz "Oprava �daj�".</div>
		</asp:panel>

		<form id="Form1" runat="server">
			<input type="hidden" id="inpReferer" runat="server" />
			<div style="margin-bottom:6px;">Registrac� z�sk�te n�kolik v�hod: Vyu�it� osobn�ch kontakt�, vypln�n� jm�na, publikov�n� vlastn�ch text�, p��stup do uzav�en�ch sekc� aj.</div>
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<div style="margin-bottom:6px;"><span class="povinne">!</span> ..jsou povinn� polo�ky.</div>
 			<div class="text-nadpis">Email: <span class="povinne">!</span></div>
 			<div><input type="text" id="inpEmail" class="input-text" size="40" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(pou��v� se z�rove� jako p�ihla�ovac� jm�no)</div>
			<div class="text-nadpis">Jm�no a p��jmen�: <span class="povinne">!</span></div>
			<div><input type="text" id="inpJmeno" class="input-text" size="40" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(va�e cel� jm�no a p��jmen�)</div>
			<div class="text-nadpis">P�ezd�vka: <span class="povinne">!</span></div>
			<div><input type="text" id="inpNick" class="input-text" size="30" runat="server" /></div>
			<div class="text-komentar" style="margin-bottom:6px;">(takto v�s budou vid�t ostatn� n�v�t�vn�ci, klidn� m��ete znovu zadat sv� jm�no �i p��jmen�; p�ezd�vka mus� b�t jedine�n� v na�i datab�zi)</div>
			<asp:panel id="pnlHeslo" runat="server">
				<div class="text-nadpis">Heslo: <span class="povinne">!</span></div>
				<div><input type="password" id="inpHeslo" class="input-text" size="15" runat="server" /></div>
				<div class="text-komentar" style="margin-bottom:6px;" id="txtRegistraceHeslo" runat="server">(zvolte si heslo)</div>
				<div class="text-nadpis">Heslo znovu: <span class="povinne">!</span></div>
				<div><input type="password" id="inpHeslo2" class="input-text" size="15" runat="server" /></div>
				<div class="text-komentar" style="margin-bottom:6px;">(zopakujte heslo - pro kontrolu p�eklepu)</div>
			</asp:panel>
			<div style="margin-bottom:6px;"><input type="checkbox" id="chbNews" checked runat="server" /> <span style="font-size: 0.8em;"> Chci dost�vat informace o novink�ch na tomto webu.</span></div>
			<input type="submit" value="� Registrovat �" class="submit" />
		</form>

</asp:Content>