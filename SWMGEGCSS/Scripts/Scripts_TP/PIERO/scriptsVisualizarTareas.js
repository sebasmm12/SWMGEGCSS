$(function () {
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
        });
        return false;
    };
    $("input[data-exp-autocomplete]").each(autcompletado);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
});