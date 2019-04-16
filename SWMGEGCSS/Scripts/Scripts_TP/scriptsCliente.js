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
    /*$(".nav-item").mouseenter(function () {
        $(".nav-item").animateCss('pulse');
    });*/
};