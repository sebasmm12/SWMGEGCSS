$(function () {
    var EnviarSolicitud = function () {
        var Solicitud = {
            names: $("#Solicitud_names").val(),
            correoE: $("#Solicitud_correoE").val(),
            asunto: $("#Solicitud_asunto").val(),
            mensaje: $("#Solicitud_mensaje").val()
        };
        $.ajax({
            url: "/Client/InsertarSolicitud",
            data: {
                Solicitud: Solicitud
            },
            type:"GET"
        }).done(function (data) {
            if (data === 1) {
                Swal.fire({
                    type: 'success',
                    title: 'Se envio su solicitud exitosamente',
                    confirmButtonText: 'OK'

                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Client/Contactenos";
                    }
                });
            }
            });
        return false;
    };
    $("#solicitud").click(EnviarSolicitud);
});