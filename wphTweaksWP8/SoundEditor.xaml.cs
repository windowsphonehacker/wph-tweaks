using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HomebrewHelperWP;
using HomebrewHelperWP.Filesystem;

namespace wphTweaks
{
    public partial class SoundEditor : PhoneApplicationPage
    {
        public SoundEditor()
        {
            InitializeComponent();
            string[] sounds = Registry.GetSubKeyNames(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds");
            if (sounds == null)
            {
                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)Registry.LastError);
            }
            else
            {
                foreach (string sound in sounds)
                {
                    var soundButton = new Button() { Content = sound, Tag = sound };
                    if (Registry.ReadString(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds\" + sound, "Sound").Length > 3)
                    {
                        soundButton.Click += soundButton_Click;
                    }
                    else
                    {
                        soundButton.IsEnabled = false;
                    }
                    SoundStack.Children.Add(soundButton);
                }
            }
        }

        void soundButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = HomebrewHelperWP.Registry.ReadString(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds\" + ((Button)sender).Tag, "Sound");
                CustomMessageBox custMsgBox = new CustomMessageBox();
                custMsgBox.Title = "Change notification sound";
                custMsgBox.Tag = ((Button)sender).Tag;
                custMsgBox.Caption = "Editing sound " + ((Button)sender).Tag;
                custMsgBox.Message = "Please select a new sound from the list below";
                custMsgBox.Content = new RingtoneChooser() { SelectedRingtone = path };
                custMsgBox.LeftButtonContent = "cancel";
                custMsgBox.RightButtonContent = "save";
                custMsgBox.IsFullScreen = true;
                custMsgBox.Dismissed += custMsgBox_Dismissed;
                custMsgBox.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void custMsgBox_Dismissed(object sender, DismissedEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.RightButton)
            {
                try
                {
                    HomebrewHelperWP.Registry.WriteString(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds\" + ((CustomMessageBox)sender).Tag, "Sound", ((RingtoneChooser)((CustomMessageBox)sender).Content).SelectedRingtone);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed: " + ex.Message);
                }
            }
        }
    }
}