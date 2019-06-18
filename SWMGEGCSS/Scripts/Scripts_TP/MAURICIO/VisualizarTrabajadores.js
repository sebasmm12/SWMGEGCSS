$(function () {
    var getPage = function () {
        var $a = $(this);
        $.ajax({
            url: $a.attr("href"),
            data: { estado: select.value },
            type: "GET"
        }).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-exp-target");
            $(target).replaceWith(data);
            $(".btnModal").each(envioajaxModal);
        });
        return false;
    };
    var envioajaxModal = function () {
        $(this).click(function () {
            var $button = $(this);
            var modal = $button.attr("data-id-target");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { usu_codigo: $(this).attr("data-id-actividad") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
            });
            return false;
        });


    };
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $(".btnModal").each(envioajaxModal);
});