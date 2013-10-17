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

namespace wphTweaks
{
    public partial class InternationalSettings : PhoneApplicationPage
    {
        public InternationalSettings()
        {
            InitializeComponent();
            NativeDigitsBox.Tag = NativeDigitsBox.Text = GetStr("sNativeDigits");
            PositiveSignBox.Tag = PositiveSignBox.Text = GetStr("sPositiveSign");
            ListSeparatorBox.Tag = ListSeparatorBox.Text = GetStr("sList");
            DateBox.Tag = DateBox.Text = GetStr("sDate");
            TimeBox.Tag = TimeBox.Text = GetStr("sTime");
            ThousandBox.Tag = ThousandBox.Text = GetStr("sThousand");
            CurrencyFormattingBox.Tag = CurrencyFormattingBox.Text = GetStr("iCurrency");
            LanguageBox.Tag = LanguageBox.Text = GetStr("sLanguage");
            LeadingZeroBox.Tag = LeadingZeroBox.Text = GetStr("iLZero");
            DateBox.Tag = DateBox.Text = GetStr("iDate");
            LongDateBox.Tag = LongDateBox.Text = GetStr("sLongDate");
            ShortDateBox.Tag = ShortDateBox.Text = GetStr("sShortDate");
            DecimalBox.Tag = DecimalBox.Text = GetStr("sDecimal");
            TimeLeadingZeroBox.Tag = TimeLeadingZeroBox.Text = GetStr("iTLZero");
            CountryBox.Tag = CountryBox.Text = GetStr("iCountry");
            CurrencyFormattingBox.Tag = CurrencyFormattingBox.Text = GetStr("sCurrency");
            CountryNameBox.Tag = CountryNameBox.Text = GetStr("sCountry");
            AmBox.Tag = AmBox.Text = GetStr("s1159");
            PmBox.Tag = PmBox.Text = GetStr("s2359");
            CurrencyDigitsBox.Tag = CurrencyDigitsBox.Text = GetStr("iCurrDigits");
            NegativeCurrencyBox.Tag = NegativeCurrencyBox.Text = GetStr("iNegCurr");
            TimeFormatBox.Tag = TimeFormatBox.Text = GetStr("iTime");
            MeasureBox.Tag = MeasureBox.Text = GetStr("iMeasure");
            LocaleIDBox.Tag = LocaleIDBox.Text = GetStr("Locale");
            DigitsBox.Tag = DigitsBox.Text = GetStr("iDigits");
        }

        string GetStr(string value)
        {
            string ret = "";
            bool b = NativeRegistry.ReadString(RegistryHive.HKCU, @"Control Panel\International", value, out ret);
            if (!b)
            {
                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
            }
            return ret;
        }

        bool SetStr(string value, string data)
        {
            bool b = NativeRegistry.WriteString(RegistryHive.HKCU, @"Control Panel\International", value, data);
            if (!b)
            {
                MessageBox.Show("Failed: " + (CSharp___DllImport.Win32ErrorCode)NativeRegistry.GetError());
            }
            return b;
        }

        void SaveChanges()
        {
            if ((string)NativeDigitsBox.Tag != NativeDigitsBox.Text)
            {
                SetStr("sNativeDigits", NativeDigitsBox.Text);
            }
            if ((string)PositiveSignBox.Tag != PositiveSignBox.Text)
            {
                SetStr("sPositiveSign", PositiveSignBox.Text);
            }
            if ((string)ListSeparatorBox.Tag != ListSeparatorBox.Text)
            {
                SetStr("sList", ListSeparatorBox.Text);
            }
            if ((string)DateBox.Tag != DateBox.Text)
            {
                SetStr("sDate", DateBox.Text);
            }
            if ((string)TimeBox.Tag != TimeBox.Text)
            {
                SetStr("sTime", TimeBox.Text);
            }
            if ((string)ThousandBox.Tag != ThousandBox.Text)
            {
                SetStr("sThousand", ThousandBox.Text);
            }
            if ((string)CurrencyFormattingBox.Tag != CurrencyFormattingBox.Text)
            {
                SetStr("iCurrency", CurrencyFormattingBox.Text);
            }
            if ((string)LanguageBox.Tag != LanguageBox.Text)
            {
                SetStr("sLanguage", LanguageBox.Text);
            }
            if ((string)LeadingZeroBox.Tag != LeadingZeroBox.Text)
            {
                SetStr("iLZero", LeadingZeroBox.Text);
            }
            if ((string)DateBox.Tag != DateBox.Text)
            {
                SetStr("iDate", DateBox.Text);
            }
            if ((string)LongDateBox.Tag != LongDateBox.Text)
            {
                SetStr("sLongDate", LongDateBox.Text);
            }
            if ((string)ShortDateBox.Tag != ShortDateBox.Text)
            {
                SetStr("sShortDate", ShortDateBox.Text);
            } if ((string)DecimalBox.Tag != DecimalBox.Text)
            {
                SetStr("sDecimal", DecimalBox.Text);
            } if ((string)TimeLeadingZeroBox.Tag != TimeLeadingZeroBox.Text)
            {
                SetStr("iTLZero", TimeLeadingZeroBox.Text);
            } if ((string)CountryBox.Tag != CountryBox.Text)
            {
                SetStr("iCountry", CountryBox.Text);
            } if ((string)CurrencyFormattingBox.Tag != CurrencyFormattingBox.Text)
            {
                SetStr("sCurrency", CurrencyFormattingBox.Text);
            } if ((string)CountryNameBox.Tag != CountryNameBox.Text)
            {
                SetStr("sCountry", CountryNameBox.Text);
            } if ((string)AmBox.Tag != AmBox.Text)
            {
                SetStr("s1159", AmBox.Text);
            } if ((string)PmBox.Tag != PmBox.Text)
            {
                SetStr("s2359", PmBox.Text);
            } if ((string)CurrencyDigitsBox.Tag != CurrencyDigitsBox.Text)
            {
                SetStr("iCurrDigits", CurrencyDigitsBox.Text);
            } if ((string)NegativeCurrencyBox.Tag != NegativeCurrencyBox.Text)
            {
                SetStr("iNegCurr", NegativeCurrencyBox.Text);
            } if ((string)TimeFormatBox.Tag != TimeFormatBox.Text)
            {
                SetStr("iTime", TimeFormatBox.Text);
            } if ((string)MeasureBox.Tag != MeasureBox.Text)
            {
                SetStr("iMeasure", MeasureBox.Text);
            } if ((string)LocaleIDBox.Tag != LocaleIDBox.Text)
            {
                SetStr("Locale", LocaleIDBox.Text);
            } if ((string)DigitsBox.Tag != DigitsBox.Text)
            {
                SetStr("iDigits", DigitsBox.Text);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }
    }
}