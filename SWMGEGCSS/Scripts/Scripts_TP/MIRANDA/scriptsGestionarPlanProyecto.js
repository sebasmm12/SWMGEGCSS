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
            var modal = $button.attr("data-id-target");
            $.ajax({
                url: $(this).attr("data-url"),
                method: "POST",
                data: { id: $(this).attr("data-id-proyecto") }
            }).done(function (data) {
               var $target = $($button.attr("data-id-target"));
                var $newhtml = $(data);
                $target.replaceWith($newhtml);
                $(modal).modal();
                $(modal).on('shown.bs.modal', function () {
                    $(document).off('focusin.modal');
                });
                $(".btnEliminar").each(function () {
                    $(this).click(function () {
                       
                        (async function getEmail() {
                            const { value: email } = await Swal.fire({
                                title: 'Input email address',
                                input: 'email',
                                inputPlaceholder: 'Enter your email address'
                            })

                            if (email) {
                                Swal.fire('Entered email: ' + email)
                            }
                        })()
                        
                    });
               });
                });
        });
        
    });

    $("input[data-exp-autocomplete]").each(autcompletado);

    
});