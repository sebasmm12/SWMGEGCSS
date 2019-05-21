$(function () {
    var cita_id;
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


    //var eliminaCita = function () {
    //    $.ajax({
    //        url: $(".btnEliminaCita").attr("data-url"),
    //        type: "POST",
    //        data: {
    //            cita_id: cita_id
    //        }
    //    }).done(function (data) {
    //        if (data === 1) {
    //            Swal.fire({
    //                type: 'success',
    //                title: 'Se elimino el servicio exitosamente',
    //                confirmButtonText: 'OK'
    //            }).then((result) => {
    //                if (result.value) {
    //                    window.location.href = "/"
    //                }
    //            });
    //        }
    //    });
    //};
    //var eliminaCita = function () {
    //    $.ajax({
    //        url: "/Secretario/Gestionar_Citas",
    //        type: "POST"
    //    }).done(function (data) {
    //        var $newhtml = $(data);
    //        var target = $("div.pagedList").attr("data-exp-target");
    //        $(target).replaceWith($newhtml);
    //    });
    //    return true;
    //}

    $(".btnModal").each(envioajaxModal);
    $(".btnModal1").each(envioajaxModal);
    $(".pcoded-content").on("click", ".pagedList a", getPage);
    //$(".btnEliminarCita").each(eliminaCita);
    //$("#btnEliminarCita").click(eliminaCita);

});