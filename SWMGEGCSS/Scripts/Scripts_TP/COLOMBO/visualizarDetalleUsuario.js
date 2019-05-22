$(function () {
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList1").attr("data-exp-target");
            $(target).replaceWith(data);
        });
        return false;
    };
    $(".pcoded-content").on("click", ".pagedList1 a", getPage);
});