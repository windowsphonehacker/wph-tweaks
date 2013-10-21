using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Windows.Media;
using System.IO.IsolatedStorage;

namespace HomebrewHelperWP.Filesystem
{
    public partial class RingtoneChooser : UserControl
    {
        public string SelectedRingtone { get; set; }
        MediaElement med = new MediaElement();
        public RingtoneChooser()
        {
            InitializeComponent();
            RingtoneList.MaxHeight = 550;
            this.Loaded += RingtoneChooser_Loaded;
        }

        void RingtoneChooser_Loaded(object sender, RoutedEventArgs e)
        {
            var noneItem = new RingtoneListItem(){
                Path = "*none*",
                DisplayName = " -- None --"
            };
            RingtoneList.Items.Add(noneItem);
            if (SelectedRingtone != null && SelectedRingtone.ToLower().Equals("*none*"))
            {
                RingtoneList.SelectedItem = noneItem;
            }
            string[] paths = new string[] { @"C:\Data\Users\Public\Ringtones", @"C:\Programs\CommonFiles\Sounds" };
            foreach (string path in paths)
            {
                string[] files = Directory.GetFiles(path, "*.*");
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var item = new RingtoneListItem();
                        item.DisplayName = Path.GetFileNameWithoutExtension(file);
                        item.Path = file;
                        RingtoneList.Items.Add(item);
                        if (SelectedRingtone != null && file.ToLower().Equals(SelectedRingtone.ToLower()))
                        {
                            RingtoneList.SelectedItem = item;
                        }
                    }
                }
            }
        }

        public class RingtoneListItem
        {
            public string DisplayName { get; set; }
            public string Path { get; set; }
            public override string ToString()
            {
                return DisplayName;
            }
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("Play button is not implemented yet!");
            return;
            //TODO: Fix this
            var file = (RingtoneListItem)((Image)sender).DataContext;
            File.Copy(file.Path, "C:\\Data\\Users\\DefApps\\AppData\\{A85AAECB-E288-4B19-A8E0-FCA5D0F2A444}\\Local\\" + file.DisplayName + ".tmp", true);
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isostream = new IsolatedStorageFileStream(file.DisplayName + ".tmp", FileMode.Open, isoStore))
                {
                    ((StackPanel)((Image)sender).Parent).Children.Add(med);
                    med.SetSource(isostream);

                    med.Play();
                }
                isoStore.DeleteFile(file.DisplayName + ".tmp");
            }
        }

        private void RingtoneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RingtoneList.SelectedItem != null)
            {
                SelectedRingtone = ((RingtoneListItem)RingtoneList.SelectedItem).Path;
            }
        }
    }
}
