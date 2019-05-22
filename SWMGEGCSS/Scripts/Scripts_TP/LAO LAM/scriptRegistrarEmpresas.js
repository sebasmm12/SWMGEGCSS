$(function() {
    /*var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("dataid")
        };
        $input.autocomplete(options);

    };
    $("input[dataid]").each(autocompletado);*/
    validacion = function () {
        //declaracion de variables jquery
        var $empRepresentante = $("#emp-representante");
        var $empDireccion = $("#emp-direccion");
        var $emptelefono = $("#emp-telefono");
        var $empFax = $("#emp-fax");
        var $empEmail = $("#emp-email");


        var $empRazonSocial = $("#emp-razon-social");
        var $empSigla = $("#emp-sigla");
        var $empRuc = $("#emp-ruc");



        var vrazon_social = validar_razon_social($empRazonSocial.val());
        var vsigla = validar_sigla($empSigla.val());
        var vruc = validar_ruc($empRuc.val());




        var vfax = validar_fax($empFax.val());
        var vrepresentante = validar_representante($empRepresentante.val());
        var vdireccion = validar_direccion($empDireccion.val());
        var vtelefono = validar_telefono($emptelefono.val());
        var vemail = validar_email($empEmail.val());


        if (vfax === false || vrepresentante === false || vdireccion === false || vtelefono === false || vemail === false || vrazon_social === false || vsigla === false || vruc === false) {

            return false;
        }
        else {

            $.ajax({
                url: "/Empresa/Registrar_Empresa",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
        
                Swal.fire({
                    type: 'success',
                    title: 'Se registró la empresa exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Gerente/Gestionar_Empresas";
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
    //-------------------------------------------------------------------------------------------------------
    //Validacion Razon Social
    function validar_razon_social(razon_social) {
        vrazon = 0;
        if (razon_social === "") {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'Debe ingresar una Razon Social');
            $("#emp-razon-social").focus();

            $("#emp-razon-social").keyup(keyRazon);
            return false;
        }
        if (razon_social === " ") {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'La Razon Social no debe empezar con un espacio en blanco');
            $("#emp-razon-social").focus();

            $("#emp-razon-social").keyup(keyRazon);
            return false;
        }
        if (esNum(razon_social) === true) {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'La Razon Social no puede ser un número');
            $("#emp-razon-social").focus();

            $("#emp-razon-social").keyup(keyRazon);
            return false;
        }
        if (tieneCaracEsp(razon_social) === true) {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'La Razon Social debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#emp-razon-social").focus();

            $("#emp-razon-social").keyup(keyRazon);
            return false;
        }
        if (maximoNumeroCaracteres100(razon_social) === true) {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'La Razon Social debe ser de menos de 100 caracteres');
            $("#emp-razon-social").focus();

            $("#emp-razon-social").keyup(keyRazon);
            return false;
        }
        else {
            $.ajax({
                url: "/Empresa/Evaluar_Nombre_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_razon_social: $("#emp-razon-social").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vrazon = 1;
                }
            });
        }
        if (vrazon === 1) {
            adderror("emp-razon-social");
            negativeattributes("error-emp-razon-social", 'Esta Razon Social ya existe, debe escribir otro');
            $("#emp-razon-sociale").keyup(keyRazon);
            return false;
        }

        addgood("emp-razon-social");
        attributes("error-emp-razon-social");

        return true;
    }
    //Validacion Sigla
    function validar_sigla(sigla) {
        var vsig = 0;
        if (sigla === "") {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'Debe ingresar una Sigla');
            $("#emp-sigla").focus();

            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        if (sigla === " ") {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'La Sigla no debe empezar con un espacio en blanco');
            $("#emp-sigla").focus();

            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        if (esNum(sigla) === true) {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'La Sigla no puede ser un número');
            $("#emp-sigla").focus();

            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        if (tieneCaracEsp(sigla) === true) {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'La Sigla debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#emp-sigla").focus();

            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        if (maximoNumeroCaracteres20(sigla) === true) {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'La Sigla debe ser de menos de 20 caracteres');
            $("#emp-sigla").focus();

            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        else {
            $.ajax({
                url: "/Empresa/Evaluar_Sigla_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_sigla: $("#emp-sigla").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vsig = 1;
                }
            });
        }
        if (vsig === 1) {
            adderror("emp-sigla");
            negativeattributes("error-emp-sigla", 'Esta Sigla ya existe, debe escribir otro');
            $("#emp-sigla").keyup(keysigla);
            return false;
        }
        addgood("emp-sigla");
        attributes("error-emp-sigla");

        return true;

    }
    //Validacion Ruc
    function validar_ruc(ruc) {
        var vru = 0;
        if (ruc === "") {
            adderror("emp-ruc");
            negativeattributes("error-emp-ruc", 'Debe ingresar un Ruc');
            $("#emp-ruc").focus();

            $("#emp-ruc").keyup(keyRuc);

            return false;
        }
        if (ruc === " ") {
            adderror("emp-ruc");
            negativeattributes("error-emp-ruc", 'El Ruc no debe empezar con un espacio en blanco');
            $("#emp-ruc").focus();

            $("#emp-ruc").keyup(keyRuc);

            return false;
        }
        if (esNum(ruc) === false) {
            adderror("emp-ruc");
            negativeattributes("error-emp-ruc", 'El Ruc debe ser un número');
            $("#emp-ruc").focus();

            $("#emp-ruc").keyup(keyRuc);

            return false;
        }

        if (igual(ruc) === false) {
            adderror("emp-ruc");
            negativeattributes("error-emp-ruc", 'El Ruc debe ser 11 dígitos');
            $("#emp-ruc").focus();
            $("#emp-ruc").keyup(keyRuc);

            return false;
        }
        else {
            $.ajax({
                url: "/Empresa/Evaluar_Ruc_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_ruc: $("#emp-ruc").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vru = 1;
                }
            });
        }
        if (vru === 1) {
            adderror("emp-ruc");
            negativeattributes("error-emp-ruc", 'Este Ruc ya existe, debe escribir otro');
            $("#emp-ruc").keyup(keyRuc);
            return false;
        }
        addgood("emp-ruc");
        attributes("error-emp-ruc");

        return true;
    }
    //Validacion Telefono
    function validar_telefono(telefono) {


        if (telefono === "") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'Debe ingresar un Número de Contacto');
            $("#emp-telefono").focus();
            //  alert("telefono vacio");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono === " ") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            $("#emp-telefono").focus();
            //   alert("telefono solo espacios");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        if (esNum(telefono) === false) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser un número');
            $("#emp-telefono").focus();
            //    alert("telefono no es numero");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        /*      if (maximoNumeroCaracteres20(telefono) === true) {
                  adderror("emp-telefono");
                  negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de menos de 20 caracteres');
                  $("#emp-telefono").focus();
                  alert("telefono largo");
           //       $("#emp-telefono").keyup(key);
                  return false;
              }
              if (telefono.match(regular)) {
                  adderror("emp-telefono");
                  negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser menor o igual a 9 digitos');
                  $("#emp-telefono").focus();
               //   alert("telefono largo");
                  $("#emp-telefono").keyup(keyTelefono);
                  return false;
              }
              */
        if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de menor de 10 caracteres');
            $("#emp-telefono").focus();
            //  alert("telefono largo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de mayor de 6 caracteres');
            $("#emp-telefono").focus();
            //  alert("telefono largo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        if (telefono < 0) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser positivo');
            $("#emp-telefono").focus();
            alert("telefono negativo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }

        addgood("emp-telefono");
        attributes("error-emp-telefono");
        return true;
    };
    //Validacion Fax


    function validar_fax(fax) {

        var regular = '51[0-9]{1,3}\\d{7}$';

        if (fax.match(regular)) {
            addgood("emp-fax");
            attributes("error-emp-fax");
            return true;
        }
        /*else if (fax === "") {
            adderror("emp-fax");
            negativeattributes("error-emp-fax", 'Debe ingresar un fax');
            $("#emp-fax").focus();
            //       $("#emp-email").keyup(key);
            alert("fax vacio");

            return false;
        }*/
        else if (fax === " ") {
            adderror("emp-fax");
            negativeattributes("error-emp-fax", 'El fax no debe empezar con un espacio en blanco');
            $("#emp-fax").focus();
            $("#emp-fax").keyup(keyFax);
            //    alert("fax con espacio ");
            return false;
        }

        else if (maximoNumeroCaracteres20(fax) === true) {
            adderror("emp-fax");
            negativeattributes("error-emp-fax", 'El fax debe ser de menos de 20 caracteres');
            $("#emp-fax").focus();
            $("#emp-fax").keyup(keyFax);
            //   alert("fax largo");
            return false;
        }

        else if (esNum(fax) === false) {
            adderror("emp-fax");
            negativeattributes("error-emp-fax", 'El fax debe ser un número');
            $("#emp-fax").focus();
            $("#emp-fax").keyup(keyFax);
            //     alert("fax no numerico");
            return false;
        }

    };
    //Validacion email
    function validar_email(email) {
        var regular = /^(?:[^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*|"[^\n"]+")@(?:[^<>()[\].,;:\s@"]+\.)+[^<>()[\]\.,;:\s@"]{2,63}$/i

        if (email === "") {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'Debe ingresar un email');
            $("#emp-email").focus();
            $("#emp-email").keyup(keyEmail);
            //    alert("email vacio");
            return false;
        }
        if (email === " ") {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'El email no debe empezar con un espacio en blanco');
            $("#emp-email").focus();
            $("#emp-email").keyup(keyEmail);
            //    alert("email con espacio ");
            return false;
        }

        if (maximoNumeroCaracteres20(email) === true) {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'El email debe ser de menos de 20 caracteres');
            $("#emp-email").focus();
            $("#emp-email").keyup(keyEmail);
            //   alert("email largo");
            return false;
        }

        addgood("emp-email");
        attributes("error-emp-email");
        return true;
    }


    //Validacion direccion

    function validar_direccion(direccion) {


        /*  var regular = "!@\$%\^\*\(\)\+=\[]\\\'\/{}\|<>\?";
          if (direccion.match(regular)) {
              return false;
          }*/
        if (direccion === "") {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'Debe ingresar una dirección');
            $("#emp-direccion").focus();
            //      alert("direccion vacio");
            $("#emp-direccion").keyup(keyDireccion);
            return false;
        }
        if (direccion.charAt(0) === ' ') {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'La dirección no debe empezar con un espacio en blanco');
            $("#emp-direccion").focus();
            //     alert("direccion con espacio");
            $("#emp-direccion").keyup(keyDireccion);
            return false;
        }

        if (maximoNumeroCaracteres200(direccion) === true) {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'La dirección debe ser de menos de 200 caracteres');
            $("#emp-direccion").focus();
            $("#emp-direccion").keyup(keyDireccion);
            //    alert("direccion largo");
            return false;
        }
        addgood("emp-direccion");
        attributes("error-emp-direccion");
        return true;
    }


    //Validacion Representante
    function validar_representante(representante) {

        var vnombre = 0;
        if (representante === "") {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'Debe ingresar un representante');
            $("#emp-representante").focus();
            //      alert("representante vacio");
            $("#emp-representante").keyup(keyRepresentante);
            return false;
        }
        if (representante === " ") {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El Representante no debe empezar con un espacio en blanco');
            $("#emp-representante").focus();
            //    alert("representante con espacio");
            $("#emp-representante").keyup(keyRepresentante);
            return false;
        }
        if (esNum(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante no puede ser un número');
            $("#emp-representante").focus();
            //    alert("representante es numero");
            $("#emp-representante").keyup(keyRepresentante);
            return false;
        }
        if (tieneCaracEsp(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#emp-representante").focus();
            //     alert("representante con caES");
            $("#emp-representante").keyup(keyRepresentante);
            return false;
        }
        if (maximoNumeroCaracteres100(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante debe ser de menos de 100 caracteres');
            $("#emp-representante").focus();
            //     alert("representante largo");
            $("#emp-representante").keyup(keyRepresentante);
            return false;
        }
        addgood("emp-representante");
        attributes("error-emp-representante");
        return true;
    };


    //--------------------------------------------------------------------------------------------
    
    var keyRazon = function () {
        var $valor = $("#emp-razon-social");
        if ($valor.val() === "") {
            negativeattributes("error-emp-razon-social", 'Debe ingresar una Razon Social');
            adderror("emp-razon-social");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-emp-razon-social", 'La Razon Social no debe empezar con un espacio en blanco');
            adderror("emp-razon-social");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-emp-razon-social", 'La Razon Social no puede ser un número');
            adderror("emp-razon-social");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-emp-razon-social", 'La Razon Social debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("emp-razon-social");
        }
        else if (maximoNumeroCaracteres100($valor.val()) === true) {
            negativeattributes("error-emp-razon-social", 'La Razon Social debe ser de menos de 100 caracteres');
            adderror("emp-razon-social");
        }
        else {
            $.ajax({
                url: "/Empresa/Evaluar_Nombre_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_razon_social: $("#emp-razon-social").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("emp-razon-social");
                    negativeattributes("error-emp-razon-social", 'Esta Razon Social ya existe, debe escribir otro');
                }
                else {

                    attributes("error-emp-razon-social");
                    addgood("emp-razon-social");
                }

            });
        }
    };

    var keysigla = function () {
        var $valor = $("#emp-sigla");
        if ($valor.val() === "") {
            negativeattributes("error-emp-sigla", 'Debe ingresar una Sigla');
            adderror("emp-sigla");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-emp-sigla", 'La Sigla no debe empezar con un espacio en blanco');
            adderror("emp-sigla");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-emp-sigla", 'La Sigla no puede ser un número');
            adderror("emp-sigla");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-emp-sigla", 'La Sigla debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("emp-sigla");
        }
        else if (maximoNumeroCaracteres20($valor.val()) === true) {
            negativeattributes("error-emp-sigla", 'La Sigla debe ser de menos de 20 caracteres');
            adderror("emp-sigla");
        }
        else {
            $.ajax({
                url: "/Empresa/Evaluar_Sigla_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_sigla: $("#emp-sigla").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("emp-sigla");
                    negativeattributes("error-emp-sigla", 'Esta Sigla ya existe, debe escribir otro');
                }
                else {

                    attributes("error-emp-sigla");
                    addgood("emp-sigla");
                }

            });
        }
    };
    var keyRuc = function () {
        var $valor = $("#emp-ruc");

        if ($valor.val() === "") {
            negativeattributes("error-emp-ruc", 'Debe ingresar un Ruc');
            adderror("emp-ruc");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-emp-ruc", 'El Ruc no debe empezar con un espacio en blanco');
            adderror("emp-ruc");
        }
        else if (esNum($valor.val()) === false) {
            negativeattributes("error-emp-ruc", 'El Ruc debe ser un número');
            adderror("emp-ruc");
        }
        else if (igual($valor.val())===false) {

            negativeattributes("error-emp-ruc", 'El Ruc debe ser 11 dígitos');
            adderror("emp-ruc");
        }
          else {
            $.ajax({
                url: "/Empresa/Evaluar_Ruc_Empresa",
                method: "GET",
                async: false,
                data: {
                    emp_ruc: $("#emp-ruc").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("emp-ruc");
                    negativeattributes("error-emp-ruc", 'Este Ruc ya existe, debe escribir otro');
                }
                else {

                    attributes("error-emp-ruc");
                    addgood("emp-ruc");
                }

            });
        }
        
    };
    var keyRepresentante = function () {
        var $valor = $("#emp-representante");
        if ($valor.val() === "") {
            negativeattributes("error-emp-representante", 'Debe ingresar un representante');
            adderror("emp-representante");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-emp-representante", 'El representante no debe empezar con un espacio en blanco');
            adderror("emp-representante");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-emp-representante", 'El representante no puede ser un número');
            adderror("emp-representante");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-emp-representante", 'El representante debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("emp-representante");
        }
        else if (maximoNumeroCaracteres100($valor.val()) === true) {
            negativeattributes("error-emp-representante", 'El representante debe ser de menos de 100 caracteres');
            adderror("emp-representante");
        }
        else {
            attributes("error-emp-representante");
            addgood("emp-representante");

        }



    };


   var keyDireccion=function() {

        var $valor = $("#emp-direccion");
        if ($valor.val() === "") {

            negativeattributes("error-emp-direccion", 'Debe ingresar una dirección');
            adderror("emp-direccion");
        }
        else if ($valor.val().charAt(0)===' ') {

            negativeattributes("error-emp-direccion", 'La dirección no debe empezar con un espacio en blanco');
            adderror("emp-direccion");
        }

        else if (maximoNumeroCaracteres200($valor.val()) === true) {

            negativeattributes("error-emp-direccion", 'La dirección debe ser de menos de 200 caracteres');
            adderror("emp-direccion");

        }
        else {
            attributes("error-emp-direccion");
            addgood("emp-direccion");
        }
    }


    var keyTelefono = function () {
        var $valor = $("#emp-telefono");
        var regular = '([0-9]{1,3}\\d{7}$)|(9[0-9]{8}$)';
        if ($valor.val() === "") {
            negativeattributes("error-emp-telefono", 'Debe ingresar un Número de Contacto');
            adderror("emp-telefono");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-emp-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            adderror("emp-telefono");
        }
        else if (esNum($valor.val()) === false) {
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser un número');
            adderror("emp-telefono");
        }

        /*    else if (maximoNumeroCaracteres50($valor.val()) === true) {
              negativeattributes("error-emp-telefono", 'El nombre debe ser de menos de 50 caracteres');
              adderror("emp-telefono");
          }
              else if ($valor.match(regular)) {
                    
                    negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser menor o igual a 9 digitos');
                    adderror("emp-telefono");
                }*/
        else if (maximoNumeroCaracteres9($valor.val()) === true) {
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser debe ser menor de 10 caracteres');
            adderror("emp-telefono");
        }
        else if (minimoNumeroCaracteres7($valor.val()) === true) {

            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser mayor de 6 caracteres');
            adderror("emp-telefono");
        }
        else if ($valor.val()<0)
        {
            

            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser positivo');
            adderror("emp-telefono");
        }
        else {
            attributes("error-emp-telefono");
            addgood("emp-telefono");
        }


    };

    var keyEmail = function () {
        var $valor = $("#emp-email");

        if ($valor.val() === "") {

            negativeattributes("error-emp-email", 'Debe ingresar un email');
            adderror("emp-email");
        }
        else if ($valor.val() === " ") {

            negativeattributes("error-emp-email", 'El email no debe empezar con un espacio en blanco');
            adderror("emp-email");
        }

        else if (maximoNumeroCaracteres20($valor.val()) === true) {

            negativeattributes("error-emp-email", 'El email debe ser de menos de 20 caracteres');
            adderror("emp-email");
        }
        else {
            attributes("error-emp-email");
            addgood("emp-email");
        }
    }

    var keyFax = function () {
        var $valor = $("#emp-fax");
      



        if ($valor.val() === " ") {

            negativeattributes("error-emp-fax", 'El fax no debe empezar con un espacio en blanco');
            adderror("emp-fax");
        }

        else if (maximoNumeroCaracteres20($valor.val()) === true) {

            negativeattributes("error-emp-fax", 'El fax debe ser de menos de 20 caracteres');
            adderror("emp-fax");
        }

        else if (esNum($valor.val()) === false) {
            adderror("emp-fax");
            negativeattributes("error-emp-fax", 'El fax debe ser un número');

        }
        else {
            attributes("error-emp-fax");
            addgood("emp-fax");
        }
    }
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
    $("#boton-Registrar").click(validacion);
});