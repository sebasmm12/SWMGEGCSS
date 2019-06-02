$(function () {
    var id_servicio = 0;
    var page = 1;
    var submitAutocompleteform = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        $("#Buscar").click();
    };


    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-serv-autocomplete"),
            select: submitAutocompleteform
        };
        $input.autocomplete(options);
    };
    var BuscarServicio = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        $.ajax({
            url: "/Servicios/VisualizarServicios",
            data: {
                searchTerm: searchTerm.value,
                estado: select.value
            },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("div.pagedList").attr("data-serv-target");
            $(target).replaceWith($newhtml);
            $(".btnModal").each(cargarModal);

            });
        return true;
    };
    var getPage = function () {
        var select = document.getElementById("estado");
        var searchTerm = document.getElementById("searchTerm");
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-serv-target");
            $(target).replaceWith(data);
            $(".btnModal").each(cargarModal);
            $(".btnActivarServicio").each(activarservicio);
            });
        return false;
    };
    var getPagConsultasA = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pgCA").attr("data-serv-target");
            $(target).replaceWith(data);
        });
        return false;
    };
    var getPagConsultasE = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pgCE").attr("data-exp-target");
            $(target).replaceWith(data);      
        });
        return false;
    };
    var cargarModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            id_servicio = $(this).attr("data-id-servicio");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { tipo_servicio_id: $(this).attr("data-id-servicio") }
            }).done(function (data) {
                var $newhtml = $(data);
                var $target = $($button.attr("data-id-target"));
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(modal).on('shown.bs.modal', function () {
                    $(document).off('focusin.modal');
                });
                $(".pcoded-content").on("click", ".pgCA a", getPagConsultasA);
                $(".pcoded-content").on("click", ".pgCE a", getPagConsultasE);
                $(".btnEliminarServicio").each(alerta);
                $(".btnActualizarServicio").each(actualizarDatosServicio);
                $(".btnActivarServicio").each(activarservicio);
            });

        });
    };
    var alerta = function () {
        $(this).click(function () {
            Swal.fire({
                title: 'Estás seguro de elimianr el servicio?',
                text: "Si se elimina las actividades quedarán inactivas",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Eliminar Servicio'
            }).then((result) => {
                if (result.value) {
                    eliminarServicio();
                }
            });
        });

    };
    var eliminarServicio = function () {
        $.ajax({
            url: $(".btnEliminarServicio").attr("data-url"),
                type: "GET",
                data: {
                    tipo_servicio_id: id_servicio
                }

            }).done(function (data) {
                if (data === 1) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se elimino el servicio exitosamente',
                        confirmButtonText: 'OK'

                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Servicios/VisualizarServicios";
                        }
                    });  
                }
            });
    };
    var actualizarDatosServicio = function () {
        $(this).click(function () {
            var $nombreservicio = $("#servicio_nombre_act");
            var $descripcion = $("#servicio_descripción_act");
            var vnombre = validar_nombreservicio($nombreservicio.val());
            var vdescripción = validar_descripcion($descripcion.val());
            if (vnombre === false || vdescripción === false) {
                return false;
            } else {
                var $button = $(this);
                var servicio = {
                    tipo_servicio_id: $button.attr("data-id-serv"),
                    tipo_servicio_nombre: document.getElementById("servicio_nombre_act").value,
                    tipo_servicio_descripcion: document.getElementById("servicio_descripción_act").value
                };
                $.ajax({
                    url: $button.attr("data-url"),
                    data: {
                        servicio: servicio
                    },
                    type: "POST"

                }).done(function (data) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se actualizo el servicio exitosamente',
                        confirmButtonText: 'OK'

                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Servicios/VisualizarServicios";
                        }
                    });

                });
            }    
        });
    };
    function validar_nombreservicio(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var evalnombre = 0;
        if (id === "") {
            adderror("servicio_nombre_act");
            negativeattributes("validatenameServicio", 'Debe ingresar un nombre');
            $("#servicio_nombre_act").keyup(keyservicio);
            return false;
        } else {
            if ($("#servicio_nombre_act").val().match(RegularExpression)) {
                adderror("servicio_nombre_act");
                negativeattributes("validatenameServicio", 'Debe ingresar un nombre válido');
                $("#servicio_nombre_act").keyup(keyservicio);
                return false;
            } else {
                $.ajax({
                    url: "/Servicios/EvaluarNombreServicio",
                    async: false,
                    data: {
                        tipo_servicio_nombre: $("#servicio_nombre_act").val()
                    },
                    method: "GET"


                }).done(function (data) {
                    if (data === 1) {
                        adderror("servicio_nombre_act");
                        negativeattributes("validatenameServicio", 'Ya existe este nombre');
                        evalnombre = 0;
                    } else {
                        attributes("validatenameServicio");
                        addgood("servicio_nombre_act");
                        evalnombre = 1;
                    }
                });
            }
        }
        if (evalnombre === 1) {
            return true;
        }
        return false;
    }
    function validar_descripcion(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        if (id === "") {
            adderror("servicio_descripción_act");
            negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
            $("#servicio_descripción_act").keyup(keydescripcionservicio);
            return false;
        } else {
            if ($("#servicio_descripción_act").val().match(RegularExpression)) {
                adderror("servicio_descripción_act");
                negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
                $("#servicio_descripción_act").keyup(keyservicio);
                return false;
            } else {
                attributes("validatedescripcionServicio");
                addgood("servicio_descripción_act");
                return true;
            }
        }
    }
    var keyservicio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#servicio_nombre_act");
        if ($valor.val() === "") {
            adderror("servicio_nombre_act");
            negativeattributes("validatenameServicio", 'Debe ingresar un nombre');
        } else {
            if ($valor.val().match(RegularExpression)) {
                adderror("servicio_nombre_act");
                negativeattributes("validatenameServicio", 'Debe ingresar un nombre');
            } else {
                $.ajax({
                    url: "/Servicios/EvaluarNombreServicio",
                    async: false,
                    data: {
                        tipo_servicio_nombre: $valor.val()
                    },
                    method: "GET"


                }).done(function (data) {
                    if (data === 1) {
                        adderror("servicio_nombre_act");
                        negativeattributes("validatenameServicio", 'Ya existe este nombre');
                    } else {
                        attributes("validatenameServicio");
                        addgood("servicio_nombre_act");
                    }
                });
            }
        }
    };
    var keydescripcionservicio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#servicio_descripción_act");
        if ($valor.val() === "") {
            adderror("servicio_descripción_act");
            negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
        } else {

            if ($valor.val().match(RegularExpression)) {
                adderror("servicio_descripción_act");
                negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
            } else {
                attributes("validatedescripcionServicio");
                addgood("servicio_descripción_act");
            }
        }
    };
    var activarservicio = function () {
        $(this).click(function () {
            var $button = $(this);
            $.ajax({
                url: $button.attr("data-url"),
                data: {
                    tipo_servicio_id: $button.attr("data-id-servicio"),
                    page : page
                },
                type: "GET"
            }).done(function (data) {
                var $newhtml = $(data);
                var target = document.getElementById("tableServ");
                $(target).replaceWith($newhtml);
                $(".btnModal").each(cargarModal);
                $(".btnEliminarServicio").each(alerta);
                $(".btnActualizarServicio").each(actualizarDatosServicio);    
            });
        });
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
    $("input[data-serv-autocomplete]").each(autocompletado);
    $("#Buscar").click(BuscarServicio);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $(".btnModal").each(cargarModal);
    $(".btnActualizarServicio").each(actualizarDatosServicio);
    $(".btnActivarServicio").each(activarservicio);
});