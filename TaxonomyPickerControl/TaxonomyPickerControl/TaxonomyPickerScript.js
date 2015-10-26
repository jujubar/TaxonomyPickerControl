(function ($, undefined) {
    $(document).ready(function () {

        //bind the taxonomy picker to the default keywords termset
        $('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);

    });

})(jQuery);