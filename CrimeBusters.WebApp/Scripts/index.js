var userCoords = [];
var hiMarkers = [];
var loMarkers = [];

$(function () {
	var map = $.getMap();
	$.plotUsersOnMap(map);

	$(document).on("click", "a[data-pictureurl]", function (e) {
	    e.preventDefault();
	    
	    var pictureUrl = $(this).attr("data-pictureurl");
	    $("img", "#uploadedImageWindow").attr("src", pictureUrl.substring(2));

	    $("#uploadedImageWindow").dialog({
	        title: "Uploaded Image",
	        show: "fade",
	        hide: "clip",
	        modal: true,
	        width: "335px"
	    });
	});

	$("a[data-reporttype]", "ul.dropdown-menu").on("click", function(e) {
	    e.preventDefault();
	    var reportType = $(this).attr("data-reporttype");
        $.updateMapClientSide(map, reportType);
    });

	$("#signOut").on("click", function (e) {
	    e.preventDefault();
	    $.logOffUser();
	});
});

(function($) {
	$.getMap = function() {
		var mapOptions = {
				zoom: 15,
				mapTypeId: google.maps.MapTypeId.ROADMAP,
				center: new google.maps.LatLng(40.099876, -88.227857)
		};
			
		return new google.maps.Map($("#map").get(0), mapOptions);
	};
	
	$.plotUsersOnMap = function (map) {
	    var coords = [];
	    var location;

	    $.ajax({
	        type: "POST",
	        dataType: "json",
	        timeout: 10000,
	        contentType: "application/json",
	        url: "../Services/Index.asmx/GetReports",
	        success: function (data) {
	            $.each(data.d, function () {
	                // Used to workaround the issue when 2 markers are on the same position.
	                var lat = this.Latitude;
	                var lng = this.Longitude;
	                var hash = lat + lng;

	                hash = hash.replace(/\./g, "").replace(",", "").replace("-", "");

	                // check to see if we've seen this hash before
	                if (userCoords[hash] == null && coords[hash] == null) {
	                    location = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
	                    // store an indicator that we've seen this point before
	                    coords[hash] = 1;
	                } else {
	                    // add some randomness to this point
	                    var newLat = parseFloat(lat) + (Math.random() - .5) / 1500;
	                    var newLong = parseFloat(lng) + (Math.random() - .5) / 1500;

	                    // get the coordinate object
	                    location = new google.maps.LatLng(newLat.toFixed(6), newLong.toFixed(6));
	                }

	                var marker = new google.maps.Marker({
	                    position: location,
	                    map: map,
	                    title: this.User.UserName,
                        icon: this.MarkerImage,
	                    animation: google.maps.Animation.BOUNCE
	                });

	                if (this.ReportType == "HIGH") {
	                    hiMarkers.push(marker);
	                } else {
	                    loMarkers.push(marker);
	                }

	                // Shows the local time in the browser.
	                // workaround for Safari
	                var s = (this.TimeStampString + "Z").split(/[^0-9]/);
	                var tst = new Date(s[2], s[0] - 1, s[1], s[3], s[4], s[5], 0); 
	                var offset = -((new Date()).getTimezoneOffset() / 60);
	                tst.setHours(tst.getHours() + offset);

	                var content = "<div id='markerPopup'>" + this.User.FirstName + " " + this.User.LastName + " needs help!" +
                                        "<h3>User Details</h3>" +
                                        "<ul>" +
                                            "<li>Report Type: " + this.ReportType + "</li>" +
                                            "<li>Message: " + this.Message + "</li>" +
                                            "<li>Date Reported: " + tst + "</li>" +
                                            "<li>Gender: " + this.User.Gender + "</li>" +
                                            "<li>Email: " + this.User.Email + "</li>" +
                                            "<li>Phone Number: " + this.User.PhoneNumber + "</li>" +
                                            "<li>Address: " + this.User.Address + "</li>" +
                                            "<li>Zip Code: " + this.User.ZipCode + "</li>" +
                                            "<li>Current Location: " + marker.getPosition().toString() + "</li>" +
                                        "</ul>" +
                                  "</div>";

	                if (this.ResourceUrl != "") {
	                    content += "<a data-pictureurl='" + this.ResourceUrl
                            + "' href='#'>View Uploaded Picture</a>";
	                }

	                $.attachInfo(map, content, marker);
	            });
	        },
	        error: function () {
	            alert("error");
	        }
	    });
	};
	
	var infoWindow = new google.maps.InfoWindow({
	    maxWidth: 500,
	    disableAutoPan: false
	});
	$.attachInfo = function (map, content, marker) {
	    google.maps.event.addListener(marker, "click", function (e) {
	        infoWindow.setContent(content);
	        infoWindow.open(map, marker);
	        marker.setAnimation(null);
	    });
	};

    $.updateMapClientSide = function(map, reportType) {
        switch (reportType) {
            case "high":
                $.showMarkers(map, hiMarkers);
                $.clearMarkers(loMarkers);
                break;
            case "low":
                $.showMarkers(map, loMarkers);
                $.clearMarkers(hiMarkers);
                break;
            default:
                $.showMarkers(map, hiMarkers);
                $.showMarkers(map, loMarkers);
                break;
        }
    };

    $.showMarkers = function(map, markers) {
        for (var i in markers) {
            markers[i].setMap(map);
        }
    };

    $.clearMarkers = function(markers) {
        for (var i in markers) {
            markers[i].setMap(null);
        }
    };

    $.logOffUser = function () {
	    $.ajax({
	        type: "POST",
	        dataType: "json",
	        timeout: 10000,
	        contentType: "application/json",
	        url: "../Services/Login.asmx/LogOutUser",
	        success: function (data) {
	            window.location.href = 'Login.aspx';
	        },
	        error: function () {
	            alert("Unable to communicate with the server. Please try again.");
	        }
	    });
	};
})(jQuery);



