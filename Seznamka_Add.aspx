<%@ Page Language="VB" Title="M�j inzer�t" EnableViewState="True" MasterPageFile="~/App_Shared/Default.Master" AutoEventWireup="False" CodeFile="Seznamka_Add.aspx.vb" Inherits="_Seznamka_Add"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<form id="Form1" runat="server">
		<p><b>D�le�it� upozorn�n�:</b></p>
		<ul style="font-size:90%; margin-bottom:10px;">
			<li>komer�n� inzer�ty a nab�dky prostituce jsou zak�z�ny</li>
			<li>pou��vejte �esk� znaky (��������)</li>
			<li>inzer�ty nesm� obsahovat ��dn� telefonn� ��sla (mo�nost zneu�it�)</li>
			<li>mus� se jednat o inzer�t za ��elem sezn�men� jednotlivce nebo skupin</li>
			<li>inzer�t mus� b�t v souladu se z�kony �esk� republiky</li>
			<li><span class="povinne">!</span>...jsou povinn� polo�ky</li>
		</ul>
		<asp:PlaceHolder ID="phErrors" Runat="server" />
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr><td height="1" width="100"></td><td></td></tr>
			<tr><td align="right">Rubrika:<span class="povinne">!</span></td><td><asp:listbox id="lbRubrika" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Region:<span class="povinne">!</span></td><td><asp:listbox id="lbRegion" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Jm�no:<span class="povinne">!</span></td><td><asp:textbox id="tbJmeno" cssclass="input-text" runat="server" columns="30" /></td></tr>
			<tr><td></td><td><span class="text-komentar">zad�vejte pouze jm�no bez p��jmen�</span></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">V�k:<span class="povinne">!</span></td><td><asp:textbox id="tbVek" cssclass="input-text" runat="server" columns="4" /> rok�</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">V��ka:</td><td><asp:textbox id="tbVyska" cssclass="input-text" runat="server" columns="4" /> cm</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Po�et d�t�:</td><td><asp:textbox id="tbDeti" cssclass="input-text" runat="server" columns="4" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Odpov�di:</td><td><asp:radiobutton id="rbOdpovedi1" runat="server" groupname="Odpovedi" Text="pouze ulo�it, nic nepos�lej" /><br/>
				<asp:radiobutton id="rbOdpovedi2" runat="server" groupname="Odpovedi" checked="True" Text="po�li upozorn�n� o odpov�di na email" />
				<%REM �asem mo�n� povolit, pokud bude email potvrzen. <br/><asp:radiobutton id="rbOdpovedi3" runat="server" groupname="Odpovedi" Text="po�li odpov�� na email" />%>
			</td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Vzd�l�n�:</td><td><asp:listbox id="lbVzdelani" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Znamen�:</td><td><asp:listbox id="lbZnameni" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Postava:</td><td><asp:listbox id="lbPostava" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Barva vlas�:</td><td><asp:listbox id="lbVlasy" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Kou�en�:</td><td><asp:listbox id="lbKoureni" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right">Pit� alkohlu:</td><td><asp:listbox id="lbAlkohol" runat="server" rows="1" /></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Kon��ky:</td><td><asp:textbox id="tbKonicky" cssclass="input-text" runat="server" columns="43" width="99%" rows="2" textmode="MultiLine" />
				<p class="text-komentar">Vypi�te kr�tce va�e hlavn� kon��ky (sport, po��ta�e, p��roda, ...)</p></td></tr>
			<tr><td height="6"></td></tr>
			<tr><td align="right" valign="top">Text:<span class="povinne">!</span></td><td><asp:textbox id="tbTxt" cssclass="input-text" runat="server" columns="43" width="99%" rows="5" textmode="MultiLine" />
				<p class="text-komentar" style="color:red; font-style:italic;">Do textu inzer�tu je zak�z�no vkl�dat e-mailovou adresu, telefonn� ��slo, komer�n� nab�dky, nab�dky odm�n za slu�by nebo nab�dky a potk�vky prostituce.</p></td></tr>
			<tr><td></td><td><asp:button id="btSubmit" text="� Ulo�it �" runat="server" cssclass="submit" /></td></tr>
		</table>
	</form>

	<asp:PlaceHolder ID="phReport" Runat="server" />

</asp:Content>