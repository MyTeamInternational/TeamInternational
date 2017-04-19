$(document).ready(function () {
    $('*[data-autocomplete-url]')
        .each(function () {
            $(this).autocomplete(
                {
                    serviceUrl: $(this).data('autocomplete-url')
                });
        });
});