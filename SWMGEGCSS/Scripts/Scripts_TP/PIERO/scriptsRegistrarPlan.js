﻿$(function () {
    $.fn.extend({
        animateCss: function (animationName, callback) {
            var animationEnd = (function (el) {
                var animations = {
                    animation: 'animationend',
                    OAnimation: 'oAnimationEnd',
                    MozAnimation: 'mozAnimationEnd',
                    WebKitAnimation: 'webkitAnimationEnd'
                };
                for (var t in animations) {
                    if (el.style[t] !== undefined) {
                        return animations[t];

                    }
                }
            })(document.createElement('div'));
            this.addClass('animated ' + animationName).one(animationEnd, function () {
                $(this).removeClass('animated ' + animationName);
                if (typeof callback === 'function') callback();
            });
            return this;
        }
    });
    
    $(".mov_button1").each(function () {
        $(this).mouseenter(function () {
            $(this).animateCss('tada');
        });
    });









    var esNum = function esNumero(txt) {
        if (isNaN(txt)) {
            return false;
        } else {
            return true;
        }
    };
    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("dataid")
        };
        $input.autocomplete(options);
    };
    validacion = function () {
        //declaracion de variables jquery
        var $planNombre = $("#plan-nombre");
        var $planFecha = $("#plan-fecha");
        //var $planCosto = $("#plan-costo");
        //var $planTiempo = $("#plan-tiempo");
        var $planEmp = $("#plan-emp");
        var $planTipoServicio = $("#plan-tipo-servicio");


        var vnombre = validar_nombre_plan($planNombre.val());
        var vfecha = validar_fecha_plan($planFecha.val());
        //var vcosto = validar_costo_plan($planCosto.val());
        //var vtiempo = validar_tiempo_plan($planTiempo.val());
        var vemp = validar_emp_plan($planEmp.val());
        var vtiposervicio = validar_tipo_servicio_plan($planTipoServicio.val());

        if (vnombre === false || vfecha === false || /*vcosto === false || vtiempo === false ||*/ vemp === false || vtiposervicio === false) {
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Agregar_Plan_de_Proyectos",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                if (data === 1) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se registro el plan exitosamente',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
                        }
                    });
                }
                else if (data === 5) {
                    Swal.fire({
                        type: 'error',
                        title: 'Debe ingresar al menos 1 actividad',
                        confirmButtonText: 'OK'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Verifique que se hayan registrado todas las actividades obligatorias',
                        confirmButtonText: 'OK'
                    });
                }
            });
        }
        return false;
    };

    function aMayusculas(obj, id) {
        obj = obj.toUpperCase();
        document.getElementById(id).value = obj;
    }

    
    var maximoNumeroCaracteres50 = function maxCharacters(X) {
        if (X.length > 50) {
            return true;
        } else {
            return false;
        }
    };
    var tieneCaracEsp = function empiezaConCaracteresEspeciales(X) {
        var iChars = "!@#_$%^&*()+=-[]\\\';,./{}|\":<>?";
        var iNum = "0123456789";
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//numero es un CE o un numero
                return true;
            }
            if (iNum.indexOf(X.charAt(i)) !== -1) {
                return true;
            }
        }
        return false;
    };
    var tieneCaracEspOnly = function empiezaConCaracteresEspecialesOnly(X) {
        var iChars = "!@#_$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//numero es un CE o un numero
                return true;
            }
        }
        return false;
    };

    //Validacion Nombre PLan
    function validar_nombre_plan(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var vnombre = 0;
        if ($("#plan-nombre").val().match(RegularExpression)) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre valido (no esp. blanco)');
            $("#plan-nombre").keyup(key);
            return false;
        }
        if (id === "") {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre');
            $("#plan-nombre").focus();
            $("#plan-nombre").keyup(key);
            return false;
        }
        if (id === " ") {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre no debe empezar con un espacio en blanco');
            $("#plan-nombre").focus();
            $("#plan-nombre").keyup(key);
            return false;
        }
        if (esNum(id) === true) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre no puede ser un número');
            $("#plan-nombre").focus();
            $("#plan-nombre").keyup(key);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#plan-nombre").focus();
            $("#plan-nombre").keyup(key);
            return false;
        }
        if (maximoNumeroCaracteres50(id) === true) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre debe ser de menos de 50 caracteres');
            $("#plan-nombre").focus();
            $("#plan-nombre").keyup(key);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                }
            });
        }
        if (vnombre === 1) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
            $("#plan-nombre").keyup(key);
            return false;
        }
        return true;
    }
    var key = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#plan-nombre");
        if ($valor.val().match(RegularExpression)) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre valido (no esp. blanco)');
        }
        else if ($valor.val() === "") {
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre');
            adderror("plan-nombre");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-nombre", 'El nombre no debe empezar con un espacio en blanco');
            adderror("plan-nombre");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre no puede ser un número');
            adderror("plan-nombre");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("plan-nombre");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-nombre");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("plan-nombre");
                    negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
                }
                else {
                    attributes("error-plan-nombre");
                    addgood("plan-nombre");
                }
            });
        }

    };
    //Validacion Fecha Plan
    function validar_fecha_plan(id) {
        var fechaIngresada = new Date($("#plan-fecha").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        if (id === "") {
            adderror("plan-fecha");
            negativeattributes("error-plan-fecha", 'Debe ingresar una fecha');
            $("#plan-fecha").focus();
            $("#plan-fecha").change(keyfechaI);
            return false;
        }
        else {
            if (fechaIngresada >= dateActual) {
                attributes("error-plan-fecha");
                addgood("plan-fecha");
            } else {
                negativeattributes("error-plan-fecha", 'Debe ingresar una fecha válida');
                adderror("plan-fecha");
                return false;
            }
        }
        return true;
    }
    var keyfechaI = function () {
        var fechaIngresada = new Date($("#plan-fecha").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#plan-fecha");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-plan-fecha", 'Debe ingresar una fecha');
            adderror("plan-fecha");
        } else {
            if (fechaIngresada >= dateActual) {
                attributes("error-plan-fecha");
                addgood("plan-fecha");
            } else {
                negativeattributes("error-plan-fecha", 'Debe ingresar una fecha válida');
                adderror("plan-fecha");
            }
        }
    };
    //Validacion Costo
    /*function validar_costo_plan(id) {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-costo");
        if (id === "" || id <= 0) {
            adderror("plan-costo");
            negativeattributes("error-plan-costo", 'Debe ingresar un numero positivo');
            $("#plan-costo").keyup(keyC);
            return false;
        }
        else {
            if ($valor.val().match(RegularExpression)) {
                attributes("error-plan-costo");
                addgood("plan-costo");
            } else {
                negativeattributes("error-plan-costo", 'Debe ingresar un numero positivo');
                adderror("plan-costo");
                $("#plan-costo").keyup(keyC);
                return false;
            }
        }
        return true;
    }
    function keyC() {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-costo");
        if ($valor.val() === "" || $valor.val() <= 0) {
            negativeattributes("error-plan-costo", 'Debe ingresar un numero positivo');
            adderror("plan-costo");
        } else {
            if ($valor.val().match(RegularExpression)) {
                attributes("error-plan-costo");
                addgood("plan-costo");
            } else {
                negativeattributes("error-plan-costo", 'Debe ingresar un numero positivo');
                adderror("plan-costo");
            }
        }
    }*/
    //Validacion Tiempo
    /*function validar_tiempo_plan(id) {
        //var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-tiempo");
        if (id === "") {
            adderror("plan-tiempo");
            negativeattributes("error-plan-tiempo", 'Debe ingresar un número');
            $("#plan-tiempo").focus();
            $("#plan-tiempo").keyup(keyT);
            return false;
        }
        if (id <= 0) {
            adderror("plan-tiempo");
            negativeattributes("error-plan-tiempo", 'El número debe ser positivo y mayor que 0');
            $("#plan-tiempo").focus();
            $("#plan-tiempo").keyup(keyT);
            return false;
        }
        else {
            if ($valor.val().match(RegularExpression)) {
                negativeattributes("error-plan-tiempo", 'No se aceptan caracteres especiales');
                adderror("plan-tiempo");
                $("#plan-tiempo").keyup(keyT);
                return false;
            } else {
                attributes("error-plan-tiempo");
                addgood("plan-tiempo");
            }
        }
        return true;
    }
    function keyT() {
        //var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-tiempo");
        if ($valor.val() === "") {
            negativeattributes("error-plan-tiempo", 'Debe ingresar un número');
            adderror("plan-tiempo");
        }
        else if ($valor.val() <= 0) {
            negativeattributes("error-plan-tiempo", 'El número debe ser positivo y mayor que 0');
            adderror("plan-tiempo");
        }
        else {
        //    if (!$valor.val().match(RegularExpression)) {
                attributes("error-plan-tiempo");
                addgood("plan-tiempo");
            }
            else {
                negativeattributes("error-plan-tiempo", 'No se aceptan caracteres especiales');
                adderror("plan-tiempo");
            }
        }
    }*/
    //Validacion Empresa
    function validar_emp_plan(id) {
        var vemp = 0;
        if (id === "") {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'Debe ingresar un nombre');
            $("#plan-emp").focus();
            $("#plan-emp").keyup(keyE);
            return false;
        }
        if (id === " ") {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'El nombre de la empresa no debe empezar con un espacio en blanco');
            $("#plan-emp").focus();
            $("#plan-emp").keyup(keyE);
            return false;
        }
        if (esNum(id) === true) {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'El nombre no puede ser un número');
            $("#plan-emp").focus();
            $("#plan-emp").keyup(keyE);
            return false;
        }
        /*if (tieneCaracEspOnly(id) === true) {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
            $("#plan-emp").focus();
            $("#plan-emp").keyup(keyE);
            return false;
        }*/
        if (maximoNumeroCaracteres50(id) === true) {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'El nombre debe ser de menos de 50 caracteres');
            $("#plan-emp").focus();
            $("#plan-emp").keyup(keyE);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Empresa_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_emp: $("#plan-emp").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vemp = 1;
                }
            });
        }
        if (vemp === 0) {
            adderror("plan-emp");
            negativeattributes("error-plan-emp", 'El nombre de esta empresa no existe');
            $("#plan-emp").keyup(keyE);
            return false;
        }
        else { return true; }
    }
    var keyE = function () {
        var $valor = $("#plan-emp");
        if ($valor.val() === "") {
            negativeattributes("error-plan-emp", 'Debe ingresar un nombre');
            adderror("plan-emp");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-emp", 'El nombre de la empresa no debe empezar con un espacio en blanco');
            adderror("plan-emp");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre no puede ser un número');
            adderror("plan-emp");
        }
        /*else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
            adderror("plan-emp");
        }*/
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-emp");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Empresa_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_emp: $("#plan-emp").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    attributes("error-plan-emp");
                    addgood("plan-emp");
                }
                else {
                    adderror("plan-emp");
                    negativeattributes("error-plan-emp", 'El nombre de esta empresa no existe');
                }
            });
        }

    };
    //Validacion Tipo Servicio
    function validar_tipo_servicio_plan(id) {
        var vtiposervicio = 0;
        if (id === "") {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'Debe ingresar un nombre');
            $("#plan-tipo-servicio").focus();
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        if (id === " ") {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'El nombre del tipo servicio no debe empezar con un espacio en blanco');
            $("#plan-tipo-servicio").focus();
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        if (esNum(id) === true) {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'El nombre no puede ser un número');
            $("#plan-tipo-servicio").focus();
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'El nombre del tipo servicio debe empezar con una letra y no debe ser caracter especial');
            $("#plan-tipo-servicio").focus();
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        if (maximoNumeroCaracteres50(id) === true) {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'El nombre debe ser de menos de 50 caracteres');
            $("#plan-tipo-servicio").focus();
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Tipo_Servicio_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_tipo_servicio: $("#plan-tipo-servicio").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vtiposervicio = 1;
                }
            });
        }
        if (vtiposervicio === 0) {
            adderror("plan-tipo-servicio");
            negativeattributes("error-plan-tipo-servicio", 'El nombre de este servicio no existe');
            $("#plan-tipo-servicio").keyup(keyTS);
            return false;
        }
        else { return true; }
    }
    var keyTS = function () {
        var $valor = $("#plan-tipo-servicio");
        if ($valor.val() === "") {
            negativeattributes("error-plan-tipo-servicio", 'Debe ingresar un nombre');
            adderror("plan-tipo-servicio");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa no debe empezar con un espacio en blanco');
            adderror("plan-tipo-servicio");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre no puede ser un número');
            adderror("plan-tipo-servicio");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
            adderror("plan-tipo-servicio");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-tipo-servicio");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Tipo_Servicio_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_tipo_servicio: $("#plan-tipo-servicio").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    attributes("error-plan-tipo-servicio");
                    addgood("plan-tipo-servicio");
                }
                else {
                    adderror("plan-tipo-servicio");
                    negativeattributes("error-plan-tipo-servicio", 'El nombre de este servicio no existe');
                }
            });
        }

    };
    //
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
    
    $("#boton-Registrar").click(validacion);
    $("input[dataid]").each(autocompletado);
});