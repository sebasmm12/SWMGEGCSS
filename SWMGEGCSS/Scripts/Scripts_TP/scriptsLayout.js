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
    $("#close").click(function () {
        localStorage.removeItem("key");
        localStorage.setItem("key", 0);
        return true;
    });
    var condicion = localStorage.getItem("key");
    if (condicion !== '1') {
        $(".alerta_servicio").animateCss('bounceInLeft', function () {

        });
        $("#cerrar_alerta").click(function () {
            $(".alerta_servicio").animateCss('bounceOutRight', function () {
                $(".alerta_servicio").addClass("cerrar_alerta");
            });
            var x = 1;
            localStorage.setItem("key", x);
        });
    } else {
        $(".alerta_servicio").addClass("cerrar_alerta");
    }
   
};