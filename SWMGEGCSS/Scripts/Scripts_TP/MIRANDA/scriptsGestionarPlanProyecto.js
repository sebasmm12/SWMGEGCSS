$(function () {
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);

    };

    $(".btnModal").each(function () {
        $(this).click(function () {
            var $button = $(this);
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { id: $(this).attr("data-id-proyecto")}
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var modal = $button.attr("data-id-target");
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal('show');

            });

           
        });
    });
    $("input[data-exp-autocomplete]").each(autcompletado);

    
});