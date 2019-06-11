$(function () {
    validacion = function () {
        //Declaraciónj de variables
        var $nombre = $("#det_usu_nombre").val();
        var $correo = $("#det_usu_correo").val();
        var $direccion = $("#det_usu_direccin").val();
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
        var $rol = $("#usu_contraseña_nuv").val();

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
        var vcontraseña = validar_contraseña($contraseña, $contraseña1);
        var vrol = validar_rol($rol);

        if (vnombre == false || vcorreo == false || vdireccion == false || vtelefono == false || vtipo_doc == false || vtip_doc_num == false || vtipo_usu == false || vsex == false || vcolegio == false || vespecialidad == false || vusuario == false || vcontraseña == false || vrol == false) {
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
                        window.location.href = "/Gerente/Gestionar_Cuentas";
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
    //Validar correo
    function validar_correo(email) {
        var regular = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

        if (email === "") {
            adderror("det_usu_correo");
            negativeattributes("error_det_usu_correo", 'Debe ingresar un email');
            $("#det_usu_correo").focus();
            $("#det_usu_correo").keyup(keyEmail);
            return false;
        }
        if (regular.test(email) == false) {
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

    //Validar Telefono
    function validar_telefono(telefono) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;

        if (telefono === "") {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'Debe ingresar un número');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono.match(RegularExpression)) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'Ingrese un telefono válido');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono === " ") {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número de contacto no debe empezar con un espacio en blanco');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (esNum(telefono) === false) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número de contacto debe ser un número');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El numero de telefono debe de tener menos de 10 caracteres');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El numero de telefono debe de ser mayor a 6 caracteres');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono < 0) {
            adderror("det_usu_telefono");
            negativeattributes("det_usu_telefono", 'El número debe de ser positivo');
            $("#det_usu_telefono").focus();
            $("#det_usu_telefono").keyup(keyTelefono);
            return false;
        }

        addgood("det_usu_telefono");
        attributes("error_det_usu_telefono");
        return true;
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

    //Validar tipo de usuario
    function validar_tipo_usuario(tip_usu) {
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

    //Validar especialidad

    function validar_especialidad(especialidad) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (especialidad === "") {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar un lugar de estudio correcto');
            $("#det_usu_especialidad").focus();
            $("#det_usu_especialidad").keyup(keyEspecialidad);
            return false;
        }
        if (especialidad.match(RegularExpression)) {
            adderror("det_usu_especialidad");
            negativeattributes("error_det_usu_especialidad", 'Debe ingresar un lugar de estudio valido');
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


});