using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CustomTranslatorSampleCode.Controllers
{

    public class HomeController : Controller
    {
        static string tenant = "common"; // Tenant for Custom Translator
        static string clientID = "1f3472a0-2738-4e9f-9092-19cecb8b8efa"; // Enter your ClientID that you created in https://apps.dev.microsoft.com 
        static string redirectUri = "http://localhost/home/index1"; // Ensure this URL is added to the Redirect URLs section (Platform = Web) for your client app
        static string clientsecret = "nkffvQ27|$cbHOUEPD323*^"; // Enter App Secret for your client app
        static string authorityUri = $"https://login.microsoftonline.com/{tenant}/oauth2/v2.0";  
        static string resourceUri = "api://6981666b-e0e0-47d6-a039-35318677bf79/access_as_user"; 

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var url = $"{authorityUri}/authorize";
                UriBuilder uriBuilder = new UriBuilder(url);
                uriBuilder.Query = String.Format("&client_id={0}&response_type={1}&scope={2}&nonce={3}&redirect_uri={4}&response_mode={5}&state={6}", clientID, "id_token+code", HttpUtility.UrlEncode("openid offline_access profile email " + resourceUri), Guid.NewGuid(), redirectUri, "form_post", "1112323423234");
                return Redirect(uriBuilder.Uri.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.InnerException);
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index1()
        {
            string form = Request.Form.ToString();
            var activationCode = this.Request.Form["code"].ToString();
            var url = $"{authorityUri}/token";
            string signInReturnUrl = Request.Url.ToString();
            Dictionary<string, string> postData = new Dictionary<string, string> { { "grant_type", "authorization_code" }, { "client_id", clientID }, { "redirect_uri", signInReturnUrl }, { "client_secret", clientsecret }, { "code", activationCode } };

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(postData));
            string responseContent = await response.Content.ReadAsStringAsync();


            JObject responseObj = JsonConvert.DeserializeObject<JObject>(responseContent);
            string tokentype = responseObj["token_type"].Value<string>();
            string accesstoken = responseObj["access_token"].Value<string>();
            string refreshtoken = responseObj["refresh_token"].Value<string>();
            string token_header = tokentype + " " + accesstoken;

            Session["token_header"] = token_header;

            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            /****************************** API CALLS*************************************/

            //string result = await clientapp.GetLanguages(token_header);
            //string result = await clientapp.GetProjects(token_header,1,"","");
            //string result = await clientapp.GetProject("cac9e32e-f3a3-4e6d-863f-0ee96c612ae8", token_header); 
            //string result = await clientapp.CreateProject(token_header);
            //string result = await clientapp.EditProject("27368779-b2e4-44a6-a4f5-b480a3fc26ba", token_header); .
            //string result = await clientapp.DeleteProject("27368779-b2e4-44a6-a4f5-b480a3fc26ba", token_header);
            //string result = await clientapp.ImportComboDocument(token_header);

            
            //string result = await clientapp.ExportDocument(62108, token_header); 
            //string result = await clientapp.ExportDocument(58712, token_header);
            //Response.Write("Supported Languages (JSON):<br /><br />" + result);

            /****************************** END API CALLS*************************************/

            return View();
        }
        /// <summary>
        /// In this Scenarion
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Scenario1()
        {
            Response.Write("<br />" + Session["token_header"] + "<br />");

            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
 