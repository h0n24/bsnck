<%@ Page Language="vb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ASCX" TagName="Audit" Src="~/App_Shared/Audit.ascx" %>

<?xml version="1.0" encoding="UTF-8" ?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="cs" lang="cs">
	<head id="Head1" runat="server">
		<title>Dosol.cz</title>
		<link rel="openid.server" href="http://id.szn.cz/openidserver" />
		<link rel="openid.delegate" href="http://dolezal.id.email.cz" />
		<link rel="openid2.provider" href="http://id.szn.cz/openidserver" />
		<link rel="openid2.local_id" href="http://dolezal.id.email.cz" />
		<style type="text/css">
			body {	background-color: #E5DBD6;
				font-family: Verdana, 'Arial CE', Arial;
				color: #000000;
				TEXT-ALIGN: center;}
		</style>
	</head>
	<body>
		<div style="margin-top: 100px;"><img src="/gfx/logo/dosol.png" alt="Dosol" /></div>
		<div style="margin-top: 50px;">"Celý svìt je plný zázrakù,<br />
na které jsme si však tak zvykli, že je nazýváme všedními vìcmi."</div>
<div style="margin-top: 4px; color: #555555">Hans Christian Andersen</div>

		<div style="margin-top: 50px;">"The whole world is a series of miracles,<br />
but we're so used to them we call them ordinary things."</div>
<div style="margin-top: 4px; color: #555555">Hans Christian Andersen</div>

		<ASCX:Audit ID="ascxAudit" runat="server" />
	</body>
</html>