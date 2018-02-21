<%@ Page Title="Pohlednice � poslat" Language="VB" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Pohlednice_Poslat.aspx.vb" Inherits="_Pohlednice_Poslat" %>
<%@ Register TagPrefix="MOJE" TagName="PohledniceFull" Src="~/App_Shared/PohledniceFull.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<form id="Form1" runat="server">
		<MOJE:PohledniceFull runat="server" Pohlednice="<%#iID%>" ID="Pohlednicefull1" />
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<p style="font-size: 90%; margin-bottom: 10px;"><span class="povinne">!</span>...jsou povinn� polo�ky</p>
		<p class="text-nadpis">Va�e Jm�no:<span class="povinne"> !</span></p>
		<p style="margin-bottom: 4px;"><asp:textbox id="tbJmeno" runat="server" cssclass="input-text" Columns="40" /></p>
		<p class="text-nadpis">V� Email:<span class="text-komentar"> (doporu�ujeme zadat, jinak nedostanete odpov��)</span></p>
		<div><asp:textbox id="tbEmailFrom" runat="server" cssclass="input-text" Columns="40" /></div>
		<p style="margin-bottom: 4px;"><asp:CheckBox ID="cbPotvrzeni" Runat="server" Checked="True" /> Chcete zaslat potvrzen� o p�e�ten�?</p>
		<div class="text-nadpis">Email adres�ta:<span class="povinne"> !</span><span class="text-komentar"> (pouze jeden p��jemce !!)</span>
			<select id="selKontakty" runat="server" size="1" style="font-size:90%; width:140px; float:right;" onchange="TransferEmail()"></select>
			<script type="text/javascript"> <!--
			function TransferEmail() {
				var str = document.forms[0].<%=selKontakty.ClientID %>.value;
				if (str.length > 0) {
					document.forms[0].<%=tbEmailTo.ClientID %>.value = str;
				}
			}
			-->
			</script>
		</div>
		<p style="margin-bottom: 4px;"><asp:textbox id="tbEmailTo" runat="server" cssclass="input-text" Columns="49" /></p>
		<p class="text-nadpis">Text:<span class="povinne"> !</span></p>
		<p><asp:TextBox id="tbTxt" cssclass="input-text" runat="server" Columns="54" Width="99%" Rows="5" TextMode="MultiLine" /></p>
		<p class="text-komentar" style="margin-bottom: 4px;">(* Inspiraci text� m��ete naj�t v sekci p��n��ek.</p>
		<div style="margin-bottom:4px;"><asp:textbox id="tbOchrana" cssclass="input-text" runat="server" columns="3" textmode="SingleLine" AutoCompleteType="None" /> <asp:label id="lblOchrana" runat="server" style="font-weight:bold" /><span class="text-komentar"> ... opi�te 3 znaky do pol��ka (ochrana proti robot�m)</span><span class="povinne"> !</span></div>
		<asp:Button id="btSubmit" text="� Poslat �" runat="server" cssclass="submit" />
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>