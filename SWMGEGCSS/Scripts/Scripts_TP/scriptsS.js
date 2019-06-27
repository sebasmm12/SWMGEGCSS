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
    function ValidarDatos() {
        var user = document.getElementById("username");
        var $password = $("#password");
        if (user.value === null || user.length === 0 || user.value === " " || user.value === "") {
            return false;
        }
        if ($password.val() === null || $password.val() === "" || $password.val()=== " ") {
            return false;
        }
        return true;
    }
    function ValidarBd() {
        var $form = $("#form");
        var options = {
            url: form
        };
        
    }
    $("#btnlogin").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('rubberBand');
        });
    });
    $("#btnlogin").click(ValidarDatos);
};