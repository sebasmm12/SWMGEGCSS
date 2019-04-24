$(function () {
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);

    };

    $("#btnModal").click(function () {
        $("#ModalProyecto").modal('show');
    });
    $("input[data-exp-autocomplete]").each(autcompletado);

    
});