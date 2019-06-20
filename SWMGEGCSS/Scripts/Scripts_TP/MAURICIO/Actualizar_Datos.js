$(function () {
    function filePreview(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imagePreview').html("<img src='" + e.target.result + "'>  ");
            }
            reader.readAsDataURL(input.files[0]);
        }
    };

    validacion = function () {
        //Declaraciòn de varibales
        var $usuario = $("#usu_usuario").val();
        var $contraseña = $("#usu_contraseña").val();
        var $rol = $("#rol_codigo").val();
        var $usuario_temp = $("#usu_usuario_temp").val();


        //Declaración de funciones
        var vusuario = validar_usuario($usuario, $usuario_temp);
        var vcontraseña = validar_contraseña($contraseña);
        var vrol = validar_rol($rol);


        if (vusuario === false || vcontraseña === false || vrol === false) {
            return false;
        } else {
            $.ajax({
                url: "/Cuentas/Actualizar_Datos",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se actualizó el usuario correctamente',
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

    //Validar Usuario

    function validar_usuario(usuario,usuario_temp) {
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
            if (usuario !== usuario_temp) {
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
        var usuario_temp = $("#usu_usuario_temp").val();
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
            if (usuario !== usuario_temp) {
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
                    } else {
                        addgood("usu_usuario");
                        attributes("error_usu_usuario");
                    }
                });
            } else {
                addgood("usu_usuario");
                attributes("error_usu_usuario");
            }
            
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

    //Validar Roles

    function validar_rol(rol) {
        if (rol === "Elige una opcion") {
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



    $("#boton-Actualizar").click(validacion);
    $('#imagen').change(function () {
        filePreview(this);
        
    });
});