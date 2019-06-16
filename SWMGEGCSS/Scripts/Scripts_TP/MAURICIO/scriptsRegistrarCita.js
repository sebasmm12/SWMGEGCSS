$(function () {


    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);
    };




    validacion = function () {
        //declaracion de variables jquery
        var $citaRepresentante = $("#cita_representante").val();
        var $citaTelefono = $("#cita_telefono").val();
        var $citaCorreo = $("#cita_correo").val();
        var $citaComentario = $("#cita_comentario").val();
        var $citaFecha = $("#cita_fecha").val();
        var $citaEmpresa = $("#cita_empresa").val();
        var $citaEncargado = $("#usu_citado").val();
        var $citaHora = $("#cita_hora").val();
        //Metodos

        var vCitaRepresentante = validar_representante($citaRepresentante);
        var vCitaTelefono = validar_telefono($citaTelefono);
        var vCitaCorreo = validar_correo($citaCorreo);
        var vCitaComentario = validar_comentario($citaComentario);
        var vCitaFecha = validar_fecha($citaFecha);
        var vCitaEmpresa = validar_empresa($citaEmpresa);
        var vCitaEncargado = validar_encargado($citaEncargado);
        var vcitaHora = validar_hora($citaHora);
        
        


        if (vCitaRepresentante === false || vCitaTelefono === false || vCitaCorreo === false || vCitaComentario === false || vCitaFecha === false || vCitaEmpresa === false || vCitaEncargado === false || vcitaHora === false) {
            return false;
        } else {

            $.ajax({
                url: "/Secretario/Registrar_Cita",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se registró la cita exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Secretario/Gestionar_Citas"
                    }
                });
                
            });
        }
        return false;
         
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


    function keyRepresentante() {
        var $valor = $("#cita_representante");
        if ($valor.val() === "") {
            negativeattributes("error-cita-representante", 'Debe ingresar un representante');
            adderror("cita_representante");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-cita-representante", 'El representante no debe empezar con un espacio en blanco');
            adderror("cita_representante");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-cita-representante", 'El representante no puede ser un número');
            adderror("cita_representante");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-cita-representante", 'El representante debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("cita_representante");
        }
        else if (maximoNumeroCaracteres100($valor.val()) === true) {
            negativeattributes("error-cita-representante", 'El representante debe ser de menos de 100 caracteres');
            adderror("cita_representante");
        }
        else {
            attributes("error-cita-representante");
            addgood("cita_representante");

        }
    }


    //Validar Telefono
    function validar_telefono(telefono) {
        var regular = '([0-9]{1,3}\\d{7}$)|(9[0-9]{8}$)';

        if (telefono === "") {
            adderror("cita_telefono");
            negativeattributes("error-cita-telefono", 'Debe ingresar un Número de Contacto');
            $("#cita_telefono").focus();
            //  alert("telefono vacio");
            $("#cita_telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono === " ") {
            adderror("cita_telefono");
            negativeattributes("error-cita-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            $("#cita_telefono").focus();
            //   alert("telefono solo espacios");
            $("#cita_telefono").keyup(keyTelefono);
            return false;
        }
        if (esNum(telefono) === false) {
            adderror("cita_telefono");
            negativeattributes("error-cita-telefono", 'El Número de Contacto debe ser un número');
            $("#cita_telefono").focus();
            //    alert("telefono no es numero");
            $("#cita_telefono").keyup(keyTelefono);
            return false;
        }
        if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("cita_telefono");
            negativeattributes("error-cita-telefono", 'El Número de Contacto debe ser de menor de 10 caracteres');
            $("#cita_telefono").focus();
            //  alert("telefono largo");
            $("#cita_telefono").keyup(keyTelefono);
            return false;
        }
        if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("cita_telefono");
            negativeattributes("error-cita-telefono", 'El Número de Contacto debe ser de mayor de 6 caracteres');
            $("#cita_telefono").focus();
            //  alert("telefono largo");
            $("#cita_telefono").keyup(keyTelefono);
            return false;
        }
        addgood("cita_telefono");
        attributes("error-cita-telefono");
        return true;
    }
    var keyTelefono = function () {
        var $valor = $("#cita_telefono");
        var regular = '([0-9]{1,3}\\d{7}$)|(9[0-9]{8}$)';
        if ($valor.val() === "") {
            negativeattributes("error-cita-telefono", 'Debe ingresar un Número de Contacto');
            adderror("cita_telefono");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-cita-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            adderror("cita_telefono");
        }
        else if (esNum($valor.val()) === false) {
            negativeattributes("error-cita-telefono", 'El Número de Contacto debe ser un número');
            adderror("cita_telefono");
        }
        else if (maximoNumeroCaracteres9($valor.val()) === true) {
            negativeattributes("error-cita-telefonoo", 'El Número de Contacto debe ser debe ser menor de 10 caracteres');
            adderror("cita_telefono");
        }
        else if (minimoNumeroCaracteres7($valor.val()) === true) {

            negativeattributes("error-cita-telefono", 'El Número de Contacto debe ser mayor de 6 caracteres');
            adderror("cita_telefono");
        }
        else {
            attributes("error-cita-telefono");
            addgood("cita_telefono");
        }


    };

    //Validar Correo
    function validar_correo(email) {
        var regular = /^(?:[^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*|"[^\n"]+")@(?:[^<>()[\].,;:\s@"]+\.)+[^<>()[\]\.,;:\s@"]{2,63}$/i

        if (email === "") {
            adderror("cita_correo");
            negativeattributes("error-cita-correo", 'Debe ingresar un email');
            $("#cita_correo").focus();
            $("#cita_correo").keyup(keyEmail);
            //    alert("email vacio");
            return false;
        }
        if (email === " ") {
            adderror("cita_correo");
            negativeattributes("error-cita-correo", 'El email no debe empezar con un espacio en blanco');
            $("#cita_correo").focus();
            $("#cita_correo").keyup(keyEmail);
            //    alert("email con espacio ");
            return false;
        }

        if (maximoNumeroCaracteres20(email) === true) {
            adderror("cita_correo");
            negativeattributes("error-cita-correo", 'El email debe ser de menos de 20 caracteres');
            $("#cita_correo").focus();
            $("#cita_correo").keyup(keyEmail);
            //   alert("email largo");
            return false;
        }
        else{
            addgood("cita_correo");
            attributes("error-cita-correo");
            return true;
        }
    }

    var keyEmail = function () {
        var $valor = $("#cita_correo");

        if ($valor.val() === "") {

            negativeattributes("error-cita-email", 'Debe ingresar un email');
            adderror("cita_correo");
        }
        else if ($valor.val() === " ") {

            negativeattributes("error-cita-email", 'El email no debe empezar con un espacio en blanco');
            adderror("cita_correo");
        }

        else if (maximoNumeroCaracteres20($valor.val()) === true) {

            negativeattributes("error-cita-email", 'El email debe ser de menos de 20 caracteres');
            adderror("cita_correo");
        }
        
            attributes("error-cita-email");
            addgood("cita_correo");
        
    };

    //Validar Comentario
    function validar_comentario(comentario) {
        if (comentario === " ") {
            adderror("cita_comentario");
            negativeattributes("error-cita-comentario", 'El Comentario no debe empezar con un espacio en blanco');
            $("#cita_comentario").focus();
            //    alert("representante con espacio");
            $("#cita_comentario").keyup(keyComentario);
            return false;
        }
        return true;
    }

    var keyComentario = function () {
        $valor = $("#cita_comentario")
        if ($valor.val() === " ") {
            negativeattributes("error-cita-comentario", 'El comentario no debe empezar con un espacio en blanco');
            adderror("error-cita-comentario");
        }
    };

    //Validar Fecha
    function validar_fecha(fecha) {
        var vcita = 0;
        var fechaIngresada = new Date(fecha);
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        //
        //fechaNoposible.setDate(fechaNoposible.getDate() + 1);
        var dateActual = new Date();
        var fechaActual = dateActual;
        var fechaNoposible = new Date(dateActual);
        fechaNoposible.setDate(fechaActual.getDate() + 60);
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        if (fecha === "") {
            adderror("cita_fecha");
            negativeattributes("error-cita-fecha", 'Debe ingresar una fecha');
            $("#cita_fecha").focus();
            $("#cita_fecha").change(keyfechaI);
            return false;
        } else if (fechaIngresada < fechaActual) {

            negativeattributes("error-cita-fecha", 'Debe ingresar una fecha válida');
            adderror("cita_fecha");
            return false;
        } else if (fechaIngresada >= fechaNoposible) {
            negativeattributes("error-cita-fecha", 'Debe ingresar más cercana');
            adderror("cita_fecha");
            return false;
        } else {
            $.ajax({
                url: "/Secretario/Evaluar_Fecha",
                method: "GET",
                async: false,
                data: {
                    cita_hora: $("#cita_hora").val(),
                    cita_fecha: $("#cita_fecha").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vcita = 1;
                }
            });
        }
           
        if (vcita === 1) {
            
            negativeattributes("error-cita-fecha", 'Ya hay una cita con la fecha indicada');
            adderror("cita_fecha");
            $("#cita_fecha").keyup(keyfechaI);
            return false;
        }
        attributes("error-cita-fecha");
        addgood("cita_fecha");
        return true;
    }
    var keyfechaI = function () {
        var fechaIngresada = new Date(fecha);
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        var fechaActual = dateActual;
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
        var $fecha_inicio = $("#cita_fecha");
        if ($fecha_inicio.val() === "") {
            negativeattributes("error-cita-fecha", 'Debe ingresar una fecha');
            adderror("cita_fecha");
        } else if (fechaIngresada < fechaActual) {
            negativeattributes("error-cita-fecha", 'Debe ingresar una fecha válida');
            adderror("cita_fecha");
        } else if (fechaIngresada >= fechaNoposible) {
            negativeattributes("error-cita-fecha", 'Debe ingresar más cercana');
            adderror("cita_fecha");
        } else {
            $.ajax({
                url: "/Secretario/Evaluar_Fecha",
                method: "GET",
                async: false,
                data: {
                    cita_hora: $("#cita_hora").val(),
                    cita_fecha: $("#cita_fecha").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("error-cita-fecha");
                    negativeattributes("error-cita-fecha", 'Ya hay una cita con la fecha indicada');
                }
            });
        }
             
        
        attributes("error-cita-fecha");
        addgood("cita_fecha");
    };
    //Validar Empresa
    function validar_empresa(empresa) {
        if (empresa === " ") {
            adderror("cita_empresa");
            negativeattributes("error-cita-empresa", 'La empresa no debe empezar con un espacio en blanco');
            $("#cita_empresa").focus();
            $("#cita_empresa").keyup(keyEmpresa);
            return false;
        } else if (empresa === "") {
            adderror("cita_empresa");
            negativeattributes("error-cita-empresa", 'Debe ingresar una empresa');
            $("#cita_empresa").focus();
            $("#cita_empresa").keyup(keyEmpresa);
            return false;
        } else if (esNum(empresa) === true) {
            adderror("cita_empresa");
            negativeattributes("error-cita-empresa", 'La empresa no puede ser un número');
            $("#cita_empresa").focus();
            $("#cita_empresa").keyup(keyEmpresa);
            return false;
        } else if (tieneCaracEsp(empresa) === true) {
            adderror("cita_empresa");
            negativeattributes("error-cita-empresa", 'La empresa debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#cita_empresa").focus();
            $("#cita_empresa").keyup(keyEmpresa);
            return false;
        } else if (maximoNumeroCaracteres100(empresa) === true) {
            adderror("cita_empresa");
            negativeattributes("error-cita-empresa", 'La empresa debe ser de menos de 100 caracteres');
            $("#cita_empresa").focus();
            $("#cita_empresa").keyup(keyEmpresa);
            return false;
        } 
        addgood("cita_empresa");
        attributes("error-cita-empresa");
        return true;
    }
    var keyEmpresa = function () {
        $valor = $("#cita_empresa")
        if ($valor.val() === " ") {
            negativeattributes("error-cita-empresa", 'La empresa no debe empezar con un espacio en blanco');
            adderror("cita_empresa");
        }
        if ($valor.val === "Elige una opcion") {
            negativeattributes("error-cita-empresa", 'Debe seleccionar una empresa');
            adderror("cita_empresa");
        }
    };
    //Validar Encargado
    function validar_encargado(encargado) {
        if (encargado === " ") {
            adderror("usu_citado");
            negativeattributes("error-cita-encargado", 'El encargado no debe empezar con un espacio en blanco');
            $("#usu_citado").focus();
            //    alert("representante con espacio");
            $("#usu_citado").keyup(keyEncargado);
            return false;
        }
        if (encargado === "Elige una opcion") {
            adderror("usu_citado");
            negativeattributes("error-cita-encargado", 'Debe seleccionar un encargado');
            $("#usu_citado").focus();
            $("#usu_citado").keyup(keyEncargado);
            return false;
        }
        return true;
    }
    var keyEncargado = function () {
        $valor = $("#usu_citado")
        if ($valor.val() === " ") {
            negativeattributes("error-cita-encargado", 'El encargado no debe empezar con un espacio en blanco');
            adderror("cita_empresa");
        }
        if ($valor.val === "Elige una opcion") {
            negativeattributes("error-cita-empresa", 'Debe seleccionar un encargado5');
            adderror("cita_empresa");
        }
    };

    //Validar Hora
    function validar_hora(hora) {
        var vcita = 0;
        $valor = $("#cita_hora").val();
        var hour = $valor.split(':');
        var hh = parseInt(hour[0], 10);
        var mm = parseInt(hour[1], 10);
        if (hora === "") {
            adderror("cita_hora");
            negativeattributes("error-cita-hora", 'La hora de la cita no puede estar vacía');
            $("#cita_hora").focus();
            $("#cita_hora").keyup(keyHora);
            return false;
        } else if (hh < 8 || hh > 20) {
            adderror("cita_hora");
            negativeattributes("error-cita-hora", "Tiene que escoger una hora en horario de trabajo");
            $("#cita_hora").focus();
            $("#cita_hora").keyup(keyHora);
            return false;
        } else if (hh >= 20 && mm >= 1) {
            adderror("cita_hora");
            negativeattributes("error-cita-hora", "Tiene que escoger una hora en horario de trabajo");
            $("#cita_hora").focus();
            $("#cita_hora").keyup(keyHora);
            return false;
        } else {
            $.ajax({
                url: "/Secretario/Evaluar_Fecha",
                method: "GET",
                async: false,
                data: {
                    cita_hora: $("#cita_hora").val(),
                    cita_fecha: $("#cita_fecha").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vcita = 1;
                }
            });
        }
        if (vcita === 1) {
            
            negativeattributes("error-cita-hora", 'Ya hay una cita en la hora designada');
            adderror("cita_hora");
            $("#cita_hora").keyup(keyHora);
            return false;
        }
        
        attributes("error-cita-hora");
        addgood("cita_hora");
        return true;
    }

    var keyHora = function () {
        $valor = $("#cita_hora").val();
        var hour = $valor.split(':');
        var hh = parseInt(hour[0], 10);
        var mm = parseInt(hour[1], 10);
        if ($valor.val() === "") {
            negativeattributes("error-cita-hora", 'La hora de la cita no puede estar vacía');
            adderror("cita_empresa");
        }
        if (hh < 8 || hh > 20) {
            negativeattributes("error-cita-hora", "Tiene que escoger una hora en horario de trabajo");
            adderror("cita_empresa");
        } else if (hh >= 20 && mm >= 1) {
            negativeattributes("error-cita-hora", "Tiene que escoger una hora en horario de trabajo");
            adderror("cita_empresa");
        } else if (hh <= 7 && mm <= 59) {
            negativeattributes("error-cita-hora", "Tiene que escoger una hora en horario de trabajo");
            adderror("cita_empresa");
        } else {
            $.ajax({
                url: "/Secretario/Evaluar_Fecha",
                method: "GET",
                async: false,
                data: {
                    cita_hora: $("#cita_hora").val(),
                    cita_fecha: $("#cita_fecha").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("cita_hora");
                    negativeattributes("error-cita-hora", 'Ya hay una cita en la hora designada');
                }
            });
        }
        attributes("error-cita-hora");
        addgood("cita_hora");
    };

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
    $("input[data-exp-autocomplete]").each(autocompletado);
});