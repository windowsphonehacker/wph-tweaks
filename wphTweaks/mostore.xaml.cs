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

namespace wphTweaks
{
    public partial class mostore : PhoneApplicationPage
    {
        public mostore()
        {
            InitializeComponent();
        }

        void doMOChange(string store, string id)
        {
            string str = "<ConfigurationFile version=\"1\">    <OEMStore>        <setting id=\"OEMName\"></setting>        <setting id=\"OEMStoreName\">" + store + "</setting>        <setting id=\"OEMStoreID\">" + id + "</setting>        <setting id=\"OEMStoreEnabled\">True</setting>    </OEMStore></ConfigurationFile>";
            WP7RootToolsSDK.FileSystem.WriteFile(@"\My Documents\Zune\PimentoCache\Keepers\LKG_OEMStoreConfig.xml", System.Text.UTF8Encoding.UTF8.GetBytes(str));
            MainPage.rbneeded();

            NavigationService.GoBack();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            doMOChange("Nokia", "nokia");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            doMOChange("HTC", "htc");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            doMOChange("Samsung", "samsung");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            doMOChange("LG", "lge");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            doMOChange("Fujitsu", "Fujitsu");
        }


    }
}