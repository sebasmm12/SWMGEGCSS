$(function () {
    var añadir = function () {
        var $button = $(this);
        var selector = document.getElementById("actividad");
        $.ajax({
            url: $button.attr("data-url"),
            data: {
                actividad: selector.value
            },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = document.getElementById("tableAct");
            $(target).replaceWith($newhtml);
            $(".btnModal").each(envioajaxModal);
            $(".btnEliminar").each(eliminarActividad);
            });
        return true;
    };
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            $.ajax({
                url: $button.attr("data-url"),
                data: {
                    id: $button.attr("data-id-actividad")
                },
                type: "POST"
            }).done(function (data) {
                var $newhtml = $(data);
                var $target = $($button.attr("data-id-target"));
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnEliminar").each(eliminarActividad);
            });
        });
        return true;
    };
    var eliminarActividad = function () {
        $(this).click(function () {
            var $button = $(this);
            $.ajax({
                url: $button.attr("data-url"),
                data: {
                    id: $button.attr("data-id-actividad")
                },
                type: "GET"

            }).done(function (data) {
                var $newhtml = $(data);
                var target = document.getElementById("tableAct");
                $(target).replaceWith($newhtml);
                $(".btnModal").each(envioajaxModal);
                $(".btnEliminar").each(eliminarActividad);
            });
        });
        
        return true;
    };

    $("#btnRegistrarActividad").click(añadir);
    $(".btnModal").each(envioajaxModal);
    $(".btnEliminar").each(eliminarActividad);
});