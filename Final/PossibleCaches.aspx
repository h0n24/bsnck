<%@ Page Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="PossibleCaches.aspx.vb" Inherits="Final_PossibleCaches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<div>List of all possible Geocaches</div>
	<asp:PlaceHolder id="phCacheDirectLink" runat="server" Visible="false">
		<div style="margin-top:8px;">Cache <asp:HyperLink ID="hlGC" runat="server"></asp:HyperLink></div>
	</asp:PlaceHolder>
	<div style="margin-top:8px;">
		<asp:Literal id="litGroups" runat="server" />
	</div>

</asp:Content>