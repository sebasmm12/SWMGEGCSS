$(function () {
    var id_plan = 0;
    var id_act = 0;
    //Para mostrar la tabla de actividades
    /*Verify = function () {
        var $button = $("#btnMostrar");
        if ($button.hasClass("vision")) {
            $("#tablaActividades").removeClass("esconder");
            $("#btnMostrar").removeClass("vision");
            $("#tablaActividades").addClass("mostrar");
            $("#btnMostrar").addClass("oculto");
            $button.text("Esconder");
        }
        else {
            $("#tablaActividades").removeClass("mostrar");
            $("#btnMostrar").removeClass("oculto");
            $("#tablaActividades").addClass("esconder");
            $("#btnMostrar").addClass("vision");
            $button.text("Mostrar");
        }
        return true;
     };*/

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            $.ajax({
                url: $(this).attr("data-url"),
                type: "GET"
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                //$(".btnRegistrarPlanAct").click(envioajaxRegistrar);
                });
            return false;
        });
    };

    /*var envioajaxRegistrar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto
        alert($("#Actividades_planeadas_aux_act_plan_nombre").val());
        var act_plan = {
            plan_id: id_plan,
            act_id: id_act,
            act_plan_nombre: $("#ActPlanNombre").val(),
            act_plan_descripcion: $("#ActPlanDescripcion").val(),
            act_plan_costo: $("#ActPlanCosto").val(),
            act_plan_tiempo: $("#ActPlanTiempo").val()
        };
        $.ajax({
            url: "/Plan/_ModalRegistrarActividadesPlanificadasFinal",
            method: "POST",
            data: {
                act_plan: act_plan
            }
        }).done(function (data) {
            window.location.href = "/Plan/Agregar_Plan_de_Proyectos_1";
        });
    };*/
    //TablaActiv : evaluar si existe y mostrar
    TablaActiv = function () {
        $.ajax({
            url: "/Plan/Agregar_Plan_de_Proyectos",
            data: {
                tipo_servicio_nombre: $("#plan-tipo-servicio").val()
            },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("#tablaActividades");
            $(target).replaceWith($newhtml);
        });
        return true;
    };
    $("#btnPrueba").click(TablaActiv);
    
    //$("#btnMostrar").click(Verify);
    $(".btnModal").each(envioajaxModal);
    //$(".btnRegistrarPlanAct").click(envioajaxRegistrar);
});