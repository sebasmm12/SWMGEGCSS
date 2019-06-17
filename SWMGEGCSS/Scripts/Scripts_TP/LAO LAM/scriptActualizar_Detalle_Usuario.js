$(function () {

    validacion = function () {
        //declaracion de variables jquery
        var $det_usu_nombre = $("#det_usu_nombre");
        var $det_usu_direccion = $("#det_usu_direccion");
        var $det_usu_telefono = $("#det_usu_telefono");
     //   var $det_usu_tip_doc_numero = $("#det_usu_tip_doc_numero");
        var $det_usu_correo = $("#det_usu_correo");
        var $det_usu_codigoColegio = $("#det_usu_codigoColegio");
        var $det_usu_especialidad = $("#det_usu_especialidad");


        var vdet_usu_nombre = validar_det_usu_nombre($det_usu_nombre.val());
        var vdet_usu_direccion = validar_det_usu_direccion($det_usu_direccion.val());
        var vdet_usu_telefono = validar_det_usu_telefono($det_usu_telefono.val());
     //  var vdet_usu_tip_doc_numero = validar_det_usu_tip_doc_numero($det_usu_tip_doc_numero.val());
        var vdet_usu_correo = validar_det_usu_correo($det_usu_correo.val());
        var vdet_usu_codigoColegio = validar_det_usu_codigoColegio($det_usu_codigoColegio.val());
        var vdet_usu_especialidad = validar_det_usu_especialidad($det_usu_especialidad.val());


        if (vdet_usu_nombre === false || vdet_usu_direccion === false || vdet_usu_telefono === false || vdet_usu_correo === false || vdet_usu_codigoColegio === false || vdet_usu_especialidad===false) {
        
          return false;
        }
        else {
        var usuario = {
            det_usu_nombre: $("#det_usu_nombre").val(),
            det_usu_direccion: $("#det_usu_direccion").val(),
            det_usu_telefono: $("#det_usu_telefono").val(),
       
            det_usu_correo: $("#det_usu_correo").val(),
            det_usu_codigoColegio: $("#det_usu_codigoColegio").val(),
            det_usu_especialidad: $("#det_usu_especialidad").val()

        };
        $.ajax({
            url: "/Account/Gestionar_Datos_Personales",
            method: "POST",
            data: { usuario: usuario }
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se actualizó su información exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Account/Gestionar_Datos_Personales";
                    }
                });
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
    var esnegativo = function esNegativo(X) {
        if (X < 0) {
            return true;
        }
        else {
            return false;
        }
    };
    var maximoNumeroCaracteres50 = function maxCharacters(X) {
        if (X.length > 50) {
            return true;
        } else {
            return false;
        }
    };

    var maximoNumeroCaracteres70 = function maxCharacters(X) {
        if (X.length > 70) {
            return true;
        } else {
            return false;
        }
    };

    var tieneCaracEsp = function empiezaConCaracteresEspeciales(X) {
        var iChars = "!@_$%^&*()+=[]\\\';,/{}|\":<>?";
      
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//numero es un CE o un numero
                return true;
            }
           
        }
        return false;
    };

    var maximoNumeroCaracteres9 = function maxCharacters(X) {
        if (X.length > 9) {
            return true;
        } else {
            return false;
        }
    };
    var minimoNumeroCaracteres7 = function maxCharacters(X) {
        if (X.length < 7) {
            return true;
        } else {
            return false;
        }
    };

    ////////////////////////////////////////////////////////////////////////
        function validar_det_usu_nombre(nombre) {
            var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
            if (nombre.match(RegularExpression)) {
                adderror("det_usu_nombre");
                negativeattributes("error-det-usu-nombre", 'Ingrese nombre válido');
                $("#det_usu_nombre").focus();
                $("#det_usu_nombre").keyup(KeyNombre);
                return false;
            }
            else if (nombre === "") {
                adderror("det_usu_nombre");
                negativeattributes("error-det-usu-nombre", 'Ingrese nombre');
                $("#det_usu_nombre").focus();
                $("#det_usu_nombre").keyup(KeyNombre);
                return false;
            }
            else if (maximoNumeroCaracteres50(nombre)===true) {
                adderror("det_usu_nombre");
                negativeattributes("error-det-usu-nombre", 'Ingrese un nombre menor a 50 caracteres');
                $("#det_usu_nombre").focus();
                $("#det_usu_nombre").keyup(KeyNombre);
                return false;
            }
            else {
                addgood("det_usu_nombre");
                attributes("error-det-usu-nombre");
                return  true;
            }
        }

    var KeyNombre = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#det_usu_nombre");

        if ($valor.val().match(RegularExpression)) {
            adderror("det_usu_nombre");
            negativeattributes("error-det-usu-nombre", 'Ingrese nombre válido');
        }
        else if ($valor.val() === "") {
            adderror("det_usu_nombre");
            negativeattributes("error-det-usu-nombre", 'Ingrese nombre');
        }
        else if (maximoNumeroCaracteres50($valor.val())==true) {
            adderror("det_usu_nombre");
            negativeattributes("error-det-usu-nombre", 'Ingrese un nombre menor a 50 caracteres');
        }
        else {
            addgood("det_usu_nombre");
            attributes("error-det-usu-nombre");

        }

    }
     //------------------------------------------------------------------------
        function validar_det_usu_direccion(direccion) {
            if (direccion === "") {
                adderror("det_usu_direccion");
                negativeattributes("error-det-usu-direccion", 'Ingrese direccion');
                $("#det_usu_direccion").focus();
                $("#det_usu_direccion").keyup(KeyDireccion);
                return false;
            }
            else if (tieneCaracEsp(direccion) === true) {
                adderror("det_usu_direccion");
                negativeattributes("error-det-usu-direccion", 'Ingrese direccion válida');
                $("#det_usu_direccion").focus();
                $("#det_usu_direccion").keyup(KeyDireccion);
                return false;

            }
            else if (maximoNumeroCaracteres70(direccion)===true) {
                adderror("det_usu_direccion");
                negativeattributes("error-det-usu-direccion", 'Ingrese una dirección menor a 70 caracteres');
                $("#det_usu_direccion").focus();
                $("#det_usu_direccion").keyup(KeyDireccion);
                return false;
            }
            else {
                addgood("det_usu_direccion");
                attributes("error-det-usu-direccion");
                return true;
            }
        }

    var KeyDireccion = function () {
        var $valor = $("#det_usu_direccion");

        if ($valor.val() === "") {
            adderror("det_usu_direccion");
            negativeattributes("error-det-usu-direccion", 'Ingrese direccion');
        }
        else if (tieneCaracEsp($valor.val())===true) {
            adderror("det_usu_direccion");
            negativeattributes("error-det-usu-direccion", 'Ingrese direccion válida');
        }
        else if (maximoNumeroCaracteres70($valor.val())===true) {
            adderror("det_usu_direccion");
            negativeattributes("error-det-usu-direccion", 'Ingrese una dirección menor a 70 caracteres');
        }
        else {
            addgood("det_usu_direccion");
            attributes("error-det-usu-direccion");
        }

    }
     //------------------------------------------------------------------------
        function validar_det_usu_telefono(telefono) {
            var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;

            if (telefono.match(RegularExpression)) {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese telefono válida');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else if (telefono === "") {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese telefono');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else if (minimoNumeroCaracteres7(telefono)===true) {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese un telefono mayor a 6 caracteres');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else if (maximoNumeroCaracteres9(telefono)===true) {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese un telefono menor a 10 caracteres');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else if (esNum(telefono)===false) {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese un telefono númerico');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else if (esnegativo(telefono)===true) {
                adderror("det_usu_telefono");
                negativeattributes("error-det-usu-telefono", 'Ingrese un telefono válido');
                $("#det_usu_telefono").focus();
                $("#det_usu_telefono").keyup(KeyTelefono);
                return false;
            }
            else {
                addgood("det_usu_telefono");
                attributes("error-det-usu-telefono");
                return true;
            }
            
    }

    var KeyTelefono = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#det_usu_telefono");

        if ($valor.val().match(RegularExpression)) {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese direccion válida');

        }
        else if ($valor.val() === "") {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese telefono');
        }
        else if (minimoNumeroCaracteres7($valor.val())===true) {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese un telefono mayor a 6 caracteres');
        }
        else if (maximoNumeroCaracteres9($valor.val())===true) {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese un telefono menor a 10 caracteres');
        }
        else if (esNum($valor.val()) === false) {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese un telefono númerico');
        }
        else if (esnegativo($valor.val()) === true) {
            adderror("det_usu_telefono");
            negativeattributes("error-det-usu-telefono", 'Ingrese un telefono válido');
        }
        else {
            addgood("det_usu_telefono");
            attributes("error-det-usu-telefono");
        }

    }
     //------------------------------------------------------------------------
        function validar_det_usu_correo(correo) {
            var regular = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            if (regular.test(correo) === false) {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email válido');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else if (correo==="") {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else if(maximoNumeroCaracteres50(correo)===true){
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese un Email menor a 50 caracteres');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else {
                addgood("det_usu_correo");
                attributes("error-det-usu-correo");
                return true;
            }

        }

    var KeyCorreo = function () {
        var regular = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        var $valor = $("#det_usu_correo");
        if (regular.test($valor.val()) === false) {
            adderror("det_usu_correo");
            negativeattributes("error-det-usu-correo", 'Ingrese Email válido');
        }
        else if ($valor.val() === "") {
            adderror("det_usu_correo");
            negativeattributes("error-det-usu-correo", 'Ingrese Email');
        }
        else if (maximoNumeroCaracteres50($valor.val())===true) {
            adderror("det_usu_correo");
            negativeattributes("error-det-usu-correo", 'Ingrese un Email menor a 50 caracteres');
        }
        else {
            addgood("det_usu_correo");
            attributes("error-det-usu-correo");
        }

    }
     //------------------------------------------------------------------------
        function validar_det_usu_codigoColegio(colegio) {
            var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
            if (colegio.match(RegularExpression)) {
                adderror("det_usu_codigoColegio");
                negativeattributes("error-det-usu-colegio", 'Ingrese código válido');
                $("#det_usu_codigoColegio").focus();
                $("#det_usu_codigoColegio").keyup(KeyColegio);
                return false;
            }
            else if (colegio==="") {
                adderror("det_usu_codigoColegio");
                negativeattributes("error-det-usu-colegio", 'Ingrese código');
                $("#det_usu_codigoColegio").focus();
                $("#det_usu_codigoColegio").keyup(KeyColegio);
                return false;
            }
            else if (maximoNumeroCaracteres50(colegio)===true) {
                adderror("det_usu_codigoColegio");
                negativeattributes("error-det-usu-colegio", 'Ingrese un código de colegiatura menor a 50 caracteres');
                $("#det_usu_codigoColegio").focus();
                $("#det_usu_codigoColegio").keyup(KeyColegio);
                return false;
            } 
            else {
                addgood("det_usu_codigoColegio");
                attributes("error-det-usu-colegio");
                return true;
            }
        }

    var KeyColegio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#det_usu_codigoColegio");
        if ($valor.val().match(RegularExpression)) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error-det-usu-colegio", 'Ingrese código válido');
        }
        else if ($valor.val() === "") {
            adderror("det_usu_codigoColegio");
            negativeattributes("error-det-usu-colegio", 'Ingrese código');
        }
        else if (maximoNumeroCaracteres50($valor.val())===true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error-det-usu-colegio", 'Ingrese un código de colegiatura menor a 50 caracteres');
        } 
        else {
            addgood("det_usu_codigoColegio");
            attributes("error-det-usu-colegio");
        }
    }
     //------------------------------------------------------------------------
        function validar_det_usu_especialidad(especialidad) {
            var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
            if (especialidad === "") {
                adderror("det_usu_especialidad");
                negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad');
                $("#det_usu_especialidad").focus();
                $("#det_usu_especialidad").keyup(KeyEspecialidad);
                return false;
            }
            else if (maximoNumeroCaracteres50(especialidad)==true) {
                adderror("det_usu_especialidad");
                negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad menor a 50 caracteres');
                $("#det_usu_especialidad").focus();
                $("#det_usu_especialidad").keyup(KeyEspecialidad);
                return false;
            }
          else if (especialidad.match(RegularExpression)) {
                adderror("det_usu_especialidad");
                negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad valida');
                $("#det_usu_especialidad").focus();
                $("#det_usu_especialidad").keyup(KeyEspecialidad);
                return false;
            }
            else {
                addgood("det_usu_especialidad");
                attributes("error-det-usu-especialidad");
                return true;
            }

        }

    var KeyEspecialidad = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#det_usu_especialidad");
        if ($valor.val() === "") {
            adderror("det_usu_especialidad");
            negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad');
        }
        else if (maximoNumeroCaracteres50($valor.val()) == true) {
            adderror("det_usu_especialidad");
            negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad menor a 50 caracteres');
        }
       else if ($valor.val().match(RegularExpression)) {
            adderror("det_usu_especialidad");
            negativeattributes("error-det-usu-especialidad", 'Ingrese Especialidad valida');
        }
        else {
            addgood("det_usu_especialidad");
            attributes("error-det-usu-especialidad");
        }
    }
     //------------------------------------------------------------------------

















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
    $("#boton-modificar").click(validacion);
});