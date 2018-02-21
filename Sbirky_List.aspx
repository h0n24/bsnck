<%@ Page Language="VB" Title="Sbírky" EnableViewState="true" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="false" CodeFile="Sbirky_List.aspx.vb" Inherits="Sbirky_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phMain" runat="server">
		<form id="form1" runat="server">
			<div style="float:right">Řadit podle <asp:ListBox id="lbSort" runat="server" Rows="1" AutoPostBack="true" EnableViewState="true" /></div>
			<div style="margin-bottom:3px;"><asp:CheckBox ID="chbNedokoncene" runat="server" AutoPostBack="true" EnableViewState="true" /> Zobrazovat i nedokončené</div>
		</form>
		<div style="margin-bottom:3px; font-style:italic"><asp:Literal ID="litZaznamu" runat="server"></asp:Literal></div>
		<asp:Repeater ID="rptSbirky" runat="server" OnItemCreated="rptSbirky_ItemCreated" EnableViewState="false">
			<ItemTemplate><asp:PlaceHolder id="phReklamaInside" runat="server" />
				<div class="box2" style="clear:both;">
					<div>
						<asp:PlaceHolder ID="phEdit" runat="server"><a href='/Sbirky_New.aspx?akce=edit&amp;sbirka=<%#Container.DataItem("ID")%>' class="pamatovatklik" style="float:right; margin-right:3px;">[EDIT]</a></asp:PlaceHolder>
						<a href='/Sbirky_Show.aspx?sbirka=<%#Container.DataItem("ID")%>' style="font-weight:bold"><%#Server.HtmlEncode(Container.DataItem("Titulek"))%></a>&nbsp;&nbsp;(<a href='/Sbirky_List.aspx?kat=<%#Container.DataItem("Kat")%>'><%#Server.HtmlEncode(Container.DataItem("KatNazev"))%></a>)
					</div>
					<div style="font-size:85%"><%#Server.HtmlEncode(ZkratitProlog(Container.DataItem("Prolog")))%></div>
					<div class="title">
						<a href='/Autori/<%#Container.DataItem("Autor")%>-info.aspx'><%#Server.HtmlEncode(Container.DataItem("UserNick"))%></a>,
						<span style="font-weight:lighter; color:Gray"><%#CType(Container.DataItem("Datum"), Date).ToString("d.M.yyyy")%> - <%#ZobrazitDokonceno(Container.DataItem("Dokonceno"))%></span> | 
						<span>Děl: <%#Container.DataItem("Kapitol")%></span> | <span>Tipy: <%#Val.ToInt(Container.DataItem("Tipy"))%></span>
					</div>
				</div>
			</ItemTemplate>
		</asp:Repeater>

		<div id="divPagesBox" runat="server" class="ListPagesBox"></div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>