<%@ Page Language="VB" Title="Export / Import databáze" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="DB_Csv.aspx.vb" Inherits="Admin_DB_Csv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="font-weight:bold; margin-bottom:6px;">Export / Import databáze</p>
	<asp:PlaceHolder ID="phReport" Runat="server" />

	<asp:Panel ID="pnlMenu" Runat="server" Visible="False">
		<form id="Form1" runat="server">
		<div style="float:right; padding:4px; background-color:#FFAAAA; border: #888888 1px solid; ">
			<asp:CheckBox ID="cbFirstLineColumnsNames" Runat="server" /> 1. øádek názevy sloupcù (pøeskoèit)<br/>
			<span style="font-weight:bold">Import:</span><br/>
			<asp:CheckBox ID="cbTruncateTable" Runat="server" /> Truncate Table (vymazat)<br/>
			<asp:CheckBox ID="cbSkipIdentityColumns" Runat="server" /> Vynechat sloupce Identity (=>nové autoID)<br/>
			<asp:CheckBox ID="cbShowSQL" Runat="server" /> Zobrazit SQL pøíkazy<br/>
			<asp:Button ID="btSubmit" Runat="server" Text="» Uložit «" Style="margin-top:8px" /> <asp:Label ID="lblSettingsReport" Runat="server" Font-Bold="True"></asp:Label>
		</div>
		</form>
		<asp:Repeater ID="rptMenu" Runat="server">
			<ItemTemplate>
				<a href='DB_Csv.aspx?akce=export&table=<%#Container.DataItem("table")%>'>Export</a> / <a onclick="if(!confirm('Opravdu importovat ?')) return false;" href='DB_Csv.aspx?akce=import&table=<%#Container.DataItem("table")%>'>Import</a>
				<span style="font-weight:bold"> - <%#Container.DataItem("table")%></span> <span style="font-weight:lighter; color:Gray"> &nbsp;&nbsp;(<%#FileChangeDate(Container.DataItem("table"))%>)</span><br/>
			</ItemTemplate>
		</asp:Repeater>
	</asp:Panel>
	
	<asp:Panel ID="pnlImport" Runat="server" Visible="False">
		<pre><%=SqlInserts%></pre>
	</asp:Panel>

</asp:Content>
