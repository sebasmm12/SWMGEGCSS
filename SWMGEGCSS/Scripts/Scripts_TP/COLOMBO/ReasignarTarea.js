$(function () {

    var Alerta = function () {
        $(this).click(function () {

            (async function getEmail() {
                const { value: comentario } = await Swal.fire({
                    title: 'Descripcion de la Asignacion',
                    input: 'textarea',
                    inputPlaceholder: 'Ingrese un comentario',
                    showCancelButton: true,
                    inputValidator: (value) => {
                        if (!value) {
                            return 'Este campo es obligatorio';
                        }
                    }
                });

                if (comentario) {
                    EnvioComentario(comentario);
                }
            })();

        });
    };

    var EnvioComentario = function (comentario) {
        $.ajax({
            url: "/ActividadesDesarrollar/RegistrarAuditoriaActividadDesarrollarFinal",
            method: "GET",
            data: {
                comentario: comentario
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            window.location.href = "/Administrador/Asignacion_Tareas";
        });
        return false;
    };

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            var act_desa_aux = {
                act_desa_fecha_inicio: $("#actDesaFechaInicio2").val(),
                act_desa_fecha_fin: $("#actDesaFechaFin2").val()
            };
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: {
                    actividadesDesarrollarAux: act_desa_aux
                }
            }).
                done(function (data) {
                    
                    var target = $button.attr("data-id-target");
                    var $newhtml = $(data);
                    $(target).replaceWith($newhtml);
                    $(modal).modal();
                    $(modal).on('shown.bs.modal', function () {
                        $(document).off('focusin.modal');
                    });
                    $(".btnActualizarActDesa").each(Alerta);
                });
            return false;
        });
    };
    /*var envioajaxRegistrar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto 
        
            $.ajax({
                url: "/Plan/_ModalRegistrarActividadesPlanificadasFinal",
                method: "POST"
            }).done(function (data) {
                window.location.href = "/Administrador/Asignacion_Tareas";
            });
    };*/

    $(".btnModalActividadesDesarrollar2").click(envioajaxModal);
    //$("#btnAsignarTarea").click(registroAsignacion);
});