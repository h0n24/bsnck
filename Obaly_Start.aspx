<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Obaly_Start.aspx.vb" Inherits="_Obaly_Start" %>
<%@ Register TagPrefix="MOJE" TagName="ObalyPismena" Src="~/App_Shared/ObalyPismena.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<MOJE:ObalyPismena runat="server" />
	<p style="margin-bottom:16px; text-align:center;">Vyber poèáteèní písmeno.</p>

</asp:Content>