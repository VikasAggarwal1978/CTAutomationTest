using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace CTAutomationTest
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void Initialize(string browserType, string browserVer, int implicitWaitSecs, Size ViewPortResolution, bool Headless)
        {
            if (string.IsNullOrEmpty(browserType))
            {
                browserType = "Chrome";
            }             

            switch (browserType)
            {
                case "Chrome": 
                default:
                    Instance = InitChromeDriver(Headless, browserVer);
                    break;

                case "Firefox": 
                    Instance = InitFirefoxDriver(Headless, browserVer);
                    break;         

                case "IE": 
                    Instance = InitIEDriver(browserVer);
                    break;               
            }       

            if (ViewPortResolution.Height == 0)
            {
                Instance.Manage().Window.Maximize();
            }
            else
            {
                Instance.Manage().Window.Size = ViewPortResolution;

                //Get client frame dimensions to generate correct viewport size
                IJavaScriptExecutor js = (IJavaScriptExecutor)Instance;
                string windowSize = js.ExecuteScript("return (window.outerWidth - window.innerWidth + "
                    + ViewPortResolution.Width.ToString() + ") + ',' + (window.outerHeight - window.innerHeight + "
                    + ViewPortResolution.Height.ToString() + "); ").ToString();

                //Get the values
                int width = int.Parse(windowSize.Split(',')[0]);
                int height = int.Parse(windowSize.Split(',')[1]);

                //Set the window
                Instance.Manage().Window.Size = new Size(width, height);
            }

            Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(implicitWaitSecs));
        }

        public static IWebDriver InitChromeDriver(bool Headless, string browserVer)
        {
            var options = new ChromeOptions();
            if (Headless == true)
            {
                options.AddArguments("--headless");
                options.AddArguments("--disable-gpu");
            }
            options.AddArguments("disable-infobars");           
            options.AddUserProfilePreference("download.default_directory", @"C:\Temp");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            options.SetLoggingPreference("performance", LogLevel.All); //to allow inspection of HTTP response codes

            new DriverManager().SetUpDriver(new ChromeConfig(), browserVer);
            return new ChromeDriver(options);
        }

        public static IWebDriver InitFirefoxDriver(bool Headless, string browserVer)
        {
            var options = new FirefoxOptions();
            if (Headless == true)
            {
                options.AddArgument("--headless");

            }          
            new DriverManager().SetUpDriver(new FirefoxConfig(), browserVer);
            return new FirefoxDriver(options);
        }     

        public static IWebDriver InitIEDriver(string browserVer)
        {
            var options = new InternetExplorerOptions();
            options.EnableNativeEvents = false;
            options.IgnoreZoomLevel = true;
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.EnablePersistentHover = true;

            new DriverManager().SetUpDriver(new InternetExplorerConfig(), browserVer);
            return new InternetExplorerDriver(options);
        }     
       
        public static void Close()
        {
            Instance.Close();
        }

        public static void Quit()
        {
            Instance.Quit();
        }

        public static void TearDownScenario()
        {
            if (Instance == null) return;
            Instance.Close();
            Instance.Quit();         
            Instance = null;
        }
        
        public static void GoTo(string URL)
        {
            Instance.Navigate().GoToUrl(URL);
        }

        public static void HighlightElement(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].style.border='3px solid red'", element);
        }

        public  static void TakeScreenShot(string path, ScreenshotImageFormat format = ScreenshotImageFormat.Png)
        {
            ITakesScreenshot screenshotDriver = Instance as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(path, format);
        }
    }   
}






