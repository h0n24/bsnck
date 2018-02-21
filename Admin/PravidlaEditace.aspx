<%@ Page Language="VB" Title="Pravidla editace" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="PravidlaEditace.aspx.vb" Inherits="Admin_PravidlaEditace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="margin-bottom:8px;"><b>Pravidla pro Editaci z�znam�</b></p>
	<p><b>D�le�it�:</b></p>
	<ul type="circle" style="margin-bottom:5px;">
		<li>V�echny tv� �kony jsou zaznamen�v�ny a z�lohov�ny.</li>
		<li>Kdy� se n�komu povede omylem smazat nebo zm�nit dobr� z�znam, m��u ho po upozorn�n� z t�to z�lohy obnovit.</li>
		<li>Pokud by n�kdo cht�l �mysln� mazat �i jinak po�kozovat z�znamy se z�m�rem po�kodit datab�zi, vystavuje se mo�nosti trestn�ho st�h�n�.</li>
		<li>Kdy� nezn� pravopis (nebo i jen tak pro kontrolu), zkop�ruj text (Ctrl+C) a vlo� do Wordu (Ctrl+V), kter� m� kontrolu pravopisu.</li>
		<li>Objev�-li dal�� problematick� bod, napi� mi.</li>
		<li>Kdy� si nev� rady, zeptej se m�.</li>
	</ul>
	<p><b>Editace:</b></p>
	<ul type="square" style="margin-bottom:5px;">
		<li>Mazat vulg�rn�, ur�ej�c�, protipr�vn� texty.</li>
		<li>Mazat a kontrolovat duplicitu (n�kter� p��sp�vky jsem �etl 5x).</li>
		<li>Za�azovat z�znamy z neza�azen�ch do pat�i�n�ch kategori� (pokud chce� p�idat novou kategorii, napi� a domluv�me se).</li>
		<li>Nepou��vat p��li� VELK� P�SMENA (pouze na za��tku v�t nebo jmen). Pokud bude cel� p��sp�vek ps�n velk�m p�smem, pak ho p�epi� na mal� nebo sma�.</li>
		<li>Smazat nadbyte�n� texty �i reklamnu na jin� str�nky (odkazy na www str�nky, emaily aj., osobn� vzkazy).</li>
		<li>Za ��rkou, te�kou a jinou interpunkc� nech�vat mezeru (kv�li p�ehlednosti a jednotnosti).</li>
		<li>Nenech�vat pr�zdn� ��dky mezi v�tami �i odstavci, pokud to nen� nezbytn� nebo kv�li p�ehlednosti nutn�.</li>
	</ul>
	<ul type="disc" id="ulDila" runat="server" style="margin-bottom:5px;">Liter:
		<li>D�la pat��c� do jin� sekce (nap�. v b�sn�ch je pov�dka) je pot�eba p�e�adit do pat�i�n� sekce.</li>
		<li>D�la pat��c� do jin� kategorie (nap�. "o l�sce" je v kategorii "P��roda") je pot�eba p�e�adit do pat�i�n� kategorie.</li>
		<li>D�la z kategorie "neza�azen�" za�adit do kategorie, pokud to jde.</li>
		<li>Grafickou a stylistickou str�nku nech�me na autorovi.</li>
		<li>Proch�zet koment��e k d�l�m a mazat nevhodn� p��sp�vky (vulg�rn�, ur�liv�, protipr�vn�).</li>
	</ul>
	<ul type="disc" id="ulTexty" runat="server" style="margin-bottom:5px;">Texty:
		<li>Up�ednost�ujeme kvalitu p�ed kvantitou !!!!</li>
		<li>Nov� p��sp�vky edituj od nejstar��ch k nov�m.</li>
		<li>Opravovat gramatick� chyby, nespisovn�, sprost� a hrub� v�razy.</li>
		<li>Nech�vej pouze opravdu kvalitn� a zaj�mav� z�znamy, ostatn� sma�.</li>
		<li>Nepou��vat p��li� VELK� P�SMENA (pouze na za��tku v�t nebo jmen). Pokud bude cel� p��sp�vek ps�n velk�m p�smem, pak ho p�epi� na mal� nebo sma�.</li>
		<li>Rozhovory, b�sni�ky apod. odsazovat na nov� ��dky (aby to cel� nebyl jeden ��dek).</li>
		<li>Doplnit diakritiku (h��ky, ��rky) "cestina" -&gt; "�e�tina". Pokud bude cel� p��sp�vek ps�n bez diakritiky, pak ho p�epi� s interpunkc� nebo sma�.</li>
		<li>N�kolik znak� za sebou zkracujeme maxim�ln� na 3 znaky (........ � ...), (!!!! � !).</li>
		<li>Nenech�vat pr�zdn� ��dky mezi v�tami �i odstavci, pokud to nen� nezbytn� nebo kv�li p�ehlednosti nutn�.</li>
		<asp:placeholder id="phPrani" runat="server">
			<li><u>P��n�:</u> ��dn� jm�na (p�eji ti Pet�e), pouze obecn�.</li>
			<li><u>P��n�:</u> Pokud n�co kon�� "ti p�eje Martin", tak to opravit na "Ti p�eje ..."</li>
		</asp:placeholder>
	</ul>
	<ul type="disc" id="ulSeznamka" runat="server" style="margin-bottom:5px;">Seznamka:
		<li>Vyma� seznamovac� inzer�t, kde je uveden e-mail nebo telefonn� ��slo, nebo to je komer�n� nab�dka, nab�dka odm�n za slu�by, nab�dka a potk�vka prostituce.</li>
	</ul>
	
</asp:Content>