var spriteWidth = 200;
var spriteHeight = 38;
var spriteSpeed = 2;

var maxSpriteSpeed = 50;
var xMax;
var yMax;
var xPos = 0;
var yPos = 0;
var xDir = 'right';
var yDir = 'down';
var spriteRunning = true;
var tempSpriteSpeed;
var currentBallSrc;
var newXDir;
var newYDir;

setTimeout('Stopit()',30000);

function Stopit() {
  if (document.all) {
    xMax = document.body.clientWidth
    yMax = document.body.clientHeight
    document.all("LetObr").style.visibility = "hidden";
  }
  else if (document.layers) {
    xMax = window.innerWidth;
    yMax = window.innerHeight;
    document.layers["LetObr"].visibility = "hidden";
  }
}

function initializeSprite() {
   if (document.all) {
      xMax = document.body.clientWidth
      yMax = document.body.clientHeight
      document.all("LetObr").style.visibility = "visible";
      }
   else if (document.layers) {
      xMax = window.innerWidth;
      yMax = window.innerHeight;
      document.layers["LetObr"].visibility = "show";
      }
   setTimeout('moveSprite()',500);
   }

function moveSprite() {
   if (spriteRunning == true) {
      calculatePosition();
      if (document.all) {
         document.all("LetObr").style.left = xPos + document.body.scrollLeft;
         document.all("LetObr").style.top = yPos + document.body.scrollTop;
         }
      else if (document.layers) {
         document.layers["LetObr"].left = xPos + pageXOffset;
         document.layers["LetObr"].top = yPos + pageYOffset;
         }
      setTimeout('moveSprite()',20);
      }
   }

function calculatePosition() {
   if (xDir == "right") {
      if (xPos > (xMax - spriteWidth - spriteSpeed)) { 
         xDir = "left";
         }
      }
   else if (xDir == "left") {
      if (xPos < (0 + spriteSpeed)) {
         xDir = "right";
         }
      }
   if (yDir == "down") {
      if (yPos > (yMax - spriteHeight - spriteSpeed)) {
         yDir = "up";
         }
      }
   else if (yDir == "up") {
      if (yPos < (0 + spriteSpeed)) {
         yDir = "down";
         }
      }
   if (xDir == "right") {
      xPos = xPos + spriteSpeed;
      }
   else if (xDir == "left") {
      xPos = xPos - spriteSpeed;
      }
   else {
      xPos = xPos;
      }
   if (yDir == "down") {
      yPos = yPos + spriteSpeed;
      }
   else if (yDir == "up") {
      yPos = yPos - spriteSpeed;
      }
   else {
      yPos = yPos;
      }
   }

if (document.all||document.layers)
window.onload = initializeSprite;