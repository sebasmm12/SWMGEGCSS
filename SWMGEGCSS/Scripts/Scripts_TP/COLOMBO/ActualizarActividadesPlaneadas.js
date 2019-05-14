$(function () {
    var id_plan = 0;

    /* */

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_plan = $(this).attr("data-id-plan");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { act_plan_id: $(this).attr("data-id-plan") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                 $(".btnActualizarPlanAct").click(envioajaxActualizar);
            });
            return false;
        });
    };
    var envioajaxActualizar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto
        var act_plan = {
            act_plan_id: id_plan,
            act_plan_nombre: $("#Actividad_planeada_act_plan_nombre").val(),
            act_plan_descripcion: $("#Actividad_planeada_act_plan_descripcion").val(),
            act_plan_costo: $("#Actividad_planeada_act_plan_costo").val(),
            act_plan_tiempo: $("#Actividad_planeada_act_plan_tiempo").val()
        };
        $.ajax({
            url: "/Plan/_ModalActualizarActividadesPlanificadasFinal",
            method: "POST",
            data: {
                act_plan: act_plan
            }
        }).done(function (data) {
            window.location.href = "/Plan/Actualizar_Plan_de_Proyectos?id=" + data;
        });
    };

    $(".btnModal").each(envioajaxModal);
    $(".btnActualizarPlanAct").click(envioajaxActualizar);
});