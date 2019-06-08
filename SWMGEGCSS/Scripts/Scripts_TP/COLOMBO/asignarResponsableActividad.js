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
                url: "/ActividadesDesarrollar/_ModalAsignarTrabajadorResponsable",
                method: "POST",
                data: { usu_codigo: usu_cod }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnRegistrarResponsable").click(envioajaxAgregar);
            });
            
        });
    };
    var envioajaxAgregar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto
        
        $.ajax({
            url: "/ActividadesDesarrollar/_ListaDetalleUsuario",
            method: "POST",
            data: {
                usu_codigo: usu_cod
            }
        }).done(function (data) {
            //window.location.href = "/ActividadesDesarrollar/_ListaDetalleUsuario";
            //$("#AsignarTrabajadorActividad").modal('hide');
            var $newhtml = $(data);
            var target = document.getElementById("tableDetUsu");
            $(target).replaceWith($newhtml);
            $("#AsignarTrabajadorActividad").modal("hide");
        });
    };

    $(".btnModalTrabajador").each(envioajaxModal);
    $(".btnRegistrarResponsable").click(envioajaxAgregar);
});