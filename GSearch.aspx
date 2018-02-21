<%@ Page Title="Hledání" Language="VB" AutoEventWireup="false" CodeFile="GSearch.aspx.vb" Inherits="_GSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body id="body" runat="server">








<style type="text/css">
@import url(http://www.google.com/cse/api/branding.css);
</style>
<div class="cse-branding-bottom">
  <div class="cse-branding-form">
    <form action="http://liter.cz/GSearch.aspx" id="cse-search-box">
      <div>
        <input type="hidden" name="cx" value="partner-pub-5102465567837043:sh5wnp-3unh" />
        <input type="hidden" name="cof" value="FORID:10" />
        <input type="hidden" name="ie" value="UTF-8" />
        <input type="text" name="q" size="30" />
        <input type="submit" name="sa" value="Hledat" />

      </div>
    </form>
  </div>
  <div class="cse-branding-logo">
    <img src="http://www.google.com/images/poweredby_transparent/poweredby_FFFFFF.gif" alt="Google" />
  </div>
  <div class="cse-branding-text">
    Vlastn&#237; vyhled&#225;v&#225;n&#237;
  </div>
</div>

 
 



<div id="cse-search-results"></div>
<script type="text/javascript">
	var googleSearchIframeName = "cse-search-results";
	var googleSearchFormName = "cse-search-box";
	var googleSearchFrameWidth = 800;
	var googleSearchDomain = "www.google.cz";
	var googleSearchPath = "/cse";
</script>
<script type="text/javascript" src="http://www.google.com/afsonline/show_afs_search.js"></script>
 
 
	
</body>
</html>