$(function () {

    var validacionReasignarActDesa = function () {
        var count = 0;
        var $actDesaFechaInicio = $("#actDesaFechaInicio2");
        var $expFechaInicio = $("#expFechaInicio2");
        var $expFechaFin = $("#expFechaFin2");
        var $actDesaFechaFin = $("#actDesaFechaFin2");


        var vActDesaFechaInicio = validar_fecha_act_desa_inicio($actDesaFechaInicio.val());
        var vActDesaFechaFin = validar_fecha_act_desa_fin($actDesaFechaFin.val());
        var vUsuarioSeleccionado = validar_usuario_seleccionado();

        var vActDesaFechasCoherencia = validar_coherencia_fechas();
        //var vActDesaFechaInicio = validar_fecha_inicio($actDesaFechaFin.val());
        if (vActDesaFechaInicio === false || vActDesaFechaFin === false || vActDesaFechasCoherencia === false /*|| vUsuarioSeleccionado === false*/) {
            return false;
        } else {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            var act_desa_aux = {
                act_desa_fecha_inicio: $("#actDesaFechaInicio2").val(),
                act_desa_fecha_fin: $("#actDesaFechaFin2").val()
            };
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                async:false,
                data: {
                    actividadesDesarrollarAux: act_desa_aux
                }
            }).
                done(function (data) {
                    count++;
                    var target = $button.attr("data-id-target");
                    var $newhtml = $(data);
                    $(target).replaceWith($newhtml);
                    $(modal).modal();
                    $(modal).on('shown.bs.modal', function () {
                        $(document).off('focusin.modal');
                    });
                    $(".btnActualizarActDesa").each(Alerta);
                });
        }
        if (count > 0) {
            return true;
        } else {
            return false;
        }
    };
    var Alerta = function () {
        $(this).click(function () {

            (async function getEmail() {
                const { value: comentario } = await Swal.fire({
                    title: 'Descripciion de la Reasignacion',
                    input: 'textarea',
                    inputPlaceholder: 'Ingrese una descripcion',
                    showCancelButton: true,
                    inputValidator: (value) => {
                        if (!value) {
                            return 'Este campo es obligatorio';
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

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            var act_desa_aux = {
                act_desa_fecha_inicio: $("#actDesaFechaInicio2").val(),
                act_desa_fecha_fin: $("#actDesaFechaFin2").val()
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
                    $(".btnActualizarActDesa").each(Alerta);
                });
            return false;
        });
    };
    /*var envioajaxRegistrar = function () {
        //mismo nombre del parametro de actualizacion del metodo
        //creando objeto 
        
            $.ajax({
                url: "/Plan/_ModalRegistrarActividadesPlanificadasFinal",
                method: "POST"
            }).done(function (data) {
                window.location.href = "/Administrador/Asignacion_Tareas";
            });
    };*/


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
            negativeattributes("error-usuario-asignado2", 'Debe seleccionar un usario Asignado');
            //$("#plan-nombre").keyup(key);
            return false;
        }
        attributes("error-usuario-asignado2");
        return true;
    }

    //Validacion Fecha logica
    function validar_coherencia_fechas() {
        var fechaIngresadaFin = new Date($("#actDesaFechaFin2").val());
        fechaIngresadaFin.setDate(fechaIngresadaFin.getDate() + 1);
        fechaIngresadaFin.setHours(0, 0, 0, 0);

        var fechaIngresadaInicio = new Date($("#actDesaFechaInicio2").val());
        fechaIngresadaInicio.setDate(fechaIngresadaInicio.getDate() + 1);
        fechaIngresadaInicio.setHours(0, 0, 0, 0);
        if (validar_fecha_act_desa_fin(fechaIngresadaFin) === false || validar_fecha_act_desa_inicio(fechaIngresadaInicio) === false) {
            $("#actDesaFechaFin2").change(keyfechaCo);
            $("#actDesaFechaInicio2").change(keyfechaCo);
            return false;
        }

        else if (fechaIngresadaFin < fechaIngresadaInicio) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha de finalizacion debe ser mayor a la de inicio');
            $("#actDesaFechaFin2").focus();
            $("#actDesaFechaFin2").change(keyfechaCo);
            return false;
        }
        else {
            attributes("error-act-desa-fecha-fin2");
            addgood("actDesaFechaFin2");
            attributes("error-act-desa-fecha-inicio2");
            addgood("actDesaFechaInicio2");
            return true;
        }
    }

    var keyfechaCo = function () {
        var fechaIngresadaFin = new Date($("#actDesaFechaFin2").val());
        fechaIngresadaFin.setDate(fechaIngresadaFin.getDate() + 1);
        fechaIngresadaFin.setHours(0, 0, 0, 0);

        var fechaIngresadaInicio = new Date($("#actDesaFechaInicio2").val());
        fechaIngresadaInicio.setDate(fechaIngresadaInicio.getDate() + 1);
        fechaIngresadaInicio.setHours(0, 0, 0, 0);

        if (validar_fecha_act_desa_fin(fechaIngresadaFin) === false || validar_fecha_act_desa_inicio(fechaIngresadaInicio) === false) {
            $("#actDesaFechaFin2").focus();
            $("#actDesaFechaInicio2").focus();
        }
        else if (fechaIngresadaFin < fechaIngresadaInicio) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha de finalizacion debe ser mayor a la de inicio');
            $("#actDesaFechaFin2").focus();
        }
        else {
            attributes("error-act-desa-fecha-fin2");
            addgood("actDesaFechaFin2");
            attributes("error-act-desa-fecha-inicio2");
            addgood("actDesaFechaInicio2");
        }
    };


    function validar_fecha_act_desa_fin(id) {
        var fechaIngresada = new Date($("#actDesaFechaFin2").val());
        var fechaInicioExp = new Date($("#expFechaInicio2").val());
        var fechaFinExp = new Date($("#expFechaFin2").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);

        if (id === "") {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'Debe ingresar una fecha');
            $("#actDesaFechaFin2").focus();
            $("#actDesaFechaFin2").change(keyfechaJ);
            return false;
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaFin2").focus();
            $("#actDesaFechaFin2").change(keyfechaJ);
            return false;
        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaFin2").focus();
            $("#actDesaFechaFin2").change(keyfechaJ);
            return false;
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-fin2");
                addgood("actDesaFechaFin2");
                $("#actDesaFechaFin2").change(keyfechaJ);
                return true;
            } else {
                negativeattributes("error-act-desa-fecha-fin2", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaFin2");
                $("#actDesaFechaFin2").change(keyfechaJ);
                return false;
            }
        }
    }
    var keyfechaJ = function () {
        var fechaIngresada = new Date($("#actDesaFechaFin2").val());
        var fechaInicioExp = new Date($("#expFechaInicio2").val());
        var fechaFinExp = new Date($("#expFechaFin2").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#actDesaFechaFin2");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-act-desa-fecha-fin2", 'Debe ingresar una fecha');
            adderror("actDesaFechaFin2");
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaFin2").focus();

        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaFin2");
            negativeattributes("error-act-desa-fecha-fin2", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaFin2").focus();
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-fin2");
                addgood("actDesaFechaFin2");
            } else {
                negativeattributes("error-act-desa-fecha-fin2", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaFin2");
            }
        }
    };
    function validar_fecha_act_desa_inicio(id) {
        var fechaIngresada = new Date($("#actDesaFechaInicio2").val());
        var fechaInicioExp = new Date($("#expFechaInicio2").val());
        var fechaFinExp = new Date($("#expFechaFin2").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);

        if (id === "") {
            adderror("actDesaFechaInicio2");
            negativeattributes("error-act-desa-fecha-inicio2", 'Debe ingresar una fecha');
            $("#actDesaFechaInicio2").focus();
            $("#actDesaFechaInicio2").change(keyfechaI);
            return false;
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaInicio2");
            negativeattributes("error-act-desa-fecha-inicio2", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaInicio2").focus();
            $("#actDesaFechaInicio2").change(keyfechaI);
            return false;
        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaInicio2");
            negativeattributes("error-act-desa-fecha-inicio2", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaInicio2").focus();
            $("#actDesaFechaInicio2").change(keyfechaI);
            return false;
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-inicio2");
                addgood("actDesaFechaInicio2");
                $("#actDesaFechaInicio2").change(keyfechaI);
                return true;
            } else {
                negativeattributes("error-act-desa-fecha-inicio2", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaInicio2");
                $("#actDesaFechaInicio2").change(keyfechaI);
                return false;
            }
        }

    }
    var keyfechaI = function () {
        var fechaIngresada = new Date($("#actDesaFechaInicio2").val());
        var fechaInicioExp = new Date($("#expFechaInicio2").val());
        var fechaFinExp = new Date($("#expFechaFin2").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        fechaInicioExp.setDate(fechaInicioExp.getDate() + 1);
        fechaFinExp.setDate(fechaFinExp.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        fechaInicioExp.setHours(0, 0, 0, 0);
        fechaFinExp.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#actDesaFechaInicio2");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-act-desa-fecha-inicio2", 'Debe ingresar una fecha');
            adderror("actDesaFechaInicio2");
        }
        else if (fechaIngresada < fechaInicioExp) {
            adderror("actDesaFechaInicio2");
            negativeattributes("error-act-desa-fecha-inicio2", 'La fecha debe ser mayor al inicio del proyecto');
            $("#actDesaFechaInicio2").focus();

        }
        else if (fechaIngresada > fechaFinExp) {
            adderror("actDesaFechaInicio2");
            negativeattributes("error-act-desa-fecha-inicio2", 'La fecha debe ser menor al fin del proyecto');
            $("#actDesaFechaInicio2").focus();
        }
        else {
            if (fechaIngresada > dateActual) {
                attributes("error-act-desa-fecha-inicio2");
                addgood("actDesaFechaInicio2");
            } else {
                negativeattributes("error-act-desa-fecha-inicio2", 'la fecha debe ser mayor a la actual');
                adderror("actDesaFechaInicio2");
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
    
    $(".btnModalActividadesDesarrollar2").click(validacionReasignarActDesa);
});