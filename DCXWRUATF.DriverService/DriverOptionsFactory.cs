using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.IE;

namespace DCXWRUATF.DriverService
{
    public static class DriverOptionsFactory
    {
        public static ChromeOptions GetChromeOptions(string lang, bool headless = false, PlatformType platformType = PlatformType.Any)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("no-sandbox", "disable-gpu", "disable-infobars", "test-type", $"--lang={lang}");
            if (headless)
            {
                options.AddArgument("headless");
            }

            SetPlatform(options, platformType);
            return options;
        }

        public static FirefoxOptions GetFirefoxOptions(string lang, bool headless = false, PlatformType platformType = PlatformType.Any)
        {
            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("intl.accept_languages", lang);
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            if (headless)
            {
                options.AddArgument("--headless");
            }

            SetPlatform(options, platformType);
            return options;
        }

        public static EdgeOptions GetEdgeOptions(string lang, bool headless = false, PlatformType platformType = PlatformType.Any)
        {
            EdgeOptions options = new EdgeOptions();
            options.UseChromium = true;
            options.AddArguments("disable-gpu", $"--lang={lang}");
            if (headless)
            {
                options.AddArgument("headless");
            }
            SetPlatform(options, platformType);
            return options;
        }

        public static T SetPlatform<T>(T options, PlatformType platformType) where T : DriverOptions
        {
            switch (platformType)
            {
                case PlatformType.Any:
                    return options;

                case PlatformType.WinNT:
                    options.PlatformName = "windows";
                    return options;

                case PlatformType.Linux:
                    options.PlatformName = "linux";
                    return options;

                case PlatformType.Mac:
                    options.PlatformName = "mac";
                    return options;

                default:
                    return options;
            }
        }
    }
}
