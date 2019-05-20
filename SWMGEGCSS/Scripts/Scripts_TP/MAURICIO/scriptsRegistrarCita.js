$(function () {
    validacion = function () {
        //declaracion de variables jquery
        var $citaRepresentante = $("#cita_representante");

        //Metodos

        var vCitaRepresentante = validar_representante($citaRepresentante);

        if (vCitaRepresentante === false) {
            return false;
        } else {

            console.log("XD");
        }
    };
    //VALIDACIONES GENERALES
    var esNum = function esNumero(txt) {
        if (isNaN(txt)) {
            return false;
        } else {
            return true;
        }
    };
    var maximoNumeroCaracteres100 = function maxCharacters(X) {
        if (X.length > 100) {
            return true;
        } else {
            return false;
        }
    };
    var maximoNumeroCaracteres200 = function maxCharacters(X) {
        if (X.length > 200) {
            return true;
        } else {
            return false;
        }
    };
    var maximoNumeroCaracteres20 = function maxCharacters(X) {
        if (X.length > 20) {
            return true;
        } else {
            return false;
        }
    };
    var maximoNumeroCaracteres9 = function maxCharacters(X) {
        if (X.length > 9) {
            return true;
        } else {
            return false;
        }
    };
    var minimoNumeroCaracteres7 = function maxCharacters(X) {
        if (X.length < 6) {
            return true;
        } else {
            return false;
        }
    };
    var igual = function maxCharacters(X) {
        if (X.length === 11) {
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
    ///////////////////////////////////////

    //Validacion Representante
    function validar_representante(representante) {

        var vnombre = 0;
        if (representante === "") {
            adderror("cita_representante");
            negativeattributes("error-cita-representante", 'Debe ingresar un representante');
            $("#cita_representante").focus();
            //      alert("representante vacio");
            $("#cita_representante").keyup(keyRepresentante);
            return false;
        }
        if (representante === " ") {
            adderror("cita_representante");
            negativeattributes("error-cita-representante", 'El Representante no debe empezar con un espacio en blanco');
            $("#cita_representante").focus();
            //    alert("representante con espacio");
            $("#cita_representante").keyup(keyRepresentante);
            return false;
        }
        if (esNum(representante) === true) {
            adderror("cita_representante");
            negativeattributes("error-cita-representante", 'El representante no puede ser un número');
            $("#cita_representante").focus();
            //    alert("representante es numero");
            $("#cita_representante").keyup(keyRepresentante);
            return false;
        }
        if (tieneCaracEsp(representante) === true) {
            adderror("cita_representante");
            negativeattributes("error-cita-representante", 'El representante debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#cita_representante").focus();
            //     alert("representante con caES");
            $("#cita_representante").keyup(keyRepresentante);
            return false;
        }
        if (maximoNumeroCaracteres100(representante) === true) {
            adderror("cita_representante");
            negativeattributes("error-cita-representante", 'El representante debe ser de menos de 100 caracteres');
            $("#cita_representante").focus();
            //     alert("representante largo");
            $("#cita_representante").keyup(keyRepresentante);
            return false;
        }
        
        addgood("cita_representante");
        attributes("error-cita-representante");
        
        return true;
    }

    ///NO SE QUE ES PERO ME DIJERON QUE ERA IMPORTANTE
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