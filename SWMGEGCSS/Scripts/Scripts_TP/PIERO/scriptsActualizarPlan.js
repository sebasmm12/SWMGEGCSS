$(function () {
    validacion = function () {
        //declaracion de variables jquery
        var $planNombre = $("#plan-nombre");
        var $planFecha = $("#plan-fecha");
        var $planCosto = $("#plan-costo");
        /*var $planTiempo = $("#plan-tiempo");
        var $planEmp = $("#plan-emp");
        var $planTipoServicio = $("#plan-tipo-servicio");*/

        var vnombre = validar_nombre_plan($planNombre.val());
        var vfecha = validar_fecha_plan($planFecha.val());
        var vcosto = validar_costo_plan($planCosto.val());
        /*var vtiempo = validar_tiempo_plan($planTiempo.val());
        var vemp = validar_emp_plan($planEmp.val());
        var vtiposervicio = validar_tipo_servicio_plan($planTipoServicio.val());*/

        if (vnombre === false|| vfecha === false /*|| vcosto === false || vtiempo === false || vemp === false || vtiposervicio === false*/) {
            return false;
        }
        else {
            /*$.ajax({
                url: "/Plan/Actualizar_Plan_de_Proyectos",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {*/
                /*Swal.fire({
                    type: 'success',
                    title: 'Se registro el plan exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
                    }
                });*/
                console.log("XD");
            //});
        }
    };
    
    var esNum = function esNumero(txt) {
        if (isNaN(txt)) {
            return false;
        } else {
            return true;
        }
    };
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
        if (iNum.indexOf(X.charAt(0)) !== -1) {
            return true;
        }
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//1er numero es un CE o un numero
                return true;
            }
        }
        return false;
    };

    //Validacion Nombre PLan
    function validar_nombre_plan(id) {
        var vnombre = 0;
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
            negativeattributes("error-plan-nombre", 'El nombre debe empezar con una letra y no debe ser caracter especial');
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
        var $valor = $("#plan-nombre");
        if ($valor.val() === "") {
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
            negativeattributes("error-plan-nombre", 'El nombre debe empezar con una letra y no debe ser caracter especial');
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
    function validar_costo_plan(id) {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-costo");
        if (id === "") {
            adderror("plan-costo");
            negativeattributes("error-plan-costo", 'Debe ingresar un numero');
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
        if ($valor.val() === "") {
            negativeattributes("error-plan-costo", 'Debe ingresar un numero');
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
    }
    ////Validacion Tiempo
    //function validar_tiempo_plan(id) {
    //    if (id === "") {
    //        adderror("plan-tiempo");
    //        negativeattributes("error-plan-tiempo", 'Debe ingresar un número');
    //        $("#plan-tiempo").focus();
    //        $("#plan-tiempo").keyup(keyT);
    //        return false;
    //    }
    //    if (id < 0 || id.match("/^\d+[.]*\d*$/")) {
    //        adderror("plan-tiempo");
    //        negativeattributes("error-plan-tiempo", 'El número debe ser positivo y entero');
    //        $("#plan-tiempo").focus();
    //        $("#plan-tiempo").keyup(keyT);
    //        return false;
    //    }
    //    else { return true; }
    //}
    //function keyT() {
    //    var $valor = $("#plan-tiempo");
    //    if ($valor.val() === "") {
    //        negativeattributes("error-plan-tiempo", 'Debe ingresar un número');
    //        adderror("plan-tiempo");
    //    }
    //    else if ($valor.val() < 0 || $valor.val().match("/^\d+[.]*\d*$/")) {
    //        negativeattributes("error-plan-tiempo", 'El número debe ser positivo y entero');
    //        adderror("plan-tiempo");
    //    }
    //    else {
    //        attributes("error-plan-tiempo");
    //        addgood("plan-tiempo");
    //    }
    //}
    ////Validacion Empresa
    //function validar_emp_plan(id) {
    //    var vemp = 0;
    //    if (id === "") {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'Debe ingresar un nombre');
    //        $("#plan-emp").focus();
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    if (id === " ") {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'El nombre de la empresa no debe empezar con un espacio en blanco');
    //        $("#plan-emp").focus();
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    if (esNum(id) === true) {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'El nombre no puede ser un número');
    //        $("#plan-emp").focus();
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    if (tieneCaracEsp(id) === true) {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
    //        $("#plan-emp").focus();
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    if (maximoNumeroCaracteres50(id) === true) {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'El nombre debe ser de menos de 50 caracteres');
    //        $("#plan-emp").focus();
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    else {
    //        $.ajax({
    //            url: "/Plan/Evaluar_Nombre_Empresa_Plan",
    //            method: "GET",
    //            async: false,
    //            data: {
    //                plan_emp: $("#plan-emp").val()
    //            },
    //            dataType: "json"
    //        }).done(function (data) {
    //            if (data !== 0) {
    //                vemp = 1;
    //            }
    //        });
    //    }
    //    if (vemp === 0) {
    //        adderror("plan-emp");
    //        negativeattributes("error-plan-emp", 'El nombre de esta empresa no existe');
    //        $("#plan-emp").keyup(keyE);
    //        return false;
    //    }
    //    else { return true;}
    //}
    //var keyE = function () {
    //    var $valor = $("#plan-emp");
    //    if ($valor.val() === "") {
    //        negativeattributes("error-plan-emp", 'Debe ingresar un nombre');
    //        adderror("plan-emp");
    //    }
    //    else if ($valor.val() === " ") {
    //        negativeattributes("error-plan-emp", 'El nombre de la empresa no debe empezar con un espacio en blanco');
    //        adderror("plan-emp");
    //    }
    //    else if (esNum($valor.val()) === true) {
    //        negativeattributes("error-plan-emp", 'El nombre no puede ser un número');
    //        adderror("plan-emp");
    //    }
    //    else if (tieneCaracEsp($valor.val()) === true) {
    //        negativeattributes("error-plan-emp", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
    //        adderror("plan-emp");
    //    }
    //    else if (maximoNumeroCaracteres50($valor.val()) === true) {
    //        negativeattributes("error-plan-emp", 'El nombre debe ser de menos de 50 caracteres');
    //        adderror("plan-emp");
    //    }
    //    else {
    //        $.ajax({
    //            url: "/Plan/Evaluar_Nombre_Empresa_Plan",
    //            method: "GET",
    //            async: false,
    //            data: {
    //                plan_emp: $("#plan-emp").val()
    //            },
    //            dataType: "json"
    //        }).done(function (data) {
    //            if (data !== 0) {
    //                attributes("error-plan-emp");
    //                addgood("plan-emp");
    //            }
    //            else {
    //                adderror("plan-emp");
    //                negativeattributes("error-plan-emp", 'El nombre de esta empresa no existe');
    //            }
    //        });
    //    }

    //};
    ////Validacion Tipo Servicio
    //function validar_tipo_servicio_plan(id) {
    //    var vtiposervicio = 0;
    //    if (id === "") {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'Debe ingresar un nombre');
    //        $("#plan-tipo-servicio").focus();
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    if (id === " ") {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre del tipo servicio no debe empezar con un espacio en blanco');
    //        $("#plan-tipo-servicio").focus();
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    if (esNum(id) === true) {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre no puede ser un número');
    //        $("#plan-tipo-servicio").focus();
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    if (tieneCaracEsp(id) === true) {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre del tipo servicio debe empezar con una letra y no debe ser caracter especial');
    //        $("#plan-tipo-servicio").focus();
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    if (maximoNumeroCaracteres50(id) === true) {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre debe ser de menos de 50 caracteres');
    //        $("#plan-tipo-servicio").focus();
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    else {
    //        $.ajax({
    //            url: "/Plan/Evaluar_Tipo_Servicio_Plan",
    //            method: "GET",
    //            async: false,
    //            data: {
    //                plan_tipo_servicio: $("#plan-tipo-servicio").val()
    //            },
    //            dataType: "json"
    //        }).done(function (data) {
    //            if (data !== 0) {
    //                vtiposervicio = 1;
    //            }
    //        });
    //    }
    //    if (vtiposervicio === 0) {
    //        adderror("plan-tipo-servicio");
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre de este servicio no existe');
    //        $("#plan-tipo-servicio").keyup(keyTS);
    //        return false;
    //    }
    //    else { return true; }
    //}
    //var keyTS = function () {
    //    var $valor = $("#plan-tipo-servicio");
    //    if ($valor.val() === "") {
    //        negativeattributes("error-plan-tipo-servicio", 'Debe ingresar un nombre');
    //        adderror("plan-tipo-servicio");
    //    }
    //    else if ($valor.val() === " ") {
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa no debe empezar con un espacio en blanco');
    //        adderror("plan-tipo-servicio");
    //    }
    //    else if (esNum($valor.val()) === true) {
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre no puede ser un número');
    //        adderror("plan-tipo-servicio");
    //    }
    //    else if (tieneCaracEsp($valor.val()) === true) {
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
    //        adderror("plan-tipo-servicio");
    //    }
    //    else if (maximoNumeroCaracteres50($valor.val()) === true) {
    //        negativeattributes("error-plan-tipo-servicio", 'El nombre debe ser de menos de 50 caracteres');
    //        adderror("plan-tipo-servicio");
    //    }
    //    else {
    //        $.ajax({
    //            url: "/Plan/Evaluar_Tipo_Servicio_Plan",
    //            method: "GET",
    //            async: false,
    //            data: {
    //                plan_tipo_servicio: $("#plan-tipo-servicio").val()
    //            },
    //            dataType: "json"
    //        }).done(function (data) {
    //            if (data !== 0) {
    //                attributes("error-plan-tipo-servicio");
    //                addgood("plan-tipo-servicio");
    //            }
    //            else {
    //                adderror("plan-tipo-servicio");
    //                negativeattributes("error-plan-tipo-servicio", 'El nombre de este servicio no existe');
    //            }
    //        });
    //    }

    //};
    
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
    
        /*var planNombre = document.getElementById("plan-nombre");
        var planFecha = document.getElementById("plan-fecha");
        var planEmp = document.getElementById("plan-emp");
        var planCosto = document.getElementById("plan-costo");
        var planTipoServicio = document.getElementById("plan-tipo-servicio");
        var planTiempo = document.getElementById("plan-tiempo");

        var erplanNombre = document.getElementById("error-plan-nombre");
        var erplanFecha = document.getElementById("error-plan-fecha");
        var erplanCosto = document.getElementById("error-plan-costo");
        var erplanTiempo = document.getElementById("error-plan-tiempo");
        var erplanEmp = document.getElementById("error-plan-emp");
        var erplanTipoServicio = document.getElementById("error-plan-tipo-servicio");

        function esNumero(txt, erdiv) {
            if (isNaN(txt.value)) {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo debe ser numérico";
                txt.style.border = '2px solid red';
                return 0;
            } else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }

        function validarVacio(txt, erdiv) {
            if (txt.value === '') {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo es obligatorio";
                txt.style.border = '2px solid red';
                return 0;
            }
            else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }
        function tieneCaracteresEspeciales(X) {
            var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
            for (var i = 0; i < X.value.length; i++) {
                if (iChars.indexOf(X.value.charAt(i)) !== -1) { return true; }
            }
            return false;
        }

        function validarDatos(txt, erdiv) {
            if (txt.value === '') {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo es obligatorio";
                txt.style.border = '2px solid red';
                return 0;
            }
            if (tieneCaracteresEspeciales(txt)) {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo no puede contener caracteres especiales";
                txt.style.border = '2px solid red';
                return 0;
            }
            else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }

        if (validarDatos(planNombre, erplanNombre) === 0) {
            $("#plan-nombre").focus();
            return false;
        }
        if (validarVacio(planFecha, erplanFecha) === 0) {
            $("#plan-fecha").focus();
            return false;
        }
        if (validarDatos(planEmp, erplanEmp) === 0) {
            $("#plan-emp").focus();
            return false;
        }
        if (validarDatos(planCosto, erplanCosto) === 0) {
            $("#plan-costo").focus();
            return false;
        }
        if (esNumero(planCosto, erplanCosto) === 0) {
            $("#plan-costo").focus();
            return false;
        }
        if (validarDatos(planTipoServicio, erplanTipoServicio) === 0) {
            $("#plan-tipo-servicio").focus();
            return false;
        }
        if (validarDatos(planTiempo, erplanTiempo) === 0) {
            $("#plan-tiempo").focus();
            return false;
        }*/
    $("#boton-Actualizar").click(validacion);
});