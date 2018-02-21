<%@ Page Title="Autoøi citátù" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/MasterPage_Citaty.Master" AutoEventWireup="False" CodeFile="Citaty_Autori.aspx.vb" Inherits="_Citaty_Autori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:Panel id="pnlAutoriSeznam" Runat="server" Visible="False">
		<p style="margin-bottom:3px;">Seznam všech autorù:</p>
		<asp:Repeater ID="rptAutoriSeznam" Runat="server">
			<ItemTemplate><p>&nbsp;<a href='/Citaty/autor-<%#Container.DataItem("AutorID")%>.aspx'><%#Container.DataItem("AutorJmeno")%></a><span style="color:#777777; padding-left:10px;">(<%#Container.DataItem("Pocet")%>)</span></p></ItemTemplate>
		</asp:Repeater>
	</asp:Panel>

	<asp:Panel id="pnlAutorInfo" runat="server" Visible="False">
		<h5 style="margin-bottom:4px; font-size:130%;"><%#dt.Rows(0)("AutorJmeno")%></h5>
		<p style="margin-bottom:4px;">»<a href='/Citaty/autor-<%#dt.Rows(0)("AutorID")%>.aspx'>Pøehled citátù</a></p>
		<p style="margin-bottom:4px;">&middot;<b>Žil: </b><%#dt.Rows(0)("AutorZivot")%></p>
		<p style="margin-bottom:4px;">&middot;<b>Význam: </b><%#dt.Rows(0)("AutorVyznam")%></p>
		<p style="margin-bottom:4px;">&middot;<b>Popis: </b><%#dt.Rows(0)("AutorPopis")%></p>
	</asp:Panel>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>