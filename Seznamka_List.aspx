<%@ Page Language="VB" Title="Seznamka - inzer�ty" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Seznamka_List.aspx.vb" Inherits="_Seznamka_List"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phMain" runat="server">
		<div id="divSeznamkaListHtml" runat="server" style="margin-top: 6px;">#Zobraz� data p�ipraven� v k�du#</div>
		<div id="divSeznamkaListPagesBox" runat="server" class="ListPagesBox">#1111111#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>