$(function () {
    var usu_cod = 0;
    var act_desa_id = 0;
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList1").attr("data-exp-target");
            $(target).replaceWith(data);
            $(".btnModalTrabajador").each(envioajaxModal);
            $(".btnRegistrarResponsable").click(envioajaxAgregar);
        });
        return false;
    };
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
                $(".btnRegistrarResponsable").click(envioajaxAgregar);
            });
            return false;
        });
    };
    var envioajaxAgregar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto

        $.ajax({
            url: "/ActividadesDesarrollar/AsignarTrabajadorResponsableFinal",
            method: "POST",
            data: {
                usu_codigo: usu_cod
            }
        }).done(function (data) {
            window.location.href = "/ActividadesDesarrollar/RegistrarAsignacionActividades_Desarrollar?act_desa_id=" + act_desa_id;
        });
    };

    $(".btnModalTrabajador").each(envioajaxModal);
    $(".btnRegistrarResponsable").click(envioajaxAgregar);
    $(".pcoded-content").on("click", ".pagedList1 a", getPage);
});