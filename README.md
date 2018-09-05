Prerequisites
=============

Create and Register your Azure AD Client App
--------------------------------------------

This sample code uses OAuth and OpenID for authentication. Please login to
<https://apps.dev.microsoft.com> with the same user credentials you use to login to 
Custom Translator. The landing page shows the list of client apps that are created/
available to use with your account.

Create an app using “Add an app” button at top right hand corner of the landing
page.

![](media/a7247b6a16b3f4151e06661514e03c17.png)

Enter application name and click on “Create”.

![](media/e90a645b51ab87c3be1002ea553beae1.png)

Generate a new password and keep it safe. This is the Application Secret, and
will be used in the application for authentication.

![](media/7af76b36b33c6fb891f6b81a279876d5.png)

When an application is created the default delegated permission for it are
“User.Read”.

Please add two additional delegated permissions (a) offline_access (b) openid.
These can be added from the “Microsoft Graph Permissions \>\> Delegated Permissions”
section in the page.

![](media/80019947a3cce059868f06af8c3ade64.png)

For the MVC web application a new “Web” platform needs to be added from
“Platforms” section of the page.

Please enter the redirect URL for the web platform. This is the URL where your
MVC web app should redirect after authentication.

You can ignore the logout URL field.

![](media/569a20d01ecc065a7c7e2dda1d71c2f8.png)

Now save the changes you made and the portal will take you to the landing page,
where all your registered applications are listed. The App Id/ Client Id listed here
will be used to authenticate in the MVC web app.

![](media/b62f689c2a22aadc75c50be3f1e4e054.png)

Provide Consent and Accept Terms
--------------------------------

If you haven't already logged in to Custom Translator Portal
(<https://portal.customtranslator.azure.ai>) with your user account (the same user
account you used to create/register the AAD app), you will need to login to accept 
the terms and conditions.

After you login to Custom Translator portal
(<https://portal.customtranslator.azure.ai>) with your user account, you will
receive a popup window asking for your consent.

![](media/6f80750d375a5554fe034a66aeb1d07b.png)

After you’ve provided consent, a popup window for terms will be shown.
Please read and accept the terms to continue.

![](media/3b8c1ee4b297b3f9349b619ab42f7e04.png)

Setup MVC App Code
==================

Open Visual Studio (Run as Administrator) and open
CustomTranslatorSampleCode.sln.

Go to CustomTranslatorSampleCode project and open Controllers \>\>
HomeController.cs

Update the values

1.  clientID: update this value with the App Id/Client Id
    listed in Application Registration Portal

2.  clientsecret: update this value with your App’s secret/ password

3.  redirectUri: update it as per your MVC app’s controller URL

![](media/d1458ea2a714990ad437a0a09cc89fbd.png)

Now build the code and run it in Visual Studio to verify everything is working
(you may want to change the mode to run in debug mode). If you have not 
modified any code other than the changes listed above, the app should show the
JSON output of supported languages for Custom Translator.

Deploy the MVC App on Local IIS (Optional)
==========================================

If you prefer to deploy and run the web app on local ISS please follow these
steps.

Publish the app from publish wizard using custom profile.

![](media/fb5278e8e901134ce7e25686304ab73d.png)

In the connection tab, please enter the Server as “localhost” and Sitename as
“Default Web Site”. Now validate the connection.

![](media/0765db8f92c8fd2dd21f1e24fd7ed5dc.png)

Go to setting tab and user configuration as “Release”

![](media/bdaac1af5aca5b5963c5939cd282fcbe.png)

Preview the publish settings and files those are being published and then
publish the same.

Open your browser hit <http://localhost/>, and this should run the MVC
application on IIS.

![](media/bea49e083166c2118f8f18de9a3a194e.png)
