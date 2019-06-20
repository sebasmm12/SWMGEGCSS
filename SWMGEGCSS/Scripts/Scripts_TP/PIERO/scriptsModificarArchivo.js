$(function () {
    var obs_act_id = 0;
    var SelectorBoton;

    var valido1 = function () {
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
                url: "/Trabajador/ModificarArchivo",
                data: Actividad_aux3,
                contentType: false, //importante enviar este parametro en false
                processData: false, //importante enviar este parametro en false
                error: function (r) {
                    alert("Error del servidor");
                }
            }).done(function (data) {
                if (data === 100) {
                    Swal.fire({
                        type: 'error',
                        title: 'Debe contestar todas las observaciones'
                    });
                }
                else if (data === 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'Debe subir un archivo',
                        showCancelButton: true
                    });
                }
                else {
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

    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            obs_act_id = $(this).attr("data-id-observacion");
            SelectorBoton = $(this);
            $.ajax({
                url: "/Trabajador/_ResponderObservacion",
                method: "GET"
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnActualizarObservacion").click(envioAxajRegistrarObs);
                });
            return false;
        });
    };
    $(".btnModal").each(envioajaxModal);

    var envioAxajRegistrarObs = function () {
        var $observacion_usuario = $("#observacion_usuario");

        var vobs = validar_obs($observacion_usuario.val());

        if (vobs === false) {
            return false;
        }
        else {
            var t_obs = {
                obs_act_id: obs_act_id,
                obs_act_usuario: $("#observacion_usuario").val()
            };
            $.ajax({
                url: "/Trabajador/_ResponderObservacion",
                method: "POST",
                data: {
                    t_obs: t_obs
                }
            }).done(function (data) {
                $('#ModificarObservacion').modal('hide');
                //cambiar de color al boton
                $(SelectorBoton).css('background-color', 'green');
                $(SelectorBoton).css('border-color', 'green');
                $(SelectorBoton).text('Observacion respondida');
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
    var maximoNumeroCaracteres = function maxCharacters(X,limite) {
        if (X.length > limite) {
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
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if ($("#actividad_comentario").val().match(RegularExpression)) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'Debe ingresar un comentario valido(no esp. blanco)');
            $("#actividad_comentario").keyup(key);
            return false;
        }
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
        if (maximoNumeroCaracteres(id,50) === true) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'El nombre debe ser de menos de 50 caracteres');
            $("#actividad_comentario").focus();
            $("#actividad_comentario").keyup(key);
            return false;
        }
        return true;
    }
    var key = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#actividad_comentario");
        if ($valor.val().match(RegularExpression)) {
            adderror("actividad_comentario");
            negativeattributes("error_actividad_comentario", 'Debe ingresar un comentario valido(no esp. blanco)');
        }
        else if ($valor.val() === "") {
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
        else if (maximoNumeroCaracteres($valor.val(),50) === true) {
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
            negativeattributes("error_actividad_archivo", 'Debe subir un archivo');
            $("#newfile").focus();
            $("#newfile").change(keyF);
            return false;
        }
        return true;
    }
    var keyF = function () {
        var $archivo = $("#newfile");

        if ($archivo.val() === "") {
            negativeattributes("error_actividad_archivo", 'Debe subir un archivo');
            adderror("newfile");
        }
        else {
            attributes("error_actividad_archivo");
            addgood("newfile");
        }
    };
    //
    function validar_obs(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if ($("#observacion_usuario").val().match(RegularExpression)) {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'Debe ingresar una observacion valido(no esp. blanco)');
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        if (id === "") {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'Debe ingresar una observacion');
            $("#observacion_usuario").focus();
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        if (id === " ") {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'La observacion no debe empezar con un espacio en blanco');
            $("#observacion_usuario").focus();
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        if (esNum(id) === true) {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'La observacion no puede ser un número');
            $("#observacion_usuario").focus();
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        if (tieneCaracEsp(id) === true) {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'La observacion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#observacion_usuario").focus();
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        if (maximoNumeroCaracteres(id,500) === true) {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'La observacion debe ser de menos de 50 caracteres');
            $("#observacion_usuario").focus();
            $("#observacion_usuario").keyup(keyObs);
            return false;
        }
        return true;
    }
    var keyObs = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#observacion_usuario");
        if ($valor.val().match(RegularExpression)) {
            adderror("observacion_usuario");
            negativeattributes("error_observacion_usuario", 'Debe ingresar una observacion valida(no esp. blanco)');
        }
        else if ($valor.val() === "") {
            negativeattributes("error_observacion_usuario", 'Debe ingresar un observacion');
            adderror("observacion_usuario");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error_observacion_usuario", 'La observacion no debe empezar con un espacio en blanco');
            adderror("observacion_usuario");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error_observacion_usuario", 'La observacion no puede ser un número');
            adderror("observacion_usuario");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error_observacion_usuario", 'La observacion debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("observacion_usuario");
        }
        else if (maximoNumeroCaracteres($valor.val(),500) === true) {
            negativeattributes("error_observacion_usuario", 'La observacion debe ser de menos de 50 caracteres');
            adderror("observacion_usuario");
        }
        else {
            attributes("error_observacion_usuario");
            addgood("observacion_usuario");
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

    $(".btnActualizarObservacion").click(envioAxajRegistrarObs);
    $("#btnModificarArchivo").click(valido1);
});