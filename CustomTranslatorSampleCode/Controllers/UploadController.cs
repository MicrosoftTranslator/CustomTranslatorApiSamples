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
            string token_header = null;

            if (Session["token_header"] != null)
            {
                token_header = Session["token_header"].ToString();
            }

            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            // Start upload single parallel document 

            string sourcelanguagefilepath = @"..."; // Enter local path for source language file
            string targetlanguagefilepath = @"..."; // Enter local path for target language file


            DocumentDetailsForImportRequest documentdetails = new DocumentDetailsForImportRequest();

            documentdetails.DocumentName = "..."; // Enter document name
            documentdetails.DocumentType = "training"; //values = training, tuning, testing
            documentdetails.IsParallel = true; // Enter if this is a parallel document. values = true, false
            documentdetails.FileDetails = new List<FileForImportRequest>();


            FileForImportRequest sourcelanguagefile = new FileForImportRequest();
            sourcelanguagefile.Name = Path.GetFileName(sourcelanguagefilepath);
            sourcelanguagefile.Language = "..."; // Enter source language. Example: de
            sourcelanguagefile.Type = "..."; // Enter source language file type. Example: TXT
            sourcelanguagefile.OverwriteIfExists = true; // Enter if you want to overwrite if file exists. values = true, false

            FileForImportRequest targetlanguagefile = new FileForImportRequest();
            targetlanguagefile.Name = Path.GetFileName(targetlanguagefilepath);
            targetlanguagefile.Language = "..."; // Enter target language. Example: en
            targetlanguagefile.Type = "..."; // Enter target language file type. Example: TXT
            targetlanguagefile.OverwriteIfExists = true; // Enter if you want to overwrite if file exists. values = true, false

            string result = await clientapp.ImportDocument(token_header, sourcelanguagefilepath, targetlanguagefilepath, documentdetails, sourcelanguagefile, targetlanguagefile);


            string jobId = getJobId(result.Trim());

            Response.Write("<br /><br />JobId: " + jobId + "<br />");

            result = await clientapp.GetDocumentUploadStatus(jobId, token_header);

            CurrentFileUploadStatus status = getUploadStatus(result);

            foreach (FileProcessingStatus fps in status.fileProcessingStatus)
            {
                Response.Write("<br />File Name: " + fps.id); // Use this File Is when training a model
                Response.Write("<br />File Name: " + fps.fileName);
                Response.Write("<br />Upload Status: " + fps.status.displayName);
            }

            return View();
        }

        public async Task<ActionResult> ComboFile()
        {
            string token_header = null;

            if (Session["token_header"] != null)
            {
                token_header = Session["token_header"].ToString();
            }

            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            string filepath = @"..."; // Example: Local path for a Zip File

            DocumentDetailsForImportRequest documentdetails = new DocumentDetailsForImportRequest();

            documentdetails.DocumentName = "..."; // Enter document name
            documentdetails.DocumentType = "training"; //values = training, tuning, testing
            documentdetails.IsParallel = true; // Enter if this is a parallel document. values = true, false
            documentdetails.FileDetails = new List<FileForImportRequest>();

            string result = await clientapp.ImportComboDocument(token_header, filepath, documentdetails);
            string jobId = getJobId(result.Trim());
            Response.Write("<br /><br />JobId: " + jobId + "<br />");

            result = await clientapp.GetDocumentUploadStatus(jobId, token_header);

            CurrentFileUploadStatus status = getUploadStatus(result);

            foreach (FileProcessingStatus fps in status.fileProcessingStatus)
            {
                Response.Write("<br />File Name: " + fps.id); // Use this File Is when training a model
                Response.Write("<br />File Name: " + fps.fileName);
                Response.Write("<br />Upload Status: " + fps.status.displayName);
            }

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