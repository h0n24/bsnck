<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:panel id="pnlPrej" runat="server" visible="False">
		<p style="margin-bottom: 8px;">V�t�me V�s na str�nce nab�zej�c� texty p��n�, pozdravy a elektronick� pohlednice.</p>
		<p style="margin-bottom: 8px;">Jedin� u n�s naleznete nej�ir�� v�b�r z r�zn�ch druh� p��n��ek a pohlednic. P�ej.cz je nejv�t�� server tohoto druhu u n�s, kde m��ete zas�lat textov� p��n��ka na email nebo SMS.</p>
		<p>Ur�it� jste dostali n�kolik p��n�, pohled� nebo dopis� od sv�ch bl�zk�ch, p��buzn�ch, koleg�, kamar�d� a kamar�dek k r�zn�m p��le�itostem. Mysl�te, �e jejich text byl zaj�mav� nebo legra�n�? A my ho v datab�zi nem�me? P�idejte ho.</p>
		<p style="margin-bottom: 8px;">D�kujeme.</p> <a href="https://www.autodoc.cz/">www.Autodoc.Cz</a>
		<p style="font-size: 0.8em;">Stru�n� obsah: v�no�n� p��n�, p��n� k narozenin�m, p��n� k sv�tku, novoro�n� p��n�, svatebn� p��n�, sms p��n�, velikono�n� p��n�, elektronick� pohlednice, internetov� pohlednice, v�no�n� pohlednice, animovan� pohlednice, velikono�n� pohlednice.</p>
	</asp:panel>
	<asp:panel id="pnlCitaty" runat="server" visible="False">
		<p>V�tejte na <b>nejv�t��</b> �esk� str�nce obsahuj�c� Cit�ty, Motta, My�lenky, Aforismy, V�roky, P��slov� a Pranostiky.<br/><br/>
			Nejhledan�j�� cit�ty: cit�ty o l�sce, o �ivot�, o p��telstv�, o smutku, o smrti, o zklam�n�, o bolesti, o ne��astn� l�sce, smutn�.<br/><br/>
			Nejhledan�j�� motta: o �ivot�, �ivotn� motta, motta o l�sce, o p��telstv�, o smutku, o smrti, o zklam�n�.<br/><br/>
			Nejhledan�j�� p��slov� a moudra: �esk� p��slov�, latinsk�, anglick�, japonsk�.<br/><br/>
			Pokud m�te n�kde poznamen�n V� obl�ben� Cit�t, kter� n�m chyb�, po�lete n�m ho a pomozte n�m vytvo�it opravdu kompletn� archiv cit�t�. D�kujeme.
		</p>
		<p style="margin-top: 8px;"><b>Cit�t dne:</b></p>
		<p><!-- Citat Text1 --><%#CitatTxt%><!-- Citat Text2 --></p>
		<p>[<!-- Citat Autor1 --><%#CitatAutor%><!-- Citat Autor2-->]</p>
	</asp:panel>
	<asp:panel id="pnlBasne" runat="server" visible="False">
		V�t� V�s nejnav�t�vovan�j�� amat�rsk� liter�rn� server na �esk�m internetu.<br/>
		U n�s naleznete nej�ir�� v�b�r z v�tvor� amat�rsk�ch spisovatel�, tj. n�v�t�vn�k� na�ich str�nek.<br/><br/>
		Pokud jste napsali n�jak� p�kn� b�sn�, pov�dky, poh�dky, rom�ny, �vahy, fejetony, kter� by mohly zaujmout na�e �ten��e, ur�it� je vlo�te do na�i datab�ze. 
		Sami se pak na z�klad� hlasov�n� �i po�tu posl�n� m��ete p�esv�d�it o kvalit� sv�ho d�la.<br/>
		<br/>
		Pro milovn�ky modr� jsme p�iravili web dom�nu basne.cz<br/>
		Zast�nci b�l� jist� vyu�ij� sp�e basnicky.cz<br/>
		<br/>
		Z obsahu vyb�r�me: zamilovan� b�sni�ky o l�sce, b�sn� o jaru, o p��telstv�, k narozenin�m, k sv�tku, na dobrou noc, na dobr� r�no, smutn�, pro maminku, pro d�ti.<br/><a href="https://www.euautodily.cz/originalni-dily/opel">www.EuAutoDily.cz</a>
	</asp:panel>
	
</asp:Content>
