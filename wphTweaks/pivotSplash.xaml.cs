using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.Net;

namespace wphSplashChanger
{
    public partial class pivotSplash : PhoneApplicationPage
    {
        public pivotSplash()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                WP7RootToolsSDK.FileSystem.DeleteFile(@"\windows\mologo.bmp");
            }
            catch
            {
            }
        }

        private void WebBrowser_Navigating_1(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.ToString().Contains("#downloadbg="))
            {
                var id = e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("=") + 1);
                e.Cancel = true;

                using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(id))
                    {
                        setBackground(id);
                    }
                    else
                    {
                        WebClient c = new WebClient();
                        c.OpenReadAsync(new Uri("http://windowsphonehacker.com/splashes/bmp/" + id, UriKind.Absolute), id);
                        c.OpenReadCompleted += c_OpenReadCompleted;
                        MessageBox.Show("Downloading. This may take a bit.");
                    }
                }

            }
        }

        void c_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            string id = (string)e.UserState;
            try
            {
                if (!e.Cancelled)
                {
                    using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var stream = store.OpenFile(id, System.IO.FileMode.Create))
                        {
                            e.Result.CopyTo(stream);
                        }
                    }
                    setBackground(id);
                }
            }
            catch
            {
                MessageBox.Show("Failed!\n" + e.Error.Message);
            }
        }

        void setBackground(string id)
        {
            string dir = @"\Applications\Data\abc1e9fe-b4ab-402c-ab21-11e97e3fde3a\Data\IsolatedStore";
            WP7RootToolsSDK.FileSystem.CopyFile(dir + "\\" + id, @"\windows\mologo.bmp");
            MessageBox.Show("Splash successfully changed.");
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            bro.Navigate(new Uri("http://windowsphonehacker.com/splashes/get.php", UriKind.Absolute));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.PhotoChooserTask t = new Microsoft.Phone.Tasks.PhotoChooserTask();
            t.Show();
            t.Completed += t_Completed;
        }

        void t_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult e)
        {
            if (e.TaskResult == Microsoft.Phone.Tasks.TaskResult.OK)
            {
                var req = HttpWebRequest.Create(new Uri("http://windowsphonehacker.com/splashes/bmp.php", UriKind.Absolute));

                req.Method = "POST";
                var boundary = "-----------------------------28520690214962";
                var newLine = Environment.NewLine;

                var fileHeaderFormat = boundary + newLine +
                                        "Content-Disposition: form-data; name=\"fileu\"; filename=\"bg.jpg\"" + newLine + "Content-Type: image/jpeg" + newLine + newLine;
                req.ContentType = "application/octet-stream";

                req.BeginGetRequestStream(callback =>
                {
                    Stream poststream = req.EndGetRequestStream(callback);
                    var reqWriter = new StreamWriter(poststream);

                    byte[] buf = new byte[e.ChosenPhoto.Length];
                    e.ChosenPhoto.Position = 0;
                    e.ChosenPhoto.Read(buf, 0, (Int32)e.ChosenPhoto.Length);

                    string data = Convert.ToBase64String(buf);
                    //reqWriter.Write(fileHeaderFormat);
                    reqWriter.Write(data);
                    reqWriter.Close();
                    //poststream.Close();
                    req.BeginGetResponse(rep =>
                    {

                        WebResponse reply = req.EndGetResponse(rep);
                        Stream str = reply.GetResponseStream();
                        //StreamReader reader = new StreamReader(str);
                        //System.Diagnostics.Debug.WriteLine(reader.ReadToEnd());

                        this.Dispatcher.BeginInvoke(() =>
                        {
                            string id = "custom__" + DateTime.Now.Millisecond.ToString();
                            using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                            {
                                using (var stream = store.OpenFile(id, System.IO.FileMode.Create))
                                {
                                    str.CopyTo(stream);

                                }
                            }
                            setBackground(id);
                            MessageBox.Show("Successfully set background.");
                        });

                    }, null);
                }, null);
            }
        }
    }
}