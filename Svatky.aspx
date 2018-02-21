<%@ Page Title="Sv�tky" Language="VB" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Svatky.aspx.vb" Inherits="_Svatky" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<div style="clear:both; font-size:80%;">
		<div style="float:left; width:150px;"><b>Leden</b><br/>
			<asp:Repeater id="rptSvatkyMesic1" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>�nor</b><br/>
			<asp:Repeater id="rptSvatkyMesic2" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>B�ezen</b><br/>
			<asp:Repeater id="rptSvatkyMesic3" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
	</div>
	<div style="clear:both; padding-top:3px; font-size:80%;">
		<div style="float:left; width:150px;"><b>Duben</b><br/>
			<asp:Repeater id="rptSvatkyMesic4" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>Kv�ten</b><br/>
			<asp:Repeater id="rptSvatkyMesic5" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>�erven</b><br/>
			<asp:Repeater id="rptSvatkyMesic6" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
	</div>
	<div style="clear:both; padding-top:3px; font-size:80%;">
		<div style="float:left; width:150px;"><b>�ervenec</b><br/>
			<asp:Repeater id="rptSvatkyMesic7" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>Srpen</b><br/>
			<asp:Repeater id="rptSvatkyMesic8" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>Z���</b><br/>
			<asp:Repeater id="rptSvatkyMesic9" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
	</div>
	<div style="clear:both; padding-top:3px; font-size:80%;">
		<div style="float:left; width:150px;"><b>��jen</b><br/>
			<asp:Repeater id="rptSvatkyMesic10" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>Listopad</b><br/>
			<asp:Repeater id="rptSvatkyMesic11" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
		<div style="float:left; width:150px;"><b>Prosinec</b><br/>
			<asp:Repeater id="rptSvatkyMesic12" runat="server">
				<ItemTemplate><%#(Container.DataItem("Den") MOD 100)%>. <%#Container.DataItem("Svatek")%><br/></ItemTemplate>
			</asp:Repeater>
		</div>
	</div>

</asp:Content>