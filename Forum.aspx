<%@ Page Title="Fórum" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Forum.aspx.vb" Inherits="_Forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:Panel ID="pnlTemata" Runat="server" Visible="False">
		<asp:Repeater ID="rptTemata" Runat="server">
			<ItemTemplate>
				<h5><a href='/Forum.aspx?tema=<%#Container.DataItem("TemaID")%>'><%#Container.DataItem("TemaNazev")%></a></h5>
				<p style="font-size:85%; margin-bottom:1px; padding-bottom:2px; border-bottom: #808080 1px dotted;">&nbsp;<%#Container.DataItem("Pocet")%> pøíspìvkù<%#ShowDatumPosledni(Container.DataItem("PosledniPrispevek"))%> .. <%#Val.ToInt(Container.DataItem("Suma")) - Val.ToInt(Container.DataItem("Pocet"))%> odpovìdí<%#ShowDatumPosledni(Container.DataItem("PosledniOdpoved"))%></p>
			</ItemTemplate>
		</asp:Repeater>
	</asp:Panel>

	<asp:Panel ID="pnlNahled" Runat="server" Visible="False">
		<ul style="margin-bottom:6px;"><li style="text-align:right;"><a href="/Forum_Add.aspx?tema=<%#TemaID%>">Pøidat nový pøíspìvek</a></li></ul>
		<asp:Repeater ID="rptNahled" Runat="server">
			<ItemTemplate>
				<h5 style="font-weight:normal;"><a class="pamatovatklik" href='/Forum.aspx?id=<%#Container.DataItem("ID")%>'><%#Container.DataItem("Predmet")%></a></h5>
				<p style="font-size:80%; margin-bottom:1px; padding-bottom:2px; border-bottom: #808080 1px dotted;">
					<span><%#Container.DataItem("Jmeno")%>, <%#CType(Container.DataItem("Datum"), Date).ToString("d.M.yy HH:mm")%></span>
					<span> (<%#Container.DataItem("Pocet")-1%> odpovìdí<%#ShowDatumPosledni(Container.DataItem("Posledni"))%>)</span></p>
			</ItemTemplate>
		</asp:Repeater>
		<ul style="margin-top:6px;">
			<li id="liShowAll" runat="server" style="float:left;"><a href="?tema=<%#TemaID%>&showall=true">Zobrazit i starší pøíspìvky</a></li>
			<li style="text-align:right;"><a href="/Forum_Add.aspx?tema=<%#TemaID%>">Pøidat nový pøíspìvek</a></li>
		</ul>
	</asp:Panel>

	<asp:Panel ID="pnlPrispevek" Runat="server" Visible="False">
		<div style="text-align:right;">Zobrazit: strom <!-- | <a href="CommentView.aspx?id={$DisID}&amp;View=seq">seznam podle data</a>--> </div>
		<asp:xml id="xmlForum" runat="server" />
	</asp:Panel>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>