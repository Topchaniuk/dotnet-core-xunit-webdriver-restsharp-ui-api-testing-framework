using DCXWRUATF.UITests.Controls;
using OpenQA.Selenium;

namespace DCXWRUATF.UITests.Pages
{
    public class SearchResultPage : BasePage
    {
        private readonly IWebDriver _driver;

        public SearchResultPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public ListView SearchResultList => new ListView(_driver, By.Id("search"));

        public string FirstLinkOfSearchResult => SearchResultList.FindElement(By.XPath("//div[@class = 'g'][1]/div/div[1]/a")).GetAttribute("href");
    }
}
