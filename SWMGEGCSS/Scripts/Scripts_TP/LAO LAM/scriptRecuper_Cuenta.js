$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})
$(function () {

    validacion = function () {
        var $det_usu_correo = $("#det_usu_correo");
        var vdet_usu_correo = validar_det_usu_correo($det_usu_correo.val());

        if (vdet_usu_correo === false) {
            return false;
        }
        else {
            return true;
        }

        function validar_det_usu_correo(correo) {
            var regular = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            if (regular.test(correo) === false) {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email válido');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else if (correo === "") {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else if (maximoNumeroCaracteres50(correo) === true) {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese un Email menor a 50 caracteres');
                $("#det_usu_correo").focus();
                $("#det_usu_correo").keyup(KeyCorreo);
                return false;
            }
            else {
                addgood("det_usu_correo");
                attributes("error-det-usu-correo");
                return true;
            }

        }

        var KeyCorreo = function () {
            var regular = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            var $valor = $("#det_usu_correo");
            if (regular.test($valor.val()) === false) {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email válido');
            }
            else if ($valor.val() === "") {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese Email');
            }
            else if (maximoNumeroCaracteres50($valor.val()) === true) {
                adderror("det_usu_correo");
                negativeattributes("error-det-usu-correo", 'Ingrese un Email menor a 50 caracteres');
            }
            else {
                addgood("det_usu_correo");
                attributes("error-det-usu-correo");
            }

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
    $("#boton-enviar-correo").click(validacion);
});



