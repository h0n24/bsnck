<%@ Page Title="Komentáøe" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Komentare_View.aspx.vb" Inherits="_Komentare_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<p style="margin-bottom:4px">Komentáøe slouží k vyjádøení vašich názorù, postojù èi kritiky.</p>
	<asp:Panel ID="pnlNew" runat="server" style="margin-bottom:5px">
		<asp:HyperLink ID="hlNew" runat="server" NavigateUrl="/Komentare_Add.aspx?sekce={0}&dbid={1}" Text="++ Pøidejte svùj komentáø ++" />
	</asp:Panel>
	<asp:PlaceHolder ID="phMain" runat="server">
		<div style="margin-bottom:3px; font-style:italic"><asp:Literal ID="litZaznamu" runat="server"></asp:Literal></div>
		<asp:panel id="pnlKomentareView" runat="server">
			<div><%=SB.ToString%></div>
			<div id="divKomentareViewPagesBox" runat="server" class="ListPagesBox">#1111111#</div>
		</asp:panel>
	</asp:PlaceHolder>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>