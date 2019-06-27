$(function () {
    var id_proyecto = 0;
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
    $("#Buscar").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('pulse');
        });
    });
    $("#Registrar_Empresa").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('pulse');
        });
    });
    $(".actualizar_empresa").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('tada');
        });
    });
    $(".btnModal").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('tada');
        });
    });
    var getPage = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
            $(".btnModal").each(envioajaxModal); 
            $(".actualizar_empresa").each(function () {
                $(this).mouseenter(function () {
                    $(this).animateCss('tada');
                });
            });
            $(".btnModal").each(function () {
                $(this).mouseenter(function () {
                    $(this).animateCss('tada');
                });
            });
        });
        return false;
    };

    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);
    };

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_proyecto = $(this).attr("data-id-proyecto");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { ruc: $(this).attr("data-ruc-empresa") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
            });
            return false;
        });
    };

    var BuscarProyecto = function () {
        var $searchTerm = $('#searchTerm').val();
        var $select = $('#estado').val();
        $.ajax({
            url: "/Gerente/Gestionar_Empresas",
            data: { searchTerm: $searchTerm, estado: $select },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("div.pagedList").attr("data-exp-target");
            $(target).replaceWith($newhtml);
            $(".btnModal").each(envioajaxModal);
        });
        return true;
    };




    $(".btnModal").each(envioajaxModal);    
    $("input[data-exp-autocomplete]").each(autocompletado);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $("#Buscar").click(BuscarProyecto);
});

