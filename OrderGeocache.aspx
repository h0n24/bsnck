<%@ Page Title="Order Geocache" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="OrderGeocache.aspx.vb" Inherits="OrderGeocache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<form id="Form1" runat="server">
		<p style="margin-bottom:8px;">You are going to buy the cache:<br />
			<asp:Label ID="lblCacheCode" runat="server" style="font-weight:bold" /><br />
			<asp:Label ID="lblCacheName" runat="server" style="font-weight:bold" />
		</p>
		<p style="margin-bottom:8px;">Price: <asp:Label ID="lblPrice" runat="server" /> credits.</p>
		
		<asp:Button id="btSubmit" text="» Buy «" runat="server" cssclass="submit" />
	</form>
	
</asp:Content>