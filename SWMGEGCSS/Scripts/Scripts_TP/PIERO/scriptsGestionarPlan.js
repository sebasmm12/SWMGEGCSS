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
                $("#plan-nombre").focus(); //para resaltar el campo
            }
            else {
                $("#boton-Registrar").removeEventListener("click", verificarValidaciones);
                $("#boton-Registrar").click();
                /*$.ajax({
                    url: 'Agregar_Plan_de_Proyectos',
                    method: "POST",
                    success: function (r) {
                        $("#boton-Registrar").load("Agregar_Plan_de_Proyectos");
                    }
                });*/
            }
        }
    });//DOMContentLoaded
})();