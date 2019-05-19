﻿$(function () {

    var validacionAsignarActDesa = function () {
        var $actDesaFechaInicio = $("#actDesaFechaInicio");
        var $expFechaInicio = $("#expFechaInicio");
        var $expFechaFin = $("#expFechaFin");

        var $actDesaFechaFin = $("#actDesaFechaFin");


        var vActDesaFechaInicio = validar_fecha_act_desa_inicio($actDesaFechaInicio.val());
        var vActDesaFechaFin = validar_fecha_act_desa_fin($actDesaFechaFin.val());
        var vUsuarioSeleccionado = validar_usuario_seleccionado();
        //var vActDesaFechaInicio = validar_fecha_inicio($actDesaFechaFin.val());
        if (vActDesaFechaInicio === false || vActDesaFechaFin === false /*|| vUsuarioSeleccionado === false*/) {
            return false;
        } else {
            $(".btnModalActividadesDesarrollar1").click(envioajaxModal);
        }
        return false;
    };

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            var act_desa_aux = {
                act_desa_fecha_inicio: $("#actDesaFechaInicio").val(),
                act_desa_fecha_fin: $("#actDesaFechaFin").val()
            };
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: {
                    actividadesDesarrollarAux: act_desa_aux
                }
            }).
                done(function (data) {
                    var target = $button.attr("data-id-target");
                    var $newhtml = $(data);
                    $(target).replaceWith($newhtml);
                    $(modal).modal();
                    $(modal).on('shown.bs.modal', function () {
                        $(document).off('focusin.modal');
                    });
                    $(".btnRegistrarActDesa").each(Alerta);
                });
            return false;
        });
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
            url: "/ActividadesDesarrollar/RegistrarAuditoriaActividadDesarrollarFinal",
            method: "GET",
            data: {
                comentario: comentario
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            window.location.href = "/Administrador/Asignacion_Tareas";
        });
        return false;
    };

    //validacion seleccionar usuario
    function validar_usuario_seleccionado() {
        var vnombre = 0;
        $.ajax({
            url: "/ActividadesDesarrollar/Evaluar_UsuarioAsignado",
            method: "GET",
            async: false,
            /*data: {
                plan_nombre: $("#plan-nombre").val()
            },*/
            dataType: "json"
        }).done(function (data) {
            if (data !== 0) {
                vnombre = 1;
            }
        });
        if (vnombre === 0) {
            //adderror("plan-nombre");
            negativeattributes("error-usuario-asignado", 'Debe seleccionar un usario Asignado');
            //$("#plan-nombre").keyup(key);
            return false;
        }
        attributes("error-usuario-asignado");
        return true;
    }
    //Validacion Fecha logica
   /* function validar_fecha_act_desa() {
        var fechaIngresadaFin = new Date($("#actDesaFechaFin").val());
        fechaIngresadaFin.setDate(fechaIngresadaFin.getDate() + 1);
        fechaIngresadaFin.setHours(0, 0, 0, 0);

        var fechaIngresadaInicio = new Date($("#actDesaFechaInicio").val());
        fechaIngresadaInicio.setDate(fechaIngresadaInicio.getDate() + 1);
        fechaIngresadaInicio.setHours(0, 0, 0, 0);

        if (fechaIngresadaFin < fechaIngresadaInicio) {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);
        }
    }
    */
    //Validacion Fecha Actividades Desarrollar
    function validar_fecha_act_desa_fin(id) {
        var fechaIngresada = new Date($("#actDesaFechaFin").val());
        var fechaInicioExp = new Date($("#expFechaInicio").val());
        var fechaFinExp = new Date($("#expFechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);

        if (id === "") {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'Debe ingresar una fecha');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);
            return false;
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);
            return false;
        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);
            return false;
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-fin");
                addgood("actDesaFechaFin");
                return true;
            } else {
                negativeattributes("error-act-desa-fecha-fin", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaFin");
                return false;
            }
        }
    }
    var keyfechaJ = function () {
        var fechaIngresada = new Date($("#actDesaFechaFin").val());
        var fechaInicioExp = new Date($("#expFechaInicio").val());
        var fechaFinExp = new Date($("#expFechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#actDesaFechaFin");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-act-desa-fecha-fin", 'Debe ingresar una fecha');
            adderror("actDesaFechaFin");
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);

        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaFin");
            negativeattributes("error-act-desa-fecha-fin", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaFin").focus();
            $("#actDesaFechaFin").change(keyfechaJ);
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-fin");
                addgood("actDesaFechaFin");
            } else {
                negativeattributes("error-act-desa-fecha-fin", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaFin");
            }
        }
    };
    function validar_fecha_act_desa_inicio(id) {
        var fechaIngresada = new Date($("#actDesaFechaInicio").val());
        var fechaInicioExp = new Date($("#expFechaInicio").val());
        var fechaFinExp    = new Date($("#expFechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);

        if (id === "") {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha-inicio", 'Debe ingresar una fecha');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
            return false;
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha-inicio", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
            return false;
        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha-inicio", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
            return false;
        }
       else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-inicio");
                addgood("actDesaFechaInicio");
                return true;
            } else {
                negativeattributes("error-act-desa-fecha-inicio", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaInicio");
                return false;
            }
        }
     
    }
    var keyfechaI = function () {
        var fechaIngresada = new Date($("#actDesaFechaInicio").val());
        var fechaInicioExp = new Date($("#expFechaInicio").val());
        var fechaFinExp = new Date($("#expFechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#actDesaFechaInicio");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-act-desa-fecha-inicio", 'Debe ingresar una fecha');
            adderror("actDesaFechaInicio");
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha-inicio", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
            
        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaInicio");
            negativeattributes("error-act-desa-fecha-inicio", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaInicio").focus();
            $("#actDesaFechaInicio").change(keyfechaI);
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-inicio");
                addgood("actDesaFechaInicio");
            } else {
                negativeattributes("error-act-desa-fecha-inicio", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaInicio");
            }
        }
    };

    //Atributos de Tipo
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
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo + "</label> ");
    }
    $(".btnModalActividadesDesarrollar1").click(validacionAsignarActDesa);
    //$("#btnAsignarTarea").click(registroAsignacion);
});