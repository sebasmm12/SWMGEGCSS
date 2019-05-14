$(function () {
    registroAsignacion = function () {

        var $actDesaFechaInicio = $("#actDesaFechaInicio");
        var $actDesaFechaFin = $("#actDesaFechaFin");

        var vfechaInicio = validar_fecha_plan($actDesaFechaInicio);

        if (vfechaInicio === false) {
            return false;
        }
        else
        {
        $.ajax({
            url: "/ActividadesDesarrollar/RegistrarAsignacionActividades_Desarrollar",
            method: "POST",
            data: $("form").serialize(),
            dataType: "json"
        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'Se registro la asignacion satisfactoriamente',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.value) {
                    //window.location.href = "/Plan/Agregar_Plan_de_Proyectos_1";
                    window.location.href = "/Administrador/Asignacion_Tareas";
                }
            });

        });
        }
        return false;
    };

    //Validacion Fecha Plan
    function validar_fecha_plan(id) {
        var fechaIngresada = new Date($("#actDesaFechaInicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        if (id === "") {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha", 'Debe ingresar una fecha');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
            return false;
        }
        else {
            if (fechaIngresada >= dateActual) {
                attributes("error-act-desa-fecha");
                addgood("actDesaFechaInicio");
            } else {
                negativeattributes("error-act-desa-fecha", 'Debe ingresar una fecha válida');
                adderror("actDesaFechaInicio");
                return false;
            }
        }
        return true;
    }
    var keyfechaI = function () {
        var fechaIngresada = new Date($("#actDesaFechaInicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#actDesaFechaInicio");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-act-desa-fecha", 'Debe ingresar una fecha');
            adderror("actDesaFechaInicio");
        } else {
            if (fechaIngresada >= dateActual) {
                attributes("error-act-desa-fecha");
                addgood("actDesaFechaInicio");
            } else {
                negativeattributes("error-plan-fecha", 'Debe ingresar una fecha válida');
                adderror("actDesaFechaInicio");
            }
        }
    };

    function attributes(id) {
        $("#" + id).removeClass("text-danger");
        $("#" + id).addClass("textsuccess");
        $("#" + id).html("");
        $("#" + id).html("<i class='fa fa-check'></i><label class='pl-2'>Correcto</label>");
    }
    function addgood(id) {
        $("#" + id).removeClass("inputerror");
        $("#" + id).addClass("inputtrue");
    }
    function adderror(id) {
        $("#" + id).removeClass("inputtrue");
        $("#" + id).addClass("inputerror");
        $("#" + id).focus();
    }
    function negativeattributes(id, tipo) {
        $("#" + id).removeClass("textsuccess");
        $("#" + id).addClass("text-danger");
        $("#" + id).html("");
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo + "</label > ");
    }

    $("#btnAsignarTarea").click(registroAsignacion);
});