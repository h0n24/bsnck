<%@ Page Title="Sp��zn�n� weby" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Empty.Master" AutoEventWireup="False" CodeFile="Odkazy.aspx.vb" Inherits="_Odkazy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<p><%=Request.Url.Host%>, <%=Now%></p>

	<div style="margin-top:20px;">
		<asp:Repeater ID="rptOdkazy1" Runat="server">
			<ItemTemplate><%#Container.DataItem%></ItemTemplate>
		</asp:Repeater>
	</div>

	<asp:Panel ID="pnlPrejCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Elektronick� Pohlednice, v�no�n� pohlednice, e pohlednice, animovan� pohlednice, internetov� pohlednice, velikono�n� pohlednice, virtu�ln� pohlednice</h2>
		<h2>p��n�, v�no�n� p��n�, p��n� k narozenin�m, p��n� k sv�tku, novoro�n� p��n�, svatebn� p��n�, sms p��n�, velikono�n� p��n�, elektronick� p��n�, p��n� k v�noc�m, narozeninov� p��n�, p��n� narozeniny, p��n��ka, elektronick� p��n��ka</h2>
		<p>M��ete ��st a pos�lat p��n��ka a pohlednnice.</p>
	</asp:Panel>
	<asp:Panel ID="pnlLiterCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Knihy, Elektronick� knihy, e knihy, Kn�ky a Recenze knih</h2>
		<h2>Sci-fi, Fantasy pov�dky, Poh�dky, �vahy, Fejetony Rom�n Report�e</h2>
		<h2>Milostn� b�sn�, romantick� b�sn�, zamilovan� b�sn�, zamilovan� b�sni�ky, romantick� b�sni�ky, milostn� poezie</h2>
		<p>U n�s si vyberete. M��ete ��st r�zn� ��nry a publikovat vlatn� d�la.</p>
	</asp:Panel>
	<asp:Panel ID="pnlCitaty" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Cit�ty, Aforismy, Motta, My�lenky, P��slov�</h2>
		<h2>Nejhledan�j�� cit�ty slavn�ch: cit�ty o l�sce, o �ivot�, o p��telstv�, o smutku, o smrti, o zklam�n�, o bolesti, o ne��astn� l�sce, smutn�.</h2>
		<p>M��ete ��st a pos�lat cit�ty a dal�� texty k zamy�len�.</p>
	</asp:Panel>
	<asp:Panel ID="pnlBasneCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Milostn� b�sn�, romantick� b�sn�, zamilovan� b�sn�, Ver�e a poezie</h2>
		<p>M��ete ��st mnoho kr�sn�ch b�sn�.</p>
	</asp:Panel>
	<asp:Panel ID="pnlBasnickyCZ" Runat="server" Visible="True" style="margin-top:20px">
		<h2>Zamilovan� b�sni�ky, romantick� b�sni�ky, milostn� poezie</h2>
		<p>Mnoho b�sni�ek je nachyst�no a vy m��ete ��st.</p>
	</asp:Panel>
	<p>Tajn� k�d: <%=FN.Text.RandomString(50 + Int(50 * Rnd))%></p>

</asp:Content>