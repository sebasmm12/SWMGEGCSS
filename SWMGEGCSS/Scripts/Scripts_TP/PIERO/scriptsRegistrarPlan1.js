$(function () {
    var id_plan = 0;
    var id_act = 0;

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_plan = $(this).attr("data-id-plan");
            id_act = $(this).attr("data-id-act");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: {
                    plan_id: id_plan,
                    act_id: id_act
                }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnRegistrarPlanAct").click(envioajaxRegistrar);
            });
            return false;
        });
    };
    var envioajaxRegistrar = function () {
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
    };
    $(".btnModal3").each(envioajaxModal);
    $(".btnRegistrarPlanAct").click(envioajaxRegistrar);
});