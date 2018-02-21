<%@ Page Title="Invite friends" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="Invitation.aspx.vb" Inherits="Invitation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:PlaceHolder ID="phReport" Runat="server" />

	<asp:PlaceHolder ID="phInvitation" runat="server">
		<div style="margin-bottom:8px;">Sent an email to your friends and add the following link to the text:<br />
			http://abux.net/inv-<%=U.ID%>.aspx
		</div>
		<div style="margin-bottom:8px;">
			Users signed up after click on your link will automaticly provide some credits to you.<br />
			See <a href="/HowItWorks.aspx">How it works?</a>
		</div>
		<div>Example:<br />
			Hi,<br />
			I'd like to recomend you web site <u>http://abux.net/inv-<%=U.ID%>.aspx</u> which contains final coordinates of Multi / Unknown geocaches.
		</div>
	</asp:PlaceHolder>

</asp:Content>