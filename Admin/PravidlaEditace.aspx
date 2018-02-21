<%@ Page Language="VB" Title="Pravidla editace" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="PravidlaEditace.aspx.vb" Inherits="Admin_PravidlaEditace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="margin-bottom:8px;"><b>Pravidla pro Editaci záznamù</b></p>
	<p><b>Dùležité:</b></p>
	<ul type="circle" style="margin-bottom:5px;">
		<li>Všechny tvé úkony jsou zaznamenávány a zálohovány.</li>
		<li>Když se nìkomu povede omylem smazat nebo zmìnit dobrý záznam, mùžu ho po upozornìní z této zálohy obnovit.</li>
		<li>Pokud by nìkdo chtìl úmyslnì mazat èi jinak poškozovat záznamy se zámìrem poškodit databázi, vystavuje se možnosti trestního stíhání.</li>
		<li>Když neznáš pravopis (nebo i jen tak pro kontrolu), zkopíruj text (Ctrl+C) a vlož do Wordu (Ctrl+V), který má kontrolu pravopisu.</li>
		<li>Objevíš-li další problematický bod, napiš mi.</li>
		<li>Když si nevíš rady, zeptej se mì.</li>
	</ul>
	<p><b>Editace:</b></p>
	<ul type="square" style="margin-bottom:5px;">
		<li>Mazat vulgární, urážející, protiprávní texty.</li>
		<li>Mazat a kontrolovat duplicitu (nìkteré pøíspìvky jsem èetl 5x).</li>
		<li>Zaøazovat záznamy z nezaøazených do patøièných kategorií (pokud chceš pøidat novou kategorii, napiš a domluvíme se).</li>
		<li>Nepoužívat pøíliš VELKÁ PÍSMENA (pouze na zaèátku vìt nebo jmen). Pokud bude celý pøíspìvek psán velkým písmem, pak ho pøepiš na malá nebo smaž.</li>
		<li>Smazat nadbyteèné texty èi reklamnu na jiné stránky (odkazy na www stránky, emaily aj., osobní vzkazy).</li>
		<li>Za èárkou, teèkou a jinou interpunkcí nechávat mezeru (kvùli pøehlednosti a jednotnosti).</li>
		<li>Nenechávat prázdné øádky mezi vìtami èi odstavci, pokud to není nezbytné nebo kvùli pøehlednosti nutné.</li>
	</ul>
	<ul type="disc" id="ulDila" runat="server" style="margin-bottom:5px;">Liter:
		<li>Díla patøící do jiné sekce (napø. v básních je povídka) je potøeba pøeøadit do patøièné sekce.</li>
		<li>Díla patøící do jiné kategorie (napø. "o lásce" je v kategorii "Pøíroda") je potøeba pøeøadit do patøièné kategorie.</li>
		<li>Díla z kategorie "nezaøazené" zaøadit do kategorie, pokud to jde.</li>
		<li>Grafickou a stylistickou stránku necháme na autorovi.</li>
		<li>Procházet komentáøe k dílùm a mazat nevhodné pøíspìvky (vulgární, urážlivé, protiprávní).</li>
	</ul>
	<ul type="disc" id="ulTexty" runat="server" style="margin-bottom:5px;">Texty:
		<li>Upøednostòujeme kvalitu pøed kvantitou !!!!</li>
		<li>Nové pøíspìvky edituj od nejstarších k novým.</li>
		<li>Opravovat gramatické chyby, nespisovné, sprosté a hrubé výrazy.</li>
		<li>Nechávej pouze opravdu kvalitní a zajímavé záznamy, ostatní smaž.</li>
		<li>Nepoužívat pøíliš VELKÁ PÍSMENA (pouze na zaèátku vìt nebo jmen). Pokud bude celý pøíspìvek psán velkým písmem, pak ho pøepiš na malá nebo smaž.</li>
		<li>Rozhovory, básnièky apod. odsazovat na nové øádky (aby to celé nebyl jeden øádek).</li>
		<li>Doplnit diakritiku (háèky, èárky) "cestina" -&gt; "èeština". Pokud bude celý pøíspìvek psán bez diakritiky, pak ho pøepiš s interpunkcí nebo smaž.</li>
		<li>Nìkolik znakù za sebou zkracujeme maximálnì na 3 znaky (........ » ...), (!!!! » !).</li>
		<li>Nenechávat prázdné øádky mezi vìtami èi odstavci, pokud to není nezbytné nebo kvùli pøehlednosti nutné.</li>
		<asp:placeholder id="phPrani" runat="server">
			<li><u>Pøání:</u> Žádná jména (pøeji ti Petøe), pouze obecnì.</li>
			<li><u>Pøání:</u> Pokud nìco konèí "ti pøeje Martin", tak to opravit na "Ti pøeje ..."</li>
		</asp:placeholder>
	</ul>
	<ul type="disc" id="ulSeznamka" runat="server" style="margin-bottom:5px;">Seznamka:
		<li>Vymaž seznamovací inzerát, kde je uveden e-mail nebo telefonní èíslo, nebo to je komerèní nabídka, nabídka odmìn za služby, nabídka a potkávka prostituce.</li>
	</ul>
	
</asp:Content>