<%@ Page Language="vb" EnableViewState="False" AutoEventWireup="false" Inherits="_ChatFrames" CodeFile="ChatFrames.aspx.vb" %>
<?xml version="1.0" encoding="UTF-8" ?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="cs" lang="cs">
	<head>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<title>Chat</title>
		<link rel="stylesheet" media="all" type="text/css" href="/gfx/_css/Chat.css" />
		<asp:Literal ID="litMetaRefresh" Runat="server"></asp:Literal>
	</head>
	<body id="Body" runat="server">
	
		<asp:Panel ID="pnlTop" Runat="server" Visible="False">
			<span style="float:left;">Místnost: <i><%#RoomName%></i></span>
			<span style="float:right; margin-right:30px;">| <a href="ChatFrames.aspx?akce=history&room=<%=RoomID%>" target="_top">Historie</a> | <a href="Chat.aspx?room=<%=RoomID%>" target="_top">Ruènì obnovit</a> | <a href="ChatFrames.aspx?akce=odejit&room=<%=RoomID%>" target="_top">Odejít</a> |</span>
		</asp:Panel>

		<asp:Panel ID="pnlChat" Runat="server" Visible="False">
			<asp:Literal ID="litChatHtml" Runat="server"></asp:Literal>
		</asp:Panel>

		<asp:Panel ID="pnlBottom" Runat="server" Visible="False">
			<form action="ChatFrames.aspx?akce=add&room=<%=RoomID%>" method="post" name=FormBottom target="_top">
				<span><%=MyUser.Nick%>:</span>
				<input type="text" name="Txt" size="60" maxlength="250" class="inputtext" style="width:65%;" accesskey="x" autocomplete="off" />
				<input type="submit" value="Odeslat" class="submit" />
			</form>
		</asp:Panel>

		<asp:Panel ID="pnlRight" Runat="server" Visible="False">
			<div id="divRightMenu" runat="server">
				<a href="ChatFrames.aspx?akce=ShowRight&room=<%=RoomID%>" class="<%#Menu1Class%>">Uživatelé</a>
				<a href="ChatFrames.aspx?akce=ShowRight&room=<%=RoomID%>&right=setup" class="<%#Menu2Class%>">Nastavení</a>
			</div>

			<asp:Panel ID="pnlRightUsers" Runat="server" Visible="False">
				<div id="right-users">
					<ul><asp:literal id="litUsersHtml" runat="server" /></ul>
					<div style="margin-top:3px; font-style:italic; border-top:#aaaaaa 1px solid;">Uživatelé online: <asp:literal id="litUsersCount" runat="server" /></div>
				</div>
				<p style="margin-top:8px; margin-left:2px; font-size:70%;">Alt+X ... pøesune kurzor do políèka psaní.</p>
			</asp:Panel>

			<asp:Panel ID="pnlRightSetup" Runat="server" Visible="False">
				<form runat="server" target="_top">
				<div id="right-setup">
					<div style="margin-top:8px;">Automaticky obnovit za<br/>
						<select id="selReloadTime" runat="server" size="1"><option>10</option><option>20</option><option>30</option><option>40</option><option>50</option><option>60</option></select> sekund
					</div>
					<div style="margin-top:8px;">
						<asp:TextBox ID="tbLimitRows" Runat="server" Columns="2" MaxLength="2" Rows="1" CssClass="text" />&nbsp;Poèet øádkù
					</div>
					<div style="margin-top:8px; margin-bottom:4px;">
						<input type="checkbox" id="chbShowDate" runat="server" class="checkbox" />&nbsp;Zobrazit datum<br/>
						<input type="checkbox" id="chbShowTime" runat="server" class="checkbox" />&nbsp;Zobrazit èas
					</div>
					<asp:Button ID="btSaveSetup" Runat="server" Text="»» Uložit ««"></asp:Button>
				</div>
				</form>
			</asp:Panel>

		</asp:Panel>

	</body>
</html>