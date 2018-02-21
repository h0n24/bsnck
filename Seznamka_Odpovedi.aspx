<%@ Page Language="VB" Title="Odpovìdi na inzerát" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Seznamka_Odpovedi.aspx.vb" Inherits="_Seznamka_Odpovedi"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<asp:Repeater ID="rptSeznamkaOdpovedi" Runat="server">
		<ItemTemplate>
			<p><b>Datum:</b> <%#Container.DataItem("Datum")%></p>
			<p><b>Jméno:</b> <%#Container.DataItem("Jmeno")%></p>
			<p><b>Email:</b> <%#Container.DataItem("Email")%></p>
			<p><b>Text:</b> <%#Container.DataItem("Txt")%></p>
		</ItemTemplate>
		<SeparatorTemplate><hr/></SeparatorTemplate>
	</asp:Repeater>

</asp:Content>