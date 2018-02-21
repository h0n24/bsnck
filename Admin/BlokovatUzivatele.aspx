<%@ Page Title="Blokovaní uživatelé" Language="VB" MasterPageFile="~/App_Shared/Admin.master" AutoEventWireup="false" CodeFile="BlokovatUzivatele.aspx.vb" Inherits="Admin_BlokovatUzivatele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form runat="server" id="form1">
		<asp:placeholder ID="phItem" runat="server">
			<h2><asp:Literal ID="literalTitulek" runat="server" Text="Pøidej nového uživatele do blokovaných." /></h2>
			<div style="font-size:90%">èíslo ID uživatele:</div>
			<div><asp:TextBox ID="tbUzivatel" runat="server" /></div>
			<div style="font-size:90%">Dùvody:</div>
			<div><asp:TextBox ID="tbDovod" runat="server" Rows="3" TextMode="MultiLine" width="99%" Columns="60" /></div>
			<div style="font-size:90%">Zpráva pro uživatele:</div>
			<div><asp:TextBox ID="tbZprava" runat="server" Rows="3" TextMode="MultiLine" width="99%" Columns="60" /></div>
			<div style="margin-bottom:6px"><asp:Button ID="btnAdd" runat="server" Text="» Uložit «" /><asp:CheckBox ID="chbEdit" runat="server" Visible="false" Checked="false" /></div>
		</asp:placeholder>
		
		<asp:PlaceHolder ID="phReport" Runat="server" />
		
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
			<Columns>
				<asp:TemplateField>
					<ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" Text='Edit' NavigateUrl='<%#Eval("Uzivatel", "?akce=edit&amp;id={0}")%>' /></ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate><asp:HyperLink ID="HyperLink2" runat="server" Text='Smazat' NavigateUrl='<%#Eval("Uzivatel", "?akce=delete&amp;id={0}")%>' /></ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField HeaderText="Datum" DataField="Datum" DataFormatString="{0:dd.MM.yyyy HH:mm}"   />
				<asp:BoundField HeaderText="Nick" DataField="UserNick" ItemStyle-ForeColor="Gray" />
				<asp:BoundField HeaderText="Uživatel" DataField="Uzivatel" ApplyFormatInEditMode="true" />
				<asp:BoundField HeaderText="Dùvod blokace" DataField="Duvod" />
				<asp:BoundField HeaderText="Zpráva zobrazená uživateli" DataField="ZpravaUzivateli" />
			</Columns>
		</asp:GridView>
	</form>

</asp:Content>