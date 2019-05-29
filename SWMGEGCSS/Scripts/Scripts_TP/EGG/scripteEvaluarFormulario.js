$(function () {
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
            if (vacio()) {

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
        today.setHours(0, 0, 0, 0);
        if ($("#act_desa_fecha_fin").val().toString() >= today.toString()) {
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