<%@ Page Title="Geocache" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="Geocache.aspx.vb" Inherits="Geocache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<asp:PlaceHolder ID="phGeocache" runat="server">
	<div>
		<dl class="Geocache"><dt><span class="form-label-required">Geocache Code:</span></dt>
			<dd><asp:Label ID="lblCode" runat="server" /> (from <asp:HyperLink ID="hlGeocachingCom" runat="server" Text="Geocaching.com" NavigateUrl="http://www.geocaching.com/seek/cache_details.aspx?wp={0}" />)</dd>
			<dt><span class="form-label-required">Geocache Name:</span></dt>
			<dd><asp:Label ID="lblName" runat="server" /></dd>
			<dt><span class="form-label-required">Type of Geocache:</span></dt>
			<dd><asp:Label ID="lblType" runat="server" /></dd>
			<dt><span class="form-label-required">Coordinates:</span></dt>
			<dd><asp:Label ID="lblCoordinates" runat="server" /></dd>
		</dl>
	
	<div style="clear:left; padding-top:6px; padding-bottom:3px; border-bottom:solid 1px #999999">List of Final Coordinates:
		<asp:HyperLink ID="hlDecode" runat="server" Text="(Show hidden)" NavigateUrl="/OrderGeocache.aspx?id={0}" style="margin-left:8px" />
	</div>
		<asp:Repeater ID="rptFinals" runat="server">
			<ItemTemplate>
				<div style="padding-bottom:3px; padding-top:3px; border-bottom:solid 1px #999999">
					<div>Coordinates: <%#ShowFinalCoordinates(Eval("FinalLat"), Eval("FinalLon"))%></div>
					<div>Published: <%#CDate(Eval("FirstFinalDate")).ToShortDateString%>, Verified:<%#Eval("Count") - 1%>x, Last <%#CDate(Eval("LastFinalDate")).ToShortDateString%></div>
					<div runat="server" visible="false">Log: 0x found, 0x not found</div>
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	
	</asp:PlaceHolder>
	
</asp:Content>