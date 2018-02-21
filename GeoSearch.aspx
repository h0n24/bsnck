<%@ Page Title="Geocaches" EnableViewState="true" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="true" CodeFile="GeoSearch.aspx.vb" Inherits="GeoSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<form id="Form1" runat="server">
		<p style="margin-bottom:8px;">Search for a geocaches.</p>
		<asp:TextBox ID="tbReferrer" runat="server" Visible="false" EnableViewState="true" />
		<asp:PlaceHolder ID="phErrors" Runat="server" />

<asp:PlaceHolder ID="phFUJJJ" runat="server" Visible="false">
		<div style="margin-bottom:8px;">
			<span class="form-label-required">Geocache Code:</span><br />
			<asp:TextBox ID="tbCacheCode" runat="server" Columns="10" />&nbsp;<asp:Button ID="btnCacheCode" runat="server" Text="Search" cssclass="submit"/><br />
		</div>
</asp:PlaceHolder>
		<div style="margin-bottom:8px;">
			<span class="form-label-required">Near Geocache code:</span><br />
			<asp:TextBox ID="tbNearCacheCode" runat="server" Columns="40" />&nbsp;<asp:Button ID="btnNearCacheCode" runat="server" Text="Search" cssclass="submit"/>
		</div>
		<div>
			<span class="form-label-required">Caches near Coordinates:</span><br />
			<asp:TextBox ID="tbCoord" runat="server" Columns="40" />&nbsp;<asp:Button ID="btnCoordinates" runat="server" Text="Search" cssclass="submit"/>
			<asp:Panel ID="pnlHomeCoord" runat="server" CssClassclass="form-description">Your home coordinates are <asp:Literal ID="lblHomeCoordinates" runat="server" /> </asp:Panel>
		</div>
	</form>
	
	<asp:PlaceHolder ID="phList" runat="server">
		<div style="margin-top:10px">
		<asp:DataGrid id="dgGeocaches" runat="server" 
			AutoGenerateColumns="false"
			BorderColor="#AAAAAA" GridLines="Horizontal"
			CellPadding="2" CellSpacing="0"
			HeaderStyle-BackColor="#F3F3F3" FooterStyle-BackColor="#cccc99"
			ItemStyle-BackColor="#ffffff" AlternatingItemStyle-Backcolor="#F8F8F8">
			<Columns>
				<asp:BoundColumn HeaderText="Code" DataField="CacheCode" />
				<asp:TemplateColumn HeaderText="Name">
					<ItemTemplate>
						<asp:HyperLink id="HyperLink2" runat="server" text='<%#Server.HtmlEncode(Eval("CacheName"))%>' NavigateUrl='<%#"/Geocache.aspx?id=" & Eval("CacheID")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Dst">
				<ItemTemplate>
					<%#CountDistance(Eval("CacheLat") / Geo.Coordinates.SQLmultiplier, Eval("CacheLon") / Geo.Coordinates.SQLmultiplier)%>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
		</div>
	</asp:PlaceHolder>
	
</asp:Content>