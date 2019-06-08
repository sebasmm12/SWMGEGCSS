$(function () {
    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("dataid")
        };
        $input.autocomplete(options);

    };
    $("input[dataid]").each(autocompletado);
    var validacion = function () {
        var $nombre_proyecto = $("#Expediente_exp_nombre");
        var $plan = $("#Expediente_plan_nombre");
        var $ganancia = $("#Expediente_exp_ganancia");
        var $fecha_inicio = $("#Expediente_exp_inicio");
        var $fecha_fin = $("#Expediente_exp_fin");
        var vnombre = validar_nombre($nombre_proyecto.val());
        var vplan = validar_plan($plan.val());
        //var vganancia = validar_ganancia($ganancia.val());
        var vfechai = validar_finicio($fecha_inicio.val());
        var vfechaf = validar_ffin($fecha_fin.val());
        if (vnombre === false || vplan === false || /*vganancia === false*/ vfechai === false || vfechaf === false) {
          
            return false;
        }
        $.ajax({
            url: "/Expediente/Registrar_Proyecto",
            method: "POST",
            data: $("form").serialize(),
            dataType: "json"
        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'Se registro el proyecto exitosamente',
                confirmButtonText: 'OK'

            }).then((result) => {
                if (result.value) {
                    window.location.href = "/Gerente/Gestionar_Proyectos";
                }
            });       
        });
         
    };
    function validar_ffin(id) {
        var fechaIngresadaF = new Date($("#Expediente_exp_fin").val());
        fechaIngresadaF.setDate(fechaIngresadaF.getDate() + 1);
        var fechaIngresadaI = new Date($("#Expediente_exp_inicio").val());
        fechaIngresadaI.setDate(fechaIngresadaI.getDate() + 1);
        fechaIngresadaI.setHours(0, 0, 0, 0);
        fechaIngresadaF.setHours(0, 0, 0, 0);
        if (id === "") {
            adderror("Expediente_exp_fin");
            negativeattributes("validateFechaFin", 'Debe ingresar una fecha');
            $("#Expediente_exp_fin").change(keyfechaF);
            return false;
        } else {
            if (fechaIngresadaF > fechaIngresadaI) {
                attributes("validateFechaFin");
                addgood("Expediente_exp_fin");
            } else {
                negativeattributes("validateFechaFin", 'Debe ingresar una fecha valida');
                adderror("Expediente_exp_fin");
                return false;
            }
        }
        return true;
    }
    var keyfechaF = function () {
        var fechaIngresadaF = new Date($("#Expediente_exp_fin").val());
        fechaIngresadaF.setDate(fechaIngresadaF.getDate() + 1);
        var fechaIngresadaI = new Date($("#Expediente_exp_inicio").val());
        fechaIngresadaI.setDate(fechaIngresadaI.getDate() + 1);
        fechaIngresadaI.setHours(0, 0, 0, 0);
        fechaIngresadaF.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#Expediente_exp_fin");
        if ($fecha_inicio.val() === "") {
            negativeattributes("validateFechaFin", 'Debe ingresar una fecha');
            adderror("Expediente_exp_fin");
        } else {
            if (fechaIngresadaF > fechaIngresadaI) {
                attributes("validateFechaFin");
                addgood("Expediente_exp_fin");
            } else {
                negativeattributes("validateFechaFin", 'Debe ingresar una fecha valida');
                adderror("Expediente_exp_fin");
            }
        }
    };

    function validar_finicio(id) {
        var fechaIngresada = new Date($("#Expediente_exp_inicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        if (id === "") {
            adderror("Expediente_exp_inicio");
            negativeattributes("validateFechaInicio", 'Debe ingresar una fecha');
            $("#Expediente_exp_inicio").change(keyfechaI);
            return false;
        }
        else {
            if (fechaIngresada >= dateActual) {
                attributes("validateFechaInicio");
                addgood("Expediente_exp_inicio");
            } else {
                negativeattributes("validateFechaInicio", 'Debe ingresar una fecha valida');
                adderror("Expediente_exp_inicio");
                return false;
            }
        }
        return true;
    }
    var keyfechaI = function () {
        var fechaIngresada = new Date($("#Expediente_exp_inicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate()+1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#Expediente_exp_inicio");
        if ($fecha_inicio.val() === "") {
            negativeattributes("validateFechaInicio", 'Debe ingresar una fecha');
            adderror("Expediente_exp_inicio");
        } else {
            if (fechaIngresada >= dateActual) {
                attributes("validateFechaInicio");
                addgood("Expediente_exp_inicio");
            } else {
                negativeattributes("validateFechaInicio", 'Debe ingresar una fecha valida');
                adderror("Expediente_exp_inicio");
            }
        }
    };
    function validar_ganancia(id) {
        var RegularExpression = /^\d+[,]*\d*$/;
        var $valor = $("#Expediente_exp_ganancia");
        if (id === "") {
            adderror("Expediente_exp_ganancia");
            negativeattributes("validateGanancia",'Debe ingresar un numero');
            $("#Expediente_exp_ganancia").keyup(keyG);
            return false;
        }
        else {
            if ($valor.val().match(RegularExpression)) {
                attributes("validateGanancia");
                addgood("Expediente_exp_ganancia");
            } else {
                negativeattributes("validateGanancia", 'Debe ingresar un numero');
                adderror("Expediente_exp_ganancia");
                return false;
            }

        }
        return true;
    }
    function keyG() {
        var RegularExpression =/^\d+[,]*\d*$/;
        var $valor = $("#Expediente_exp_ganancia");
        if ($valor.val() === "") {
            negativeattributes("validateGanancia", 'Debe ingresar un numero');
            adderror("Expediente_exp_ganancia");
        } else {
            if ($valor.val().match(RegularExpression)) {
                attributes("validateGanancia");
                addgood("Expediente_exp_ganancia");
            } else {
                negativeattributes("validateGanancia", 'Debe ingresar un numero');
                adderror("Expediente_exp_ganancia");
            }

        }
    }



    function validar_plan(id) {
         var vplan = 0;
        if (id === "") {
            adderror("Expediente_plan_nombre");
            negativeattributes("validatePlanProyecto", 'Debe ingresar un plan');
            $("#Expediente_plan_nombre").keyup(keyp);
            return false;
        } else {
            $.ajax({
                url: "/Expediente/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    exp_plan: $("#Expediente_plan_nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data === 0) {
                    vplan = 0;
                } else {
                    vplan = 1;
                }
            });
        }
        if (vplan === 0) {
            adderror("Expediente_plan_nombre");
            negativeattributes("validatePlanProyecto", 'El plan no existe');
            $("#Expediente_plan_nombre").keyup(keyp);
            return false;
        }

        return true;
    }

    function validar_nombre(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var vnombre = 0;
        if (id === "") {
            adderror("Expediente_exp_nombre");
            negativeattributes("validatenameProyecto", 'Debe ingresar un nombre');
            $("#Expediente_exp_nombre").keyup(key);
            return false;
        } else {
            if (!$("#Expediente_exp_nombre").val().match(RegularExpression)) {
                attributes("validatenameProyecto");
                addgood("Expediente_exp_nombre");
                $.ajax({
                    url: "/Expediente/Evaluar_Nombre_Proyecto",
                    method: "GET",
                    async: false,
                    data: {
                        exp_nombre: $("#Expediente_exp_nombre").val()
                    },
                    dataType: "json"
                }).done(function (data) {
                    if (data !== 0) {
                        vnombre = 1;
                        adderror("Expediente_exp_nombre");
                        negativeattributes("validatenameProyecto", 'Este nombre ya existe');
                    }
                });
            }
            else {
                adderror("Expediente_exp_nombre");
                negativeattributes("validatenameProyecto", 'Este nombre no es valido');
                return false;
            }

            
        }
        if (vnombre === 1) {
            adderror("Expediente_exp_nombre");
            negativeattributes("validatenameProyecto", 'Este nombre ya existe');
            $("#Expediente_exp_nombre").keyup(key);
            return false;
        }

        return true;
    }
    var key = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#Expediente_exp_nombre");
        if ($valor.val() === "") {
            negativeattributes("validatenameProyecto", 'Debe ingresar un nombre');
            adderror("Expediente_exp_nombre");
        } else {
            if (!$valor.val().match(RegularExpression)) {
                attributes("validatenameProyecto");
                addgood("Expediente_exp_nombre");
                $.ajax({
                    url: "/Expediente/Evaluar_Nombre_Proyecto",
                    method: "GET",
                    async: false,
                    data: {
                        exp_nombre: $("#Expediente_exp_nombre").val()
                    },
                    dataType: "json"
                }).done(function (data) {
                    if (data !== 0) {
                        adderror("Expediente_exp_nombre");
                        negativeattributes("validatenameProyecto", 'Este nombre ya existe');
                    } else {
                        attributes("validatenameProyecto");
                        addgood("Expediente_exp_nombre");
                    }
                });

            } else {
                adderror("Expediente_exp_nombre");
                negativeattributes("validatenameProyecto", 'Este nombre no es valido');
            }

        }

    };
    var keyp = function () {
        var $valor = $("#Expediente_plan_nombre");
        if ($valor.val() === "") {
            negativeattributes("validatePlanProyecto", 'Debe ingresar un plan');
            adderror("Expediente_plan_nombre");
        } else {
            $.ajax({
                url: "/Expediente/Evaluar_Nombre_Plan",
                method: "GET",
                data: {
                    exp_plan: $("#Expediente_plan_nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data === 0) {
                    vplan = 0;
                    negativeattributes("validatePlanProyecto", 'El plan no existe');
                    adderror("Expediente_plan_nombre");
                } else {
                    attributes("validatePlanProyecto");
                    addgood("Expediente_plan_nombre");
                    enviarcosto();
                }
            });
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
    function negativeattributes(id,tipo) {
        $("#" + id).removeClass("textsuccess");
        $("#" + id).addClass("text-danger");
        $("#" + id).html("");
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo+"</label > ");
    }

    var enviarcosto = function () {
        $.ajax({
            url: "/Expediente/Calcular_Costo_Actividades",
            type: "GET",
            data: {
                plan_nombre: $("#Expediente_plan_nombre").val()
            }
        }).done(function (data) {
            $("#Expediente_exp_ganancia").val(data);

        });
       
    };

    $("#btnRegistrar").click(validacion);
    $("#Expediente_plan_nombre").keyup(function () {
        enviarcosto();
    });
});