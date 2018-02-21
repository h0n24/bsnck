var n = (document.layers) ? 1:0;
var ie = (document.all) ? 1:0;

function cursorInit(){
	oMove=new makeCursorObj('divhejbejKurzor','divKurzorimidz')
	oContainer=new makeCursorObj('divKurzorimidz')
	if(n)document.captureEvents(Event.MOUSEMOVE)
	document.onmousemove=move;
}
function makeCursorObj(obj,nest){
	nest=(!nest) ? '':'document.'+nest+'.'										
	this.css=(n) ? eval(nest+'document.'+obj):eval('document.all.'+obj+'.style')		
	this.x=(n)? this.css.left:this.css.pixelLeft;
	this.y=(n)? this.css.top:this.css.pixelTop;														
	this.moveIt=b_moveIt; this.moveBy=b_moveBy;																			
	return this
}
function b_moveIt(x,y){
	this.x=x; this.y=y
   	this.css.left=this.x
	this.css.top=this.y
}
function b_moveBy(x,y){
	this.x=this.x+x; this.y=this.y+y
   	this.css.left=this.x
	this.css.top=this.y
}
var tim;
var tim2
var cnum=0
function move(e){
	clearTimeout(tim2)
	clearTimeout(tim)
	x=n?e.pageX:event.x
	y=n?e.pageY:event.y
	cnum++
	if(cnum==5)cnum=1
	if(cnum==1) {oldx=x; oldy=y}
	oContainer.moveIt(x+10,y+10)
	if(oMove.x<-5){
		oMove.moveBy(5,0)
	}
	tim2=setTimeout("moveIt()",100)
}

function moveIt(){
	if(oMove.x>-430){
		oMove.moveBy(-5,0)
		tim=setTimeout("moveIt()",20)
	}
}
onload=cursorInit;
