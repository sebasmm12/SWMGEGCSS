$(function () {

    var validacionRepo = function () {
        var $fechaIni = $("fechaInicio");
        var $fechaFin = $("fechaFin");

        var vFechaIni = validar_fecha_inicio($fechaIni.val());
        var vFechaFin = validar_fecha_fin($fechaFin.val());
        //var vCoherenciaFecha = validar_coherencia_fechas();
        if (vFechaIni === false || vFechaFin === false || validar_coherencia_fechas() === false || Validar_IE_Registros() === false) {
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



    function Validar_IE_Registros() {
        var fechainicio = document.getElementById("fechaInicio");
        var fechafin = document.getElementById("fechaFin");
        $.ajax({
            url: "/Gerente/Evaluar_IE_Cantidad",
            method: "GET",
            async: false,
            data: { fechaIni: fechainicio.value, fechaFin: fechafin.value },
            dataType: "json"
        }).done(function (data) {
            if (data === 0) {
                adderror("cant-reg");
                negativeattributes("cant-reg", 'No existe flujo Financiero para esa Fecha');
                return false;
            }
            });
        return true;
    }
    function validar_fecha_inicio(id) {
        var fechaIngresada = new Date($("#fechaInicio").val());
        fechaIngresada.setDate(fechaIngresada.getDate() + 1);
        var dateActual = new Date();
        dateActual.setHours(0, 0, 0, 0);
        fechaIngresada.setHours(0, 0, 0, 0);

        if (fechaIngresada < dateActual) {
            attributes("error-fechaini-repo");
            addgood("fechaInicio");
            $("#fechaInicio").change(keyfechax);
            return true;
        } else {
            negativeattributes("error-fechaini-repo", 'la fecha debe ser menor a la actual');
            adderror("fechaInicio");
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
            addgood("fechaInicio");
        } else {
            negativeattributes("error-fechaini-repo", 'la fecha debe ser menor a la actual');
            adderror("fechaInicio");
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
            addgood("fechaFin");
            $("#fechaFin").change(keyfechaJ);
            return true;
        } else {
            negativeattributes("error-fechafin-repo", 'la fecha debe ser menor a la actual');
            adderror("fechaFin");
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
            addgood("fechaFin");
        } else {
            negativeattributes("error-fechafin-repo", 'la fecha debe ser menor a la actual');
            adderror("fechaFin");
        }
    };

    //Validar de coherencia
    function validar_coherencia_fechas() {
        var fechaIngresadaFin = new Date($("#fechaFin").val());
        fechaIngresadaFin.setDate(fechaIngresadaFin.getDate() + 1);
        fechaIngresadaFin.setHours(0, 0, 0, 0);

        var fechaIngresadaInicio = new Date($("#fechaInicio").val());
        fechaIngresadaInicio.setDate(fechaIngresadaInicio.getDate() + 1);
        fechaIngresadaInicio.setHours(0, 0, 0, 0);
        if (validar_fecha_fin(fechaIngresadaFin) === false || validar_fecha_inicio(fechaIngresadaInicio) === false) {
            $("#fechaFin").change(keyfechaCo);
            $("#fechaInicio").change(keyfechaCo);
            return false;
        }

        else if (fechaIngresadaFin < fechaIngresadaInicio) {
            adderror("fechaFin");
            negativeattributes("error-fechafin-repo", 'La fecha de finalizacion debe ser mayor a la de inicio');
            $("#fechaFin").focus();
            $("#fechaFin").change(keyfechaCo);
            return false;
        }
        else {
            attributes("error-fechaini-repo");
            addgood("fechaFin");
            attributes("error-fechafin-repo");
            addgood("fechaInicio");
            return true;
        }
    }

    var keyfechaCo = function () {
        var fechaIngresadaFin = new Date($("#fechaFin").val());
        fechaIngresadaFin.setDate(fechaIngresadaFin.getDate() + 1);
        fechaIngresadaFin.setHours(0, 0, 0, 0);

        var fechaIngresadaInicio = new Date($("#fechaInicio").val());
        fechaIngresadaInicio.setDate(fechaIngresadaInicio.getDate() + 1);
        fechaIngresadaInicio.setHours(0, 0, 0, 0);

        if (validar_fecha_fin(fechaIngresadaFin) === false || validar_fecha_inicio(fechaIngresadaInicio) === false) {
            $("#fechaFin").focus();
            $("#fechaInicio").focus();
        }
        else if (fechaIngresadaFin < fechaIngresadaInicio) {
            adderror("fechaFin");
            negativeattributes("error-fechafin-repo", 'La fecha de finalizacion debe ser mayor a la de inicio');
            $("#fechaFin").focus();
        }
        else {
            attributes("error-fechafin-repo");
            addgood("fechaFin");
            attributes("error-fechaini-repo");
            addgood("fechaInicio");
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