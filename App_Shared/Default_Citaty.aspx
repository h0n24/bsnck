<%@ Page Language="VB" Title="Kategorie" EnableViewState="False" MasterPageFile="~/App_Shared/MasterPage_Citaty.Master" AutoEventWireup="False" CodeFile="Default_Citaty.aspx.vb" Inherits="_Default_Citaty"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p>V�tejte na �esk� str�nce obsahuj�c� Cit�ty zn�m�ch osobnost�.
	</p>

	<asp:PlaceHolder ID="phMain" runat="server">
		<div id="divViewKatHtml" runat="server" style="margin-top: 6px;">#Zobraz� data p�ipraven� v k�du#</div>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>