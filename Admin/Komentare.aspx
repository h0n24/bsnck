<%@ Page Title="Komentáøe" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Admin.Master" AutoEventWireup="False" CodeFile="Komentare.aspx.vb" Inherits="Admin_Komentare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<asp:Panel id="pnlList" Runat="server" Visible="False">
		<asp:DataGrid id="dgKomentareList" runat="server" 
			AutoGenerateColumns="False"
			BorderColor="black" GridLines="Vertical"
			CellPadding="2" CellSpacing="0"
			Font-Names="Arial" Font-Size="8pt"
			HeaderStyle-BackColor="#cccc99" FooterStyle-BackColor="#cccc99"
			ItemStyle-BackColor="#ffffff" AlternatingItemStyle-Backcolor="#dddddd">
			<Columns>
				<asp:TemplateColumn HeaderText="Edit">
					<ItemTemplate>
						<asp:HyperLink id="HyperLink2" runat="server" text="edit" NavigateUrl='<%#String.Format("?sekce={0}&amp;id={1}", Request.QueryString("sekce"), Eval("KomentID"))%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:BoundColumn HeaderText="Datum" DataField="KomentDatum" DataFormatString="{0:d}" />
				<asp:TemplateColumn HeaderText="Poslal">
					<ItemTemplate><%#Server.HtmlEncode(Container.DataItem("UserNick") & "")%></ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Text">
					<ItemTemplate><%#Server.HtmlEncode(Container.DataItem("KomentTxt"))%></ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</asp:Panel>

	<asp:Panel id="pnlEdit" Runat="server" Visible="False">
		<form id="Form1" runat="server"><input id="inpReferer" runat="server" type="hidden" />
			<div>Komentoval: <asp:Label ID="lblJmeno" Runat="server" /><br/>
				Datum: <asp:Label ID="lblDatum" Runat="server" /><br/>
				Titulek: <asp:hyperlink ID="hlTitulek" Runat="server" /><br/>
				Sekce: <asp:Label ID="lblSekce" Runat="server" />
			</div>
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<asp:TextBox ID="tbText" Runat="server" TextMode="MultiLine" Columns="70" Rows="14"></asp:TextBox>
			<br/>
			<asp:Button id="btSubmit" text="» Uložit «" runat="server" cssclass="ButtonSubmit" />
			<asp:HyperLink ID="hlDelete" runat="server" Text="!! SMAZAT !!" NavigateUrl="?akce=delete&amp;sekce={0}&amp;id={1}&referer={2}" onclick="if(!confirm('Smazat komentáø?')) return false;" class="ButtonDelete" style="margin-left:350px;"></asp:HyperLink>
		</form>
	</asp:Panel>
</asp:Content>