$(function () {
    //var id_plan = 0;
    var id_act = 0;
    var SelectorBoton;
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
    var envioajaxRegistrar = function () {
        var $actplannombre = $("#ActPlanNombre");
        var $actplandescripcion = $("#ActPlanDescripcion");
        var $actplancosto = $("#ActPlanCosto");
        var $actplantiempo = $("#ActPlanTiempo");

        var vnombre = validar_nombre($actplannombre.val());
        var vdescripcion = validar_descripcion($actplandescripcion.val());
        var vcosto = validar_costo($actplancosto.val());
        var vtiempo = validar_tiempo($actplantiempo.val());

        if (vnombre === false || vdescripcion === false || vcosto === false || vtiempo === false) {
            return false;
        }
        else {
            var act_plan = {
                act_id: id_act,
                act_plan_nombre: $("#ActPlanNombre").val(),
                act_plan_descripcion: $("#ActPlanDescripcion").val(),
                act_plan_costo: $("#ActPlanCosto").val(),
                act_plan_tiempo: $("#ActPlanTiempo").val()
            };
            /*var act_plan = {
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
            //$(modal).modal("hide");
        });*/
            $.ajax({
                url: "/Plan/_ModalRegistrarActividadesPlanificadas",
                method: "POST",
                data: {
                    act_plan: act_plan
                }
            }).done(function (data) {
                $('#RegistrarActividad').modal('hide');
                //cambiar de color al boton
                $(SelectorBoton).css('background-color', 'red');
                $(SelectorBoton).css('border-color', 'red');
                $(SelectorBoton).text('Actividad Registrada');
                $(SelectorBoton).attr('disabled', 'disabled');
            });
        }
        return false;
    };
    var esNum = function esNumero(txt) {
        if (isNaN(txt)) {
            return false;
        } else {
            return true;
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
        /*************************************************************************/
        
    //validacion nombre actividad
    function validar_nombre(id) {
        if (id === "") {
            adderror("ActPlanNombre");
            negativeattributes("error_act_plan_nombre", 'Debe ingresar un nombre');
            $("#ActPlanNombre").focus();
            $("#ActPlanNombre").keyup(keyN);
            return false;
        }
        if (id === " ") {
            adderror("ActPlanNombre");
            negativeattributes("error_act_plan_nombre",'El nombre no debe empezar con un espacio en blanco');
            $("#ActPlanNombre").focus();
            $("#ActPlanNombre").keyup(keyN);
            return false;
        }
        if (esNum(id) === true) {
            adderror("ActPlanNombre");
            negativeattributes("error_act_plan_nombre",'El nombre no puede ser un número');
            $("#ActPlanNombre").focus();
            $("#ActPlanNombre").keyup(keyN);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("ActPlanNombre");
            negativeattributes("error_act_plan_nombre",'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#ActPlanNombre").focus();
            $("#ActPlanNombre").keyup(keyN);
            return false;
        }
        return true;
    }
    var keyN = function () {
        var $valor = $("#ActPlanNombre");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_nombre", 'Debe ingresar un nombre');
            adderror("ActPlanNombre");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error_act_plan_nombre", 'El nombre no debe empezar con un espacio en blanco');
            adderror("ActPlanNombre");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_act_plan_nombre", 'El nombre no puede ser un número');
            adderror("ActPlanNombre");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("ActPlanNombre");
        }
        else {
            attributes("error_act_plan_nombre");
            addgood("ActPlanNombre");
        }
    };
    //validacion descripcion actividad
    function validar_descripcion(id) {
        if (id === "") {
            adderror("ActPlanDescripcion");
            negativeattributes("error_act_plan_descripcion", 'Debe ingresar un nombre en la descripcion');
            $("#ActPlanDescripcion").focus();
            $("#ActPlanDescripcion").keyup(keyD);
            return false;
        }
        if (id === " ") {
            adderror("ActPlanDescripcion");
            negativeattributes("error_act_plan_descripcion", 'La descripcion no debe empezar con un espacio en blanco');
            $("#ActPlanDescripcion").focus();
            $("#ActPlanDescripcion").keyup(keyD);
            return false;
        }
        if (esNum(id) === true) {
            adderror("ActPlanDescripcion");
            negativeattributes("error_act_plan_descripcion", 'La descripcion no puede ser un número');
            $("#ActPlanDescripcion").focus();
            $("#ActPlanDescripcion").keyup(keyD);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("ActPlanDescripcion");
            negativeattributes("error_act_plan_descripcion", 'La descripcion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#ActPlanDescripcion").focus();
            $("#ActPlanDescripcion").keyup(keyD);
            return false;
        }
        return true;
    }
    var keyD = function () {
        var $valor = $("#ActPlanDescripcion");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_descripcion", 'Debe ingresar un nombre en la descripcion');
            adderror("ActPlanDescripcion");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error_act_plan_descripcion", 'La descripcion no debe empezar con un espacio en blanco');
            adderror("ActPlanDescripcion");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_act_plan_descripcion", 'La descripcion no puede ser un número');
            adderror("ActPlanDescripcion");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_descripcion", 'La descripcion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("ActPlanDescripcion");
        }
        else {
            attributes("error_act_plan_descripcion");
            addgood("ActPlanDescripcion");
        }
    };
    //validacion costo actividad
    function validar_costo(id) {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#ActPlanCosto");
        if (id === "" || id <= 0) {
            adderror("ActPlanCosto");
            negativeattributes("error_act_plan_costo", 'Debe ingresar un numero positivo');
            $("#ActPlanCosto").keyup(keyC);
            return false;
        }
        else {
            if ($valor.val().match(RegularExpression)) {
                attributes("error_act_plan_costo");
                addgood("ActPlanCosto");
            } else {
                negativeattributes("error_act_plan_costo", 'Debe ingresar un numero positivo');
                adderror("plan-costoActPlanCosto");
                $("#ActPlanCosto").keyup(keyC);
                return false;
            }
        }
        return true;
    }
    function keyC() {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#ActPlanCosto");
        if ($valor.val() === "" || $valor.val() <= 0) {
            negativeattributes("error_act_plan_costo", 'Debe ingresar un numero positivo');
            adderror("ActPlanCosto");
        } else {
            if ($valor.val().match(RegularExpression)) {
                attributes("error_act_plan_costo");
                addgood("ActPlanCosto");
            } else {
                negativeattributes("error_act_plan_costo", 'Debe ingresar un numero positivo');
                adderror("ActPlanCosto");
            }
        }
    }

    //Validacion tiempo
    function validar_tiempo(id) {
        if (id === "") {
            adderror("ActPlanTiempo");
            negativeattributes("error_act_plan_tiempo", 'Debe ingresar un número');
            $("#ActPlanTiempo").focus();
            $("#ActPlanTiempo").keyup(keyT);
            return false;
        }
        if (id <= 0) {
            adderror("ActPlanTiempo");
            negativeattributes("error_act_plan_tiempo", 'El número debe ser positivo y mayor que 0');
            $("#ActPlanTiempo").focus();
            $("#ActPlanTiempo").keyup(keyT);
            return false;
        }
        return true;
    }
    function keyT() {
        var $valor = $("#ActPlanTiempo");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_tiempo", 'Debe ingresar un número');
            adderror("ActPlanTiempo");
        }
        else if ($valor.val() <= 0) {
            negativeattributes("error_act_plan_tiempo", 'El número debe ser positivo y mayor que 0');
            adderror("ActPlanTiempo");
        }
        else {
            attributes("error_act_plan_tiempo");
            addgood("ActPlanTiempo");
        }
    }

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_act = $button.attr("data-id-act");
            SelectorBoton = $(this);
            $.ajax({
                url: $(this).attr("data-url"),
                method: "GET"/*,
                data: {
                    id_act: id_act
                }*/
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnRegistrarActividades").click(envioajaxRegistrar);
                //alert($button.attr("data-id-act")); funciona
            });
            return false;
        });
    };
    $(".btnModal").each(envioajaxModal);
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
            $(".btnModal").each(envioajaxModal);
        });
        return false;
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
    function negativeattributes(id, tipo) {
        $("#" + id).removeClass("textsuccess");
        $("#" + id).addClass("text-danger");
        $("#" + id).html("");
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo + "</label > ");
    }

    $("#btnPrueba").click(TablaActiv);
    //$("#btnMostrar").click(Verify);
    $(".btnRegistrarActividades").click(envioajaxRegistrar);
});