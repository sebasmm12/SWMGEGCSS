(function () {
    "use strict";
    document.addEventListener('DOMContentLoaded', function () {
        //Campos Agregar Plan 
        var planNombre = document.getElementById('plan-nombre');
        var errorDivPN = document.getElementById('errordiv-plan-nombre');
        /*var planFecha = document.getElementById('apellido');
        var usuCodigo = document.getElementById('email');
        var empID = document.getElementById('email');*/

        planNombre.addEventListener('blur', validarCampos);
        /*planFecha.addEventListener('blur', validarCampos);*/

        function validarCampos() {
            if (this.value === '') {
                errorDivPN.style.display = 'block';
                errorDivPN.innerHTML = "Este campo es obligatorio";
                this.style.border = '1px solid red';
                errorDivPN.style.border = '2px solid red';
            } else {
                errorDivPN.style.display = 'none';
                this.style.border = '1px solid #cccccc';
            }
        }
    });
});