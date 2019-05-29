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
        


        if (vfax === false || vrepresentante === false || vdireccion === false || vtelefono === false || vemail === false) {
          
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
    var maximoNumeroCaracteres9 = function maxCharacters(X) {
        if (X.length > 9) {
            return true;
        } else {
            return false;
        }
    };
    var minimoNumeroCaracteres7 = function maxCharacters(X) {
        if (X.length <6) {
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
    //Validacion Telefono
    function validar_telefono(telefono) {
        var regular = '([0-9]{1,3}\\d{7}$)|(9[0-9]{8}$)';

        if (telefono === "") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'Debe ingresar un Número de Contacto');
            $("#emp-telefono").focus();
            //  alert("telefono vacio");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        else if (telefono === " ") {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto no debe empezar con un espacio en blanco');
            $("#emp-telefono").focus();
            //   alert("telefono solo espacios");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        else if (esNum(telefono) === false) {
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
        else if (maximoNumeroCaracteres9(telefono) === true) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de menor de 10 caracteres');
            $("#emp-telefono").focus();
            //  alert("telefono largo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        else if (minimoNumeroCaracteres7(telefono) === true) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser de mayor de 6 caracteres');
            $("#emp-telefono").focus();
            //  alert("telefono largo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        else if (telefono < 0) {
            adderror("emp-telefono");
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser positivo');
            $("#emp-telefono").focus();
            alert("telefono negativo");
            $("#emp-telefono").keyup(keyTelefono);
            return false;
        }
        else {
            addgood("emp-telefono");
            attributes("error-emp-telefono");
            return true;
        }
    }
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
        if (direccion === " ") {
            adderror("emp-direccion");
            negativeattributes("error-emp-direccion", 'La dirección no debe empezar con un espacio en blanco');
            $("#emp-direccion").focus();
       //     alert("direccion con espacio");
            $("#emp-direccion").keyup(keyDireccion);
            return false;
        }
        var vnombre;
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
    }

    //--------------------------------------------------------------------------------------------
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


    function keyDireccion() {
      
        var $valor = $("#emp-direccion");
        if ($valor.val() === "") {

            negativeattributes("error-emp-direccion", 'Debe ingresar una dirección');
            adderror("emp-direccion");
        }
        else if ($valor.val() === " ") {

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
     /*   else if ($valor.length < 7 || $valor.length > 9) {

            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser entre 7 a 9');
            adderror("emp-telefono");
        }*/
        else if (maximoNumeroCaracteres9($valor.val()) === true) {
            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser debe ser menor de 10 caracteres');
            adderror("emp-telefono");
        }
        else if (minimoNumeroCaracteres7($valor.val())===true) {

            negativeattributes("error-emp-telefono", 'El Número de Contacto debe ser mayor de 6 caracteres');
            adderror("emp-telefono");
        }
        else if ($valor.val() < 0) {


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
    $("#boton-Actualizar").click(validacion);
});