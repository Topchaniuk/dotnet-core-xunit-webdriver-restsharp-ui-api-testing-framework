using OpenQA.Selenium;

namespace DCXWRUATF.UITests.Controls
{
    public class ListView : BaseElement
    {
        public ListView(IWebDriver driver, By by, int timeout = 3) : base(driver, by, timeout) { }
        public ListView(IWebDriver driver, string xpath, int timeout = 3) : base(driver, xpath, timeout) { }
        public ListView(IWebDriver driver, BaseElement element) : base(driver, element) { }
    }
}
