<%@ Page Language="VB" Title="Premium účet" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Users_Premium.aspx.vb" Inherits="Users_Premium"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:Panel ID="pnlInfo" Runat="server" Visible="false" style="margin-bottom:8px;">
		Váš Premium účet vyprší: <asp:Label ID="lblExpires" runat="server"></asp:Label>
	</asp:Panel>
	
	<div>Chceteli podpořit provoz serveru nebo získat výhody, objednejte si u nás Premium účet.</div>
	<div style="margin-top:8px">Výhody:<br />
		<ul><li>Vložení více než 1 díla za den</li>
			<li>Odstraní reklamní bannery</li>
		</ul>
	</div>	
	<div style="margin-top:8px; margin-bottom:4px">Cena:<br />
		<ul><li>240kč na 12 měsíců</li>
			<li>100kč na 4 měsíce</li>
		</ul>
	</div>
	
	<asp:Panel ID="pnlPlatba" Runat="server" style="margin-top:8px;">
		<div>Kvůli relativně malému provozu plateb je technicky náročné vytvářet automatický platení systém, proto zůstáváme u osvětčeného převodu na účet. Po obrdržení platby provedeme povýšení vašeho účtu na Premium.</div>
		<div style="margin-top:8px">Účet: 1166547019/3030<br />
		Variabilní symbol: <asp:Label ID="lblVS" runat="server"></asp:Label>
		</div>
	</asp:Panel>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>