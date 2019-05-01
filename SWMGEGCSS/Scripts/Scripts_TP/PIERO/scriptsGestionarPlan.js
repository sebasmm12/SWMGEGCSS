$(function () {
       
    //var id_plan = 0;
    var getPage = function () {
        //var select = document.getElementById("estado");
        //var searchTerm = document.getElementById("searchTerm");
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            //data: { searchTerm: searchTerm.value, estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
            //$(".btnModal").each(envioajaxModal);
        });
        return false;
    };
    var autcompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
            //select: submitAutocompleteform
        };
        $input.autocomplete(options);
    };
    $("input[data-exp-autocomplete]").each(autcompletado);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
});