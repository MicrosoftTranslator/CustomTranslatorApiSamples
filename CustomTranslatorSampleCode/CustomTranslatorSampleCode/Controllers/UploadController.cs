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
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> ParallelFile()
        {
            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            // Start upload single parallel document 

            string workspaceId = "..."; // Enter Workspace Id

            string sourcelanguagefilepath = @"..."; // Enter local path for source language file
            string targetlanguagefilepath = @"..."; // Enter local path for target language file

            DocumentDetailsForImportRequest documentdetails = new DocumentDetailsForImportRequest();

            documentdetails.DocumentName = "..."; // Enter document name
            documentdetails.DocumentType = "training"; //values = training/ tuning/ testing
            documentdetails.IsParallel = true; // Enter if this is a parallel document. values = true, false
            documentdetails.FileDetails = new List<FileForImportRequest>();


            FileForImportRequest sourcelanguagefile = new FileForImportRequest();
            sourcelanguagefile.Name = Path.GetFileName(sourcelanguagefilepath);
            sourcelanguagefile.LanguageCode = "..."; // Enter source language. Example: de. //Determined from the call to GetLanguages 
            sourcelanguagefile.OverwriteIfExists = true; // Enter if you want to overwrite if file exists. values = true, false

            FileForImportRequest targetlanguagefile = new FileForImportRequest();
            targetlanguagefile.Name = Path.GetFileName(targetlanguagefilepath);
            targetlanguagefile.LanguageCode = "..."; // Enter target language. Example: en. //Determined from the call to GetLanguages 
            targetlanguagefile.OverwriteIfExists = true; // Enter if you want to overwrite if file exists. values = true, false

            string result = await clientapp.ImportDocument(workspaceId, sourcelanguagefilepath, targetlanguagefilepath, documentdetails, sourcelanguagefile, targetlanguagefile);

            string jobId = getJobId(result.Trim());

            Response.Write("<div class=\"jumbotron\">");
            Response.Write("<br /><br />JobId: " + jobId + "<br />");

            result = await clientapp.GetDocumentUploadStatus(jobId);

            CurrentFileUploadStatus status = getUploadStatus(result);

            foreach (FileProcessingStatus fps in status.fileProcessingStatus)
            {
                Response.Write("<br />File Id: " + fps.id); // Use this File Is when training a model
                Response.Write("<br />File Name: " + fps.fileName);
                Response.Write("<br />Upload Status: " + fps.status.displayName);
                Response.Write("<br />");
            }

            Response.Write("</div>");
            return View();
        }

        public async Task<ActionResult> ComboFile()
        {
            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();
            
            string workspaceId = "..."; // Enter Workspace Id

            string filepath = @"..."; // Enter local path for combo file

            DocumentDetailsForImportRequest documentdetails = new DocumentDetailsForImportRequest();

            documentdetails.DocumentName = "..."; // Enter document name
            documentdetails.DocumentType = "training"; //values = training/ tuning/ testing
            documentdetails.IsParallel = true; // Enter if this is a parallel document. values = true, false
            documentdetails.FileDetails = new List<FileForImportRequest>();

            string result = await clientapp.ImportComboDocument(workspaceId, filepath, documentdetails);
            string jobId = getJobId(result.Trim());
            Response.Write("<div class=\"jumbotron\">");
            Response.Write("<br /><br />JobId: " + jobId + "<br />");

            result = await clientapp.GetDocumentUploadStatus(jobId);

            CurrentFileUploadStatus status = getUploadStatus(result);

            foreach (FileProcessingStatus fps in status.fileProcessingStatus)
            {
                Response.Write("<br />File Id: " + fps.id); // Use this File Is when training a model
                Response.Write("<br />File Name: " + fps.fileName);
                Response.Write("<br />Upload Status: " + fps.status.displayName);
            }

            Response.Write("</div>");
            return View();
        }

        string getJobId(string result)
        {
            FileUploadJob jobresult = JsonConvert.DeserializeObject<FileUploadJob>(result);
            return jobresult.jobId;
        }

        CurrentFileUploadStatus getUploadStatus(string result)
        {
            CurrentFileUploadStatus status = JsonConvert.DeserializeObject<CurrentFileUploadStatus>(result);
            return status;
        }
    }
}