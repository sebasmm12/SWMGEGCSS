$(function () {



    var validacion = function () {
        var $nombre_plan = $("#plans_aux_plan_nombre");
        var vnombre = validar_nombre($nombre_plan.val());
        if (vnombre === false) {

            return false;
        }
        $.ajax({
            url: "/Plan/Actualizar_Plan_de_Proyecto",
            method: "POST",
            data: $("form").serialize(),
            dataType: "json"
        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'El Plan se ha actualizado satisfactoriamente',
                confirmButtonText: 'OK'

            }).then((result) => {
                if (result.value) {
                    window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
                }
            });
        });

    };
    

    function validar_nombre(id) {
        var vnombre = 0;
        if (id === "") {
            adderror("plans_aux_plan_nombre");
            negativeattributes("validatenamePlan", 'Debe ingresar un nombre de Plan');
            $("#plans_aux_plan_nombre").keyup(key);
            return false;
        } else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",/**/
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plans_aux_plan_nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                    adderror("plans_aux_plan_nombre");
                    negativeattributes("validatenamePlan", 'Este nombre ya existe');
                }
            });
        }
        if (vnombre === 1) {
            adderror("plans_aux_plan_nombre");
            negativeattributes("validatenamePlan", 'Este nombre ya existe');
            $("#plans_aux_plan_nombre").keyup(key);
            return false;
        }

        return true;
    }
    var key = function () {
        var $valor = $("#plans_aux_plan_nombre");
        if ($valor.val() === "") {
            negativeattributes("validatenamePlan", 'Debe ingresar un nombre');
            adderror("plans_aux_plan_nombre");
        } else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                data: {
                    plan_nombre: $("#plans_aux_plan_nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("plans_aux_plan_nombre");
                    negativeattributes("validatenamePlan", 'Este nombre ya existe');
                } else {
                    attributes("validatenamePlan");
                    addgood("plans_aux_plan_nombre");
                }
            });
        }

    };

    $("#btnActualizar").click(validacion);
});