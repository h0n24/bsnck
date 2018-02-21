<%@ Page Language="VB" Title="Vzkazy" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Vzkazy.aspx.vb" Inherits="_Vzkazy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<div style="margin-bottom:5px">» <a href="Vzkazy_Add.aspx">Napiš nový vzkaz</a></div>
		<asp:Repeater ID="repeaterList" Runat="server">
			<ItemTemplate>
			<div style="font-size:80%; border-bottom: gray 1px solid; <%#IIf(IsDBNull(Container.DataItem("VzkazPrecteno")),"font-weight:bold;","")%>">
				<a href='Vzkazy.aspx?akce=delete&amp;id=<%#Eval("VzkazID")%>' onclick="if(!confirm('Smazat vzkaz?')) return false;" title="Smazat">x</a>|<span><%#CDate(Eval("VzkazDatum")).ToString("d.M.yy HH:mm")%></span> |
				<span><%#Server.HtmlEncode(Eval("OdNick") & "")%></span> |
				<a href='Vzkaz_View.aspx?id=<%#Eval("VzkazID")%>'><%#Server.HtmlEncode(Eval("VzkazPredmet"))%></a>
			</div>
			</ItemTemplate>
		</asp:Repeater>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>