(function ($, undefined) {

    if (typeof CustomControls === "undefined" || CustomControls === null) CustomControls = {};

    CustomControls.TaxonomyPicker = 
        {
            getValue: function (objInfo)
            {
                return $("#" + objInfo.CurrentControlID).val();
            },

            setValue: function (objInfo)
            {
                $("#" + objInfo.CurrentControlID).val(objInfo.Value);
            }
        }
}) 