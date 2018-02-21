<%@ Page Language="VB" Title="Seznam textù" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="TxtListLong.aspx.vb" Inherits="_TxtListLong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">
		<asp:PlaceHolder ID="phSortBy" runat="server" />
		<div id="divTxtList" runat="server" style="margin-top: 6px;">#Zobrazí data pøipravená v kódu#</div>
		<div id="divPagesBox" runat="server" class="ListPagesBox">#1111111#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>