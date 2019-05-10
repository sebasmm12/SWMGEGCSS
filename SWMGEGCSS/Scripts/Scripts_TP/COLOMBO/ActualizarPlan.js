$(function () {

    registro = function () {
        $.ajax({
            url: "/Plan/Actualizar_Plan_de_Proyectos",
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
                    window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
                }
            });
        });
    }
    $("#btnActualizarPlan").click(registro);
});