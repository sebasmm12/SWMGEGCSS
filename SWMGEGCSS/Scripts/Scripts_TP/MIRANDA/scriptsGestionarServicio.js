$(function () {

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
            });
        return false;
    };




    $("input[data-serv-autocomplete]").each(autocompletado);
    $("#Buscar").click(BuscarServicio);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
});