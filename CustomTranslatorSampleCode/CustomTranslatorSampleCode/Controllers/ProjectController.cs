using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomTranslatorSampleCode.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            string token_header = null;

            if (Session["token_header"] != null)
            {
                token_header = Session["token_header"].ToString();
            }

            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            ProjectCreateRequest newproject = new ProjectCreateRequest();
            newproject.name = "..."; // Enter Project Name
            newproject.languagePairId = 18; //Determined from the call to GetLanguagePairs
            newproject.categoryid = 1; //Determined from the call to GetCategories 
            newproject.categoryDescriptor = "..."; // Enter Project Category Descriptor
            newproject.label = "..."; // Enter Project Label
            newproject.description = "..."; // Enter Project Decription

            string categoryresult = await clientapp.GetCategories(token_header);
            List<Category> categorylist = getCategoryList(categoryresult);
            Category category = categorylist.Find(c => c.id == newproject.categoryid);

            string lpresult = await clientapp.GetLanguagePairs(token_header);
            List<LanguagePair> languagePairList = getLanguagePairList(lpresult);
            LanguagePair languagePair = languagePairList.Find(lp => lp.id == newproject.languagePairId);

            string result = await clientapp.CreateProject(token_header, newproject, Session["ws_id"].ToString()); 
            string[] resultarray = result.Split('/');

            if (resultarray.Length > 1)
            {
                string newprojectid = resultarray[resultarray.Length - 1];
                Response.Write("<br/>New Project Created");
                Response.Write("<br/>Project Id: " + newprojectid);
                Response.Write("<br/>Project Name: " + newproject.name);
                Response.Write("<br/>Language Pair: " + languagePair.sourceLanguage.displayName + " to " + languagePair.targetLanguage.displayName);
                Response.Write("<br/>Project Category: " + category.name);
                Response.Write("<br/>Project Label: " + newproject.label);
                Response.Write("<br/>Project Description: " + newproject.description);
            }
            else
            {
                Response.Write("<br/>Could not create project: " + result);
            }

            return View();
        }

        static List<Category> getCategoryList(string result)
        {
            List<Category> categoryList = JsonConvert.DeserializeObject<List<Category>>(result);
            return categoryList;
        }

        static List<LanguagePair> getLanguagePairList(string result)
        {
            List<LanguagePair> LanguagePairList = JsonConvert.DeserializeObject<List<LanguagePair>>(result);
            return LanguagePairList;
        }
    }
}