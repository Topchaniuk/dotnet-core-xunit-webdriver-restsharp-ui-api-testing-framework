using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Edge.SeleniumTools;
using System.Runtime.InteropServices;

namespace DCXWRUATF.DriverService
{
    public static class WebDriverFactory
    {
        private static string DriverPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static IWebDriver GetLocalWebDriver(Browsers browser, string lang, bool headless = false)
        {
            var platform = GetRuntimePlatform();
            switch (browser)
            {
                case Browsers.Firefox:
                    return GetLocalWebDriver(DriverOptionsFactory.GetFirefoxOptions(lang, headless, platform));

                case Browsers.Chrome:
                    return GetLocalWebDriver(DriverOptionsFactory.GetChromeOptions(lang, headless, platform));

                case Browsers.Edge:
                    return GetLocalWebDriver(DriverOptionsFactory.GetEdgeOptions(lang, headless, platform));

                default:
                    throw new PlatformNotSupportedException($"{browser} is not currently supported.");
            }
        }

        public static IWebDriver GetLocalWebDriver(ChromeOptions options, WindowSizes windowSize = WindowSizes.Maximize)
        {
            IWebDriver driver = new ChromeDriver(DriverPath, options);
            return SetWindowSize(driver, windowSize);
        }

        public static IWebDriver GetLocalWebDriver(FirefoxOptions options, WindowSizes windowSize = WindowSizes.Maximize)
        {
            IWebDriver driver = new FirefoxDriver(DriverPath, options);
            return SetWindowSize(driver, windowSize);
        }

        public static IWebDriver GetLocalWebDriver(EdgeOptions options, WindowSizes windowSize = WindowSizes.Maximize)
        {
            if (!Platform.CurrentPlatform.IsPlatformType(PlatformType.WinNT))
            {
                throw new PlatformNotSupportedException("Microsoft Edge is only available on Microsoft Windows.");
            }

            IWebDriver driver = new EdgeDriver(options);
            return SetWindowSize(driver, windowSize);
        }

        public static IWebDriver SetWindowSize(IWebDriver driver, WindowSizes windowSize)
        {
            Point size;
            switch (windowSize)
            {
                case WindowSizes.Unchanged:
                    return driver;

                case WindowSizes.Maximize:
                    driver.Manage().Window.Maximize();
                    return driver;

                case WindowSizes.Proprietary:
                    driver.Manage().Window.Position = Point.Empty;

                    try
                    {
                        string[] coords = Environment.GetEnvironmentVariable("RESOLUTION").Split(',');
                        size = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                        driver.Manage().Window.Size = new Size(size);
                        return driver;
                    }
                    catch
                    {
                        driver.Manage().Window.Maximize();
                        return driver;
                    }
                    
                default:
                    return driver;
            }
        }

        public static PlatformType GetRuntimePlatform()
        {
            var platform = PlatformType.Windows;
             
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = PlatformType.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = PlatformType.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platform = PlatformType.Mac;
            }

            return platform;
        }
    }
}
