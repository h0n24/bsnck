<%@ Page ContentType="text/css" Language="VB" AutoEventWireup="false" CodeFile="Css.aspx.vb" Inherits="_Css" %>
<%@ OutputCache Duration="1" VaryByCustom="AbsolutePath" VaryByParam="none" %>
/* Generováno <%=Now%> pro web <%=MyIni.Web.Name%>*/

BODY {FONT-SIZE: 13px;}
h1, h2, h3, h4, h5, h6 {
	font-size: 1em;
	margin: 0px;}
P, FORM, DIV, IMG {margin: 0px;}
UL {margin: 0px 0px 0px 0px;
	padding: 0px;}
LI {margin: 0px 0px 0px 16px;
	padding: 0px 0px 0px 0px;}
IMG {border: 0px;}
a:link, a:active, a:visited {color: <%=MyIni.Colors.AHref%>; text-decoration: none;}
a:hover {color: <%=MyIni.Colors.AHrefOver%>; text-decoration: underline;}
a.pamatovatklik:visited {color: <%=MyIni.Colors.AHrefVisited%>; text-decoration: none;}
.ButtonDelete {color:#FFFFFF; background-color:#AA2222; padding: 0px 10px 0px 10px; border: 1px solid #000000;}
A.ButtonDelete:link {color:#FFFFFF; background-color:#AA2222; padding: 0px 10px 0px 10px; border: 1px solid #808080;}

body {
	background-color: <%=MyIni.Colors.Bg%>;
	font-family: Verdana, 'Arial CE', Arial;
	color: <%=MyIni.Colors.Text%>;
	margin: 0px;
	TEXT-ALIGN: center;}

.box-menu {
	background-color: <%=MyIni.Colors.Box1Bg%>;
	border: 1px solid <%=MyIni.Colors.Box1Border%>;
	margin-bottom: 6px;}
.box-menu h2 {
	font-weight: normal;
	padding-left: 2px;}
.box-menu h3 {
	font-weight: normal;
	font-size: 1em;
	padding-left: 2px;}
.box-menu h4 {
	font-size: 90%;
	font-weight: normal;
	padding-left: 2px;}
.box-menu .title {
	border-bottom: 1px solid #888888;
	padding-left: 1px;
	background-color: <%=MyIni.Colors.Box1Title%>;}
.box-menu .underline {
	border-bottom: solid 1px gray; padding-bottom: 1px; margin-bottom: 1px;}
.box2 {
	background-color: <%=MyIni.Colors.Box2Bg%>;
	border: 1px solid <%=MyIni.Colors.Box2Border%>;
	margin-bottom: 6px;}
.box2 P {
	padding-left: 1px;}
.box2 .title {
	padding: 1px;
	font-size: 85%;
	background-color: <%=MyIni.Colors.Box2Title%>;}
.box2 .nadpis {
	padding: 1px;
	margin-bottom: 1px;
	font-weight: bold;}
.box2 H3 {
	padding: 1px;
	background-color: <%=MyIni.Colors.Box2Title%>;}

.hidden {
	DISPLAY: none}
#page {
	margin: 0px auto 0px auto;
	width: 770px;
	text-align: left;}
#page-top {
	height: 60px;
	margin-bottom: 4px;}
#page-middle {
	margin-bottom: 3px;
	clear: both;}
#page-bottom {
	margin-bottom: 3px;
	clear: both;}
#page-left {
	width: 130px;
	float: left;
	margin-right: 3px;
	overflow: hidden;}
#page-main {
	float: left;
 	width: 474px;
	overflow: hidden;}
#page-main-citaty {
	overflow: hidden;}
#page-right {
	width: 160px;
	float: right;
	overflow: hidden;}

.font12 {font-size: 1.2em;}
.font11 {font-size: 1.1em;}
.font10 {font-size: 1em;}
.font09 {font-size: 0.9em;}
.font08 {font-size: 0.8em;}
.font07 {font-size: 0.7em;}
.normal {font-size:100%; font-weight:normal}
.NEWtitle {font-size:120%; font-weight:bold}
.NEWcomment {font-size:80%; font-weight:normal}
.bold {font-weight:bold}
.notbold {font-weight:normal}

.text-nadpis {font-weight: bold; font-size: 1em;}
.text-nadpis-koment {font-weight: normal; font-size: 0.9em;}
.text-komentar {font-weight: normal; font-size: 0.85em;}
.povinne {color: #FF0000; font-weight: bolder;}
TEXTAREA.textarea {width: 470px; border: 1px solid #888888;}
INPUT.input-text {border: 1px solid #888888; padding: 1px;}
.text-chyba {color: #FF0000; font-size: 1.1em; font-weight: bold; margin-bottom: 3px;}
.submit {color: #FFFFFF; font-family: verdana; FONT-SIZE: 12px; FONT-WEIGHT: bold; background-color: #2266bb; border-color: #2266bb;}

FORM INPUT.input-text-povinne {border: 10px solid #888888; padding: 1px;}

.BoxSortBy {}
.BoxSortBy .selected {
	text-decoration: underline;
	color: <%=MyIni.Colors.AHref%>;}
div.ListPagesBox {
	margin-top: 7px;
	border-top: 1px dotted <%=MyIni.Colors.Box2Border%>; border-bottom: 1px dotted <%=MyIni.Colors.Box2Border%>;
	padding-left: 2px; padding-top: 1px; padding-bottom: 1px;}

.table2 {
	border: 1px solid <%=MyIni.Colors.Box2Border%>;}
.Table2Title { background-color: <%=MyIni.Colors.Box2Title%>;}

.FormErrors {
	background-color: <%=MyIni.Colors.Box1Bg%>;
	color: #FF0000;
	padding: 2px;
	margin-bottom: 4px;
	border: solid 1px Red; }

/* Pro více stránek */
#hPageTitle {
	background-color: <%=MyIni.Colors.Box1Bg%>;
	border: 1px solid <%=MyIni.Colors.Box1Border%>;
	margin-bottom: 10px;
	text-align: center;
	font-weight: normal;
	clear: both; }

/* Jednotlivé stránky x*/
#pnlAutoriSeznamMenu {text-align: center;}


<asp:PlaceHolder id="phBillboard" runat="Server" Visible="False">
html#bbtPage body .bbtext .bbtWord.double,
html body .bbtext .bbtWord.double
{
	color: #FFF0D0!important;
	border-bottom: 2px dotted!important;
	text-decoration: none!important;
	font-weight: bold;
}
</asp:PlaceHolder>