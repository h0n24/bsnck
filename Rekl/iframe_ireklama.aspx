<%@ Page Language="vb" %>
<html>
	<head>
		<title>Reklama</title>
		<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
			P, FORM, DIV, IMG {margin: 0px;}
			IMG {border:0px;}
			a:link, a:active, a:visited {color: #FFA244; text-decoration: none;}
			a:hover {color: #FF4020; text-decoration: underline;}
			a.pamatovatklik:visited {color: #AAAAAA; text-decoration: none;}
			body {margin: 0px;}
		</style>
	</head>
	<body leftmargin="0" topmargin="0" style="background-color:transparent;"><%
Dim WebID as String = Request.Querystring("webid")
Dim sPozice as String = Request.Querystring("pozice")
Dim s as String
Select Case WebID & sPozice
	Case "prejcz468N": s="pos=1&typ=1&svr=1750&sekce=5501"
	Case "prejcz468D": s="pos=2&typ=1&svr=1750&sekce=5525"
	Case "prejcz125N": s="pos=1&typ=2&svr=1750&sekce=5526"
	Case "citujcz468N": s="pos=1&typ=1&svr=1770&sekce=5580"
	Case "citujcz468D": s="pos=2&typ=1&svr=1770&sekce=5581"
	Case "citujcz125N": s="pos=1&typ=2&svr=1770&sekce=5609"
	Case "ilovecz468N": s="pos=1&typ=1&svr=3117&sekce=9612"
	Case "ilovecz468D": s="pos=2&typ=1&svr=3117&sekce=9613"
	Case "ilovecz125N": s="pos=1&typ=2&svr=3117&sekce=9614"
	Case "litercz468N": s="pos=1&typ=1&svr=3742&sekce=11363"
	Case "litercz468D": s="pos=2&typ=1&svr=3742&sekce=11364"
	Case "litercz125N": s="pos=1&typ=2&svr=3742&sekce=11365"
	Case "basnecz468N": s="pos=1&typ=1&svr=2991&sekce=9270"
	Case "basnecz468D": s="pos=2&typ=1&svr=2991&sekce=9271"
	Case "basnecz125N": s="pos=1&typ=2&svr=2991&sekce=9272"
	Case "basnickycz468N": s="pos=1&typ=1&svr=2991&sekce=9270"
	Case "basnickycz468D": s="pos=2&typ=1&svr=2991&sekce=9271"
	Case "basnickycz125N": s="pos=1&typ=2&svr=2991&sekce=9272"
	Case "ifuncz468N": s="pos=1&typ=1&svr=3416&sekce=10402"
	Case "ifuncz468D": s="pos=2&typ=1&svr=3416&sekce=10403"
	Case "ifuncz125N": s="pos=1&typ=2&svr=3416&sekce=10404"
	Case "obalycdcz468N": s="pos=1&typ=1&svr=5680&sekce=16392"
	Case "obalycdcz468D": s="pos=2&typ=1&svr=5680&sekce=16393"
	Case "obalycdcz125N": s="pos=1&typ=2&svr=5680&sekce=16394"
End Select%>
<!-- iReklama.cz - reklamní plocha sekce - zaèátek -->
<SCRIPT Language="JavaScript" type="text/javascript">
<!--
Tmp=Math.floor(1000000 * Math.random());
document.write("<SCR" + "IPT SRC=\"http://ad2.ireklama.cz/rs-ukaz.asp?<%=s%>&tmp="+Tmp+"\" Language=\"JavaScript\" type=\"text/javascript\"></SCR" + "IPT>");
// -->
</SCRIPT>
<NOSCRIPT><center><a href="http://ad2.ireklama.cz/"><font size="-2" face="Verdana"><b><img src="http://ad2.ireklama.cz/images/bann1.gif" border="1" alt="iReklama.cz - nový reklamní systém" width="468" height="60" /><br/>iReklama.cz - nový reklamní systém</b></font></a></center></NOSCRIPT>
<!-- iReklama.cz - reklamní plocha sekce - konec -->
	</body>
</html>