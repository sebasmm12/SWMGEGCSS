﻿$(function () {

    validacion = function () {


        var $ing_egr_Nombre = $("#ing-egr-nombre");
        var $ing_egr_Descripcion = $("#ing-egr-descripcion");
        var $ing_egr_Fecha = $("#ing-egr-fecha");
        var $ing_egr_Monto = $("#ing-egr-monto");


        var vnombre = validar_nombre($ing_egr_Nombre.val());
        var vdescripcion = validar_descripcion($ing_egr_Descripcion.val());
        var vfecha = validar_fecha($ing_egr_Fecha.val());
        var vmonto = validar_monto($ing_egr_Monto.val());


        if (vnombre === false || vdescripcion === false || vmonto === false || vfecha === false) {
            return false;
        }
        else {

            $.ajax({
                url: "/Ing_Egr/Registrar_Ing_Egr",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se registró exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Gerente/Gestionar_I_E";
                    }
                });
            });

        }
        return false;


    };
        //validar fecha registrar mismo mes actualizar 28d
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

    var maximoNumeroCaracteres250 = function maxCharacters(X) {
        if (X.length > 250) {
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
        var iChars = "/^[0-9]+$/";
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//numero es un CE o un numero
                return true;
            }
        }
        return false;
    };
    var esnegativo = function esNegativo(X) {
        if (X < 0) {
            return true;
        }
        else {
            return false;
        }
    };
//-------------------------------------------------------------------------------------------------------------------------------
 /*   function validar_ing_egr(ingresos, egresos) {
        if (ingresos !==1 && egresos !== 0) {
            adderror("ingresos");
            adderror("egresos");
            negativeattributes("error-ing-egr-ingreso", "Debe seleccionar un tipo");
          $("#ingresos").keyup(KeyIngreso);
           $("#egresos").keyup(KeyIngreso);
            return false;
        }
        addgood("ingresos");
        addgood("egresos");
        attributes("error-ing-egr-ingreso");
        return true;


    }*/
    function validar_nombre(nombre) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (nombre === "") {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe estar vacío");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (nombre.match(RegularExpression)) {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "Ingrese nombre válido");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (nombre.charAt(0)===' ') {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe comenzar con espacio en blanco");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (tieneCaracEspOnly(nombre)===true) {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe contener caracteres especiales");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (maximoNumeroCaracteres50(nombre)===true) {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe contener más de 50 caracteres");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (esNum(nombre)===true) {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe ser un número");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        addgood("ing-egr-nombre");
        attributes("error-ing-egr-nombre");
        return true;    
    }

    function validar_fecha(fechaa) {
        var fechaingresada = new date($("#ing-egr-fecha").val());
        var fechaactual = new date();

        if (fechaa === "") {
            adderror("ing-egr-fecha");
            negativeattributes("error-ing-egr-fecha", "La fecha no debe estar vacía");
            $("#ing-egr-fecha").focus();
            $("#ing-egr-fecha").keyup(KeyFecha);
            return false;
        }



        addgood("ing-egr-fecha");
        attributes("error-ing-egr-fecha");
        return true;

    }
    function validar_descripcion(descripcion) {
        if (descripcion === "") {
            adderror("ing-egr-descripcion");
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe estar vacío");
            $("#ing-egr-descripcion").focus();
            $("#ing-egr-descripcion").keyup(KeyDescripcion);
            return false;
        }
        else if (descripcion.charAt(0) === ' ') {
            adderror("ing-egr-descripcion");
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe comenzar con espacio en blanco");
            $("#ing-egr-descripcion").focus();
            $("#ing-egr-descripcion").keyup(KeyDescripcion);
            return false;
        }
        else if (tieneCaracEspOnly(descripcion) === true) {
            adderror("ing-egr-descripcion");
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe contener caracteres especiales");
            $("#ing-egr-descripcion").focus();
            $("#ing-egr-descripcion").keyup(KeyDescripcion);
            return false;
        }
        else if (maximoNumeroCaracteres250(descripcion) === true) {
            adderror("ing-egr-descripcion");
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe contener más de 250 caracteres");
            $("#ing-egr-descripcion").focus();
            $("#ing-egr-descripcion").keyup(KeyDescripcion);
            return false;
        }
        else if (esNum(descripcion) === true) {
            adderror("ing-egr-descripcion");
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe ser un número");
            $("#ing-egr-descripcion").focus();
            $("#ing-egr-descripcion").keyup(KeyDescripcion);
            return false;
        }
        addgood("ing-egr-descripcion");
        attributes("error-ing-egr-descripcion");
        return true;
    }


    function validar_monto(monto) {
        if (monto === "") {
            adderror("ing-egr-monto");
            negativeattributes("error-ing-egr-monto", "El monto no debe estar vacío");
            $("#ing-egr-monto").focus();
            $("#ing-egr-monto").keyup(KeyMonto);
            return false;
        }
        else if (monto.charAt(0) === ' ') {
            adderror("ing-egr-monto");
            negativeattributes("error-ing-egr-monto", "El monto no debe comenzar con espacio en blanco");
            $("#ing-egr-monto").focus();
            $("#ing-egr-monto").keyup(KeyMonto);
            return false;
        }
        else if (esNum(monto) === false) {
            adderror("ing-egr-monto");
            negativeattributes("error-ing-egr-monto", "El monto debe ser un número");
            $("#ing-egr-monto").focus();
            $("#ing-egr-monto").keyup(KeyMonto);
            return false;
        }
        else if (esnegativo(monto) === true) {
            adderror("ing-egr-monto");
            negativeattributes("error-ing-egr-monto", "El monto debe ser positivo");
            $("#ing-egr-monto").focus();
            $("#ing-egr-monto").keyup(KeyMonto);
            return false;
        }
        addgood("ing-egr-monto");
        attributes("error-ing-egr-monto");
        return true;
    }
//---------------------------------------------------------------------------------------------------------------------------------
 /*   var KeyIngreso = function () {
        var $ing = $("ingresos");
        var $egr = $("egresos");
        if ($ing.val() !== 1 && $egr.val() !== 0) {
            adderror("ingresos");
            adderror("egresos");
            negativeattributes("error-ing-egr-ingreso", "Debe seleccionar un tipo");

        }
        else {
            addgood("ingresos");
            addgood("egresos");
            attributes("error-ing-egr-ingreso");
        }

    }*/
    var KeyNombre = function () {
        var $valor = $("#ing-egr-nombre");
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if ($valor.val() === "") {
            negativeattributes("error-ing-egr-nombre", "El nombre no debe estar vacío");
            adderror("ing-egr-nombre");
        }
        else if ($valor.val().match(RegularExpression)) {
            negativeattributes("error-ing-egr-nombre", "Ingrese nombre válido");
            adderror("ing-egr-nombre");

        }
        else if ($valor.val().charAt(0) === ' ') {
            negativeattributes("error-ing-egr-nombre", "El nombre no debe comenzar con espacio en blanco");
            adderror("ing-egr-nombre");
        }
        else if (tieneCaracEspOnly($valor.val()) === true) {
            negativeattributes("error-ing-egr-nombre", "El nombre no debe contener caracteres especiales");
            adderror("ing-egr-nombre");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-ing-egr-nombre", "El nombre no debe contener más de 50 caracteres");
            adderror("ing-egr-nombre");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-ing-egr-nombre", "El nombre no debe ser un número");
            adderror("ing-egr-nombre");
        }
        else {
            attributes("error-ing-egr-nombre");
            addgood("ing-egr-nombre");
        }
    }
    var KeyDescripcion = function () {
        var $valor = $("#ing-egr-descripcion");
        if ($valor.val() === "") {
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe estar vacío");
            adderror("ing-egr-descripcion");
        }
        else if ($valor.val().charAt(0) === ' ') {
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe comenzar con espacio en blanco");
            adderror("ing-egr-descripcion");
        }
        else if (tieneCaracEspOnly($valor.val()) === true) {
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe contener caracteres especiales");
            adderror("ing-egr-descripcion");
        }
        else if (maximoNumeroCaracteres250($valor.val()) === true) {
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe contener más de 250 caracteres");
            adderror("ing-egr-descripcion");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-ing-egr-descripcion", "La descripción no debe ser un número");
            adderror("ing-egr-descripcion");
        }
        else {
            attributes("error-ing-egr-descripcion");
            addgood("ing-egr-descripcion");
        }
    }
    var KeyFecha = function () {
        var $valor = $("#ing-egr-fecha");
        if ($valor.val() === "") {
            negativeattributes("error-ing-egr-fecha", "La fecha no debe estar vacía");
            adderror("ing-egr-fecha");
        }
        else {
            attributes("error-ing-egr-fecha");
            addgood("ing-egr-fecha");
        }
    }
    var KeyMonto = function () {
        var $valor = $("#ing-egr-monto");
        if ($valor.val() === "") {
            negativeattributes("error-ing-egr-monto", "El monto no debe estar vacío");
            adderror("ing-egr-monto");
        }
        else if ($valor.val().charAt(0) === ' ') {
            negativeattributes("error-ing-egr-monto", "El monto no debe comenzar con espacio en blanco");
            adderror("ing-egr-monto");
        }
        else if (esNum($valor.val()) === false) {
            negativeattributes("error-ing-egr-monto", "El monto debe ser un número");
            adderror("ing-egr-monto");
        }
        else if (esnegativo($valor.val())===true) {
            negativeattributes("error-ing-egr-monto", "El monto debe ser positivo");
            adderror("ing-egr-monto");
        }
        else {
            attributes("error-ing-egr-monto");
            addgood("ing-egr-monto");
        }

    }
//---------------------------------------------------------------------------------------------------------------------------------
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
});