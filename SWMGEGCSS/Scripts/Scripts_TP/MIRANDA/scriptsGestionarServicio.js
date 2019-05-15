$(function () {
    var id_servicio = 0;
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
            });

        });
    };


    $("input[data-serv-autocomplete]").each(autocompletado);
    $("#Buscar").click(BuscarServicio);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $(".btnModal").each(cargarModal);
});