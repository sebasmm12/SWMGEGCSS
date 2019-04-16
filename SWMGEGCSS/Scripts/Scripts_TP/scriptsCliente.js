window.onload = function () {
    $.fn.extend({
        animateCss: function (animationName, callback) {
            var animationEnd = (function (el) {
                var animations = {
                    animation: 'animationend',
                    OAnimation: 'oAnimationEnd',
                    MozAnimation: 'mozAnimationEnd',
                    WebKitAnimation: 'webkitAnimationEnd'
                };
                for (var t in animations) {
                    if (el.style[t] !== undefined) {
                        return animations[t];

                    }
                }
            })(document.createElement('div'));
            this.addClass('animated ' + animationName).one(animationEnd, function () {
                $(this).removeClass('animated ' + animationName);
                if (typeof callback === 'function') callback();
            });
            return this;
        }
    });
    $(".nombre_imagen").animateCss('zoomInLeft');
    $(".nombre_imagen").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('tada');
        });
    });

    $(".nav-item").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('pulse');
        });
    });
    $(".float_left").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('tada');
        });
    });
    $(".link_footer").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('pulse');
        });
    });
    $(".fab").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('heartBeat');
        });
    });
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(OnSuccess, OnError, {
            enableHighAccuracy: true,
            maximumAge: 1000,
            timeout: 500
        });
    } else {
        document.getElementById("map").innerHTML = "No soporta la geolocaclización, no pudimos encontrarlo";
    }

    /*$(".nav-item").mouseenter(function () {
        $(".nav-item").animateCss('pulse');
    });*/
};
function OnSuccess(position) {
    showMap(
        position.coords.latitude,
        position.coords.longitude
    );
}
function OnError(error) {
    var mapDiv = document.getElementById("map");
    switch (error.code) {
        case error.PERMISSION_DENIED:
            mapDiv.innerHTML = "User denied the geolocation request."
            break;
        case error.POSITION_UNAVAILABLE:
            mapDiv.innerHTML = "No position information unavailable."
            break;
        case error.TIMEOUT:
            mapDiv.innerHTML = "A timeout occurred."
            break;
        case error.UNKNOW_ERROR:
            mapDiv.innerHTML = "An unknown error occurred."
            break;
    }
}
function showMap(lat, lang) {
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer();
    var route = {
        origin: new google.maps.LatLng(lat, lang),
        destination: "GEO CONSULT SERVICE SAC, San Juan de Miraflores",
        travelMode: google.maps.DirectionsTravelMode.DRIVING
    };
    var mapOptions = {
        zoom: 10,
        center: new google.maps.LatLng(lat, lang),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
    directionsRenderer.setMap(map);
    directionsRenderer.setPanel(document.getElementById("driving-directions"));
    directionsService.route(route, function (result, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsRenderer.setDirections(result);
        }
    });

}