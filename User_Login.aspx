<%@ Page Title="User Login" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="User_Login.aspx.vb" Inherits="Final_User_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />

	<form id="Form1" runat="server" visible="false">
		<div>We have to verify our account.</div>
		<div style="margin-top:6px;">Check your mailbox to find email with verification code. Type code to the form field bellow.</div>
		<div style="margin-top:10px;">Don't have your code? <asp:HyperLink ID="hlResend" runat="server" NavigateUrl="/User_Login.aspx?task=sendcode&user={0}" Text="Resend your Verification Email" /></div>
		<div style="margin-top:15px;">
			<asp:PlaceHolder ID="phErrors" Runat="server" />
			<asp:TextBox ID="tbReferrer" runat="server" Visible="false" EnableViewState="true" />
			<span class="form-label-required">Verification Code*:</span><br />
			<asp:TextBox ID="tbCode" runat="server" Columns="20" AutoCompleteType="Disabled" />
		</div>
		<div style="margin-top:8px;">
			<asp:Button id="btSubmit" text="» Submit «" runat="server" cssclass="submit" />
		</div>
	</form>
</asp:Content>