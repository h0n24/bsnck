<%@ Page Title="Geocaches" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="Geocaches.aspx.vb" Inherits="Geocaches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<asp:PlaceHolder ID="phGeocaches" runat="server">

		<div>New geocaches with final coordinates:</div>
		
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
			</Columns>
		</asp:DataGrid>

	</asp:PlaceHolder>
	
</asp:Content>