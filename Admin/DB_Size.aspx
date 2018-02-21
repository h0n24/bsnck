<%@ Page Title="Velikost databáze" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.master" AutoEventWireup="false" CodeFile="DB_Size.aspx.vb" Inherits="Admin_DB_Size" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<asp:DataGrid id="dgDatabase" runat="server"
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

			<asp:DataGrid id="dgTables" runat="server"
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

</asp:Content>