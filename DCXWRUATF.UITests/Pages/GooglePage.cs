using DCXWRUATF.UITests.Controls;
using OpenQA.Selenium;

namespace DCXWRUATF.UITests.Pages
{
    public class GooglePage : BasePage
    {
        private readonly IWebDriver _driver;
        public GooglePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public BaseElement SearchInput => new BaseElement(_driver, By.Name("q"));

        public SearchResultPage Search(string lookingFor)
        {
            SearchInput.SetTextInARow(lookingFor);
            SearchInput.SendKeys(Keys.Enter);

            return new SearchResultPage(_driver);
        }
    }
}
