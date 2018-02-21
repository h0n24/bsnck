<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="Geocaches_Map.aspx.vb" Inherits="Geocaches_Map" %>

<html>
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
	<link rel="SHORTCUT ICON" href="/gfx/logo/geofinalcom.ico" />
	<title>Map</title>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDtxD-2s2nrt2F8xSP_01XI62pw_0anuf0&sensor=true"></script>

<asp:Literal ID="litJavaScript" runat="server" />

<script type="text/javascript">
	var map;
	function initialize() {
		var myOptions = {
			zoom: 5,
			center: centerLatlng,
			mapTypeControlOptions: {style: google.maps.MapTypeControlStyle.DROPDOWN_MENU},
			mapTypeId: google.maps.MapTypeId.ROADMAP
		}
		map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
//		var southWest = new google.maps.LatLng(49, 16);
//		var northEast = new google.maps.LatLng(53, 20);
//		var bounds = new google.maps.LatLngBounds(southWest, northEast);
//		map.fitBounds(bounds);
//		var lngSpan = northEast.lng() - southWest.lng();
//		var latSpan = northEast.lat() - southWest.lat();
		for (var i = 0; i < Geocaches.length; i++) {
			var location = new google.maps.LatLng(Geocaches[i][1], Geocaches[i][2]);
			var marker = new google.maps.Marker({
				position: location,
				map: map
			});
			var j = i + 1;
			marker.setTitle(Geocaches[i][0]);
			setInfoWindow(marker, i);
		}
	}

	function RefreshGeocaches() {
		var newCenter = map.getCenter();
		window.location = '?lat=' + newCenter.lat() + '&lon=' + newCenter.lng()
	}
	function setInfoWindow(marker, index) {
		var GC = Geocaches[index]
		var html = '<div id="MapBalloon">' +
				'<div id="BalloonCode"><a href="Geocache.aspx?code=' + GC[0] + '"><b>' + GC[0] + '</b></a><br/>' + GC[3] + '</div>' +
				'<div id="BalloonInfo"></div>' +
				'</div>';
		var infowindow = new google.maps.InfoWindow({
			content: html
		});
		google.maps.event.addListener(marker, 'click', function() {
			infowindow.open(map, marker);
		});
	}
	
</script>
</head>
<body style="margin:0px; padding:0px;" onload="initialize()">
	<div style="background-color:#DDFFDD">Limited to 200 geocaches from <asp:Label ID="lblCenter" runat="server" />
		<input type="button" title="Update Coordinates and Geocaches" value="Load Geocaches here" style="margin-left:10px;" onclick="RefreshGeocaches();"/>
	</div>
	
	<div id="map_canvas" style="width:100%; height:95%;"></div>

</body>
</html>