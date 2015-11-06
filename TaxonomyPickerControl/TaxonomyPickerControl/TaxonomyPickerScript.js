(function ($, undefined) {
    $(document).ready(function () {

        $(document).delegate('.TaxonomyPickerControl-TaxonomyPicker', 'change.TaxonomyPickerControl-TaxonomyPicker', function (e) {
            //SFC SourceCode-Forms-Controls-Web-Button

            raiseEvent(this.id, 'Control', 'OnChange');

        });



        //bind the taxonomy picker to the default keywords termset
        //$('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);
        $('.TaxonomyPickerControl-TaxonomyPicker').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true },null, function (objInfo) {
    
            raiseEvent($('.TaxonomyPickerControl-TaxonomyPicker').attr('id'), 'Control', 'OnChange');
        });

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
            
            // Need to add something to watch for change. - Maybe not, there is already a change event available.
            // var beforeHtml = $.taxpicker[0].selectedTermsToHtml();

            // May need to update - Not sure about the objInfo value
            $.each(objInfo.Value.split(";"), function (index, value) {
                var term = $.taxpicker[0].TermSet.getTermById(value);
                $.taxpicker[0]._selectedTerms.push(term);
            });
            $.taxpicker[0]._editor.html($.taxpicker[0].selectedTermsToHtml());

            //if (beforeHtml == $.taxpicker[0].selectedTermsToHtml()) {
            //    raiseEvent(objInfo.CurrentControlID, 'Control', 'OnChange');
            //}
        }
    }
})(jQuery);