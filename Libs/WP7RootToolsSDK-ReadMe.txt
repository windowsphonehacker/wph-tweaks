WP7 Root Tools SDK ReadMe
-------------------------

WP7 Root Tools and WP7 Root Tools SDK are Copyright by Heathcliff74 / www.wp7roottools.com in 2012
Full license details and disclaimer in WP7RootToolsSDK-License.txt
--------------------------------------------------------------------------------------------------

Download: www.wp7roottools.com
------------------------------

How to use the SDK
------------------

There are 3 files in the archive that you need to add to your WP7 project.
The x's are the fileversions of the SDK.
The file-version-references in the filenames are necessary, because native libraries will have
DLL-hell-issues if different versions of those native libraries are installed and used at the same
time.

- WP7RootToolsSDK-x.x.x.x.dll
- WP7RootToolsSDKNative-x.x.x.x.dll
- WPInteropManifest.xml

Add a refence in your project to WP7RootToolsSDK-x.x.x.x.dll.
Include WP7RootToolsSDKNative-x.x.x.x.dll and WPInteropManifest.xml in your project. In properties
of both these files set "Build Action" to "Content" and "Copy to Ouput" to "Copy Always".

Important note: If you ever download a newer version of this SDK and you update your app to use that
new version of the SDK, then don't forget to drop the old SDK-files and include then new SDK-files
in your project, because the old files will not be overwritten due to their unique filenames!


How to run your app
-------------------

If you deploy your app the first time, your app might not have Root Access. If that is the case,
then start WP7 Root Tools 0.9 (or later) and flag your app as "Trusted". If you start your app
again, your app will run with high privileges! Remember that when you do a "Rebuild Solution", that
will trigger a reinstall of your app. You will need to reapply Root Access for your app, using WP7
Root Tools, after you did that.
