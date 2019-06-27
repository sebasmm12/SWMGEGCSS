$(function () {

    var RegularExpression = /(^\s.)|(.\s{2,}.)|.\s$|(.[+-\.\@0-9-_\|/?ŋ?ī`š!Š\\Ļ{\][}į\^<>Ž%&()·].*)/;

    var validacion = function () {
        $(this).click(function () {
            var abc = $("#act_desa_id").val();
            var observaciones_actividades = {
                obs_act_nombre: $("#obs_act_nombre").val(),
                obs_act_id: $("#obs_act_id").val(),
                act_desa_id: $("#act_desa_id").val(),
                obs_act_revisor: $("#obs_act_revisor").val()


            };
            if (vacio()) {
                if (vacio2()) {
                    $.ajax({
                        url: "/EvaluarFormulario/ModificarObservacion",
                        method: "POST",
                        data: {
                            observaciones_actividades: observaciones_actividades
                        }
                    }).done(function (data) {
                        Swal.fire({
                            type: 'success',
                            title: 'Se actualizó la observación  exitosamente',
                            confirmButtonText: 'OK'
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/EvaluarFormulario/RevisarFormulario/" + abc;
                            }
                        });
                    });
                }
            }
            return false;
        });

    };
    var vacio = function esNumero() {
        if ($("#obs_act_nombre").val().trim() !== "") {

            var valor = $("#obs_act_nombre");
            if (valor.val().match(RegularExpression)) {
                $.ajax({

                }).done(function (data) {
                    Swal.fire({
                        type: 'error',
                        title: 'La observacion no debe tener caracteres especiales, ni espacios al inicio o final, ni dobles',
                        confirmButtonText: 'OK'
                    });
                });


                return false;
            }
            return true;
        } else {
            $.ajax({

            }).done(function (data) {
                Swal.fire({
                    type: 'error',
                    title: 'El campo de observacion no debe estar vacio',
                    confirmButtonText: 'OK'
                });
            });

            return false;
        }
    };
    var vacio2 = function vacios2() {


        if ($("#obs_act_revisor").val().trim() !== "") {
            var valor = $("#obs_act_revisor");
            if (valor.val().match(RegularExpression)) {
                $.ajax({

                }).done(function (data) {
                    Swal.fire({
                        type: 'error',
                        title: 'La observacion no debe tener caracteres especiales, ni espacios al inicio o final, ni dobles',
                        confirmButtonText: 'OK'
                    });
                });
                return false;
            }
            return true;
        } else {
            $.ajax({


            }).done(function (data) {
                Swal.fire({
                    type: 'error',
                    title: 'El campo de observacion no debe estar vacio',
                    confirmButtonText: 'OK'
                });
            });

            return false;
        }

    };

    var validacion2 = function () {
        $(this).click(function () {
            var abc = $("#act_desa_id").val();
            var observaciones_actividades = {
                obs_act_nombre: $("#obs_act_nombre").val(),
                obs_act_revisor: $("#obs_act_revisor").val(),
            };
            if (vacio()) {
                if (vacio2()) {
                    $.ajax({
                        url: "/EvaluarFormulario/ModificarObservacion2",
                        method: "POST",
                        data: {
                            observaciones_actividades: observaciones_actividades
                        }
                    }).done(function (data) {
                        Swal.fire({
                            type: 'success',
                            title: 'Se actualizó la observación exitosamente',
                            confirmButtonText: 'OK'
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/EvaluarFormulario/RevisarFormulario/" + abc;
                            }
                        });
                    });
                }
            }
            return false;
        });
    };

    $("#corregir").each(validacion);
    $("#levantar").each(validacion2);


});