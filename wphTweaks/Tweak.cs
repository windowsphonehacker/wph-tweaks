using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using WP7RootToolsSDK;
namespace wphTweaks
{
    public class Tweak
    {
        public enum tweakType { dword = 0, str }
        public enum controlType { toggle = 0, slider, selector, title }
        public string title;

        public string key;
        
        public tweakType keyType;

        public string defaultString;
        public int defaultInt;

        public controlType type;
        public bool rebootNeeded;

        //slider
        public int minValue;
        public int maxValue;

        //toggle
        public int onValue = 1;
        public int offValue = 0;
        public string strOnValue;
        public string strOffValue;
        public string description;


        //selector
        public List<SelectorTweak> options;

        public WP7RootToolsSDK.RegistryHyve getHyve()
        {
            string firstFour = key.Substring(0,4);
            switch (firstFour)
            {
                case "HKLM":
                    return RegistryHyve.LocalMachine;
                case "HKCU":
                    return RegistryHyve.CurrentUser;
            }
            return RegistryHyve.LocalMachine;
        }
        public string getValueName()
        {
            return key.Substring(key.LastIndexOf("\\") + 1);
        }
        public string getKeyName()
        {
            return key.Substring(5, key.LastIndexOf("\\") - 5);
        }
    }
    
    public class Tweaks
    {
        public static Collection<Tweak> tweaks;
        static Tweaks()
        {
            tweaks = new Collection<Tweak>();
           
            Tweak t;

            t = new Tweak();
            t.title = "Useful Toggles";
            t.type = Tweak.controlType.title;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Access Media While Syncing";
            t.description = "Toggle each sync";
            t.key = @"HKLM\Software\Microsoft\Zune\Events\ZNetSyncState";
            t.onValue = 0;
            t.offValue = 1;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Fake Xbox 'Gold'";
            t.strOnValue = "Gold";
            t.strOffValue = "Silver";
            t.keyType = Tweak.tweakType.str;
            t.key = @"HKCU\Software\Microsoft\GameFoundation\Store\GamerProfile\Current\MembershipLevel";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "System Settings";
            t.type = Tweak.controlType.title;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Hide clock";
            t.key = @"HKLM\System\State\DateTime\ClockInvisibleOnSystemTray";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Dehydration Hack";
            t.offValue = 3;
            t.onValue = 0;
            t.key = @"HKLM\Software\Microsoft\TaskHost\DehydrateOnPause";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "32bit color";
            t.onValue = 32;
            t.offValue = 16;
            t.key = @"HKLM\Drivers\Display\Primary\bpp";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "'Never' timeout option";
            t.onValue = 0;
            t.offValue = 1;
            t.key = @"HKLM\ControlPanel\Lock\DisableNever";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Soft shutter button";
            t.key = @"HKLM\SOFTWARE\Microsoft\Camera\Settings\SoftShutterButton";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Disable Start Menu Letters";
            t.onValue = 500;
            t.offValue = 45;
            t.key = @"HKCU\Software\Microsoft\Start\GroupingThreshold";
            t.rebootNeeded = true;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Max Task Switcher Apps";
            t.key = @"HKLM\System\Shell\Frame\MaxSessions";
            t.type = Tweak.controlType.selector;
            t.options = new List<SelectorTweak>();

            t.options.Add(new SelectorTweak() { Title = "3", IntValue = 4 });
            t.options.Add(new SelectorTweak() { Title = "5", IntValue = 6 });
            t.options.Add(new SelectorTweak() { Title = "8", IntValue = 9 });
            t.options.Add(new SelectorTweak() { Title = "15", IntValue = 16 });
            t.options.Add(new SelectorTweak() { Title = "no limit", IntValue = 9999 });
            t.rebootNeeded = true;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Play video sound over Bluetooth";
            t.key = @"HKLM\Software\Microsoft\Zune\Playback\Video\A2DP\CapsSet1\DisableA2DPPLayback";
            t.onValue = 0;
            t.offValue = 1;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Enable SmartDJ";
            t.key = @"HKLM\Software\Microsoft\Zune\SmartDj\LocalEnabled";
            t.onValue = 1;
            t.offValue = 0;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Customizations";
            t.type = Tweak.controlType.title;
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Voice (reboot required)";
            t.type = Tweak.controlType.selector;
            t.key = @"HKLM\Software\Microsoft\Speech\UXLanguages\Tokens\" + System.Globalization.CultureInfo.CurrentCulture + @"\Voice";
            t.keyType = Tweak.tweakType.str;
            string prefix = @"HKEY_LOCAL_MACHINE\SOFTWARE\MICROSOFT\SPEECH\Voices\Tokens\";
            t.options = new List<SelectorTweak>();
            t.options.Add(new SelectorTweak() { Title = "Hazel (en-GB)", Value = prefix + "MSTTS_V30_EnGB_HazelM" });
            t.options.Add(new SelectorTweak() { Title = "Helena (es-ES)", Value = prefix + "MSTTS_V30_EsES_HelenaM" });
            t.options.Add(new SelectorTweak() { Title = "Hedda (de-DE)", Value = prefix + "MSTTS_V30_DeDE_HeddaM" });
            t.options.Add(new SelectorTweak() { Title = "Lucia (it-IT)", Value = prefix + "MSTTS_V30_ItIT_LuciaM" });
            t.options.Add(new SelectorTweak() { Title = "Hortense (fr-FR)", Value = prefix + "MSTTS_V30_FrFR_HortenseM" });
            t.options.Add(new SelectorTweak() { Title = "Zira (en-US)", Value = prefix + "MSTTS_V30_EnUS_ZiraM" });

            tweaks.Add(t);

            t = new Tweak();
            t.title = "Volume Control (reboot required)";
            t.key = @"HKCU\ControlPanel\Volume\MaxSystemUIVolume";
            t.type = Tweak.controlType.selector;
            t.options = new List<SelectorTweak>();
            t.options.Add(new SelectorTweak() { Title = "0 - 10", IntValue = 10 });
            t.options.Add(new SelectorTweak() { Title = "0 - 15", IntValue = 15 });
            t.options.Add(new SelectorTweak() { Title = "0 - 20", IntValue = 20 });
            t.options.Add(new SelectorTweak() { Title = "0 - 30 (Default)", IntValue = 30 });
            t.options.Add(new SelectorTweak() { Title = "0 - 40", IntValue = 40 });
            t.options.Add(new SelectorTweak() { Title = "0 - 50", IntValue = 50 });
            tweaks.Add(t);


            t = new Tweak();
            t.title = "Rename 3G+ to 4G";
            t.key = @"HKLM\Software\Microsoft\Connectivity\CellularUx\DataConnectionIcon\1XHSDPA";
            t.keyType = Tweak.tweakType.str;
            t.strOnValue = "4G";
            t.strOffValue = "3G+";
            t.rebootNeeded = true;
            tweaks.Add(t);

            t = new Tweak();
            t.type = Tweak.controlType.title;
            t.title = "Tango-only";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Static IP";
            t.key = @"HKLM\Comm\Connectivity\WiFiSplashUX\EnableStaticIP";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Export contacts to SIM";
            t.key = @"HKLM\Software\Microsoft\Contacts\Sim\EnableSIMAddressBookAndExport";
            tweaks.Add(t);

            t = new Tweak();
            t.type = Tweak.controlType.title;
            t.title = "Pranks";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "Wireless charge";
            t.onValue = 6553618;
            t.offValue = 6553613;
            t.key = @"HKLM\System\State\Battery\Main";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "AC Power";
            t.key = @"HKLM\System\State\Power\ACLineStatus";
            tweaks.Add(t);

            t = new Tweak();
            t.title = "5G";
            t.key = @"HKLM\Software\Microsoft\Connectivity\CellularUx\DataConnectionIcon\1XUMTS";
            t.keyType = Tweak.tweakType.str;
            t.strOnValue = "5G";
            t.strOffValue = "3G";
            t.rebootNeeded = true;
            tweaks.Add(t);
           
        }
    }
    public class SelectorTweak
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public int IntValue { get; set; }
        public override string ToString()
        {
            return Title;
        }
         
    }
}
