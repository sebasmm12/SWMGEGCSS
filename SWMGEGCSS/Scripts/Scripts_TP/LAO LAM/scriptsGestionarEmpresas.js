$(function () {
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
       

        var vfax = validar_fax($empFax.val());
        var vrepresentante = validar_representante($empRepresentante.val());
        var vdireccion = validar_direccion($empDireccion.val());
        var vtelefono = validar_telefono($emptelefono.val());
        var vemail = validar_email($empEmail.val());
        


        if (vfax === false || vrepresentante === false || vdireccion === false || vtelefono === false ||vemail === false) {
            return false;
        }
        else {
            $.ajax({
                url: "/Empresa/Actualizar_Empresa",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se actualizó la empresa exitosamente',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.value) {
                        window.location.href = "/Gerente/Gestionar_Empresas";
                    }
                });
            });
        }
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
    //Validacion Telefono
    function validar_telefono(telefono) {


        if (telefono === "") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'Debe ingresar un Número de Contacto');
            $("#emp-telefono").focus();
            $("#emp-telefono").keyup(key);
            return false;
        }
        if (telefono === " ") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            $("#emp-telefono").focus();
            $("#emp-telefono").keyup(key);
            return false;
        }
        if (esNum(telefono) === false) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser un número');
            $("#emp-telefono").focus();
            $("#emp-telefono").keyup(key);
            return false;
        }
        if (maximoNumeroCaracteres20(telefono) === true) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de menos de 20 caracteres');
            $("#emp-telefono").focus();
            $("#emp-telefono").keyup(key);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                }
            });
        }
        if (vnombre === 1) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
            $("#plan-nombre").keyup(key);
            return false;
        }
        return true;
    }
    //Validacion Fax


    function validar_fax(fax) {
        var regular = "51[0-9]{1,3}\d{7}";
        if (fax.val().match(regular)) {
            return true;
        } else {

            return false;
        }
    };
    //Validacion email
    function validar_email(email) {


        if (email === "") {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'Debe ingresar un email');
            $("#emp-email").focus();
            $("#emp-email").keyup(key);
            return false;
        }
        if (email === " ") {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'El email no debe empezar con un espacio en blanco');
            $("#emp-email").focus();
            $("#emp-email").keyup(key);
            return false;
        }

        if (maximoNumeroCaracteres20(email) === true) {
            adderror("emp-email");
            negativeattributes("error-emp-email", 'El email debe ser de menos de 20 caracteres');
            $("#emp-email").focus();
            $("#emp-email").keyup(key);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                }
            });
        }
        if (vnombre === 1) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
            $("#plan-nombre").keyup(key);
            return false;
        }
        return true;
    }
     //Validacion direccion

    function validar_direccion(direccion) {
   
        var regular = "!@$%^*()+=[]\\\'/{}|<>?";
        if (direccion.val().match(regular)) {
            return false;
        }
        if (representante === "") {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'Debe ingresar una dirección');
            $("#emp-direccion").focus();
            $("#emp-direccion").keyup(key);
            return false;
        }
        if (representante === " ") {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'La dirección no debe empezar con un espacio en blanco');
            $("#emp-direccion").focus();
            $("#emp-direccion").keyup(key);
            return false;
        }

        if (maximoNumeroCaracteres200(direccion) === true) {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'La dirección debe ser de menos de 200 caracteres');
            $("#emp-direccion").focus();
            $("#emp-direccion").keyup(key);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                }
            });
        }
        if (vnombre === 1) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
            $("#plan-nombre").keyup(key);
            return false;
        }
        return true;
    }


        //Validacion Representante
    function validar_representante(representante) {
        var vnombre = 0;
        if (representante === "") {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'Debe ingresar un representante');
            $("#emp-representante").focus();
            $("#emp-representante").keyup(key);
            return false;
        }
        if (representante === " ") {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El Representante no debe empezar con un espacio en blanco');
            $("#emp-representante").focus();
            $("#emp-representante").keyup(key);
            return false;
        }
        if (esNum(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante no puede ser un número');
            $("#emp-representante").focus();
            $("#emp-representante").keyup(key);
            return false;
        }
        if (tieneCaracEsp(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante debe empezar con una letra, no debe contener caracteres especiales o numeros');
            $("#emp-representante").focus();
            $("#emp-representante").keyup(key);
            return false;
        }
        if (maximoNumeroCaracteres100(representante) === true) {
            adderror("emp-representante");
            negativeattributes("error-emp-representante", 'El representante debe ser de menos de 100 caracteres');
            $("#emp-representante").focus();
            $("#emp-representante").keyup(key);
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    vnombre = 1;
                }
            });
        }
        if (vnombre === 1) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
            $("#plan-nombre").keyup(key);
            return false;
        }
        return true;
    }
    var key = function () {
        var $valor = $("#plan-nombre");
        if ($valor.val() === "") {
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre');
            adderror("plan-nombre");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-nombre", 'El nombre no debe empezar con un espacio en blanco');
            adderror("plan-nombre");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre no puede ser un número');
            adderror("plan-nombre");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre debe empezar con una letra, no debe contener caracteres especiales o numeros');
            adderror("plan-nombre");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-nombre", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-nombre");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_nombre: $("#plan-nombre").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    adderror("plan-nombre");
                    negativeattributes("error-plan-nombre", 'Este nombre ya existe, debe escribir otro');
                }
                else {
                    attributes("error-plan-nombre");
                    addgood("plan-nombre");
                }
            });
        }

    };


    function keyT() {
        var RegularExpression = /^\d+[.]*\d*$/;
        var $valor = $("#plan-tiempo");
        if ($valor.val() === "") {
            negativeattributes("error-plan-tiempo", 'Debe ingresar un número');
            adderror("plan-tiempo");
        }
        else if ($valor.val() <= 0) {
            negativeattributes("error-plan-tiempo", 'El número debe ser positivo y mayor que 0');
            adderror("plan-tiempo");
        }
        else {
            //    if (!$valor.val().match(RegularExpression)) {
            attributes("error-plan-tiempo");
            addgood("plan-tiempo");
            /*    }
                else {
                    negativeattributes("error-plan-tiempo", 'No se aceptan caracteres especiales');
                    adderror("plan-tiempo");
                }*/
        }
    }

    var keyE = function () {
        var $valor = $("#plan-emp");
        if ($valor.val() === "") {
            negativeattributes("error-plan-emp", 'Debe ingresar un nombre');
            adderror("plan-emp");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-emp", 'El nombre de la empresa no debe empezar con un espacio en blanco');
            adderror("plan-emp");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre no puede ser un número');
            adderror("plan-emp");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
            adderror("plan-emp");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-emp", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-emp");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Nombre_Empresa_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_emp: $("#plan-emp").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    attributes("error-plan-emp");
                    addgood("plan-emp");
                }
                else {
                    adderror("plan-emp");
                    negativeattributes("error-plan-emp", 'El nombre de esta empresa no existe');
                }
            });
        }

    };

    var keyTS = function () {
        var $valor = $("#plan-tipo-servicio");
        if ($valor.val() === "") {
            negativeattributes("error-plan-tipo-servicio", 'Debe ingresar un nombre');
            adderror("plan-tipo-servicio");
        }
        else if ($valor.val() === " ") {
            negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa no debe empezar con un espacio en blanco');
            adderror("plan-tipo-servicio");
        }
        else if (esNum($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre no puede ser un número');
            adderror("plan-tipo-servicio");
        }
        else if (tieneCaracEsp($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre de la empresa debe empezar con una letra y no debe ser caracter especial');
            adderror("plan-tipo-servicio");
        }
        else if (maximoNumeroCaracteres50($valor.val()) === true) {
            negativeattributes("error-plan-tipo-servicio", 'El nombre debe ser de menos de 50 caracteres');
            adderror("plan-tipo-servicio");
        }
        else {
            $.ajax({
                url: "/Plan/Evaluar_Tipo_Servicio_Plan",
                method: "GET",
                async: false,
                data: {
                    plan_tipo_servicio: $("#plan-tipo-servicio").val()
                },
                dataType: "json"
            }).done(function (data) {
                if (data !== 0) {
                    attributes("error-plan-tipo-servicio");
                    addgood("plan-tipo-servicio");
                }
                else {
                    adderror("plan-tipo-servicio");
                    negativeattributes("error-plan-tipo-servicio", 'El nombre de este servicio no existe');
                }
            });
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
    $("#boton-Actualizar").click(validacion);
});