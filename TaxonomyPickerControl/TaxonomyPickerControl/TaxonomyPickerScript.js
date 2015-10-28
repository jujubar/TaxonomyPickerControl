(function ($, undefined) {
    $(document).ready(function () {

        //bind the taxonomy picker to the default keywords termset
        $('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);

    });

    if (typeof TaxonomyPickerControl === "undefined" || TaxonomyPickerControl === null) TaxonomyPickerControl = {};

    TaxonomyPickerControl.Textbox =
    {
        getValue: function (objInfo) {
            var selectedTerms = "";
            $.each($.taxpicker[0]._selectedTerms, function (index, value) {
                selectedTerms += value.Name;
            });

            return selectedTerms;
        },

        setValue: function (objInfo) {
            
            // May need to update - Not sure about the objInfo value
            $.each(objInfo.Value.split(";"), function (index, value) {
                var term = $.taxpicker[0].TermSet.getTermById(value);
                $.taxpicker[0]._selectedTerms.push(term);
            });
            $.taxpicker[0]._editor.html($.taxpicker[0].selectedTermsToHtml());
        }
    }
})(jQuery);