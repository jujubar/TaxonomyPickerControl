using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//the namespaces used in the control 
using SourceCode.Forms.Controls.Web.SDK;
using SourceCode.Forms.Controls.Web.SDK.Attributes;
using SourceCode.Forms.Controls.Web.SDK.Utilities;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Other assemsblies 
using System.Data;  // For working with datatable
using SourceCode.SmartObjects.Client;  // For calling SMO
using SourceCode.Forms.Utilities;
using System.Web.Script.Serialization;

// Adding the JS file as a resource
[assembly: WebResource("TaxonomyPickerControl.TaxonomyPickerScript.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("TaxonomyPickerControl.taxonomypickercontrol.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("TaxonomyPickerControl.ControlScript.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("TaxonomyPickerControl.taxonomypickercontrol.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("TaxonomyPickerControl.Close.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.EMMCopyTerm.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.EMMDoubleTag.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.EMMTerm.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.EMMTermSet.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.MDNCollapsed.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.MDNExpanded.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.MDNNo.png", "image/png")]
[assembly: WebResource("TaxonomyPickerControl.menubuttonhover.png", "image/gif")]

namespace TaxonomyPickerControl
{
    [ControlTypeDefinition("TaxonomyPickerControl.Definition.xml")]
    [ClientScript("TaxonomyPickerControl.TaxonomyPickerScript.js")]
    [ClientScript("TaxonomyPickerControl.ControlScript.js")]
    [ClientScript("TaxonomyPickerControl.taxonomypickercontrol.js")]
    [ClientCss("TaxonomyPickerControl.taxonomypickercontrol.css")]

    public class TaxonomyPicker : BaseControl
    {

        public TaxonomyPicker() 
            : base("input")
        {       
            //base.Attributes.Add("id", "TaxonomyPickerControl");

        }

        public string TermStoreGuid { get; set; }
        public string TermSetGuid { get; set; }
        public string SMOInternalName { get; set; }

        private string getTaxData()
        {


               System.Text.StringBuilder sb = new System.Text.StringBuilder();


                SmartObjectClientServer smoServer = ConnectionClass.GetSmartObjectClient();
                SmartObject mmdSmo = smoServer.GetSmartObject(this.SMOInternalName);
                SmartListMethod  getList = mmdSmo.ListMethods["GetAllChildTermsInTermSet"];
                getList.InputProperties["TermStoreId"].Value = this.TermStoreGuid;
                getList.InputProperties["TermSetId"].Value = this.TermSetGuid;
                mmdSmo.MethodToExecute = "GetAllChildTermsInTermSet";
                

                var TermList = new List<Term>();
                SmartObjectList smoList = smoServer.ExecuteList(mmdSmo);
                foreach (SmartObject smo in smoList.SmartObjectsList)
                {
                    string termId = smo.Properties["TermId"].Value;
                    string termLabel = smo.Properties["Label"].Value;
                    string termPath = smo.Properties["Path"].Value;
                    TermList.Add(new Term() { TermId = termId, TermLabel = termLabel, TermPath = termPath });
                }
            var serializer = new JavaScriptSerializer();
            var serialized = serializer.Serialize(TermList);
            var fixedString = serialized.Replace("&quot;", @"""");
            sb.Append(@"{""terms"":");
            sb.Append(fixedString);
            sb.Append(@"}");
            return HttpUtility.HtmlDecode(sb.ToString());
        }

        protected override void CreateChildControls()
        {

            switch(base.State)
            {
                case SourceCode.Forms.Controls.Web.Shared.ControlState.Designtime:
                    this.Attributes.Add("type", "text");
                    break;
                case SourceCode.Forms.Controls.Web.Shared.ControlState.Preview:
                    this.Attributes.Add("type", "text");
                    break;
                case SourceCode.Forms.Controls.Web.Shared.ControlState.Runtime:
                    this.Attributes.Add("type", "hidden");
                    this.Attributes.Add("data-taxJSON", getTaxData());
                    break;
            }
            
            base.CreateChildControls();
        }

    }

        
    public class Term
    {

        public string TermId { get; set; }
        public string TermLabel { get; set; }
        public string TermPath { get; set; }
    }
}
