<%@ Page Language="vb" EnableViewState="False" AutoEventWireup="false" Inherits="_Chat" CodeFile="Chat.aspx.vb" %>
<?xml version="1.0" encoding="UTF-8" ?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="cs" lang="cs">
	<head>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<title>Chat</title>
	</head>
	<frameset cols="*,160" border="0">
		<frameset rows="20,*,26" border="0">
			<frame src="ChatFrames.aspx?akce=ShowTop&amp;room=<%=Request.Querystring("room")%>" name="topframe" id="topframe" scrolling="no" marginwidth="0" marginheight="0" />
			<frame src="ChatFrames.aspx?akce=ShowChat&amp;room=<%=Request.Querystring("room")%>" name="mainframe" id="mainframe" scrolling="auto" marginwidth="0" marginheight="0" />
			<frame src="ChatFrames.aspx?akce=ShowBottom&amp;room=<%=Request.Querystring("room")%>" name="bottomframe" id="bottomframe" scrolling="no" marginwidth="0" marginheight="0" />
		</frameset>
		<frame src="ChatFrames.aspx?akce=ShowRight&amp;room=<%=Request.Querystring("room")%>" name="rightframe" id="rightframe" scrolling="auto" marginwidth="0" marginheight="0" />
	</frameset>
</html>