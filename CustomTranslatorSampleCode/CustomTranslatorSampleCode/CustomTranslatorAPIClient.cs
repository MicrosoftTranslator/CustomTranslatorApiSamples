using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomTranslatorSampleCode
{
    public class CustomTranslatorAPIClient
    {
        static string subscription_key = "..."; // Enter your subscription_key, you can fetch it from the "Keys and Endpoint" tab in the translator resource 
        static string resource_name = "..."; // Enter your translator resource name
        static string host = "https://" + resource_name + ".cognitiveservices.azure.com/translator/customtranslator"; 

        /// <summary>
        /// Gets the list of workspaces.
        /// </summary>
        public async Task<string> GetWorkspaces()
        {
            string apipath = "/api/texttranslator/v1.0/workspaces";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of categories that can be assigned to the project.
        /// </summary>
        public async Task<string> GetCategories()
        {
            string apipath = "/api/texttranslator/v1.0/categories";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of subscription billing regions.
        /// </summary>
        public async Task<string> GetBillingRegions()
        {
            string apipath = "/api/texttranslator/v1.0/subscriptions/billingregions";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of languages supported by Text Translator
        /// </summary>
        public async Task<string> GetLanguages()
        {
            string apipath = "/api/texttranslator/v1.0/languages";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of language pairs are supported by Text Translator
        /// </summary>
        public async Task<string> GetLanguagePairs()
        {
            string apipath = "/api/texttranslator/v1.0/languages/supportedlanguagepairs";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }


        /// <summary>
        /// Get the model details for specific model id
        /// </summary>
        /// <param name="id">The Id of the requested model.</param>
        public async Task<string> GetModel(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of tests for a specific model. Tests can be filtered using standard OData $filter syntax. 
        /// TestName - test name to filter by. Supported operations are 'eq' and 'substringof'.
        /// Status - Supported operations are 'eq'. 
        /// Only basic 'and' operator is supported between different field filters.Also no nested conditions are supported.
        /// Example: /api/texttranslator/v1/models/{id}/tests?$filter=substringof(testName, 'Test 1') and status eq 'Complete'
        /// </summary>
        /// <param name="id">The Id of the model to which tests belong.</param>
        /// <param name="pageindex">The page index.</param>
        /// <param name="filters">OData $filter parameter.</param>
        public async Task<string> GetModelTests(long id, int pageindex, string filters = "")
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}/tests?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of projects. 
        /// Projects can be filtered using standard OData $filter syntax. Supported fields and operations:
        /// Name- project name to filter by.Supported operations are 'eq' and 'substringof'.
        /// SourceLanguage- Supported operations are 'eq'.
        /// TargetLanguage- Supported operations are 'eq'.
        /// Category- Supported operations are 'eq'.
        /// Status- Supported operations are 'eq'. 
        /// Only basic 'and' operator is supported between different field filters.Also no nested conditions are supported.
        /// Example: /api/texttranslator/v1/projects?$filter= substringof(name, 'Project 1') and status eq 'Deployed' and targetLanguage eq 'de' 
        /// To sort the returned results please use the standard OData $orderby syntax. Supported fields are:
        /// Name- project name to order by.
        /// Status- project status to order by.
        /// Example: /api/texttranslator/v1/projects?$orderby= name desc, status asc
        /// </summary>
        /// <param name="workspaceid">The Id of the workspace.</param>
        /// <param name="pageindex">The page index.</param>
        /// <param name="filters">OData $filter parameter</param>
        /// <param name="orderby"></param>
        public async Task<string> GetProjects(string workspaceid, int pageindex, string filters = "", string orderby = "")
        {
            string apipath = $"/api/texttranslator/v1.0/projects?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}&workspaceId={Uri.EscapeUriString(workspaceid)}&$orderby={Uri.EscapeUriString(orderby)}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the project specified by Id.
        /// </summary>
        /// <param name="id">The Id for the project for which details are requested.</param>
        public async Task<string> GetProject(string id)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets details of a specific test.
        /// </summary>
        /// <param name="id">The Id for the test for which details are requested.</param>
        public async Task<string> GetTest(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets aligned source, ref, and MT sentences for a specific test. 
        /// </summary>
        /// <param name="id">The Id for the test for which details are requested.</param>
        public async Task<string> GetTestResults(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}/results";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the documents. Documents can be filtered using standard OData $filter syntax. 
        /// 
        /// Supported fields and operations:
        /// Name - document name to filter by.Supported operations are 'eq' and 'substringof'.
        /// DocumentType - Document type filter.Supported operations are 'eq'.
        /// LanguageCode - Language code filter.Supported operations are 'eq'.
        /// IsParallel - Filter by the IsParallel boolean flag.Supported operations are 'eq'.
        /// ProjectLanguages - Filter for documents applicable to a project with the specified language pair. Language pair should be specified with a '->' between the languages.Supported operations are 'eq'. 
        /// Only basic 'and' operator is supported between different field filters.Also no nested conditions are supported. 
        /// Example: /api/texttranslator/v1/documents?$filter= substringof(Name, 'Document 1') and documentType eq 'training' and languageCode eq 'de' and isParallel eq false 
        /// Example with ProjectLanguages - /api/texttranslator/v1/documents?$filter= projectLanguage eq 'en->de' 
        /// 
        /// To sort the returned results please use the standard OData $orderby syntax. 
        /// Supported fields are:Name- document name to order by.
        /// Example: /api/texttranslator/v1/documents?$orderby= name desc
        /// </summary>
        /// 
        /// <param name="pageindex">Index of the page.</param>
        /// <param name="workspaceid">The Id of the workspace.</param>
        /// <param name="filters">Optional parameter to pass standard OData $filter syntax.</param>
        /// <param name="orderby">To sort the returned results please use the standard OData $orderby syntax.</param>
        public async Task<string> GetDocuments(int pageindex, string workspaceid, string filters = "", string orderby = "")
        {
            string apipath = $"/api/texttranslator/v1.0/documents?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}&workspaceId={Uri.EscapeUriString(workspaceid)}&$orderby={Uri.EscapeUriString(orderby)}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the requested document.
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested</param>
        public async Task<string> GetDocument(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the files for the document
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested</param>
        public async Task<string> GetFiles(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/files";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the content of the requested file
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested.</param>
        /// <param name="language">The language of the file requested</param>
        /// <param name="pageindex">Index of the page.</param>
        public async Task<string> GetFileContent(long id, string language, int pageindex)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/files/{language}/contents?pageIndex={pageindex}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Downloads a zip containing the file(s) belonging to this document.
        /// </summary>
        /// <param name="id">The Id of the document for which files are requested</param>
        public async Task<string> ExportDocument(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/export";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri, true);
            return response;
        }

        /// <summary>
        /// Gets the status of the document import.
        /// </summary>
        /// <param name="jobid">
        /// The job Id of the document upload process for which details are requested. 
        /// This job Id is can be fetched from the return value of a /api/texttranslator/v1.0/documents/import API call. 
        /// </param>
        public async Task<string> GetDocumentUploadStatus(string jobid)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/import/jobs/{jobid}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.GetRestRequest(uri);
            return response;
        }


        /// <summary>
        /// Create a project.
        /// </summary>
        /// <param name="newproject">Object containing project details</param>  
        /// <param name="workspaceid">The Id of the workspace.</param>
        public async Task<string> CreateProject(ProjectCreateRequest newproject, string workspaceid)
        {
            string apipath = $"/api/texttranslator/v1.0/projects?workspaceId={Uri.EscapeUriString(workspaceid)}";
            string uri = host + apipath;
            string response = "";
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            try
            {
                string stringdata = JsonConvert.SerializeObject(newproject, Formatting.Indented); //Serialize the object and convert to required JSON
                StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PostRestRequest(uri, content);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Create a workspace.
        /// </summary>
        /// <param name="newworkspace">Object containing workspace details</param>  
        public async Task<string> CreateWorkspace(WorkspaceCreateRequest newworkspace)
        {
            string apipath = $"/api/texttranslator/v1.0/workspaces";
            string uri = host + apipath;
            string response = "";
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            try
            {
                string stringdata = JsonConvert.SerializeObject(newworkspace, Formatting.Indented); //Serialize the object and convert to required JSON
                StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PostRestRequest(uri, content);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Update a specific project.
        /// </summary>
        /// <param name="id">The id of the project to update.</param>
        public async Task<string> EditProject(string id)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;

            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            // Edit the project configuration 
            ProjectEditRequest newproject = new ProjectEditRequest();
            newproject.name = "..."; // Enter Project Name
            newproject.categoryDescriptor = "..."; // Enter Project Category Descriptor
            newproject.description = "..."; // Enter Project Decription

            string stringdata = JsonConvert.SerializeObject(newproject, Formatting.Indented); //Serialize the object and convert to required JSON
            StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
            string response = await request.PutRestRequest(uri, content);
            return response;
        }

        /// <summary>
        /// Update a specific project.
        /// </summary>
        /// <param name="id">The id of the project to delete.</param>
        public async Task<string> DeleteProject(string id)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);
            string response = await request.DeleteRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Upload parallel files for processing. Requires both source language file and destination language file to upload. 
        /// .XLSX|.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN
        /// Documents are created asynchronously. Upload status can be checked using the returned job id.
        /// </summary>
        /// <param name="workspaceid">The Id of the workspace.</param>
        /// <param name="sourcelanguagefilepath">Local path for source language file</param>
        /// <param name="targetlanguagefilepath">Local path for target language file</param>
        /// <param name="documentdetails">Object containing details of the document</param>
        /// <param name="sourcelanguagefile">Object containing source language file details</param>
        /// <param name="targetlanguagefile">Object containing target language file details</param>
        /// <returns></returns>
        public async Task<string> ImportDocument(string workspaceid, string sourcelanguagefilepath, string targetlanguagefilepath, DocumentDetailsForImportRequest documentdetails, FileForImportRequest sourcelanguagefile, FileForImportRequest targetlanguagefile)
        {
            string result = "";
            string apipath = $"/api/texttranslator/v1.0/documents/import?workspaceId={Uri.EscapeUriString(workspaceid)}";

            documentdetails.FileDetails.Add(sourcelanguagefile);
            documentdetails.FileDetails.Add(targetlanguagefile);

            var details = new List<DocumentDetailsForImportRequest>() { documentdetails };
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscription_key);
            string uri = host + apipath;

            HttpResponseMessage response = new HttpResponseMessage();
            MultipartFormDataContent formData = new MultipartFormDataContent();

            FileStream sourcelanguagestream = File.OpenRead(sourcelanguagefilepath);
            HttpContent sourcefileStreamContent = new StreamContent(sourcelanguagestream);
            formData.Add(sourcefileStreamContent, "Files", sourcelanguagefile.Name);

            FileStream targetlanguagestream = File.OpenRead(targetlanguagefilepath);
            HttpContent targetfileStreamContent = new StreamContent(targetlanguagestream);
            formData.Add(targetfileStreamContent, "Files", targetlanguagefile.Name);

            string stringdata = JsonConvert.SerializeObject(details, Formatting.Indented);
            StringContent stringcontent = new StringContent(stringdata);
            formData.Add(stringcontent, "DocumentDetails");

            try
            {
                response = await client.PostAsync(uri, formData);
                result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                result = e.Message + ". " + e.InnerException;
            }
            return result;
        }

        /// <summary>
        /// Upload files for processing. Requires single combo file to upload. 
        /// .TMX|.XLF|.XLIFF|.LCL|.XLSX|.ZIP
        /// Documents are created asynchronously. Upload status can be checked using the returned job id.
        /// </summary>
        /// <param name="workspaceid">The Id of the workspace.</param>
        /// <param name="filepath">Local path of the combo file</param>
        /// <param name="documentdetails">Object containing details of the document</param>
        /// <returns></returns>
        public async Task<string> ImportComboDocument(string workspaceid, string filepath, DocumentDetailsForImportRequest documentdetails)
        {
            string result = "";
            string apipath = $"/api/texttranslator/v1.0/documents/import?workspaceId={Uri.EscapeUriString(workspaceid)}";

            FileForImportRequest combofile = new FileForImportRequest();
            combofile.Name = Path.GetFileName(filepath);
            combofile.OverwriteIfExists = true;

            documentdetails.FileDetails.Add(combofile);


            var details = new List<DocumentDetailsForImportRequest>() { documentdetails };
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscription_key);
            string uri = host + apipath;

            HttpResponseMessage response = new HttpResponseMessage();
            MultipartFormDataContent formData = new MultipartFormDataContent();

            FileStream targetlanguagestream = File.OpenRead(filepath);
            HttpContent targetfileStreamContent = new StreamContent(targetlanguagestream);
            formData.Add(targetfileStreamContent, "Files", combofile.Name);

            string stringdata = JsonConvert.SerializeObject(details, Formatting.Indented);
            StringContent stringcontent = new StringContent(stringdata);
            formData.Add(stringcontent, "DocumentDetails");

            try
            {
                response = await client.PostAsync(uri, formData);
                result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                result = e.Message + ". " + e.InnerException;
            }
            return result;
        }

        /// <summary>
        /// Creates a new model.
        /// </summary>
        public async Task<string> CreateModel(ModelCreateRequest model)
        {
            string apipath = $"/api/texttranslator/v1.0/models";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            string response = "";
            try
            {
                string stringdata = JsonConvert.SerializeObject(model, Formatting.Indented);
                StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PostRestRequest(uri, content);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Updates a specific model.
        /// </summary>
        /// <param name="id">The id of the model to update.</param>
        public async Task<string> EditModel(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}";
            string uri = host + apipath;

            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            ModelEditRequest model = new ModelEditRequest();
            model.name = "..."; // Enter model name

            string response = "";
            try
            {
                string stringdata = JsonConvert.SerializeObject(model, Formatting.Indented);
                StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PutRestRequest(uri, content);
                Console.WriteLine(response);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Deploy or undeploy a model. 
        /// </summary>
        /// <param name="id">The Id of the model to deploy or undeploy.</param>
        /// <param name="deploymentconfig">List of deployment configuration for different region.
        public async Task<string> CreateModelDeploymentRequest(long id, List<DeploymentConfiguration> deploymentconfig)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}/deployment";

            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Ocp-Apim-Subscription-Key", subscription_key);

            StringContent content = null;
            string response = "";

            try
            {
                string stringdata = JsonConvert.SerializeObject(deploymentconfig, Formatting.Indented);
                content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PostRestRequest(uri, content);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Export the test resuls as a zip file.
        /// </summary>
        /// <param name="id">The Id of the test.</param>
        public async Task<string> ExportTest(long id)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}/export";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscription_key);
            string uri = host + apipath;

            string result = "";
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, null);
                response.EnsureSuccessStatusCode();
                RestRequest r = new RestRequest();
                Helper helper = new Helper();
                string downloadpath = await helper.DownlaodFile(response);
                result = "File downloaded at " + downloadpath;
            }
            catch (Exception e)
            {
                result = e.Message + ". " + e.InnerException;
            }
            return result;
        }
    }
}