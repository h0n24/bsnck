<%@ Page Language="VB" AutoEventWireup="false" EnableViewState="false" MasterPageFile="~/App_Shared/Default.Master" CodeFile="Sbirky_Show.aspx.vb" Inherits="Sbirky_Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<asp:PlaceHolder ID="phMain" runat="server">
		<h4 id="txtTitulek" runat="server" style="font-size:1.2em; margin-bottom:4px; text-decoration:underline">Titulek</h4>
		<div style="margin-bottom:2px;">
			<asp:HyperLink ID="hlAutor" runat="server" NavigateUrl="/Autori/{0}-info.aspx" />
		</div>
		<div style="margin-bottom:2px; padding-bottom:2px; border-bottom:#808080 1px dashed">
			<asp:HyperLink ID="hlKategorie" runat="server" NavigateUrl="/Sbirky_List.aspx?kat={0}" style="float:right; font-size:85%" />
			<asp:Label ID="lblDatum" runat="server" CssClass="text-komentar" />
		</div>
		<div style="margin-bottom:2px; padding-bottom:2px; border-bottom:#808080 1px dashed">
			<span style="font-weight:bold">Prolog: </span><span id="txtProlog" runat="server"></span>
		</div>
		
		<div class="box-menu" style="margin-top:8px; width:210px; padding:2px; float:right">
			<div><asp:HyperLink ID="hlKomentare" runat="server" NavigateUrl="/Komentare_View.aspx?sekce=Sbirky&amp;id={0}" Text="Komentáře">
					<img src="/gfx/ico/comment.gif" style="float:left; margin-right:2px;" alt="ikonka" />
					<span>Komentáře</span>
				</asp:HyperLink>
				<span>(<asp:Literal ID="litKomentare" runat="server" />)</span>
			</div>
			<div>Tipy (<asp:Literal ID="litTipy" runat="server" />)... dát
				<asp:HyperLink ID="hlTip" runat="server" NavigateUrl="/Tip.aspx?akce=tipuj&val=1&sekce=Sbirky&id={0}" Text="Tip" ToolTip="Tip doporučí text ostatním" />/<asp:HyperLink ID="hlTip2" runat="server" NavigateUrl="/Tip.aspx?akce=tipuj&val=2&sekce=Sbirky&id={0}" Text="SuperTip" ToolTip="SuperTip doporučí text ostatním silněji" />
			</div>
			<asp:Panel ID="pnlTipujici" runat="server" style="margin-top:3px; font-size:85%; font-style:italic;">
				Poslední tipující:
				<asp:Label ID="lblTipujici" runat="server" />
			</asp:Panel>
		</div>

		<div>
			<span style="font-weight:bold;">Obsah sbírky:</span><br />
			<asp:Repeater ID="rptObsah" runat="server">
				<ItemTemplate>
					<div>
						<asp:HyperLink ID="hlDilo" runat="server" NavigateUrl='<%#String.Format("/{0}/{1}-view.aspx",Eval("Sekce"),Eval("ObsahDilo"))%>' Text='<%#Server.HtmlEncode(OdkazText(Eval("Titulek")))%>'/>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>

	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" runat="server" />

</asp:Content>