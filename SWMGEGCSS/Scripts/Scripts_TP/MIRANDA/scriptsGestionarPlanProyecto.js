$(function () {
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);

    };


    $("input[data-exp-autocomplete]").each(autcompletado);
});