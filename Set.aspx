<%@ Page Title="SET" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Empty.Master" AutoEventWireup="False" CodeFile="Set.aspx.vb" Inherits="_Set" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<h1>Nastavení prostøedí [<%=Request.Url.Host%>]</h1>
	<p><a href="/">START</a> | <a id="lnkReferer" runat="server" href="Referer">Referer</a></p>
	<p>Vše: <a href="Set.aspx?akce=true">True</a> / <a href="Set.aspx?akce=false">False</a></p>
	<form id="Form1" runat="server">
		<div style="border: #AAAAAA 1px solid; background-color: #DDDDDD;">
			<asp:checkbox id="cbReklama" runat="server"></asp:checkbox> Vypnout Reklamu<br/>
			<asp:checkbox id="cbAudit" runat="server"></asp:checkbox> Vypnout Audit<br/>
			<asp:checkbox id="cbStatistiky" runat="server"></asp:checkbox> Vypnout Statistiky<br/>
			<asp:checkbox id="cbErrors" runat="server"></asp:checkbox> Podrobné chybové hlášky<br/><br/>
			<asp:button id="btUlozit" text="» Uložit «" runat="server"></asp:button><br/>
			<asp:Label ID="lblReport" Runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
		</div>
	</form>

</asp:Content>