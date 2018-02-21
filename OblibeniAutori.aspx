<%@ Page Title="Obl�ben� auto�i" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="OblibeniAutori.aspx.vb" Inherits="_OblibeniAutori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<asp:Panel ID="pnlList" Runat="server" Visible="False">
		<asp:Repeater ID="repeaterList" Runat="server">
			<ItemTemplate>
				<a href='/OblibeniAutori.aspx?akce=delete&amp;id=<%#Container.DataItem("OblibID")%>' onclick="if(!confirm('Smazat z obl�ben�ch autor�?')) return false;" title="Smazat">x</a>|<a href='/Autori/<%#Container.DataItem("OblibAutor")%>-info.aspx' title="Bli��� informace">info</a>| 
				<a href='/Autori/<%#Container.DataItem("OblibAutor")%>-dila.aspx' title="P�ehled autorov�ch p��sp�vk�"><%#Server.HtmlEncode(Container.DataItem("UserNick"))%></a>
				<span style="font-size:80%">(od <%#CDate(Container.DataItem("OblibDatum")).ToString("d.M.yyyy")%>)</span><br/>
			</ItemTemplate>
		</asp:Repeater>
	</asp:Panel>
	<asp:Panel ID="pnlAkce" Runat="server" CssClass="box-menu" style="margin-top:10px; width:320px;">
		<div>�<a href="/Autori/oblibenci-dila.aspx">D�la obl�ben�ch autor�</a></div>
		<div style="margin-top:4px">�<a href="Autori_Seznam.aspx">Naj�t a p�idat nov�ho autora do obl�ben�ch</a></div>
	</asp:Panel>

</asp:Content>