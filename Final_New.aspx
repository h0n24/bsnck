<%@ Page Title="New Final Coordinates" Language="VB" MasterPageFile="~/App_Shared/GeoFinal.master" AutoEventWireup="false" CodeFile="Final_New.aspx.vb" Inherits="Final_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:PlaceHolder ID="phReport" Runat="server" />
	
	<form id="Form1" runat="server">
		<p style="margin-bottom:8px;">Please enter all data well. Wrong information will be banned.</p>
		<asp:TextBox ID="tbReferrer" runat="server" Visible="false" EnableViewState="true" />
		<asp:PlaceHolder ID="phErrors" Runat="server" />

		<asp:PlaceHolder ID="phGeocache" runat="server">
			<dl><dt><span class="form-label-required">Geocache Code*:</span></dt>
				<dd><asp:TextBox ID="tbCacheCode" runat="server" Columns="10" /></dd>
			</dl>
			<dl><dt><span class="form-label-required">Geocache Name*:</span></dt>
				<dd><asp:TextBox ID="tbCacheName" runat="server" Columns="40" /><br />
					<span class="form-description">(must equal to original Geocaching name)</span>
				</dd>
			</dl>
			<dl><dt><span class="form-label-required">Type of Geocache*:</span></dt>
				<dd><asp:ListBox ID="lbCacheType" runat="server" SelectionMode="Single" Rows="1" /></dd>
			</dl>
		</asp:PlaceHolder>
		<dl><dt><span class="form-label-required">Geocache Coordinates*:</span></dt>
			<dd><asp:TextBox ID="tbCacheCoord" runat="server" Columns="40" /><br />
				<span class="form-description">for example: N49°17.654 E016°34.567 or N49°17'39.24" E16°34'34.02"</span>
			</dd>
		</dl>
		<dl><dt><span class="form-label-required">Final Coordinates*:</span></dt>
			<dd><asp:TextBox ID="tbFinalCoord" runat="server" Columns="40" /><br />
				<span class="form-description">for example: N49°17.654 E016°34.567 or N49°17'39.24" E16°34'34.02"</span>
			</dd>
		</dl>
		
		<asp:Button id="btSubmit" text="» Submit «" runat="server" cssclass="submit" />
	</form>

	
</asp:Content>