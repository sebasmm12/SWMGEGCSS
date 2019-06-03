$(function () {
    var modal;
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-serv-target");
            $(target).replaceWith(data);
            $(".btnModal").each(envioajaxModal);
            $(".btnEliminar").each(eliminarActividad);
            $(".btnRegistrar").each(registrarActividadParcial);
        });
        return false;
    };
    var añadir = function () {

        var $button = $(this);
        var selector = document.getElementById("actividad");
        $.ajax({
            url: "/Servicios/EvaluarActividades",
            async: false,
            data: {
                tipo_actividad: selector.value
            },
            type: "GET"
        }).done(function (data) {
            if (data === 1) {
                adderror("actividad");
                negativeattributes("validateActividad", 'Ya existe este nombre');
            } else {
                attributes("validateActividad");
                addgood("actividad");
                $.ajax({
                    url: $button.attr("data-url"),
                    data: {
                        actividad: selector.value
                    },
                    type: "GET"
                }).done(function (data) {
                    var $newhtml = $(data);
                    var target = document.getElementById("tableAct");
                    $(target).replaceWith($newhtml);
                    $(".btnModal").each(envioajaxModal);
                    $(".btnEliminar").each(eliminarActividad);
                    $(".btnRegistrar").each(registrarActividadParcial);
                });
            }

        });
        
        return true;
    };
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            modal = $button.attr("data-id-target");
            $.ajax({
                url: $button.attr("data-url"),
                data: {
                    id: $button.attr("data-id-actividad")
                },
                type: "POST"
            }).done(function (data) {
                var $newhtml = $(data);
                var $target = $($button.attr("data-id-target"));
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(".btnEliminar").each(eliminarActividad);
                $(".btnRegistrar").each(registrarActividadParcial);
            });
        });
        return true;
    };
    var eliminarActividad = function () {
        $(this).click(function () {
            var $button = $(this);
            $.ajax({
                url: $button.attr("data-url"),
                data: {
                    id: $button.attr("data-id-actividad")
                },
                type: "GET"

            }).done(function (data) {
                var $newhtml = $(data);
                var target = document.getElementById("tableAct");
                $(target).replaceWith($newhtml);
                $(".btnModal").each(envioajaxModal);
                $(".btnEliminar").each(eliminarActividad);
                $(".btnRegistrar").each(registrarActividadParcial);
            });
        });
        
        return true;
    };
    var validar_costo_actividad = function (id) {
        var RegularExpression = /^\d+[,]*\d*$/;
        if (id === ""||id==="0") {
            adderror("actividades_costo");
            negativeattributes("validatecostoActividad", 'Debe ingresar un costo');
            $("#actividades_costo").keyup(keycostoactividad);
            return false;
        } else {
            if ($("#actividades_costo").val().match(RegularExpression)) {
                attributes("validatecostoActividad");
                addgood("actividades_costo");
                return true;
            } else {
                adderror("actividades_costo");
                negativeattributes("validatecostoActividad", 'Debe ingresar un costo válido');
                $("#actividades_costo").keyup(keycostoactividad);
                return false;
            }
        }
    };
    var keycostoactividad = function () {
        var RegularExpression = /^\d+[,]*\d*$/;
        var $valor = $("#actividades_costo");
        if ($valor.val() === "" || $valor.val() === "0") {
            adderror("actividades_costo");
            negativeattributes("validatecostoActividad", 'Debe ingresar un costo');
            $("#actividades_costo").keyup(keycostoactividad);
            return false;
        } else {
            if ($valor.val().match(RegularExpression)) {
                attributes("validatecostoActividad");
                addgood("actividades_costo");
                return true;
            } else {
                adderror("actividades_costo");
                negativeattributes("validatecostoActividad", 'Debe ingresar un costo válido');
                $("#actividades_costo").keyup(keycostoactividad);
                return false;
            }
        }
    };
    var registrarActividadParcial = function () {
        $(this).click(function () {
            var $costo = $("#actividades_costo");
            var vcosto_actividad = validar_costo_actividad($costo.val());
            if (vcosto_actividad === false) {
                return false;
            } else {
                var $button = $(this);
                let params = new URLSearchParams(location.search);
                var page = params.get('page');
                var actividad = {
                    act_id: $button.attr("data-id-act"),
                    act_nombre: document.getElementById("actividades_act_nombre").value,
                    act_descripcion: document.getElementById("actividades_act_descripcion").value,
                    costo: document.getElementById("actividades_costo").value,
                    act_obligatorio: document.getElementById("est_actividad").value
                };
                $.ajax({
                    url: "/Servicios/ModificarDatosActividad",
                    data: {
                        actividad: actividad,
                        page: page
                    },
                    type: "POST"

                }).done(function (data) {
                    var $newhtml = $(data);
                    var target = document.getElementById("tableAct");
                    $(target).replaceWith($newhtml);
                    $(modal).modal('hide');
                    $("#btnRegistrarActividad").click(añadir);
                    $(".btnModal").each(envioajaxModal);
                    $(".btnEliminar").each(eliminarActividad);
                });
            }
            });       
    };
    var registrarServicioCompleto = function () {
        var $nombreservicio = $("#tipoServicio_tipo_servicio_nombre");
        var $descripcion = $("#tipoServicio_tipo_servicio_descripcion");
        var vnombre = validar_nombreservicio($nombreservicio.val());
        var vdescripción = validar_descripcion($descripcion.val());
        var evalactividades = validar_g_actividades();
        if (vnombre === false || vdescripción === false || vnombre === false || evalactividades===false) {
            return false;
        } else {
            var servicio = {
                tipo_servicio_nombre: document.getElementById("tipoServicio_tipo_servicio_nombre").value,
                tipo_servicio_descripcion: document.getElementById("tipoServicio_tipo_servicio_descripcion").value
            };
            $.ajax({
                url: "/Servicios/RegistrarServicioActividad",
                data: {
                    servicio: servicio
                },
                type: "POST"

            }).done(function (data) {
                if (data === 1) {
                    Swal.fire({
                        type: 'success',
                        title: 'Se registro el servicio exitosamente',
                        confirmButtonText: 'OK'

                    }).then((result) => {
                        if (result.value) {
                            window.location.href = "/Servicios/VisualizarServicios";
                        }
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Registro Incompleto de costos',
                        text:  'No registro todos los costos de las actividades'
                    });
                }

            });
        }
       
    };
    var validar_g_actividades = function () {
        var cont = 0;
        $.ajax({
            url: "/Servicios/ContarActividades",
            method: "GET",
            async: false

        }).done(function (data) {
            if (data === 0) {
                adderror("actividad");
                negativeattributes("validateActividad", 'No registro ninguna actividad');
                cont++;
            }
            });
        if (cont >0) {
            return false;
        }
        return true;
    };
    function validar_nombreservicio(id) {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var evalnombre = 0;
        if (id === "") {
            adderror("tipoServicio_tipo_servicio_nombre");
            negativeattributes("validatenameServicio", 'Debe ingresar un nombre');
            $("#tipoServicio_tipo_servicio_nombre").keyup(keyservicio);
            return false;
        } else {
            if ($("#tipoServicio_tipo_servicio_nombre").val().match(RegularExpression)) {
                adderror("tipoServicio_tipo_servicio_nombre");
                negativeattributes("validatenameServicio", 'Debe ingresar un nombre válido');
                $("#tipoServicio_tipo_servicio_nombre").keyup(keyservicio);
                return false;
            } else {
                $.ajax({
                    url: "/Servicios/EvaluarNombreServicio",
                    async: false,
                    data: {
                        tipo_servicio_nombre: $("#tipoServicio_tipo_servicio_nombre").val()
                    },
                    method: "GET"


                }).done(function (data) {
                    if (data === 1) {
                        adderror("tipoServicio_tipo_servicio_nombre");
                        negativeattributes("validatenameServicio", 'Ya existe este nombre');
                        evalnombre = 0;
                    } else {
                        attributes("validatenameServicio");
                        addgood("tipoServicio_tipo_servicio_nombre");
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
            adderror("tipoServicio_tipo_servicio_descripcion");
            negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
            $("#tipoServicio_tipo_servicio_descripcion").keyup(keydescripcionservicio);
            return false;
        } else {
            if ($("#tipoServicio_tipo_servicio_descripcion").val().match(RegularExpression)) {
                adderror("tipoServicio_tipo_servicio_descripcion");
                negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
                $("#tipoServicio_tipo_servicio_nombre").keyup(keyservicio);
                return false;
            } else {
                attributes("validatedescripcionServicio");
                addgood("tipoServicio_tipo_servicio_descripcion");
                return true;
            }
        }
    }
    var keyservicio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#tipoServicio_tipo_servicio_nombre");
        if ($valor.val() === "") {
            adderror("tipoServicio_tipo_servicio_nombre");
            negativeattributes("validatenameServicio", 'Debe ingresar un nombre');
        } else {
            if ($valor.val().match(RegularExpression)) {
                adderror("tipoServicio_tipo_servicio_nombre");
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
                        adderror("tipoServicio_tipo_servicio_nombre");
                        negativeattributes("validatenameServicio", 'Ya existe este nombre');
                    } else {
                        attributes("validatenameServicio");
                        addgood("tipoServicio_tipo_servicio_nombre");
                    }
                });



                
            }
        }
    };
    var keydescripcionservicio = function () {
        var RegularExpression = /(^\s.*)|(.*\s{2,}.*)|.*\s$|(.*[+-\.\*@0-9-_\|/?¿?´`º!ª\\¨{\][}ç\^<>¬%&()·].*)/;
        var $valor = $("#tipoServicio_tipo_servicio_descripcion");
        if ($valor.val() === "") {
            adderror("tipoServicio_tipo_servicio_descripcion");
            negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
        } else {
            
            if ($valor.val().match(RegularExpression)) {
                adderror("tipoServicio_tipo_servicio_descripcion");
                negativeattributes("validatedescripcionServicio", 'Debe ingresar una descripción');
            } else {
                attributes("validatedescripcionServicio");
                addgood("tipoServicio_tipo_servicio_descripcion");
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
    $("#btnRegistrarActividad").click(añadir);
    $(".btnModal").each(envioajaxModal);
    $(".btnEliminar").each(eliminarActividad);
    $(".btnRegistrar").each(registrarActividadParcial);
    $("#btnRegistrarServicio").click(registrarServicioCompleto);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
});