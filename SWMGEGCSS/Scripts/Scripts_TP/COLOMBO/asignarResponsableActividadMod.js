$(function () {
    var usu_cod = 0;
    var act_desa_id = 0;
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            usu_cod = $(this).attr("data-id-usu");
            act_desa_id = $(this).attr("data-act-desa-id");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { usu_codigo: $(this).attr("data-id-usu") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnRegistrarResponsableMod").click(envioajaxAgregar);
            });
            return false;
        });
    };
    var envioajaxAgregar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto

        $.ajax({
            url: "/ActividadesDesarrollar/AsignarTrabajadorResponsableFinalMod",
            method: "POST",
            data: {
                usu_codigo: usu_cod
            }
        }).done(function (data) {
            window.location.href = "/ActividadesDesarrollar/ActualizarAsignacionActividades_Desarrollar?act_desa_id=" + act_desa_id;
        });
    };

    $(".btnModalTrabajadorMod").each(envioajaxModal);
    $(".btnRegistrarResponsableMod").click(envioajaxAgregar);
});