(function () {
    "use strict";
    document.addEventListener("DOMContentLoaded", function () {
        //Campos Agregar Plan 
        var planNombre = document.getElementById("plan-nombre");
        var errorDivPN = document.getElementById("errordiv-plan-nombre");
        var botonRegistrar = document.getElementById("boton-Registrar");

        planNombre.addEventListener('blur', validarCampos);
        botonRegistrar.addEventListener('click', verificarValidaciones);

        function validarCampos() {
            if (this.value === '') {
                errorDivPN.style.display = 'inline-block';
                errorDivPN.innerHTML = "Este campo es obligatorio";
                this.style.border = '2px solid red';
            } else {
                errorDivPN.style.display = 'none';
                this.style.border = '1px solid #cccccc';
            }
        }
        function verificarValidaciones(event) {
            event.preventDefault();

            if (planNombre.value === '') {
                planNombre.focus(); //para resaltar el icono
            } 


        }
    });//DOMContentLoaded
})();