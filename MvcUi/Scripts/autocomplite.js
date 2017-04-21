
    $(document).ready(function () {
    
        $('*[data-autocomplete-url]')
            .each(function () {
                $(this).autocomplete(
                    {
                        serviceUrl: $(this).data('autocomplete-url'),
                    });
                $(this).autocomplete().disable();
                $(this).keyup(function () {
                    
                    if (this.value.length > 2) {

                        $(this).autocomplete().enable();
                    } else {
                        $(this).autocomplete().disable();
                    }
                });
            });
    });
