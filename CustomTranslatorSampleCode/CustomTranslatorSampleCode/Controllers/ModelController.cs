﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomTranslatorSampleCode.Controllers
{
    public class ModelController : Controller
    {
        // GET: Model
        public async Task<ActionResult> Index(int? id)
        {
            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            if (id == null)
            {
                return View();
            }

            long modelid = (long) id;
            string result = await clientapp.GetModel(modelid);

            TrainingModel m = getTrainingDetail(result);

            Response.Write("<div class=\"jumbotron\">");
            Response.Write("<br />Model Id: " + m.id);
            Response.Write("<br />Model Name: " + m.name);
            Response.Write("<br />Model Status: " + m.modelStatus);
            Response.Write("<br/>");

            foreach (Document d in m.documents)
            {
                Response.Write("<br/>Document Name: " + d.documentInfo.name);
                Response.Write("<br/>Aligned Sentence Count: " + d.processedDocumentInfo?.alignedSentenceCount);
                Response.Write("<br/>Used Sentence Count: " + d.processedDocumentInfo?.usedSentenceCount);


                int i = 1;
                foreach (FileInDocument f in d.documentInfo.files)
                {
                    Response.Write("<br/>File " + i + " : " + f.fileName);
                    Response.Write("<br/>Language: " + f.language.displayName);
                    i++;
                }
            }
            Response.Write("<br/>");
            Response.Write("<br />Bleu Score: " + m.bleuScoreCIPunctuated);
            Response.Write("<br />Training Sentence Count: " + m.trainingSentenceCount);
            Response.Write("<br />Tuning Sentence Count: " + m.tuningSentenceCount);
            Response.Write("<br />Testing Sentence Count: " + m.testingSentenceCount);
            Response.Write("</div>");

            return View();
        }


        public async Task<ActionResult> Create()
        {
            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();

            ModelCreateRequest model = new ModelCreateRequest(); // Create new object for Model and add values
            model.name = "..."; // Enter model name
            model.projectId = "..."; // Enter project id
            model.documentIds = new List<int>();
            model.documentIds.Add(-1); // Add multiple documents using DocumentID. DocumentID is int.
            model.isTuningAuto = true; // Enter if tuning set will be set to auto. values = true, false
            model.isTestingAuto = true; // Enter if testing set will be set to auto. values = true, false
            model.isAutoDeploy = false; // Enter if this model will be automatically deployed. values = true, false
            model.isAutoTrain = true; // Enter if this model will be automatically trained. values = true, false. If False, will be saved as draft

            string result = await clientapp.GetProject(model.projectId);
            Project project = getProjectDetail(result);

            result = await clientapp.CreateModel(model);
            string[] resultarray = result.Split('/');

            if (resultarray.Length > 1)
            {
                string newtrainingid = resultarray[resultarray.Length - 1];

                Response.Write("<div class=\"jumbotron\">");
                Response.Write("<br/>New Model Created");
                Response.Write("<br/>Model Id: " + newtrainingid);
                Response.Write("<br/>Model Name: " + model.name);
                Response.Write("<br/>Project Name: " + project.name);
                Response.Write("<br/>Language Pair: " + project.languagePair.sourceLanguage.displayName + " to " + project.languagePair.targetLanguage.displayName);
                foreach (int fileid in model.documentIds)
                {
                    result = await clientapp.GetDocument(fileid);
                    DocumentInfo doc = getDocumentDetail(result);
                    Response.Write("<br/>Document : " + doc.name);
                    int i = 1;
                    foreach (FileInDocument f in doc.files)
                    {
                        Response.Write("<br/>File " + i + " :");
                        Response.Write("<br/>File Name: " + f.fileName);
                        Response.Write("<br/>Language: " + f.language.displayName);
                        i++;
                    }
                }
                Response.Write("</div>");
            }
            else
            {
                Response.Write("<br/>Could not create project: " + result);
            }

            return View();
        }

        public async Task<ActionResult> Deploy(int? id)
        {
            if (id == null)
            {
                return View();
            }

            long modelid = (long)id;
            CustomTranslatorAPIClient clientapp = new CustomTranslatorAPIClient();


            DeploymentConfiguration regionaldeployment1 = new DeploymentConfiguration();
            regionaldeployment1.region = 1; // North America = 1
            regionaldeployment1.isDeployed = true; // true = deployment ; false = undeployment ; You can get these regions from the Get Regions call

            DeploymentConfiguration regionaldeployment2 = new DeploymentConfiguration();
            regionaldeployment2.region = 2; // Europe = 2
            regionaldeployment2.isDeployed = true; // true = deployment ; false = undeployment

            DeploymentConfiguration regionaldeployment3 = new DeploymentConfiguration();
            regionaldeployment3.region = 3; // Asia Pacific 
            regionaldeployment3.isDeployed = true; // true = deployment ; false = undeployment

            List<DeploymentConfiguration> deploymentconfig = new List<DeploymentConfiguration>();
            deploymentconfig.Add(regionaldeployment1);
            deploymentconfig.Add(regionaldeployment2);
            deploymentconfig.Add(regionaldeployment3);

            string result = await clientapp.CreateModelDeploymentRequest(modelid, deploymentconfig);
            Response.Write(result);
            return View();
        }

        Project getProjectDetail(string result)
        {
            Project project = JsonConvert.DeserializeObject<Project>(result);
            return project;
        }

        DocumentInfo getDocumentDetail(string result)
        {
            DocumentInfo doc = JsonConvert.DeserializeObject<DocumentInfo>(result);
            return doc;
        }

        TrainingModel getTrainingDetail(string result)
        {
            TrainingModel model = JsonConvert.DeserializeObject<TrainingModel>(result);
            return model;
        }
    }
}