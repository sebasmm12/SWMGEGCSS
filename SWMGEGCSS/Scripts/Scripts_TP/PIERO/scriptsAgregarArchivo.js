$(function () {
    var valido = function () {
        var $actdesacomentario = $("#actividad_comentario");
        var $archivo = $("#newfile");

        var vComentario = validar_Comentario($actdesacomentario.val());
        var vArchivo = validar_Archivo($archivo.val());

            if (vComentario === false || vArchivo === false) {
                return false;
            }
            else {
                        //FormData es necesario para el envio de archivo,
                        //y de la siguiente manera capturamos todos los elementos del formulario
                        var Actividad_aux3 = new FormData($("#form")[0]);
                        //realizamos la petición ajax con la función de jquery
                        $.ajax({
                            type: "POST",
                            url: "/Trabajador/AgregarArchivo",
                            data: Actividad_aux3,
                            contentType: false, //importante enviar este parametro en false
                            processData: false, //importante enviar este parametro en false
                            success: function (r) {
                                //alert("Archivo enviado");
                            },
                            error: function (r) {
                                //alert("Error del servidor");
                            }
                        }).done(function (data) {
                            if (data !== 0) {
                                Swal.fire({
                                    type: 'success',
                                    title: 'Se subio el archivo exitosamente',
                                    confirmButtonText: 'OK'
                                }).then((result) => {
                                    if (result.value) {
                                        window.location.href = "/Trabajador/V_Tareas";
                                    }
                                });
                            }
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
        var iChars = "!@#_$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < X.length; i++) {
            if (iChars.indexOf(X.charAt(i)) !== -1) {//numero es un CE o un numero
                return true;
            }
        }
        return false;
    };
    //Validacion Comentario
    function validar_Comentario(id) {
        if (id === "") {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'Debe ingresar un nombre');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        if (id === " ") {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'El nombre no debe empezar con un espacio en blanco');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        if (esNum(id) === true) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'El nombre no puede ser un número');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        if (maximoNumeroCaracteres50(id) === true) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'El nombre debe ser de menos de 50 caracteres');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        return true;
    }
    var key = function () {
        var $valor = $("#actividad_comentario");
        if ($valor.val() === "") {
            negativeattributes("error_actividad_comentario", 'Debe ingresar un nombre');
            adderror("actividad_comentario");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error_actividad_comentario", 'El nombre no debe empezar con un espacio en blanco');
            adderror("actividad_comentario");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_actividad_comentario", 'El nombre no puede ser un número');
            adderror("actividad_comentario");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_actividad_comentario", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("actividad_comentario");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error_actividad_comentario", 'El nombre debe ser de menos de 50 caracteres');
            adderror("actividad_comentario");
        }
        else {
             attributes("error_actividad_comentario");
             addgood("actividad_comentario");
        }
    };
    //Validacion File
    function validar_Archivo(id) {
            var archivo = id;
            if (archivo === '') {
                adderror("newfile");
                negativeattributes("error_actividad_archivo", 'Debe escribir algo!');
                $("#newfile").focus();
                $("#newfile").change(keyF);
                return false;
            }
        return true;
    }
    var keyF = function () {
        var $archivo = $("#newfile");

        if ($archivo.val() === "") {
            negativeattributes("error_actividad_archivo", 'Debe escribir algo!');
            adderror("newfile");
        }
        else {
            attributes("error_actividad_archivo");
            addgood("newfile");
        }
    };
    //
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
    $("#btnSubirArchivo").click(valido);
});