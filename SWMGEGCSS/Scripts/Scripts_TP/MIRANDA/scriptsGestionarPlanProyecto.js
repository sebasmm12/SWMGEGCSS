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
                            return 'Debe escribir algo!'
                        }
                    }
                });

                if (comentario) {
                    EnvioComentario(comentario);
                }
            })();

        });
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
            });

        return true;
    };
    $(".btnModal").each(envioajaxModal);        
    $("input[data-exp-autocomplete]").each(autcompletado);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $("#Buscar").click(BuscarProyecto);
});