$(function){

    validacion = function () {


        var $ing_egr_Nombre = $("#ing-egr-nombre");
        var $ing_egr_Descripcion = $("#ing-egr-descripcion");
        var $ing_egr_Fecha = $("#ing-egr-fecha");
        var $ing_egr_Monto = $("#ing-egr-fecha");

        var vnombre = validar_nombre($ing_egr_Nombre.val());
        var vdescripcion = validar_descripcion($ing_egr_Descripcion.val());
        var vfecha = validar_fecha($ing_egr_Fecha.val());
        var vmonto = validar_monto($ing_egr_Monto.val());

        if (vnombre === false || vdescripcion === false || vfecha === false || vmonto === false) {
            return false;
        }
        else {

        }

   
    }

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
//-------------------------------------------------------------------------------------------------------------------------------
    function validar_nombre(nombre) {
        if (nombre === "") {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe estar vacío");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }
        else if (nombre === "") {
            adderror("ing-egr-nombre");
            negativeattributes("error-ing-egr-nombre", "El nombre no debe estar vacío");
            $("#ing-egr-nombre").focus();
            $("#ing-egr-nombre").keyup(KeyNombre);
            return false;
        }


    }

    function validar_descripcion(descripcion) {

    }
    function validar_fecha(fecha) {

    }

    function validar_monto(monto) {

    }
//---------------------------------------------------------------------------------------------------------------------------------
    var KeyNombre=function(){

    }
    var KeyDescripcion = function () {

    }
    var KeyFecha = function () {

    }
    var KeyMonto = function () {

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
}