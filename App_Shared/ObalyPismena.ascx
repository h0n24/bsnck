<%@ Control Language="vb" AutoEventWireup="false" Inherits="_ObalyPismena_Ascx" CodeFile="ObalyPismena.ascx.vb" %>
<div class="box-menu" style="text-align:center">
	<asp:Repeater ID="rptObalyPismena" Runat="server">
		<ItemTemplate><a style="font-weight:bold; margin-left:4px" href="/Obaly_List.aspx?sekce=<%#Request.QueryString("sekce")%>&amp;znak=<%#Container.DataItem("Pismeno")%>"><%#Container.DataItem("Zobrazit")%></a></ItemTemplate>
	</asp:Repeater>
</div>