$(function () {
    validacion = function () {
        //declaracion de variables jquery
        var $planNombre = $("#plan-nombre");
        var $planFecha = $("#plan-fecha");
        var $planCosto = $("#plan-costo");
        var $planTiempo = $("#plan-tiempo");
        var $planEmp = $("#plan-emp");
        var $planTipoServicio = $("#plan-tipo-servicio");

        var vnombre = validar_nombre_plan($planNombre.val());
        /*var vfecha = validar_fecha_plan($planFecha.val());
        var vcosto = validar_costo_plan($planCosto.val());
        var vtiempo = validar_tiempo_plan($planTiempo.val());
        var vemp = validar_emp_plan($planEmp.val());
        var vtiposervicio = validar_tipo_servicio_plan($planTipoServicio.val());*/

        if (vnombre === false/* || vfecha === false || vcosto === false || vtiempo === false || vemp === false || vtiposervicio === false*/) {
            return false;
        }
        else {
            $.ajax({
                url: "/Plan/Agregar_Plan_de_Proyectos",
                method: "POST",
                data: $("form").serialize(),
                dataType: "json"
            }).done(function (data) {
                Swal.fire({
                    type: 'success',
                    title: 'Se registro el plan exitosamente',
                    showConfirmButton: false,
                    timer: 3000
                }).then(function (dismiss) {
                    if (dismiss === 'timer') {
                        window.location.href = "/Gerente/Gestionar_Plan_Proyecto";
                    }
                });
            });
        }
    };
    function validar_nombre_plan(id) {
        if (id === "") {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'Debe ingresar un nombre');
            $("#plan-nombre").keyup(key);
            return false;
        }
        else if (id === " ") {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre no debe empezar con un espacio en blanco');
            $("#plan-nombre").keyup(key);
            return false;
        }
        else if (esNumero(id)) {
            adderror("plan-nombre");
            negativeattributes("error-plan-nombre", 'El nombre no puede ser un número');
            $("#plan-nombre").keyup(key);
            return false;
        }
        else { return true;}
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
        else if (esNumero($valor.val())) {
            negativeattributes("error-plan-nombre", 'El nombre no puede ser un número');
            adderror("plan-nombre");
        }
        else {
            attributes("error-plan-nombre");
            addgood("plan-nombre");
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
    function esNumero(txt) {
        if (isNaN(txt)) {
            return false;
        } else {
            return true;
        }
    }
        /*var planNombre = document.getElementById("plan-nombre");
        var planFecha = document.getElementById("plan-fecha");
        var planEmp = document.getElementById("plan-emp");
        var planCosto = document.getElementById("plan-costo");
        var planTipoServicio = document.getElementById("plan-tipo-servicio");
        var planTiempo = document.getElementById("plan-tiempo");

        var erplanNombre = document.getElementById("error-plan-nombre");
        var erplanFecha = document.getElementById("error-plan-fecha");
        var erplanCosto = document.getElementById("error-plan-costo");
        var erplanTiempo = document.getElementById("error-plan-tiempo");
        var erplanEmp = document.getElementById("error-plan-emp");
        var erplanTipoServicio = document.getElementById("error-plan-tipo-servicio");

        function esNumero(txt, erdiv) {
            if (isNaN(txt.value)) {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo debe ser numérico";
                txt.style.border = '2px solid red';
                return 0;
            } else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }

        function validarVacio(txt, erdiv) {
            if (txt.value === '') {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo es obligatorio";
                txt.style.border = '2px solid red';
                return 0;
            }
            else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }
        function tieneCaracteresEspeciales(X) {
            var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
            for (var i = 0; i < X.value.length; i++) {
                if (iChars.indexOf(X.value.charAt(i)) !== -1) { return true; }
            }
            return false;
        }

        function validarDatos(txt, erdiv) {
            if (txt.value === '') {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo es obligatorio";
                txt.style.border = '2px solid red';
                return 0;
            }
            if (tieneCaracteresEspeciales(txt)) {
                erdiv.style.display = 'inline-block';
                erdiv.innerHTML = "Este campo no puede contener caracteres especiales";
                txt.style.border = '2px solid red';
                return 0;
            }
            else {
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
                return 1;
            }
        }

        if (validarDatos(planNombre, erplanNombre) === 0) {
            $("#plan-nombre").focus();
            return false;
        }
        if (validarVacio(planFecha, erplanFecha) === 0) {
            $("#plan-fecha").focus();
            return false;
        }
        if (validarDatos(planEmp, erplanEmp) === 0) {
            $("#plan-emp").focus();
            return false;
        }
        if (validarDatos(planCosto, erplanCosto) === 0) {
            $("#plan-costo").focus();
            return false;
        }
        if (esNumero(planCosto, erplanCosto) === 0) {
            $("#plan-costo").focus();
            return false;
        }
        if (validarDatos(planTipoServicio, erplanTipoServicio) === 0) {
            $("#plan-tipo-servicio").focus();
            return false;
        }
        if (validarDatos(planTiempo, erplanTiempo) === 0) {
            $("#plan-tiempo").focus();
            return false;
        }*/
    $("#boton-Registrar").click(validacion);
});