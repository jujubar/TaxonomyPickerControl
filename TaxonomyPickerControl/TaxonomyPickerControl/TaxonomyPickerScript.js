(function ($, undefined) {
    $(document).ready(function () {

        //bind the taxonomy picker to the default keywords termset
        $('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);

    });

    if (typeof TaxonomyPickerControl === "undefined" || TaxonomyPickerControl === nul) TaxonomyPickerControl = {};

    TaxonomyPickerControl.Textbox =
    {
        getValue: function (objInfo) {
            return $("#" + objInfo.CurrentControlID).val();
        },

        setValue: function (objInfo) {
            $("#" + objInof.CurrentControlID).val(ObjInfo.Value);
        }
    }
})(jQuery);