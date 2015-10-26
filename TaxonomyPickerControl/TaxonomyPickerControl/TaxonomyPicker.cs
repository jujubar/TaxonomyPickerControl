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
//using SourceCode.Forms.Controls.Web.Utilities; // For using the ConnectionClass
using SourceCode.Forms.Utilities;
//using SourceCode.Forms.Controls.Web.Shared;
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
       
            //   base.Attributes.Add("type", "hidden"); //needs to be hidden
            base.Attributes.Add("id", "TaxonomyPickerControl");

         //   base.Attributes.Add("data-taxJSON", @"{""TermId"":""1234"",""TermLabel"":""aLabel"",""TermPath"":""alabel""}");
        }

     

        private string getTaxData()
        {


               System.Text.StringBuilder sb = new System.Text.StringBuilder();


                SmartObjectClientServer smoServer = ConnectionClass.GetSmartObjectClient();

                //  Just a SP List SMO??
                //SmartObject aceReplicationTrackerList = Server.GetSmartObject(new Guid("4ffb5465-2022-4a2c-83a6-130083858a03"));
                SmartObject mmdSmo = smoServer.GetSmartObject("portal_denallix_com_Management_Taxonomy");
                SmartListMethod  getList = mmdSmo.ListMethods["GetAllChildTermsInTermSet"];
                getList.InputProperties["TermStoreId"].Value = "0dd4b72451474d17a41b303ff2505c5f";
                getList.InputProperties["TermSetId"].Value = "b2534bad-9f43-496b-a759-c33d6e4d59b6";
                mmdSmo.MethodToExecute = "GetAllChildTermsInTermSet";
              

                //sb.Append(@"{""terms"":[");
                var TermList = new List<Term>();
                //getList.Filter = andCondition;
                SmartObjectList smoList = smoServer.ExecuteList(mmdSmo);
                foreach (SmartObject smo in smoList.SmartObjectsList)
                {
                    string termId = smo.Properties["TermId"].Value;
                    string termLabel = smo.Properties["Label"].Value;
                    string termPath = smo.Properties["Path"].Value;
                    //sb.Append(termId + ":" + termLabel + ":" + termPath);
                    //sb.Append(@"{""TermId"":""" + termId +"}")
                    TermList.Add(new Term() { TermId = termId, TermLabel = termLabel, TermPath = termPath });
                }
                //sb.
                //sb.Append(@"]}");
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
            //this.Attributes.Add();

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
                    //this.Attributes.Add("data-taxJSON", @"{""TermId"":""1234"",""TermLabel"":""aLabel"",""TermPath"":""alabel""}");
                    this.Attributes.Add("data-taxJSON", getTaxData());
                    //Page.ClientScript.RegisterStartupScript(
                    //    typeof(Page), "TESTETSE", "$('#TaxonomyPickerControl').taxpicker({ isMulti: true, allowFillIn: true, useKeywords: true }, null);", true
                    //    );
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
