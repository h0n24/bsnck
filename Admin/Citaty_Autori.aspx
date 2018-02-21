<%@ Page Language="VB" Title="Autoøi citátù" EnableViewState="True" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="false" CodeFile="Citaty_Autori.aspx.vb" Inherits="Admin_Citaty_Autori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<div style="margin-bottom:6px;"><asp:ListBox ID="lbAutori" Runat="server" Rows="15" AutoPostBack="True" OnSelectedIndexChanged="ChangeAutor" /></div>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<asp:Panel ID="pnlMainForm"	Runat="server" Visible="False">
			<div style="margin-bottom:6px;"><b>Jméno:</b><br/><input id="inpJmeno" runat="server" size="40" /></div>
			<div style="margin-bottom:6px;"><b>Život:</b><br/><input id="inpZivot" runat="server" size="80" /></div>
			<div style="margin-bottom:6px;"><b>Význam:</b><br/><input id="inpVyznam" runat="server" size="80" /></div>
			<div style="margin-bottom:6px;"><b>Popis:</b><br/><textarea id="txtPopis" runat="server" rows="6" cols="80" style="width:99%"></textarea></div>
			<div style="float:left;">
				<asp:button id="btSave" runat="server" text="» Uložit «" font-bold="True" cssclass="submit" OnClick="ButtonSave_Click" />
			</div>
			<div style="float:right;">
				<asp:button id="btDelete" runat="server" text="!! SMAZAT !!" cssclass="ButtonDelete" OnClick="ButtonDelete_Click" />
			</div>
		</asp:Panel>
	</form>

</asp:Content>