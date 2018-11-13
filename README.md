Prerequisites
=============

Create and Register your Azure AD Client App
--------------------------------------------

This sample code uses OAuth and OpenID for authentication. Please sign in to
<https://apps.dev.microsoft.com> with the same user credentials you use to login to 
Custom Translator. The landing page shows the list of client apps that are created/
available to use with your account.

Create an app using “Add an app” button at top right-hand corner of the landing
page.

![My applications](media/a7247b6a16b3f4151e06661514e03c17.png)

Enter application name and click on “Create”.

![Client app registration ](media/e90a645b51ab87c3be1002ea553beae1.png)

Generate a new password and keep it safe. This is the Application Secret, and
will be used in the application for authentication.

![Generate secret](media/7af76b36b33c6fb891f6b81a279876d5.png)

When an application is created, the default delegated permission for it are
“User.Read”.

Add two additional delegated permissions (a) offline_access (b) openid.
These permissions can be added from the “Microsoft Graph Permissions and then Delegated Permissions”
section in the page.

![Client app settings](media/80019947a3cce059868f06af8c3ade64.png)

For the MVC web application, a new “Web” platform needs to be added from
“Platforms” section of the page.

Enter the redirect URL for the web platform. This URL is where your
MVC web app should redirect after authentication.

You can ignore the sign-out URL field.

![Client app platform](media/569a20d01ecc065a7c7e2dda1d71c2f8.png)

Now save the changes you made and the portal will take you to the landing page,
where all your registered applications are listed. The App Id/ Client ID listed here
will be used to authenticate in the MVC web app.

![Client app ID](media/b62f689c2a22aadc75c50be3f1e4e054.png)

Provide Consent and Accept Terms
--------------------------------

If you haven't already logged in to [Custom Translator Portal](https://portal.customtranslator.azure.ai) with your user account (the same user
account you used to create/register the AAD app), you will need to login to accept 
the terms and conditions.

After you sign in to [Custom Translator portal](https://portal.customtranslator.azure.ai) with your user account, you will
receive a popup window requesting your consent.

![Consent](media/6f80750d375a5554fe034a66aeb1d07b.png)

After you’ve provided consent, a popup window for terms will be shown.
Read and accept the terms to continue.

![Accept terms](media/3b8c1ee4b297b3f9349b619ab42f7e04.png)

Setup MVC App Code
==================

Open Visual Studio (Run as Administrator) and open
CustomTranslatorSampleCode.sln.

Go to CustomTranslatorSampleCode project and open Controllers and then 
HomeController.cs

Update the values in your code:

1.  clientID: update this value with the App Id/Client ID
    listed in Application Registration Portal.

2.  clientsecret: update this value with your App’s secret/ password.

3.  redirectUri: update it as per your MVC app’s controller URL.

    ![Variables](media/d1458ea2a714990ad437a0a09cc89fbd.png)

4. session Session["ws_id"]: update this variable based on your workspace ID.

    ![Workspace](media/update_workspace_id.png)

Build the code and run it in Visual Studio to verify everything is working. If you have not modified any code other than the changes listed above, you should see the landing page of the ASP.NET MVC application.

![MVC app landing page](media/mvc_app_landing_page.png)