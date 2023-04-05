
Overview
=============
This sample code provides guidance on how to use the Custom Translator API (preview) using ASP.NET MVC application. Refer to [Custom Translator documentation](https://docs.microsoft.com/en-gb/azure/cognitive-services/translator/custom-translator/overview) to know more about different features of Custom Translator. 

Custom Translator API (preview) documentation can be found [here](https://custom-api.cognitive.microsofttranslator.com/swagger/).

For support, please select the 'Issues' tab at the top of the page and submit your request.

Prerequisites
=============

Follow the instructions [here](https://learn.microsoft.com/en-gb/azure/cognitive-services/Translator/create-translator-resource) to create a translator resource. 


Setup MVC App Code
==================

Run Visual Studio, open CustomTranslatorSampleCode.sln and expand CustomTranslatorSampleCode.

In **CustomTranslatorAPIClient.cs** update the following code:

1. **subscription_key**: update this value with one of the subscription keys of your translator resource, you can fetch it from the "Keys and Endpoint" tab in the translator resource on azure portal
2. **resource_name**: update this value with your Translator resource's name.

![Resource details](media/update_resource_details.png)

In Controllers folder open WorkspaceController.cs, and update following code:

1. **translatorResource_Location**: Update this value with the location for your translator resource, can be found on overview page of the resource on the azure portal. eg: 'West US 2'
2. **translatorResource_SubscriptionKey**: update this value with one of the subscription keys of your translator resource, you can fetch it from the "Keys and Endpoint" tab in the translator resource on azure portal
3. **newWorkspace.Name**: update this value with desired workspace name.

![Workspace details](media/update_workspace_details.png)

To create the workspace, start the project in Visual Studio
In the home page, click on "Execute" to create a workspace. This will give you the **WorkspaceId**

![Execute create workspace](media/execute_workspace.png)

Follow the same procedure to execute the changes after each code update.

In Controllers folder open **ProjectController.cs**, go to **Create()** method and update following code:

1. **workspaceId**: update this value with your workspace Id. You can get this by running the "
2. **newproject.name**: update this value desired project name.
3. **newproject.languagePairId**: update this value with appropriate language pair id.
4. **newproject.categoryid**: update this value with appropriate category id.
5. **newproject.categoryDescriptor**: update this value desired project category descriptor.
6. **newproject.label**: update this value desired project label.
7. **newproject.description**: update this value desired project description.

![Project details](media/update_project_details.png)

In Controllers folder open **UploadController.cs**, go to **ParallelFile()** method and update following code:

1. **workspaceId**: update this value with your workspace Id.
2. **sourcelanguagefilepath**: update this value of the local path for source language file.
3. **targetlanguagefilepath**: update this value of the local path for source target file.
4. **documentdetails.DocumentName**: update this value with desired document name.
5. **documentdetails.DocumentType**: update this value desired document type. Values can be of training/ tuning/ testing.
6. **sourcelanguagefile.LanguageCode**: update this value with source language code.
7. **sourcelanguagefile.OverwriteIfExists**: if you want to overwrite with this file, if the same file name exists use **true**, else use **false**.
8. **targetlanguagefile.LanguageCode**: update this value with target language code.
9. **targetlanguagefile.OverwriteIfExists**: if you want to overwrite with this file, if the same file name exists use **true**, else use **false**.

![Parallel document upload details](media/update_project_details.png)

In Controllers folder open **UploadController.cs**, go to **ComboFile()** method and update following code:

1. **workspaceId**: update this value with your workspace Id.
2. **filepath**: update this value of the local path for combo file.
3. **documentdetails.DocumentName**: update this value with desired document name.
4. **documentdetails.DocumentType**: update this value desired document type. Values can be of training/ tuning/ testing.

![Combo document upload details](media/update_combo_document_upload.png)

In Controllers folder open **ModelController.cs**, go to **Create()** method and update following code:

1. **model.name**: update this value desired model name.
2. **model.projectId**: update this value with your project id.
3. **model.documentIds.Add()**: add document id in this list. You can add multiple documents.

![model details](media/update_model_details.png)

Build the code and run it in Visual Studio to verify everything is working.
