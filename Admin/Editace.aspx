<%@ Page Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="False" CodeFile="Editace.aspx.vb" Inherits="Admin_Editace" %>
<%@ Reference Page="~/Add.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<asp:Panel id="pnlReport" runat="server" Visible="False" style="text-align:center;">
			<asp:Label ID="lblReport" runat="server" style="background-color:#55BBff; padding:0px 10px 0px 10px; text-align:center; border:1px dashed #000000;" />
		</asp:Panel>
		<asp:Panel id="pnlAdminEditace" runat="server">
				<input type="hidden" id="inpID" runat="server" value="<%=iID%>" />
				<input type="hidden" id="inpReferer" runat="server" />
				<div style="float:right; border-left:1px solid #888888; padding-left:6px; margin-left:6px; margin-right:4px;">
					Posláno <asp:Literal ID="litOdeslano" runat="server" />x<br/>
					<asp:Panel ID="pnlHodnoceni" runat="server" Visible="false">Hodnocení: <asp:label ID="lblHodnoceni" runat="server" /></asp:Panel>
					<asp:Panel id="pnlKomentare" runat="server" Width="100" Visible="false"><asp:HyperLink ID="hlKoment" runat="server" Text="Komentáøe" NavigateUrl="Komentare.aspx?sekce={0}&amp;dbid={1}" />:<asp:Label ID="lblKomentare" runat="server" /> (<asp:HyperLink ID="hlKomentNew" runat="server" Text="+" NavigateUrl="/Komentare_Add.aspx?sekce={0}&amp;dbid={1}" />)</asp:Panel>
				</div>
				<div style="float:right;" align="right">
					<span>Sekce: <asp:Label ID="lblSekce" runat="server" /></span> <br/>
					<asp:ListBox ID="lbSekce" runat="server" Rows="1" /> <asp:Button id="btnSekce" Runat="server" Text="Zmìnit sekci" CssClass="ButtonSubmit" />
				</div>
				<div style="margin-bottom:3px;"><asp:Label id="lblPoslal" runat="server" /> | <asp:Label id="lblDatum" runat="server" style="color:#888888"></asp:Label> | ID=<%=iID%></div>
				<div style="margin-bottom:3px;">Kategorie: <select id="lbKat" runat="server"></select> <asp:ListBox ID="lbCitatyAutor" runat="server" style="margin-left:15px;" Rows="1" /></div>
				<asp:Panel ID="pnlTitulek" runat="server" style="margin-bottom:3px;">Titulek: <asp:TextBox ID="tbTitulek" runat="server" Columns="40" /></asp:Panel>
				<asp:Panel ID="pnlAnotace" runat="server" style="margin-bottom:3px;"><asp:TextBox ID="tbAnotace" runat="server" Rows="2" Columns="80" style="width:99%;" TextMode="MultiLine" /></asp:Panel>
				<div style="margin-bottom:3px;"><asp:TextBox ID="tbTxt" runat="server" rows="15" Columns="80" TextMode="MultiLine" style="width:99%" /></div>
				<div>
					<div style="float:left;"><asp:Button id="btAdminEditaceSave" Runat="server" Text="» Uložit «" Font-Bold="True" CssClass="ButtonSubmit" /></div>
					<div style="float:right;">
						<span style="margin-left:30px;">Dùvod:</span><input id="inpAdminEditaceDelete" runat="server" size="30"/>
						<asp:Button id="btnDelete" Runat="server" Text="!! SMAZAT !!" CssClass="ButtonDelete" OnClientClick="if(!confirm('Smazat dílo?')) return false;" />
					</div>&nbsp;
				</div>
				<asp:Panel ID="pnlNavigace" runat="server" style="clear:left; background-color:#A5B69E; border:1px solid #000000; padding:2px; margin-top:8px;">
					<div style="float:left;">
						<a id="aGoFirst" runat="server" title="První záznam" class="ButtonSubmit" style="margin-left:5px;">První</a>
						<a id="aGoPrevious" runat="server" title="Pøedešlý záznam" class="ButtonSubmit" style="margin-left:5px;">&lt;&lt;&lt;</a>
						<span style="margin-left:5px;">(<asp:literal ID="litZaznamCislo" runat="server" /> / <asp:literal ID="litZaznamuCelkem" runat="server" />)</span>
						<a id="aGoNext" runat="server" title="Další záznam" class="ButtonSubmit" style="margin-left:5px;">&gt;&gt;&gt;</a>
						<a id="aGoLast" runat="server" title="Poslední záznam" class="ButtonSubmit" style="margin-left:5px; margin-right:30px;">Poslední</a>
						èíslo ID:<asp:TextBox ID="tbGoTo" runat="server" Columns="7" style="text-align:center;" /><asp:button id="btnGoTo" runat="server" text="Pøejdi" cssclass="ButtonSubmit" />
					</div>
					<div align="right">
						<span>&nbsp;</span>
						<select id="lbFilter" runat="server"></select>&nbsp;
						<asp:Button id="btnFilter" Runat="server" Text="Filtrovat" CssClass="ButtonSubmit" />
					</div>
				</asp:Panel>
			<table border="0" cellpadding="0" cellspacing="0" bgcolor="#EEEEEE" style="border: 1px solid #888888; margin-top:10px;">
				<asp:repeater id="rptAdminEditaceEditHistory" runat="server">
					<itemtemplate>
						<tr><td style="padding-right:3px;"><%#CDate(Container.DataItem("HistoryDatum")).ToString("d.M. HH:mm")%></td>
							<td style="padding-left:3px; padding-right:3px; background-color:#DDDDDD"><%#Server.HtmlEncode(Container.DataItem("HistoryJmeno"))%></td><td style="padding-left:3px;"><%#Server.HtmlEncode(Container.DataItem("HistoryTxt"))%></td>
						</tr>
					</itemtemplate>
				</asp:repeater>
			</table>
			
		</asp:Panel>
	</form>
	
</asp:Content>