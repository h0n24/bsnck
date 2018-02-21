<%@ Page Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="User_SignUp.aspx.vb" Inherits="User_SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<form id="Form1" runat="server">
		<div style="margin-bottom:6px;">Please provide your account details in the form below.</div>
		<asp:TextBox ID="tbReferrer" runat="server" Visible="false" EnableViewState="true" />
		<asp:PlaceHolder ID="phErrors" Runat="server" />

		<dl><dt><span class="form-label-required">UserName*:</span><br />
				<span class="form-description">Should match your UserName at Geocaching.com.</span></dt>
			<dd><asp:TextBox ID="tbUserName" runat="server" Columns="30" /><br />
				<asp:CheckBox ID="cbHideUserName" runat="server" Text="Do not show it to other users" />
			</dd>
		</dl>
		<dl><dt><span class="form-label-required">Email*:</span></dt>
			<dd><asp:TextBox ID="tbEmail" runat="server" Columns="30" /><br />
				<span class="form-description">(Validation e-mail will be sent to this address.)</span>
			</dd>
		</dl>
		<dl><dt><span class="form-label">Home Coordinates:</span></dt>
			<dd><asp:TextBox ID="tbHomeCoord" runat="server" Columns="30" /><br />
				<span class="form-description">for example: N49°17.654 E016°34.567 or N49°17'39.24" E16°34'34.02"</span>
			</dd>
		</dl>
		<dl><dt><span class="form-label-required">Password*:</span></dt>
			<dd><asp:TextBox ID="tbPassword" runat="server" Columns="20" TextMode="Password" />
			</dd>
		</dl>
		<dl><dt><span class="form-label-required">Re-Enter Password*:</span></dt>
			<dd><asp:TextBox ID="tbPassword2" runat="server" Columns="20" TextMode="Password" />
			</dd>
		</dl>
		<asp:PlaceHolder ID="phCaptcha" runat="server">
			<dl><dt><asp:Label ID="lblCaptcha" runat="server" CssClass="form-label-required" /></dt>
				<dd><asp:TextBox ID="tbCaptcha" runat="server" Columns="10" TextMode="SingleLine" /><asp:TextBox ID="tbCaptchaHash" runat="server" Visible="false" EnableViewState="true" />
				</dd>
			</dl>
		</asp:PlaceHolder>
		<dl><dt><span class="form-label">Newsletter:</span></dt>
			<dd><asp:CheckBox ID="cbNewsletter" runat="server" Text="Inform me of changes to the web site" />
			</dd>
		</dl>
		
		<asp:PlaceHolder ID="phAgree" runat="server">
			<div style="margin-bottom:0.5em;"><asp:CheckBox ID="cbAgree" runat="server" Text="I have read and agree to the <a href='TermsOfUse.aspx' target='blank'>Terms of Use and Privacy Statement</a>" /></div>
		</asp:PlaceHolder>
		
		<asp:Button id="btSubmit" text="» Submit «" runat="server" cssclass="submit" />
	</form>

</asp:Content>