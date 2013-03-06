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
using System.Collections.ObjectModel;
namespace wphTweaks
{
    public partial class Rearrange : PhoneApplicationPage
    {
        class settingid {
            public string title;

            public string id;
            public int order;

            public override string ToString()
            {
                return title;
            }
        }
        ObservableCollection<settingid> settings = new ObservableCollection<settingid>();
        ObservableCollection<string> str = new ObservableCollection<string>();
        public Rearrange()
        {
            InitializeComponent();

            var key = WP7RootToolsSDK.Registry.GetKey(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"\Software\Microsoft\Settings\{69DAA7D1-09EA-4eae-A67E-56E4B0B4CA5B}\SecureItems");
            foreach (WP7RootToolsSDK.RegistryValue id in key.GetSubItems())
            {
                string title = WP7RootToolsSDK.Registry.GetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"\Software\Microsoft\Settings\" + id.ValueName, "Title");
                settings.Add(new settingid() { id = id.ValueName, title = title, order = Convert.ToInt16(id.Value) });
            }
            settings = new ObservableCollection<settingid>(settings.OrderBy(item => item.order));


            reorderListBox.DataContext = this.settings;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (settingid id in settings)
            {
                WP7RootToolsSDK.Registry.SetDWordValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"\Software\Microsoft\Settings\{69DAA7D1-09EA-4eae-A67E-56E4B0B4CA5B}\SecureItems", id.id, (uint)i);
                i++;
            }
        }
    }
}