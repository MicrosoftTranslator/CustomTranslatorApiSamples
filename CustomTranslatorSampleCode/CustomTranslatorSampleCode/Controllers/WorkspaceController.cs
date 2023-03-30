using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace CustomTranslatorSampleCode.Controllers
{
    public class WorkspaceController : Controller
    {
        // GET: Workspace
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            var clientapp = new CustomTranslatorAPIClient();

            WorkspaceCreateRequest newWorkspace = new WorkspaceCreateRequest();
            newWorkspace.Name = "..."; // Enter workspace name
            newWorkspace.Subscription = new Subscription()
            {
                SubscriptionKey = "...",  // Enter your subscription_key, you can fetch it from the "Keys and Endpoint" tab in the translator resource 
                BillingRegionCode = "...", // Enter billing region for the subscription, can be found on the azure portal. The code is determinded in the GetBillingRegions, eg: WEU
            };

            string billingRegionResult = await clientapp.GetBillingRegions();
            List<BillingRegionDetails> billingRegionList = getBillingRegionList(billingRegionResult);
            BillingRegionDetails billingRegion = billingRegionList.Find(c => c.billingRegionCode == newWorkspace.Subscription.BillingRegionCode);

            string result = await clientapp.CreateWorkspace(newWorkspace);
            string[] resultarray = result.Split('/');

            if (resultarray.Length > 1)
            {
                string newWorkspaceId = resultarray[resultarray.Length - 1];
                Response.Write("<div class=\"jumbotron\">");
                Response.Write("<br/>New Worspace Created");
                Response.Write("<br/>Workspace Id: " + newWorkspaceId);
                Response.Write("<br/>Workspace Name: " + newWorkspace.Name);
                Response.Write("</div>");
            }
            else
            {
                Response.Write("<br/>Could not create Workspace: " + result);
            }

            return View();
        }

        static List<BillingRegionDetails> getBillingRegionList(string result)
        {
            List<BillingRegionDetails> billingRegionDetailsList = JsonConvert.DeserializeObject<List<BillingRegionDetails>>(result);
            return billingRegionDetailsList;
        }
    }
}