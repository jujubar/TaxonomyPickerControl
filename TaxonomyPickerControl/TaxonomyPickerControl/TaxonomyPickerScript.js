(function ($, undefined) {
    $(document).ready(function () {


        //  Works, dont need to run this anymore
        //alert('Java Script from Control modified again');
        var termId = "9df7c69b-267c-4b8b-ab3c-ac5c15cbbfae";



        //bind the taxonomy picker to the default keywords termset
        $('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);

    });

})(jQuery);


//function () {
//    $.getScript(layoutsRoot + 'sp.taxonomy.js',
//        function () {
//            //termset used for dependant selection
//            var termId = "9df7c69b-267c-4b8b-ab3c-ac5c15cbbfae";

//            //bind the taxonomy picker to the default keywords termset
//            $('#taxPickerKeywords').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, context);

//            //bind taxpickers that depend on eachothers choices
//            $('#taxPickerContinent').taxpicker({ isMulti: false, allowFillIn: false, useKeywords: false, termSetId: termId, levelToShowTerms: 1 }, context, function () {
//                $('#taxPickerCountry').taxpicker({ isMulti: false, allowFillIn: false, useKeywords: false, termSetId: termId, filterTermId: this._selectedTerms[0].Id, levelToShowTerms: 2 }, context, function () {
//                    $('#taxPickerRegion').taxpicker({ isMulti: false, allowFillIn: false, useKeywords: false, termSetId: termId, filterTermId: this._selectedTerms[0].Id, levelToShowTerms: 3 }, context);
//                });
//            });

//            taxPickerIndex["#taxPickerContinent"] = 0;
//            taxPickerIndex["#taxPickerCountry"] = 4;
//            taxPickerIndex["#taxPickerRegion"] = 5;
//        });