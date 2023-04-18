using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace CustomTranslatorSampleCode.Controllers
{

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
