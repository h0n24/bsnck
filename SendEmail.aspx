<%@ Page Language="VB" Title="Pošli email" EnableViewState="False" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="SendEmail.aspx.vb" Inherits="_SendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<form id="Form1" runat="server">
		<input type="hidden" id="inpReferer" runat="server" name="inpReferer"/>
		<p style="font-size:90%; margin-bottom:10px;"><span class="povinne">!</span>...jsou povinné položky</p>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p class="text-nadpis">Vaše Jméno:<span class="povinne"> !</span></p>
		<p style="margin-bottom: 4px;"><asp:textbox id="tbJmeno" runat="server" cssclass="input-text" Columns="40" /></p>
		<p class="text-nadpis">Váš Email:<span class="text-komentar"> (doporuèujeme zadat, jinak nedostanete odpovìï)</span></p>
		<p style="margin-bottom: 4px;"><asp:textbox id="tbEmailFrom" runat="server" cssclass="input-text" Columns="40" /></p>
		<p class="text-nadpis">Email adresáta:<span class="povinne"> !</span>
			<select id="selKontakty" runat="server" size="1" style="font-size:90%; width:140px; float:right;" onchange="TransferEmail()"></select>
			<script type="text/javascript"> <!--
			function TransferEmail() {
				var str = document.forms[0].<%=selKontakty.ClientID%>.value;
/*	Pro více emailù				var sEmail = document.forms[0].tbEmailTo.value;	*/
				if (str.length > 0) {
/*							if (sEmail.length > 0) {
						sEmail = sEmail + ', ';
					}	
					document.forms[0].tbEmailTo.value = sEmail + str;	*/
					document.forms[0].<%=tbEmailTo.ClientID%>.value = str;
				}
			}
			-->
			</script>
		</p>
		<p style="margin-bottom: 4px;"><asp:textbox id="tbEmailTo" runat="server" cssclass="input-text" Columns="49" /></p>
		<div id="divTxt" runat="server">
			<p class="text-nadpis">Text:<span class="povinne"> !</span></p>
			<p style="margin-bottom: 4px;"><asp:TextBox id="tbTxt" cssclass="input-text" runat="server" Columns="54" Width="99%" Rows="8" TextMode="MultiLine" /></p>
		</div>
		<asp:Button id="btSubmit" text="» Poslat «" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>