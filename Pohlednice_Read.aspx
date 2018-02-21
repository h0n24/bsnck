<%@ Page Title="Vyzvednutí pohlednice" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Pohlednice_Read.aspx.vb" Inherits="_Pohlednice_Read" %>
<%@ Register TagPrefix="MOJE" TagName="Pohlednice" Src="~/App_Shared/Pohlednice.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:panel id="pnlPohledniceReadUid" runat="server" visible="False">
		<form action="/Pohlednice/read.aspx" method="post">
			<p id="pPohledniceReadUidErr" runat="server" style="text-align:center; margin-bottom:6px; color:Red;" class="text-nadpis">#Chyba#</p>
			<p style="text-align:center; margin-bottom:2px;" class="text-nadpis">Kód vaši pohlednice:</p>
			<p style="text-align:center; margin-bottom:6px;"><input name="UID" type="text" size="20" value="<%#sUID%>" style="text-align:center" /></p>
			<p style="text-align:center;"><input type="submit" value="» Ukázat «" class="submit" /></p>
		</form>
	</asp:panel>

	<asp:panel id="pnlPohledniceRead" runat="server" visible="False">
		<p style="text-align:center; margin-bottom:6px;">Pohlednice od èlovìka jménem <i><%#dt.rows(0)("SendOdJmeno")%></i> ze dne <i><%#sDatum%></i></p>
		<MOJE:Pohlednice runat="server" Format="<%#sFormat%>" Soubor="<%#sSoubor%>" Rozmery="<%#sRozmery%>" />
		<p style="text-align:center;"><i><%#Server.HtmlEncode(sTxt).Replace(vbCr, "<br/>" & vbCr)%></i></p>
	</asp:panel>
</asp:Content>