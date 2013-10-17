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
using Registry;
using RPCComponent;

namespace wphTweaks
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool cooldown = true;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            CRPCComponent.Initialize();
            // Check for root
            if (true)
            {
                // Add the quick toggles defined in Tweaks
                foreach (Tweak tweak in Tweaks.tweaks)
                {
                    if (tweak.type == Tweak.controlType.toggle)
                    {
                        ToggleSwitch control = new ToggleSwitch();

                        control.Header = tweak.title;
                        if (tweak.description != "")
                            control.Content = tweak.description;

                        control.FontSize = 22;

                        //get valuelolo
                        if (tweak.keyType == Tweak.tweakType.dword)
                        {
                            uint val = 0;
                            try
                            {
                                NativeRegistry.ReadDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), out val);
                            }
                            catch
                            {

                            }
                            control.IsChecked = (val == tweak.onValue);
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(tweak.title + " = " + val);
#endif
                        }
                        else if (tweak.keyType == Tweak.tweakType.str)
                        {
                            string val = "";
                            try
                            {
                                NativeRegistry.ReadString(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), out val);
                            }
                            catch
                            {
                            }
                            control.IsChecked = (val == tweak.strOnValue);
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(tweak.title + " = " + val);
#endif
                        }

                        control.Checked += new EventHandler<RoutedEventArgs>(control_Checked);
                        control.Unchecked += new EventHandler<RoutedEventArgs>(control_Checked);

                        controlsPanel.Children.Add(control);
                    }
                    else if (tweak.type == Tweak.controlType.selector)
                    {
                        ListPicker lp = new ListPicker();
                        lp.Header = tweak.title;
                        lp.ItemsSource = tweak.options;
                        lp.SetValue(ListPicker.ItemCountThresholdProperty, 10);
                        if (tweak.keyType == Tweak.tweakType.dword)
                        {
                            uint val = 0;
                            try
                            {
                                NativeRegistry.ReadDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), out val);
                            }
                            catch
                            {

                            }
                            foreach (var opt in tweak.options)
                            {
                                if (opt.IntValue == val)
                                {
                                    lp.SelectedItem = opt;
                                    break;
                                }
                            }
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(tweak.title + " = " + val);
#endif
                        }
                        else if (tweak.keyType == Tweak.tweakType.str)
                        {
                            string val = "";
                            try
                            {
                                NativeRegistry.ReadString(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), out val);
                            }
                            catch
                            {
                            }
                            foreach (var opt in tweak.options)
                            {
                                if (opt.Value == val)
                                {
                                    lp.SelectedItem = opt;
                                    break;
                                }
                            }
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(tweak.title + " = " + val);
#endif
                        }
                        lp.SelectionChanged += new SelectionChangedEventHandler(lp_SelectionChanged);
                        lp.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(lp_Tap);
                        controlsPanel.Children.Add(lp);
                    }
                    else if (tweak.type == Tweak.controlType.title)
                    {
                        addCategory(tweak.title);
                    }
                    else if (tweak.type == Tweak.controlType.slider)
                    {
                        uint val = (uint)tweak.minValue;

                        NativeRegistry.ReadDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), out val);
                        if (val < tweak.minValue)
                        {
                            val = (uint)tweak.minValue;
                        }
                        else if (val > tweak.maxValue)
                        {
                            val = (uint)tweak.maxValue;
                        }

                        StackPanel sliderStack = new StackPanel();
                        Grid vertStack = new Grid();

                        TextBlock tb2 = new TextBlock();
                        tb2.FontSize = (double)Application.Current.Resources["PhoneFontSizeNormal"];
                        tb2.Padding = new Thickness(10, 0, 0, 0);
                        tb2.Text = tweak.title;
                        tb2.FontFamily = (FontFamily)Application.Current.Resources["PhoneFontFamilyNormal"];
                        tb2.Foreground = (Brush)Application.Current.Resources["PhoneSubtleBrush"];
                        vertStack.Children.Add(tb2);

                        TextBlock tb = new TextBlock();
                        tb.FontSize = 18;
                        tb.Text = val.ToString();
                        tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        tb.Margin = new Thickness(0, 0, 10, 0);
                        vertStack.Children.Add(tb);
                        sliderStack.Children.Add(vertStack);

                        Slider sl = new Slider();
                        sl.Name = tweak.title;
                        sl.Minimum = tweak.minValue;
                        sl.Maximum = tweak.maxValue;
                        sl.SmallChange = 1;
                        sl.LargeChange = 2;

                        sl.Value = val;

                        sl.ValueChanged += sl_ValueChanged;
                        sliderStack.Children.Add(sl);
                        controlsPanel.Children.Add(sliderStack);

                    }
                }
            }
            else
            {
                LayoutRoot.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        void sl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider sl = (Slider)sender;
            TextBlock tb = (TextBlock)((Grid)((StackPanel)sl.Parent).Children[0]).Children[1];
            uint oldVal = uint.Parse(tb.Text);
            uint newVal = (uint)e.NewValue;
            if (oldVal != newVal)
            {
                tb.Text = (newVal).ToString();
                foreach (Tweak tweak in Tweaks.tweaks)
                {
                    if (tweak.title == sl.Name)
                    {
                        NativeRegistry.CreateKey(tweak.getHive(), tweak.getKeyName());

                        bool b = NativeRegistry.WriteDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), newVal);
                        if (!b)
                        {
                            MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
                        }
                        if (tweak.rebootNeeded)
                            rbneeded();
                    }

                }
            }


        }

        void rearrangeSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Rearrange.xaml", UriKind.Relative));
        }

        void addCategory(string str)
        {
            TextBlock tb2 = new TextBlock();
            tb2.FontSize = 25;
            tb2.Text = str;
            controlsPanel.Children.Add(tb2);
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            //HKCU\Software\Microsoft\Internet Explorer\SearchProviders\Google\URL
            //http://www.google.com/search?hl=en&q={searchTerms}&meta=
            try
            {
                bool b = NativeRegistry.CreateKey(RegistryHive.HKCU, @"Software\Microsoft\Internet Explorer\SearchProviders\Google");
                if (!b)
                {
                    MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                }
            }
            catch
            {
            }
            try
            {
                bool b = NativeRegistry.WriteString(RegistryHive.HKCU, @"Software\Microsoft\Internet Explorer\SearchProviders\Google", "URL", "http://www.google.com/search?q={searchTerms}");
                if (!b)
                {
                    MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                }
            }
            catch
            {
            }
            //TODO: Find a fix
            uint id = Processes.NativeProcess.CreateProc(@"C:\Programs\InternetExplorer\BrowserSettingsHost.exe");
            MessageBox.Show(id.ToString());
            MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
            //CSharp___DllImport.Phone.AppLauncher.LaunchBuiltInApplication(CSharp___DllImport.Phone.AppLauncher.Apps.Internet7Settings, "_default");
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Disclaimer
            bool disclaimer = false;
            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.TryGetValue<bool>("disclaimer", out disclaimer);
            if (!disclaimer)
            {
                NavigationService.Navigate(new Uri("/DisclaimerPage.xaml", UriKind.Relative));
            }

            //if (!WP7RootToolsSDK.Environment.HasRootAccess())
            {
                //NavigationService.Navigate(new Uri("/NeedsRoot.xaml", UriKind.Relative));
            }

            LayoutRoot.Visibility = System.Windows.Visibility.Visible;
            UpdateChecker.CheckUpdatesAsync();
        }

        void picker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((ListPicker)sender).Open();
        }

        void lp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ListPicker lp = (ListPicker)sender;
            lp.Open();
        }

        void lp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cooldown)
            {
                ListPicker ctrl = (ListPicker)sender;

                foreach (Tweak tweak in Tweaks.tweaks)
                {
                    if (tweak.title == (string)ctrl.Header)
                    {
                        if (tweak.keyType == Tweak.tweakType.str)
                        {
                            string val = ((SelectorTweak)ctrl.SelectedItem).Value;
                            bool b = NativeRegistry.WriteString(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), val);
                            if (!b)
                            {
                                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                            }
                            System.Diagnostics.Debug.WriteLine(val);
                        }
                        else
                        {
                            try
                            {
                                NativeRegistry.CreateKey(tweak.getHive(), tweak.getKeyName());
                            }
                            catch
                            {
                            }
                            int val = ((SelectorTweak)ctrl.SelectedItem).IntValue;
                            bool b = NativeRegistry.WriteDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), (uint)val);
                            if (!b)
                            {
                                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                            }
                            if (tweak.rebootNeeded)
                                rbneeded();
                        }
                    }
                }
            }
        }

        void control_Checked(object sender, RoutedEventArgs e)
        {
            if (!cooldown)
            {
                ToggleSwitch ctrl = (ToggleSwitch)sender;

                foreach (Tweak tweak in Tweaks.tweaks)
                {
                    if (tweak.title == (string)ctrl.Header)
                    {
                        if (tweak.keyType == Tweak.tweakType.dword)
                        {
                            try
                            {
                                NativeRegistry.CreateKey(tweak.getHive(), tweak.getKeyName());
                            }
                            catch
                            {
                            }
                            int val = (ctrl.IsChecked.Value ? tweak.onValue : tweak.offValue);
                            bool b = NativeRegistry.WriteDWORD(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), (uint)val);
                            if (!b)
                            {
                                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                            }
                        }
                        else
                        {
                            string val = (ctrl.IsChecked.Value ? tweak.strOnValue : tweak.strOffValue);
                            bool b = NativeRegistry.WriteString(tweak.getHive(), tweak.getKeyName(), tweak.getValueName(), val);
                            if (!b)
                            {
                                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());

                            }
                        }
                        if (tweak.rebootNeeded)
                        {
                            rbneeded();
                        }
                    }
                }
            }
        }

        private void controlsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            cooldown = false;
        }

        private void btnUnlockMarketplace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/mostore.xaml", UriKind.Relative));
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            //CSharp___DllImport.Phone.OS.Shutdown(CSharp___DllImport.EWX.EWX_REBOOT);
        }

        private void btnBrandedUpdates_Click(object sender, RoutedEventArgs e)
        {
            var old = "";
            String[] obvals = { "MOName", "OemName", "MobileOperator" };
            foreach (String val in obvals)
            {
                //old = WP7RootToolsSDK.Registry.GetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"System\Platform\DeviceTargetingInfo", val);
                //WP7RootToolsSDK.Registry.SetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"System\Platform\DeviceTargetingInfo", val, old + "_blocked");
            }

        }

        private void btnEnableBrandedUpdates_Click(object sender, RoutedEventArgs e)
        {
            var old = "";
            String[] obvals = { "MOName", "OemName", "MobileOperator" };

            foreach (String val in obvals)
            {
                //old = WP7RootToolsSDK.Registry.GetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"System\Platform\DeviceTargetingInfo", val);
                //WP7RootToolsSDK.Registry.SetStringValue(WP7RootToolsSDK.RegistryHyve.LocalMachine, @"System\Platform\DeviceTargetingInfo", val, old.Replace("_blocked", ""));
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //WP7RootToolsSDK.FileSystem.CopyFile(@"\Applications\Install\abc1e9fe-b4ab-402c-ab21-11e97e3fde3a\Install\SplashScreenImage.jpg", @"\Windows\mologo.bmp");
        }

        private void btnRestoreCarrierLogo_Click(object sender, RoutedEventArgs e)
        {
            //WP7RootToolsSDK.FileSystem.DeleteFile(@"\Windows\mologo.bmp");
        }

        public static void rbneeded()
        {
            if (MessageBox.Show("Reboot needed for this change to take effect. Reboot now?", "Reboot Needed", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                uint stuff = 0;
                uint thingy = CRPCComponent.System_Reboot(out stuff);
                System.Diagnostics.Debug.WriteLine(stuff + " - " + thingy);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pivotSplash.xaml", UriKind.Relative));
        }

        private void btnCustomizeKeyboard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Keyboard.xaml", UriKind.Relative));
        }

    }
}