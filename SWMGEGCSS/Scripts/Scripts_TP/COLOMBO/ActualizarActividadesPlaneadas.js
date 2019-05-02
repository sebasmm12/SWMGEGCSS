﻿$(function () {
    var id_plan = 0;
    /*var getPage = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
            $(".btnModal").each(envioajaxModal);
        });
        return false;

    };
    var submitAutocompleteform = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        $("#Buscar").click();
    };
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete"),
            select: submitAutocompleteform
        };
        $input.autocomplete(options);

    };
    var Alerta = function () {
        $(this).click(function () {

            (async function getEmail() {
                const { value: comentario } = await Swal.fire({
                    title: 'Ingrese el porque quiere eliminarlo',
                    input: 'textarea',
                    inputPlaceholder: 'Ingrese comentario',
                    showCancelButton: true,
                    inputValidator: (value) => {
                        if (!value) {
                            return 'Debe escribir algo!';
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
            url: "/Plan/EliminarPlanDeProyecto",
            method: "GET",
            data: {
                id: id_plan,
                comentario: comentario
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
        });
        return false;
    };*/
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
            act_plan_nombre: $("#Actividades_planeadas_aux_act_plan_nombre").val(),
            act_plan_descripcion: $("#Actividades_planeadas_aux_act_plan_descripcion").val(), 
            act_plan_costo: $("#Actividades_planeadas_aux_act_plan_costo").val(),
            act_plan_tiempo: $("#Actividades_planeadas_aux_act_plan_tiempo").val()
        };
        $.ajax({
            url: "/Plan/_ModalActualizarActividadesPlanificadasFinal",
            method: "POST",
            data: {
                act_plan: act_plan
            }
        }).done(function (data) {
            alert("LLEGO ACA!!");
        });
    }
    /*var BuscarProyecto = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        $.ajax({
            url: "/Gerente/Gestionar_Proyectos",
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("div.pagedList").attr("data-exp-target");
            $(target).replaceWith($newhtml);
        });

        return true;
    };*/
    $(".btnModal").each(envioajaxModal);
    $(".btnActualizarPlanAct").click(envioajaxActualizar);
    //$("input[data-exp-autocomplete]").each(autcompletado);
    //$(".pcoded-content").on("click", ".pagedList a", getPage);
    //$("#Buscar").click(BuscarProyecto);
});