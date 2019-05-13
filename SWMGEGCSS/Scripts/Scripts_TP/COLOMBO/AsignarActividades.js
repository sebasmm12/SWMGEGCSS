$(function () {
    var registroAsignacion = function () {
        $.ajax({
            url: "/ActividadesDesarrollar/RegistrarAsignacionActividades_Desarrollar",
            method: "POST",
            data: $("form").serialize(),
            dataType: "json"
        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'Se registro el plan exitosamente',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.value) {
                    //window.location.href = "/Plan/Agregar_Plan_de_Proyectos_1";
                    window.location.href = "/Administrador/Asignacion_Tareas";
                }
            });

        });
    };
    $("#btnAsignarTarea").click(registroValidado);
});