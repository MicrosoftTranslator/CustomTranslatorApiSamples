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
        static string host = "https://custom-api.cognitive.microsofttranslator.com"; // prod



        /// <summary>
        /// Gets the list of categories that can be assigned to the project.
        /// </summary>
        /// <param name="authtoken">Access Token</param>
        public async Task<string> GetCategories(string authtoken)
        {
            string apipath = "/api/texttranslator/v1.0/categories";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of languages supported by Text Translator
        /// </summary>
        /// <param name="authtoken">Access Token</param>
        public async Task<string> GetLanguages(string authtoken)
        {
            string apipath = "/api/texttranslator/v1.0/languages";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the list of language pairs are supported by Text Translator
        /// </summary>
        /// <param name="authtoken">Access Token</param>
        public async Task<string> GetLanguagePairs(string authtoken)
        {
            string apipath = "/api/texttranslator/v1.0/languages/supportedlanguagepairs";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }


        /// <summary>
        /// Get the model details for specific model id
        /// </summary>
        /// <param name="id">The Id of the requested model.</param>
        /// <param name="authtoken">Access Token.</param>
        public async Task<string> GetModel(long id, string authtoken) // need testing
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
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
        /// <param name="authtoken">Access Token.</param>
        /// <param name="pageindex">The page index.</param>
        /// <param name="filters">OData $filter parameter.</param>
        public async Task<string> GetModelTests(long id, string authtoken, int pageindex, string filters = "")
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}/tests?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
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
        /// <param name="authtoken">Access token.</param>
        /// <param name="pageindex">The page index.</param>
        /// <param name="filters">OData $filter parameter</param>
        /// <param name="orderby"></param>
        public async Task<string> GetProjects(string authtoken, int pageindex, string filters = "", string orderby = "")
        {
            string apipath = $"/api/texttranslator/v1.0/projects?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}&$orderby={Uri.EscapeUriString(orderby)}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the project specified by Id.
        /// </summary>
        /// <param name="id">The Id for the project for which details are requested.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> GetProject(string id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets details of a specific test.
        /// </summary>
        /// <param name="id">The Id for the test for which details are requested.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> GetTest(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets aligned source, ref, and MT sentences for a specific test. 
        /// </summary>
        /// <param name="id">The Id for the test for which details are requested.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> GetTestResults(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}/results";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
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
        /// <param name="authtoken">Access token</param>
        /// <param name="pageindex">Index of the page.</param>
        /// <param name="filters">Optional parameter to pass standard OData $filter syntax.</param>
        /// <param name="orderby">To sort the returned results please use the standard OData $orderby syntax.</param>
        public async Task<string> GetDocuments(string authtoken, int pageindex, string filters = "", string orderby = "")
        {
            string apipath = $"/api/texttranslator/v1.0/documents?filter={Uri.EscapeUriString(filters)}&pageIndex={pageindex}&$orderby={Uri.EscapeUriString(orderby)}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the requested document.
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested</param>
        /// <param name="authtoken">Access token</param>
        public async Task<string> GetDocument(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the files for the document
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested</param>
        /// <param name="authtoken">Access token</param>
        public async Task<string> GetFiles(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/files";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Gets the content of the requested file
        /// </summary>
        /// <param name="id">The Id of the document for which details are requested.</param>
        /// <param name="authtoken">Access token</param>
        /// <param name="language">The language of the file requested</param>
        /// <param name="pageindex">Index of the page.</param>
        public async Task<string> GetFileContent(long id, string authtoken, string language, int pageindex)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/files/{language}/contents?pageIndex={pageindex}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Downloads a zip containing the file(s) belonging to this document.
        /// </summary>
        /// <param name="id">The Id of the document for which files are requested</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> ExportDocument(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/{id}/export";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
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
        /// <param name="authtoken">Access token.</param>
        public async Task<string> GetDocumentUploadStatus(string jobid, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/documents/import/jobs/{jobid}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.GetRestRequest(uri);
            return response;
        }


        /// <summary>
        /// Create a project.
        /// </summary>
        /// <param name="authtoken">Access token.</param>
        /// <param name="newproject">Object containing project details</param>
        /// <returns></returns>
        public async Task<string> CreateProject(string authtoken, ProjectCreateRequest newproject)
        {
            string apipath = $"/api/texttranslator/v1.0/projects";
            string uri = host + apipath;
            string response = "";
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);

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
        /// Update a specific project.
        /// </summary>
        /// <param name="id">The id of the project to update.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> EditProject(string id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;

            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);

            // Edit the project configuration 
            ProjectEditRequest newproject = new ProjectEditRequest();
            newproject.name = "Edit Test July 25"; // Enter Project Name
            newproject.categoryDescriptor = "Test 333"; // Enter Project Category Descriptor
            newproject.description = "Project Edit July 25"; // Enter Project Decription

            string stringdata = JsonConvert.SerializeObject(newproject, Formatting.Indented); //Serialize the object and convert to required JSON
            StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
            string response = await request.PutRestRequest(uri, content);
            return response;
        }

        /// <summary>
        /// Update a specific project.
        /// </summary>
        /// <param name="id">The id of the project to delete.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> DeleteProject(string id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/projects/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.DeleteRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Upload parallel files for processing. Requires both source language file and destination language file to upload. 
        /// .XLSX|.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN
        /// Documents are created asynchronously. Upload status can be checked using the returned job id.
        /// </summary>
        /// <param name="authtoken">Access token.</param>
        /// <param name="sourcelanguagefilepath">Local path for source language file</param>
        /// <param name="targetlanguagefilepath">Local path for target language file</param>
        /// <param name="documentdetails">Object containing details of the document</param>
        /// <param name="sourcelanguagefile">Object containing source language file details</param>
        /// <param name="targetlanguagefile">Object containing target language file details</param>
        /// <returns></returns>
        public async Task<string> ImportDocument(string authtoken, string sourcelanguagefilepath, string targetlanguagefilepath, DocumentDetailsForImportRequest documentdetails, FileForImportRequest sourcelanguagefile,FileForImportRequest targetlanguagefile)
        {
            string result = "";
            string apipath = $"/api/texttranslator/v1/documents/import";

            documentdetails.FileDetails.Add(sourcelanguagefile);
            documentdetails.FileDetails.Add(targetlanguagefile);

            var details = new List<DocumentDetailsForImportRequest>() { documentdetails };
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", authtoken);
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
        /// <param name="authtoken">Access token.</param>
        /// <param name="filepath">Local path of the combo file</param>
        /// <param name="documentdetails">Object containing details of the document</param>
        /// <returns></returns>
        public async Task<string> ImportComboDocument(string authtoken, string filepath, DocumentDetailsForImportRequest documentdetails)
        {
            string result = "";
            string apipath = $"/api/texttranslator/v1/documents/import";

            FileForImportRequest combofile = new FileForImportRequest();
            combofile.Name = Path.GetFileName(filepath);
            combofile.OverwriteIfExists = true;

            documentdetails.FileDetails.Add(combofile);


            var details = new List<DocumentDetailsForImportRequest>() { documentdetails };
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", authtoken);
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
        /// <param name="authtoken">Access token.</param>
        public async Task<string> CreateModel(string authtoken, ModelCreateRequest model)
        {
            string apipath = $"/api/texttranslator/v1.0/models";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);

            //ModelCreateRequest model = new ModelCreateRequest(); // Create new object for Model and add values
            //model.name = "test Model May 4 prod"; // Enter model name
            //model.projectId = "eb12ef3b-a122-4d73-8741-802426a0ab7c"; // Enter project id
            //model.documentIds = new List<int>();
            //model.documentIds.Add(215); // Add multiple documents using DocumentID. DocumentID is int.
            //model.isTuningAuto = true; // Enter if tuning set will be set to auto. values = true, false
            //model.isTestingAuto = true; // Enter if testing set will be set to auto. values = true, false
            //model.isAutoDeploy = false; // Enter if this model will be automatically deployed. values = true, false
            //model.autoDeployThreshold = 0; // Enter the value of auto deploy threshold value

            string response = "";
            try
            {
                string stringdata = JsonConvert.SerializeObject(model, Formatting.Indented);
                StringContent content = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json-patch+json");
                response = await request.PostRestRequest(uri, content);
                Console.WriteLine(response);
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
        /// <param name="authtoken">Access token.</param>
        public async Task<string> EditModel(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}";
            string uri = host + apipath;

            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);

            ModelEditRequest model = new ModelEditRequest();
            model.name = "Edit Model May 10"; // Enter model name

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
        /// <param name="action">The deployment action. Value = deploy/ undeploy</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> CreateModelDeploymentRequest(long id, string action, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}/deployment?action={action}";

            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);

            StringContent content = null;
            string response = "";
            try
            {
                response = await request.PostRestRequest(uri, content);
            }
            catch (Exception e)
            {
                response = e.Message + ". " + e.InnerException;
            }
            return response;
        }

        /// <summary>
        /// Deletes a specific model
        /// </summary>
        /// <param name="id">The Id of the model to delete.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> DeleteModel(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/models/{id}";
            string uri = host + apipath;
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", authtoken);
            string response = await request.DeleteRestRequest(uri);
            return response;
        }

        /// <summary>
        /// Export the test resuls as a zip file.
        /// </summary>
        /// <param name="id">The Id of the test.</param>
        /// <param name="authtoken">Access token.</param>
        public async Task<string> ExportTest(long id, string authtoken)
        {
            string apipath = $"/api/texttranslator/v1.0/tests/{id}/export";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", authtoken);
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