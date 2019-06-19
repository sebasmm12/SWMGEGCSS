$(function () {

    var validacionRepo = function () {
        var $fechaIni = $("fechaInicio");
        var $fechaFin = $("fechaFin");

        var vFechaIni = validar_fecha_inicio($fechaIni.val());
        var vFechaFin = validar_fecha_fin($fechaFin.val());
        if (vFechaIni === false || vFechaFin === false) {
            return false;
        } else {
            var fechainicio = document.getElementById("fechaInicio");
            var fechafin = document.getElementById("fechaFin");
            $.ajax({
                url: "/Gerente/ExportarReporteFinanciero",
                data: { fechaIni: fechainicio.value, fechaFin: fechafin.value },
                type: "GET"
            }).done(function (data) {
                alert("XD");
                window.location.href = "/Gerente/ExportarReporteFinanciero?fechaIni=" + fechainicio.value + "&fechaFin=" + fechafin.value;
            });
            return true;
        }
    };

    function validar_fecha_inicio(id) {
        var fechaIngresada = new Date($("#fechaInicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);

        if (fechaIngresada < dateActual) {
            attributes("error-fechaini-repo");
            addgood("error-fechaini-repo");
            $("#fechaInicio").change(keyfechax);
            return true;
        } else {
            negativeattributes("error-fechaini-repo", 'la fecha debe ser menor a la actual');
            adderror("error-fechaini-repo");
            $("#fechaInicio").change(keyfechax);
            return false;
        }
    }
    var keyfechax = function () {
        var fechaIngresada = new Date($("#fechaInicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);

        if (fechaIngresada < dateActual) {
            attributes("error-fechaini-repo");
            addgood("error-fechaini-repo");
        } else {
            negativeattributes("error-fechaini-repo", 'la fecha debe ser menor a la actual');
            adderror("error-fechaini-repo");
        }
    };


    function validar_fecha_fin(id) {
        var fechaIngresada = new Date($("#fechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);
    
        if (fechaIngresada < dateActual) {
            attributes("error-fechafin-repo");
            addgood("error-fechafin-repo");
            $("#fechaFin").change(keyfechaJ);
            return true;
        } else {
            negativeattributes("error-fechafin-repo", 'la fecha debe ser menor a la actual');
            adderror("error-fechafin-repo");
            $("#fechaFin").change(keyfechaJ);
            return false;
        }
    }
    var keyfechaJ = function () {
        var fechaIngresada = new Date($("#fechaFin").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);

        if (fechaIngresada < dateActual) {
            attributes("error-fechafin-repo");
            addgood("error-fechafin-repo");
        } else {
            negativeattributes("error-fechafin-repo", 'la fecha debe ser menor a la actual');
            adderror("error-fechafin-repo");
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
        $("#" + id).html("<i class='fa fa-times'></i><label class='pl-2'>" + tipo + "</label> ");
    }

    $("#generar").click(validacionRepo);
});