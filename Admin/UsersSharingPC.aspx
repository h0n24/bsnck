<%@ Page Language="VB" Title="Users sharing PC" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="UsersSharingPC.aspx.vb" Inherits="Admin_UsersSharingPC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div style="margin-bottom:8px">Uživatelé, kteří používají stejné PC.<br />
	POZOR! Nemusí se jednat o duplicitní nicky - např. sourozenci sdílejí počítač.</div>
	<h3>Přepínání uživatelů</h3>
	
	<asp:Label ID="lbl1" runat="server"></asp:Label>

</asp:Content>