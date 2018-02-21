<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="TxtViewLong.aspx.vb" Inherits="_TxtViewLong"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phMain" runat="server">
		<h2 id="txtTitulek" runat="server" style="font-size:120%; margin-bottom:6px; text-decoration:underline">Titulek</h2>
		<asp:panel id="pnlAnotace" visible="False" runat="server">
			<div style="font-size:90%; margin-bottom:4px">
				<asp:HyperLink ID="hlAutor" runat="server" style="float:left;" NavigateUrl="/Autori/{0}-info.aspx" />
				<asp:HyperLink ID="hlSekce" runat="server" style="float:right;" NavigateUrl="/{0}/kat-{1}.aspx" />
			</div>
			<p style="clear:left; padding-bottom:4px; border-bottom: #808080 1px dashed"><b><i>Anotace: </i></b><span id="txtAnotace" runat="server">text anotace</span></p>
		</asp:panel>
		<div id="txtText" runat="server" style="clear:left; margin-bottom:8px">Text jednoho pøíspìvku</div>
		<div id="txtPoslal" runat="server" style="font-size:85%;">Publikoval: Jméno + Datum</div>
		<div class="box-menu" style="margin-top:8px; float:left; width:240px; padding:2px;">
			<asp:PlaceHolder id="phHodnoceniTxtDila" runat="server"> 
				<div>Pøeèteno&nbsp;<asp:Label ID="lblPrecteno" runat="server" /></div>
				<div>Tipy (<asp:Literal ID="litTipy" runat="server"/>) ...  dát <asp:HyperLink ID="hlTip" runat="server" Text="Tip" NavigateUrl="/Tip.aspx?akce=tipuj&amp;val=1&amp;sekce={0}&amp;id={1}" ToolTip="Tip doporuèí text ostatním" />/<asp:HyperLink ID="hlTip2" runat="server" Text="SuperTip" NavigateUrl="/Tip.aspx?akce=tipuj&amp;val=2&amp;sekce={0}&amp;id={1}" ToolTip="SuperTip doporuèí text ostatním silnìji" /></div>
				<asp:Panel ID="pnlTipujici" runat="server" style="margin-top:3px; font-size:85%; font-style:italic;">
					Poslední tipující: <asp:Label id="lblTipujici" runat="server" />
				</asp:Panel>
			</asp:PlaceHolder>

			<asp:PlaceHolder id="phHodnoceniTxtLong" runat="server"> 
				<p id="txtHodnoceni" runat="server" style="font-size: 0.9em; margin-bottom: 4px;">Hodnocení: </p>
				<div id="divHodnoceni" runat="server">
					<div style="padding-left: 10px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="0" /><input type="image" name="Submit" src="/gfx/hodnoceni/hodnoceni0.gif" alt="0 % .. Katastrofa" /></form>
					</div>
					<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="1" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni1.gif" alt="20% .. Špatné" /></form>
					</div>
					<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="2" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni2.gif" alt="40% .. Ucházející" /></form>
					</div>
					<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="3" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni3.gif" alt="60% .. Dobré" /></form>
					</div>
					<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="4" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni4.gif" alt="80% Výborné" /></form>
					</div>
					<div style="padding-left: 3px; float: left;"><form method="post" action="<%#HodnoceniAction%>">
						<input name="h" type="hidden" value="5" /><input name="Submit" type="image" src="/gfx/hodnoceni/hodnoceni5.gif" alt="100 % .. Nejlepší" /></form>
					</div>
					&nbsp;
					<div style="clear:left; margin-top:3px; font-size: 85%; padding-bottom:3px;">Hlasujte kliknutím na obrázek.<br/>Na kvalitu máme vlastní vzorec!</div>
				</div>
			</asp:PlaceHolder>
		</div>
		<div class="box-menu" style="margin-top:8px; font-size:0.9em; float:right; width:170px;">
			<div style="clear:left;"><a id="aKomentare" runat="server" href="/{0}/{1}-koment.aspx"><img src="/gfx/ico/comment.gif" style="float: left; margin-right: 2px;" alt="ikonka" />Komentáøe</a>&nbsp;<asp:Label ID="lblKomentare" runat="server" /></div>
			<div style="clear:left;">
				<a id="aDoporucit" runat="server" href="/SendEmail.aspx?sekce={0}&amp;id={1}"><img src="/gfx/ico/email_doporucit.gif" style="float: left; margin-right: 2px;" alt="ikonka" />Doporuèit</a>
				&nbsp;<asp:Label ID="lblOdeslano" runat="server" />
			</div>
			<div style="clear:left;">
				<a id="aPrint" runat="server" href="/{0}/{1}-print.aspx"><img src="/gfx/ico/print.gif" style="float: left; margin-right: 2px;" alt="ikonka" />Tisk</a>
			</div>
			<asp:Panel ID="pnlAdmin" runat="server" style="clear:left;">
				<a id="aAdmin" runat="server" href="/Admin/Editace.aspx?sekce={0}&amp;id={1}&amp;nav=False">» Admin</a>
			</asp:Panel>
		</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>