<%@ Page Language="VB" Title="Lost Password" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="User_LostPassword.aspx.vb" Inherits="User_LostPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<p style="margin-bottom:1em;">Lost Password can be requested at this web page.</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="margin-bottom:0.5em;" class="text-nadpis">Your email:</p>
		<div style="margin-bottom:0.5em;"><asp:TextBox ID="tbEmail" runat="server" Columns="30" TextMode="SingleLine" /></div>
		<asp:Button id="btSubmit" text="» Submit «" runat="server" cssclass="submit" />
		<asp:TextBox ID="tbReferrer" runat="server" Visible="false" EnableViewState="true" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>