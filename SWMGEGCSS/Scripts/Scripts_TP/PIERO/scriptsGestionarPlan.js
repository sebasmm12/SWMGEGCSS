/*(function () {
    "use strict";
    document.addEventListener("DOMContentLoaded", function () {
        //Campos Agregar Plan 
        var planNombre = document.getElementById("plan-nombre");
        var planFecha = document.getElementById("plan-fecha");
        var planCosto = document.getElementById("plan-costo");
        var planTiempo = document.getElementById("plan-tiempo");
        var erplanNombre = document.getElementById("error-plan-nombre");
        var erplanFecha = document.getElementById("error-plan-fecha");
        var erplanCosto = document.getElementById("error-plan-costo");
        var erplanTiempo = document.getElementById("error-plan-tiempo");

        planNombre.addEventListener('blur', normalidad(planNombre, erplanNombre));
        planFecha.addEventListener('blur', normalidad(planFecha, erplanFecha));
        planCosto.addEventListener('blur', normalidad(planCosto, erplanCosto));
        planTiempo.addEventListener('blur', normalidad(planTiempo, erplanTiempo));

        function normalidad(txt, erdiv) {
            if (txt !== '') {
                console.log("blur ????");
                erdiv.style.display = 'none';
                txt.style.border = '1px solid #cccccc';
            }
        }
    });//DOMContentLoaded
})();*/

$(function () {
    function validarCampos() {
        var planNombre = document.getElementById("plan-nombre");
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
        }
        Swal.fire({
            position: 'center',
            type: 'success',
            title: 'Plan registrado correctamente',
            showConfirmButton: false,
            timer: 3000
        });
        setTimeout( function() { return true; }, 8000);
    }
    $("#boton-Registrar").click(validarCampos);
});