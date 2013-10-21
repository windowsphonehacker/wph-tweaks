using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace wphTweaks
{
    class UpdateChecker
    {
        public static void CheckUpdatesAsync()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringAsync(new Uri("http://ci.dy.fi/job/WPH-Tweaks/api/xml"));
            wc.DownloadStringCompleted += wc_DownloadStringCompleted;
        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                //Load xml
                XDocument xdoc = XDocument.Parse(e.Result);

                //Run query
                var lastbuild = (from lv1 in xdoc.Descendants("lastSuccessfulBuild")
                                 select new
                                 {
                                     BuildNumber = lv1.Descendants("number").FirstOrDefault().Value,
                                     BuildUrl = lv1.Descendants("url").FirstOrDefault().Value
                                 }).FirstOrDefault();
                var curbuildfile = System.Windows.Application.GetResourceStream(new Uri("buildnum.txt", UriKind.Relative));
                int curbuild = 1;
                using (StreamReader sr = new StreamReader(curbuildfile.Stream))
                {
                    Int32.TryParse(sr.ReadLine().Trim(), out curbuild);
                }

                int lastbuildnum = 1;
                Int32.TryParse(lastbuild.BuildNumber, out lastbuildnum);
                if (lastbuildnum > curbuild)
                {
                    MessageBox.Show("There is a new build available to download!\nCurrent build: " + curbuild + "\nLatest build: " + lastbuildnum + "\nDownload URL:\n" + lastbuild.BuildUrl, "Update available!", MessageBoxButton.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Update check failed! " + ex.Message);
            }
        }
    }
}
