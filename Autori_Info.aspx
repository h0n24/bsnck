<%@ Page Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Autori_Info.aspx.vb" Inherits="_Autori_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phAutorInfo" runat="server">
		<h3 id="txtAutorInfoJmeno" runat="server" style="margin-bottom: 6px; font-size: 1.3em;">#Jm�no#</h3>
		<p style="margin-bottom: 4px;">&middot;<b>V�k: </b><span id="spanAutorInfoVek" runat="server" style="font-style: italic">#XY let#</span></p>
		<p style="margin-bottom: 4px;">&middot;<b>Registrace: </b><span id="spanAutorInfoDatum" runat="server" style="font-style: italic">#Datum (XY dn�)#</span></p>
		<p>&middot;<b>Napsal(a) o sob�:</b></p>
		<p id="txtAutorInfoText" runat="server" style="margin-bottom:8px;">#Text s informacemi o autorovi.#</p>
		<p><b>P�ehled d�l:</b></p>
		<asp:Repeater ID="rptAutorDila" Runat="server">
			<ItemTemplate><p>&middot; <a href='/<%#Container.DataItem("Sekce")%>/autor-<%#Container.DataItem("Autor")%>.aspx'><%#Container.DataItem("SekceNazev")%></a> (<%#Container.DataItem("Pocet")%>)</p></ItemTemplate>
		</asp:Repeater>
		<p style="margin-top:1px;">�<a id="linkAutorInfoDila" runat="server">V�echna d�la</a></p>
		<p style="margin-top:5px;">�<a href="/Sbirky_List.aspx?autor=<%=UserID%>">Sb�rky, Knihy</a> (<%=SbirekCelkem%>) - <span class="text-komentar">nedokon�en�ch (<%=SbirekNedokoneno%>)</span></p>
	</asp:PlaceHolder>

	<div id="divAkce" runat="server" class="box-menu" style="margin-top:10px; font-size:0.9em; width:230px">
		<div>�<a href="/Vzkazy_Add.aspx?komu=<%=UserID%>">Napi� vzkaz</a></div>
		<div id="divOblibeniAutori" runat="server">�<a href="/OblibeniAutori.aspx?akce=add&autor=<%=UserID%>">P�idej mezi m� obl�ben� autory</a></div>
	</div>

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
</asp:Content>