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
    public partial class DisclaimerPage : PhoneApplicationPage
    {
        public DisclaimerPage()
        {
            InitializeComponent();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Remove("disclaimer");
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Add("disclaimer", true);
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Save();
            }
            catch
            {
            }
            if (e.NewValue > 7)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }
    }
}