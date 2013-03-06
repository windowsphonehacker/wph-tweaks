using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace wphTweaks
{
    public partial class Keyboard : PhoneApplicationPage
    {
        string key = @"Software\Microsoft\FingerKB\AlternateMappings\{805d58c2-096a-4451-b2cb-40996fcb236d}";
        public Keyboard()
        {
            InitializeComponent();
        }

        private void tbKey_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbKey.Text == "key, e.g. @")
            {
                tbKey.Text = "";
            }
        }

        private void tbKey_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var results = WP7RootToolsSDK.Registry.GetMultiStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, key, tbKey.Text);
                tbAlternates.Text = String.Join("\r\n", results);
            }
            catch
            {
                tbAlternates.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                tbAlternates.Text = tbAlternates.Text.Replace("\n", "");
                WP7RootToolsSDK.Registry.SetMultiStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, key, tbKey.Text, tbAlternates.Text.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}