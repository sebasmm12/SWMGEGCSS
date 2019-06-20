$(function () {
    var id_proyecto = 0;
    var getPage = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
            $(".btnModal").each(envioajaxModal); 
            $(".btnSubModal").each(envioAjaxActividad);
        });
        return false;
    };
    var submitAutocompleteform = function (event,ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        $("#Buscar").click();
    };
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete"),
            select: submitAutocompleteform
        };
        $input.autocomplete(options);

    };
    var Alerta = function () {
        $(this).click(function () {

            (async function getEmail() {
                const { value: comentario } = await Swal.fire({
                    title: 'Ingrese el porque quiere eliminarlo',
                    input: 'textarea',
                    inputPlaceholder: 'Ingrese comentario',
                    showCancelButton: true,
                    inputValidator: (value) => {
                        if (!value) {
                            return 'Debe escribir algo!';
                        }
                    }
                });

                if (comentario) {
                    EnvioComentario(comentario);
                }
            })();

        });
    };
    var AlertaActividad = function () {

        (async function getEmail() {
            const { value: comentario } = await Swal.fire({
                title: 'Ingrese el porque quiere cambiar las actividades',
                input: 'textarea',
                inputPlaceholder: 'Ingrese comentario',
                showCancelButton: true,
                inputValidator: (value) => {
                    if (!value) {
                        return 'Debe escribir algo!';
                    }
                }
            });

            if (comentario) {
                EnvioComentarioFinal(comentario);
            }
        })();
    };
    var EnvioComentarioFinal = function (comentario) {
        $.ajax({
            url: "/Expediente/ModificarExpediente",
            method: "GET",
            data: {
                comentario: comentario
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            Swal.fire({
                type: 'success',
                title: 'Se modifico los datos exitosamente',
                confirmButtonText: 'OK'

            }).then((result) => {
                if (result.value) {
                    window.location.href = "/Gerente/Gestionar_Proyectos";
                }
            }); 

        });
        return false;
    };

    var EnvioComentario = function (comentario) {
        $.ajax({
            url: "/Expediente/EliminarExpediente",
            method: "GET",
            data: {
                id: id_proyecto,
                comentario: comentario
            },
            contentType: "application/json; charset=utf-8",
            dataType:"json"
        }).done(function (data) {
                window.location.href = "/Gerente/Gestionar_Proyectos";
              
            });
        return false;
    };
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_proyecto = $(this).attr("data-id-proyecto");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { id: $(this).attr("data-id-proyecto") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(modal).on('shown.bs.modal', function () {
                    $(document).off('focusin.modal');
                });
                $(".btnEliminar").each(Alerta);
                $(".btnSubModal").each(envioAjaxActividad);
                $("#btnActualizarTotal").click(enviarActualizarTotal);
                $(".btnArchivo").each(envioarchivoMinam);
                });
            return false;
        });
    };
    var BuscarProyecto = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        $.ajax({
            url: "/Gerente/Gestionar_Proyectos",
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("div.pagedList").attr("data-exp-target");
            $(target).replaceWith($newhtml);
            $(".btnModal").each(envioajaxModal);
            $(".btnSubModal").each(envioAjaxActividad);
            $("#btnActualizarTotal").click(enviarActualizarTotal);
            $(".btnArchivo").each(envioarchivoMinam);
            });

        return true;
    };
    var envioAjaxActividad = function () {
        $(this).click(function () {
            var $button = $(this);

            $.ajax({
                url: $button.attr("data-url"),
                data: { act_nombre: $button.attr("data-id-target"), exp_id: $button.attr("data-exp-target") },
                contentType: "json"
            }).done(function (data) {

                if (data === 1) {

                    $button.removeClass("btn-success");
                    $button.addClass("btn-danger");
                    $button.attr("data-url", "/Expediente/EliminarActividad");
                    $button.text("Eliminar Actividad");
                    $button.text("");
                    $button.html();
                    $button.append("<i class='fa fa-times-circle'></i> Eliminar Actividad");
                } else {
                    $button.removeClass("btn-danger");
                    $button.addClass("btn-success");
                    $button.attr("data-url", "/Expediente/AñadirActividad");
                    $button.text("");
                    $button.html();
                    $button.append("<i class='fa fa-check-circle'></i> Insertar Actividad");
                }
               
                }
           ); 
        });
    };
    var enviarActualizarTotal = function () {
        $.ajax({
            url: "/Expediente/Comprobar_Validacion_Actividades",
            dataType: "json"
        }).done(function (data) {
            if (data !== 0) {
                AlertaActividad();
            } else {
                $.ajax({
                    url: "/Expediente/ModificarExpediente",
                    method:"POST",
                    dataType: "json"
                }).done(function (data) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se modifico los datos exitosamente',
                        confirmButtonText: 'OK'

                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Gerente/Gestionar_Proyectos";
                        }
                    }); 
                });
            }
        });
    };
    var envioarchivoMinam = function () {
        $(this).click(function () {
            var Expediente = new FormData($("#form")[0]);
            var $archivo = $("#newfile");
            var vArchivo = validar_Archivo($archivo.val());
            if (vArchivo === false) {
                return false;
            } else {
                $.ajax({
                    url: "/Expediente/_AgregarArchivo",
                    type: "POST",
                    data: Expediente,
                    contentType: false, //importante enviar este parametro en false
                    processData: false //importante enviar este parametro en false
                }).done(function (data) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se finalizo el proyecto exitosamente',
                        confirmButtonText: 'OK'

                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Gerente/Gestionar_Proyectos";
                        }
                    });
                });
            }
          
        });
    };
    //Validacion File
    function validar_Archivo(id) {
        var archivo = id;
        if (archivo === '') {
            adderror("newfile");
            negativeattributes("validateArchivo", 'Debe subir un archivo');
            $("#newfile").focus();
            $("#newfile").change(keyF);
            return false;
        }
        return true;
    }
    var keyF = function () {
        var $archivo = $("#newfile");

        if ($archivo.val() === "") {
            negativeattributes("validateArchivo", 'Debe subir un archivo');
            adderror("newfile");
        }
        else {
            attributes("validateArchivo");
            addgood("newfile");
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

    $(".btnModal").each(envioajaxModal);        
    $("input[data-exp-autocomplete]").each(autcompletado);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $("#Buscar").click(BuscarProyecto);
    $(".btnSubModal").each(envioAjaxActividad);
    $("#btnActualizarTotal").click(enviarActualizarTotal);
});