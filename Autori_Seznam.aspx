<%@ Page Title="Seznam autor�" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Autori_Seznam.aspx.vb" Inherits="_Autori_Seznam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:Panel id="pnlAutoriSeznamMenu" Runat="server">
		<form id="Form1" runat="server" style="text-align:center">
		<p>Vyber po��te�n� p�smeno autora:</p>
		<p style="margin-bottom: 4px;">
			<asp:Repeater ID="RepeaterPismena" Runat="server">
				<ItemTemplate><a href="/Autori/seznam-<%#Container.DataItem("Pismeno")%>.aspx"><%#UCase(Container.DataItem("Pismeno"))%></a></ItemTemplate>
				<SeparatorTemplate>&nbsp;</SeparatorTemplate>
			</asp:Repeater>
		</p>
		<p style="margin-bottom: 20px;"><a href="/Autori/seznam.aspx">� V�ichni auto�i �</a></p>
		<p>P��padn� zkuste autora naj�t.</p>
			<asp:RegularExpressionValidator id="RegularExpressionValidatorFiltr" Runat="server" Display="Dynamic"
				ControlToValidate="tbFiltr" ValidationExpression=".{3}.*" EnableClientScript="False"
				ErrorMessage="<p>Mus�te zadat nejm�n� 3 znaky !!</p>" />
			<asp:RequiredFieldValidator id="RequiredFieldValidatorFiltr" runat="server" Display="Dynamic"
	            ControlToValidate="tbFiltr"
	            ErrorMessage="Mus�te zadat nejm�n� 3 znaky !!" />
			<div><asp:TextBox id="tbFiltr" runat="server" CssClass="input-text" Width="200px"></asp:TextBox>&nbsp;<input type="submit" class="submit" value="Najdi" /></div>
		</form>
	</asp:Panel>
	
	<asp:Panel id="pnlAutoriSeznamShow" Runat="server" Visible="False">
		<p style="margin-bottom: 3px;">Nalezeno <%#PocetAutoru%> autor�.</p>
		<asp:Repeater ID="RepeaterAutori" Runat="server">
			<ItemTemplate><p>� <a href="/Autori/<%#Container.DataItem("AutorUser")%>-info.aspx"><%#Server.HtmlEncode(Container.DataItem("UserNick"))%></a></p></ItemTemplate>
		</asp:Repeater>
	</asp:Panel>
	
</asp:Content>