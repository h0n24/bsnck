// Version 20.07.2000

var IE4 = (document.all) ? 1 : 0;		// Just the "Mickey Mouse" Browser?
var NS4 = (Math.round(parseFloat(navigator.appVersion) * 1000) >= 4500) ? 1 : 0;

var ver4 = (NS4 || IE4) ? 1 : 0;

if (ver4)			// Begin Code Browser Version >= 4
{


var x, y, 		// Initial Screenpos. of "sticky" Object
    sticky,		// Pointer to current used "sticky" Object
    interval,		// Timer Handle
    t_time = 100,	// 100 ms Timer Interval
    sWidth = 115,	// adSticky / Layer max. Width in Pixel
    sHeight = 180;	// adSticky / Layer max. Height in Pixel

// document.write ("Body: Javascript " + (ver4 ? "(4er Browser) " : "") + "ist eingeschaltet<P>");

if (IE4)
{
	document.write("<DIV align=left id='sticky' style='POSITION:Absolute;background-color:none;top:0;left:0;WIDTH:115;HEIGHT:180;display:none;visibility:hidden;'>\n");
	document.write("<IMG SRC='" + ad_vollbanner + "' WIDTH=115 HEIGHT=180 BORDER=0 USEMAP='#StickyMap'><BR>\n");
	document.write("<MAP NAME='StickyMap'>\n");
	document.write("<AREA SHAPE='rect' COORDS='0,0,115,165' HREF='" + ad_linkurl + "' TARGET=_blank>\n");
	document.write("<AREA SHAPE='rect' COORDS='0,166,115,180' HREF='javascript:FooterClick();'>\n");
	document.write("</MAP>\n");
	document.write("</DIV>\n");

	document.write("<DIV align=left id='stickyon' style='POSITION:Absolute;background-color:none;top:0;left:0;WIDTH:94;HEIGHT:20;display:none;visibility:hidden;'>\n");
	document.write("<A HREF='javascript: FooterOpen();'><IMG SRC='" + ad_keinbanner + "' height=20 width=94 border=0><BR></A>\n");
	document.write("</DIV>\n");

	sticky = document.all["sticky"];
	x = document.body.clientWidth - sWidth - 5;
	y = 130;

	sticky.style.top = y; sticky.style.left = x;
	sticky.style.visibility = 'visible'; sticky.style.display = '';
}

else
{
	// "Layers" are nicer with NS 4 as "CSS" are

//	document.write("<style type='text/css'>\n");
//	document.write("#sticky {position:absolute; left:0; top:0; z-index:1; visibility:hidden}\n");
//	document.write("#stickyon {position:absolute; left:0; top:0; z-index:2; visibility:hidden}\n");
//	document.write("</style>\n");

//	document.write("<DIV align=left id='sticky' style='POSITION:Absolute;background-color:none;top:50;left:50;'>\n");
	document.write("<LAYER ID='sticky' LEFT=0 TOP=0 VISIBILITY=HIDDEN>\n");
	document.write("<IMG SRC='" + ad_vollbanner + "' WIDTH=115 HEIGHT=180 BORDER=0 USEMAP='#StickyMap'><BR>\n");
	document.write("<MAP NAME='StickyMap'>\n");
	document.write("<AREA SHAPE='rect' COORDS='3,0,114,158' HREF='" + ad_linkurl + "' TARGET=_blank>\n");
	document.write("<AREA SHAPE='rect' COORDS='3,159,114,176' HREF='javascript:FooterClick();'>\n");
	document.write("</MAP>\n");
//	document.write("</DIV>\n");
	document.write("</LAYER>\n");


//	document.write("<DIV align=left id='stickyon' style='POSITION:Absolute;background-color:none;top:0;left:0;WIDTH:115;HEIGHT:26;display:none;visibility:hidden;'>\n");
	document.write("<LAYER ID='stickyon' LEFT=0 TOP=0 VISIBILITY=HIDDEN>\n");
	document.write("<A HREF='javascript:FooterOpen();'><IMG SRC='" + ad_keinbanner + "' height=20 width=94 border=0><BR></A>\n");
//	document.write("</DIV>\n");
	document.write("</LAYER>\n");

	sticky = document.layers["sticky"];	
	x = window.innerWidth - sWidth - 20;
	y = 10;
	sticky.moveToAbsolute(x, y);
	sticky.visibility = 'visible';
}

// Set Event Handler
if (NS4)
{

//	window.captureEvents(Event.DRAGDROP | Event.RESIZE);
	window.captureEvents(Event.RESIZE);
//	window.onDragDrop = DragDropHandler;	
	window.onResize = ResizeHandler;
}

else
{
/*	
	document.ondragstart = DragDropHandler;
	document.ondrag = DragDropHandler;
	document.ondragend = DragDropHandler;
	window.onResize = ResizeHandler;
*/
}

	
// activate moving sticky object every <t_time> ms
interval = window.setInterval("move_sticky()", t_time);


function move_sticky()
{
	var xpos, ypos;

	
	// disable timer interval
	window.clearInterval(interval);

	// move sticky object to current position after scrolling
	if (NS4)
	{
//		xpos = x + window.pageXOffset;
		xpos = window.innerWidth - sWidth - 20;
		ypos = y + window.pageYOffset;
		sticky.moveToAbsolute(xpos, ypos);
	}
	else
	{
		xpos = document.body.clientWidth - sWidth - 10;
//		xpos = x + document.body.scrollLeft;
		ypos = y + document.body.scrollTop;
		sticky.style.top = ypos;
		sticky.style.left = xpos;
	}

	// activate timer again but with current values
	interval = window.setInterval("move_sticky()", t_time);
}

function FooterClick()
{
	var xpos, ypos;


	if (NS4)
	{
		document.layers["sticky"].visibility = 'hidden';	// hidden adBanner
		document.layers["stickyon"].moveToAbsolute(x, y);	// repositioning "on" Banner
		document.layers["stickyon"].visibility = 'visible';	// show it
		sticky = document.layers["stickyon"];			// set pointer to "on" Banner
	}

	else
	{
		xpos = document.body.clientWidth - sWidth - 10;
		ypos = y + document.body.scrollTop;

		document.all["sticky"].style.visibility = 'hidden';
		document.all["stickyon"].style.top = ypos;
		document.all["stickyon"].style.left = xpos;
		document.all["stickyon"].style.visibility = 'visible';
		document.all["stickyon"].style.display = '';
		sticky = document.all["stickyon"];
	}
}

function FooterOpen()
{
	var xpos, ypos;


	if (NS4)
	{
		document.layers["stickyon"].visibility = 'hidden';	// hidden "on" Banner
		document.layers["sticky"].moveToAbsolute(x, y);		// repositioning adBanner
		document.layers["sticky"].visibility = 'visible';		// show it
		sticky = document.layers["sticky"];			// set pointer to adBanner
	}

	else
	{
		xpos = document.body.clientWidth - sWidth - 10;
		ypos = y + document.body.scrollTop;

		document.all["stickyon"].style.visibility = 'hidden';
		document.all["sticky"].style.top = ypos;
		document.all["sticky"].style.left = xpos;
		document.all["sticky"].style.visibility = 'visible';
		document.all["sticky"].style.display = '';
		sticky = document.all["sticky"];
	}
}

function DragDropHandler(e)
{
	var xpos, ypos;

	window.status = (NS4) ? e.target + " x: " + e.pageX + " y: " + e.pageY
		: window.event.type + " x: " + window.event.clientX + " y: " + window.event.clientY;

	//	alert("DragDrap Event: x = " + e.pageX + ", y = " + e.pageY);
	//	xpos = sticky.clip.width / 2;
	//	ypos = sticky.clip.height / 2;

	//	sticky.moveToAbsolute(e.pageX - xpos, e.pageY - ypos);
		
	if (NS4)
	{
		x = e.pageX; y = e.pageY;
	}
	else
	{
		x = window.event.clientX; y = window.event.clientY;
	}

	return false;
}

function ResizeHandler(e)
{
//	window.status = "RESIZE Event";
		
	if (NS4)
	{
	/*
		// sticky = document.layers["sticky"];	
		x = window.innerWidth - sWidth - 20;
		y = 10;
		sticky.moveToAbsolute(x, y);
		sticky.visibility = 'visible';
	*/
		window.location.reload();
		
//		document.releaseEvents(Event.RESIZE);
//		window.clearInterval(interval);
//		window.location.reload();
//		sticky.moveToAbsolute(x, y);
//		sticky.visibility = 'show';
    }
	
	return false;
}

}	// Ende Code Browser Version >= 4
