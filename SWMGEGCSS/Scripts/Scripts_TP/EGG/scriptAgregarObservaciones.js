$(function () {

    var RegularExpression = /(^\s.)|(.\s{2,}.)|.\s$|(.[+-\.\@0-9-_\|/?ŋ?ī`š!Š\\Ļ{\][}į\^<>Ž%&()·].*)/;

    var validacion = function () {
        var observaciones_actividades = {
            obs_act_nombre: $("#obs_act_nombre").val(),
            obs_act_revisor: $("#obs_act_revisor").val(),
        };
        var actividades_desarrollarGerente = {
            act_desa_id: $("#act_desa_id").val(),
        };


        $(this).click(function () {
            var abc = $("#act_desa_id").val();
            if (vacio()) {
                if (vacio2()) {
                    $.ajax({
                        url: "/EvaluarFormulario/AgregarObservacion",
                        method: "POST",
                        data: {
                            observaciones_actividades: observaciones_actividades,
                            actividades_desarrollarGerente: actividades_desarrollarGerente
                        }
                    }).done(function (data) {
                        Swal.fire({
                            type: 'success',
                            title: 'Se agrego la observacion  exitosamente',
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
    var vacio = function vacios() {


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
    var vacio2 = function vacios() {

        if ($("#obs_act_nombre").val().trim() !== "") {
            var valor = $("#obs_act_nombre");
            if (valor.val().match(RegularExpression)) {
                $.ajax({

                }).done(function (data) {
                    Swal.fire({
                        type: 'error',
                        title: 'El nombre de la observación no debe tener caracteres especiales, ni espacios al inicio o final, ni dobles',
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
                    title: 'El nombre de la observación  no debe estar vacio',
                    confirmButtonText: 'OK'
                });
            });

            return false;
        }

    };




    $("#guardar").click(validacion);


});