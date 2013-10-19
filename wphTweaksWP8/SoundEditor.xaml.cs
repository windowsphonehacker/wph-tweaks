using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Registry;
using FileSystem;

namespace wphTweaks
{
    public partial class SoundEditor : PhoneApplicationPage
    {
        public SoundEditor()
        {
            InitializeComponent();
            string[] sounds;
            bool b = NativeRegistry.GetSubKeyNames(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds", out sounds);
            if (!b)
            {
                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
            }
            else
            {
                foreach (string sound in sounds)
                {
                    var soundButton = new Button() { Content = sound, Tag = sound };
                    soundButton.Click += soundButton_Click;
                    SoundStack.Children.Add(soundButton);
                }
            }
        }

        void soundButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            NativeRegistry.ReadString(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds\" + ((Button)sender).Tag, "Sound", out path);
            CustomMessageBox custMsgBox = new CustomMessageBox();
            custMsgBox.Title = "Edit sound";
            custMsgBox.Tag = ((Button)sender).Tag;
            custMsgBox.Caption = "Editing sound";
            custMsgBox.Message = "Edit the sound";
            custMsgBox.Content = new TextBox() { Text = path };
            custMsgBox.LeftButtonContent = "cancel";
            custMsgBox.RightButtonContent = "save";
            custMsgBox.Dismissed += custMsgBox_Dismissed;
            custMsgBox.Show();

        }

        void custMsgBox_Dismissed(object sender, DismissedEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.RightButton)
            {
                bool b = NativeRegistry.WriteString(RegistryHive.HKLM, @"SOFTWARE\Microsoft\EventSounds\Sounds\" + ((CustomMessageBox)sender).Tag, "Sound", ((TextBox)((CustomMessageBox)sender).Content).Text);
                if (!b)
                {
                    MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
                }
            }
        }
    }
}