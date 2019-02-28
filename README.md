
Overview
=============
This sample code provides guidance on how to use the Custom Translator API (preview) using ASP.NET MVC application. Refer to [Custom Translator documentation](https://docs.microsoft.com/en-gb/azure/cognitive-services/translator/custom-translator/overview) to know more about different features of Custom Translator. 

Custom Translator API (preview) documentation can be found [here](https://custom-api.cognitive.microsofttranslator.com/swagger/).

Please contact <custommt@microsoft.com> for questions and support.

Prerequisites
=============

Create and Register your Azure AD Client App
--------------------------------------------

This sample code uses OAuth and OpenID for authentication. Please sign in to
<https://apps.dev.microsoft.com> with the same user credentials you use to login
to Custom Translator. The landing page shows the list of client apps that are
created/ available to use with your account.

Create an app using **Add an app** button at top right-hand corner of the landing
page.

![My applications](media/a7247b6a16b3f4151e06661514e03c17.png)

Enter application name and click on **Create**.

![Client app registration](media/e90a645b51ab87c3be1002ea553beae1.png)

Generate a new password and keep it safe. This is the **Application Secret**, and
will be used in the application for authentication.

![Generate secret](media/7af76b36b33c6fb891f6b81a279876d5.png)

When an application is created, the default delegated permission for it are **User.Read**.
Add two additional delegated permissions 
- **offline_access** 
- **openid**.

Go to **Microsoft Graph Permissions**, the **Delegated Permissions** in the page to add these permissions.

![Client app settings](media/80019947a3cce059868f06af8c3ade64.png)

For the MVC web application, a new “Web” platform needs to be added from
“Platforms” section of the page.

Enter the **Redirect URLs** for the web platform. This URL is where your MVC web app
should redirect after authentication. You can change Redirect URLs based on your application configuration.

![Client app platform](media/569a20d01ecc065a7c7e2dda1d71c2f8.png)

Now save the changes you made and the portal will take you to the landing page,
where all your registered applications are listed. The **App Id/ Client ID** listed here will be used to authenticate in the MVC web app.

![Client app ID](media/b62f689c2a22aadc75c50be3f1e4e054.png)

Provide Consent and Accept Terms
--------------------------------

If you haven't already logged in to [Custom Translator
Portal](https://portal.customtranslator.azure.ai) with your user account (the
same user account you used to create/register the AAD app), you will need to
login to accept the terms and conditions.

After you sign in to [Custom Translator
portal](https://portal.customtranslator.azure.ai) with your user account, you
will receive a popup window requesting your consent.

![Consent](media/6f80750d375a5554fe034a66aeb1d07b.png)

After you’ve provided consent, a popup window for terms will be shown. Read and
accept the terms to continue.

![Accept terms](media/3b8c1ee4b297b3f9349b619ab42f7e04.png)

Setup MVC App Code
==================

Run Visual Studio, open CustomTranslatorSampleCode.sln and expand CustomTranslatorSampleCode.

In Controllers folder open **HomeController.cs**, and update following code:

1. **clientID**: update this value with the App Id/Client ID listed in Application Registration Portal.
2. **clientsecret**: update this value with your App’s secret/ password.
3. **redirectUri**: update it as per your MVC app’s URL.

![Variables](media/d1458ea2a714990ad437a0a09cc89fbd.png)

4.  **session Session["ws_id"]**: update this variable based on your workspace ID.

![Workspace](media/f651beb476cce3fe6e48a2841cb6feeb.png)

In Controllers folder open **ModelController.cs**, go to **Create()** method and update following code:

1. **model.name**: update this value desired model name.
2. **model.projectId**: update this value with your project id.
3. **model.documentIds.Add()**: add document id in this list. You can add multiple documents.

![Workspace](media/model_create.png)

In Controllers folder open **ProjectController.cs**, go to **Index()** method and update following code:

1. **newproject.name**: update this value desired project name.
2. **newproject.languagePairId**: update this value with appropriate language pair id.
3. **newproject.categoryid**: update this value with appropriate category id.
4. **newproject.categoryDescriptor**: update this value desired project category descriptor.
5. **newproject.label**: update this value desired project label.
6. **newproject.description**: update this value desired project description.

![Workspace](media/project_index.png)

In Controllers folder open **UploadController.cs**, go to **ParallelFile()** method and update following code:

1. **sourcelanguagefilepath**: update this value of the local path for source language file.
2. **targetlanguagefilepath**: update this value of the local path for source target file.
3. **documentdetails.DocumentName**: update this value with desired document name.
4. **documentdetails.DocumentType**: update this value desired document type. Values can be of training/ tuning/ testing.
5. **sourcelanguagefile.Language**: update this value with source language code.
6. **sourcelanguagefile.OverwriteIfExists**: if you want to overwrite with this file, if the same file name exists use **true**, else use **false**.
7. **targetlanguagefile.Language**: update this value with target language code.
8. **targetlanguagefile.OverwriteIfExists**: if you want to overwrite with this file, if the same file name exists use **true**, else use **false**.

![Workspace](media/upload_parallel.png)

In Controllers folder open **UploadController.cs**, go to **ComboFile()** method and update following code:

1. **filepath**: update this value of the local path for combo file.
2. **documentdetails.DocumentName**: update this value with desired document name.
3. **documentdetails.DocumentType**: update this value desired document type. Values can be of training/ tuning/ testing.

![Workspace](media/upload_combo.png)

Build the code and run it in Visual Studio to verify everything is working.
