using FluentAssertions;
using Microsoft.Extensions.Configuration;
using DCXWRUATF.DriverService;
using DCXWRUATF.UITests.Pages;
using DCXWRUATF.UITests.Support;
using OpenQA.Selenium;
using System;
using Xunit;

namespace DCXWRUATF.UITets
{
    public class WebTests : IDisposable
    {
        private IWebDriver Driver { get; set; }

        private IConfiguration Configuration => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public void Dispose()
        {
            if (Driver != null) Driver.Quit();
        }

        [Theory]
        [InlineData(Browsers.Chrome, "en")]
        [InlineData(Browsers.Firefox, "en")]
        [InlineData(Browsers.Edge, "en")]
        [InlineData(Browsers.Chrome, "fr")]
        [InlineData(Browsers.Firefox, "fr")]
        [InlineData(Browsers.Edge, "fr")]
        public void Selenium_dev_site_lookup(Browsers browser, string lang)
        {
            Driver = WebDriverFactory.GetLocalWebDriver(browser, lang);
            BasePage StartPage = new BasePage(Driver);
            GooglePage Google = StartPage.Navigate<GooglePage>("https://www.google.com/");
            SearchResultPage SearchResult = Google.Search("webdriver documentation for selenium");
            SearchResult.FirstLinkOfSearchResult.Should().Be("https://www.selenium.dev/documentation/en/webdriver/");
        }

        [Fact]
        public void Verify_unread_Gmail_IMAP()
        {
            using (EmailClient emailClient = new EmailClient().Connect(Configuration["server"], Int32.Parse(Configuration["port"]), true))
            {
                emailClient.Authenticate(Configuration["username"], Configuration["password"]).
                OpenInbox().FindUnread().Should().BeTrue();
            }
        }

    }
}
