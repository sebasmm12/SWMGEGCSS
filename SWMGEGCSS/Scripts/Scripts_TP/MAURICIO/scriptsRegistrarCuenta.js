$(function () {
    validacion = function () {
        //Declaraciónj de variables
        var $nombre = $("#det_usu_nombre").val();
        var $correo = $("#det_usu_correo").val();
        var $direccion = $("#det_usu_direccion").val();
        var $telefono = $("#det_usu_telefono").val();
        var $tipo_doc = $("#det_usu_tip_doc").val();
        var $tipo_usu = $("#tipo_det_usu_tipo").val();
        var $tipo_doc_num = $("#det_usu_tip_doc_numero").val();
        var $sex = $("#det_usu_sexo").val();
        var $colegio = $("#det_usu_codigoColegio").val();
        var $especialidad = $("#det_usu_especialidad").val();
        var $usuario = $("#usu_usuario").val();
        var $contraseña = $("#usu_contraseña").val();
        var $contraseña1 = $("#usu_contraseña_nuv").val();
        var $rol = $("#rol_codigo").val();

        //Declaración de funciones

        var vnombre = validar_nombre($nombre);
        var vcorreo = validar_correo($correo);
        var vdireccion = validar_direccion($direccion);
        var vtelefono = validar_telefono($telefono);
        var vtipo_doc = validar_tipo_documento($tipo_doc);
        var vtipo_usu = validar_tipo_usuario($tipo_usu);
        var vtip_doc_num = validar_numero_documento($tipo_doc_num);
        var vsex = validar_sexo($sex);
        var vcolegio = validar_colegio($colegio);
        var vespecialidad = validar_especialidad($especialidad);
        var vusuario = validar_usuario($usuario);
        var vcontraseña = validar_contraseña($contraseña);
        var vcontraseña1 = validar_contraseña1($contraseña1, $contraseña);
        var vrol = validar_rol($rol);

        if (vnombre === false || vcorreo === false || vdireccion === false || vtelefono === false || vtipo_doc === false || vtip_doc_num === false || vtipo_usu === false || vsex === false || vcolegio === false || vespecialidad === false || vusuario === false || vcontraseña === false || vrol === false || vcontraseña1 === false) {
            return false;
        } else {
            $.ajax({
                url: "/Cuentas/Registrar_Cuenta",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se registró el usuario correctamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Gerente/Gestionar_Cuenta";
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
    var maximoNumeroCaracteres13 = function maxCharacters(X) {
        if (X.length > 13) {
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

    //VALIDACIÓN DE NOMBRE
    function validar_nombre(nombre) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (nombre === "") {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'Debe ingresar un nombre correcto');
            $("#det_usu_nombre").focus();
            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }
        if (nombre.match(RegularExpression)) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'Debe ingresar un nombre valido');
            $("#det_usu_nombre").focus();

            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }
        if (nombre === " ") {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'No puede estar el vacío el campo');
            $("#det_usu_nombre").focus();

            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }
        if (esNum(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre no puede tener números');
            $("#det_usu_nombre").focus();

            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }
        if (tieneCaracEsp(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#det_usu_nombre").focus();

            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }
        if (maximoNumeroCaracteres100(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre debe ser de menos de 100 caracteres');
            $("#det_usu_nombre").focus();

            $("#det_usu_nombre").keyup(keyNombre);
            return false;
        }

        addgood("det_usu_nombre");
        attributes("error_det_usu_nombre");

        return true;
    }

    var keyNombre = function () {
        var nombre = $("#det_usu_nombre").val();
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (nombre === "") {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'Debe ingresar un nombre correcto');
        } else if (nombre.match(RegularExpression)) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'Debe ingresar un nombre valido');
        } else if (nombre === " ") {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'No puede estar el vacío el campo');
        } else if (esNum(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre no puede tener números');
        } else if (tieneCaracEsp(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
        } else if (maximoNumeroCaracteres100(nombre) === true) {
            adderror("det_usu_nombre");
            negativeattributes("error_det_usu_nombre", 'El nombre debe ser de menos de 100 caracteres');
        } else {
            addgood("det_usu_nombre");
            attributes("error_det_usu_nombre");
        }
        
    };
    //Validar correo
    function validar_correo(email) {
        var RegularExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        if (email === "") {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Debe ingresar un email');
            $("#det_usu_correo").focus();
            $("#det_usu_correo").keyup(keyEmail);
            return false;
        } 
        if (RegularExpression.test(email) == false) {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Ingrese un email valido');
            $("#det_usu_correo").focus();
            $("#det_usu_correo").keyup(keyEmail);
            return false;
        }
        if (email === " ") {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'El email no debe empezar con un espacio en blanco');
            $("#det_usu_correo").focus();
            $("#det_usu_correo").keyup(keyEmail);
            return false;
        }

        if (maximoNumeroCaracteres100(email) === true) {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Email debe de ser menos de 100 caracteres');
            $("#det_usu_correo").focus();
            $("#det_usu_correo").keyup(keyEmail);
            return false;
        }

        addgood("det_usu_correo");
        attributes("error_det_usu_correo");
        return true;
    }

    var keyEmail = function () {
        var RegularExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        var email = $("#det_usu_correo").val();
        if (email === "") {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Debe ingresar un email');
        } else if (RegularExpression.test(email)==false) {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Ingrese un email valido');
        } else if (email === " ") {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'El email no debe empezar con un espacio en blanco');
        } else if (maximoNumeroCaracteres100(email) === true) {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Email debe de ser menos de 100 caracteres');
        } else {
            addgood("det_usu_correo");
            attributes("error_det_usu_correo");
        }
        
    };

    //Validar Telefono
    function validar_telefono(telefono) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;

        if (telefono === "") {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'Debe ingresar un número');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono.match(RegularExpression)) {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'Ingrese un telefono válido');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono === " ") {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'El número de contacto no debe empezar con un espacio en blanco');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (esNum(telefono) === false) {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'El número de contacto debe ser un número');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'El numero de telefono debe de tener menos de 10 caracteres');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'El numero de telefono debe de ser mayor a 6 caracteres');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono < 0) {
            adderror("det_usu_telefono");
            negativeattributes("error_det_usu_telefono", 'El número debe de ser positivo');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }

        addgood("det_usu_telefono");
        attributes("error_det_usu_telefono");
        return true;
    }

    var keyTelefono = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var telefono = $("#det_usu_telefono").val();
        if (telefono === "") {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'Debe ingresar un número');
        } else if (telefono.match(RegularExpression)) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'Ingrese un telefono válido');
        } else if (telefono === " ") {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número de contacto no debe empezar con un espacio en blanco');
        } else if (esNum(telefono) === false) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número de contacto debe ser un número');
        } else if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El numero de telefono debe de tener menos de 10 caracteres');
        } else if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El numero de telefono debe de ser mayor a 6 caracteres');
        } else if (telefono < 0) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número debe de ser positivo');
        } else {
            addgood("det_usu_telefono");
            attributes("error_det_usu_telefono");
        }
        
    };

    //Validar dirección

    function validar_direccion(direccion) {

        if (direccion === "") {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'Debe ingresar una dirección');
            $("#det_usu_direccion").focus();
            $("#det_usu_direccion").keyup(keyDireccion);
            return false;
        }
        if (direccion.charAt(0) === ' ') {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'La dirección no debe empezar con un espacio en blanco');
            $("#det_usu_direccion").focus();
            $("#det_usu_direccion").keyup(keyDireccion);
            return false;
        }

        if (maximoNumeroCaracteres200(direccion) === true) {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'La dirección debe de ser menos de 200 caracteres');
            $("#det_usu_direccion").focus();
            $("#det_usu_direccion").keyup(keyDireccion);
            return false;
        }
        addgood("det_usu_direccion");
        attributes("error_det_usu_direccion");
        return true;
    }

    var keyDireccion = function () {
        var direccion = $("#det_usu_direccion").val();
        if (direccion === "") {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'Debe ingresar una dirección');
        } else if (direccion.charAt(0) === ' ') {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'La dirección no debe empezar con un espacio en blanco');
        } else if (maximoNumeroCaracteres200(direccion) === true) {
            adderror("det_usu_direccion");
            negativeattributes("error_det_usu_direccion", 'La dirección debe de ser menos de 200 caracteres');
        } else {
            addgood("det_usu_direccion");
            attributes("error_det_usu_direccion");
        }
    };

    //Validar tipo de documento
    function validar_tipo_documento(tipo_doc) {
        if (tipo_doc === "Tipo de Documento") {
            adderror("det_usu_tip_doc");
            negativeattributes("error_det_usu_tip_doc", 'El tipo de documento no debe empezar con un espacio en blanco');
            $("#det_usu_tip_doc").focus();
            $("#det_usu_tip_doc").keyup(keyTipoDocumento);
            return false;
        }
        addgood("det_usu_tip_doc");
        attributes("error_det_usu_tip_doc");
        return true;
    }

    var keyTipoDocumento = function () {
        var tipo_doc = $("#det_usu_tip_doc").val();
        if (tipo_doc === "Tipo de Documento") {
            adderror("det_usu_tip_doc");
            negativeattributes("error_det_usu_tip_doc", 'El tipo de documento no debe empezar con un espacio en blanco');
        } else {
            addgood("det_usu_tip_doc");
            attributes("error_det_usu_tip_doc");
        }
        
    };

    //Validar tipo de usuario
    function validar_tipo_usuario(tipo_doc) {
        if (tipo_doc === "Elige una opcion") {
            adderror("tipo_det_usu_tipo");
            negativeattributes("error_tipo_det_usu_tipo", 'Debe escoger un tipo de usuario');
            $("#tipo_det_usu_tipo").focus();
            $("#tipo_det_usu_tipo").keyup(keyTipoUsuario);
            return false;
        }
        addgood("tipo_det_usu_tipo");
        attributes("error_tipo_det_usu_tipo");
        return true;
    }

    var keyTipoUsuario = function () {
        var tipo_doc = $("#tipo_det_usu_tipo").val();
        if (tipo_doc === "Elige una opcion") {
            adderror("tipo_det_usu_tipo");
            negativeattributes("error_tipo_det_usu_tipo", 'Debe escoger un tipo de usuario');
        } else {
            addgood("tipo_det_usu_tipo");
            attributes("error_tipo_det_usu_tipo");
        }
    }


    //Validar  Numero Documento

    function validar_numero_documento(num_documento) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (num_documento === "") {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'Debe ingresar un número');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (num_documento.match(RegularExpression)) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'Ingrese número válido');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (num_documento === " ") {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número no debe empezar con un espacio en blanco');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (esNum(num_documento) === false) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número  debe ser un número');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (maximoNumeroCaracteres13(num_documento) === true) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número debe ser de menor de 13 caracteres');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (minimoNumeroCaracteres7(num_documento) === true) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número debe ser de mayor de 6 caracteres');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        if (num_documento < 0) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número de Contacto debe ser positivo');
            $("#det_usu_tip_doc_numero").focus();
            $("#det_usu_tip_doc_numero").keyup(keyNumeroDocumento);
            return false;
        }
        addgood("det_usu_tip_doc_numero");
        attributes("error_det_usu_tip_doc_numero");
        return true;
    }

    var keyNumeroDocumento = function() {
        var num_documento = $("#det_usu_tip_doc_numero").val();
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (num_documento === "") {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'Debe ingresar un número');
        } else if (num_documento.match(RegularExpression)) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'Ingrese número válido');
        } else if (num_documento === " ") {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número no debe empezar con un espacio en blanco');
        } else if (esNum(num_documento) === false) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número  debe ser un número');
        } else if (maximoNumeroCaracteres13(num_documento) === true) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número debe ser de menor de 13 caracteres');

        } else if (minimoNumeroCaracteres7(num_documento) === true) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número debe ser de mayor de 6 caracteres');
        } else if (num_documento < 0) {
            adderror("det_usu_tip_doc_numero");
            negativeattributes("error_det_usu_tip_doc_numero", 'El Número de Contacto debe ser positivo');
        } else {
            addgood("det_usu_tip_doc_numero");
            attributes("error_det_usu_tip_doc_numero");
        }
    }

    

    //Validar sexo

    function validar_sexo(sexo) {
        if (sexo === "Elige una opcion") {
            adderror("det_usu_sexo");
            negativeattributes("error_det_usu_sexo", 'Debe escoger un tipo de sexo');
            $("#det_usu_sexo").focus();
            $("#det_usu_sexo").keyup(keySexo);
            return false;
        }
        addgood("det_usu_sexo");
        attributes("error_det_usu_sexo");
        return true;
    }

    var keySexo = function () {
        var sexo = $("#det_usu_sexo").val();
        if (sexo === "Elige una opcion") {
            adderror("det_usu_sexo");
            negativeattributes("error_det_usu_sexo", 'Debe escoger un tipo de sexo');
        } else {
            addgood("det_usu_sexo");
            attributes("error_det_usu_sexo");
        }
    };

    //Validar Colegio

    function validar_colegio(colegio) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (colegio === "") {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'Debe ingresar un lugar de estudio correcto');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }
        if (colegio.match(RegularExpression)) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'Debe ingresar un lugar de estudio valido');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }
        if (colegio === " ") {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'No puede estar el vacío el campo');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }
        if (esNum(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo no puede tener números');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }
        if (tieneCaracEsp(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }
        if (maximoNumeroCaracteres100(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo debe ser de menos de 100 caracteres');
            $("#det_usu_codigoColegio").focus();
            $("#det_usu_codigoColegio").keyup(keyColegio);
            return false;
        }

        addgood("det_usu_codigoColegio");
        attributes("error_det_usu_codigoColegio");

        return true;
    }

    var keyColegio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var colegio = $("#det_usu_codigoColegio").val();
        if (colegio === "") {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'Debe ingresar un lugar de estudio correcto');
        } else if (colegio.match(RegularExpression)) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'Debe ingresar un lugar de estudio valido');
        } else if (colegio === " ") {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'No puede estar el vacío el campo');
        } else if (esNum(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo no puede tener números');
        } else if (tieneCaracEsp(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo debe empezar con una letra, no debe contener caracteres especiales o numeros');
        } else if (maximoNumeroCaracteres100(colegio) === true) {
            adderror("det_usu_codigoColegio");
            negativeattributes("error_det_usu_codigoColegio", 'El campo debe ser de menos de 100 caracteres');
        } else {
            addgood("det_usu_codigoColegio");
            attributes("error_det_usu_codigoColegio");
        }
    };

    //Validar especialidad

    function validar_especialidad(especialidad) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (especialidad === "") {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar una especialidad');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (especialidad.match(RegularExpression)) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar una especialidad valida');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (especialidad === " ") {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'No puede estar el vacío el campo');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (esNum(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'El campo no puede tener números');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (tieneCaracEsp(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("det_usu_especialidad", 'El campo debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (maximoNumeroCaracteres100(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'El campo debe ser de menos de 100 caracteres');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        addgood("det_usu_especialidad");
        attributes("error_det_usu_especialidad");
        return true;
    }

    var keyEspecialidad = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var especialidad = $("#det_usu_especialidad").val();
        if (especialidad === "") {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar un lugar de estudio correcto');
        } else if (especialidad.match(RegularExpression)) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar un lugar de estudio valido');
        } else if (especialidad === " ") {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'No puede estar el vacío el campo');
        } else if (esNum(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'El campo no puede tener números');
        } else if (tieneCaracEsp(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("det_usu_especialidad", 'El campo debe empezar con una letra, no debe contener caracteres especiales o numeros');
        } else if (maximoNumeroCaracteres100(especialidad) === true) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'El campo debe ser de menos de 100 caracteres');
        } else {
            addgood("det_usu_especialidad");
            attributes("error_det_usu_especialidad");
        }
    };

    //Validar Usuario

    function validar_usuario(usuario) {
        var vru = 0;
        if (usuario === "") {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'Debe ingresar un usuario');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        }
        if (usuario === " ") {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'No puede estar el vacío el campo');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        }
        if (esNum(usuario) === true) {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'El campo no puede tener números');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        }
        if (maximoNumeroCaracteres9(usuario) === true) {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'El campo debe ser de menos de 100 caracteres');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        } else {
            $.ajax({
                url: "/Cuentas/Evaluar_Usuario",
                method: "GET",
                async: false,
                data: { usu_usuario: usuario },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vru = 1;
                }
            });
        }
        if (vru === 1) {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'El usuario ya está disponible, debe ingresar otro');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        }
        addgood("usu_usuario");
        attributes("error_usu_usuario");
        return true;
    }

    var keyUsuario = function () {
        var usuario = $("#usu_usuario").val();
        if (usuario === "") {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'Debe ingresar un usuario');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        } else if (usuario === " ") {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'No puede estar el vacío el campo');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        } else if (esNum(usuario) === true) {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'El campo no puede tener números');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        } else if (maximoNumeroCaracteres9(usuario) === true) {
            adderror("usu_usuario");
            negativeattributes("error_usu_usuario", 'El campo debe ser de menos de 100 caracteres');
            $("#usu_usuario").focus();
            $("#usu_usuario").keyup(keyUsuario);
            return false;
        } else {
            $.ajax({
                url: "/Cuentas/Evaluar_Usuario",
                method: "GET",
                async: false,
                data: { usu_usuario: usuario },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("usu_usuario");
                    negativeattributes("error_usu_usuario", 'El usuario ya existe, debe ingresar otro');
                } else{
                    addgood("usu_usuario");
                    attributes("error_usu_usuario");
                }
            });
        }
    };


    //Validar Contraseña

    function validar_contraseña(contra1) {
        if (contra1 === "") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña", 'Debe ingresar una contraseña');
            $("#usu_contraseña").focus();
            $("#usu_contraseña").keyup(keyContraseña1);
            return false;
        }
        if (contra1 === " ") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña", 'No puede estar el vacío el campo');
            $("#usu_contraseña").focus();
            $("#usu_contraseña").keyup(keyContraseña1);
            return false;
        }
        addgood("usu_contraseña");
        attributes("error_usu_contraseña");
        return true;
    }

    var keyContraseña1 = function () {
        var contra1 = $("#usu_contraseña").val();
        if (contra1 === "") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña", 'Debe ingresar un usuario');
        } else if (contra1 === " ") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña", 'No puede estar el vacío el campo');
        } else {
            addgood("usu_contraseña");
            attributes("error_usu_contraseña");
        }
    };


    //Validar Repetición de Contraseña

    function validar_contraseña1(contra1, contra) {

        if (contra1 === "") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña1", 'Debe ingresar una contraseña');
            $("#usu_contraseña1").focus();
            $("#usu_contraseña1").keyup(keyContraseña1);
            return false;
        }
        if (contra1 === " ") {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña1", 'No puede estar el vacío el campo');
            $("#usu_contraseña1").focus();
            $("#usu_contraseña1").keyup(keyContraseña1);
            return false;
        }
        if (contra1 !== contra) {
            adderror("usu_contraseña");
            negativeattributes("error_usu_contraseña", 'Las contraseñas no son iguales');
            adderror("usu_contraseña1");
            negativeattributes("error_usu_contraseña1", 'Las contraseñas no son iguales');
            $("#usu_contraseña").focus();
            $("#usu_contraseña").keyup(keyContraseña2);
            return false;
        }
        addgood("usu_contraseña");
        attributes("error_usu_contraseña");
        addgood("usu_contraseña1");
        attributes("error_usu_contraseña1");
        return true;
    }

    var keyContraseña2 = function () {
        var contraseña = $("#usu_contraseña").val();
        var contraseña1 = $("#usu_contraseña_nuv").val();
        if (contraseña1 !== contraseña) {
            adderror("usu_contraseña1");
            negativeattributes("error_usu_contraseña1", 'Las contrseñas no son iguales');
        } else if (contraseña1 === " ") {
            adderror("usu_contraseña1");
            negativeattributes("error_usu_contraseña1", 'No puede estar el vacío el campo');
        } else {
            addgood("usu_contraseña");
            attributes("error_usu_contraseña");
            addgood("usu_contraseña1");
            attributes("error_usu_contraseña1");
        }
    };

    //Validar Roles

    function validar_rol(rol) {
        if (rol=== "Elige una opcion") {
            adderror("rol_codigo");
            negativeattributes("error_rol_codigo", 'Debe escoger un tipo de usuario');
            $("#rol_codigo").focus();
            $("#rol_codigo").keyup(keyRol);
            return false;
        }
        addgood("rol_codigo");
        attributes("error_rol_codigo");
        return true;
    }

    var keyRol = function () {
        var rol = $("#rol_codigo").val();
        if (rol === "Elige una opcion") {
            adderror("rol_codigo");
            negativeattributes("error_rol_codigo", 'Debe escoger un tipo de usuario');
        } else {
            addgood("rol_codigo");
            attributes("error_rol_codigo");
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
    function negativeattributes(id, tipo) {
        $("#" + id).removeClass("textsuccess");
        $("#" + id).addClass("text-danger");
        $("#" + id).html("");
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo + "</label > ");
    }
    $("#boton-Registrar").click(validacion);
});

