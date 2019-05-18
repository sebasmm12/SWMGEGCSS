$(function () {
var valido = function () {
    var $actdesacomentario = $("#actividad_comentario");
    var $archivo = $("#newFile");

    var vComentario = true;//validar_Comentario($actdesacomentario.val());

        if (vComentario === false ) {
            return false;
        }
        else {
            act_desa_comentario: $("#actividad_comentario").val();
            archivo = $("#newFile");
            $.ajax({
                url: "/Trabajador/AgregarArchivo",
                method: "POST",
                data: {
                    act_desa_comentario: act_desa_comentario,
                    archivo: archivo
                }
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se subio el archivo exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Trabajador/V_Tareas";
                    }
                });
            });
        }
        return false;
    };
    $("#btnSubirArchivo").click(valido);
});