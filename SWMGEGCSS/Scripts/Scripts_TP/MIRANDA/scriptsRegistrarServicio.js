$(function () {
    var modal;
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
            $(".btnRegistrar").each(registrarActividadParcial);
            });
        return true;
    };
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            modal = $button.attr("data-id-target");
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
                $(".btnRegistrar").each(registrarActividadParcial);
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
                $(".btnRegistrar").each(registrarActividadParcial);
            });
        });
        
        return true;
    };
    var registrarActividadParcial = function () {
        $(this).click(function () {
            var $button = $(this);
            let params = new URLSearchParams(location.search);
            var page = params.get('page');
            var actividad = {
                act_id : $button.attr("data-id-act"),
                act_nombre: document.getElementById("actividades_act_nombre").value,
                act_descripcion: document.getElementById("actividades_act_descripcion").value,
                costo: document.getElementById("actividades_costo").value,
                act_obligatorio: document.getElementById("est_actividad").value
            };
            $.ajax({
                url: "/Servicios/ModificarDatosActividad",
                data: {
                    actividad: actividad,
                    page: page
                },
                type: "POST"

            }).done(function (data) {
                var $newhtml = $(data);
                var target = document.getElementById("tableAct");
                $(target).replaceWith($newhtml);
                $(modal).modal('hide');
                $("#btnRegistrarActividad").click(añadir);
                $(".btnModal").each(envioajaxModal);
                $(".btnEliminar").each(eliminarActividad);
            });
        });
    };
    var registrarServicioCompleto = function () {
        var servicio = {
            tipo_servicio_nombre: document.getElementById("tipoServicio_tipo_servicio_nombre").value,
            tipo_servicio_descripcion: document.getElementById("tipoServicio_tipo_servicio_descripcion").value
        };
        $.ajax({
            url: "/Servicios/RegistrarServicioActividad",
            data: {
                servicio: servicio
            },
            type: "POST"

        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'Se registro el servicio exitosamente',
                confirmButtonText: 'OK'

            }).then((result) => {
                if (result.value) {
                    window.location.href = "/Servicios/VisualizarServicios";
                }
            });   
        });
    };

    $("#btnRegistrarActividad").click(añadir);
    $(".btnModal").each(envioajaxModal);
    $(".btnEliminar").each(eliminarActividad);
    $(".btnRegistrar").each(registrarActividadParcial);
    $("#btnRegistrarServicio").click(registrarServicioCompleto);
});