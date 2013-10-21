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

namespace wphTweaks
{
    public partial class DataConnectionStringsEditor : PhoneApplicationPage
    {
        bool allempty = true;
        public DataConnectionStringsEditor()
        {
            InitializeComponent();
            UMTS_HSPAPLUS.Tag = UMTS_HSPAPLUS.Text = GetStr("UMTS_HSPAPLUS");
            TDSCDMA_DC_HSPAPLUS.Tag = TDSCDMA_DC_HSPAPLUS.Text = GetStr("TDSCDMA_DC_HSPAPLUS");
            TDSCDMA_DEFAULT.Tag = TDSCDMA_DEFAULT.Text = GetStr("TDSCDMA_DEFAULT");
            EVDO_REVB.Tag = EVDO_REVB.Text = GetStr("EVDO_REVB");
            EVDO_REVA.Tag = EVDO_REVA.Text = GetStr("EVDO_REVA");
            TDSCDMA_HSUPA.Tag = TDSCDMA_HSUPA.Text = GetStr("TDSCDMA_HSUPA");
            UMTS_HSUPA.Tag = UMTS_HSUPA.Text = GetStr("UMTS_HSUPA");
            UMTS_UMTS.Tag = UMTS_UMTS.Text = GetStr("UMTS_UMTS");
            _1XRTT.Tag = _1XRTT.Text = GetStr("1XRTT");
            TDSCDMA_UMTS.Tag = TDSCDMA_UMTS.Text = GetStr("TDSCDMA_UMTS");
            EVDO_REV0.Tag = EVDO_REV0.Text = GetStr("EVDO_REV0");
            TDSCDMA_HSPAPLUS.Tag = TDSCDMA_HSPAPLUS.Text = GetStr("TDSCDMA_HSPAPLUS");
            _1XRTT_DEFAULT.Tag = _1XRTT_DEFAULT.Text = GetStr("1XRTT_DEFAULT");
            LTE_DEFAULT.Tag = LTE_DEFAULT.Text = GetStr("LTE_DEFAULT");
            UMTS_DEFAULT.Tag = UMTS_DEFAULT.Text = GetStr("UMTS_DEFAULT");
            GSM_DEFAULT.Tag = GSM_DEFAULT.Text = GetStr("GSM_DEFAULT");
            LTE_FDD.Tag = LTE_FDD.Text = GetStr("LTE_FDD");
            UMTS_DC_HSPAPLUS.Tag = UMTS_DC_HSPAPLUS.Text = GetStr("UMTS_DC_HSPAPLUS");
            GSM_GPRS.Tag = GSM_GPRS.Text = GetStr("GSM_GPRS");
            EVDO_DEFAULT.Tag = EVDO_DEFAULT.Text = GetStr("EVDO_DEFAULT");
            UMTS_HSDPA.Tag = UMTS_HSDPA.Text = GetStr("UMTS_HSDPA");
            GSM_EDGE.Tag = GSM_EDGE.Text = GetStr("GSM_EDGE");
            TDSCDMA_HSDPA.Tag = TDSCDMA_HSDPA.Text = GetStr("TDSCDMA_HSDPA");
            LTE_TDD.Tag = LTE_TDD.Text = GetStr("LTE_TDD");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (allempty)
            {
                Registry.CreateKey(RegistryHive.HKLM, @"Software\Microsoft\Shell\OEM\SystemTray\DataConnectionStrings");
            }
            if ((string)UMTS_HSPAPLUS.Tag != UMTS_HSPAPLUS.Text) { SetStr("UMTS_HSPAPLUS", UMTS_HSPAPLUS.Text); }
            if ((string)TDSCDMA_DC_HSPAPLUS.Tag != TDSCDMA_DC_HSPAPLUS.Text) { SetStr("TDSCDMA_DC_HSPAPLUS", TDSCDMA_DC_HSPAPLUS.Text); }
            if ((string)TDSCDMA_DEFAULT.Tag != TDSCDMA_DEFAULT.Text) { SetStr("TDSCDMA_DEFAULT", TDSCDMA_DEFAULT.Text); }
            if ((string)EVDO_REVB.Tag != EVDO_REVB.Text) { SetStr("EVDO_REVB", EVDO_REVB.Text); }
            if ((string)EVDO_REVA.Tag != EVDO_REVA.Text) { SetStr("EVDO_REVA", EVDO_REVA.Text); }
            if ((string)TDSCDMA_HSUPA.Tag != TDSCDMA_HSUPA.Text) { SetStr("TDSCDMA_HSUPA", TDSCDMA_HSUPA.Text); }
            if ((string)UMTS_HSUPA.Tag != UMTS_HSUPA.Text) { SetStr("UMTS_HSUPA", UMTS_HSUPA.Text); }
            if ((string)UMTS_UMTS.Tag != UMTS_UMTS.Text) { SetStr("UMTS_UMTS", UMTS_UMTS.Text); }
            if ((string)_1XRTT.Tag != _1XRTT.Text) { SetStr("1XRTT", _1XRTT.Text); }
            if ((string)TDSCDMA_UMTS.Tag != TDSCDMA_UMTS.Text) { SetStr("TDSCDMA_UMTS", TDSCDMA_UMTS.Text); }
            if ((string)EVDO_REV0.Tag != EVDO_REV0.Text) { SetStr("EVDO_REV0", EVDO_REV0.Text); }
            if ((string)TDSCDMA_HSPAPLUS.Tag != TDSCDMA_HSPAPLUS.Text) { SetStr("TDSCDMA_HSPAPLUS", TDSCDMA_HSPAPLUS.Text); }
            if ((string)_1XRTT_DEFAULT.Tag != _1XRTT_DEFAULT.Text) { SetStr("1XRTT_DEFAULT", _1XRTT_DEFAULT.Text); }
            if ((string)LTE_DEFAULT.Tag != LTE_DEFAULT.Text) { SetStr("LTE_DEFAULT", LTE_DEFAULT.Text); }
            if ((string)UMTS_DEFAULT.Tag != UMTS_DEFAULT.Text) { SetStr("UMTS_DEFAULT", UMTS_DEFAULT.Text); }
            if ((string)GSM_DEFAULT.Tag != GSM_DEFAULT.Text) { SetStr("GSM_DEFAULT", GSM_DEFAULT.Text); }
            if ((string)LTE_FDD.Tag != LTE_FDD.Text) { SetStr("LTE_FDD", LTE_FDD.Text); }
            if ((string)UMTS_DC_HSPAPLUS.Tag != UMTS_DC_HSPAPLUS.Text) { SetStr("UMTS_DC_HSPAPLUS", UMTS_DC_HSPAPLUS.Text); }
            if ((string)GSM_GPRS.Tag != GSM_GPRS.Text) { SetStr("GSM_GPRS", GSM_GPRS.Text); }
            if ((string)EVDO_DEFAULT.Tag != EVDO_DEFAULT.Text) { SetStr("EVDO_DEFAULT", EVDO_DEFAULT.Text); }
            if ((string)UMTS_HSDPA.Tag != UMTS_HSDPA.Text) { SetStr("UMTS_HSDPA", UMTS_HSDPA.Text); }
            if ((string)GSM_EDGE.Tag != GSM_EDGE.Text) { SetStr("GSM_EDGE", GSM_EDGE.Text); }
            if ((string)TDSCDMA_HSDPA.Tag != TDSCDMA_HSDPA.Text) { SetStr("TDSCDMA_HSDPA", TDSCDMA_HSDPA.Text); }
            if ((string)LTE_TDD.Tag != LTE_TDD.Text) { SetStr("LTE_TDD", LTE_TDD.Text); }
            MessageBox.Show("Changes will be visible after a reboot.");
        }
        string GetStr(string value)
        {
            string ret = Registry.ReadString(RegistryHive.HKLM, @"Software\Microsoft\Shell\OEM\SystemTray\DataConnectionStrings", value);
            //if (Registry.HasError)
            //{
            //    MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)Registry.LastError);
            //}
            if (allempty && ret.Length > 0)
            {
                allempty = false;
            }
            return ret;
        }

        void SetStr(string value, string data)
        {
            Registry.WriteString(RegistryHive.HKLM, @"Software\Microsoft\Shell\OEM\SystemTray\DataConnectionStrings", value, data);
            if (Registry.HasError)
            {
                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)Registry.LastError);
            }
        }
    }
}