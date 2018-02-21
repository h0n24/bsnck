<%@ Page Language="VB" Title="Cache" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="Cache.aspx.vb" Inherits="Admin_Cache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

		<asp:Panel id="pnlAdminCacheList" Runat="server" Visible="False">
			<p style="margin-bottom:4px;"><u>Cache:</u></p>
			<asp:Repeater ID="rptAdminCacheList" Runat="server">
				<itemtemplate>[<a href="Cache.aspx?akce=delete&type=cache&name=<%#Container.DataItem%>">DEL</a>] [<a href="Cache.aspx?akce=view&type=cache&name=<%#Container.DataItem%>">zobraz</a>] <b><%#Container.DataItem%></b> <span style="color:#777777;">(<%#Cache.Get(Container.DataItem).GetType.ToString%>)</span><br/></itemtemplate>
			</asp:Repeater>
			<p style="margin-top:6px; margin-bottom:4px;"><u>Application:</u></p>
			<asp:Repeater ID="rptAdminCacheListApp" Runat="server">
				<itemtemplate>[<a href="Cache.aspx?akce=delete&type=application&name=<%#Container.DataItem%>">DEL</a>] 
						[<a href="Cache.aspx?akce=view&type=application&name=<%#Container.DataItem%>">zobraz</a>] <b><%#Container.DataItem%></b> <span style="color:#777777;">(<%#GetApplicationType(Container.DataItem)%>)</span><br/>
				</itemtemplate>
			</asp:Repeater>
		</asp:Panel>

		<asp:Panel id="pnlAdminCacheView" Runat="server" Visible="False">
			<asp:literal id="litAdminCacheView" runat="server" visible="False">#Text#</asp:literal>
			<asp:DataGrid id="dgAdminCacheView" runat="server" visible="False"
				BorderColor="black"
				GridLines="Vertical"
				cellpadding="2"
				cellspacing="0"
				Font-Names="Arial"
				Font-Size="8pt"
				HeaderStyle-BackColor="#cccc99"
				FooterStyle-BackColor="#cccc99"
				ItemStyle-BackColor="#ffffff"
				AlternatingItemStyle-Backcolor="#cccccc">
			</asp:DataGrid>
		</asp:Panel>

</asp:Content>