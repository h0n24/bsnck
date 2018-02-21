<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Obaly_View.aspx.vb" Inherits="_Obaly_View" %>
<%@ Register TagPrefix="MOJE" TagName="ObalyPismena" Src="~/App_Shared/ObalyPismena.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
		<MOJE:ObalyPismena runat="server" />

		<p style="color:red; margin-bottom:6px;">Chyba #1774</p>
		<p>Pøi naèítání stránky došlo k chybì. Záznam o chybì byl uložen do databáze a bude správcem provìøen.</p>

</asp:Content>