using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTAutomationTest.utils
{
    public static class Utils
    {
        public static List<Browser> GetBrowsers()
        {
            RegistryKey browserKeys;
            //on 64bit the browsers are in a different location
            browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            string[] browserNames = browserKeys.GetSubKeyNames();
            var browsers = new List<Browser>();
            for (int i = 0; i < browserNames.Length; i++)
            {
                Browser browser = new Browser();
                RegistryKey browserKey = browserKeys.OpenSubKey(browserNames[i]);
                browser.Name = (string)browserKey.GetValue(null);
                RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
                browser.Path = (string)browserKeyPath.GetValue(null).ToString().StripQuotes();
                RegistryKey browserIconPath = browserKey.OpenSubKey(@"DefaultIcon");
                browser.IconPath = (string)browserIconPath.GetValue(null).ToString().StripQuotes();
                browsers.Add(browser);
                if (browser.Path != null)
                    browser.Version = FileVersionInfo.GetVersionInfo(browser.Path).FileVersion;
                else
                    browser.Version = "unknown";

            }
            return browsers;
        }

        internal static String StripQuotes(this String s)
        {
            if (s.EndsWith("\"") && s.StartsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }
    }

    public class Browser
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string IconPath { get; set; }
        public string Version { get; set; }
    }
}
