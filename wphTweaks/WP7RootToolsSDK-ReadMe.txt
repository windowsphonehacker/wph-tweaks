WP7 Root Tools SDK Reference
----------------------------

WP7 Root Tools and WP7 Root Tools SDK are Copyright by Heathcliff74 in 2012
---------------------------------------------------------------------------

Download: www.wp7roottools.com
------------------------------


Use the tools and the SDK with care and at your own risk! The author of WP7 Root Tools and WP7 Root Tools SDK can not be held responsible for any damage that is caused directly or indirectly by the use of WP7 Root Tools or WP7 Root Tools SDK!

Use of the software is free of charge. If you use the software components of the WP7 Root Tools SDK, you are expected to respect the amount of work put into it by the author and give the author proper credits of that work!

This is an early alpha release. That means that the software is not properly tested yet and it can be subject to change! The documentation will be extended with more code-snippets and -examples.


How to use the SDK
------------------

There are 3 files in the archive that you need to add to your WP7 project.
The x's are the fileversions of the SDK.
This is necessary, because native libraries will have DLL-hell-issues, if different versions of those native libraries are installed and used at the same time.

- WP7RootToolsSDK-x.x.x.x.dll
- WP7RootToolsSDKNative-x.x.x.x.dll
- WPInteropManifest.xml

Add a refence in your project to WP7RootToolsSDK-x.x.x.x.dll.
Include WP7RootToolsSDKNative-x.x.x.x.dll and WPInteropManifest.xml in your project. In properties of both these files set "Build Action" to "Content" and "Copy to Ouput" to "Copy Always".

Important note: If you ever download a newer version of this SDK and you update your app to use that new version of the SDK, then don't forget to drop the old SDK-files and include then new SDK-files in your project, because the old files will not be overwritten due to their unique filenames!


How to run your app
-------------------

If you deploy your app the first time, your app might not have Root Access. If that is the case, then start WP7 Root Tools 0.9 (or later) and flag your app as "Trusted". If you start your app again, your app will run with high privileges! Remember that when you do a "Rebuild Solution", that will trigger a reinstall of your app. You will need to reapply Root Access for your app, using WP7 Root Tools, after you did that.


Sample code
-----------

            // FileSystem

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.Environment.HasRootAccess());

            WP7RootToolsSDK.Folder folder = WP7RootToolsSDK.FileSystem.GetFolder("\\Applications");
            List<WP7RootToolsSDK.FileSystemEntry> items = folder.GetSubItems();
            
            System.Diagnostics.Debug.Assert(items.Count >= 2); // Expect at least "Data" and "Install" folder

            // Write UTF8 text file
            string writetext = "Heathcliff74";
            WP7RootToolsSDK.FileSystem.WriteFile(@"\Windows\MyTestFile.txt", System.Text.Encoding.UTF8.GetBytes(writetext));

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.FileSystem.FileExists(@"\Windows\MyTestFile.txt"));

            // Read UTF8 text file
            byte[] buffer = WP7RootToolsSDK.FileSystem.ReadFile(@"\Windows\MyTestFile.txt");
            string readtext = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            System.Diagnostics.Debug.Assert(readtext == "Heathcliff74");

            WP7RootToolsSDK.FileSystem.CreateFolder(@"\MyTestFolder");

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.FileSystem.FileExists(@"\MyTestFolder"));

            if (WP7RootToolsSDK.FileSystem.FileExists(@"\MyTestFolder\MyTestFile.txt"))
                WP7RootToolsSDK.FileSystem.DeleteFile(@"\MyTestFolder\MyTestFile.txt");

            WP7RootToolsSDK.FileSystem.MoveFile(@"\Windows\MyTestFile.txt", @"\MyTestFolder\MyTestFile.txt");

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.FileSystem.FileExists(@"\MyTestFolder\MyTestFile.txt") && !WP7RootToolsSDK.FileSystem.FileExists(@"\Windows\MyTestFile.txt"));

            WP7RootToolsSDK.FileSystem.CopyFile(@"\MyTestFolder\MyTestFile.txt", @"\MyTestFolder\MyTestFile2.txt");

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.FileSystem.FileExists(@"\MyTestFolder\MyTestFile2.txt"));

            buffer = new byte[0];
            WP7RootToolsSDK.FileSystem.WriteFile(@"\MyTestFolder\ZeroLength.txt", buffer);
            buffer = WP7RootToolsSDK.FileSystem.ReadFile(@"\MyTestFolder\ZeroLength.txt");

            System.Diagnostics.Debug.Assert((buffer != null) && (buffer.Length == 0));

            WP7RootToolsSDK.FileStream s = new WP7RootToolsSDK.FileStream(@"\MyTestFolder\MyTestFile3.txt", WP7RootToolsSDK.FileStreamMode.Write);
            buffer = System.Text.Encoding.UTF8.GetBytes(writetext);
            s.Write(buffer, 0, 6);
            s.Write(buffer, 6, 6);
            s.Close();

            System.Diagnostics.Debug.Assert(WP7RootToolsSDK.FileSystem.GetFileSize(@"\MyTestFolder\MyTestFile3.txt") == 12);

            buffer = new byte[12];
            s = new WP7RootToolsSDK.FileStream(@"\MyTestFolder\MyTestFile3.txt", WP7RootToolsSDK.FileStreamMode.Read);
            s.Read(buffer, 0, 6);
            s.Read(buffer, 6, 6);
            s.Close();

            readtext = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            System.Diagnostics.Debug.Assert(readtext == "Heathcliff74");

            // Applications

            List<WP7RootToolsSDK.ApplicationInfo> AppList = WP7RootToolsSDK.Applications.GetApplicationList();

            System.Diagnostics.Debug.Assert((AppList != null) && (AppList.Count >= 1) && (AppList[0].Name == WP7RootToolsSDK.Applications.GetApplicationName(AppList[0].ProductID)));

            // Registry

            WP7RootToolsSDK.Registry.CreateKey(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey");

            WP7RootToolsSDK.Registry.SetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyStringValue", writetext);
            readtext = WP7RootToolsSDK.Registry.GetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyStringValue");

            System.Diagnostics.Debug.Assert(readtext == writetext);

            UInt32 writevalue = 74;
            WP7RootToolsSDK.Registry.SetDWordValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyDWordValue", writevalue);
            UInt32 readvalue = WP7RootToolsSDK.Registry.GetDWordValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyDWordValue");

            System.Diagnostics.Debug.Assert(readvalue == writevalue);

            WP7RootToolsSDK.RegistryKey MyKey = WP7RootToolsSDK.Registry.GetKey(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey");

            System.Diagnostics.Debug.Assert(MyKey.GetSubItems().Count >= 2);

            WP7RootToolsSDK.Registry.SetValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyStringValue", writetext);
            readtext = (string)WP7RootToolsSDK.Registry.GetValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyStringValue");

            System.Diagnostics.Debug.Assert(readtext == writetext);

            WP7RootToolsSDK.Registry.SetValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyDWordValue", writevalue);
            readvalue = (UInt32)WP7RootToolsSDK.Registry.GetValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey", "MyDWordValue");

            System.Diagnostics.Debug.Assert(readvalue == writevalue);

            WP7RootToolsSDK.Registry.DeleteKey(WP7RootToolsSDK.RegistryHyve.LocalMachine, "MyKey");
