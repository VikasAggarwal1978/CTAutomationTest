using CTAutomationTest.utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace CTAutomationTest.steps
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var DriverType = AppName.GetSection("AppSettings")["Browser"];
            var DriverVersion = AppName.GetSection("AppSettings")["BrowserVersion"];
            bool HeadlessOption = bool.Parse(AppName.GetSection("AppSettings")["HeadlessBrowser"]);
            if (DriverType.Equals("Chrome"))
            {
                DriverVersion = GetChromeBrowserVersion();
            }
            var BrowserSize = GetBrowserSize(AppName.GetSection("AppSettings")["ResponsiveSizing"]);
            Driver.Initialize(DriverType, DriverVersion, 30,BrowserSize,HeadlessOption);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var screenshotPath = AppName.GetSection("AppSettings")["LogOutputPath"];
            Directory.CreateDirectory(screenshotPath);
            Driver.TakeScreenShot(screenshotPath + _scenarioContext.ScenarioInfo.Title);
            Driver.Quit();
        }

        public Size GetBrowserSize(string responsiveSize)
        {
            Size BrowserSize = new Size(0, 0);

            switch (responsiveSize)
            {
                case "MobilePortrait":
                    BrowserSize = new Size(320, 568);
                    break;
                case "MobileLandscape":
                    BrowserSize = new Size(568, 320);
                    break;
                case "TabletPortrait":
                    BrowserSize = new Size(768, 1024);
                    break;
                case "TabletLandscape":
                    BrowserSize = new Size(1024, 768);
                    break;
                case "Desktop":
                    BrowserSize = new Size(1440, 1080);
                    break;
                default: //Desktop maximised
                    BrowserSize = new Size(0, 0);
                    break;
            }

            return BrowserSize;
        }

        public string GetChromeBrowserVersion()
        {
            string DriverVersion = null;

            foreach (Browser browser in Utils.GetBrowsers())
            {
                if (browser.Name.Contains("Chrome"))
                {
                    switch (browser.Version.Split('.')[0])
                    {
                        case "83":
                            DriverVersion = "83.0.4103.39";
                            break;
                        case "84":
                            DriverVersion = "84.0.4147.30";
                            break;
                        case "85":
                            DriverVersion = "85.0.4183.87";
                            break;
                        default:
                            DriverVersion = "Latest";
                            break;
                    }
                }               
            }
            return DriverVersion;
        }
    }
}
