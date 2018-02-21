<%@ Control Language="vb" AutoEventWireup="false" Inherits="AscxAnketa" CodeFile="Anketa.ascx.vb" %>
<div class="box-menu">
<form method="POST" action="Anketa_Hlasuj.aspx?id=<%=Anketa.IDx%>"><input type="hidden" name="Referer" value="<%=Request.Url.ToString%>" />
	<div class="title" align="center">Anketa</div>
	<div style="font-size:90%; margin-bottom:3px;"><%=Anketa.Dotaz%></div>
	<%For f as Int16 = 0 To Anketa.Hlasy.Count - 1%>
		<div style="font-size:80%; background-color: <%=MyIni.Colors.Anketa.Split(",")(f AND 1)%>;"><a></a>
			<input type="radio" name="AnketaHlas" value="<%=f+1%>">
			<img width="<%=Anketa.Hlasy(f)/Anketa.HlasyMax*50%>" height="10" src="gfx/anketa_prouzek.gif"><%=Int(Anketa.Hlasy(f)*100/Anketa.HlasySuma)%>%<br/><%=Anketa.Texty(f)%>
		</div>
	<%Next%>
	<div id="divHlasuj" runat="server" align="center" style=" margin-top:5px;"><input type="submit" class="submit" value="Hlasuj" /></div>
	<div id="divZprava" runat="server" align="center" style="font-size:80%; font-weight:bold; margin-top:5px;">##Zpráva##</div>
	<div style="font-size:80%; text-align:center;">Hlasovalo <%=Anketa.HlasySuma%> lidí</div>
</form>
</div>
