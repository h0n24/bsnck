<%@ Page Title="How it works?" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="HowItWorks.aspx.vb" Inherits="HowItWorks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div style="margin-bottom:10px;">
	<ul><li>The first publisher of the cache has to enter basic data about the cache.</li>
		<li>The first publisher of the coordinates gets credits from other users orders after their confirmations.</li>
		<li>The second and next publisher of the same coordinates is called the Validator.</li>
		<li>Validator gets credits for each valadation.</li>
		<li>Validator's IP address and identity must be unique within all cache publishers and validators.</li>
		<li>You never know whether the first published coordinates are correct. Still you can be the first one.</li>
		<li>You get credits only from Multi or Unknown (Mystery) geocaches.</li>
		<li>Your credits must be validated to avoid cheating before you order coordinates.</li>
	</ul>
	</div>
	
	<h6>Credit list:</h6>
	<div>
		+10 credits from each order made by user signed up after your invitation - <a href="/Invitation.aspx">Invite!</a><br />
		+10 credits from each order to the first publisher of coordinates<br />
		+10 credits to each validator (next publisher)<br />
		+10 credits to the Purchaser who gives feedback (found / not found)<br />
		-50 credits for each cache order<br />
	<asp:PlaceHolder ID="aaa" runat="server" Visible="false">
		+100 credits for 1€<br />
		+1000 credits for 9€<br />
	</asp:PlaceHolder>
	</div>
</asp:Content>