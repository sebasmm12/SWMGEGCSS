$(function () {
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
            $(".btnModal1").each(envioajaxModal);
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
                data: { id: $(this).attr("data-id-cita") }
            }).done(function (data) {
                var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
            });
            return false;
        });
    };

    var autocompletado = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-exp-autocomplete")
        };
        $input.autocomplete(options);
    };

    var BuscarProyecto = function () {
        var $searchTerm = $('#searchTerm').val();
        var $select = $('#estado').val();
        $.ajax({
            url: "/Gerente/Gestionar_Cuenta",
            data: { searchTerm: $searchTerm, estado: $select },
            type: "GET"
        }).done(function (data) {
            var $newhtml = $(data);
            var target = $("div.pagedList").attr("data-exp-target");
            $(target).replaceWith($newhtml);
            $(".btnModal").each(envioajaxModal);
            $(".btnModal1").each(envioajaxModal);
        });
        return true;
    }

    
    $(".btnModal").each(envioajaxModal);
    $(".btnModal1").each(envioajaxModal);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    $("input[data-exp-autocomplete]").each(autocompletado);
    $("#Buscar").click(BuscarProyecto);
    
});

