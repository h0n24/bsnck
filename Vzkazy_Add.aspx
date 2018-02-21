<%@ Page Title="Napiš vzkaz" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" Inherits="_Vzkazy_Add" CodeFile="Vzkazy_Add.aspx.vb"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phMain" runat="server">

		<form runat="server" id="Form1">
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<div id="divNick" runat="server" class="text-nadpis" style="margin-bottom:4px;">Komu:<span class="text-komentar"> (Nick uživatele)</span><br/>
				<asp:textbox id="tbNick" runat="server" cssclass="input-text" Columns="40" />
			</div>
			<div class="text-nadpis">Pøedmìt:<span class="text-komentar"> (struènì naznaè pøedmìt zprávy, max.100 znakù)</span></div>
			<div style="margin-bottom: 4px;"><asp:textbox id="tbPredmet" runat="server" cssclass="input-text" Columns="40" Width="99%" /></div>
			<div class="text-nadpis">Text:</div>
			<div style="margin-bottom:6px;"><asp:TextBox id="tbTxt" cssclass="input-text" runat="server" Columns="54" Width="99%" Rows="10" TextMode="MultiLine" /></div>
			<asp:Button id="btSubmit" text="» Poslat «" runat="server" cssclass="submit"/>
		</form>

	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>