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

        var $actplannombre = $("#ActPlanNombre1");
        var $actplandescripcion = $("#ActPlanDescripcion1");
        var $actplancosto = $("#ActPlanCosto1");
        var $actplantiempo = $("#ActPlanTiempo1");

        var vnombre = validar_nombre($actplannombre.val());
        var vdescripcion = validar_descripcion($actplandescripcion.val());
        var vcosto = validar_costo($actplancosto.val());
        var vtiempo = validar_tiempo($actplantiempo.val());

        if (vnombre === false || vdescripcion === false || vcosto === false || vtiempo === false) {
            return false;
        }
        else {
            var act_plan = {
                plan_id: id_plan,
                act_id: id_act,
                act_plan_nombre: $("#ActPlanNombre1").val(),
                act_plan_descripcion: $("#ActPlanDescripcion1").val(),
                act_plan_costo: $("#ActPlanCosto1").val(),
                act_plan_tiempo: $("#ActPlanTiempo1").val()
            };
            $.ajax({
                url: "/Plan/_ModalRegistrarActividadesPlanificadasFinal",
                method: "POST",
                data: {
                    act_plan: act_plan
                }
            }).done(function (data) {
                window.location.href = "/Plan/Actualizar_Plan_de_Proyectos?id=" + data;
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
    ////
    var tieneLetraCaracterEsp = function empiezaConCaracteresEspecialesLetra(X) {
        var iChars = "!@#_$%^&*()+=[]\\\';,./{}|\":<>?-";
        var iLetra = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnÑñOoPpQqRrSsTtUuVvWwXxYyZz";
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//es un CE?
                return true;
            }
            if (iLetra.indexOf(X.charAt(i)) !== -1) {//es una Letra?
                return true;
            }
        }
        return false;
    };
    /*************************************************************************/

    //validacion nombre actividad
    function validar_nombre(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;

        if (id === "") {
            adderror("ActPlanNombre1");
            negativeattributes("error_act_plan_nombre1", 'Debe ingresar un nombre');
            $("#ActPlanNombre1").focus();
            $("#ActPlanNombre1").keyup(keyN);
            return false;
        }
        if ($("#ActPlanNombre1").val().match(RegularExpression)) {
            adderror("ActPlanNombre1");
            negativeattributes("error_act_plan_nombre1", 'El nombre no debe empezar con un espacio en blanco');
            $("#ActPlanNombre1").focus();
            $("#ActPlanNombre1").keyup(keyN);
            return false;
        }
        if (esNum(id) === true) {
            adderror("ActPlanNombre1");
            negativeattributes("error_act_plan_nombre1", 'El nombre no puede ser un número');
            $("#ActPlanNombre1").focus();
            $("#ActPlanNombre1").keyup(keyN);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("ActPlanNombre1");
            negativeattributes("error_act_plan_nombre1", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#ActPlanNombre1").focus();
            $("#ActPlanNombre1").keyup(keyN);
            return false;
        }
        return true;
    }
    var keyN = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#ActPlanNombre1");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_nombre1", 'Debe ingresar un nombre');
            adderror("ActPlanNombre1");
        }
        else if ($valor.val().match(RegularExpression)) {
            negativeattributes("error_act_plan_nombre1", 'El nombre no debe empezar con un espacio en blanco');
            adderror("ActPlanNombre1");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_act_plan_nombre1", 'El nombre no puede ser un número');
            adderror("ActPlanNombre1");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_nombre1", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("ActPlanNombre1");
        }
        else {
            attributes("error_act_plan_nombre1");
            addgood("ActPlanNombre1");
        }
    };
    //validacion descripcion actividad
    function validar_descripcion(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (id === "") {
            adderror("ActPlanDescripcion1");
            negativeattributes("error_act_plan_descripcion1", 'Debe ingresar un nombre en la descripcion');
            $("#ActPlanDescripcion1").focus();
            $("#ActPlanDescripcion1").keyup(keyD);
            return false;
        }
        if ($("#ActPlanDescripcion1").val().match(RegularExpression)) {
            adderror("ActPlanDescripcion1");
            negativeattributes("error_act_plan_descripcion1", 'La descripcion no debe empezar con un espacio en blanco');
            $("#ActPlanDescripcion1").focus();
            $("#ActPlanDescripcion1").keyup(keyD);
            return false;
        }
        if (esNum(id) === true) {
            adderror("ActPlanDescripcion1");
            negativeattributes("error_act_plan_descripcion1", 'La descripcion no puede ser un número');
            $("#ActPlanDescripcion1").focus();
            $("#ActPlanDescripcion1").keyup(keyD);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("ActPlanDescripcion1");
            negativeattributes("error_act_plan_descripcion1", 'La descripcion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#ActPlanDescripcion1").focus();
            $("#ActPlanDescripcion1").keyup(keyD);
            return false;
        }
        return true;
    }
    var keyD = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#ActPlanDescripcion1");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_descripcion1", 'Debe ingresar un nombre en la descripcion');
            adderror("ActPlanDescripcion1");
        }
        else if ($("#ActPlanDescripcion1").val().match(RegularExpression)) {
            negativeattributes("error_act_plan_descripcion1", 'La descripcion no debe empezar con un espacio en blanco');
            adderror("ActPlanDescripcion1");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_act_plan_descripcion1", 'La descripcion no puede ser un número');
            adderror("ActPlanDescripcion1");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_descripcion1", 'La descripcion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("ActPlanDescripcion1");
        }
        else {
            attributes("error_act_plan_descripcion1");
            addgood("ActPlanDescripcion1");
        }
    };
    //validacion costo actividad
    function validar_costo(id) {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#ActPlanCosto1");
        if (tieneLetraCaracterEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_costo1", 'No se aceptan numeros ni CE');
            adderror("ActPlanCosto1");
            $("#ActPlanCosto1").keyup(keyC);
            return false;
        }
        else if ($valor.val().match(RegularExpression) === false) {
            negativeattributes("error_act_plan_costo1", 'Debe ingresar un numero valido');
            adderror("ActPlanCosto1");
            $("#ActPlanCosto1").keyup(keyC);
            return false;
        }
        else if ($valor.val() < 0) {
            negativeattributes("error_act_plan_costo1", 'El valor no puede ser negativo ni 0');
            adderror("ActPlanCosto1");
            $("#ActPlanCosto1").keyup(keyC);
            return false;
        }
        else  if (id === "" || document.getElementById("ActPlanCosto1").value === "") {
                var valorSugerido = $("#precioSugerido").text().trim();
                $("#ActPlanCosto1").val(valorSugerido.toString().trim());
                document.getElementById("ActPlanCosto1").value = valorSugerido.trim();

                //("#ActPlanCosto1").val()
                addgood("ActPlanCosto1");
                attributes("error_act_plan_costo1", 'Si no ingresa un valor se tomara el precio sugerido');
                $("#ActPlanCosto1").keyup(keyC);
                return true;
        }
        else {
                addgood("ActPlanCosto1");
            attributes("error_act_plan_costo1");
            $("#ActPlanCosto1").keyup(keyC);
            return true;
        }
    }
    function keyC() {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#ActPlanCosto1");
        if (tieneLetraCaracterEsp($valor.val()) === true) {
            negativeattributes("error_act_plan_costo1", 'No se aceptan numeros ni CE');
            adderror("ActPlanCosto1");
        }
        else if ($valor.val().match(RegularExpression) === false) {
            negativeattributes("error_act_plan_costo1", 'Debe ingresar un numero valido');
            adderror("ActPlanCosto1");
        }
        else if ($valor.val() < 0) {
            negativeattributes("error_act_plan_costo1", 'El valor no puede ser negativo ni 0');
            adderror("ActPlanCosto1");
        }
        else if (document.getElementById("ActPlanCosto1").value === "" ) {
                /*var valorSugerido = $("#precioSugerido").text().trim();
                $("#ActPlanCosto1").val(valorSugerido.toString().trim());
                document.getElementById("ActPlanCosto1").value = valorSugerido.trim();
                */
                //("#ActPlanCosto1").val()
                addgood("ActPlanCosto1");
                attributes("error_act_plan_costo1");
        }
        else {
                addgood("ActPlanCosto1");
            attributes("error_act_plan_costo1");
       }
    }

    //Validacion tiempo
    function validar_tiempo(id) {
        if (id === "") {
            adderror("ActPlanTiempo1");
            negativeattributes("error_act_plan_tiempo1", 'Debe ingresar un número');
            $("#ActPlanTiempo1").focus();
            $("#ActPlanTiempo1").keyup(keyT);
            return false;
        }
        if (id <= 0) {
            adderror("ActPlanTiempo1");
            negativeattributes("error_act_plan_tiempo1", 'El número debe ser positivo y mayor que 0');
            $("#ActPlanTiempo1").focus();
            $("#ActPlanTiempo1").keyup(keyT);
            return false;
        }
        return true;
    }
    function keyT() {
        var $valor = $("#ActPlanTiempo1");
        if ($valor.val() === "") {
            negativeattributes("error_act_plan_tiempo1", 'Debe ingresar un número');
            adderror("ActPlanTiempo1");
        }
        else if ($valor.val() <= 0) {
            negativeattributes("error_act_plan_tiempo1", 'El número debe ser positivo y mayor que 0');
            adderror("ActPlanTiempo1");
        }
        else {
            attributes("error_act_plan_tiempo1");
            addgood("ActPlanTiempo1");
        }
    }
    //$(".btnModal").each(envioajaxModal);
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





    $(".btnModal3").each(envioajaxModal);
    $(".btnRegistrarPlanAct").click(envioajaxRegistrar);
    //$("input[data-exp-autocomplete]").each(autcompletado);
    //$(".pcoded-content").on("click", ".pagedList a", getPage);
    //$("#Buscar").click(BuscarProyecto);
});