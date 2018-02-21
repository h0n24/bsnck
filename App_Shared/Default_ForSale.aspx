<%@ Page Language="vb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ASCX" TagName="Audit" Src="~/App_Shared/Audit.ascx" %>

<?xml version="1.0" encoding="UTF-8" ?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="cs" lang="cs">
	<head id="Head1" runat="server">
		<title>Na prodej / For Sale</title>
		<style type="text/css">
			body {	background-color: #D5CBC6;
				font-family: Verdana, 'Arial CE', Arial;
				color: #000000;
				TEXT-ALIGN: center;}
		</style>
	</head>
	<body>
		<div style="margin-top: 100px;">(CZ) Na prodej<br />(EN) For Sale</div>
		<ASCX:Audit ID="ascxAudit" runat="server" />
	</body>
</html>