$(function () {

    var RegularExpression = /(^\s.)|(.\s{2,}.)|.\s$|(.[+-\.\@0-9-_\|/?ŋ?ī`š!Š\\Ļ{\][}į\^<>Ž%&()·].*)/;


    var obj = {
        act_desa_nombre: $("#act_desa_nombre"),
        act_desa_id: $("#act_desa_id"),
        act_desa_descripcion: $("#act_desa_descripcion"),
        act_desa_archivo_nombre: $("#act_desa_archivo_nombre"),
        act_desa_comentario: $("#act_desa_comentario"),
        act_desa_revisor_obs: $("#act_desa_revisor_obs"),
        est_act_id: $("est_act_id")
    };
    var validacion = function () {
        $(this).click(function () {
            if (vacio() && fecha()) {

                $.ajax({
                    url: "/EvaluarFormulario/RevisarFormulario",
                    method: "POST",
                    data: $("#form").serialize()
                }).done(function (data) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se actualizó  exitosamente',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/EvaluarFormulario/Index";
                        }
                    });
                });
            }
            return false;
        });

    };
    var vacio = function esNumero() {
        if ($("#act_desa_revisor_obs").val().trim() !== "") {

            var valor = $("#act_desa_revisor_obs");
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




    var fecha = function fechaval() {
        var today = new Date();
        var today2 = new Date();
        var today2letras = $("#act_desa_fecha_fin").val().toString();
        var arrayDeCadenas = today2letras.split("-");
        today.setHours(0, 0, 0, 0);
        today2.setHours(0, 0, 0, 0);
        today2.setFullYear(arrayDeCadenas[0], parseInt(arrayDeCadenas[1]) - 1, arrayDeCadenas[2]);
       
        if (today < today2) {
            return true;
        } else {
            $.ajax({

            }).done(function (data) {
                Swal.fire({
                    type: 'error',
                    title: 'La fecha debe ser mayor o igual a la actual',
                    confirmButtonText: 'OK'
                });
            });

            return false;
        }
    };
    var validacion2 = function () {
        $(this).click(function () {
            if (vacio()) {
                $.ajax({
                    url: "/EvaluarFormulario/RevisarFormulario2",
                    method: "POST",
                    data: $("#form").serialize()
                }).done(function (data) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se actualizó  exitosamente',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/EvaluarFormulario/Index";
                        }
                    });
                });
            }
            return false;
        });
    };
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
        });
        return false;
    };
    $("#auxfin").val($("#act_desa_fecha_fin").val());
    $("#auxfin").attr("readonly", "readonly");
    $("#guardar").each(validacion);
    $("#corregir").each(validacion2);
    $(".pcoded-content").on("click", ".pagedList a", getPage);

});